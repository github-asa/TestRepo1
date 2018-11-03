using J2BIOverseasOps.Pages.Customer_Interaction.CreateACaseActions;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.Customer_Interaction.CreateACaseActions
{
    [Binding]
    public class CcmeAddActionsSteps : BaseStepDefs
    {
        private readonly CaseViewActionsPage _caseViewActions;

        private readonly CaseAddActionsPage _caseAddActionsPage;

        public CcmeAddActionsSteps(IWebDriver driver, ILog log) : base(driver, log)
        {
            _caseViewActions = new CaseViewActionsPage(driver,log);
            _caseAddActionsPage = new CaseAddActionsPage(driver, log);
        }

        [When(@"I click the Add Action button on the Case Management Actions and Notes page")]
        public void WhenIClickTheAddActionButtonOnTheCaseManagementActionsAndNotesPage()
        {
            _caseViewActions.ClickAddAction();
        }

        [When(@"I fill in the action on the Case Create an Action page:")]
        public void WhenIFillInTheActionOnTheCaseCreateAnActionPage(Table table)
        {
            _caseAddActionsPage.EnterActionDetails(table);
        }

        [When(@"I click the save button on the Case Create an Action page")]
        public void WhenIClickTheSaveButtonOnTheCaseCreateAnActionPage()
        {
            _caseAddActionsPage.ClickSave();
        }

        [Then(@"I verify that the Case Active actions table contains the following actions:")]
        public void ThenIVerifyThatTheCaseActiveActionsTableContainsTheFollowingActions(Table table)
        {
            _caseViewActions.VerifyActiveActionsTable(table);
        }

        [Then(@"I verify that the Case Closed actions table contains the following actions:")]
        public void ThenIVerifyThatTheCaseClosedActionsTableContainsTheFollowingActions(Table table)
        {
            _caseViewActions.VerifyClosedActionsTable(table);
        }

        [Then(@"I verify that the Case Action list on the Case Create an Action page should contain:")]
        public void ThenTheCaseActionListOnTheCaseCreateAnActionPageShouldContain(Table table)
        {
            _caseAddActionsPage.VerifyActionsList(table);
        }

        [Then(@"I verify the headings on the Case Management Actions and Notes page '(.*)', '(.*)', '(.*)'")]
        public void ThenIVerifyTheHeadingsOnTheCaseManagementActionsAndNotesPage(string heading, string subheading1, string subheading2)
        {
            _caseViewActions.VerifyPageHeadings(heading, subheading1, subheading2);
        }

        [Then(@"I verify the headings for the Case Active actions table '(.*)', '(.*)', '(.*)', '(.*)', '(.*)', '(.*)'")]
        public void ThenIVerifyTheHeadingsForTheCaseActiveActionsTable(string actionId, string category, string status, string dueDate, string assignedDept, string assignedUser)
        {
            _caseViewActions.VerifyActiveTableActions(actionId, category, status, dueDate, assignedDept, assignedUser);
        }

        [Then(@"I verify the headings on the Case Closed actions table '(.*)', '(.*)', '(.*)', '(.*)', '(.*)', '(.*)'")]
        public void ThenIVerifyTheHeadingsOnTheCaseClosedActionsTable(string caseId, string category, string status, string dueDate, string assignedDept, string assignedUser)
        {
            _caseViewActions.VerifyClosedTableActions(caseId, category, status, dueDate, assignedDept, assignedUser);
        }

        [Then(@"I verify the headings on the Case Create an Action page '(.*)', '(.*)', '(.*)', '(.*)', '(.*)', '(.*)'")]
        public void ThenIVerifyTheHeadingsOnTheCaseCreateAnActionPage(string header, string action, string description, string assignTo, string assignToPerson, string dueDate)
        {
            _caseAddActionsPage.VerifyAddActionHeadings(header, action, description, assignTo, assignToPerson, dueDate);
        }

        [Then(@"I verify that the Assign to person dropdown is disabled on the Case Create an Action page")]
        public void ThenIVerifyThatTheAssignToPersonDropdownIsDisabledOnTheCaseCreateAnActionPage()
        {
            _caseAddActionsPage.VerifyAssignToPersonIsDisabled();
        }

        [Then(@"I verify that the Assign to person dropdown is enabled on the Case Create an Action page")]
        public void ThenIVerifyThatTheAssignToPersonDropdownIsEnabledOnTheCaseCreateAnActionPage()
        {
            _caseAddActionsPage.VerifyAssignToPersonIsEnabled();
        }

        [Then(@"I verify the User list is alphabetically ordered and the usernames are below the full names in the list on the Case Create an Action page")]
        public void ThenIVerifyTheUserListIsAlphabeticallyOrderedFullNamesAboveUsernamesUsernamesDisplayedWhenFullNameIsIncomplete()
        {
            _caseAddActionsPage.VerifyUsersCaseActions();
        }

        [Then(@"I verify that the usernames are displayed when the user has no firstname or last name or both in the system on the Case Create an Action page:")]
        public void ThenIVerifyThatTheUsernamesAreDisplayedWhenTheeUserHasNoFirstnameOrLastNameOrBothInTheSystem(Table table)
        {
            _caseAddActionsPage.VerifyUserListContains(table);
        }

        [Then(@"I verify that the dates in the past for Due Date are disabled '(.*)' on the Case Create an Action page")]
        public void ThenIVerifyThatTheDatesInThePastForDueDateAreDisabled(string days)
        {
            _caseAddActionsPage.VerifyDueDatesAreDisabled(days);
        }

        [When(@"I enter a due date of '(.*)' on the Case Create an Action page")]
        public void ThenIEnterADueDateOfOnTheCaseCreateAnActionPage(string date)
        {
            _caseAddActionsPage.EnterADueDate(date);
        }

        [Then(@"I verify that the Due Date is set to '(.*)' day\(s\) ahead")]
        public void ThenIVerifyThatTheDueDateIsSetToDaySAhead(string days)
        {
            _caseAddActionsPage.VerifyDueDate(days);
        }

        [Then(@"I verify that there are no users selected on the Case Create an Action page")]
        public void ThenIVerifyThatThereAreNoUsersSelectedOnTheCaseCreateAnActionPage()
        {
            _caseAddActionsPage.VerifyAssignToPersonIsEmpty();
        }

        [When(@"I clear the assign to person dropdown on the Case Create an Action page")]
        public void WhenIClearTheAssignToPersonDropdownOnTheCaseCreateAnActionPage()
        {
            _caseAddActionsPage.ClearAssignToPerson();
        }

        [When(@"I click the Edit Action on the Case Management Actions and Notes page for:")]
        public void WhenIClickTheEditActionOnTheCaseManagementActionsAndNotesPageFor(Table table)
        {
            _caseViewActions.ClickEditActionFor(table);
        }

        [Then(@"I verify that the due date field is readonly on the Case Create an Action page")]
        public void ThenIVerifyThatTheDueDateFieldIsReadonlyOnTheCaseCreateAnActionPage()
        {
            _caseAddActionsPage.VerifyDueDateIsReadonly();
        }

        [When(@"I click back to overview on the Case Management Actions page")]
        public void WhenIClickBackToOverviewOnTheCaseManagementActionsPage()
        {
            _caseAddActionsPage.ClickBackToOverview();
        }

        [Then(@"I verify that the total number of action on the case overview page is '(.*)'")]
        public void ThenIVerifyThatTheTotalNumberOfActionOnTheCaseOverviewPageIs(string total)
        {
            _caseAddActionsPage.VerifyTotalActionsCount(total);
        }

        [Then(@"I verify that the total number of due actions on the case overview page is '(.*)'")]
        public void ThenIVerifyThatTheTotalNumberOfDueActionsOnTheCaseOverviewPageIs(string total)
        {
            _caseAddActionsPage.VerifyDueActionsCount(total);
        }

    }
}