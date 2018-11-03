using J2BIOverseasOps.Pages.BuildingWork.Nearby.OverallImpact;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.BuildingWork.Nearby
{
    [Binding]
    public sealed class NearbyOverallImpactStepDefs : BaseStepDefs
    {
        private readonly NearbyOverallImpactPage _overAllImpact;

        public NearbyOverallImpactStepDefs(IWebDriver driver, ILog log, IRunData rundata) : base(driver, log)
        {
            _overAllImpact = new NearbyOverallImpactPage(driver, log, rundata);
        }

        [Then(@"I can enter the following answers for the following questions on the Nearby overall impact page:")]
        [When(@"I enter the following answers for the following questions on the Nearby overall impact page:")]
        public void WhenIEnterTheFollowingAnswersForTheFollowingQuestionsOnTheTheNearbyGeneralQuestionsPage(Table table)
        {
            _overAllImpact.EnterOverallImpactQAnswers(table);
        }

        [Then(@"I am ""(displayed|not displayed)"" the following Pselect option fields on the Nearby overall impact page:")]
        public void ThenIAmTheFollowingPselectOptionFieldsOnTheNearbyOverallImpactPage(string displayedOrNot, Table table)
        {
            _overAllImpact.VerifyPselectOptionDisplayedOrNot(displayedOrNot, table);
        }

        [Then(@"I can select the following Pselect options fields on the Nearby overall impact page:")]
        public void ThenICanSelectTheFollowingPselectOptionsFieldsOnTheTheNearbyOverallImpactPage(Table table)
        {
            _overAllImpact.VerifyCanSelectPOption(table);
        }

        [Then(@"I can see the following mandatory error message on the following Nearby overall impact fields:")]
        public void ThenICanSeeTheFollowingMandatoryErrorMessageOnTheFollowingNearbyGeneralQuestionsFields(Table table)
        {
            _overAllImpact.VerifyNearbyMandatoryMessage(table);
        }
    }
}