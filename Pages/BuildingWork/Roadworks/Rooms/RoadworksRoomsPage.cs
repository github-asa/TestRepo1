using System.Collections.Generic;
using System.Linq;
using J2BIOverseasOps.Extensions;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Pages.BuildingWork.Roadworks.Rooms
{
    internal class RoadworksRoomsPage : BuildingWorkCommon
    {
        private readonly ApiCalls _apiCall;

        public RoadworksRoomsPage(IWebDriver driver, ILog log, IRunData runData) : base(driver, log,runData)
        {
            _apiCall = new ApiCalls(runData);

        }

        private readonly By _allRoomsAffected = By.XPath("//p-selectbutton[@id='allRoomsAffected']");
        private readonly By _allContractedRoomsAffectedValidation = By.XPath("//p-message[@id='allRoomsAffected-validation']//span[2]");

        private readonly By _typesOfRoomsAffected = By.XPath("//p-multiselect[@id='typesOfRoomsAffected']");
        private readonly By _typesOfRoomlRoomsAffectedValidation = By.XPath("//p-message[@id='typesOfRoomsAffected-validation']");

        private readonly By _detailsTypeOfRoomsAffected = By.XPath("//textarea[@id='detailsAboutFloorsBlocksAffected']");
        private readonly By _detailsTypeOfRoomsAffectedValidation = By.XPath("//p-message[@id='detailsAboutFloorsBlocksAffected-validation']");

        public void VerifyRoomsValidationErrorMessage(Table table)
        {
            foreach (var row in table.Rows)
            {
                var field = row["field"];
                var expectedError = row["error"];
                switch (field.ToLower())
                {
                    case "all contracted rooms affected":
                        VerifyBwFieldValidationErrorMessage(_allContractedRoomsAffectedValidation, expectedError);
                        break;
                    case "types of room affected":
                        VerifyBwFieldValidationErrorMessage(_typesOfRoomlRoomsAffectedValidation, expectedError);
                        break;
                    case "details on types of room affected":
                        VerifyBwFieldValidationErrorMessage(_detailsTypeOfRoomsAffectedValidation, expectedError);
                        break;
                    default:
                        Assert.Fail($"{field} is not a valid field");
                        return;
                }
            }
        }

        public void EnterAnswerToRoadworksRooms(Table table)
        {
            foreach (var row in table.Rows)
            {
                var question = row["question"];
                var answer = row["answer"];

                switch (question.ToLower())
                {
                    case "all contracted rooms affected":
                        Driver.ClickPSelectOption(_allRoomsAffected, answer);
                        break;
                    case "select types of rooms affected":
                        SelectTypesOfRoomsAffected(answer);
                        break;
                    case "details on types of room affected":
                        Driver.EnterText(_detailsTypeOfRoomsAffected, answer);
                        break;
                    default:
                        Assert.Fail($"{question} is not a valid Rooms question");
                        return;
                }
            }
        }

        public void VerifyRoadworksQDisplayed(string displayedNotDisplayed, string question)
        {
            By questionToVerify = null;
            switch (question.ToLower())
            {
                case "all contracted rooms affected":
                    questionToVerify = _allRoomsAffected;
                    break;
                case "select types of rooms affected":
                    questionToVerify = _typesOfRoomsAffected;
                    break;
                case "details on types of room affected":
                    questionToVerify = _detailsTypeOfRoomsAffected;
                    break;
                default:
                    Assert.Fail($"{question} is not a valid Rooms question");
                    return;
            }
        }

        public void VerifyListOfTypesOfRoomsAffected()
        {
            GetListOfRoomsAffected();
            var expectedListOfAffectedRooms = ListOfRoomsAffected;
            var actualListOfAffectedRooms = Driver.GetAllMultiselectOptions(_typesOfRoomsAffected);
            Assert.AreEqual(actualListOfAffectedRooms, expectedListOfAffectedRooms, $"Expected list of affected rooms {expectedListOfAffectedRooms} was not the same as actual {actualListOfAffectedRooms}");
        }

        private void SelectTypesOfRoomsAffected(string affectedRooms)
        {
            GetListOfRoomsAffected();
            var allListOfAffectedRooms = ListOfRoomsAffected;
            var affectedRoomsListIndex = affectedRooms.ConvertStringIntoList();
            var affectedRoomsToSelect = new List<string>();
            foreach (var roomNumbToSelect in affectedRoomsListIndex)
            {
                var i = roomNumbToSelect.GetIntOnly();
                affectedRoomsToSelect.Add(allListOfAffectedRooms[i - 1]);
            }

            Driver.SelectMultiselectOption(_typesOfRoomsAffected, affectedRoomsToSelect, true);
        }

        public void GetListOfRoomsAffected()
        {
            ListOfRoomsAffected = _apiCall   // get list of all rooms for the given property
                .GetListOfTypesOfRooms(Destination, Property)
                .Select(r => r.Name)
                .ToList();
        }
    }
}
