
using J2BIOverseasOps.Pages.BuildingWork.Roadworks.RoadworksIntroQs;
using TechTalk.SpecFlow;
using log4net;
using OpenQA.Selenium;

namespace J2BIOverseasOps.StepDefs.BuildingWork.Roadworks
{
    [Binding]
    public sealed class RoadworksIntroQsStepDefs : BaseStepDefs
    {
        private readonly RoadworksIntroQsPage _RoadworksIntroQs;

        public RoadworksIntroQsStepDefs(IWebDriver driver, ILog log, IRunData rundata) : base(driver, log)

        {
            _RoadworksIntroQs = new RoadworksIntroQsPage(driver, log, rundata);
        }

        [Then(@"I verify following fields are ""(displayed|not displayed*)"" on the Roadworks Intro Questions page")]
        [When(@"I verify following fields are ""(displayed|not displayed*)"" on the Roadworks Intro Questions page")]
        public void ThenIVerifyFollowingFieldsAreOnTheRoadworksIntroQuestionsPage(string displayedNotDisplayed, Table table)
        {
            _RoadworksIntroQs.VerifyFieldsDisplayedOrNot(displayedNotDisplayed, table);
        }

        [When(@"I enter the following answers for the following questions on the Roadworks Intro Questions page:")]
        [Then(@"I enter the following answers for the following questions on the Roadworks Intro Questions page:")]
        public void ThenIEnterTheFollowingAnswersForTheFollowingQuestionsOnTheRoadworksIntroQuestionsPage(Table table)
        {
              _RoadworksIntroQs.EnterRoadworksIntroQsAnswers(table);
        }

        [When(@"I enter the following dates for the following questions on the Roadworks Intro Questions page:")]
        public void WhenIEnterTheFollowingDatesForTheFollowingQuestionsOnTheRoadworksIntroQuestionsPage(Table table)
        {
            //_RoadworksIntroQs.EnterDates();
          
        }



        //    [Then(@"I verify following fields are ""(displayed|not displayed)"" on the Roadworks Bars page:")]
        //    public void ThenIVerifyFollowingFieldsAreOnTheRoadworksBarsPage(string displayedNotDisplayed, Table table)
        //    {
        //        _RoadworksIntroQs.VerifyFieldsDisplayedOrNot(displayedNotDisplayed, table);
        //    }

        //    [When(@"I click continue on bars page")]
        //    public void WhenIClickContinueOnBarsPage()
        //    {
        //        _RoadworksIntroQs.ClickContinueButton();
        //    }

        //    [Then(@"I can see the following mandatory error message on the Roadworks Bars page")]
        //    public void ThenICanSeeTheFollowingMandatoryErrorMessageOnTheRoadworksBarsPage(Table table)
        //    {
        //        _RoadworksIntroQs.VerifyBarsMandatoryMessage(table);
        //    }

        //    [Then(@"I can see the following mandatory error message on the following on Roadworks Bars fields:")]
        //    public void ThenICanSeeTheFollowingMandatoryErrorMessageOnTheFollowingOnRoadworksBarsFields(Table table)
        //    {
        //        _RoadworksIntroQs.VerifyBarsMandatoryMessage(table);
        //    }

        //    [Then(@"I verify the list of bars on the roadworks bars page displays all of the bars")]
        //    public void ThenIVerifyTheListOfBarsOnTheRoadworksBarsPageDisplaysAllOfTheBars()
        //    {
        //        _RoadworksIntroQs.VerifyListOfBars();
        //    }

        //    [When(@"I get the lists of Roadworks Bars for the current property")]
        //    public void WhenIGetTheListsOfBarsForTheCurrentProperty()
        //    {
        //        _RoadworksIntroQs.GetListOfBars();
        //    }

        //    [When(@"I click ""(.*)"" button on the roadworks bars page")]
        //    public void WhenIClickButtonOnTheRoadworksBarsPage(string btn)
        //    {
        //        _RoadworksIntroQs.ClickBarsButton(btn);
        //    }

        //    [Then(@"I verify the list of bars on the roadworks bars page ""(.*)"" the ""(.*)""")]
        //    public void ThenIVerifyTheListOfBarsOnTheRoadworksBarsPageThe(string excludesIncludes, string poolsList)
        //    {
        //        _RoadworksIntroQs.VerifyBarsNotPresent(excludesIncludes, poolsList);
        //    }

        //    [Then(@"I verify ""(.*)"" button is ""(Enabled|Disabled)"" on the Roadworks Bars page")]
        //    public void ThenIVerifyButtonIsOnTheBarsPage(string addRemoveBtn, string enabledDisabled)
        //    {
        //        _RoadworksIntroQs.VerifyBarsBtnEnabledDisabled(addRemoveBtn, enabledDisabled);
        //    }
        //}
    }
}
