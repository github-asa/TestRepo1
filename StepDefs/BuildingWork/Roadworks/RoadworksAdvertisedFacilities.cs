using J2BIOverseasOps.Pages.BuildingWork.Roadworks.OtherAdvertisedFacilities;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.BuildingWork.Roadworks
{
    [Binding]
    public sealed class NearbyOtherFacilitiesStepDefs : BaseStepDefs
    {
        private readonly RoadworksOtherFacilitiesPage _nearbyFacilitiesPage;

        public NearbyOtherFacilitiesStepDefs(IWebDriver driver, ILog log, IRunData rundata) : base(driver, log)
        {
            _nearbyFacilitiesPage = new RoadworksOtherFacilitiesPage(driver, log, rundata);
        }

        [Then(@"I verify the list of facilities on the Roadworks other advertised facilities page displays all of the facilities")]
        public void ThenIVerifyTheListOfFacilitiesOnTheNearbyOtherAdvertisedFacilitiesPageDisplaysAllOfTheFacilities()
        {
            _nearbyFacilitiesPage.VerifyListOfOtherFacilities();
        }

        [When(@"I enter the following answers for the following questions on the Roadworks other facilities page:")]
        [Then(@"I can enter the following answers for the following questions on the Roadworks other facilities page:")]
        public void WhenIEnterTheFollowingAnswersForTheFollowingQuestionsOnTheNearbyOtherFacilitiesPage(Table table)
        {
            _nearbyFacilitiesPage.EnterRoadworksOtherFacilitiesAnswers(table);
        }

        [Then(@"I am ""(.*)"" the following fields on the On Roadworks other facilities page")]
        public void ThenIAmTheFollowingFieldsOnTheOnNearbyOtherFacilitiesPage(string displayedOrNot, Table table)
        {
            _nearbyFacilitiesPage.VerifyFieldsDisplayedOrNot(displayedOrNot, table);
        }

        [Then(@"I verify the list of options on how are other facilities affected on the Roadworks other facilities page as:")]
        public void ThenIVerifyTheListOfOptionsOnHowAreOtherFacilitiesAffectedOnTheNearbyOtherFacilitiesPageAs(Table table)
        {
            _nearbyFacilitiesPage.VerifyListHowOtherFacilitiesAffected(table);
        }

        [When(@"I click ""(.*)"" button on the Roadworks other facilities page")]
        public void WhenIClickButtonOnTheNearbyOtherFacilitiesPage(string btnName)
        {
            _nearbyFacilitiesPage.ClickFacilitiesButton(btnName);
        }

        [Then(@"I verify the list of other facilities on the Roadworks other facilities page ""(excludes|includes)"" the ""(.*)""")]
        public void ThenIVerifyTheListOfOtherFacilitiesOnTheNearbyOtherFacilitiesPageThe(string excludes, string otherfacilities)
        {
            _nearbyFacilitiesPage.VerifyFacilityNotPresent(excludes, otherfacilities);
        }

        [Then(@"I verify ""(.*)"" button is ""(.*)"" on the Roadworks other facilities page")]
        public void ThenIVerifyButtonIsOnTheNearbyOtherFacilitiesPage(string btnName, string enabledDisabled)
        {
            _nearbyFacilitiesPage.VerifyOtherFacilitiesBtnEnabledDisabled(btnName, enabledDisabled);
        }

        [Then(@"I can see the following mandatory error message on the following Roadworks other facilities fields:")]
        public void ThenICanSeeTheFollowingMandatoryErrorMessageOnTheFollowingNearbyOtherFacilitiesFields(Table table)
        {
            _nearbyFacilitiesPage.VerifyOtherFacilitiesMandatoryMessage(table);
        }
    }
}