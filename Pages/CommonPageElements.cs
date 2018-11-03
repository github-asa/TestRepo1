using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using J2BIOverseasOps.Extensions;
using J2BIOverseasOps.Models;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;


namespace J2BIOverseasOps.Pages
{
    internal class CommonPageElements : BasePage
    {
        protected static List<UserMgmtApi.Users> _listOfUsers;
        public static UserMgmtApi.Users _currentUser;

       
        private readonly By _helpTextIcon = By.CssSelector(".fa-info-circle");
        private readonly By _helpTextOverlayPanel = By.CssSelector(".ui-overlaypanel>div");
        private readonly By _roleManagementLink = By.LinkText("Role Management");
        private readonly By _userManagementLink = By.LinkText("User Management");
        public readonly By ConfirmButtons = By.CssSelector("p-confirmDialog div button");
        public readonly By ConfirmPopup = By.CssSelector("p-confirmDialog div");
        public readonly By ConfirmPopupMessage = By.CssSelector("p-confirmDialog div .ui-confirmdialog-message");
        public readonly By ConfirmPopupTitle = By.CssSelector("p-confirmDialog div .ui-dialog-title");
        public readonly By DialogButtons = By.CssSelector("p-dialog div button");
        public readonly By DialogPopup = By.CssSelector("p-dialog div");
        public readonly By DialogPopupMessage = By.CssSelector("p-dialog div p");
        public readonly By DialogPopupTitle = By.CssSelector("p-dialog div .ui-dialog-title");
        public readonly By GrowlItem = By.XPath("//div[@class='ui-growl-item']");
        private readonly By _backButton = By.Id("backButton");

        private readonly By _validationError = By.CssSelector(".error-message");
        public readonly By GrowlNotificationClose = By.XPath("//*[@class='ui-growl-icon-close pi pi-times']");

        private readonly By _forbiddenErrorMessageContainer =
            By.XPath("//*[contains(@class,'ui-message-error')]");

        private readonly ApiCalls _apiCall;
        private IRunData rundata;
        public string Token => ((string) ((IJavaScriptExecutor)Driver).ExecuteScript("return localStorage.getItem('authorizationData_angularClient')")).Replace("\"", "");
  

        public CommonPageElements(IWebDriver driver, ILog log) : base(driver, log)
        {
             rundata = new RunData();
            _apiCall = new ApiCalls(rundata);
        }



        public void NavigateToUserManagement()
        {
            Driver.ClickItem(_userManagementLink);
            WaitForSpinnerToDisappear();
            Driver.VerifyNavigatedToPage("/user-management");
        }

        public void NavigateToRoleManagement()
        {
            Driver.ClickItem(_roleManagementLink);
            WaitForSpinnerToDisappear();
            Driver.VerifyNavigatedToPage("/role-management");
        }

        public void VerifyUserMgmtLinkNotDisplayed()
        {
            Assert.True(!Driver.IsElementPresent(_userManagementLink),
                $"User Management link displayed, while expecting it not to display");
        }

        public void VerifyRoleMgmtLinkNotDisplayed()
        {
            Assert.True(!Driver.IsElementPresent(_roleManagementLink),
                $"Role Management link displayed, while expecting it not to display");
        }

        public void VerifyTextOnGrowlNotification(string expectedText)
        {
            Driver.WaitForItem(GrowlItem);
            var growlItemText = Driver.FindElements(GrowlItem);
            var textVerified = false;
            foreach (var notification in growlItemText)
            {
                if (notification.Text.Contains(expectedText))
                {
                    textVerified = true;
                }
            }

            Assert.True(textVerified, "Unable to verify text " + expectedText + " on growl notification");
        }


        public void VerifySystemErrorNotDisplayed()
        {
            var expectedText = "System Error";
            if (Driver.WaitForItem(GrowlItem, 2))
            {
                var growlItemText = Driver.FindElements(GrowlItem);
                var isErrorDisplayed = false;
                foreach (var notification in growlItemText)
                {
                    if (notification.Text.Contains(expectedText))
                    {
                        isErrorDisplayed = true;
                    }
                }

                Assert.True(!isErrorDisplayed, $"System Error was displayed.");
            }
        }

        public void VerifyValidationErrorNotDisplayed()
        {
            Assert.True(!Driver.IsElementPresent(_validationError),
                $"Was able to find the validation error wen expecting it not to be there.");
        }

        public void WaitForGrowlNotificationToDisappear()
        {
            Driver.WaitUntilElementNotDisplayed(GrowlItem, 30);
        }

