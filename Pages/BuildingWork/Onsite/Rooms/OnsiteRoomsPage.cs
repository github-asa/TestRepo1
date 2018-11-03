using System.Collections.Generic;
using System.Linq;
using J2BIOverseasOps.Extensions;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Pages.BuildingWork.Onsite.Rooms
{
    internal class OnsiteRoomsPage : BuildingWorkCommon
    {
        private readonly ApiCalls _apiCall;

        public OnsiteRoomsPage(IWebDriver driver, ILog log,IRunData runData) : base(driver, log,runData)
        {
            _apiCall = new ApiCalls(runData);

        }

        private readonly By _isTheWorkStructuralValidation = By.XPath("//p-message[@id='typeOfWork-validation']//span[2]");
        private readonly By _isTheWorkCosmeticStructPSelect=By.XPath("//p-selectbutton[@id='typeOfWork']");

        private readonly By _doesWorkInvolveBalconiesValidation = By.XPath("//p-message[@id='doesWorkInvolveBalconies-validation']//span[2]");
        private readonly By _doesWorkInvolveBalconiesPSelect = By.XPath("//p-selectbutton[@id='doesWorkInvolveBalconies']");
        private readonly By _workInvolveBalconiesDetailsValidation = By.XPath("//p-message[@id='detailsAboutBalconies-validation']//span[2]");
        private readonly By _workInvolveBalconiesDetails = By.XPath("//textarea[@id='detailsAboutBalconies']");

        private readonly By _isWorkAddingNewRoomsBlocksPSelect=By.XPath("//p-selectbutton[@id='newRoomsFloorsBlocks']");
        private readonly By _isWorkAddingNewRoomsBlocksValidation = By.XPath("//p-message[@id='newRoomsFloorsBlocks-validation']//span[2]");

        private readonly By _areAllRoomsAffectedPSelect=By.XPath("");
        private readonly By _affectedRoomsDetails = By.XPath("//textarea[@id='detailsAboutTheWork']");
        private readonly By _affectedRoomsDetailsValidation = By.XPath("//p-message[@id='detailsAboutTheWork-validation']//span[2]");

        private readonly By _haveTheHotelBlockedFloorPSelect=By.XPath("//p-selectbutton[@id='isHotelBlockedOffBtn']");
        private readonly By _haveTheHotelBlockedFloorValidation = By.XPath("//p-message[@id='isHotelBlockedOff-validation']");
        private readonly By _detailsBlockedFloor=By.XPath("//textarea[@id='txtHowFloorBlocked']");
        private readonly By _detailsBlockedFloorValidation = By.XPath("//p-message[@id='txtHowFloorBlocked-validation']//span[2]");

        private readonly By _isItSafeToAccessPSelect = By.XPath("//p-selectbutton[@id='isHotelSafeViaWorkSite']");
        private readonly By _detailsOfSafeToAccess = By.XPath("//textarea[@name='txtMoreDetails']");
        private readonly By _isTheFloorSafeForAccessValidation = By.XPath("//p-message[@id='isHotelSafeViaWorkSite-validation']//span[2]");
        private readonly By _isTheFloorSafeForAccessDetailsValidation = By.XPath("//p-message[@id='txtMoreDetails-validation']//span[2]");

        private readonly By _allRoomsAffected=By.XPath("//p-selectbutton[@id='allRoomsAffected']");
        private readonly By _allContractedRoomsAffectedValidation = By.XPath("//p-message[@id='allRoomsAffected-validation']//span[2]");

        private readonly By _typesOfRoomsAffected=By.XPath("//p-multiselect[@id='typesOfRoomsAffected']");
        private readonly By _typesOfRoomlRoomsAffectedValidation = By.XPath("//p-message[@id='typesOfRoomsAffected-validation']");

        private readonly By _detailsTypeOfRoomsAffected = By.XPath("//textarea[@id='detailsAboutFloorsBlocksAffected']");
        private readonly By _detailsTypeOfRoomsAffectedValidation=By.XPath("//p-message[@id='detailsAboutFloorsBlocksAffected-validation']");
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
                    case "is the work structural or cosmetic":
                        VerifyBwFieldValidationErrorMessage(_isTheWorkStructuralValidation, expectedError);
                        break;
                    case "work involve balconies":
                        VerifyBwFieldValidationErrorMessage(_doesWorkInvolveBalconiesValidation, expectedError);
                        break;
                    case "balconies details":
                        VerifyBwFieldValidationErrorMessage(_workInvolveBalconiesDetailsValidation, expectedError);
                        break;
                    case "is the work adding new rooms,floors, or blocks":
                        VerifyBwFieldValidationErrorMessage(_isWorkAddingNewRoomsBlocksValidation, expectedError);
                        break;
                    case "have the hotel blocked off the floor":
                        VerifyBwFieldValidationErrorMessage(_haveTheHotelBlockedFloorValidation, expectedError);
                        break;
                    case "please give more details hotel blocked off the floor":
                        VerifyBwFieldValidationErrorMessage(_detailsBlockedFloorValidation, expectedError);
                        break;
                    case "is the floor safe for access via worksite":
                        VerifyBwFieldValidationErrorMessage(_isTheFloorSafeForAccessValidation, expectedError);
                        break;
                    case "details of the floor safe for access via worksite":
                        VerifyBwFieldValidationErrorMessage(_isTheFloorSafeForAccessDetailsValidation, expectedError);
                        break;
                    case "details of affected rooms":
                        VerifyBwFieldValidationErrorMessage(_affectedRoomsDetailsValidation, expectedError);
                        break;
                    default:
                        Assert.Fail($"{field} is not a valid field");
                        return;
                }
            }
        }

        public void EnterAnswerToOnSiteRooms(Table table)
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
                    case "is the work":
                        Driver.ClickPSelectOption(_isTheWorkCosmeticStructPSelect, answer);
                        break;
                    case "does the building work have balconies":
                        Driver.ClickPSelectOption(_doesWorkInvolveBalconiesPSelect, answer);
                        if (answer.ToLower()== "yes")
                        {
                            Driver.WaitForItem(_workInvolveBalconiesDetails,10);
                        }
                        break;
                    case "details of building work balconies":
                        Driver.EnterText(_workInvolveBalconiesDetails, answer); 
                        break;
                    case "is the work adding new rooms,floors, or blocks":
                        Driver.ClickPSelectOption(_isWorkAddingNewRoomsBlocksPSelect, answer);
                        break;
                    case "are all rooms affected":
                        Driver.ClickPSelectOption(_areAllRoomsAffectedPSelect, answer);
                        break;
                    case "details of affected rooms":
                        Driver.EnterText(_affectedRoomsDetails, answer);
                        break;
                    case "have the hotel blocked off the floor":
                        Driver.ClickPSelectOption(_haveTheHotelBlockedFloorPSelect,answer);
                        break;
                    case "is the floor safe for access via worksite":
                        Driver.ClickPSelectOption(_isItSafeToAccessPSelect, answer);
                        break;
                    case "please give more details hotel blocked off the floor":
                        Driver.EnterText(_detailsBlockedFloor,answer);
                        break;
                    case "details of the floor safe for access via worksite":
                        Driver.EnterText(_detailsOfSafeToAccess, answer);
                        break;     
                    default:
                        Assert.Fail($"{question} is not a valid Onsite Rooms question");
                        return;
                }

            }
        }


        public void VerifyAnswerToOnSiteRooms(Table table)
        {
            foreach (var row in table.Rows)
            {
                var question = row["question"];
                var answer = row["answer"];
                switch (question.ToLower())
                {
                    case "all contracted rooms affected":
                        Driver.VerifySingleSelectedPOption(_allRoomsAffected, answer);
                        break;
                    case "select types of rooms affected":
                        break;
                    case "details on types of room affected":
                        break;
                    case "is the work":
                        Driver.VerifySingleSelectedPOption(_isTheWorkCosmeticStructPSelect, answer);
                        break;
                    case "does the building work have balconies":
                        Driver.VerifySingleSelectedPOption(_doesWorkInvolveBalconiesPSelect, answer);
                        break;
                    case "details of building work balconies":
                        Driver.VerifyInputBoxText(_workInvolveBalconiesDetails, answer);
                        break;
                    case "is the work adding new rooms,floors, or blocks":
                        break;
                    case "are all rooms affected":
                        break;
                    case "details of affected rooms":
                        break;
                    case "have the hotel blocked off the floor":
                        Driver.VerifySingleSelectedPOption(_haveTheHotelBlockedFloorPSelect, answer);
                        break;
                    case "is the floor safe for access via worksite":
                        Driver.VerifySingleSelectedPOption(_isItSafeToAccessPSelect, answer);
                        break;
                    case "please give more details hotel blocked off the floor":
                        break;
                    case "details of the floor safe for access via worksite":
                        Driver.VerifyInputBoxText(_detailsOfSafeToAccess, answer);
                        break;
                    default:
                        Assert.Fail($"{question} is not a valid Onsite Rooms question");
                        return;
                }

            }
        }

        public void VerifyOnSiteQDisplayed(string displayedNotDisplayed,string question)
        {
            By questionToVerify=null;
            switch (question.ToLower())
            {
                case "all contracted rooms affected":
                    questionToVerify= _allRoomsAffected;
                    break;
                case "select types of rooms affected":
                    questionToVerify= _typesOfRoomsAffected;
                    break;
                case "details on types of room affected":
                    questionToVerify=_detailsTypeOfRoomsAffected;
                    break;
                case "is the work adding new rooms,floors or blocks":
                    questionToVerify = _isWorkAddingNewRoomsBlocksPSelect;
                        break;
                case "details of affected rooms":
                    questionToVerify = _affectedRoomsDetails;
                    break;
                case "is the floor safe for access via worksite":
                    questionToVerify = _isItSafeToAccessPSelect;
                    break;
                case "details about the floor safe for access via worksite":
                    questionToVerify = _detailsOfSafeToAccess;
                    break;
                case "please give more details hotel blocked off the floor":
                    questionToVerify = _detailsBlockedFloor;
                    break;
                default:
                    Assert.Fail($"{question} is not a valid Onsite Rooms question");
                    return;
            }

            switch (displayedNotDisplayed.ToLower())
            {
                case "displayed":
                    Assert.True(Driver.WaitForItem(questionToVerify, 5),"Could not find rooms question");
                    break;
                case "not displayed":
                    Assert.True(!Driver.WaitForItem(questionToVerify, 1), "Was able to find rooms question while expecting it not to be there");
                    break;
                default:
                    Assert.Fail($"{displayedNotDisplayed} is not a valid option, please select from either displayed or not displayed");
                    break;
            }
        }

        public void GetListOfRoomsAffected()
        {
            ListOfRoomsAffected = _apiCall   // get list of all rooms for the given property
                .GetListOfTypesOfRooms(Destination, Property)
                .Select(r => r.Name)
                .ToList();
        }


        public void VerifyListOfTypesOfRoomsAffected()
        {
            GetListOfRoomsAffected();
            var expectedListOfAffectedRooms = ListOfRoomsAffected;
            var actualListOfAffectedRooms = Driver.GetAllMultiselectOptions(_typesOfRoomsAffected);
            Assert.AreEqual(actualListOfAffectedRooms, expectedListOfAffectedRooms, $"Expected list of affected rooms {expectedListOfAffectedRooms} was not the same as actual {actualListOfAffectedRooms}");
        }

        private  void SelectTypesOfRoomsAffected(string affectedRooms)
        {
            GetListOfRoomsAffected();
            var allListOfAffectedRooms = ListOfRoomsAffected;
            var affectedRoomsListIndex = affectedRooms.ConvertStringIntoList();
            var affectedRoomsToSelect= new List<string>();
            foreach (var roomNumbToSelect in affectedRoomsListIndex)
            {
                var i = roomNumbToSelect.GetIntOnly();
                affectedRoomsToSelect.Add(allListOfAffectedRooms[i-1]);
            }

            Driver.SelectMultiselectOption(_typesOfRoomsAffected, affectedRoomsToSelect, true);
        }

    }
}

