using J2BIOverseasOps.Pages.BuildingWork.Nearby.OtherPublicAreas;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.BuildingWork.Nearby
{
    [Binding]
    public sealed class NearbyOtherPublicAreasStepDefs : BaseStepDefs
    {
        private readonly NearbyOtherPublicAreasPage _otherPublicAreas;

        public NearbyOtherPublicAreasStepDefs(IWebDriver driver, ILog log, IRunData rundata) : base(driver, log)
        {
            _otherPublicAreas = new NearbyOtherPublicAreasPage(driver, log, rundata);
        }

        [Then(@"I verify the list of affected public areas on the Nearby other public areas page as:")]
        public void ThenIVerifyTheListOfAffectedPublicAreasOnTheNearbyOtherPublicAreasPageAs(Table table)
        {
            _otherPublicAreas.VerifyListOfOtherPublicAreas(table);
        }

        [Then(@"I can enter the following answers to the following questions on the Nearby other public areas page:")]
        [When(@"I enter the following answers to the following questions on the Nearby other public areas page:")]
        public void WhenIEnterTheFollowingAnswersToTheFollowingQuestionsOnTheNearbyOtherPublicAreasPage(Table table)
        {
            _otherPublicAreas.EnterOnSiteOtherPublicAreasAnswers(table);
        }

        [Then(@"I am ""(displayed|not displayed)"" the following fields on the Nearby other public areas page")]
        public void ThenIAmTheFollowingFieldsOnTheNearbyOtherPublicAreasPage(string displayedNotDisplayed, Table table)
        {
            _otherPublicAreas.VerifyFieldsDisplayedOrNot(displayedNotDisplayed, table);
        }

        [Then(@"I can see the following mandatory error message on the following Nearby other public areas fields:")]
        public void ThenICanSeeTheFollowingMandatoryErrorMessageOnTheFollowingNearbyOtherPublicAreasFields(Table table)
        {
            _otherPublicAreas.VerifyOtherPublicAreasMandatoryMessage(table);
        }

        [When(@"I click ""(.*)"" button on the Nearby other public areas page")]
        public void WhenIClickButtonOnTheNearbyOtherPublicAreasPage(string btn)
        {
            _otherPublicAreas.ClickOtherPublicAreasButton(btn);
        }

        [Then(@"I verify the list of other public areas on the Nearby other public areas page ""(.*)"" the ""(.*)""")]
        public void ThenIVerifyTheListOfOtherPublicAreasOnTheNearbyOtherPublicAreasPageThe(string excludesIncludes, string poolsList)
        {
            _otherPublicAreas.VerifyOtherPublicAreaNotPresent(excludesIncludes, poolsList);
        }

        [Then(@"I verify ""(.*)"" button is ""(Enabled|Disabled)"" on the Nearby other public areas page")]
        public void ThenIVerifyButtonIsOnTheOtherPublicAreasPage(string addRemoveBtn, string enabledDisabled)
        {
            _otherPublicAreas.VerifyOtherPublicAreasBtnEnabledDisabled(addRemoveBtn, enabledDisabled);
        }

        [Then(@"I verify the following status on the navigation bar for the Nearby page:")]
        public void ThenIVerifyTheFollowingStatusOnTheNavigationBarForTheNearbyPage(Table table)
        {
            _otherPublicAreas.VerifyNavigationIconStatus(table);
        }
    }
}
