using J2BIOverseasOps.Pages.BuildingWork.Onsite.OtherPublicAreas;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.BuildingWork.Onsite
{
    [Binding]
    public sealed class OnsiteOtherPublicAreasStepDefs : BaseStepDefs
    {
        private readonly OnsiteOtherPublicAreasPage _otherPublicAreas;

        public OnsiteOtherPublicAreasStepDefs(IWebDriver driver, ILog log, IRunData rundata) : base(driver, log)
        {
            _otherPublicAreas = new OnsiteOtherPublicAreasPage(driver, log,rundata);
        }

        [Then(@"I can see the following mandatory error message on the following On Site other public areas fields:")]
        public void ThenICanSeeTheFollowingMandatoryErrorMessageOnTheFollowingOnSitePublicAreasFields(Table table)
        {
            _otherPublicAreas.VerifyOtherPublicAreasMandatoryMessage(table);
        }

        [Then(@"I can enter the following answers for the following questions on the the Onsite other public areas page:")]
        [When(@"I enter the following answers for the following questions on the the Onsite other public areas page:")]
        public void WhenIEnterTheFollowingAnswersForTheFollowingQuestionsOnTheTheOtherPublicAreasPage(Table table)
        {
            _otherPublicAreas.EnterOnSiteOtherPublicAreasAnswers(table);
        }

        [Then(@"I verify the following answers for the following questions on the the Onsite other public areas page:")]
        public void ThenIVerifyTheFollowingAnswersForTheFollowingQuestionsOnTheTheOnsiteOtherPublicAreasPage(Table table)
        {
            _otherPublicAreas.VerifyOnSiteOtherPublicAreasAnswers(table);
        }


        [Then(@"I am ""(displayed|not displayed)"" the following fields on the On Site other public areas page")]
        public void ThenIAmTheFollowingFieldsOnTheOnSiteOtherPublicAreasPage(string displayedNotDisplayed, Table table)
        {
            _otherPublicAreas.VerifyFieldsDisplayedOrNot(displayedNotDisplayed,table);
        }

        [Then(@"I verify ""(.*)"" button is ""(Enabled|Disabled)"" on the onsite other public areas page")]
        public void ThenIVerifyButtonIsOnTheOtherPublicAreasPage(string addRemoveBtn, string enabledDisabled)
        {
            _otherPublicAreas.VerifyOtherPublicAreasBtnEnabledDisabled(addRemoveBtn,enabledDisabled);
        }

        [When(@"I click ""(.*)"" button on the onsite other public areas page")]
        public void WhenIClickButtonOnTheOtherPublicAreasPage(string btn)
        {
            _otherPublicAreas.ClickOtherPublicAreasButton(btn);
        }

        [Then(@"I verify the following fields values on the Onsite other public areas page:")]
        public void ThenIVerifyTheFollowingFieldsValuesOnTheOtherPublicAreasPage(Table table)
        {
            _otherPublicAreas.VerifyOnSiteOtherPublicAreasAnswers(table);
        }

        [Then(@"I verify the list of affected list of other public areas on the other other public areas page as:")]
        public void ThenIVerifyTheListOfAffectedListOfOtherPublicAreasOnTheOtherOtherPublicAreasPageAs(Table table)
        {
            _otherPublicAreas.VerifyListOfOtherPublicAreas(table);
        }


        [Then(@"I verify the list of other public areas on the onsite other public areas page ""(excludes|includes)"" the ""(.*)""")]
        public void ThenIVerifyTheListOfOtherPublicAreasOnTheOtherPublicAreasPageThe(string excludesIncludes, string poolsList)
        {
            _otherPublicAreas.VerifyOtherPublicAreaNotPresent(excludesIncludes, poolsList);
        }


    }
}