        public void CloseGrowlNotification()
        {
            WaitForSpinnerToDisappear();
            if (Driver.IsElementPresent(GrowlNotificationClose))
            {
                try
                {
                    Driver.ClickItemIfExists(GrowlNotificationClose);
                    Driver.WaitUntilElementNotDisplayed(GrowlItem, 5);
                }
                catch (NoSuchElementException e)
                {
                    Console.WriteLine(e);
                }
                catch (StaleElementReferenceException e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        public void ConfirmationPopupDisplayedWithMessage(string title, string message)
        {
            if (Driver.WaitForItem(ConfirmPopupTitle, 1))
            {
                Assert.IsTrue(Driver.WaitForItem(ConfirmPopup), "The confirmation popup was not displayed.");
                Assert.AreEqual(title, Driver.GetText(ConfirmPopupTitle),
                    "The confirmation popup title is not as expected.");
                Assert.AreEqual(message, Driver.GetText(ConfirmPopupMessage).Trim(),
                    "The confirmation popup message is not as expected.");
            }
            else
            {
                Assert.IsTrue(Driver.WaitForItem(DialogPopup), "The confirmation popup was not displayed.");
                Assert.AreEqual(title, Driver.GetText(DialogPopupTitle),
                    "The confirmation popup title is not as expected.");
                Assert.AreEqual(message, Driver.GetText(DialogPopupMessage).Trim(),
                    "The confirmation popup message is not as expected.");
            }
        }

        public void ClickOnConfirmationPopup(string buttonText)
        {
            var locator = Driver.WaitForItem(ConfirmButtons, 1) ? ConfirmButtons : DialogButtons;

            var buttons = Driver.FindElements(locator);
            foreach (var button in buttons)
            {
                if (Driver.GetText(button).Equals(buttonText, StringComparison.InvariantCultureIgnoreCase))
                {
                    Driver.ClickItem(button);
                    break;
                }
            }
        }

        public void VerifyConfirmationPopUpIsDismissed()
        {
            Assert.IsTrue(Driver.WaitUntilElementNotDisplayed(ConfirmPopup),
                "The confirmation popup is not being dismissed.");
            Assert.IsTrue(Driver.WaitUntilElementNotDisplayed(DialogPopup),
                "The confirmation popup is not being dismissed.");
            WaitForSpinnerToDisappear();
        }

        public void VerifyHelpText(string expectedText)
        {
            Driver.WaitForItem(_helpTextIcon);
            var listOfHelpTextIcons = Driver.FindElements(_helpTextIcon); // list of helptext icons

            var helpTextFound = false; // flag to verify if the help text is found
            foreach (var icon in listOfHelpTextIcons) // loop around the help text icon
            {
                Driver.ClickItem(icon,true);
                var listOfHelpText = Driver.FindElements(_helpTextOverlayPanel);
                foreach (var helpText in listOfHelpText) // loop around the Help text content
                {
                    var actualText =Driver.GetText(helpText).RemoveAllSpaces().ToLower().Trim();
                    expectedText = expectedText.ToLower().Trim().RemoveAllSpaces();
                    if (expectedText == actualText)
                    {
                        helpTextFound = true; // if found
                        break;
                    }
                }
                // close the ui panel
                Driver.ClickItem(icon);

                if (helpTextFound)
                {
                    break;
                } // breaking the outer loop 
            }
            Assert.True(helpTextFound, $" Expected text {expectedText} could not be found on the help text icon");
        }


        public void VerifyConfirmationPopupNotDisplayed()
        {
            Assert.IsTrue(Driver.WaitUntilElementNotDisplayed(ConfirmPopup, 5), "The confirmation popup is displayed.");
        }

        #region Helpers

        public void VerifyElementVisibility(string expectedState, By element)
        {
            switch (expectedState)
            {
                case "visible":
                    Assert.True(Driver.IsElementPresent(element),
                        $"Could not confirm that the element {element} is visible ");
                    return;
                case "invisible":
                    Assert.True(!Driver.IsElementPresent(element),
                        $"Could not confirm that the element {element} is Invisible ");
                    return;
                default:
                    Assert.Fail(
                        $"{expectedState} is not a valid state, this function checks if the element is visible or invisible");
                    return;
            }
        }

        public void VerifyElementVisibility(string expectedState, IWebElement element)
        {
            switch (expectedState)
            {
                case "visible":
                    Assert.True(element.Displayed, $"Could not confirm that the element {element} is visible ");
                    return;
                case "invisible":
                    Assert.True(!element.Displayed, $"Could not confirm that the element {element} is Invisible ");
                    return;
                default:
                    Assert.Fail(
                        $"{expectedState} is not a valid state, this function checks if the element is visible or invisible");
                    return;
            }
        }

        public void VerifyElementState(string expectedState, By element)
        {
            switch (expectedState.ToLower())
            {
                case "enabled":
                    Assert.True(Driver.IsElementEnabled(element),
                        $"Could not confirm that the element {element} is enabled ");
                    return;
                case "disabled":
                    Assert.IsFalse(Driver.IsElementEnabled(element),
                        $"Could not confirm that the element {element} is Disabled ");
                    return;
                default:
                    Assert.Fail(
                        $"{expectedState} is not a valid state, this function checks if the element is enabled or Disabled");
                    return;
            }
        }

        public void VerifyElementState(string expectedState, IWebElement element)
        {
            switch (expectedState)
            {
                case "enabled":
                    Assert.True(Driver.IsElementEnabled(element),
                        $"Could not confirm that the element {element} is enabled ");
                    return;
                case "disabled":
                    Assert.False(Driver.IsElementEnabled(element),
                        $"Could not confirm that the element {element} is Disabled ");
                    return;
                default:
                    Assert.Fail(
                        $"{expectedState} is not a valid state, this function checks if the element is enabled or Disabled");
                    return;
            }
        }

        public void VerifyCheckBoxState(string expectedState, By element)
        {
            switch (expectedState)
            {
                case "enabled":
                    Assert.True(Driver.IsElementEnabled(element),
                        $"Could not confirm that the element {element} is enabled ");
                    return;
                case "disabled":
                    Assert.True(Driver.IsElementDisabled(element),
                        $"Could not confirm that the element {element} is Disabled ");
                    return;
                default:
                    Assert.Fail(
                        $"{expectedState} is not a valid state, this function checks if the element is enabled or Disabled");
                    return;
            }
        }

        public void VerifyCheckBoxState(string expectedState, IWebElement element)
        {
            switch (expectedState)
            {
                case "enabled":
                    Assert.True(Driver.IsElementEnabled(element),
                        $"Could not confirm that the element {element} is enabled ");
                    return;
                case "disabled":
                    Assert.True(Driver.IsElementDisabled(element),
                        $"Could not confirm that the element {element} is Disabled ");
                    return;
                default:
                    Assert.Fail(
                        $"{expectedState} is not a valid state, this function checks if the element is enabled or Disabled");
                    return;
            }
        }

        public void VerifyCheckBoxTickState(string expectedState, By element)
        {
            switch (expectedState)
            {
                case "checked":
                    Assert.True(Driver.IsCheckBoxTicked(element),
                        $"Could not confirm that the element {element} is checked ");
                    return;
                case "unchecked":
                    Assert.True(!Driver.IsCheckBoxTicked(element),
                        $"Could not confirm that the element {element} is unchecked ");
                    return;
                default:
                    Assert.Fail(
                        $"{expectedState} is not a valid state, this function checks if the element is enabled or Disabled");
                    return;
            }
        }

        #endregion

        public void VerifyNavigationIconStatus(Table table)
        {
            foreach (var row in table.Rows)
            {
                var navItem = row["navitem"];
                var expectedStatus = row["status"];
                var isDisplayed = Convert.ToBoolean(row["displayed"]);
                Assert.AreEqual(Driver.DoesNavItemStateExist(navItem, expectedStatus), isDisplayed,
                    $"Could not find navItem {navItem} , status as {expectedStatus}, expected to be {isDisplayed}");
            }
        }

        /// <summary>
        /// clicks and verifies the user is navigated to the pages
        /// </summary>
        /// <param name="table"></param>
        public void ClickAndNavigatetoPages(Table table)
        {
            foreach (var row in table.Rows)
            {
                var navItem = row["navitem"];
                var page = row["page"];
                CloseGrowlNotification();
                ClickNavItem(navItem);
                Driver.VerifyNavigatedToPage(page);
            }
        }

        /// <summary>
        /// Clicks an item on navigation bar
        /// </summary>
        /// <param name="nav"> nav item to click</param>
        /// <returns></returns>
        public void ClickNavItem(string nav)
        {
            Assert.True(Driver.DoesNavItemExist(nav), $"Could not find navigation item for {nav}");
            Driver.ClickNavItem(nav);
        }

        // Gets list of users and populates a static list
        internal void PopulateListOfUsers()
        {
            _listOfUsers = _apiCall.GetListOfAllUsers();
        }

        public List<UserMgmtApi.Users> GetListOfUsers()
        {
            return _listOfUsers;
        }

        public List<UserMgmtApi.Users> GetListOfUsers(List<string> indexes)
        {
            var listOfUsers = new List<UserMgmtApi.Users>();
            foreach (var index in indexes)
            {
                listOfUsers.Add(_listOfUsers[Convert.ToInt32(index)]);
            }

            return listOfUsers;
        }

        public void ClickBackButton()
        {
            Driver.ClickItem(_backButton);
        }

        public void VerifyForbiddenPageErrorMessage(string expectedErrorMessage)
        {
            var actualText = Driver.GetText(_forbiddenErrorMessageContainer);
            Assert.True(actualText.Contains(expectedErrorMessage),
                $"Actual : {actualText} Expected: {expectedErrorMessage}");
        }

        
        public void CreateRoleAPI(string rolename)
        {
            _apiCall.CreateRoleIfNotCreated(rolename);
        }

        public void MapPermissionsToRole(string role, Table table)
        {
            _apiCall.UnmapAllPermissionsFromRole(role); // removes all permissions from a role
            var allPermissions = _apiCall.GetAllPermissions(); // get list of all permissions available
            foreach (var row in table.Rows)
            {
                var expectedPermission = row["permissions"];
                foreach (var permission in allPermissions)
                {
                    if (expectedPermission.ToLower()=="all") // assign all permissions
                    {
                        _apiCall.MapAPermissionToRole(permission.Name, role);
                    }
                    else
                    {
                        if (string.Equals(permission.Name, expectedPermission, StringComparison.CurrentCultureIgnoreCase))
                        {
                            _apiCall.MapAPermissionToRole(expectedPermission, role);

                        }
                    }
  
                }
            }
        }

        public void MapARoleToUser(string role, string username="")
        {
            if (username=="restricted")
            {
                username = rundata.RestrictedUserName;
            }
            _apiCall.MapRoleToUser(role,username);
        }

        public void MapDestinationsToUsers(string username, Table table)
        {
            if (username == "restricted")
            {
                username = rundata.RestrictedUserName;
            }

            if (username == "loggedin_user")
            {
                username = ScenarioContext.Current[CurrentUsername].ToString();
            }
            var listOfDestinations = table.Rows.ToColumnList("destinations");
            var mapAll = listOfDestinations.Contains("All");
            _apiCall.AssignDestinationsToUser(listOfDestinations, username, mapAll);
        }

        public void UnmapAllDestinationsFromUser(string username)
        {
            if (username == "restricted")
            {
                username = rundata.RestrictedUserName;
            }

            if (username == "loggedin_user")
            {
                username = ScenarioContext.Current[CurrentUsername].ToString();
            }

            _apiCall.RemoveAllDestinationsFromUser(username);
        }

        public void MapPropertiesToAUser(string username, Table table)
        {
            if (username == "restricted")
            {
                username = rundata.RestrictedUserName;
            }
            if (username == "loggedin_user")
            {
                username = ScenarioContext.Current[CurrentUsername].ToString();
            }
            foreach (var row in table.Rows)
            {
                var destination = row["destination"];
                var properties = row["properties"];
                var destinationId=_apiCall.GetDestinationId(destination);
                var listOfProperties = properties.ConvertStringIntoList();
                var propertiesId = new List<int>();
                foreach (var property in listOfProperties)
                {
                    propertiesId.Add(_apiCall.GetPropertyId(destinationId,property));
                }

                _apiCall.MapPropertiesToAUser(propertiesId, username);
            }
        }

        public void UnMapPropertiesToAUser(string username, Table table)
        {
            if (username == "restricted")
            {
                username = rundata.RestrictedUserName;
            }
            if (username == "loggedin_user")
            {
                username = ScenarioContext.Current[CurrentUsername].ToString();
            }
            foreach (var row in table.Rows)
            {
                var destination = row["destination"];
                var properties = row["properties"];
                var destinationId = _apiCall.GetDestinationId(destination);
                var listOfProperties = properties.ConvertStringIntoList();
                var propertiesId = new List<int>();
                foreach (var property in listOfProperties)
                {
                    propertiesId.Add(_apiCall.GetPropertyId(destinationId, property));
                }

                _apiCall.UnMapPropertiesFromAUser(propertiesId, username);
            }
        }

        public void SetCurrentUser(string username)
        {
            if (username== "current_loggedin_user")
            {
                username = ScenarioContext.Current[CurrentUsername].ToString();
            }
            _currentUser= _apiCall.GetUserDetails(username);
        }
    }
}