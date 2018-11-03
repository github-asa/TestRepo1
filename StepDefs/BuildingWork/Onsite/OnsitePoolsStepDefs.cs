using J2BIOverseasOps.Pages.BuildingWork.Onsite.Pools;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.BuildingWork.Onsite
{
    [Binding]
    public sealed class OnsitePoolsStepDefs : BaseStepDefs
    {
        private readonly OnsitePools _onSitePools;

        public OnsitePoolsStepDefs(IWebDriver driver, ILog log, IRunData rundata) : base(driver, log)
        {
            _onSitePools = new OnsitePools(driver, log,rundata);
        }


        [Then(@"I can see the following mandatory error message on the following On Site Pools fields:")]
        public void ThenICanSeeTheFollowingMandatoryErrorMessageOnTheFollowingOnSitePoolsFields(Table table)
        {
            _onSitePools.VerifyPoolsMandatoryMessage(table);
        }

        [Then(@"I can enter the following answers for the following questions on the the Onsite Pools page:")]
        [When(@"I enter the following answers for the following questions on the the Onsite Pools page:")]
        public void WhenIEnterTheFollowingAnswersForTheFollowingQuestionsOnTheTheOnsitePoolsPage(Table table)
        {
            _onSitePools.EnterOnSitePoolsAnswers(table);
        }

        [Then(@"I verify the following answers for the following questions on the the Onsite Pools page:")]
        public void ThenIVerifyTheFollowingAnswersForTheFollowingQuestionsOnTheTheOnsitePoolsPage(Table table)
        {
            _onSitePools.VerifyOnSitePoolsAnswers(table);
        }


        [Then(@"I am ""(displayed|not displayed)"" the following fields on the On Site Pools page")]
        public void ThenIAmTheFollowingFieldsOnTheOnSitePoolsPage(string displayedNotDisplayed, Table table)
        {
            _onSitePools.VerifyFieldsDisplayedOrNot(displayedNotDisplayed,table);
        }

        [Then(@"I verify the following fields values on the Onsite Pools page:")]
        public void ThenIVerifyTheFollowingFieldsValuesOnTheOnsitePoolsPage(Table table)
        {
            _onSitePools.VerifyOnSitePoolsAnswers(table);
        }

        [Then(@"I verify the list of options on how are pools affected on the onsite pools page as:")]
        public void ThenIVerifyTheListOfOptionsOnHowArePoolsAffectedOnTheOnsitePoolsPageAs(Table table)
        {
            _onSitePools.VerifyListHowPoolsAffected(table);
        }

        [When(@"I click ""(.*)"" button on the pools page")]
        public void WhenIClickButtonOnThePoolsPage(string button)
        {
            _onSitePools.ClickPoolsButton(button);
        }

        [Then(@"I verify ""(.*)"" button is ""(.*)"" on the Pools page")]
        public void ThenIVerifyButtonIsOnThePoolsPage(string button, string enabledDisabled)
        {
            _onSitePools.VerifyPoolsBtnEnabledDisabled(button,enabledDisabled);
        }

        [Then(@"I verify the list of pools on the onsite pools page displays all of the pools")]
        public void ThenIVerifyTheListOfPoolsOnTheOnsitePoolsPageDisplaysAllOfThePools()
        {
            _onSitePools.VerifyListOfPools();
        }

        [When(@"I get the lists of Pools for the current property")]
        public void WhenIGetTheListsOfPoolsForTheCurrentProperty()
        {
            _onSitePools.GetListOfPools();

        }

        [Then(@"I verify the list of pools on the onsite pools page ""(excludes|includes)"" the ""(.*)""")]
        public void ThenIVerifyTheListOfPoolsOnTheOnsitePoolsPageThe(string excludesIncludes, string poolsList)
        {
            _onSitePools.VerifyPoolsNotPresent(excludesIncludes, poolsList);
        }


    }
}