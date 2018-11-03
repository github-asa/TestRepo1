using System.Threading;
using J2BIOverseasOps.Pages.RoleManagement;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.RoleManagement
{
    [Binding]
    public sealed class RolesToUsersStepDefs : BaseStepDefs
    {
        private readonly AssignEmployeeToRole _assignRolesToUsersPage;
        private readonly CreateUpdateRolesPage _createUpdateRoles;

        public RolesToUsersStepDefs(IWebDriver driver, ILog log, IRunData rundata) : base(driver, log)
        {
            _createUpdateRoles = new CreateUpdateRolesPage(driver, log);
            _assignRolesToUsersPage = new AssignEmployeeToRole(driver, log, rundata);
        }

        [When(@"I search for employee ""(.*)"" on assign employees page")]
        public void WhenISearchForEmployee(string name)
        {
            _assignRolesToUsersPage.SearchForUser(name);
        }

        [When(@"I search for the role on assign employees page")]
        public void WhenISearchForTheRoleCreatedAbove()
        {
            _assignRolesToUsersPage.FilterRoleByName();
        }

        [When(@"I assign the role to the user ""(.*)""")]
        public void WhenIAssignTheRoleToTheUser(string name)
        {
            _assignRolesToUsersPage.ClearRoleByNameField();
            _assignRolesToUsersPage.ClickActiveAllFilterBtn();
            _assignRolesToUsersPage.WaitForSpinnerToDisappear();
            _assignRolesToUsersPage.SearchForUser(name);
            _assignRolesToUsersPage.WaitForSpinnerToDisappear();
            _assignRolesToUsersPage.FilterRoleByName();
            _assignRolesToUsersPage.WaitForSpinnerToDisappear();
            _assignRolesToUsersPage.AssignRole();
        }

        [Given(@"I remove the currently assigned role to the users ""(.*)""")]
        public void GivenIRemoveTheCurrentlyAssignedRoleToTheUsers(string users)
        {
            _assignRolesToUsersPage.RemoveRoleUsingApi(users);
        }



        [Given(@"I assign ""(.*)"" to the user ""(.*)""")]
        public void GivenIAssignToTheUser(string roleKey, string userName)
        {
            _assignRolesToUsersPage.SearchForUser(userName);
            _assignRolesToUsersPage.FilterRoleByName(roleKey);
            _assignRolesToUsersPage.AssignRole(roleKey);
        }

        [When(@"I click the All filter button on Assign Employee page")]
        [Then(@"I click the All filter button on Assign Employee page")]
        public void ThenIClickTheAllFilterButtonOnAssignEmployeePage()
        {
            _assignRolesToUsersPage.ClickActiveAllFilterBtn();
        }

        [When(@"I click the Active filter button on Assign Employee page")]
        public void WhenIClickTheActiveFilterButtonOnAssignEmployeePage()
        {
            _assignRolesToUsersPage.ClickActiveFilterBtn();
        }

        [When(@"I click the assigned filter button on the assign employees page")]
        public void WhenIClickTheAssignedFilterButtonOnTheAssignEmployeesPage()
        {
            _assignRolesToUsersPage.ClickAssignedFilterBtn();
        }

        [When(@"I click the all assigned filter button on the assign employees page")]
        public void WhenIClickTheAllAssignedFilterButtonOnTheAssignEmployeesPage()
        {
            _assignRolesToUsersPage.ClickAllAssignedFilterBtn();
        }

        [Then(@"I verify the role status is ""(Active|Inactive)"" on the Assign Employee page")]
        public void ThenIVerifyTheRoleStatusIsOnTheAssignEmployeePage(string rolestatus)
        {
            _assignRolesToUsersPage.VerifyRoleStatus(rolestatus);
        }

        [When(@"I assign the role to the employee")]
        public void WhenIAssignTheRoleToTheUser()
        {
            _assignRolesToUsersPage.AssignRole();
        }

        [Then(@"I verify the assigned employees count is ""(.*)"" on the assign employees page")]
        public void ThenIVerifyTheAssignedEmployeesCountIsOnTheAssignEmployeesPage(string expectedNumberOfEmployees)
        {
            _assignRolesToUsersPage.VerifyAssignedEmployeesCount(expectedNumberOfEmployees);
        }

        [Then(@"I verify the assigned permissions count is ""(.*)"" on the assign employees page")]
        public void ThenIVerifyTheAssignedPermissionsCountIsOnTheAssignEmployeesPage(string expectedNumberOfPermissions)
        {
            _assignRolesToUsersPage.VerifyPermissionsCount(expectedNumberOfPermissions);
        }

        [Then(@"I verify the role is displayed on the roles list for the assign employees page")]
        public void ThenIVerifyTheRoleIsDisplayedOnTheRolesListForTheAssignEmployeesPage()
        {
            _assignRolesToUsersPage.VerifyRoleNameDisplayed();
        }

        [Then(@"I verify the role is not displayed on the list of roles on the assign employee page")]
        public void ThenIVerifyTheRoleIsNotDisplayedOnTheListOfRolesOnTheAssignEmployeePage()
        {
            _assignRolesToUsersPage.VerifyRoleNotDisplayed();
        }

        [Given(@"I note down the role name as ""(.*)""")]
        [When(@"I note down the role name as ""(.*)""")]
        public void GivenINoteDownTheRoleNameAs(string rolename)
        {
            _createUpdateRoles.NoteDownRoleNameAs(rolename);
        }

        [Then(@"I verify the ""(.*)"" is displayed on the roles list for the assign employees page")]
        public void ThenIVerifyTheIsDisplayedOnTheList(string rolenameKey)
        {
            _assignRolesToUsersPage.VerifyRoleNameDisplayed(rolenameKey);
        }

        [Then(@"I verify the ""(.*)"" is not displayed on the roles list for the assign employees page")]
        public void ThenIVerifyTheIsNotDisplayedOnTheRolesListForTheAssignEmployeesPage(string rolenameKey)
        {
            _assignRolesToUsersPage.VerifyRoleNotDisplayed(rolenameKey);
        }

        [Then(@"I verify the employee by name filter displays ""(.*)"" on assign employees page")]
        public void ThenIVerifyTheEmployeeByNameFilterDisplaysOnAssignEmployeesPage(string employeeName)
        {
            _assignRolesToUsersPage.VerifyEmployeeNameFilterText(employeeName);
        }

        [Then(@"I verify the role name is selected and highlighted")]
        public void ThenIVerifyTheRoleNameIsSelectedAndHighlighted()
        {
            _assignRolesToUsersPage.VerifyRoleSelected();
        }

        [Then(@"I verify the breadcrumb links are displayed for ""(.*)""")]
        public void ThenIVerifyTheBreadcrumbLinksAreDisplayedFor(string breadcrumbLinks)
        {
            _assignRolesToUsersPage.VerifyBreadCrumbLinksDisplayed(breadcrumbLinks);
        }

        [When(@"I click on the ""(.*)"" breadcrumb link")]
        public void WhenIClickOnTheBreadcrumbLink(string linkText)
        {
            _assignRolesToUsersPage.ClickBreadCrumbLink(linkText);
        }

        [Then(@"I verify I can not see the radio button to assign the role to an employee")]
        public void ThenIVerifyICanNotSeeTheRadioButtonToAssignTheRoleToAnEmployee()
        {
            _assignRolesToUsersPage.VerifyRadioButtonNotDisplayed();
        }
    }
}