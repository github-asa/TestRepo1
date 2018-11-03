using J2BIOverseasOps.Pages.BuildingWork.Summary;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.BuildingWork
{
    [Binding]
    public sealed class SummaryPageStepDefs : BaseStepDefs
    {
        private readonly BuildingWorkSummaryPage _buildingWorkSummary;

        public SummaryPageStepDefs(IWebDriver driver, ILog log,IRunData rundata) : base(driver, log)
        {
            _buildingWorkSummary = new BuildingWorkSummaryPage(driver, log,rundata);
        }

        [Then(@"I verify the value of the following fields on the summary page:")]
        public void ThenIVerifyTheValueOfTheFollowingFieldsOnTheSummaryPage(Table table)
        {
            _buildingWorkSummary.VerifyFieldsOnSummaryPage(table);
        }

        [Then(@"I verify the value of the following fields for the phases on the summary page:")]
        public void ThenIVerifyTheValueOfTheFollowingFieldsForThePhasesOnTheSummaryPage(Table table)
        {
            _buildingWorkSummary.VerifyPhasesDates(table);
        }

        [Then(@"I verify the phases dates are not displayed on the summary page")]
        public void ThenIVerifyThePhasesDatesAreNotDisplayedOnTheSummaryPage()
        {
            _buildingWorkSummary.VerifyPhaseDatesNotDisplayed();
        }

        [Then(@"I verify number of affected bookings and customers are correct for the data userid ""(.*)"",date from ""(.*)"", date to ""(.*)"",destination ""(.*)"",resort ""(.*)"" and property ""(.*)""")]
        public void VerifyCustomersAndBookingsAffected(string userId, string dateFrom, string dateTo, string destination, string resort, string property)
        {

            _buildingWorkSummary.VerifyCustomersAndBookingsAffected( userId,  dateFrom,  dateTo,  destination,  resort, property);
        }

        [Then(@"I verify the affected bookings and customers are displayed")]
        public void ThenIVerifyTheAffectedBookingsAndCustomersAreDisplayed()
        {
            _buildingWorkSummary.VerifyCustomersAndBookingsAffectedDisplay();
        }

        [Then(@"I can enter the following answers for the following fields on summary page:")]
        [When(@"I enter the following answers for the following fields on summary page:")]
        public void WhenIEnterTheFollowingAnswersForTheFollowingFieldsOnSummaryPage(Table table)
        {
            _buildingWorkSummary.EnterSummaryPageAnswers(table);
        }

        [Then(@"I verify the following answers for the following fields on summary page:")]
        public void ThenIVerifyTheFollowingAnswersForTheFollowingFieldsOnSummaryPage(Table table)
        {
            _buildingWorkSummary.VerifySummaryPageAnswers(table);
        }


        [When(@"I click the save and close button on summary page")]
        public void WhenIClickTheSaveAndCloseButtonOnSummaryPage()
        {
            _buildingWorkSummary.ClickSaveAndCloseButton();
        }

        [Then(@"I can see the following mandatory error message on the following summary page fields:")]
        public void ThenICanSeeTheFollowingMandatoryErrorMessageOnTheFollowingSummaryPageFields(Table table)
        {
            _buildingWorkSummary.VerifSummaryValidationErrorMessage(table);
        }


    }
}