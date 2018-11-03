using J2BIOverseasOps.Pages.BuildingWork.Nearby.Rooms;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.BuildingWork.Nearby
{
    [Binding]
    public sealed class NearbyRoomsStepDefs : BaseStepDefs

    {
        private readonly NearbyRoomsPage _nearbyRooms;

        public NearbyRoomsStepDefs(IWebDriver driver, ILog log, IRunData rundata) : base(driver, log)
        {
            _nearbyRooms = new NearbyRoomsPage(driver, log, rundata);
        }

        [Then(@"I can see the following mandatory error message on the following Nearby Rooms fields:")]
        public void ThenICanSeeTheFollowingMandatoryErrorMessageOnTheFollowingNearbyRoomsFields(Table table)
        {
            _nearbyRooms.VerifyRoomsValidationErrorMessage(table);
        }

        [When(@"I enter the following answers for the following Nearby Rooms Questions:")]
        [Then(@"I can enter the following answers for the following Nearby Rooms Questions:")]
        public void WhenIEnterTheFollowingAnswersForTheFollowingNearbyRoomsQuestions(Table table)
        {
            _nearbyRooms.EnterAnswerToNearbyRooms(table);
        }

        [Then(@"I am ""(displayed|not displayed)"" with the Nearby Rooms question ""(.*)""")]
        public void ThenIAmWithTheNearbyRoomsQuestion(string displayedNotDisplayed, string question)
        {
            _nearbyRooms.VerifyNearbyQDisplayed(displayedNotDisplayed, question);
        }

        [Then(@"I verify the list of rooms on the nearby affected rooms page displays all of the types of affected rooms")]
        public void ThenIVerifyTheListOfRoomsOnTheNearbyAffectedRoomsPageDisplaysAllOfTheTypesOfAffectedRooms()
        {
            _nearbyRooms.VerifyListOfTypesOfRoomsAffected();
        }
    }
}
