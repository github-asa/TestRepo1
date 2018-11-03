using J2BIOverseasOps.Pages.BuildingWork.Onsite.OtherAdvertisedFacilities;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.BuildingWork.Onsite
{
    [Binding]
    public sealed class OnsiteOtherFacilitiesStepDefs : BaseStepDefs
    {
        private readonly OnsiteOtherFacilitiesPage _onSiteOtherFacilities;

        public OnsiteOtherFacilitiesStepDefs(IWebDriver driver, ILog log,IRunData rundata) : base(driver, log)
        {
            _onSiteOtherFacilities = new OnsiteOtherFacilitiesPage(driver, log,rundata);
        }


        [Then(@"I verify the list of affected facilities on the other advertised facilities page as:")]
        public void ThenIVerifyTheListOfAffectedFacilitiesOnTheOtherAdvertisedFacilitiesPageAs(Table table)
        {
            _onSiteOtherFacilities.VerifyListOfFacilities(table);
        }



        [Then(@"I can see the following mandatory error message on the following On Site other facilities fields:")]
        public void ThenICanSeeTheFollowingMandatoryErrorMessageOnTheFollowingOnSiteRestaurantsFields(Table table)
        {
            _onSiteOtherFacilities.VerifyOtherFacilitiesMandatoryMessage(table);
        }

        [Then(@"I am ""(displayed|not displayed)"" the following fields on the On Site other facilities page")]
        public void ThenIAmTheFollowingFieldsOnTheOnSiteRestaurantsPage(string displayedOrNot, Table table)
        {
            _onSiteOtherFacilities.VerifyFieldsDisplayedOrNot(displayedOrNot, table);
        }

        [When(@"I enter the following answers for the following questions on the the Onsite other facilities page:")]
        [Then(@"I can enter the following answers for the following questions on the the Onsite other facilities page:")]
        public void WhenIEnterTheFollowingAnswersForTheFollowingQuestionsOnTheTheOnsiteRestaurantsPage(Table table)
        {
            _onSiteOtherFacilities.EnterOnSiteOtherFacilitiesAnswers(table);
        }

        [Then(@"I verify the following fields values on the Onsite other facilities page:")]
        public void ThenIVerifyTheFollowingFieldsValuesOnTheOnsiteRestaurantsPage(Table table)
        {
            _onSiteOtherFacilities.VerifyOnSiteOtherFacilitiesAnswers(table);
        }

        [Then(@"I verify the list of options on how are other facilities affected on the onsite other facilities page as:")]
        public void ThenIVerifyTheListOfOptionsOnHowAreRestaurantsAffectedOnTheOnsiteRestaurantsPageAs(Table table)
        {
            _onSiteOtherFacilities.VerifyListHowOtherFacilitiesAffected(table);
        }

        [Then(@"I verify ""(.*)"" button is ""(Enabled|Disabled)"" on the other facilities page")]
        public void ThenIVerifyButtonIs(string btnName, string enabledDisabled)
        {
            _onSiteOtherFacilities.VerifyOtherFacilitiesBtnEnabledDisabled(btnName, enabledDisabled);
        }

        [When(@"I click ""(.*)"" button on the other facilities page")]
        public void WhenIClickButtonOnTheRestaurantsPage(string btnName)
        {
            _onSiteOtherFacilities.ClickFacilitiesButton(btnName);
        }
        
        [Then(@"I verify the list of other facilities on the onsite other facilities page ""(excludes|includes)"" the ""(.*)""")]
        public void ThenIVerifyTheListOfRestaurantsOnTheOnsiteRestaurantsPageThe(string excludes, string otherfacilities)
        {
            _onSiteOtherFacilities.VerifyFacilityNotPresent(excludes, otherfacilities); 
        }



    }
}