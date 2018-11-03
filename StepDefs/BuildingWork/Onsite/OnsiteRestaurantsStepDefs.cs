using J2BIOverseasOps.Pages.BuildingWork.Onsite;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.BuildingWork.Onsite
{
    [Binding]
    public sealed class OnsiteRestaurantsStepDefs : BaseStepDefs
    {
        private readonly OnsiteRestaurantsPage _onSiteRestuarants;

        public OnsiteRestaurantsStepDefs(IWebDriver driver, ILog log,IRunData rundata) : base(driver, log)
        {
            _onSiteRestuarants = new OnsiteRestaurantsPage(driver, log,rundata);
        }

        [Then(@"I verify the list of restaurants on the onsite restaurants page displays all of the restaurants")]
        public void ThenIVerifyTheListOfRestaurantsOnTheOnsiteRestaurantsPageAs()
        {
            _onSiteRestuarants.VerifyListOfRestaurants();
        }

        [Then(@"I can see the following mandatory error message on the following On Site Restaurants fields:")]
        public void ThenICanSeeTheFollowingMandatoryErrorMessageOnTheFollowingOnSiteRestaurantsFields(Table table)
        {
            _onSiteRestuarants.VerifyRestaurantsMandatoryMessage(table);
        }

        [Then(@"I am ""(displayed|not displayed)"" the following fields on the On Site Restaurants page")]
        public void ThenIAmTheFollowingFieldsOnTheOnSiteRestaurantsPage(string displayedOrNot, Table table)
        {
            _onSiteRestuarants.VerifyFieldsDisplayedOrNot(displayedOrNot, table);
        }

        [When(@"I enter the following answers for the following questions on the the Onsite Restaurants page:")]
        [Then(@"I can enter the following answers for the following questions on the the Onsite Restaurants page:")]
        public void WhenIEnterTheFollowingAnswersForTheFollowingQuestionsOnTheTheOnsiteRestaurantsPage(Table table)
        {
            _onSiteRestuarants.EnterOnSiteRestaurantsAnswers(table);
        }

        [Then(@"I verify the following answers for the following questions on the the Onsite Restaurants page:")]
        public void ThenIVerifyTheFollowingAnswersForTheFollowingQuestionsOnTheTheOnsiteRestaurantsPage(Table table)
        {
            _onSiteRestuarants.VerifyOnSiteRestaurantsAnswers(table);
        }

        [Then(@"I verify the following fields values on the Onsite Restaurants page:")]
        public void ThenIVerifyTheFollowingFieldsValuesOnTheOnsiteRestaurantsPage(Table table)
        {
            _onSiteRestuarants.VerifyOnSiteRestaurantsAnswers(table);
        }

        [Then(@"I verify the list of options on how are restaurants affected on the onsite restaurants page as:")]
        public void ThenIVerifyTheListOfOptionsOnHowAreRestaurantsAffectedOnTheOnsiteRestaurantsPageAs(Table table)
        {
            _onSiteRestuarants.VerifyListHowRestaurantsAffected(table);
        }

        [Then(@"I verify ""(.*)"" button is ""(Enabled|Disabled)"" on the Restaurants page")]
        public void ThenIVerifyButtonIs(string btnName, string enabledDisabled)
        {
            _onSiteRestuarants.VerifyRestaurantsBtnDisabled(btnName, enabledDisabled);
        }

        [When(@"I click ""(.*)"" button on the restaurants page")]
        public void WhenIClickButtonOnTheRestaurantsPage(string btnName)
        {
            _onSiteRestuarants.ClickRestaurantsButton(btnName);

        }

        
        [Then(@"I verify the list of restaurants on the onsite restaurants page ""(excludes|includes)"" the ""(.*)""")]
        public void ThenIVerifyTheListOfRestaurantsOnTheOnsiteRestaurantsPageThe(string excludes, string restaurants)
        {
            _onSiteRestuarants.VerifyRestaurantsNotPresent(excludes, restaurants);
        }

        [When(@"I get the lists of Restaurants for the current property")]
        public void WhenIGetTheListsOfRestaurantsForTheCurrentProperty()
        {
            _onSiteRestuarants.GetListOfRestaurants();
        }

        [When(@"I click ""(.*)"" for the delete confirmation box")]
        public void WhenIClickForTheDeleteConfirmationBox(string btnText)
        {
            _onSiteRestuarants.ClickOnConfirmationPopup(btnText);
        }

    }
}