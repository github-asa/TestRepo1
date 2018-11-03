using System;
using System.Collections.Generic;
using System.Linq;
using J2BIOverseasOps.Extensions;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Pages.RoleManagement
{
    internal class AssignEmployeeToRole : RoleMgmtCommon
    {
        //active filter
        private readonly By _activeFilterBtn =
            By.XPath("//*[@id='show-only-active-switch']//*[contains(text(),'Active')]");
        private readonly By _activeFilterSelected =
            By.XPath(
                "//*[@id='show-only-active-switch']//div[contains(@class, 'ui-state-active')]//*[contains(text(),'Active')]");
        private readonly By _allActiveFilterBtn =
            By.XPath("//*[@id='show-only-active-switch']//*[contains(text(),'All')]");
        private readonly By _allActiveFilterSelected =
            By.XPath(
                "//*[@id='show-only-active-switch']//div[contains(@class, 'ui-state-active')]//*[contains(text(),'All')]");
        private readonly By _assignedAllFilterBtn =
            By.XPath("//*[@id='show-only-assigned-switch']//*[contains(text(),'All')]");
        private readonly By _assignedAllSelected =
            By.XPath(
                "//*[@id='show-only-assigned-switch']//div[contains(@class, 'ui-state-active')]//*[contains(text(),'All')]");

        //assigned filter
        private readonly By _assignedFilterBtn =
            By.XPath("//*[@id='show-only-assigned-switch']//*[contains(text(),'Assigned')]");
        private readonly By _assignedSelected =
            By.XPath(
                "//*[@id='show-only-assigned-switch']//div[contains(@class, 'ui-state-active')]//*[contains(text(),'Assigned')]");
        private readonly By _numberOfUsers = By.XPath(".//a[@class='numOfUsers']");
        private readonly By _numbOfPermissions = By.XPath(".//*[@class='fa fa-lock']/..");
        private readonly By _radioBtnSelected =
            By.XPath("//*[@class='ui-radiobutton-box ui-widget ui-state-default ui-state-active']");
        private readonly By _roleNameActiveIndicator = By.XPath(".//*[@title='Active']");
        private readonly By _roleNameElement = By.XPath("//*[@rolename]");
        private readonly By _roleNameInActiveIndicator = By.XPath(".//*[@title='Inactive']");
        private readonly string _roleNameXpath = "//*[@rolename='rolenamehere']";
        private readonly By _roleSearchTextBox = By.Id("role-name-filter");
        private readonly By _selectRoleRadioBtn = By.XPath(".//p-radiobutton[@name='assigned']//span");
        private readonly By _userAutoCompletionList = By.XPath("//*[@id='user-search-box']/..//ul");
        private readonly By _userSearchTextBox = By.Id("user-search-box");
        protected IRunData RunData;


        public AssignEmployeeToRole(IWebDriver driver, ILog log, IRunData data) : base(driver, log)
        {
            RunData = data;
        }

        private static string RoleName => ScenarioContext.Current["rolename"].ToString();


        public void SearchForUser(string name)
        {
            if (name == "restricted_user")
            {
                name = RunData.RestrictedUserFullName;
            }

            if (name == "adfs_restricted_user")
            {
                name = RunData.RestrictedAdfsUserFullName;
            }


            Driver.Clear(_userSearchTextBox);
            Driver.EnterText(_userSearchTextBox, name);
            Driver.WaitForItem(_userAutoCompletionList);
            var element = Driver.FindElement(_userAutoCompletionList);
            var searchedusers = element.FindElements(By.TagName("li"));
            IWebElement matchedElement = null;

            foreach (var webElement in searchedusers)
            {
                var div = webElement.FindElement(By.TagName("div")).Text;
                if (div.Contains(name))
                {
                    matchedElement = webElement;
                }
            }

            matchedElement?.Click();
        }

        public void FilterRoleByName(string roleKey = "")
        {
            var roleValue = RoleName;
            if (roleKey != "")
            {
                roleValue = ScenarioContext.Current[roleKey].ToString();
            }

            Driver.EnterText(_roleSearchTextBox, roleValue);
            Driver.WaitUntilNumberOfElementsPresent(_roleNameElement, 1); // wait until only one role is displayed
            Driver.WaitForPageToLoad();
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

            var role = By.XPath(_roleNameXpath.Replace("rolenamehere", roleValue));
            Assert.True(!Driver.IsElementPresent(role),
                "Found role: " + roleValue +
                " in the list on the assign employees page, while expecting it to be not found");
        }

        public void AssignRole(string roleKey = "")
        {
            Driver.WaitForPageToLoad();
            var roleValue = RoleName;
            if (roleKey != "")
            {
                roleValue = ScenarioContext.Current[roleKey].ToString();
            }

            var role = By.XPath(_roleNameXpath.Replace("rolenamehere", roleValue));
            Driver.WaitForItem(role);
            Driver.FindElement(role).FindElement(_selectRoleRadioBtn).Click();
            Driver.WaitForPageToLoad();
            WaitForSpinnerToDisappear();
            Assert.True(Driver.WaitForItem(GrowlItem),
                "Could not find growl notification for assign a role to employee");
            Assert.True(Driver.WaitUntilElementIsPresent(_radioBtnSelected),
                "Unable to verify radio button as selected on assign role page");
            WaitForSpinnerToDisappear();
        }

        public void ClickActiveAllFilterBtn()
        {
            if (Driver.IsElementPresent(_allActiveFilterSelected)) return;

            Driver.FindElement(_userSearchTextBox).SendKeys(Keys.PageUp);
            Driver.ClickItem(_allActiveFilterBtn);
            Driver.WaitForItem(_allActiveFilterSelected);
        }

        public void ClickActiveFilterBtn()
        {
            if (Driver.IsElementPresent(_activeFilterSelected)) return;
            Driver.ClickItem(_activeFilterBtn);
            Driver.WaitForItem(_activeFilterSelected);
        }

        public void ClickAssignedFilterBtn()
        {
            if (Driver.IsElementPresent(_assignedSelected)) return;
            Driver.ClickItem(_assignedFilterBtn);
            Driver.WaitForItem(_assignedSelected);
        }

        public void ClickAllAssignedFilterBtn()
        {
            if (Driver.IsElementPresent(_assignedAllSelected)) return;
            Driver.ClickItem(_assignedAllFilterBtn);
            Driver.WaitForItem(_assignedAllSelected);
        }

        public void VerifyRoleStatus(string roleStatus)
        {
            var roleNameElem = Driver.FindElement(By.XPath(_roleNameXpath.Replace("rolenamehere", RoleName)));
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

        public void VerifyAssignedEmployeesCount(string expectedNumberOfEmployees)
        {
            var roleNameElem = By.XPath(_roleNameXpath.Replace("rolenamehere", RoleName));
            Driver.WaitForItemWithinWebElement(roleNameElem, _numberOfUsers);
            var numberOfAssignedEmployees = Driver.FindElement(roleNameElem).FindElement(_numberOfUsers).Text.Trim();
            Assert.True(expectedNumberOfEmployees == numberOfAssignedEmployees,
                " Expected number fo employees :" + expectedNumberOfEmployees +
                " was not same as actual number of employees :" + numberOfAssignedEmployees);
        }

        public void VerifyPermissionsCount(string expectedNumberOfPermissions)
        {
            var roleNameElem = By.XPath(_roleNameXpath.Replace("rolenamehere", RoleName));
            Driver.WaitForItemWithinWebElement(roleNameElem, _numbOfPermissions);
            var numberOfAssignedPerm = Driver.FindElement(roleNameElem).FindElement(_numbOfPermissions).Text.Trim();
            Assert.True(expectedNumberOfPermissions == numberOfAssignedPerm,
                " Expected number fo permissions :" + expectedNumberOfPermissions +
                " was not same as actual number of permissions :" + numberOfAssignedPerm);
        }

        public void VerifyEmployeeNameFilterText(string expectedName)
        {
            var actualText = Driver.FindElement(_userSearchTextBox).GetAttribute("value");
            Assert.True(expectedName == actualText);
        }

        public void VerifyRoleSelected(string roleKey = "")
        {
            var roleValue = RoleName;
            if (roleKey != "")
            {
                roleValue = ScenarioContext.Current[roleKey].ToString();
            }

            var role = By.XPath(_roleNameXpath.Replace("rolenamehere", roleValue));
            Driver.WaitForItem(role);
            Assert.True(Driver.FindElement(role).GetAttribute("class").Contains("selected"));
        }

        public void ClearRoleByNameField()
        {
            Driver.Clear(_roleSearchTextBox);
        }

        public void VerifyRadioButtonNotDisplayed()
        {
            var role = By.XPath(_roleNameXpath.Replace("rolenamehere", RoleName));
            Driver.WaitForItem(role);
            Assert.True(!Driver.WaitForItemWithinWebElement(role, _selectRoleRadioBtn, 2),
                "Was able to find assign role to employee radio button, while expecting it not to be there.");
        }

        public void RemoveRoleUsingApi(string users)
        {
            if (users== "restricted_user")
            {
                users = RunData.RestrictedUserFullName;
            }

            var listOfUsers = users.ConvertStringIntoList();
            var apiCall = new ApiCalls(RunData);
            apiCall.RemoveRolesAssignedToUsers(listOfUsers);
        }
    }
}