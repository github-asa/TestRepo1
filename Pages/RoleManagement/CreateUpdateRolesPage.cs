using System;
using J2BIOverseasOps.Extensions;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Pages.RoleManagement
{
    internal class CreateUpdateRolesPage : RoleMgmtCommon
    {
        private string _roleName;
//        private readonly By _activeFilterBtn = By.XPath("//div[@id='filter-buttons']//*[contains(text(),'Active')]");
//        private readonly By _activeFilterDefault =
//            By.XPath(
//                "//div[@class='ui-button ui-widget ui-state-default ui-button-text-only ng-star-inserted']//*[contains(text(),'Active')]");
        private readonly By _allFilterBtn = By.XPath("//p-selectbutton[@id='show-only-active-switch']");
//        private readonly By _allFilterDefault =
//            By.XPath(
//                "//div[@class='ui-button ui-widget ui-state-default ui-button-text-only ng-star-inserted']//*[contains(text(),'All')]");
        private readonly By _confirmNameChangeBtn = By.XPath(".//*[@icon='fa fa-check']");
        private readonly By _emptyListMessage = By.XPath("//p-datalist[@emptymessage]");
        private readonly By _filterByRoleName = By.Id("role-name-filter");
        private readonly By _filterByRoleNameReadOnly = By.XPath("//*[@id='role-name-filter'][@readonly]");
        private By ActiveInactivePselect=>By.XPath($"//*[@rolename='{_roleName}']//p-selectbutton");
        private readonly By _newRoleBtn = By.Id("newbutton");
        private readonly By _newRoleBtnDisabled = By.XPath("//*[@id='newbutton'][@disabled]");
        private readonly By _newRoleName = By.Id("newRoleName");
        private readonly By _numbOfAssignedEmployees = By.XPath(".//a[@class='numOfUsers']");
        private readonly By _numbOfAssignedEmployeesZero = By.XPath(".//a[@class='numOfUsers warning']");
        private readonly By _numbOfAssignedPermissions = By.XPath(".//*[@class='fa fa-lock']/..");
        private readonly By _resetButton = By.XPath("//button[@id='resetButton']");
        private readonly By _roleNameDuplicateError =
            By.XPath(
                "//div[@class='ui-overlaypanel-content']//div[@class='alert alert-danger error-message ng-star-inserted']");
        private readonly By _roleNameEditMode = By.XPath(".//input[@class='ui-inputtext']");
        private readonly By _rolenameElement = By.XPath("//div[@rolename]");

        private By RoleNameRow=>By.XPath($"//*[@rolename='{_roleName}']");

      //  private readonly string _roleNameXpath = "//*[@rolename='rolenamehere']";
        private readonly By _saveRoleBtn = By.Id("save-button");
        private readonly string _searchFormId = "search-form";
        private readonly string _updateRoleName = " UPDATED";

        public CreateUpdateRolesPage(IWebDriver driver, ILog log) : base(driver, log)
        {
        }

        public void VerifyRolePresent()
        {
            WaitForSpinnerToDisappear();
            CloseGrowlNotification();
            SearchByRoleName(_roleName);
            Assert.True(Driver.WaitForItem(RoleNameRow, 20), "Could not find " + _roleName + " On create roles page");
        }

        public void CreateARandomRole(string name)
        {
            _roleName = name + StringExtensions.GenerateRandomUniqueString();
            ScenarioContext.Current["rolename"] = _roleName;
            CreateRole();
        }

        public void SetCurrentRole(string role)
        {
            _roleName = role;
            ScenarioContext.Current["rolename"] = _roleName;

            if (Driver.WaitForItem(RoleNameRow, 2) == false) // if role is not present
            {
                ClickAllFilterBtn();
                if (Driver.WaitForItem(RoleNameRow, 2)) // check if role is present in All filter
                {
                    UpdatePermissionStatus("Active");
                    CloseGrowlNotification();
                    ClickActiveFilterBtn();
                    CloseGrowlNotification();
                }
                else
                {
                    Log.Info(role + "Role not present, Creating the role");
                    CreateRole();
                }
            }
        }

        //updates the value of the role name to a new value
        public void UpdateRoleValue(string newKey)
        {
            var valueToReplace = ScenarioContext.Current[newKey].ToString();

            if (valueToReplace != "")
            {
                ScenarioContext.Current["rolename"] = valueToReplace;
            }
            else
            {
                throw new Exception("Role name is empty for the key :" + newKey);
            }
        }

        public void CreateDuplicateRole(string roleKey)
        {
            _roleName = ScenarioContext.Current[roleKey].ToString();
            Driver.ClickItem(_newRoleBtn);
            Driver.EnterText(_newRoleName, _roleName);
            Driver.ClickItem(_saveRoleBtn);
        }

        public void VerifyRoleStatus(string roleStatus)
        {
            switch (roleStatus)
            {
                case "Active":
                    Assert.True(Driver.GetSelectedPOption(ActiveInactivePselect)=="Active",
                        "Unable to confirm the role status as Active");
                    return;
                case "Inactive":
                    Assert.True(Driver.GetSelectedPOption(ActiveInactivePselect) == "Inactive",
                        "Unable to confirm the role status as Inactive");
                    return;
                default:
                    throw new InvalidOperationException("EXPECTED ROLE STATUS IS INVALID");
            }
        }

        public void VerifyAssignedEmployees(string expectedNumOfAssignedEmployees)
        {
            Driver.WaitForItem(RoleNameRow);
            var assignedEmployeesXpath = _numbOfAssignedEmployees;
            if (expectedNumOfAssignedEmployees == "0")
            {
                assignedEmployeesXpath = _numbOfAssignedEmployeesZero;
            }

            var numberOfAssignedEmployees =
                Driver.FindElement(RoleNameRow).FindElement(assignedEmployeesXpath).Text.Trim();
            Assert.True(numberOfAssignedEmployees == expectedNumOfAssignedEmployees,
                "Number of assigned employees : " + numberOfAssignedEmployees +
                " are not the same as expected employees " + expectedNumOfAssignedEmployees);
        }

        public void VerifyAssignedPermissions(string expectedNumberOfAssignedPermissions)
        {
            Driver.WaitForItem(RoleNameRow);
            Driver.WaitForItemWithinWebElement(RoleNameRow, _numbOfAssignedPermissions);
            var numberOfAssignedPerms =
                Driver.FindElement(RoleNameRow).FindElement(_numbOfAssignedPermissions).Text.Trim();
            Assert.True(numberOfAssignedPerms == expectedNumberOfAssignedPermissions,
                "Number of assigned permissions :" + numberOfAssignedPerms +
                " are not the same as expected permissions " + expectedNumberOfAssignedPermissions);
        }

        public void UpdateRoleName()
        {
            Driver.ClickItem(RoleNameRow);
            Driver.WaitForItemWithinWebElement(RoleNameRow, _roleNameEditMode);
            Driver.EnterText(Driver.FindElement(RoleNameRow).FindElement(_roleNameEditMode), _updateRoleName);
            Driver.ClickItem(Driver.FindElement(RoleNameRow).FindElement(_confirmNameChangeBtn));
            _roleName = _roleName + _updateRoleName;
        }

        public void UpdatePermissionStatus(string permissionStatus)
        {
            Driver.WaitForItem(RoleNameRow);
            switch (permissionStatus)
            {
                case "Active":
                {
                    Driver.ClickPSelectOption(ActiveInactivePselect, "Active");
                    return;
                }
                case "Inactive":
                   
                    Driver.ClickPSelectOption(ActiveInactivePselect, "Inactive");
                    if (Driver.WaitForItem(ConfirmPopup, 1))
                        {
                            ClickOnConfirmationPopup("Yes");
                            Driver.WaitUntilElementNotDisplayed(ConfirmPopup);
                        }
                    return;
                default:
                    throw new InvalidOperationException("EXPECTED ROLE STATUS IS INVALID");
            }
        }


        public void ClickInactiveRole()
        {
            Driver.WaitForItem(RoleNameRow);    
            Driver.ClickPSelectOption(ActiveInactivePselect, "Inactive");
        }

        public void ClickAllFilterBtn()
        {
            Driver.ScrollElementIntoViewById(_searchFormId);
            Driver.ClickPSelectOption(_allFilterBtn,"All");
        }

        public void ClickActiveFilterBtn()
        {
            Driver.ScrollElementIntoViewById(_searchFormId);
            Driver.ClickPSelectOption(_allFilterBtn, "Active");
        }

        public void SearchByRoleName(string textToType)
        {
            if (textToType == "role_name")
            {
                textToType = _roleName;
            }

            Driver.EnterText(_filterByRoleName, textToType);
        }

        public void VerifyAllRolesActive()
        {
            var listOfRoleStatus= Driver.FindElements(By.XPath("//p-datalist//p-selectbutton"));
            foreach (var role in listOfRoleStatus)
            {
                var selectedRoleStatus = Driver.GetSelectedPOption(role);
                Assert.AreEqual("Active", selectedRoleStatus);   
            }
        }

        public void VerifyRoleNotDisplayed()
        {
            CloseGrowlNotification();
            WaitForSpinnerToDisappear();
            Assert.True(!Driver.IsElementPresent(RoleNameRow));
        }

        public void VerifyRolesListEmpty()
        {
            Assert.True(Driver.IsElementPresent(_emptyListMessage));
        }

        public void ClickResetButton()
        {
            Driver.ClickItem(_resetButton);
        }

        public void NoteDownRoleNameAs(string rolename)
        {
            ScenarioContext.Current[rolename] = ScenarioContext.Current["rolename"].ToString();
        }

        public void VerifyRoleCreationText(string text)
        {
            if (ScenarioContext.Current.ContainsKey("Name"))
            {
                var name = ScenarioContext.Current.Get<string>("Name");
                text = text.Replace("Random", name);
            }

            VerifyTextOnGrowlNotification(text.Replace("role_name", _roleName));
        }

        public void VerifyRoleBeingUsedErrorMessage()
        {
            Assert.True(Driver.IsElementPresent(_roleNameDuplicateError));
        }

        public void VerifyFilterByRoleNameDisabled()
        {
            Driver.WaitForItem(_filterByRoleName);
            Assert.True(Driver.IsElementPresent(_filterByRoleNameReadOnly),
                $"Unable to verify filter by role name as read only");
        }

        public void VerifyAddRolesButtonDisabled()
        {
            Driver.WaitForItem(_newRoleBtn);
            Assert.True(Driver.IsElementPresent(_newRoleBtnDisabled),
                $"Unable to verify Create new role button as disabled");
        }

        public void VerifyActiveFilterNotDisplayed()
        {
            Assert.True(!Driver.IsElementPresent(_allFilterBtn),
                $"Was able to see the all filter button while expecting it not to be there");
        }

        public void VerifyResetButtonNotDisplayed()
        {
            Assert.True(!Driver.IsElementPresent(_resetButton), "Was able to see the reset button while expexcting it not to be there");
        }

        #region Helpers

        private void CreateRole()
        {
            WaitForSpinnerToDisappear();
            Driver.ClickItem(_newRoleBtn);
            WaitForSpinnerToDisappear();
            Driver.EnterText(_newRoleName, _roleName);
            Driver.ClickItem(_saveRoleBtn);
            WaitForSpinnerToDisappear();
        }
        #endregion


        public void ClickRoleMgmtBackButton()
        {
            Driver.ClickItem(BackButton);
        }
    }
}