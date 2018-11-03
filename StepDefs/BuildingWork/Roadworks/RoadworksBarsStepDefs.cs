using J2BIOverseasOps.Pages.BuildingWork.Roadworks;
using J2BIOverseasOps.Pages.BuildingWork.Roadworks.Bars;
using TechTalk.SpecFlow;
using log4net;
using OpenQA.Selenium;

namespace J2BIOverseasOps.StepDefs.BuildingWork.Roadworks
{
    [Binding]
    public sealed class RoadworksBarsStepDefs : BaseStepDefs
    {
        private readonly RoadworksBarsPage _RoadworksBars;

        public RoadworksBarsStepDefs(IWebDriver driver, ILog log, IRunData rundata) : base(driver, log)

        {
            _RoadworksBars = new RoadworksBarsPage(driver, log, rundata);
        }
        [Then(@"I enter the following answers for the following questions on the Roadworks Bars page:")]

        [When(@"I enter the following answers for the following questions on the Roadworks Bars page:")]
        public void WhenIEnterTheFollowingAnswersForTheFollowingQuestionsOnTheRoadworksBarsPage(Table table)
        {
            _RoadworksBars.EnterRoadworksBarsAnswers(table);
        }

        [Then(@"I verify following fields are ""(displayed|not displayed)"" on the Roadworks Bars page:")]
        public void ThenIVerifyFollowingFieldsAreOnTheRoadworksBarsPage(string displayedNotDisplayed, Table table)
        {
            _RoadworksBars.VerifyFieldsDisplayedOrNot(displayedNotDisplayed, table);
        }

        [When(@"I click continue on bars page")]
        public void WhenIClickContinueOnBarsPage()
        {
            _RoadworksBars.ClickContinueButton();
        }

        [Then(@"I can see the following mandatory error message on the Roadworks Bars page")]
        public void ThenICanSeeTheFollowingMandatoryErrorMessageOnTheRoadworksBarsPage(Table table)
        {
            _RoadworksBars.VerifyBarsMandatoryMessage(table);
        }

        [Then(@"I can see the following mandatory error message on the following on Roadworks Bars fields:")]
        public void ThenICanSeeTheFollowingMandatoryErrorMessageOnTheFollowingOnRoadworksBarsFields(Table table)
        {
            _RoadworksBars.VerifyBarsMandatoryMessage(table);
        }

        [Then(@"I verify the list of bars on the roadworks bars page displays all of the bars")]
        public void ThenIVerifyTheListOfBarsOnTheRoadworksBarsPageDisplaysAllOfTheBars()
        {
            _RoadworksBars.VerifyListOfBars();
        }

        [When(@"I get the lists of Roadworks Bars for the current property")]
        public void WhenIGetTheListsOfBarsForTheCurrentProperty()
        {
            _RoadworksBars.GetListOfBars();
        }

        [When(@"I click ""(.*)"" button on the roadworks bars page")]
        public void WhenIClickButtonOnTheRoadworksBarsPage(string btn)
        {
            _RoadworksBars.ClickBarsButton(btn);
        }

        [Then(@"I verify the list of bars on the roadworks bars page ""(.*)"" the ""(.*)""")]
        public void ThenIVerifyTheListOfBarsOnTheRoadworksBarsPageThe(string excludesIncludes, string poolsList)
        {
            _RoadworksBars.VerifyBarsNotPresent(excludesIncludes, poolsList);
        }

        [Then(@"I verify ""(.*)"" button is ""(Enabled|Disabled)"" on the Roadworks Bars page")]
        public void ThenIVerifyButtonIsOnTheBarsPage(string addRemoveBtn, string enabledDisabled)
        {
            _RoadworksBars.VerifyBarsBtnEnabledDisabled(addRemoveBtn, enabledDisabled);
        }
    }
}
