using System.Collections.Generic;
using System.Linq;
using J2BIOverseasOps.Extensions;
using J2BIOverseasOps.Models;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Pages.UserManagement
{
    internal class AssignDestToUserPage : CommonPageElements
    {
        private static string _fullName = "";
        private readonly By _destinationButton = By.XPath("//div[@class='destinationContents']//button");

        private readonly By _destinationToUserTab =
            By.XPath("//span[contains(@class,'ui-tabview-title')][contains(text(),'Assign Destination to User')]/..");
        private readonly By _destinationToUserTabSelected =
            By.XPath("//*[@id='ui-tabpanel-0-label'][@aria-selected='true']");
        private readonly string _nonSelectedDestinations =
            "//button[contains(@class, 'destinationBtn') and contains(@class,'ui-button-gray')]";
        private readonly By _noUserFound = By.XPath("//app-user-search//li[@style='display: none;']");
        private readonly By _noUserSelectedMessage = By.ClassName("messageBrd");
        private readonly By _searchUserInput = By.XPath("//app-user-search//input[@role='textbox']");
        private readonly string _selectedDestinations =
            "//button[contains(@class, 'destinationBtn') and contains(@class,'ui-button-success')]";
        private readonly By _selectedUserLabelFooter = By.XPath("//*[@name='selectedUserName']//strong");
        private readonly string _userNameEmail = "//*[@id='userNameAndEmailId'][contains(.,'nameOrEmail')]";
        private string UserFnameLname=>$"//*[@id='name']//*[contains(text(),'{_fullName}')]";
        private readonly string _editViewProfileLink = $"/../../..//*[@id='viewEdit']";
        public AssignDestToUserPage(IWebDriver driver, ILog log, IRunData rundata) : base(driver, log)
        {

        }

        public void NavigateToDestToUserTab()
        {
            WaitForSpinnerToDisappear();
            WaitForSpinnerToDisappear();
            Driver.ClickItem(_destinationToUserTab);
            Driver.WaitForItem(_destinationToUserTabSelected);
            WaitForSpinnerToDisappear();
            WaitForSpinnerToDisappear();
        }


        public void SearchUser(int userIndex)
        {
            _currentUser = GetListOfUsers()[userIndex];
            Driver.EnterText(_searchUserInput,$"{_currentUser.forename} {_currentUser.surname}");
            WaitForSpinnerToDisappear();
        }

        public void SearcRandomUser()
        {
            var apiCall=new ApiCalls(new RunData());
            var listOfUsers = GetListOfUsers();
            var userNumberToFind = 0;
            for (var i = 0; i < 5; i++)
            {
                userNumberToFind = userNumberToFind.GenerateRandomNumber(0, listOfUsers.Count);
                var user = apiCall.GetUserDetails(userNumberToFind);
                if (user.forename!=""&user.surname!="")
                {
                    break;
                }
            }
            SearchUser(userNumberToFind);
            SelectCurrentUserFromList();
        }

        public void SearchUser(string user)
        {
            Driver.EnterText(_searchUserInput,user);
            WaitForSpinnerToDisappear();
        }

        public void SelectCurrentUserFromList()
        {
            SelectUserFromList($"{_currentUser.forename} {_currentUser.surname}");
        }


        public void SelectUserFromList(string name)
        {
            _fullName = name;
            Driver.ClickItem(By.XPath(UserFnameLname));
            Driver.WaitForPageToLoad();
        }

        public void VerifyNoResultFound()
        {
            Assert.True(Driver.IsElementPresent(_noUserFound));
        }

        public void VerifyNoUserUiMessage(string expectedMessage)
        {
            Assert.True(Driver.FindElement(_noUserSelectedMessage).Text.Contains(expectedMessage));
        }

        public void VerifyMappedDestinations(string expectedMappedDestinations)
        {
            if (expectedMappedDestinations.ToLower() == "none")
            {
                Assert.True(GetSelectedDestinations().Count == 0,
                    "Unable to verify the mapped destinations as 0,Expected mapped destinations :" + 0 +
                    " Actual Mapped :" + GetSelectedDestinations().Count);
            }
            else
            {
                IEnumerable<string> listOfExpectedDestinations;
                switch (expectedMappedDestinations.ToLower())
                {
                    case "all":
                        listOfExpectedDestinations = GetAllDestinations();
                        break;
                    default:
                        listOfExpectedDestinations = expectedMappedDestinations.ConvertStringIntoList();
                        break;
                }

                var ofExpectedDestinations = listOfExpectedDestinations.ToList();
                Assert.True(GetSelectedDestinations().All(ofExpectedDestinations.Contains),
                    "Expected list of destinations did not match with actual list of destinations,Expected size :" +
                    ofExpectedDestinations.Count() + "Actual size :" + GetSelectedDestinations().Count);
            }
        }

        public void VerifyUserSelectedLabel(string fnameLname)
        {
            Assert.True(Driver.FindElement(_selectedUserLabelFooter).Text.Contains(fnameLname));
        }

        // notes down the selected destinations
        public void NoteDownSelectedDestinations()
        {
            ScenarioContext.Current.Add("userDefaultDestinations", GetSelectedDestinations().ConvertListIntoString());
        }

        public void SelectDestinations(string destinations)
        {
            Driver.WaitForItem(_destinationButton);
            DeselectAllDestinations();
            IEnumerable<string> listOfDestinationsToSelect;
            switch (destinations.ToLower())
            {
                case "all":
                    listOfDestinationsToSelect = GetAllDestinations();
                    break;
                case "none":
                    listOfDestinationsToSelect = GetSelectedDestinations();
                    break;
                default:
                    listOfDestinationsToSelect = destinations.ConvertStringIntoList();
                    break;
            }

            foreach (var destination in listOfDestinationsToSelect)
            {
                Driver.ClickItem(By.XPath(_nonSelectedDestinations + "[contains(.,'" + destination + "')]"));
            }
        }

        public void VerifyDestinationComponentsNotDisplayed()
        {
            Assert.True(!Driver.IsElementPresent(_destinationToUserTab),
                $"Was able to find Assign Destinations to Users tab while expecting it not to be found.");
            Assert.True(!Driver.IsElementPresent(_searchUserInput),
                $"_searchUserInput was displayed while expecting it not displayed");
            Assert.True(!Driver.IsElementPresent(_selectedUserLabelFooter),
                $"_selectedUserLabelFooter was displayed while expecting it not to be displayed");
            Assert.True(!Driver.IsElementPresent(_destinationButton),
                $"_destinationButton was displayed while expecting it not to be displayed");
        }




        public void ClickEditViewProfileLink()
        {
            var viewEditProfileLink =By.XPath(UserFnameLname + _editViewProfileLink);
            Driver.ClickItem(viewEditProfileLink);
        }

        #region Helpers

        // gets list of all the selected destinations
        internal IList<string> GetSelectedDestinations()
        {
            Driver.WaitForItem(_destinationButton);
            var listOfSelectedDestinations = Driver.FindElements(By.XPath(_selectedDestinations));
            var selectedDestinationsText = new List<string>();
            foreach (var destination in listOfSelectedDestinations)
            {
                selectedDestinationsText.Add(destination.Text);
            }

            return selectedDestinationsText;
        }

        // gets list of all the deselected destinations
        internal IList<string> GetDeSelectedDestinations()
        {
            Driver.WaitForItem(_destinationButton);
            var listOfDeSelectedDestinations = Driver.FindElements(By.XPath(_nonSelectedDestinations));
            var deSelectedDestinationsText = new List<string>();
            foreach (var destination in listOfDeSelectedDestinations)
            {
                deSelectedDestinationsText.Add(destination.Text);
            }

            return deSelectedDestinationsText;
        }

        // deselects all the selected destinations
        internal void DeselectAllDestinations()
        {
            Driver.WaitForItem(_destinationButton);
            var listOfSelectedDestinations = Driver.FindElements(By.XPath(_selectedDestinations));
            foreach (var destination in listOfSelectedDestinations)
            {
                Driver.ClickItem(destination);
            }
        }

        // gets the list of all the destinations
        internal IEnumerable<string> GetAllDestinations()
        {
            var selectedDestinations = GetSelectedDestinations();
            var deselectedDestinations = GetDeSelectedDestinations();
            return selectedDestinations.Concat(deselectedDestinations);
        }

        private void _verifyFnameLname(string fnamelname)
        {
            _fullName = fnamelname;
            Assert.True(Driver.IsElementPresent(By.XPath(UserFnameLname)));
        }

        private void _verifyUsername(string username)
        {
            Assert.True(Driver.IsElementPresent(By.XPath(_userNameEmail.Replace("nameOrEmail", username))));
        }

        private void _verifyWorkEmail(string workEmail)
        {
            Assert.True(Driver.IsElementPresent(By.XPath(_userNameEmail.Replace("nameOrEmail", workEmail))));
        }
        #endregion


        public void VerifyUserDetails(Table table)
        {
            foreach (var row in table.Rows)
            {
                var expectedField = row["field_to_verify"];
                switch (expectedField.ToLower())
                {
                    case "fnamelname":
                        _verifyFnameLname($"{_currentUser.forename} {_currentUser.surname}");
                        break;
                    case "username":
                        _verifyUsername(_currentUser.username);
                        break;
                    case "work email":
                        _verifyWorkEmail(_currentUser.workEmail);
                        break;
                    case "user_fname user_lname_footer":
                        Assert.True(Driver.FindElement(_selectedUserLabelFooter).Text.Contains($"{_currentUser.forename} {_currentUser.surname}"));
                        break;
                    default:
                        Assert.Fail($"{expectedField} is not a valid field");
                        break;
                }
            }

        }

        public void VerifyNewlyImportedUserDisplayed()
        {
            var employeeToVerify = ScenarioContext.Current["new_employee"] as Employee;
            _fullName = $"{employeeToVerify?.FirstName} {employeeToVerify?.LastName}";
            var found = false;
            for (var i = 0; i < 5; i++)
            {
                SearchUser(employeeToVerify?.FirstName);
                WaitForSpinnerToDisappear();
                found = Driver.WaitForItem(By.XPath(UserFnameLname), 2);
                if (found)
                {
                    break;
                }
                Driver.RefreshPage();
                Driver.WaitForPageToLoad();
                WaitForSpinnerToDisappear();
            }

            if (!found)
            {
                Assert.Fail($"Could not find user {employeeToVerify?.FirstName} {employeeToVerify?.LastName}");
            }

            _verifyWorkEmail(employeeToVerify?.EmailAddress);
            _verifyUsername(employeeToVerify?.UserName);
        }


        public void VerifyUserNotDisplayed()
        {
            Driver.RefreshPage();
            Driver.WaitForPageToLoad();
            WaitForSpinnerToDisappear();

            var employeeToVerify = ScenarioContext.Current["new_employee"] as Employee;
            _fullName = $"{employeeToVerify?.FirstName} {employeeToVerify?.LastName}";
            var found = true;
            for (var i = 0; i < 5; i++)
            {
                SearchUser(employeeToVerify?.FirstName);
                WaitForSpinnerToDisappear();
                found = Driver.WaitForItem(By.XPath(UserFnameLname), 2);
                if (!found)
                {
                    break;
                }
                Driver.RefreshPage();
                Driver.WaitForPageToLoad();
                WaitForSpinnerToDisappear();
            }

            if (found)
            {
                Assert.Fail($"Was able to  find user {employeeToVerify?.FirstName} {employeeToVerify?.LastName}, expecting it not to be found");
            }

        }

    }
}