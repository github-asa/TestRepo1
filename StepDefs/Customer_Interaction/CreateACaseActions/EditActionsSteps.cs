using J2BIOverseasOps.Pages.Customer_Interaction.CreateACaseActions;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.Customer_Interaction.CreateACaseActions
{
    [Binding]
    public class EditActionsSteps : BaseStepDefs
    {
        private readonly CaseEditActionPage _caseEditActionPage;

        public EditActionsSteps(IWebDriver driver, ILog log) : base(driver, log)
        {
            _caseEditActionPage = new CaseEditActionPage(driver, log);
        }

        [Then(@"I verify the headings on the Case Edit Action page '(.*)', '(.*)', '(.*)', '(.*)', '(.*)', '(.*)'")]
        public void ThenIVerifyTheHeadingsOnTheCaseCreateAnActionPage(string header, string action, string description, string assignTo, string assignToPerson, string dueDate)
        {
            _caseEditActionPage.VerifyAddActionHeadings(header, action, description, assignTo, assignToPerson, dueDate);
        }

        [Then(@"I verify that the following details are displayed on the Case Edit Action page:")]
        public void ThenIVerifyThatTheFollowingDetailsAreDisplayedCaseEditActionPage(Table table)
        {
            _caseEditActionPage.VerifyEditDetails(table);
        }

        [When(@"I update the action on the Case Edit Action page:")]
        public void WhenIUpdateTheActionOnTheCaseEditActionPage(Table table)
        {
            _caseEditActionPage.UpdateAction(table);
        }

        [When(@"I click save on the Case Edit Action page")]
        public void WhenIClickSaveOnTheCaseEditActionPage()
        {
            _caseEditActionPage.ClickSave();
        }

        [Then(@"I verify the User list is alphabetically ordered and the usernames are below the full names in the list on the Case Edit Action page")]
        public void ThenIVerifyTheUserListIsAlphabeticallyOrderedAndTheUsernamesAreBelowTheFullNamesInTheListOnTheCaseEditActionPage()
        {
            _caseEditActionPage.VerifyUsersCaseActions();
        }

        [Then(@"I verify that the usernames are displayed when the user has no firstname or last name or both in the system on the Case Edit Action page:")]
        public void ThenIVerifyThatTheUsernamesAreDisplayedWhenTheUserHasNoFirstnameOrLastNameOrBothInTheSystemOnTheCaseEditActionPage(Table table)
        {
            _caseEditActionPage.VerifyUserListContains(table);
        }

        [Then(@"I verify that the Assign to person dropdown is disabled on the Case Edit Action page")]
        public void ThenIVerifyThatTheAssignToPersonDropdownIsDisabledOnTheCaseEditActionPage()
        {
            _caseEditActionPage.VerifyAssignToPersonIsDisabled();
        }

        [Then(@"I verify that the Assign to person dropdown is enabled on the Case Edit Action page")]
        public void ThenIVerifyThatTheAssignToPersonDropdownIsEnabledOnTheCaseEditActionPage()
        {
            _caseEditActionPage.VerifyAssignToPersonIsEnabled();
        }

        [Then(@"I verify that there are no users selected on the Case Edit Action page")]
        public void ThenIVerifyThatThereAreNoUsersSelectedOnTheCaseEditActionPage()
        {
            _caseEditActionPage.VerifyAssignToPersonIsEmpty();
        }

        [When(@"I clear the assign to person dropdown on the Case Edit Action page")]
        public void WhenIClearTheAssignToPersonDropdownOnTheCaseEditActionPage()
        {
            _caseEditActionPage.ClearAssignToPerson();
        }

        [Then(@"I verify that the dates in the past for Due Date are disabled '(.*)' on the Case Edit Action page")]
        public void ThenIVerifyThatTheDatesInThePastForDueDateAreDisabledOnTheCaseEditActionPage(string days)
        {
            _caseEditActionPage.VerifyDueDatesAreDisabled(days);
        }

        [When(@"I enter a due date of '(.*)' on the Case Edit Action page")]
        public void WhenIEnterADueDateOfOnTheCaseEditActionPage(string date)
        {
            _caseEditActionPage.EnterADueDate(date);
        }

        [When(@"I enter a note '(.*)' on the Case Edit Action page")]
        public void WhenIEnterANoteOnTheCaseEditActionPage(string note)
        {
            _caseEditActionPage.AddNote(note);
        }

        [Then(@"I verify that the following notes are displayed with a timestamp on the Case Edit Action page:")]
        public void ThenIVerifyThatTheFollowingNotesAreDisplayedWithATimestampOnTheCaseEditActionPage(Table table)
        {
            _caseEditActionPage.VerifyNotes(table);
        }

        [Then(@"I verify that the due date field is readonly on the Case Edit Action page")]
        public void ThenIVerifyThatTheDueDateFieldIsReadonlyOnTheCaseEditActionPage()
        {
           _caseEditActionPage.VerifyDueDateIsReadonly();
        }
    }
}