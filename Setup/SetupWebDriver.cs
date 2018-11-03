using System;
using System.Collections.Generic;
using BoDi;
using J2BIOverseasOps.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Setup
{
    [Binding]
    public class SetupWebDriver
    {
        private readonly IObjectContainer _container;
        private readonly IRunData _data;

        public SetupWebDriver(IObjectContainer container, IRunData data)
        {
            _container = container;
            _data = data;
        }

        [BeforeScenario(Order = 1)]
        public void RegisterDriver()
        {
            IWebDriver driver;
            //To be enabled if switched to windows authentication
            switch (_data.Browser)
            {
                case "chrome":
                    var options = new ChromeOptions();
                    options.AddArgument("--window-size=1920,1080");
                    if (_data.Headless.ToLower().Equals("true"))
                    {
                        options.AddArgument("--headless");
                    }

                    driver = new ChromeDriver(options);
                    break;

                default:
                    throw new Exception("No valid browser config found");
            }

            if (_data.Headless.ToLower().Equals("false"))
            {
                driver.Manage().Window.Maximize();
            }

            _container.RegisterInstanceAs(driver);
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            SetupAdminRole();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            DeleteTestData();
        }

        private static void DeleteTestData()
        {
            var data = new RunData();
            var apiCall = new ApiCalls(data);
            apiCall.DeleteTestRoles();
            apiCall.DeleteTestForms();
        }

        // checks if the admin role is present, if not then creates one and then assigns the current user to the role
        private static void SetupAdminRole()
        {
            var data = new RunData();
            var apiCall = new ApiCalls(data);
            var expectedRole = data.AdminRole; // get the admin role from config file
            var expectedUserName = data.AdminUserName; // get username from config file
            var roleId= apiCall.CreateRoleIfNotCreated(expectedRole); // create a role if not present
            var allPermissions = apiCall.GetAllPermissions(); // get list of all permissions available
            var numberOfPermissionsForCurrentRole = apiCall.GetNumberOfPermissionsforRole(roleId); // get total number of permissions assigned to a role
            if (allPermissions.Count != numberOfPermissionsForCurrentRole) // check if user has all the permissions
            {
                foreach (var permission in allPermissions)
                {
                    var permissionId = permission.Id;
                    apiCall.AssignAPermissionToRole(permissionId, roleId);
                }
            }

            // check if user exists
            var allUsers = apiCall.GetListOfAllUsers();
            var userId = 0;
            var username = "";
            var userFound = false;
            foreach (var user in allUsers)
            {
                if (string.Equals(user.username, expectedUserName, StringComparison.CurrentCultureIgnoreCase))
                {
                    userId = user.userId;
                    username = user.username;
                    userFound = true;
                    break;
                }
            }
            if (!userFound)
            {
                Assert.Fail($"Could not find user {expectedUserName} from the list of users");
            }

            // check if the role is already assigned to a user
            var roleAssignedToUser = apiCall.GetRoleAssignedToAUsername(username);
            if (roleAssignedToUser.Rolename != expectedRole)
            {
                if (roleAssignedToUser.Rolename != null)
                {
                    var roleIdAssignedToUser = apiCall.GetRoleId(roleAssignedToUser.Rolename);
                    apiCall.UnmapUserFromRole(roleIdAssignedToUser, userId);
                }
            }

            if ( roleAssignedToUser.Rolename != expectedRole)
            { 
                apiCall.MapRoleToUserById(roleId, userId);
            }

            apiCall.AssignDestinationsToUser(null,username,true); // assign all destinations to the user

            // map properties to the user
            var listOfProperties = new List<string> {"Hotel Gran Garbi","Hotel Gran Garbi Mar","Hotel Garbi","Costa Encantada Aparthotel","Rosamar Garden Resort","Hotel Anabel" };
            var destinationId = apiCall.GetDestinationId("BCN");
            var propertiesId = new List<int>();
            foreach (var property in listOfProperties)
            {
                propertiesId.Add(apiCall.GetPropertyId(destinationId, property));
            }
            apiCall.MapPropertiesToAUser(propertiesId, username);
        }
    }
}

    