using J2BIOverseasOps.Pages.BuildingWork.Nearby.OtherAdvertisedFacilities;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.BuildingWork.Nearby
{
    [Binding]
    public sealed class NearbyOtherFacilitiesStepDefs : BaseStepDefs
    {
        private readonly NearbyOtherFacilitiesPage _nearbyFacilitiesPage;

        public NearbyOtherFacilitiesStepDefs(IWebDriver driver, ILog log, IRunData rundata) : base(driver, log)
        {
            _nearbyFacilitiesPage = new NearbyOtherFacilitiesPage(driver, log, rundata);
        }

        [Then(@"I verify the list of facilities on the Nearby other advertised facilities page displays all of the facilities")]
        public void ThenIVerifyTheListOfFacilitiesOnTheNearbyOtherAdvertisedFacilitiesPageDisplaysAllOfTheFacilities()
        {
            _nearbyFacilitiesPage.VerifyListOfOtherFacilities();
        }

        [When(@"I enter the following answers for the following questions on the Nearby other facilities page:")]
        [Then(@"I can enter the following answers for the following questions on the Nearby other facilities page:")]
        public void WhenIEnterTheFollowingAnswersForTheFollowingQuestionsOnTheNearbyOtherFacilitiesPage(Table table)
        {
            _nearbyFacilitiesPage.EnterOnSiteOtherFacilitiesAnswers(table);
        }

        [Then(@"I am ""(.*)"" the following fields on the On Nearby other facilities page")]
        public void ThenIAmTheFollowingFieldsOnTheOnNearbyOtherFacilitiesPage(string displayedOrNot, Table table)
        {
            _nearbyFacilitiesPage.VerifyFieldsDisplayedOrNot(displayedOrNot, table);
        }

        [Then(@"I verify the list of options on how are other facilities affected on the nearby other facilities page as:")]
        public void ThenIVerifyTheListOfOptionsOnHowAreOtherFacilitiesAffectedOnTheNearbyOtherFacilitiesPageAs(Table table)
        {
            _nearbyFacilitiesPage.VerifyListHowOtherFacilitiesAffected(table);
        }

        [When(@"I click ""(.*)"" button on the Nearby other facilities page")]
        public void WhenIClickButtonOnTheNearbyOtherFacilitiesPage(string btnName)
        {
            _nearbyFacilitiesPage.ClickFacilitiesButton(btnName);
        }

        [Then(@"I verify the list of other facilities on the Nearby other facilities page ""(excludes|includes)"" the ""(.*)""")]
        public void ThenIVerifyTheListOfOtherFacilitiesOnTheNearbyOtherFacilitiesPageThe(string excludes, string otherfacilities)
        {
            _nearbyFacilitiesPage.VerifyFacilityNotPresent(excludes, otherfacilities);
        }

        [Then(@"I verify ""(.*)"" button is ""(.*)"" on the Nearby other facilities page")]
        public void ThenIVerifyButtonIsOnTheNearbyOtherFacilitiesPage(string btnName, string enabledDisabled)
        {
            _nearbyFacilitiesPage.VerifyOtherFacilitiesBtnEnabledDisabled(btnName, enabledDisabled);
        }

        [Then(@"I can see the following mandatory error message on the following Nearby other facilities fields:")]
        public void ThenICanSeeTheFollowingMandatoryErrorMessageOnTheFollowingNearbyOtherFacilitiesFields(Table table)
        {
            _nearbyFacilitiesPage.VerifyOtherFacilitiesMandatoryMessage(table);
        }
    }
}