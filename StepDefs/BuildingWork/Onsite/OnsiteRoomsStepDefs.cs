using J2BIOverseasOps.Pages.BuildingWork.Onsite.Rooms;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.BuildingWork.Onsite
{
    [Binding]
    public sealed class OnsiteRoomsStepDefs : BaseStepDefs
    {
        private readonly OnsiteRoomsPage _onsiteRooms;

        public OnsiteRoomsStepDefs(IWebDriver driver, ILog log,IRunData rundata) : base(driver, log)
        {
            _onsiteRooms = new OnsiteRoomsPage(driver, log,rundata);
        }


        [Then(@"I can see the following mandatory error message on the following On Site Rooms fields:")]
        public void ThenICanSeeTheFollowingMandatoryErrorMessageOnTheFollowingOnSiteRoomsFields(Table table)
        {
            _onsiteRooms.VerifyRoomsValidationErrorMessage(table);
        }

        [When(@"I enter the following answers for the following Onsite Rooms Questions:")]
        [Then(@"I can enter the following answers for the following Onsite Rooms Questions:")]
        public void WhenIEnterTheFollowingAnswersForTheFollowingOnsiteRoomsQuestions(Table table)
        {
            _onsiteRooms.EnterAnswerToOnSiteRooms(table);
        }

        [Then(@"I verify the following answers for the following Onsite Rooms Questions:")]
        public void WhenIVerifyTheFollowingAnswersForTheFollowingOnsiteRoomsQuestions(Table table)
        {
            _onsiteRooms.VerifyAnswerToOnSiteRooms(table);
        }



        [Then(@"I am ""(displayed|not displayed)"" with the Onsite Rooms question ""(.*)""")]
        public void ThenIAmWithTheOnsiteRoomsQuestion(string displayedNotDisplayed, string question)
        {
            _onsiteRooms.VerifyOnSiteQDisplayed(displayedNotDisplayed, question);
        }

        [Then(@"I verify the list of restaurants on the onsite affected rooms page displays all of the types of affected rooms")]
        public void ThenIVerifyTheListOfRestaurantsOnTheOnsiteAffectedRoomsPageDisplaysAllOfTheTypesOfAffectedRooms()
        {
            _onsiteRooms.VerifyListOfTypesOfRoomsAffected();
        }



    }
}