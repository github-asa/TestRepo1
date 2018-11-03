using J2BIOverseasOps.Pages.RoleManagement;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.RoleManagement
{
    [Binding]
    public sealed class RoleManagementStepDefs : BaseStepDefs
    {
        private readonly RoleMgmtCommon _roleMgmtPage;
        private readonly CreateUpdateRolesPage _createUpdateRoles;

        public RoleManagementStepDefs(IWebDriver driver, ILog log) : base(driver, log)
        {
            _createUpdateRoles = new CreateUpdateRolesPage(driver, log);
            _roleMgmtPage = new RoleMgmtCommon(driver, log);
        }

        [Given(@"I navigate to the role management page")]
        [When(@"I navigate to the role management page")]
        public void GivenINavigateToTheRoleManagementPage()
        {
            _createUpdateRoles.NavigateToRoleManagement();
        }

        [When(@"I create a user role with a name as ""(.*)""")]
        public void WhenICreateAUserRoleWithANameAsDescriptionAsAndTheStatusOfIsActiveAs(string name)
        {
            _createUpdateRoles.CreateARandomRole(name);
        }

        [When(@"I set the current role to ""(.*)""")]
        public void WhenISetTheCurrentRoleTo(string roleName)
        {
            _createUpdateRoles.SetCurrentRole(roleName);
        }

        [When(@"I update the value of the role to the ""(.*)""")]
        public void WhenIUpdateTheValueOfTheRoleToThe(string newKey)
        {
            _createUpdateRoles.UpdateRoleValue(newKey);
        }

        [When(@"I create another role with a name as ""(.*)""")]
        public void WhenICreateAnotherRoleWithANameAs(string roleKey)
        {
            _createUpdateRoles.CreateDuplicateRole(roleKey);
        }

        [Then(@"I verify I am displayed the role already used error message")]
        public void ThenIVerifyIAmDisplayedTheRoleAlreadyUsedErrorMessage()
        {
            _createUpdateRoles.VerifyRoleBeingUsedErrorMessage();
        }

        [Then(@"I verify the role is displayed on the list of roles on the create role page")]
        public void ThenIVerifyTheRoleIsDisplayedOnTheListOfRoles()
        {
            _createUpdateRoles.VerifyRolePresent();
        }

        [Then(@"I verify the role status is ""(Active|Inactive)"" on create role page")]
        public void ThenIVerifyTheRoleStatusIs(string roleStatus)
        {
            _createUpdateRoles.VerifyRoleStatus(roleStatus);
        }

        [Then(@"I verify only the Active roles are displayed on the list")]
        public void ThenIVerifyOnlyTheActiveRolesAreDisplayedOnTheList()
        {
            _createUpdateRoles.VerifyAllRolesActive();
        }

        [Then(@"I verify the role is not displayed on the list of roles on create role page")]
        public void ThenIVerifyTheRoleIsNotDisplayedOnTheListOfRoles()
        {
            _createUpdateRoles.VerifyRoleNotDisplayed();
        }

        [Then(@"I verify the assigned employees as ""(.*)"" on create role page")]
        public void ThenIVerifyTheAssignedEmployeesAs(string numberOfAssignedEmployees)
        {
            _createUpdateRoles.VerifyAssignedEmployees(numberOfAssignedEmployees);
        }

        [Then(@"I verify the assigned permissions as ""(.*)"" on create role page")]
        public void ThenIVerifyTheAssignedPermissionsAs(string numberOfAssignedPermissions)
        {
            _createUpdateRoles.VerifyAssignedPermissions(numberOfAssignedPermissions);
        }

        [When(@"I update the role name")]
        public void WhenIUpdateTheRoleName()
        {
            _createUpdateRoles.UpdateRoleName();
        }

        [When(@"I update the role permission to ""(Active|Inactive)""")]
        public void WhenIUpdateTheRolePermissionTo(string permissionStatus)
        {
            _createUpdateRoles.UpdatePermissionStatus(permissionStatus);
        }

        [When(@"I click the role to mark it as Inactive")]
        public void WhenIClickTheRoleToMarkItAsInactive()
        {
            _createUpdateRoles.ClickInactiveRole();
        }

        [When(@"I click the All button on create role page")]
        public void WhenIClickTheAllButton()
        {
            _createUpdateRoles.ClickAllFilterBtn();
        }

        [When(@"I click on the Active button on create role page")]
        public void WhenIClickOnTheActiveButton()
        {
            _createUpdateRoles.ClickActiveFilterBtn();
        }

        [When(@"I type in the ""(.*)"" in the filter search box on the create role page")]
        public void WhenITypeInTheRoleNameInTheFilterSearchBox(string textToType)
        {
            _createUpdateRoles.SearchByRoleName(textToType);
        }

        [Then(@"I verify no roles are found on the search list")]
        public void ThenIVerifyNoRolesAreFoundOnTheSearchList()
        {
            _createUpdateRoles.VerifyRoleNotDisplayed();
        }

        [Then(@"I verify the list of roles is empty")]
        public void ThenIVerifyTheListOfRolesIsEmpty()
        {
            _createUpdateRoles.VerifyRolesListEmpty();
        }

        [When(@"I click the Reset button on Roles by Name page")]
        [Then(@"I click the Reset button on Roles by Name page")]
        public void WhenIClickTheResetButtonOnRolesByNamePage()
        {
            _createUpdateRoles.ClickResetButton();
        }

        [When(@"I click the Assigned button on the ""(collapsed|expanded)"" sidebar navigation")]
        public void WhenIClickTheAssignedButtonOnTheSideBarNavigation(string navStatus)
        {
            var navCollapsed = navStatus == "expanded";
            _createUpdateRoles.ClickAssignSideBarBtn(navCollapsed);
        }

        [When(@"I click the Report button on the ""(collapsed|expanded)"" sidebar navigation")]
        public void WhenIClickTheReportButtonOnTheSideBarNavigation(string navStatus)
        {
            var navCollapsed = navStatus == "expanded";
            _createUpdateRoles.ClickReportSideBarBtn(navCollapsed);
        }

        [When(@"I click the Permissions button on the ""(collapsed|expanded)"" sidebar navigation")]
        public void WhenIClickThePermissionsButtonOnTheSideBarNavigation(string navStatus)
        {
            var navCollapsed = navStatus == "expanded";
            _createUpdateRoles.ClickPermissionSideBarBtn(navCollapsed);
        }

        [When(@"I click the Manage button on the ""(collapsed|expanded)"" sidebar navigation")]
        public void WhenIClickTheManageButtonOnTheSideBarNavigation(string navStatus)
        {
            var navCollapsed = navStatus == "expanded";
            _createUpdateRoles.ClickManageSideBarBtn(navCollapsed);
        }

        [When(@"I click the Roles button on the ""(.*)"" sidebar navigation")]
        public void WhenIClickTheRolesButtonOnTheSidebarNavigation(string navStatus)

        {
            var navCollapsed = navStatus == "expanded";
            _createUpdateRoles.ClickRolesSideBarBtn(navCollapsed);
        }

        [When(@"I expand the sidebar navigation")]
        public void WhenIExpandTheSidebarNavigation()
        {
            _createUpdateRoles.ExpandSideBar();
        }

        [Then(@"I collapse the sidebar navigation")]
        public void ThenICollapseTheSidebarNavigation()
        {
            _createUpdateRoles.CollapseSideBar();
        }

        [Then(@"I verify the Growl notification displays title ""(.*)""")]
        public void ThenIVerifyTheGrowlNotificationDisplaysTitle(string verifyTitle)
        {
            _createUpdateRoles.VerifyTextOnGrowlNotification(verifyTitle);
        }

        [Then(@"I verify the text on Growl notification as ""(.*)""")]
        public void ThenIVerifyTheRoleCreationTextOnGrowlNotificationAs(string text)
        {
            _createUpdateRoles.VerifyRoleCreationText(text);
        }

        [Then(@"I verify the filter by role name input box is disabled on the create role page")]
        public void ThenIVerifyTheFilterByRoleNameInputBoxIsDisabledOnTheCreateRolePage()
        {
            _createUpdateRoles.VerifyFilterByRoleNameDisabled();
        }

        [Then(@"I verify the Add roles button is disabled on the create role page")]
        public void ThenIVerifyTheAddRolesButtonIsDisabledOnTheCreateRolePage()
        {
            _createUpdateRoles.VerifyAddRolesButtonDisabled();
        }

        [Then(@"I verify I can not see the Active/All filter on the create role page")]
        public void ThenIVerifyICanNotSeeTheActiveAllFilterOnTheCreateRolePage()
        {
            _createUpdateRoles.VerifyActiveFilterNotDisplayed();
        }

        [Then(@"I verify I can not see the Reset button on the create role page")]
        public void ThenIVerifyICanNotSeeTheResetButtonOnTheCreateRolePage()
        {
            _createUpdateRoles.VerifyResetButtonNotDisplayed();
        }

        [Then(@"I verify I can not see the Role Management link")]
        public void ThenIVerifyICanNotSeeTheRoleManagementLink()
        {
            _createUpdateRoles.VerifyRoleMgmtLinkNotDisplayed();
        }

        [When(@"I click the Back button on the Role Management")]
        public void WhenIClickTheBackButtonOnTheRoleManagement()
        {
            _createUpdateRoles.ClickRoleMgmtBackButton();
        }

        [When(@"I click the assigned employees icon")]
        public void WhenIClickTheAssignedEmployeesIcon()
        {
            _createUpdateRoles.ClickAssignedEmployeesIcon();
        }

        [When(@"I click the assigned permissions icon")]
        public void WhenIClickTheAssignedPermissionsIcon()
        {
            _createUpdateRoles.ClickAssignedPermissionsIcon();
        }

        [Then(@"I verify I am displayed the message ""(.*)""")]
        public void ThenIVerifyIAmDisplayedTheMessage(string message)
        {
            _roleMgmtPage.VerifyEmptyPopUpMessage(message);
        }

        [Then(@"I click the close icon to close the assigned employee pop up")]
        public void ThenICloseTheAssignedEmployeePopUp()
        {
            _roleMgmtPage.CloseAssignedEmployeePopUp();
        }

        [Then(@"I click the close icon to close the assigned permission pop up")]
        public void ThenICloseTheAssignedPermissionPopUp()
        {
            _roleMgmtPage.CloseAssignedPermissionPopUp();
        }

        [Then(@"I verify that I can see ""(.*)"" on the assigned permissions pop up list")]
        public void ThenIVerifyThatICanSeeOnTheAssignedPermissionsPopUpList(string permissionName)
        {
            _createUpdateRoles.VerifyPermisionVisibleInPopUp(permissionName);
        }

        [Then(@"I verify that I can see ""(.*)"" on the assigned employees pop up list")]
        public void ThenIcanVerifyThatICanSeeOnTheAssignedEmployeesPopUpList(string employeeName)
        {
            _createUpdateRoles.VerifyEmployeeVisibleInPopUp(employeeName);
        }

        [Then(@"I verify I am not presented with a Growl notification system error")]
        public void ThenIVerifyIAmNotPresentedWithAGrowlNotificationSystemError()
        {
            _roleMgmtPage.VerifySystemErrorNotDisplayed();
        }

        [Then(@"I verify I am not presented with a validation error message")]
        public void ThenIVerifyIAmNotPresentedWithAValidationErrorMessage()
        {
            _roleMgmtPage.VerifyValidationErrorNotDisplayed();
        }

        [When(@"I assign any role to the user ""(.*)""")]
        public void WhenIAssignAnyRoleToTheUser(string userFullname)
        {
            _roleMgmtPage.AssignAnyRoleToUser(userFullname);
        }

    }
}