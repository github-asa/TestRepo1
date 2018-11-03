using J2BIOverseasOps.Extensions;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Pages.BuildingWork.Roadworks.GeneralQuestions
{
    internal class RoadworksGeneralQPage : BuildingWorkCommon
    {
        //Can the hotel commit to allocating all Jet2holidays rooms away from the work? * 
        private readonly By _canHotelCommitAllocating = By.XPath("//p-selectbutton[@id='canHotelCommitAwayFromWork']");
        private readonly By _canHotelCommitAllocatingValidation = By.XPath("//p-message[@id='canHotelCommitAwayFromWork-validation']");
        private readonly By _canHotelCommitAllocatingDetails = By.XPath("//textarea[@id='detailsOfBlockRoomAndDistance']");
        private readonly By _canHotelCommitAllocatingDetailsValidation = By.XPath("//p-message[@id='detailsOfBlockRoomAndDistance-validation']");
        private readonly By _sendNotificationToContracting = By.XPath("//p-selectbutton[@id='doNeedToSendNotificationForHelp']");

        //Is the property offering any Gesture of Goodwill (GOGW)? * 
        private readonly By _hotelOfferingGoGw = By.XPath("//p-selectbutton[@id='isHotelOfferingGogw']");
        private readonly By _hotelOfferingGoGwValidation = By.XPath("//p-message[@id='isHotelOfferingGogw-validation']");
        private readonly By _hotelOfferingGoGwDetails = By.XPath("//textarea[@id='hotelOfferingGogwDetails']");
        private readonly By _hotelOfferingGoGwDetailsValidation = By.XPath("//p-message[@id='hotelOfferingGogwDetails-validation']");
        private readonly By _sendNotfContractingForGoGw = By.XPath("//p-selectbutton[@id='notificationToContractingForNotOfferingGogw']");

        public RoadworksGeneralQPage(IWebDriver driver, ILog log, IRunData rundata) : base(driver, log, rundata)
        { }

        public void VerifyRoadworksMandatoryMessage(Table table)
        {
            foreach (var row in table.Rows)
            {
                var field = row["field"];
                var expectedError = row["error"];
                switch (field.ToLower())
                {
                    case "can hotel commit allocating all rooms":
                        VerifyBwFieldValidationErrorMessage(_canHotelCommitAllocatingValidation, expectedError);
                        break;
                    case "can hotel commit allocating all rooms details":
                        VerifyBwFieldValidationErrorMessage(_canHotelCommitAllocatingDetailsValidation, expectedError);
                        break;
                    case "is hotel offering any gogw":
                        VerifyBwFieldValidationErrorMessage(_hotelOfferingGoGwValidation, expectedError);
                        break;
                    case "is hotel offering any gogw details":
                        VerifyBwFieldValidationErrorMessage(_hotelOfferingGoGwDetailsValidation, expectedError);
                        break;
                    default:
                        Assert.Fail($"{field} is not a valid field");
                        break;
                }
            }
        }

        public void EnterRoadworksGeneralQAnswers(Table table)
        {
            foreach (var row in table.Rows)
            {
                var question = row["question"];
                var answer = row["answer"];

                switch (question.ToLower())
                {
                    case "can hotel commit allocating all rooms":
                        Driver.ClickPSelectOption(_canHotelCommitAllocating, answer);
                        break;
                    case "can hotel commit allocating all rooms details":
                        Driver.EnterText(_canHotelCommitAllocatingDetails, answer);
                        break;
                    case "send a notification to contracting for help":
                        Driver.ClickPSelectOption(_sendNotificationToContracting, answer);
                        break;
                    case "is hotel offering any gogw":
                        Driver.ClickPSelectOption(_hotelOfferingGoGw, answer);
                        break;
                    case "is hotel offering any gogw details":
                        Driver.EnterText(_hotelOfferingGoGwDetails, answer);
                        break;
                    case "send a notification to contracting for help with gogw":
                        Driver.ClickPSelectOption(_sendNotfContractingForGoGw, answer);
                        break;
                    default:
                        Assert.Fail($"{question} is not a valid field");
                        break;
                }
            }
        }

        public void VerifyFieldsDisplayedOrNot(string displayedOrNot, Table table)
        {
            var expectedState = displayedOrNot == "displayed";
            foreach (var row in table.Rows)
            {
                var expectedField = row["field"];
                By element = null;
                switch (expectedField.ToLower())
                {
                    case "can hotel commit allocating all rooms details":
                        element = _canHotelCommitAllocatingDetails;
                        break;
                    case "send a notification to contracting for help":
                        element = _sendNotificationToContracting;
                        break;
                    case "is hotel offering any gogw":
                        element = _hotelOfferingGoGw;
                        break;
                    case "is hotel offering any gogw details":
                        element = _hotelOfferingGoGwDetails;
                        break;
                    case "send a notification to contracting for help with gogw":
                        element = _sendNotfContractingForGoGw;
                        break;
                    default:
                        Assert.Fail($"{expectedField} is not a valid field");
                        break;
                }
                Assert.AreEqual(Driver.WaitForItem(element, 1), expectedState);
            }
        }
    }
}

