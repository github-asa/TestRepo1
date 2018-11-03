using J2BIOverseasOps.Pages;
using J2BIOverseasOps.Pages.PreSeasonChecklist;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.PreSeasonChecklist
{
    [Binding]
    public class ViewAutoCreatedPscActionSteps : BaseStepDefs
    {
        private readonly ViewActionListPage _viewActionListPage;

        private readonly ViewActionPage _viewActionPage;

        public ViewAutoCreatedPscActionSteps(IWebDriver driver, ILog log) : base(driver, log)
        {
           _viewActionListPage = new ViewActionListPage(driver, log);
           _viewActionPage = new ViewActionPage(driver, log);
        }

        [Then(@"the action should be displayed on the Action List page")]
        public void ThenTheActionShouldBeDisplayedOnTheActionListPage()
        {
            _viewActionListPage.VerifyActionsList();
        }

        [Then(@"the action should not be displayed on the Action List page")]
        public void ThenTheActionShouldNotBeDisplayedOnTheActionListPage()
        {
            _viewActionListPage.VerifyActionListDoesNotContainAction();
        }


        [When(@"I select the action on the actions page")]
        public void WhenISelectTheAction()
        {
            _viewActionListPage.ClickEditActionButton();
        }

        [Then(@"I verify the Summary and Failed Questions headings on the page as '(.*)', '(.*)', '(.*)', '(.*)', '(.*)'")]
        public void ThenIVerifyTheSummaryAndFailedQuestionsHeadingsOnThePageAs(string summary, string action, string description, string failedquestions, string notes)
        {
            _viewActionPage.VerifySummaryAndFailedQuestionsTitlesAreDisplayed(summary, action, description, failedquestions, notes);
        }

        [Then(@"I verify the Assignment headings on the page as '(.*)', '(.*)', '(.*)', '(.*)', '(.*)', '(.*)', '(.*)'")]
        public void ThenIVerifyTheAssignmentHeadingsOnThePageAs(string assignment, string assignedto, string person, string progressstatus, string duedate, string notes, string linkedactions)
        {
            _viewActionPage.VerifyAssignmentHeadingsAreDisplayed(assignment, assignedto, person, progressstatus, duedate, notes, linkedactions);
        }

        [Then(@"I verify the following header details are displayed on the view action page:")]
        public void ThenTheFollowingHeaderDetailsAreDisplayedOnTheViewActionPage(Table table)
        {
            _viewActionPage.VerifyHeaderDetails(table);
        }

        [Then(@"I verify the following Summary details are displayed on the view action page:")]
        public void ThenTheFollowingSummaryDetailsAreDisplayedOnTheViewActionPage(Table table)
        {
            _viewActionPage.VerifySummaryDetailsAreDisplayed(table);
        }

        [Then(@"I verify the following Failed Questions details are displayed on the view action page:")]
        public void ThenTheFollowingFailedQuestionsDetailsAreDisplayedOnTheViewActionPage(Table table)
        {
            _viewActionPage.VerifySummaryAndFailedQuestionsDetailsAreDisplayed(table);
        }

        [Then(@"I verify the following Assignment details are displayed on the view action page:")]
        public void ThenTheFollowingAssignmentDetailsAreDisplayedOnTheViewActionPage(Table table)
        {
            _viewActionPage.VerifyAssignmentDetailsAreDisplayed(table);
        }

        [Then(@"I verify the following Notes details are displayed on the view action page:")]
        public void ThenTheFollowingNotesDetailsAreDisplayedOnTheViewActionPage(Table table)
        {
            _viewActionPage.VerifyNotesAreDisplayed(table);
        }

        [Then(@"I verify the following Linked Actions are displayed on the view action page:")]
        public void ThenTheFollowingLinkedActionsAreDisplayedOnTheViewActionPage(Table table)
        {
            _viewActionPage.VerifyLinkedActionsAreDisplayed(table);
        }
    }
}