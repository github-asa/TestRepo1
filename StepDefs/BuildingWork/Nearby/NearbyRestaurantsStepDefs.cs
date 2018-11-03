using J2BIOverseasOps.Pages.BuildingWork.Nearby.Restaurants;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.BuildingWork.Nearby
{
    [Binding]
    public sealed class NearbyRestaurantStepDefs : BaseStepDefs
    {
        private readonly NearbyRestaurantsPage _nearbyRestaurants;

        public NearbyRestaurantStepDefs(IWebDriver driver, ILog log, IRunData rundata) : base(driver, log)
        {
            _nearbyRestaurants = new NearbyRestaurantsPage(driver, log, rundata);
        }

        [Then(@"I verify the list of restaurants on the nearby restaurants page displays all of the restaurants")]
        public void ThenIVerifyTheListOfRestaurantsOnTheNearbyRestaurantsPageDisplaysAllOfTheRestaurants()
        {
            _nearbyRestaurants.VerifyListOfRestaurants();
        }

        [Then(@"I am ""(displayed|not displayed)"" the following fields on the Nearby Restaurants page")]
        public void ThenIAmTheFollowingFieldsOnTheNearbyRestaurantsPage(string displayedNotDisplayed, Table table)
        {
            _nearbyRestaurants.VerifyFieldsDisplayedOrNot(displayedNotDisplayed, table);
        }

        [When(@"I get the lists of nearby Restaurants for the current property")]
        public void WhenIGetTheListsOfNearbyRestaurantsForTheCurrentProperty()
        {
            _nearbyRestaurants.GetListOfRestaurants();
        }

        [Then(@"I can enter the following answers to the following questions on the nearby restaurants page:")]
        [Then(@"I enter the following answers to the following questions on the nearby restaurants page:")]
        [When(@"I enter the following answers to the following questions on the nearby restaurants page:")]
        public void WhenIEnterTheFollowingAnswersToTheFollowingQuestionsOnTheNearbyRestaurantsPage(Table table)
        {
            _nearbyRestaurants.EnterNearbyRestaurantsAnswers(table);
        }

        [When(@"I click ""(.*)"" button on the nearby restaurants page")]
        public void WhenIClickButtonOnTheNearbyRestaurantsPage(string btn)
        {
            _nearbyRestaurants.ClickRestaurantsButton(btn);
        }

        [Then(@"I verify the list of options on how are restaurants affected on the Nearby restaurants page as:")]
        public void ThenIVerifyTheListOfOptionsOnHowAreRestaurantsAffectedOnTheNearbyRestaurantsPageAs(Table table)
        {
            _nearbyRestaurants.VerifyListHowRestaurantsAffected(table);
        }

        [Then(@"I verify the list of restaurants on the nearby restaurants page ""(excludes|includes)"" the ""(.*)""")]
        public void ThenIVerifyTheListOfRestaurantsOnTheNearbyRestaurantsPageThe(string excludesIncludes, string restaurantsList)
        {
            _nearbyRestaurants.VerifyRestaurantsNotPresent(excludesIncludes, restaurantsList);
        }

        [When(@"I click ""(.*)"" for the delete confirmation box on the nearby restaurants page")]
        public void WhenIClickForTheDeleteConfirmationBoxOnTheNearbyRestaurantsPage(string btnText)
        {
            _nearbyRestaurants.ClickOnConfirmationPopup(btnText);
        }

        [Then(@"I verify ""(.*)"" button is ""(Enabled|Disabled)"" on the nearby restaurants page")]
        public void ThenIVerifyButtonIsOnTheNearbyRestaurantsPage(string addRemoveBtn, string enabledDisabled)
        {
            _nearbyRestaurants.VerifyRestaurantsBtnEnabledDisabled(addRemoveBtn, enabledDisabled);
        }

        [Then(@"I can see the following mandatory error messages on the following nearby restaurants fields:")]
        public void ThenICanSeeTheFollowingMandatoryErrorMessagesOnTheFollowingNearbyRestaurantsFields(Table table)
        {
            _nearbyRestaurants.VerifyNearbyRestaurantsMandatoryMessage(table);
        }
    }
}
