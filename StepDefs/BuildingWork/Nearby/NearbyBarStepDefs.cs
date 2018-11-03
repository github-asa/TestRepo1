using J2BIOverseasOps.Pages.BuildingWork.Nearby.Bars;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.BuildingWork.Nearby
{
    [Binding]
    public sealed class NearbyBarStepDefs : BaseStepDefs
    {
        private readonly NearbyBarsPage _nearbyBars;

        public NearbyBarStepDefs(IWebDriver driver, ILog log, IRunData rundata) : base(driver, log)
        {
            _nearbyBars = new NearbyBarsPage(driver, log, rundata);
        }

        [Then(@"I verify the list of bars on the nearby bars page displays all of the bars")]
        public void ThenIVerifyTheListOfBarsOnTheNearbyBarsPageDisplaysAllOfTheBars()
        {
            _nearbyBars.VerifyListOfBars();
        }

        [Then(@"I am ""(displayed|not displayed)"" the following fields on the Nearby Bars page")]
        public void ThenIAmTheFollowingFieldsOnTheNearbyBarsPage(string displayedNotDisplayed, Table table)
        {
            _nearbyBars.VerifyFieldsDisplayedOrNot(displayedNotDisplayed, table);
        }

        [When(@"I get the lists of nearby Bars for the current property")]
        public void WhenIGetTheListsOfNearbyBarsForTheCurrentProperty()
        {
            _nearbyBars.GetListOfBars();
        }

        [Then(@"I can enter the following answers to the following questions on the nearby bars page:")]
        [Then(@"I enter the following answers to the following questions on the nearby bars page:")]
        [When(@"I enter the following answers to the following questions on the nearby bars page:")]
        public void WhenIEnterTheFollowingAnswersToTheFollowingQuestionsOnTheNearbyBarsPage(Table table)
        {
            _nearbyBars.EnterNearbyBarsAnswers(table);
        }

        [When(@"I click ""(.*)"" button on the nearby bars page")]
        public void WhenIClickButtonOnTheNearbyBarsPage(string btn)
        {
            _nearbyBars.ClickBarsButton(btn);
        }

        [Then(@"I verify the list of options on how are bars affected on the Nearby bars page as:")]
        public void ThenIVerifyTheListOfOptionsOnHowAreBarsAffectedOnTheNearbyBarsPageAs(Table table)
        {
            _nearbyBars.VerifyListHowBarsAffected(table);
        }

        [Then(@"I verify the list of bars on the nearby bars page ""(excludes|includes)"" the ""(.*)""")]
        public void ThenIVerifyTheListOfBarsOnTheNearbyBarsPageThe(string excludesIncludes, string barsList)
        {
            _nearbyBars.VerifyBarsNotPresent(excludesIncludes, barsList);
        }

        [When(@"I click ""(.*)"" for the delete confirmation box on the nearby bars page")]
        public void WhenIClickForTheDeleteConfirmationBoxOnTheNearbyBarsPage(string btnText)
        {
            _nearbyBars.ClickOnConfirmationPopup(btnText);
        }

        [Then(@"I verify ""(.*)"" button is ""(Enabled|Disabled)"" on the nearby bars page")]
        public void ThenIVerifyButtonIsOnTheNearbyBarsPage(string addRemoveBtn, string enabledDisabled)
        {
            _nearbyBars.VerifyBarsBtnEnabledDisabled(addRemoveBtn, enabledDisabled);
        }

        [Then(@"I can see the following mandatory error messages on the following nearby bars fields:")]
        public void ThenICanSeeTheFollowingMandatoryErrorMessagesOnTheFollowingNearbyBarsFields(Table table)
        {
            _nearbyBars.VerifyNearbyBarsMandatoryMessage(table);
        }
    }
}
