using System.Linq;
using J2BIOverseasOps.Extensions;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Pages.UserManagement
{
    internal class UserOverviewPage : CommonPageElements
    {
        private readonly ApiCalls _apiCall;
        private readonly By _userFname = By.XPath("//*[@id='firstnameField']");
        private readonly By _userSurname = By.XPath("//*[@id='surnameField']");
        private readonly By _userJobTitle = By.XPath("//*[@id='jobTitleField']");
        private readonly By _userDepartment = By.XPath("//*[@id='departmentField']");
        private readonly By _userPhone = By.XPath("//*[@id='phoneField']");
        private readonly By _userEmail = By.XPath("//*[@id='emailField']");
       // private readonly By _userDepartmentEditLink = By.XPath("//a[@id='viewEditRole']");
        private readonly By _userDestinationsEditLink = By.XPath("//a[@id='viewEditDestinations']");
        private readonly By _userRole = By.Id("roleField");

        private readonly By _destinationsRows =   By.XPath("//*[@id='destinationField']//div");

        private readonly By _userRoleEditLink = By.XPath("//a[@id='viewEditRole']");
        private readonly By _blockUserButton = By.Id("blockButton");

        public UserOverviewPage(IWebDriver driver, ILog log, IRunData rundata) : base(driver, log)
        {
            _apiCall = new ApiCalls(rundata);

        }

        public void ClickEditViewLink(string link)
        {
            By _linkToClick = null;
            switch (link)
            {
//                case "Department":
//                    _linkToClick = _userDepartmentEditLink;
//                    break;
                case "Destination":
                    _linkToClick = _userDestinationsEditLink;
                    break;
                case "Role":
                    _linkToClick = _userRoleEditLink;
                    break;
                default:
                    Assert.Fail($"{link} is not a valid link");
                    break;
            }

            Driver.ClickItem(_linkToClick);
        }

        public void VerifyDetailsOnOverviewPage(Table table)
        {
            foreach (var row in table.Rows)
            {
                var field = row["field"];
                switch (field.ToLower())
                {
                    case "firstname":
                        Assert.AreEqual(_currentUser.forename, Driver.GetText(_userFname));
                        break;
                    case "surname":
                        Assert.AreEqual(_currentUser.surname, Driver.GetText(_userSurname));
                        break;
                    case "jobtitle":
                        Assert.AreEqual(_currentUser.title, Driver.GetText(_userJobTitle));
                        break;
                    case "department":
                        Assert.AreEqual(_currentUser.department, Driver.GetText(_userDepartment));
                        break;
                    case "phone":
                        Assert.AreEqual(_currentUser.mobile, Driver.GetText(_userPhone));
                        break;
                    case "email":
                        Assert.AreEqual(_currentUser.workEmail, Driver.GetText(_userEmail));
                        break;
                    case "destinations":
                        var destinationsId = _apiCall.GetDestinationsAssignedToAUser(_currentUser.username).DestinationIds.ToList();
                        var listOfExpectedDestinations= _apiCall.GetDestinationIATACode(destinationsId); 
                        if (listOfExpectedDestinations.Count==0)
                        {
                            Assert.True(!Driver.WaitForItem(_destinationsRows,1),"Was able to find destinations when expecting it not to be there");
                        }
                        else
                        {
                            var listOfActualDestinations = Driver.GetTexts(_destinationsRows, true);
                            Assert.True(listOfExpectedDestinations.All(listOfActualDestinations.Contains));
                        }
                        break;
                    case "role":
                        Assert.AreEqual(_currentUser.surname, Driver.GetText(_userSurname));
                        break;
                    default:
                        Assert.Fail($"{field} is not a valid field");
                        break;
                }
            }
        }
        public void VerifyExpectedRole(string key)
        {
            var actualRole=Driver.GetText(_userRole);
            var expectedRole = ScenarioContext.Current[key].ToString();
            Assert.AreEqual(actualRole,expectedRole,$" actual role {actualRole} was not same as expected role {expectedRole}");
        }
    }
}