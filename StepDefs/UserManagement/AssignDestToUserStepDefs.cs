using J2BIOverseasOps.Extensions;
using J2BIOverseasOps.Pages.UserManagement;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.UserManagement
{
    [Binding]
    public sealed class AssignDestToUserStepDefs : BaseStepDefs
    {
        private readonly AssignDestToUserPage _destToUserPage;

        public AssignDestToUserStepDefs(IWebDriver driver, ILog log, IRunData rundata) : base(driver, log)
        {
            _destToUserPage = new AssignDestToUserPage(driver, log, rundata);
        }


        [When(@"I get the list of all the Users from the API")]
        public void WhenIGetTheListOfAllTheUsersFromTheAPI()
        {
            _destToUserPage.PopulateListOfUsers();
        }



        [Given(@"I navigate to Assign Destination to Users tab")]
        [When(@"I navigate to Assign Destination to Users tab")]
        [Then(@"I navigate to Assign Destination to Users tab")]
        public void GivenINavigateToAssignDestinationToUsersTab()
        {
            _destToUserPage.NavigateToDestToUserTab();
        }

        [When(@"I search for the user ""(.*)""")]
        public void WhenISearchForTheUser(string user)
        {
            _destToUserPage.SearchUser(user);
        }


        [When(@"I search for user number ""(.*)"" from the list of users")]
        public void WhenISearchForUserNumberFromTheListOfUsers(int userNumber)
        {
            _destToUserPage.SearchUser(userNumber);
        }


        [When(@"I search for a random user from the list of users")]
        public void RandomUser()
        {
            _destToUserPage.SearcRandomUser();
        }

        [Then(@"I verify the following details for the user is displayed on Destinations to Users section:")]
        public void ThenIVerifyTheFollowingDetailsForTheUserIsDisplayedOnDestinationsToUsersSection(Table table)
        {
            _destToUserPage.VerifyUserDetails(table);
        }

        [Given(@"I select the current user from the list")]
        [When(@"I select the current user from the list")]
        public void SelectCurrentUser()
        {
            _destToUserPage.SelectCurrentUserFromList();
        }

        [Given(@"I select the user ""(.*)"" from the list")]
        [When(@"I select the user ""(.*)"" from the list")]
        public void ThenISelectTheUserFromTheList(string name)
        {
            _destToUserPage.SelectUserFromList(name);
        }


        [Then(@"I verify the mapped destinations as ""(.*)""")]
        public void ThenIVerifyTheDefaultDestinationsAs(string expectedMappedDestinations)
        {
            _destToUserPage.VerifyMappedDestinations(expectedMappedDestinations);
        }

        [Then(@"I verify that I am displayed with ""(.*)"" is selected at the footer of search panel")]
        public void ThenIVerifyThatIAmDisplayedWithIsSelectedAtTheFooterOfSearchPanel(string fnameLname)
        {
            _destToUserPage.VerifyUserSelectedLabel(fnameLname);
        }

        [Then(@"I verify no result is found")]
        public void ThenIVerifyNoResultIsFound()
        {
            _destToUserPage.VerifyNoResultFound();
        }

        [Then(@"I am presented with message ""(.*)""")]
        public void ThenIAmPresentedWithMessage(string message)
        {
            _destToUserPage.VerifyNoUserUiMessage(message);
        }

        [Given(@"I note down the already selected destinations mapped to the user")]
        public void GivenINoteDownTheDefaultSelectedDestinationsMappedToTheUser()
        {
            _destToUserPage.NoteDownSelectedDestinations();
        }

        [When(@"I click on the ""(.*)"" to map them to the selected user")]
        public void WhenIClickOnTheToMapThemToTheSelectedUser(string listOfDestinations)
        {
            _destToUserPage.SelectDestinations(listOfDestinations);
        }

        [Then(@"I reset the current users mapped destinations to the default destinations")]
        public void ThenIResetTheCurrentUsersMappedDestinationsToTheDefaultDestinations()
        {
            _destToUserPage.SelectDestinations(ScenarioContext.Current.Get<string>("userDefaultDestinations"));
        }

        [When(@"I map destination ""(.*)"" to user ""(.*)""")]
        public void GivenIMapDestinationToUser(string destination, string username)
        {
            _destToUserPage.SelectUserFromList(username);
            _destToUserPage.NoteDownSelectedDestinations();
            _destToUserPage.SelectDestinations(destination);
        }

        [When(@"I unmap destination ""(.*)"" to user ""(.*)""")]
        public void UnMapUser(string destination, string indexes)
        {
            var listOfUsers = _destToUserPage.GetListOfUsers();
            foreach (var index in indexes.ConvertStringIntoList())
            {
                var i = int.Parse(index);
                var user = listOfUsers[i];
                var userFullName = $"{user.forename} {user.surname}";
                _destToUserPage.SelectUserFromList(userFullName);
                _destToUserPage.DeselectAllDestinations();
            }
        }

        [Given(@"I navigate to user management page")]
        public void GivenINavigateToUserManagementPage()
        {
            _destToUserPage.NavigateToUserManagement();
        }

        [Then(@"I verify I can not see any of the Assign Destination to Users components")]
        public void ThenIVerifyICanNotSeeAnyOfTheAssignDestinationToUsersComponents()
        {
            _destToUserPage.VerifyDestinationComponentsNotDisplayed();
        }



        [Then(@"I verify I can not see the User Management link")]
        public void GivenIVerifyICanNotSeeTheUserManagementLink()
        {
            _destToUserPage.VerifyUserMgmtLinkNotDisplayed();
        }

        [Given(@"I assign Destination as ""(.*)"" to the users ""(.*)""")]
        public void GivenIAssignDestinationAsToTheUsers(string destination, string indexes)
        {
            var listOfUsers = _destToUserPage.GetListOfUsers();
            foreach (var index in indexes.ConvertStringIntoList())
            {
                var i = int.Parse(index);
                var user=listOfUsers[i];
                var userFullName = $"{user.forename} {user.surname}";
                _destToUserPage.SelectUserFromList(userFullName);
                _destToUserPage.SelectDestinations(destination);
            }
        }

        [Given(@"I assign Destination as ""(.*)"" to the single user ""(.*)""")]
        public void GivenIAssignDestinationAsToTheSingleUser(string destination, string index)
        {
            var listOfUsers = _destToUserPage.GetListOfUsers();
                var i = int.Parse(index);
                var user = listOfUsers[i];
                var userFullName = $"{user.forename} {user.surname}";
                _destToUserPage.SelectUserFromList(userFullName);
                _destToUserPage.NoteDownSelectedDestinations();
                _destToUserPage.SelectDestinations(destination);
            
        }

        [When(@"I click on the viewedit profile for the current user on the assign destinations to users page")]
        public void WhenIClickOnTheVieweditProfileForTheCurrentUserOnTheAssignDestinationsToUsersPage()
        {
            _destToUserPage.ClickEditViewProfileLink();
        }

        [Then(@"I verify the newly pushed user appears on the list of users on the destination to users page")]
        public void ThenIVerifyTheNewlyPushedUserAppearsOnTheListOfUsersOnTheDestinationToUsersPage()
        {
            _destToUserPage.VerifyNewlyImportedUserDisplayed();
        }

        [Then(@"I verify the user is not displayed in the list")]
        public void ThenIVerifyTheUserIsNotDisplayedInTheList()
        {
            _destToUserPage.VerifyUserNotDisplayed();
        }


    }
}