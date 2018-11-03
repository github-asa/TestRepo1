using J2BIOverseasOps.Pages.RoleManagement;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.RoleManagement
{
    [Binding]
    public sealed class PermissionsStepDefs : BaseStepDefs
    {
        private readonly AssignPermissionsToRolesPage _permissionsPage;

        public PermissionsStepDefs(IWebDriver driver, ILog log,IRunData rundata) : base(driver, log)
        {
            _permissionsPage = new AssignPermissionsToRolesPage(driver, log, rundata);
        }

        [When(@"I select the role from drop down on the assign permissions page")]
        public void WhenISelectTheRoleFromDropDownOnTheAssignedPermissionsPage()
        {
            _permissionsPage.SelectRoleFromDrpDwn();
        }

        [Then(@"I verify list of permissions are displayed")]
        public void ThenIVerifyListOfPermissionsAreDisplayed()
        {
            _permissionsPage.VerifyListOfPermissionsDisplayed();
        }

        [Then(@"I verify none of the permissions are assigned to the role")]
        public void ThenIVerifyNoneOfThePermissionsAreAssignedToTheRole()
        {
            _permissionsPage.VerifyNoPermissionsAssigned();
        }

        [Given(@"I search for permission ""(.*)""")]
        public void GivenISearchForPermission(string permissionName)
        {
            _permissionsPage.FilterByPermission(permissionName);
        }

        [Then(@"I verify the ""(.*)"" is displayed on the list of permissions")]
        public void ThenIVerifyTheIsDisplayedOnTheListOfPermissions(string permission)
        {
            _permissionsPage.VerifyPermissionDisplayed(permission);
        }

        [When(@"I assign the following permissions to the role:")]
        public void WhenIAssignTheFollowingPermissionsToTheRole(Table table)
        {
            _permissionsPage.SelectRoleFromDrpDwn();
            _permissionsPage.AssignPermissionsToRole(table);
        }

        [When(@"I remove the following permissions from the role:")]
        [Then(@"I remove the following permissions from the role:")]
        public void ThenIRemoveTheFollowingPermissionsFromTheRole(Table table)
        {
            _permissionsPage.SelectRoleFromDrpDwn();
            _permissionsPage.RemovePermissionsFromRole(table);
        }

        [Then(@"I verify the following permissions are assigned to the role:")]
        public void ThenIVerifyTheFollowingPermissionsAreAssignedToTheRole(Table table)
        {
            _permissionsPage.VerifyPermissionsAssigned(table);
        }

        [Then(@"I verify the number of roles for the following permissions is incremented by ""(.*)"":")]
        public void ThenIVerifyTheNumberOfRolesForTheFollowingPermissionsIsIncrementedBy(int incrementValue,
            Table table)
        {
            _permissionsPage.VerifyRolesIncremented(incrementValue, table);
        }

        [Then(@"I verify the number of roles for the following permissions is decremented by ""(.*)"":")]
        public void ThenIVerifyTheNumberOfRolesForTheFollowingPermissionsIsDecrementedBy(int decrementValue,
            Table table)
        {
            _permissionsPage.VerifyRolesDecremented(decrementValue, table);
        }

        [When(@"I click the Assigned filter button on the permissions page")]
        public void WhenIClickTheAssignedFilterButtonOnThePermissionsPage()
        {
            _permissionsPage.ClickAssignedFilterBtn();
        }

        [Then(@"I verify no permissions are displayed on the list")]
        public void ThenIVerifyNoPermissionsAreDisplayedOnTheList()
        {
            _permissionsPage.VerifyNoPermissionsDisplayed();
        }

        [When(@"I click the All filter button on the permissions page")]
        public void WhenIClickTheAllFilterButtonOnThePermissionsPage()
        {
            _permissionsPage.ClickAllAssignedFilterBtn();
        }

        [Then(@"I remove all the permissions from the current role")]
        [When(@"I remove all the permissions from the current role")]
        public void ThenIResetThePermissionsAssignedToTheRole()
        {
            _permissionsPage.RemoveAllPermissions();
        }

        [When(@"I click the assigned roles icon on the permissions page for permission ""(.*)""")]
        public void WhenIClickTheAssignedRolesIcon(string permission)
        {
            _permissionsPage.ClickAssignedRolesIcon(permission);
        }

        [Then(@"I verify the role name is displayed on the pop up")]
        public void ThenIVerifyTheRoleNameIsDisplayedOnThePopUp()
        {
            _permissionsPage.VerifyRoleNameIsDisplayed();
        }
    }
}