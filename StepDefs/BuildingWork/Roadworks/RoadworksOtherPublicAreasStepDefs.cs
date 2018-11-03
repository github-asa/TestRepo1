using J2BIOverseasOps.Pages.BuildingWork.Roadworks.OtherPublicAreas;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.BuildingWork.Roadworks
{
    [Binding]
    public sealed class RoadworksOtherPublicAreasStepDefs : BaseStepDefs
    {
        private readonly RoadworksOtherPublicAreasPage _otherPublicAreas;

        public RoadworksOtherPublicAreasStepDefs(IWebDriver driver, ILog log, IRunData rundata) : base(driver, log)
        {
            _otherPublicAreas = new RoadworksOtherPublicAreasPage(driver, log, rundata);
        }

        [Then(@"I verify the list of affected public areas on the Roadworks other public areas page as:")]
        public void ThenIVerifyTheListOfAffectedPublicAreasOnTheRoadworksOtherPublicAreasPageAs(Table table)
        {
            _otherPublicAreas.VerifyListOfOtherPublicAreas(table);
        }

        [Then(@"I can enter the following answers to the following questions on the Roadworks other public areas page:")]
        [When(@"I enter the following answers to the following questions on the Roadworks other public areas page:")]
        public void WhenIEnterTheFollowingAnswersToTheFollowingQuestionsOnTheRoadworksOtherPublicAreasPage(Table table)
        {
            _otherPublicAreas.EnterRoadworksOtherPublicAreasAnswers(table);
        }

        [Then(@"I am ""(displayed|not displayed)"" the following fields on the Roadworks other public areas page")]
        public void ThenIAmTheFollowingFieldsOnTheRoadworksOtherPublicAreasPage(string displayedNotDisplayed, Table table)
        {
            _otherPublicAreas.VerifyFieldsDisplayedOrNot(displayedNotDisplayed, table);
        }

        [Then(@"I can see the following mandatory error message on the following Roadworks other public areas fields:")]
        public void ThenICanSeeTheFollowingMandatoryErrorMessageOnTheFollowingRoadworksOtherPublicAreasFields(Table table)
        {
            _otherPublicAreas.VerifyOtherPublicAreasMandatoryMessage(table);
        }

        [When(@"I click ""(.*)"" button on the Roadworks other public areas page")]
        public void WhenIClickButtonOnTheRoadworksOtherPublicAreasPage(string btn)
        {
            _otherPublicAreas.ClickOtherPublicAreasButton(btn);
        }

        [Then(@"I verify the list of other public areas on the Roadworks other public areas page ""(.*)"" the ""(.*)""")]
        public void ThenIVerifyTheListOfOtherPublicAreasOnTheRoadworksOtherPublicAreasPageThe(string excludesIncludes, string poolsList)
        {
            _otherPublicAreas.VerifyOtherPublicAreaNotPresent(excludesIncludes, poolsList);
        }

        [Then(@"I verify ""(.*)"" button is ""(Enabled|Disabled)"" on the Roadworks other public areas page")]
        public void ThenIVerifyButtonIsOnTheOtherPublicAreasPage(string addRemoveBtn, string enabledDisabled)
        {
            _otherPublicAreas.VerifyOtherPublicAreasBtnEnabledDisabled(addRemoveBtn, enabledDisabled);
        }

        [Then(@"I verify the following status on the navigation bar for the Roadworks page:")]
        public void ThenIVerifyTheFollowingStatusOnTheNavigationBarForTheRoadworksPage(Table table)
        {
            _otherPublicAreas.VerifyNavigationIconStatus(table);
        }
    }
}
