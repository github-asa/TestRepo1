using System;
using J2BIOverseasOps.Extensions;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Pages.RoleManagement
{
    internal class AssignedRolesToPermPage : RoleMgmtCommon
    {
        private readonly By _assignedEmployeesIcon = By.XPath(".//*[@class='fa fa-users']");
        private readonly By _emptyListMessage = By.XPath("//p-datalist[@emptymessage]");
        private readonly By _filterByRoleNameInput = By.XPath("//input[@inputid='role-name-filter']");
        private readonly By _numberOfPermissions = By.XPath(".//i[@class='fa fa-lock']/..");
        private readonly By _numberOfUsers = By.XPath(".//i[@class='fa fa-users']/..");
        private readonly By _permName = By.XPath(".//span[@permissionname]");
        private readonly By _resetFilterButton = By.Id("resetButton");
        private readonly By _roleNameActiveIndicator = By.XPath(".//*[@title='Active']");
        private readonly By _roleNameInActiveIndicator = By.XPath(".//*[@title='Inactive']");
        private readonly string _roleNameXpath = "//li//div[contains(text(),'rolenamehere')]/..";
        private readonly By _selectedPermOnDrpDwn = By.XPath(".//*[@selected]");
        private readonly By _selectPermDropDown = By.XPath("//p-dropdown[@inputid='select-permission']");
        private readonly By _selectPermDropDownList = By.XPath("//p-dropdown[@inputid='select-permission']//li");
        private int _assignedPermissionsCount;
        private int _assignedUsersCount;

        public AssignedRolesToPermPage(IWebDriver driver, ILog log) : base(driver, log)
        {
        }

        private static string RoleName => ScenarioContext.Current["rolename"].ToString();

        public void SelectPermission(string permission)
        {
            Driver.SelectDropDownOption(_selectPermDropDown, permission);
        }

        public void VerifyFirstPermSelected()
        {
            Driver.ClickItem(_selectPermDropDown);
            Driver.WaitForItem(_selectPermDropDownList);
            var listOfRoles = Driver.FindElements(_selectPermDropDownList);
            var firstPermOnList = listOfRoles[0].FindElement(_permName).GetAttribute("permissionname");
            Driver.ClickItem(_selectPermDropDown); // close dropdown
            Driver.WaitUntilElementNotPresent(_selectPermDropDownList);

            var selectedPermName = Driver.FindElement(_selectPermDropDown).FindElement(_selectedPermOnDrpDwn)
                .GetAttribute("permissionname");
            Assert.True(selectedPermName == firstPermOnList,
                "Selected Role :" + selectedPermName + " was not equal to first permission on the list :" +
                firstPermOnList);
        }

        public void FilterRoleByName(string roleKey = "")
        {
            var roleValue = RoleName;
            if (roleKey != "")
            {
                roleValue = ScenarioContext.Current[roleKey].ToString();
            }

            Driver.EnterText(_filterByRoleNameInput, roleValue);
        }

        // enters text in filter roles by name field
        public void EnterTextFilterRoleByName(string text)
        {
            Driver.EnterText(_filterByRoleNameInput, text);
        }

        public void VerifyFilterByNameReset()
        {
            var filterText = Driver.FindElement(_filterByRoleNameInput).GetAttribute("value");
            Assert.True(filterText == "",
                "Unable to verify filter by role name input text as blank. Actual Value :" + filterText);
        }

        public void VerifyRoleNameDisplayed(string rolenameKey = "")
        {
            var roleValue = RoleName;
            if (rolenameKey != "")
            {
                roleValue = ScenarioContext.Current[rolenameKey].ToString();
            }

            var roleNameElement = By.XPath(_roleNameXpath.Replace("rolenamehere", roleValue));
            Assert.True(Driver.WaitForItem(roleNameElement, 10),
                "Could not find " + roleValue + " On assign employees to roles page");
        }

        public void VerifyRoleNotDisplayed(string roleNameKey = "")
        {
            var roleValue = RoleName;
            if (roleNameKey != "")
            {
                roleValue = ScenarioContext.Current[roleNameKey].ToString();
            }

            var role = By.XPath(_roleNameXpath.Replace("rolename_here", roleValue));
            Assert.True(!Driver.IsElementPresent(role),
                "Found role: " + roleValue +
                " in the list on the assign employees page, while expecting it to be not found");
        }

        public void VerifyRoleStatus(string roleStatus)
        {
            var roleName = By.XPath(_roleNameXpath.Replace("rolenamehere", RoleName));
            Driver.WaitForItem(roleName);
            var roleNameElem = Driver.FindElement(roleName);
            switch (roleStatus)
            {
                case "Active":
                    Assert.True(roleNameElem.IsElementWithinWebElementPresent(_roleNameActiveIndicator),
                        "Unable to confirm the role status as Active");
                    return;
                case "Inactive":
                    Assert.True(roleNameElem.IsElementWithinWebElementPresent(_roleNameInActiveIndicator),
                        "Unable to confirm the role status as Inactive");
                    return;
                default:
                    throw new InvalidOperationException("EXPECTED ROLE STATUS IS INVALID");
            }
        }

        public void ClickResetFilterBtn()
        {
            Driver.ClickItem(_resetFilterButton);
        }

        public void VerifyNoRolesDisplayed()
        {
            Assert.True(Driver.IsElementPresent(_emptyListMessage));
        }

        public void NoteDownNumberOfUsersAssigned()
        {
            _assignedUsersCount = GetNumberOfUsersOnRole();
        }

        public void NoteDownNumberOfPermissionsAssigned()
        {
            _assignedPermissionsCount = GetNumberOfPermissionsOnRole();
        }

        public void VerifyUsersCountIncremented(int incrementValue)
        {
            var expectedValue = _assignedUsersCount + incrementValue;
            var actualValue = GetNumberOfUsersOnRole();
            Assert.True(expectedValue == actualValue,
                "Unable to verify the users count. Expected Value :" + expectedValue + " Actual Value :" + actualValue);
        }

        public void VerifyPermissionCountIncremented(int incrementValue)
        {
            var expectedValue = _assignedPermissionsCount + incrementValue;
            var actualValue = GetNumberOfPermissionsOnRole();
            Assert.True(expectedValue == actualValue,
                "Unable to verify the permissions count. Expected Value :" + expectedValue + " Actual Value :" +
                actualValue);
        }

        public void VerifyUsersCountDecremented(int decrementValue)
        {
            var expectedValue = _assignedUsersCount - decrementValue;
            var actualValue = GetNumberOfUsersOnRole();
            Assert.True(expectedValue == actualValue,
                "Unable to verify the users count. Expected Value :" + expectedValue + " Actual Value :" + actualValue);
        }

        public void VerifyPermissionsCountDecremented(int decrementValue)
        {
            var expectedValue = _assignedPermissionsCount - decrementValue;
            var actualValue = GetNumberOfPermissionsOnRole();
            Assert.True(expectedValue == actualValue,
                "Unable to verify the permissions count. Expected Value :" + expectedValue + " Actual Value :" +
                actualValue);
        }

        #region Helpers

        private int GetNumberOfUsersOnRole()
        {
            var roleNameElement = By.XPath(_roleNameXpath.Replace("rolenamehere", RoleName));
            Driver.WaitForItem(roleNameElement);
            var roleNameWebEl = Driver.FindElement(roleNameElement);
            if (roleNameWebEl.IsElementWithinWebElementPresent(_numberOfUsers))
            {
                return int.Parse(roleNameWebEl.FindElement(_numberOfUsers).Text);
            }

            throw new NoSuchElementException("Could not find number of users assigned to a role icon ");
        }

        private int GetNumberOfPermissionsOnRole()
        {
            var roleNameElement = By.XPath(_roleNameXpath.Replace("rolenamehere", RoleName));
            Driver.WaitForItem(roleNameElement);
            var roleNameWebEl = Driver.FindElement(roleNameElement);
            if (roleNameWebEl.IsElementWithinWebElementPresent(_numberOfPermissions))
            {
                return int.Parse(roleNameWebEl.FindElement(_numberOfPermissions).Text);
            }

            throw new NoSuchElementException("Could not find number of permissions assigned to a role icon ");
        }

        #endregion
    }
}