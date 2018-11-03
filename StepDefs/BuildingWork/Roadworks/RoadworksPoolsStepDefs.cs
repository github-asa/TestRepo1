
using J2BIOverseasOps.Pages.BuildingWork.Roadworks.Pools;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.BuildingWork.Roadworks
{
    [Binding]
    public sealed class NearbyPoolsStepDefs : BaseStepDefs
    {
        private readonly RoadworksPoolsPage _roadWorksPools;

        public NearbyPoolsStepDefs(IWebDriver driver, ILog log, IRunData rundata) : base(driver, log)
        {
            _roadWorksPools = new RoadworksPoolsPage(driver, log, rundata);
        }

        [Then(@"I can enter the following answers to the following questions on the Roadworks pools page:")]
        [Then(@"I enter the following answers to the following questions on the Roadworks pools page:")]
        [When(@"I enter the following answers to the following questions on the Roadworks pools page:")]
        public void WhenIEnterTheFollowingAnswersToTheFollowingQuestionsOnTheNearbyPoolsPage(Table table)
        {
            _roadWorksPools.EnterRoadworksPoolsAnswers(table);
        }

        [Then(@"I am ""(displayed|not displayed)"" the following fields on the Roadworks pools page")]
        public void ThenIAmTheFollowingFieldsOnTheNearbyPoolsPage(string displayedNotDisplayed, Table table)
        {
            _roadWorksPools.VerifyFieldsDisplayedOrNot(displayedNotDisplayed, table);
        }

        [Then(@"I verify the list of options on how are pools affected on the Roadworks pools page as:")]
        public void ThenIVerifyTheListOfOptionsOnHowAreBarsAffectedOnTheNearbyPoolsPageAs(Table table)
        {
            _roadWorksPools.VerifyListHowPoolsAffected(table);
        }

        [Then(@"I verify the list of pools on the Roadworks pools page displays all of the pools")]
        public void ThenIVerifyTheListOfBarsOnTheNearbyBarsPageDisplaysAllOfThePools()
        {
            _roadWorksPools.VerifyListOfPools();
        }

        [When(@"I get the lists of Roadworks pools for the current property")]
        public void WhenIGetTheListsOfNearbyBarsForTheCurrentProperty()
        {
            _roadWorksPools.GetListOfPools();
        }

        [When(@"I click ""(.*)"" button on the Roadworks pools page")]
        public void WhenIClickButtonOnTheNearbyPoolsPage(string btn)
        {
            _roadWorksPools.ClickPoolsButton(btn);
        }

        [Then(@"I verify the list of pools on the Roadworks pools page ""(excludes|includes)"" the ""(.*)""")]
        public void ThenIVerifyTheListOfBarsOnTheNearbyPoolsPageThe(string excludesIncludes, string barsList)
        {
            _roadWorksPools.VerifyPoolsNotPresent(excludesIncludes, barsList);
        }

        [When(@"I click ""(.*)"" for the delete confirmation box on the Roadworks pools page")]
        public void WhenIClickForTheDeleteConfirmationBoxOnTheNearbyPoolsPage(string btnText)
        {
            _roadWorksPools.ClickOnConfirmationPopup(btnText);
        }

        [Then(@"I verify ""(.*)"" button is ""(Enabled|Disabled)"" on the Roadworks pools page")]
        public void ThenIVerifyButtonIsOnTheNearbyPoolsPage(string addRemoveBtn, string enabledDisabled)
        {
            _roadWorksPools.VerifyPoolsBtnEnabledDisabled(addRemoveBtn, enabledDisabled);
        }

        [Then(@"I can see the following mandatory error messages on the following Roadworks pools fields:")]
        public void ThenICanSeeTheFollowingMandatoryErrorMessagesOnTheFollowingNearbyPoolsFields(Table table)
        {
            _roadWorksPools.VerifyRoadworksPoolsMandatoryMessage(table);
        }
    }
}
