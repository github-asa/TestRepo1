using J2BIOverseasOps.Pages.BuildingWork.Roadworks.Restaurants;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.BuildingWork.Roadworks
{
    [Binding]
    public sealed class NearbyRestaurantStepDefs : BaseStepDefs
    {
        private readonly RoadworksRestaurantsPage _nearbyRestaurants;

        public NearbyRestaurantStepDefs(IWebDriver driver, ILog log, IRunData rundata) : base(driver, log)
        {
            _nearbyRestaurants = new RoadworksRestaurantsPage(driver, log, rundata);
        }

        [Then(@"I verify the list of restaurants on the Roadworks restaurants page displays all of the restaurants")]
        public void ThenIVerifyTheListOfRestaurantsOnTheNearbyRestaurantsPageDisplaysAllOfTheRestaurants()
        {
            _nearbyRestaurants.VerifyListOfRestaurants();
        }

        [Then(@"I am ""(displayed|not displayed)"" the following fields on the Roadworks Restaurants page")]
        public void ThenIAmTheFollowingFieldsOnTheNearbyRestaurantsPage(string displayedNotDisplayed, Table table)
        {
            _nearbyRestaurants.VerifyFieldsDisplayedOrNot(displayedNotDisplayed, table);
        }

        [When(@"I get the lists of Roadworks Restaurants for the current property")]
        public void WhenIGetTheListsOfNearbyRestaurantsForTheCurrentProperty()
        {
            _nearbyRestaurants.GetListOfRestaurants();
        }

        [Then(@"I can enter the following answers to the following questions on the Roadworks restaurants page:")]
        [Then(@"I enter the following answers to the following questions on the Roadworks restaurants page:")]
        [When(@"I enter the following answers to the following questions on the Roadworks restaurants page:")]
        public void WhenIEnterTheFollowingAnswersToTheFollowingQuestionsOnTheNearbyRestaurantsPage(Table table)
        {
            _nearbyRestaurants.EnterRoadworksRestaurantsAnswers(table);
        }

        [When(@"I click ""(.*)"" button on the Roadworks restaurants page")]
        public void WhenIClickButtonOnTheNearbyRestaurantsPage(string btn)
        {
            _nearbyRestaurants.ClickRestaurantsButton(btn);
        }

        [Then(@"I verify the list of options on how are restaurants affected on the Roadworks restaurants page as:")]
        public void ThenIVerifyTheListOfOptionsOnHowAreRestaurantsAffectedOnTheNearbyRestaurantsPageAs(Table table)
        {
            _nearbyRestaurants.VerifyListHowRestaurantsAffected(table);
        }

        [Then(@"I verify the list of restaurants on the Roadworks restaurants page ""(excludes|includes)"" the ""(.*)""")]
        public void ThenIVerifyTheListOfRestaurantsOnTheNearbyRestaurantsPageThe(string excludesIncludes, string restaurantsList)
        {
            _nearbyRestaurants.VerifyRestaurantsNotPresent(excludesIncludes, restaurantsList);
        }

        [When(@"I click ""(.*)"" for the delete confirmation box on the Roadworks restaurants page")]
        public void WhenIClickForTheDeleteConfirmationBoxOnTheNearbyRestaurantsPage(string btnText)
        {
            _nearbyRestaurants.ClickOnConfirmationPopup(btnText);
        }

        [Then(@"I verify ""(.*)"" button is ""(Enabled|Disabled)"" on the Roadworks restaurants page")]
        public void ThenIVerifyButtonIsOnTheNearbyRestaurantsPage(string addRemoveBtn, string enabledDisabled)
        {
            _nearbyRestaurants.VerifyRestaurantsBtnEnabledDisabled(addRemoveBtn, enabledDisabled);
        }

        [Then(@"I can see the following mandatory error messages on the following Roadworks restaurants fields:")]
        public void ThenICanSeeTheFollowingMandatoryErrorMessagesOnTheFollowingNearbyRestaurantsFields(Table table)
        {
            _nearbyRestaurants.VerifyRoadworksRestaurantsMandatoryMessage(table);
        }
    }
}
