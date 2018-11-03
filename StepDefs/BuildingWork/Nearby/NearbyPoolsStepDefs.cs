
using J2BIOverseasOps.Pages.BuildingWork.Nearby.Pools;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.BuildingWork.Nearby
{
    [Binding]
    public sealed class NearbyPoolsStepDefs : BaseStepDefs
    {
        private readonly NearbyPoolsPage _nearbyPools;

        public NearbyPoolsStepDefs(IWebDriver driver, ILog log, IRunData rundata) : base(driver, log)
        {
            _nearbyPools = new NearbyPoolsPage(driver, log, rundata);
        }

        [Then(@"I can enter the following answers to the following questions on the nearby pools page:")]
        [Then(@"I enter the following answers to the following questions on the nearby pools page:")]
        [When(@"I enter the following answers to the following questions on the nearby pools page:")]
        public void WhenIEnterTheFollowingAnswersToTheFollowingQuestionsOnTheNearbyPoolsPage(Table table)
        {
            _nearbyPools.EnterNearbyPoolsAnswers(table);
        }

        [Then(@"I am ""(displayed|not displayed)"" the following fields on the Nearby pools page")]
        public void ThenIAmTheFollowingFieldsOnTheNearbyPoolsPage(string displayedNotDisplayed, Table table)
        {
            _nearbyPools.VerifyFieldsDisplayedOrNot(displayedNotDisplayed, table);
        }

        [Then(@"I verify the list of options on how are pools affected on the Nearby pools page as:")]
        public void ThenIVerifyTheListOfOptionsOnHowAreBarsAffectedOnTheNearbyPoolsPageAs(Table table)
        {
            _nearbyPools.VerifyListHowPoolsAffected(table);
        }

        [Then(@"I verify the list of pools on the nearby pools page displays all of the pools")]
        public void ThenIVerifyTheListOfBarsOnTheNearbyBarsPageDisplaysAllOfThePools()
        {
            _nearbyPools.VerifyListOfPools();
        }

        [When(@"I get the lists of nearby pools for the current property")]
        public void WhenIGetTheListsOfNearbyBarsForTheCurrentProperty()
        {
            _nearbyPools.GetListOfPools();
        }

        [When(@"I click ""(.*)"" button on the nearby pools page")]
        public void WhenIClickButtonOnTheNearbyPoolsPage(string btn)
        {
            _nearbyPools.ClickPoolsButton(btn);
        }

        [Then(@"I verify the list of pools on the nearby pools page ""(excludes|includes)"" the ""(.*)""")]
        public void ThenIVerifyTheListOfBarsOnTheNearbyPoolsPageThe(string excludesIncludes, string barsList)
        {
            _nearbyPools.VerifyPoolsNotPresent(excludesIncludes, barsList);
        }

        [When(@"I click ""(.*)"" for the delete confirmation box on the nearby pools page")]
        public void WhenIClickForTheDeleteConfirmationBoxOnTheNearbyPoolsPage(string btnText)
        {
            _nearbyPools.ClickOnConfirmationPopup(btnText);
        }

        [Then(@"I verify ""(.*)"" button is ""(Enabled|Disabled)"" on the nearby pools page")]
        public void ThenIVerifyButtonIsOnTheNearbyPoolsPage(string addRemoveBtn, string enabledDisabled)
        {
            _nearbyPools.VerifyPoolsBtnEnabledDisabled(addRemoveBtn, enabledDisabled);
        }

        [Then(@"I can see the following mandatory error messages on the following nearby pools fields:")]
        public void ThenICanSeeTheFollowingMandatoryErrorMessagesOnTheFollowingNearbyPoolsFields(Table table)
        {
            _nearbyPools.VerifyNearbyPoolsMandatoryMessage(table);
        }
    }
}
