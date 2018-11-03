using J2BIOverseasOps.Pages.BuildingWork.Roadworks.Rooms;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.BuildingWork.Roadworks
{
    [Binding]
    public sealed class RoadworksRoomsStepDefs : BaseStepDefs

    {
        private readonly RoadworksRoomsPage _roadWorksRooms;

        public RoadworksRoomsStepDefs(IWebDriver driver, ILog log, IRunData rundata) : base(driver, log)
        {
            _roadWorksRooms = new RoadworksRoomsPage(driver, log, rundata);
        }

        [Then(@"I can see the following mandatory error message on the following Roadworks Rooms fields:")]
        public void ThenICanSeeTheFollowingMandatoryErrorMessageOnTheFollowingRoadworksRoomsFields(Table table)
        {
            _roadWorksRooms.VerifyRoomsValidationErrorMessage(table);
        }

        [When(@"I enter the following answers for the following Roadworks Rooms Questions:")]
        [Then(@"I can enter the following answers for the following Roadworks Rooms Questions:")]
        public void WhenIEnterTheFollowingAnswersForTheFollowingRoadworksRoomsQuestions(Table table)
        {
            _roadWorksRooms.EnterAnswerToRoadworksRooms(table);
        }

        [Then(@"I am ""(displayed|not displayed)"" with the Roadworks Rooms question ""(.*)""")]
        public void ThenIAmWithTheRoadworksRoomsQuestion(string displayedNotDisplayed, string question)
        {
            _roadWorksRooms.VerifyRoadworksQDisplayed(displayedNotDisplayed, question);
        }

        [Then(@"I verify the list of rooms on the Roadworks affected rooms page displays all of the types of affected rooms")]
        public void ThenIVerifyTheListOfRoomsOnTheRoadworksAffectedRoomsPageDisplaysAllOfTheTypesOfAffectedRooms()
        {
            _roadWorksRooms.VerifyListOfTypesOfRoomsAffected();
        }
    }
}
