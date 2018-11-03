using J2BIOverseasOps.Pages.BuildingWork.Nearby.NearbyIntroQs;
using J2BIOverseasOps.Pages.BuildingWork;
using TechTalk.SpecFlow;
using log4net;
using OpenQA.Selenium;

namespace J2BIOverseasOps.StepDefs.BuildingWork.Nearby.NearbyIntroQs
{

    [Binding]
    public sealed class NearbyIntroQsStepDefs : BaseStepDefs
    {
        private readonly NearbyIntroQsPage _NearbyIntroQs;
        private readonly BuildingWorkCommon _bwc;
        private readonly Pages.CommonPageElements _commonElements;
        private readonly StepDefs.CommonStepDefs _commonSteps;

        public NearbyIntroQsStepDefs(IWebDriver driver, ILog log, IRunData rundata) : base(driver, log)

        {
            _NearbyIntroQs = new NearbyIntroQsPage(driver, log, rundata);
            _bwc = new BuildingWorkCommon(driver, log, rundata);
            _commonElements = new Pages.CommonPageElements(driver, log);
            _commonSteps = new StepDefs.CommonStepDefs(driver, log, rundata);
        }

        [Then(@"I verify following fields are ""(displayed|not displayed*)"" on the Nearby Intro Questions page")]
        [When(@"I verify following fields are ""(displayed|not displayed*)"" on the Nearby Intro Questions page")]
        public void ThenIVerifyFollowingFieldsAreOnTheRoadworksIntroQuestionsPage(string displayedNotDisplayed, Table table)
        {
            _NearbyIntroQs.VerifyFieldsDisplayedOrNot(displayedNotDisplayed, table);
        }

        [When(@"I enter the following answers for the following questions on the Nearby Intro Questions page")]
        [Then(@"I enter the following answers for the following questions on the Nearby Intro Questions page")]
        public void ThenIEnterTheFollowingAnswersForTheFollowingQuestionsOnTheRoadworksIntroQuestionsPage(Table table)
        {
            _NearbyIntroQs.EnterNearbyIntroQsAnswers(table);
        }

        [When(@"I Continue to the '(.*)' page then return to the '(.*)' page")]
        public void WhenIClickContinueToThePageThenReturnToThePage(string URLa, string URLb)
        {
            _bwc.ClickContinueButton();
            _commonSteps.ThenIShouldBeNavigatedToPage(URLa);
            _commonElements.ClickNavItem("Start");
            _commonSteps.ThenIShouldBeNavigatedToPage(URLb);
        }


        [Then(@"I can see the following mandatory error message")]
        public void ICanSeeTheFollowingMandatoryErrorMessage(Table table)
        {
            _NearbyIntroQs.IntroQValidationErrorMessageIsDisplayed(table);
        }

        [Then(@"I can no longer see the following mandatory error message")]
        public void ThenICanNotSeeTheFollowingMandatoryErrorMessage(Table table)
        {
            _NearbyIntroQs.IntroQValidationErrorMessageIsNotDisplayed(table);
        }



    }
}
