using J2BIOverseasOps.Pages.RoleManagement;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.RoleManagement
{
    [Binding]
    internal class AssignedRolesToPermissionsStepDefs : BaseStepDefs
    {
        private readonly AssignedRolesToPermPage _rolesToPermissions;

        public AssignedRolesToPermissionsStepDefs(IWebDriver driver, ILog log) : base(driver, log)
        {
            _rolesToPermissions = new AssignedRolesToPermPage(driver, log);
        }

        [When(@"I select ""(.*)"" on select permission filter for assigned roles page")]
        public void WhenISelectOnSelectPermissionFilterForAssignedRolesPage(string permission)
        {
            _rolesToPermissions.SelectPermission(permission);
        }

        [When(@"I type in the role name that does not exist on the role by name filter for the assigned roles page")]
        public void WhenITypeInTheRoleNameThatDoesNotExistOnTheRoleByNameFilterForTheAssignedRolesPage()
        {
            _rolesToPermissions.EnterTextFilterRoleByName("random role name");
        }

        [Then(@"I verify no roles found message is displayed on the assigned roles page")]
        public void ThenIVerifyNoRolesFoundMessageIsDisplayedOnTheAssignedRolesPage()
        {
            _rolesToPermissions.VerifyNoRolesDisplayed();
        }

        [When(@"I click the Reset filter button on the assigned roles page")]
        public void WhenIClickTheResetFilterButtonOnTheAssignedRolesPage()
        {
            _rolesToPermissions.ClickResetFilterBtn();
        }

        [Then(
            @"I verify the select permission filter displays the first permission on the drop down on the assigned roles page")]
        public void ThenIVerifyTheSelectPermissionFilterDisplaysTheFirstPermissionOnTheDropDownOnTheAssignedRolesPage()
        {
            _rolesToPermissions.VerifyFirstPermSelected();
        }

        [Then(@"I verify the filter roles by name input box is blank on the assigned roles page")]
        public void ThenIVerifyTheFilterRolesByNameInputBoxIsBlankOnTheAssignedRolesPage()
        {
            _rolesToPermissions.VerifyFilterByNameReset();
        }

        [Then(@"the role appears on the list for assigned roles page")]
        public void ThenTheRoleAppearsOnTheListForAssignedRolesPage()
        {
            _rolesToPermissions.VerifyRoleNameDisplayed();
        }

        [Then(@"the role is not displayed on the list for assigned roles page")]
        public void ThenTheRoleIsNotDisplayedOnTheListForAssignedRolesPage()
        {
            _rolesToPermissions.VerifyRoleNotDisplayed();
        }

        [Then(@"I verify the role status is ""(Active|Inactive)"" on the assigned roles page")]
        public void ThenIVerifyTheRoleStatusIsOnTheAssignedRolesPage(string status)
        {
            _rolesToPermissions.VerifyRoleStatus(status);
        }

        [Given(@"I note down the number of users assigned to the role on the assigned roles page")]
        public void GivenINoteDownTheNumberOfUsersAssignedToTheRoleOnTheAssignedRolesPage()
        {
            _rolesToPermissions.NoteDownNumberOfUsersAssigned();
        }

        [Given(@"I note down the number of permissions assigned to the role on the assigned roles page")]
        public void GivenINoteDownTheNumberOfPermissionsAssignedToTheRoleOnTheAssignedRolesPage()
        {
            _rolesToPermissions.NoteDownNumberOfPermissionsAssigned();
        }

        [Then(@"I verify the number of users for the role is incremented by ""(.*)"" on the assigned roles page")]
        public void ThenIVerifyTheNumberOfUsersForTheRoleIsIncrementedByOnTheAssignedRolesPage(int incrementValue)
        {
            _rolesToPermissions.VerifyUsersCountIncremented(incrementValue);
        }

        [Then(@"I verify the number of permissions for the role is incremented by ""(.*)"" on the assigned roles page")]
        public void ThenIVerifyTheNumberOfPermissionsForTheRoleIsIncrementedByOnTheAssignedRolesPage(int incrementValue)
        {
            _rolesToPermissions.VerifyPermissionCountIncremented(incrementValue);
        }

        [Then(@"I verify the number of users for the role is decremented by ""(.*)"" on the assigned roles page")]
        public void ThenIVerifyTheNumberOfUsersForTheRoleIsDecrementedByOnTheAssignedRolesPage(int decrementValue)
        {
            _rolesToPermissions.VerifyUsersCountDecremented(decrementValue);
        }

        [Then(@"I verify the number of permissions for the role is decremented by ""(.*)"" on the assigned roles page")]
        public void ThenIVerifyTheNumberOfPermissionsForTheRoleIsDecrementedByOnTheAssignedRolesPage(int decrementValue)
        {
            _rolesToPermissions.VerifyPermissionsCountDecremented(decrementValue);
        }
    }
}