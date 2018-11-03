using J2BIOverseasOps.Pages.UserManagement;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.UserManagement
{
    [Binding]
    public sealed class AssignPropToUseStepDefs : BaseStepDefs
    {
        private readonly AssignPropToUsersPage _propToUsers;

        public AssignPropToUseStepDefs(IWebDriver driver, ILog log) : base(driver, log)
        {
            _propToUsers = new AssignPropToUsersPage(driver, log);
        }

        [When(@"I navigate to Assign Properties to Users tab")]
        public void WhenINavigateToAssignPropertiesToUsersTab()
        {
            _propToUsers.NavigateToPropToUserTab();
        }

        [When(@"I select the Destination as ""(.*)""")]
        public void WhenISelectTheDestinationAs(string destination)
        {
            _propToUsers.SelectDestination(destination);
        }

        [When(@"I select the Resort as ""(.*)""")]
        public void WhenISelectTheResortAs(string resorts)
        {
            _propToUsers.SelectResorts(resorts);
        }

        [When(@"I select the Property as ""(.*)""")]
        public void WhenISelectThePropertyAs(string properties)
        {
            _propToUsers.SelectProperties(properties);
        }

        [When(@"I select the users as ""(.*)""")]
        public void WhenISelectTheUserAs(string users)
        {
            _propToUsers.SelectUsers(users);
        }

        [Then(@"I am presented with a table of users and properties")]
        public void ThenIAmPresentedWithATableOfUserAndProperties()
        {
            _propToUsers.VerifyPropertiesToUsersTableDisplayed();
        }

        [Then(@"I verify the list of properties displayed are ""(.*)""")]
        public void ThenIVerifyTheListOfPropertiesDisplayedAre(string properties)
        {
            _propToUsers.VerifyPropertiesInTable(properties);
        }

        [Then(@"I verify the users displayed are ""(.*)""")]
        public void ThenIVerifyTheUsersDisplayedAre(string users)
        {
            _propToUsers.VerifyUsersInTable(users);
        }

        [When(@"I search for property name ""(.*)""")]
        public void WhenISearchForPropertyName(string propertyName)
        {
            _propToUsers.SearchProperty(propertyName);
        }

        [Then(@"I revoke all the permissions for all the users")]
        [When(@"I revoke all the permissions for all the users")]
        public void ThenIRevokeAllThePermissionsForAllTheUsers()
        {
            _propToUsers.RevokeAllPermissions();
        }

        [Then(@"I verify all the users permissions are revoked")]
        public void ThenIVerifyAllTheUsersPermissionsAreRevoked()
        {
            _propToUsers.VerifyAllPermissionsRevoked();
        }

        [When(@"I give following access to the following users for the given properties:")]
        public void WhenIGiveFollowingPermissionsToTheFollowingUsersForTheGivenProperties(Table table)
        {
            _propToUsers.GrantRevokePermission(table);
        }

        [Then(@"I verify the following access to the following users for the given properties:")]
        public void ThenIVerifyTheFollowingAccessToTheFollowingUsersForTheGivenProperties(Table table)
        {
            _propToUsers.VerifyUserToPropertiesPermissions(table);
        }

        [Then(@"I verify I can see the user ""(.*)"" in the list of users")]
        public void WhenIVerifyICanSeeTheUserInTheListOfUsers(int userIndex)
        {
            _propToUsers.VerifyUserPresentInTheList(userIndex);
        }

        [Then(@"I verify I cannot see the user ""(.*)"" in the list of users")]
        public void WhenIVerifyICantSeeTheUserInTheListOfUsers(int index)
        {
            _propToUsers.VerifyUserNotPresentInTheList(index);
        }

        [Given( @"I select destination as ""(.*)"" , resorts as ""(.*)"", properties as ""(.*)"" and users as ""(.*)""""")]
        public void GivenISelectDestinationAsResortsAsPropertiesAsAndUsersAs(string destination, string resorts, string properties, string userindex)
        {
            _propToUsers.SelectDestination(destination);
            _propToUsers.SelectResorts(resorts);
            _propToUsers.SelectProperties(properties);
            _propToUsers.SelectUsers(userindex);
        }

        [When(@"I click on the ""(.*)"" filter")]
        public void WhenIClickOnTheFilter(string filterName)
        {
            _propToUsers.ClickPropertiesFilter(filterName);
        }

        [Then(@"I verify the expected list of resorts as empty")]
        public void ThenIVerifyTheExpectedListOfResortsAsEmpty()
        {
            _propToUsers.VerifyResortsListEmpty();
        }

        [Then(@"I verify expected list of properties as empty")]
        public void ThenIVerifyExpectedListOfPropertiesAsEmpty()
        {
            _propToUsers.VerifyPropertiesListEmpty();
        }

        [Then(@"I verify the expected list of users as empty")]
        public void ThenIVerifyTheExpectedListOfUsersAsEmpty()
        {
            _propToUsers.VerifyUsersListEmpty();
        }

        [Then(@"I verify the selected item badge for destination displays ""(.*)""")]
        public void ThenIVerifyTheSelectedItemBadgeForDestinationDisplays(string expected)
        {
            _propToUsers.VerifyDestinationBadgeItem(expected);
        }

        [Then(@"I verify the selected item badge displays ""(.*)""")]
        public void ThenIVerifyTheSelectedItemBadgeDisplays(string expectedItems)
        {
            _propToUsers.VerifyMultiListBadgeItems(expectedItems);
        }

        [Then(@"I verify the selected item badge for user displays ""(.*)""")]
        public void ThenIVerifyTheSelectedUserItemBadgeDisplays(string index)
        {
            _propToUsers.VerifyUserMultiListBadgeItems(index);
        }
        

        [Then(@"I verify I can not see any of the Assign Properties to Users components")]
        public void ThenIVerifyICanNotSeeAnyOfTheAssignPropertiesToUsersComponents()
        {
            _propToUsers.VerifyNoComponentsDisplayed();
        }
    }
}