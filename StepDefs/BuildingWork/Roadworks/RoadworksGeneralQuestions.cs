using J2BIOverseasOps.Pages.BuildingWork.Roadworks.GeneralQuestions;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.BuildingWork.Roadworks
{
    [Binding]
    public sealed class RoadworksGeneralQuestionsStepDefs : BaseStepDefs
    {
        private readonly RoadworksGeneralQPage _nearbyGeneralQ;

        public RoadworksGeneralQuestionsStepDefs(IWebDriver driver, ILog log, IRunData rundata) : base(driver, log)
        {
            _nearbyGeneralQ = new RoadworksGeneralQPage(driver, log, rundata);
        }

        [Then(@"I can enter the following answers for the following questions on the Roadworks General Questions page:")]
        [When(@"I enter the following answers for the following questions on the Roadworks General Questions page:")]
        public void WhenIEnterTheFollowingAnswersForTheFollowingQuestionsOnTheRoadworksGeneralQuestionsPage(Table table)
        {
            _nearbyGeneralQ.EnterRoadworksGeneralQAnswers(table);
        }

        [Then(@"I am ""(.*)"" the following fields on the Roadworks General Questions page")]
        public void ThenIAmTheFollowingFieldsOnTheRoadworksGeneralQuestionsPage(string displayedOrNot, Table table)
        {
            _nearbyGeneralQ.VerifyFieldsDisplayedOrNot(displayedOrNot, table);
        }

        [Then(@"I can see the following mandatory error message on the following Roadworks General Questions fields:")]
        public void ThenICanSeeTheFollowingMandatoryErrorMessageOnTheFollowingRoadworksGeneralQuestionsFields(Table table)
        {
            _nearbyGeneralQ.VerifyRoadworksMandatoryMessage(table);
        }
    }
}