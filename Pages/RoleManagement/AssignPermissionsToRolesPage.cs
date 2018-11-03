using System.Threading;
using J2BIOverseasOps.Extensions;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Pages.RoleManagement
{
    internal class AssignPermissionsToRolesPage : RoleMgmtCommon
    {
        //assigned filter
        private readonly By _allFilterBtn = By.XPath("//*[@id='show-only-assigned-switch']//*[contains(text(),'All')]");
        private readonly By _allFilterDefault =
            By.XPath(
                "//div[@class='ui-button ui-widget ui-state-default ui-button-text-only ng-star-inserted']//*[contains(text(),'All')]");
        private readonly By _assignedFilterBtn =
            By.XPath("//*[@id='show-only-assigned-switch']//*[contains(text(),'Assigned')]");
        private readonly By _assignedFilterDefault =
            By.XPath(
                "//div[@class='ui-button ui-widget ui-state-default ui-button-text-only ng-star-inserted']//*[contains(text(),'Assigned')]");
        private readonly By _filterByPermissions = By.Id("permissions-name-filter");
        private readonly By _numberOfRoles = By.XPath(".//*[@class='numOfRoles']");
        private readonly By _numberOfRolesZero = By.XPath(".//*[@class='numOfRoles warning']");
        private readonly By _numbOfRolesIcon = By.XPath(".//*[@class='fa fa-user-circle']");
        private readonly By _permissionCheckbox = By.XPath(".//p-checkbox//*[contains(@class,'ui-chkbox-box')]");
        private readonly string _permissionName = "//*[@permissionname='permission_Name_Here']";
        private readonly By _permissions = By.XPath("//*[@permissionname]");
        private readonly By _permissionsTickBoxChecked =
            By.XPath(".//span[@class='ui-chkbox-icon ui-clickable pi pi-check']");
        private readonly By _permissionsToRolePopUp = By.Id("permission-assigned-roles-pop");
        private readonly string _roleNameSelector = "//*[@rolename='role_Name_Here']";
        private readonly By _selectRoleDrpDwn = By.XPath("//p-dropdown[@inputid='select-role']");
        private readonly By _selectRoleDrpDwnList = By.CssSelector(".ui-dropdown-items > li");
        private IRunData runData;
        public AssignPermissionsToRolesPage(IWebDriver driver, ILog log,IRunData rundata) : base(driver, log)
        {
            runData=new RunData();
        }

        public void SelectRoleFromDrpDwn()
        {
            var rolename = ScenarioContext.Current["rolename"].ToString();

            WaitForSpinnerToDisappear();
            Driver.OpenDropDown(_selectRoleDrpDwn);

            var dropdownFilter = By.CssSelector(".ui-dropdown-filter");
            Assert.True(Driver.WaitForItem(dropdownFilter));
            Driver.EnterText(dropdownFilter, rolename);

            var rolenameListItem = By.XPath($"//li//span[@rolename='{rolename}']");
            Driver.ClickItem(rolenameListItem);

            //Close dropdown
            if (!Driver.WaitUntilElementNotPresent(_selectRoleDrpDwnList,3))
            {
                Driver.ClickItem(By.CssSelector("p-dropdown[inputid='select-role'] .ui-dropdown-trigger-icon"));
            }

            Assert.True(Driver.WaitUntilElementNotPresent(_selectRoleDrpDwnList),
                "Could not close the roles drop down menu");
            WaitForSpinnerToDisappear();
            Driver.WaitForPageToLoad();
        }

        public void FilterByPermission(string permission)
        {
            Driver.EnterText(_filterByPermissions, permission);
        }

        public void VerifyFilterByPermissionsReset()
        {
            var filterText = Driver.FindElement(_filterByPermissions).GetAttribute("value");
            Assert.True(filterText == "",
                "Unable to verify filter by name input text as blank. Actual Value :" + filterText);
        }


        public void VerifyListOfPermissionsDisplayed()
        {
            Assert.True(Driver.IsElementPresent(_permissions));
        }

        public void VerifyNoPermissionsAssigned()
        {
            Assert.True(Driver.FindElements(_permissionsTickBoxChecked).Count == 0,
                "Unable to verify all the assigned permissions as 0");
        }

        public void VerifyPermissionsAssigned(Table table)
        {
            foreach (var row in table.Rows)
            {
                var permissionName = row["permissions"];
                var permissionElement = GetPermissionElement(permissionName);
                Assert.True(Driver.WaitForItemWithinWebElement(permissionElement, _permissionsTickBoxChecked, 5),
                    "Unable to Verify " + permissionName + " as selected");
            }
        }

        public void VerifyPermissionDisplayed(string permission)
        {
            var permissionElement = GetPermissionElement(permission);
            Assert.True(Driver.WaitForItem(permissionElement));
        }

        public void AssignPermissionsToRole(Table table)
        {
            foreach (var row in table.Rows)
            {
                var permissionName = row["permissions"];
                var permissionElement = GetPermissionElement(permissionName);
                FilterByPermission(permissionName);
                NoteDownNumberOfRoles(permissionName);
                if (!Driver.FindElement(permissionElement).IsElementWithinWebElementPresent(_permissionsTickBoxChecked))
                {
                    Driver.FindElement(permissionElement).FindElement(_permissionCheckbox).Click();
                    Thread.Sleep(500);
                    WaitForSpinnerToDisappear();
                }
            }
        }

        //removes all the permissions on the given role
        public void RemoveAllPermissions()
        {
            Driver.WaitForItem(_permissions);
            do
            {
                WaitForSpinnerToDisappear();
                var listOfAssignedPermissions = Driver.FindElements(_permissionsTickBoxChecked);
                foreach (var permission in listOfAssignedPermissions)
                {
                    WaitForSpinnerToDisappear();
                    Driver.ClickItem(permission);
                    WaitForSpinnerToDisappear();
                    Driver.WaitUntilElementIsPresent(GrowlItem, 10);
                    WaitForSpinnerToDisappear();
                    Driver.WaitForItem(GrowlItem);
                    WaitForSpinnerToDisappear();
                    CloseGrowlNotification();
                    break;
                }
            } while (Driver.FindElements(_permissionsTickBoxChecked).Count != 0);
        }

        public void RemovePermissionsFromRole(Table table)
        {
            foreach (var row in table.Rows)
            {
                var permissionName = row["permissions"];
                FilterByPermission(permissionName);
                var permissionElement = GetPermissionElement(permissionName);
                NoteDownNumberOfRoles(permissionName);
                if (Driver.FindElement(permissionElement).IsElementWithinWebElementPresent(_permissionsTickBoxChecked))
                {
                    Driver.FindElement(permissionElement).FindElement(_permissionCheckbox).Click();
                    Thread.Sleep(1000);
                }
            }
        }

        public void VerifyRolesIncremented(int incrementValue, Table table)
        {
            foreach (var row in table.Rows)
            {
                var permissionName = row["permissions"];
                FilterByPermission(permissionName);
                var actualNumbOfRoles = int.Parse(GetNumberOfRolesAssigned(permissionName));
                var numbOfRolesString = ScenarioContext.Current[permissionName + "_roles"].ToString();
                Log.Debug($"Number of roles before in string :{numbOfRolesString}");
                var beforeNumbOfRoles = int.Parse(numbOfRolesString);
                var totalNumberOfExpectedRoles = beforeNumbOfRoles + incrementValue;
                Assert.True(totalNumberOfExpectedRoles == actualNumbOfRoles,
                    "Unable to verify the increment value of number of roles. Expected roles :" + beforeNumbOfRoles +
                    incrementValue + " Actual roles :" + actualNumbOfRoles);
            }
        }

        public void VerifyRolesDecremented(int decrementValue, Table table)
        {
            foreach (var row in table.Rows)
            {
                var permissionName = row["permissions"];
                FilterByPermission(permissionName);
                var actualNumbOfRoles = int.Parse(GetNumberOfRolesAssigned(permissionName));
                var numbOfRolesString = ScenarioContext.Current[permissionName + "_roles"].ToString();
                Log.Debug($"Number of roles before in string :{numbOfRolesString}");
                var beforeNumbOfRoles = int.Parse(numbOfRolesString);
                Assert.True(beforeNumbOfRoles - decrementValue == actualNumbOfRoles,
                    "Unable to verify the value of number of roles. Expected roles :" +
                    (beforeNumbOfRoles - decrementValue) + " Actual roles :" + actualNumbOfRoles);
            }
        }

        public void ClickRolesIcon(string permissionName)
        {
            var permissionElem = GetPermissionElement(permissionName);
            Driver.WaitForItemWithinWebElement(permissionElem, _numbOfRolesIcon);
            Driver.FindElement(permissionElem).FindElement(_numbOfRolesIcon).Click();
        }

        private void NoteDownNumberOfRoles(string permissionName)
        {
            ScenarioContext.Current[permissionName + "_roles"] =
                GetNumberOfRolesAssigned(permissionName); // note down attached number roles before
        }


        public void ClickAssignedFilterBtn()
        {
            if (Driver.IsElementPresent(_allFilterDefault)) return;
            Driver.ClickItem(_assignedFilterBtn);
            Driver.WaitUntilElementNotPresent(_assignedFilterDefault);
        }

        public void ClickAllAssignedFilterBtn()
        {
            if (Driver.IsElementPresent(_assignedFilterDefault)) return;
            Driver.ClickItem(_allFilterBtn);
            Driver.WaitUntilElementNotPresent(_allFilterDefault);
        }


        public void VerifyNoPermissionsDisplayed()
        {
            Assert.True(Driver.FindElements(_permissions).Count == 0,
                "Found permissions while expecting the list of permissions to be Zero");
        }

        private string GetNumberOfRolesAssigned(string permissionName)
        {
            CloseGrowlNotification();
            var permissionElement = GetPermissionElement(permissionName);
            var numbOfRoles = _numberOfRolesZero;
            if (Driver.FindElement(permissionElement).IsElementWithinWebElementPresent(_numberOfRoles))
            {
                numbOfRoles = _numberOfRoles;
            }

            return Driver.FindElement(GetPermissionElement(permissionName)).FindElement(numbOfRoles).Text;
        }

        // Gets the indiviodual permission By element from DOM
        private By GetPermissionElement(string permission)
        {
            return By.XPath(_permissionName.Replace("permission_Name_Here", permission));
        }

        public void ClickAssignedRolesIcon(string permission)
        {
            var permissionElement = GetPermissionElement(permission);
            Driver.FindElement(permissionElement).FindElement(_numbOfRolesIcon).Click();
        }

        public void VerifyRoleNameIsDisplayed()
        {
            var rolename = ScenarioContext.Current["rolename"].ToString();
            Assert.True(Driver.FindElement(_permissionsToRolePopUp)
                            .FindElements(By.XPath(_roleNameSelector.Replace("role_Name_Here", rolename))).Count > 0);
        }
    }
}