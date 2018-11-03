using J2BIOverseasOps.Pages.BuildingWork.Nearby.GeneralQuestions;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.BuildingWork.Nearby
{
    [Binding]
    public sealed class NearbyGeneralQuestionsStepDefs : BaseStepDefs
    {
        private readonly NearbyGeneralQPage _nearbyGeneralQ;

        public NearbyGeneralQuestionsStepDefs(IWebDriver driver, ILog log, IRunData rundata) : base(driver, log)
        {
            _nearbyGeneralQ = new NearbyGeneralQPage(driver, log, rundata);
        }

        [Then(@"I can enter the following answers for the following questions on the Nearby General Questions page:")]
        [When(@"I enter the following answers for the following questions on the Nearby General Questions page:")]
        public void WhenIEnterTheFollowingAnswersForTheFollowingQuestionsOnTheNearbyGeneralQuestionsPage(Table table)
        {
            _nearbyGeneralQ.EnterNearbyGeneralQAnswers(table);
        }

        [Then(@"I am ""(.*)"" the following fields on the Nearby General Questions page")]
        public void ThenIAmTheFollowingFieldsOnTheNearbyGeneralQuestionsPage(string displayedOrNot, Table table)
        {
            _nearbyGeneralQ.VerifyFieldsDisplayedOrNot(displayedOrNot, table);
        }

        [Then(@"I can see the following mandatory error message on the following Nearby General Questions fields:")]
        public void ThenICanSeeTheFollowingMandatoryErrorMessageOnTheFollowingNearbyGeneralQuestionsFields(Table table)
        {
            _nearbyGeneralQ.VerifyNearbyMandatoryMessage(table);
        }
    }
}