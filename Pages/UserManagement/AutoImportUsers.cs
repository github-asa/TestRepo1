using System;
using System.IO;
using System.Linq;
using J2BIOverseasOps.Extensions;
using J2BIOverseasOps.Models;
using log4net;
using Newtonsoft.Json;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Pages.UserManagement
{
    internal class AutoImportUsers:CommonPageElements
    {
        private readonly ApiCalls _apiCall;
        private static string _sourceFilePath;
        public AutoImportUsers(IWebDriver driver, ILog log, IRunData rundata) : base(driver, log)
        {
            _apiCall = new ApiCalls(rundata);
        }

        // saves the list of employees in a file
        public void SaveExistingEmployees()
        {
            var response = _apiCall.GetListOfEmployeesDataFeed();
            var outputFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources");
            Directory.CreateDirectory(outputFolderPath);
            _sourceFilePath = Path.Combine(outputFolderPath, "DataFeedEmployees.json");
            File.WriteAllText(_sourceFilePath, response.Content);
        }


        public void PostNewEmployees(string rank)
        {
            if (rank.ToLower()=="existing")
            {
                rank = "";
            }

           var existingDataFeedEmployees = JsonConvert.DeserializeObject<DataFeedEmployees>(File.ReadAllText(_sourceFilePath));
           var newEmployee= _generateRandomEmployee(existingDataFeedEmployees, rank); // generate a new employee based on existing data
            existingDataFeedEmployees.Employees.Employee.Add(newEmployee);
            _apiCall.PostListOfEmployeesDataFeed(existingDataFeedEmployees); // post new employee along with the old employees
            System.Threading.Thread.Sleep(5000);
        }

        private static Employee _generateRandomEmployee(DataFeedEmployees existingDataFeedEmployees,string rank = "")
        {
            var listOfExistingId = existingDataFeedEmployees.Employees.Employee.Select(e => e.EmployeeId);
            var newEmployeeId = 0;
            for (var i = 0; i < 10; i++)
            {
                newEmployeeId = newEmployeeId.GenerateRandomNumber(1000, 9999);

                if (!listOfExistingId.Contains(newEmployeeId))
                {
                    break;
                }
            }

            // get an existing employee
            var employee = existingDataFeedEmployees.Employees.Employee[1];

            var newEmployee = new Employee {EmployeeId = newEmployeeId, Rank = employee.Rank};
            if (rank!="")
            {
                newEmployee.Rank = rank;
            }
            newEmployee.FirstName = "AutoImport";
            newEmployee.LastName = $"User{newEmployeeId.ToString()}";
            newEmployee.UserName = $"DGTEST\\A{newEmployee.LastName}";
            newEmployee.EmailAddress = $"{newEmployee.FirstName}.{newEmployee.LastName}@jet2.com";
            newEmployee.Department = employee.Department;

            ScenarioContext.Current["new_employee"] = newEmployee;
            return newEmployee;
        }


        public void ResetToOldEmployeesData()
        {
            var existingDataFeedEmployees = JsonConvert.DeserializeObject<DataFeedEmployees>(File.ReadAllText(_sourceFilePath));
            _apiCall.PostListOfEmployeesDataFeed(existingDataFeedEmployees); 
        }

        public void GetExpectedRole(string scenarioContextKey)
        {
            var employee = ScenarioContext.Current["new_employee"] as Employee;
            var rank = employee?.Rank;
            var role= _apiCall.GetRoleAssignedToRanks(rank);
            ScenarioContext.Current[scenarioContextKey] = role;
        }

        public void UpdateTitlesToRolesMapping()
        {
            _apiCall.UpdateTitlesToRolesMapping();
        }
    }
}
