using J2BIOverseasOps.Pages.BuildingWork.Onsite.Bars;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.BuildingWork.Onsite
{
    [Binding]
    public sealed class OnsiteBarsStepDefs : BaseStepDefs
    {
        private readonly OnsiteBarsPage _onSiteBars;

        public OnsiteBarsStepDefs(IWebDriver driver, ILog log, IRunData rundata) : base(driver, log)
        {
            _onSiteBars = new OnsiteBarsPage(driver, log,rundata);
        }

        [Then(@"I can see the following mandatory error message on the following On Site Bars fields:")]
        public void ThenICanSeeTheFollowingMandatoryErrorMessageOnTheFollowingOnSiteBarsFields(Table table)
        {
            _onSiteBars.VerifyBarsMandatoryMessage(table);
        }

        [Then(@"I can enter the following answers for the following questions on the the Onsite Bars page:")]
        [When(@"I enter the following answers for the following questions on the the Onsite Bars page:")]
        public void WhenIEnterTheFollowingAnswersForTheFollowingQuestionsOnTheTheOnsiteBarsPage(Table table)
        {
            _onSiteBars.EnterOnSiteBarsAnswers(table);
        }

        [Then(@"I verify the following answers for the following questions on the the Onsite Bars page:")]
        public void ThenIVerifyTheFollowingAnswersForTheFollowingQuestionsOnTheTheOnsiteBarsPage(Table table)
        {
            _onSiteBars.VerifyOnSiteBarsAnswers(table);
        }


        [Then(@"I am ""(displayed|not displayed)"" the following fields on the On Site Bars page")]
        public void ThenIAmTheFollowingFieldsOnTheOnSiteBarsPage(string displayedNotDisplayed, Table table)
        {
            _onSiteBars.VerifyFieldsDisplayedOrNot(displayedNotDisplayed,table);
        }

        [Then(@"I verify ""(.*)"" button is ""(Enabled|Disabled)"" on the Bars page")]
        public void ThenIVerifyButtonIsOnTheBarsPage(string addRemoveBtn, string enabledDisabled)
        {
            _onSiteBars.VerifyBarsBtnEnabledDisabled(addRemoveBtn,enabledDisabled);
        }

        [When(@"I click ""(.*)"" button on the bars page")]
        public void WhenIClickButtonOnTheBarsPage(string btn)
        {
            _onSiteBars.ClickBarsButton(btn);
        }

        [Then(@"I verify the following fields values on the Onsite Bars page:")]
        public void ThenIVerifyTheFollowingFieldsValuesOnTheOnsiteBarsPage(Table table)
        {
            _onSiteBars.VerifyOnSiteBarsAnswers(table);
        }
        [Then(@"I verify the list of options on how are bars affected on the onsite bars page as:")]
        public void ThenIVerifyTheListOfOptionsOnHowAreBarsAffectedOnTheOnsiteBarsPageAs(Table table)
        {
            _onSiteBars.VerifyListHowBarsAffected(table);
        }

        [Then(@"I verify the list of bars on the onsite bars page displays all of the bars")]
        public void ThenIVerifyTheListOfBarsOnTheOnsiteBarsPageDisplaysAllOfTheBars()
        {
            _onSiteBars.VerifyListOfBars();
        }

        [When(@"I get the lists of Bars for the current property")]
        public void WhenIGetTheListsOfBarsForTheCurrentProperty()
        {
            _onSiteBars.GetListOfBars();
        }

        [Then(@"I verify the list of bars on the onsite bars page ""(excludes|includes)"" the ""(.*)""")]
        public void ThenIVerifyTheListOfBarsOnTheOnsiteBarsPageThe(string excludesIncludes, string poolsList)
        {
            _onSiteBars.VerifyBarsNotPresent(excludesIncludes, poolsList);
        }


    }
}