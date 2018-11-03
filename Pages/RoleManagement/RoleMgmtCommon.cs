using System;
using J2BIOverseasOps.Extensions;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Pages.RoleManagement
{
    internal class RoleMgmtCommon : CommonPageElements
    {
        private const string RoleNameXpath = "//*[@rolename='rolenamehere']";
        private readonly By _assignedPermissionsPopUpCloseButton =
            By.XPath("//div[@id='role-assign-permissions-pop']/../../..//a");
        private readonly By _assignedUsersPopUpCloseButton =
            By.XPath("//div[@id='role-assigned-users-pop']/../../..//a//*[contains(@class,'pi-times')]");
        private readonly By _assignEmployeeBtnCollapsed = By.Id("btnAssignCollapsed");

        private readonly By _breadCrumbsMenuItems = By.XPath("//*[@role='menuitem']");
        private readonly By _btnassignCollapsed = By.Id("btnAssignCollapsed");
        private readonly By _btnassignExpanded = By.Id("btnAssignExpanded");

        // collapsed buttons sidebar nav
        private readonly By _btnManageCollapsed = By.Id("btnManageCollapsed");

        // expanded buttons sidebar nav
        private readonly By _btnManageExpanded = By.Id("btnManageExpanded");
        private readonly By _btnPermissionsCollapsed = By.Id("btnPermissionsCollapsed");
        private readonly By _btnPermissionsExpanded = By.Id("btnPermissionsExpanded");
        private readonly By _btnReportCollapsed = By.Id("btnReportCollapsed");
        private readonly By _btnReportExpanded = By.Id("btnReportExpanded");
        private readonly By _btnRolesCollapsed = By.Id("btnRolesCollapsed");
        private readonly By _btnRolesExpanded = By.Id("btnRolesExpanded");
        private readonly By _collapseSidebarBtn = By.XPath("//button[contains(@class,'btnNavCollapse')]");
        private readonly By _expandSidebarBtn = By.Id("expand-button");
        private readonly By _homeBreadCrumb = By.XPath("//*[@class='ui-breadcrumb-home ng-star-inserted']//a");

        // No Permissions Message
        private readonly By _noPermissionsMessage = By.XPath("//p-message[@severity='error']");
        private readonly By _numbOfAssignedEmployees = By.ClassName("numOfUsers");
        private readonly By _numbOfAssignedPermissions = By.ClassName("numOfPermissions");
        private readonly By _popUpemployeeFullName = By.Id("fullname");
        private readonly By _popUpPermissionList = By.XPath(".//*[@id='permission-list']");
        private readonly By _popUpPermissions = By.Id("permissionName");
        private readonly By _popUpUserList = By.XPath(".//*[@id='user-list']");
        private readonly By _roleAssignedEmployeesPopUp = By.Id("role-assigned-users-pop");
        private readonly By _roleAssignedPermissionsPopUp = By.Id("role-assign-permissions-pop");
        private readonly By _sideBarCollapsed = By.XPath("//*[@class='ng-star-inserted collapsed']");
        private readonly By _sideBarExpanded = By.XPath("//div[@id='side-panel'][@class='ng-star-inserted expanded']");
        protected readonly By BackButton = By.Id("backButton");

        public RoleMgmtCommon(IWebDriver driver, ILog log) : base(driver, log)
        {
        }

        public void ExpandSideBar()
        {
            WaitForSpinnerToDisappear();
            Driver.ClickItem(_expandSidebarBtn);
            Driver.WaitForItem(_sideBarExpanded);
            WaitForSpinnerToDisappear();
        }

        public void CollapseSideBar()
        {
            if (Driver.WaitForItem(_sideBarExpanded, 3))
            {
                Driver.ClickItem(_collapseSidebarBtn);
                Driver.WaitForItem(_sideBarCollapsed);
            }
        }

        public void ClickManageSideBarBtn(bool expanded = false)
        {
            Driver.ClickItem(expanded ? _btnManageExpanded : _btnManageCollapsed);
        }

        public void ClickAssignSideBarBtn(bool expanded = false)
        {
            Driver.ClickItem(expanded ? _btnassignExpanded : _btnassignCollapsed);
        }

        public void ClickReportSideBarBtn(bool expanded = false)
        {
            Driver.ClickItem(expanded ? _btnReportExpanded : _btnReportCollapsed);
        }

        public void ClickPermissionSideBarBtn(bool expanded = false)
        {
            Driver.ClickItem(expanded ? _btnPermissionsExpanded : _btnPermissionsCollapsed);
        }

        public void ClickRolesSideBarBtn(bool expanded = false)
        {
            Driver.ClickItem(expanded ? _btnRolesExpanded : _btnRolesCollapsed);
        }

        public void NavigateToAssignRolesToEmployeesPage()
        {
            Driver.ClickItem(_assignEmployeeBtnCollapsed, true);
            Driver.WaitForPageToLoad();
        }

        public void VerifyBreadCrumbLinksDisplayed(string links)
        {
            WaitForSpinnerToDisappear();
            var listOfExpectedLinks = links.ConvertStringIntoList();
            var listOfActualLinks = Driver.FindElements(_breadCrumbsMenuItems);
            foreach (var link in listOfActualLinks)
            {
                Assert.True(listOfExpectedLinks.Contains(link.Text),
                    "Unable to find :" + link.Text + " in the list of expected links.");
            }

            if (listOfExpectedLinks.Contains("home_icon"))
            {
                Driver.IsElementPresent(_homeBreadCrumb);
            }
        }

        public void ClickBreadCrumbLink(string linkText)
        {
            Driver.WaitForItem(_breadCrumbsMenuItems);
            Driver.FindElement(_breadCrumbsMenuItems);
            Driver.ClickItem(linkText == "home_icon" ? _homeBreadCrumb : By.LinkText(linkText));
        }

        public void VerifyNoPermissionsMessage(string expectedText)
        {
            var actualText = Driver.FindElement(_noPermissionsMessage).GetAttribute("text");
            Assert.True(actualText.Contains(expectedText),
                $"Expected Text{expectedText} was not equal to Actual Text {actualText}");
        }

        public void ClickAssignedEmployeesIcon()
        {
            var rolename = ScenarioContext.Current["rolename"].ToString();
            var roleNameElem = By.XPath(RoleNameXpath.Replace("rolenamehere", rolename));
            Driver.FindElement(roleNameElem).FindElement(_numbOfAssignedEmployees).Click();
        }

        public void ClickAssignedPermissionsIcon()
        {
            var rolename = ScenarioContext.Current["rolename"].ToString();
            var roleNameElem = By.XPath(RoleNameXpath.Replace("rolenamehere", rolename));
            Driver.FindElement(roleNameElem).FindElement(_numbOfAssignedPermissions).Click();
        }

        public void CloseAssignedEmployeePopUp()
        {
            Driver.ClickItem(_assignedUsersPopUpCloseButton);
        }

        public void CloseAssignedPermissionPopUp()
        {
            Driver.ClickItem(_assignedPermissionsPopUpCloseButton);
        }

        public void VerifyEmptyPopUpMessage(string message)
        {
            if (message.Contains("employees"))
            {
                Assert.AreEqual(
                    Driver.FindElement(_roleAssignedEmployeesPopUp).FindElement(_popUpUserList)
                        .GetAttribute("emptymessage"), message);
            }
            else if (message.Contains("Permissions"))
            {
                Assert.AreEqual(
                    Driver.FindElement(_roleAssignedPermissionsPopUp).FindElement(_popUpPermissionList)
                        .GetAttribute("emptymessage"), message);
            }
        }

        public void VerifyPermisionVisibleInPopUp(string permissionName)
        {
            Driver.WaitForItem(_popUpPermissions);
            var listOfAllPermissions = Driver.FindElements(_popUpPermissions);
            var permissionFound = false;
            foreach (var permission in listOfAllPermissions)
            {
                try
                {
                    if (permission.Text == permissionName)
                    {
                        permissionFound = true;
                        break;
                    }
                }
                catch (StaleElementReferenceException e)
                {
                    Console.WriteLine($"EXCEPTION CAUGHT :{e}");
                }
            }

            Assert.True(permissionFound, $"Could not find {permissionName} in the permissions pop up box ");
        }

        public void VerifyEmployeeVisibleInPopUp(string employeeName)
        {
            Driver.WaitForItem(_popUpemployeeFullName);
            var listOfAllEmployees = Driver.FindElements(_popUpemployeeFullName);
            var employeeFound = false;
            foreach (var employee in listOfAllEmployees)
            {
                if (employee.Text == employeeName)
                {
                    employeeFound = true;
                    break;
                }
            }

            Assert.True(employeeFound, $"Was not able to find employee {employeeName} on the pop up list ");
        }

        //assignsthe first role from thee to a user using the API
        public void AssignAnyRoleToUser(string userFullname)
        {
            var rundata= new RunData();
            if (userFullname== "restricted_user")
            {
                userFullname = rundata.RestrictedUserFullName;
            }
            var runData = new RunData();
            var apiCall=new ApiCalls(runData);
            // check if the role is already assigned to a user
            var roleAssignedToUser = apiCall.GetRoleAssignedToUserFullName(userFullname);
                if (roleAssignedToUser.Rolename == null) // if no role is assigned
                {
                    var listOfRoles = apiCall.GetListOfRoles();
                    apiCall.MapRoleToUserById(listOfRoles[0].Id, roleAssignedToUser.UserId); // assign the first role to the user
                }
        }
    }
}