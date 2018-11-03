using J2BIOverseasOps.Pages.BuildingWork.Roadworks.OverallImpact;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.BuildingWork.Roadworks
{
    [Binding]
    public sealed class RoadworksOverallImpactStepDefs : BaseStepDefs
    {
        private readonly RoadworksOverallImpactPage _overAllImpact;

        public RoadworksOverallImpactStepDefs(IWebDriver driver, ILog log, IRunData rundata) : base(driver, log)
        {
            _overAllImpact = new RoadworksOverallImpactPage(driver, log, rundata);
        }

        [Then(@"I can enter the following answers for the following questions on the Roadworks overall impact page:")]
        [When(@"I enter the following answers for the following questions on the Roadworks overall impact page:")]
        public void WhenIEnterTheFollowingAnswersForTheFollowingQuestionsOnTheTheRoadworksGeneralQuestionsPage(Table table)
        {
            _overAllImpact.EnterOverallImpactQAnswers(table);
        }

        [Then(@"I am ""(displayed|not displayed)"" the following Pselect option fields on the Roadworks overall impact page:")]
        public void ThenIAmTheFollowingPselectOptionFieldsOnTheRoadworksOverallImpactPage(string displayedOrNot, Table table)
        {
            _overAllImpact.VerifyPselectOptionDisplayedOrNot(displayedOrNot, table);
        }

        [Then(@"I can select the following Pselect options fields on the Roadworks overall impact page:")]
        public void ThenICanSelectTheFollowingPselectOptionsFieldsOnTheTheRoadworksOverallImpactPage(Table table)
        {
            _overAllImpact.VerifyCanSelectPOption(table);
        }

        [Then(@"I can see the following mandatory error message on the following Roadworks overall impact fields:")]
        public void ThenICanSeeTheFollowingMandatoryErrorMessageOnTheFollowingRoadworksGeneralQuestionsFields(Table table)
        {
            _overAllImpact.VerifyRoadworksMandatoryMessage(table);
        }
    }
}