using System;
using System.Collections.Generic;
using System.Linq;
using J2BIOverseasOps.Extensions;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Pages.UserManagement
{
    internal class AssignPropToUsersPage : CommonPageElements
    {
        private static readonly string _individualPermissionButton = "//p-datatable//tbody//tr[property]//td[user]";
        private static readonly string _accessGrantedBtn = "//a[@class='userGreen']";
        private static readonly string _accessRevokedBtn = "//a[@class='userRed']";
        private static readonly string _applyFilterLink = "//p-selectbutton//*[@class][(text()='filterName')]";

        // apply filter
        private readonly By _applyFilterContainer = By.XPath("//div[@id='ui-accordiontab-0-content']");
        private readonly By _applyFilterExpandLink = By.Id("ui-accordiontab-0");

        // property
        private readonly By _propertyMultiList = By.XPath("//strong[contains(text(),'Select Property')]/../..//p-multiselect");
        private readonly By _propertyNameInput = By.XPath("//thead//input");
        private readonly By _propertyNameTable = By.XPath("//app-user-property-grid//tr//td[1]//span");

        // Destination
        private readonly By _propToUsersTab =
            By.XPath("//span[contains(@class,'ui-tabview-title')][contains(text(),'Assign Properties to User')]/..");

        //resorts
        private readonly By _resortMultiList = By.XPath("//strong[contains(text(),'Select Resort')]/../..//p-multiselect");
        private readonly By _selectDestDropDwn = By.XPath("//p-dropdown[@placeholder='Select a Destination']");
        private readonly By _selectDestinationDrpDwnList =
            By.XPath("//p-dropdown[@placeholder='Select a Destination']");
        private readonly By _selectedItemBadgeDropDownList = By.XPath("//span[@class='badge selectedItems']");
        private readonly By _selectedItemBadgeMultiList =
            By.XPath("//span[@class='badge selectedItems ng-star-inserted']");
        private readonly By _selectPropertyListItems = By.XPath("//*[@name='Select Property']/../..//li");
        private readonly By _selectResortListItems = By.XPath("//*[@name='Select Resort']/../..//li");
        private readonly By _selectUserListItems = By.XPath("//*[@name='Select User']/../..//li");
        private readonly By _tooltipText = By.CssSelector(".ui-tooltip-text ui-shadow ui-corner-all");

        //user
        private readonly By _userMultiList = By.XPath("//strong[contains(text(),'Select User')]/../..//p-multiselect");

        // grid table
        private readonly By _userPropertyGrid = By.XPath("//app-user-property-grid");
        private readonly By _usersNameTable = By.XPath("//app-user-property-grid//th//span");

        public AssignPropToUsersPage(IWebDriver driver, ILog log) : base(driver, log)
        {
        }

        public void NavigateToPropToUserTab()
        {
            WaitForSpinnerToDisappear();
            Driver.ClickItem(_propToUsersTab);
            WaitForSpinnerToDisappear();
        }

        public void VerifyMultiListBadgeItems(string expectedItems)
        {
            var listOfActualBadgeItems = Driver.FindElements(_selectedItemBadgeMultiList);
            var badgeItemText = new List<string>();
            foreach (var badgeItem in listOfActualBadgeItems)
            {
                badgeItemText.Add(badgeItem.Text);
            }

            var listOfExpectedBadgeItems = expectedItems.ConvertStringIntoList();
            foreach (var item in listOfExpectedBadgeItems)
            {
                Assert.True(badgeItemText.Contains(item), "Unable to verify :" + item + " On the selected items badge");
            }
        }

        public void VerifyUserMultiListBadgeItems(string userindex)
        {
            var listOfActualBadgeItems = Driver.FindElements(_selectedItemBadgeMultiList);
            var badgeItemText = new List<string>();
            foreach (var badgeItem in listOfActualBadgeItems)
            {
                badgeItemText.Add(badgeItem.Text);
            }
            var listOfExpectedBadgeItems = userindex.ConvertStringIntoList();
            foreach (var userIndex in listOfExpectedBadgeItems)
            {
                _currentUser = _listOfUsers[Convert.ToInt32(userIndex)];
                Assert.True(badgeItemText.Contains($"{_currentUser.forename} {_currentUser.surname}"), "Unable to verify user:" + _currentUser.forename + " On the selected items badge");
            }
        }

        

        #region Filter By

        public void ClickPropertiesFilter(string filterName)
        {
            if (Driver.FindElement(_applyFilterContainer).GetAttribute("aria-hidden") == "true")
            {
                Driver.ClickItem(_applyFilterExpandLink);
            }

            var filterLinkXpath = By.XPath(_applyFilterLink.Replace("filterName", filterName));
            Driver.ClickItem(filterLinkXpath);
        }

        #endregion


        #region Destination

        public void SelectDestination(string destination)
        {
            Driver.SelectDropDownOption(_selectDestinationDrpDwnList, destination,true);

        }

        public void VerifyDestinationBadgeItem(string expectedDestination)
        {
            Assert.True(Driver.FindElement(_selectedItemBadgeDropDownList).Text.Contains(expectedDestination));
        }

        #endregion

        #region Resorts

        public void SelectResorts(string resorts)
        {
            var resortsToSelectList = resorts.ConvertStringIntoList();
            Driver.SelectMultiselectOption(_resortMultiList, resortsToSelectList,true);
        }


        public void VerifyResortsListEmpty()
        {
            Assert.True(Driver.FindElements(_selectResortListItems).Count == 0,
                "Expected List of resorts was not empty");
        }

        #endregion

        #region Properties

        public void SelectProperties(string properties)
        {
            var propertiesToSelectList = properties.ConvertStringIntoList();
            Driver.SelectMultiselectOption(_propertyMultiList, propertiesToSelectList,true);
        }


        public void VerifyPropertiesListEmpty()
        {
            Assert.True(Driver.FindElements(_selectPropertyListItems).Count == 0,
                "Expected List of Properties was not empty");
        }

        #endregion

        #region Users

        public void SelectUsers(string userIndex)
        {
            var usersToSelectList = userIndex.ConvertStringIntoList();
            foreach (var index in usersToSelectList)
            {
               var user= GetListOfUsers()[Convert.ToInt32(index)];
                Driver.SelectMultiselectPartialOption(_userMultiList, $"{user.forename} {user.surname}");
            }

        }

        public void VerifyUserPresentInTheList(int userindex)
        {
            _currentUser = _listOfUsers[userindex];
            Assert.True(Driver.IsPartialStringPresentInMultiSelectOption(_userMultiList,_currentUser.username),
                "Unable to find user :" + _currentUser.forename + _currentUser.surname + " in the list of users when user should be in the list.");
        }

        public void VerifyUserNotPresentInTheList(int index)
        {
            _currentUser = _listOfUsers[index];
            Assert.True(!Driver.IsPartialStringPresentInMultiSelectOption(_userMultiList, _currentUser.username),
                "Was able to find user :" + _currentUser.forename + _currentUser.surname + " in the list of users when user should not be in the list");
        }

        public void VerifyUsersListEmpty()
        {
            Assert.True(Driver.FindElements(_selectUserListItems).Count == 0, "Expected List of Users was not empty");
        }

        public void VerifyNoComponentsDisplayed()
        {
            Assert.True(!Driver.IsElementPresent(_propToUsersTab),
                "Assign Properties to User tab was displayed, while expecting it not to be there.");
            Assert.True(!Driver.IsElementPresent(_selectDestDropDwn),
                "_selectDestDropDwn was displayed on Assign Properties to Users Tab while expecting it not to be displayed");
            Assert.True(!Driver.IsElementPresent(_resortMultiList),
                "_resortMultiList was displayed on Assign Properties to Users Tab while expecting it not to be displayed");
            Assert.True(!Driver.IsElementPresent(_selectResortListItems),
                "_selectResortListItems was displayed on Assign Properties to Users Tab while expecting it not to be displayed");
            Assert.True(!Driver.IsElementPresent(_propertyMultiList),
                "_propertyMultiList  was displayed on Assign Properties to Users Tab while expecting it not to be displayed");
            Assert.True(!Driver.IsElementPresent(_userMultiList),
                "_userMultiList  was displayed on Assign Properties to Users Tab while expecting it not to be displayed");
            Assert.True(!Driver.IsElementPresent(_userPropertyGrid),
                "_userPropertyGrid was displayed on Assign Properties to Users Tab while expecting it not to be displayed");
        }

        #endregion


        #region TableGrid

        public void VerifyPropertiesToUsersTableDisplayed()
        {
            Assert.True(Driver.WaitForItem(_userPropertyGrid));
        }

        public void VerifyPropertiesInTable(string properties)
        {
            var expectedListOfProperties = properties.ConvertStringIntoList();
            var actualListOfProperties = GetPropertiesFromUserTable();
            Assert.True(actualListOfProperties.All(expectedListOfProperties.Contains),
                "Expected List of properties were not equal to Actual List ");
        }

        public void VerifyUsersInTable(string userindexes)
        {
            var expectedListOfUsersIndexes = userindexes.ConvertStringIntoList();
            var listOfExpectedUser = GetListOfUsers(expectedListOfUsersIndexes);
            var actualListOfUsers = GetUsersFromUserTable();

            foreach (var user in listOfExpectedUser)
            {
                Assert.True(actualListOfUsers.Contains($"{user.forename} {user.surname}"),
                    "Expected List of users were not equal to Actual List of users");
            }
        }

        public void SearchProperty(string propertyName)
        {
            Driver.EnterText(_propertyNameInput, propertyName);
            var loopCounter = 0;
            do
            {
                loopCounter++;
                if (loopCounter == 100)
                {
                    Assert.Fail("Failed on while loop SearchProperty()");
                }
            } while (Driver.FindElements(_propertyNameTable).Count != 1); // wait for search result to be displayed
        }


        public void RevokeAllPermissions()
        {
            var loopCounter = 0;
            do
            {
                var listOfPermissionsGranted = GetListOfGrantedPermissions();
                foreach (var permission in listOfPermissionsGranted)
                {
                    try
                    {
                        Driver.WaitUntilElementNotDisplayed(_tooltipText);
                        Driver.ClickItem(permission);
                        Driver.WaitUntilElementNotDisplayed(_tooltipText);
                    }
                    catch (StaleElementReferenceException e)
                    {
                        Console.WriteLine(e);
                    }
                }

                loopCounter++;
                if (loopCounter == 100)
                {
                    Assert.Fail("Failed on SearchProperty()");
                }

            } while (GetListOfGrantedPermissions().Count != 0);
        }

        public void VerifyAllPermissionsRevoked()
        {
            Assert.True(GetListOfGrantedPermissions().Count == 0,
                "Unable to verify the number of revoked permissions as 0,Current list of permissions granted is :" +
                GetListOfGrantedPermissions().Count);
        }

        public void GrantRevokePermission(Table table)
        {
            foreach (var row in table.Rows)
            {
                var userIndex = row["userindex"];
                _currentUser = _listOfUsers[Convert.ToInt32(userIndex)];
                var propertyName = row["propertyname"];
                var permission = row["permission"];
                var propertyRowNumber = GetRowNumberForGivenProperty(propertyName);
                var userColumnNumber = GetColumnNumberForGivenUser($"{_currentUser.forename} {_currentUser.surname}");
                var propertyRow = _individualPermissionButton.Replace("property", propertyRowNumber);
                var permissionButton = propertyRow.Replace("user", userColumnNumber);
                switch (permission)
                {
                    case "Grant":
                        GrantAccess(permissionButton);
                        break;
                    case "Revoke":
                        RevokeAccess(permissionButton);
                        break;
                    default:
                        throw new Exception(permission + " is not a valid permission!");
                }
            }
        }

        public void VerifyUserToPropertiesPermissions(Table table)
        {
            foreach (var row in table.Rows)
            {
                var userIndex = row["userindex"];
                _currentUser = _listOfUsers[Convert.ToInt32(userIndex)];
                var propertyName = row["propertyname"];
                var permission = row["permission"];
                var propertyRowNumber = GetRowNumberForGivenProperty(propertyName);
                var userColumnNumber = GetColumnNumberForGivenUser($"{_currentUser.forename} {_currentUser.surname}");
                var propertyRow = _individualPermissionButton.Replace("property", propertyRowNumber);
                var permissionButton = propertyRow.Replace("user", userColumnNumber);
                switch (permission)
                {
                    case "Grant":
                        VerifyGrantAccess(permissionButton);
                        break;
                    case "Revoke":
                        VerifyRevokeAccess(permissionButton);
                        break;
                    default:
                        throw new Exception(permission + " is not a valid permission!");
                }
            }
        }

        #endregion


        #region Helpers

        internal void GrantAccess(string xpath)
        {
            var grantAccessBtnXpath = By.XPath(xpath + _accessRevokedBtn);
            if (Driver.IsElementPresent(grantAccessBtnXpath))
            {
                Driver.ClickItem(grantAccessBtnXpath);
            }
        }

        internal void VerifyGrantAccess(string xpath)
        {
            var grantAccessBtnXpath = By.XPath(xpath + _accessGrantedBtn);
            Assert.True(Driver.IsElementPresent(grantAccessBtnXpath),
                "Unable to verify the access of :" + xpath + " as Grant");
        }

        internal void RevokeAccess(string xpath)
        {
            var revokedAccessBtnXpath = By.XPath(xpath + _accessGrantedBtn);
            if (Driver.IsElementPresent(revokedAccessBtnXpath))
            {
                Driver.ClickItem(revokedAccessBtnXpath);
            }
        }

        internal void VerifyRevokeAccess(string xpath)
        {
            var revokeAccessBtnXpath = By.XPath(xpath + _accessRevokedBtn);
            Assert.True(Driver.IsElementPresent(revokeAccessBtnXpath),
                "Unable to verify the access of :" + xpath + " as Revoke");
        }

        internal string GetRowNumberForGivenProperty(string propertyName)
        {
            var listOfProperties = Driver.FindElements(_propertyNameTable);
            for (var i = 0;
                i < listOfProperties.Count;
                i++) // first two indeces contain the header name so starts from index 2
            {
                if (listOfProperties[i].Text != propertyName) continue;
                i++;
                return i.ToString();
            }

            throw new InvalidOperationException("Unable to find the property :" + propertyName +
                                                " from the table grid");
        }

        internal string GetColumnNumberForGivenUser(string userName)
        {
            var listOfUsers = Driver.FindElements(_usersNameTable);
            for (var i = 0; i < listOfUsers.Count; i++)
            {
                if (listOfUsers[i].Text == userName)
                {
                    return i.ToString();
                }
            }

            throw new InvalidOperationException("Unable to find the user :" + userName + " from the table grid");
        }

        internal IList<IWebElement> GetListOfGrantedPermissions()
        {
            return Driver.FindElements(By.XPath(_accessGrantedBtn));
        }

        internal IList<string> GetUsersFromUserTable()
        {
            var listOfUsers = Driver.FindElements(_usersNameTable);
            var usersList = new List<string>();
            for (var i = 2;
                i < listOfUsers.Count;
                i++) // first two indeces contain the header name so starts from index 2
            {
                usersList.Add(listOfUsers[i].Text);
            }

            return usersList;
        }

        internal IList<string> GetPropertiesFromUserTable()
        {
            var listOfProperties = Driver.FindElements(_propertyNameTable);
            var propertiesList = new List<string>();
            foreach (var property in listOfProperties)
            {
                propertiesList.Add(property.Text);
            }

            return propertiesList;
        }

        #endregion
    }
}