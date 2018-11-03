using J2BIOverseasOps.Extensions;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Pages.BuildingWork.Onsite.OnsiteGeneralQ
{
    internal class OnsiteGeneralQPage : BuildingWorkCommon
    {
        //Can the hotel commit to allocating all Jet2holidays away from the work?
        private readonly By _canHotelCommitAllocating = By.XPath("//p-selectbutton[@id='canHotelCommitAwayFromWork']");
        private readonly By _canHotelCommitAllocatingValidation = By.XPath("//p-message[@id='canHotelCommitAwayFromWork-validation']");
        private readonly By _canHotelCommitAllocatingDetails = By.XPath("//textarea[@id='detailsOfBlockRoomAndDistance']");
        private readonly By _canHotelCommitAllocatingDetailsValidation = By.XPath("//p-message[@id='detailsOfBlockRoomAndDistance-validation']");
        private readonly By _sendNotificationToContracting = By.XPath("//p-selectbutton[@id='doNeedToSendNotificationForHelp']");

        //Does the work impact any aspect of the fire detection/alarm system?
        private readonly By _theWorkimpactsAspectOfFireDetection = By.XPath("//p-selectbutton[@id='doesWorkImpactFireDetectionSystem']");
        private readonly By _theWorkimpactsAspectOfFireDetectionValidation = By.XPath("//p-message[@id='doesWorkImpactFireDetectionSystem-validation']");
        private readonly By _selfClosingFireDoorsAffected = By.XPath("//p-selectbutton[@id='areTheSelfClosingFireDoorsAffected']");
        private readonly By _selfClosingFireDoorsAffectedValidation = By.XPath("//p-message[@id='areTheSelfClosingFireDoorsAffected-validation']");
        private readonly By _sprinklerSystemAffected = By.XPath("//p-selectbutton[@id='isTheSprinklerSystemAffected']");
        private readonly By _sprinklerSystemAffectedValidation = By.XPath("//p-message[@id='isTheSprinklerSystemAffected-validation']");
        private readonly By _emergencyLightingAffected = By.XPath("//p-selectbutton[@id='isTheEmergencyLightAffected']");
        private readonly By _emergencyLightingAffectedValidation = By.XPath("//p-message[@id='isTheEmergencyLightAffected-validation']");
        private readonly By _escapeRouteAffected = By.XPath("//p-selectbutton[@id='isTheEscapeRoutesAffected']");
        private readonly By _escapeRouteAffectedValidation = By.XPath("//p-message[@id='isTheEscapeRoutesAffected-validation']");
        private readonly By _workImpactDetails=By.XPath("//textarea[@id='impactOfFireDetectionSystemDetails']");
        private readonly By _workImpactDetailsValidation = By.XPath("//p-message[@id='impactOfFireDetectionSystemDetails-validation']");

        //Is the work affecting the hot/cold water system
        private readonly By _theWorkaffectingHotColdWater = By.XPath("//p-selectbutton[@id='isWorkAffectingWaterSystem']");
        private readonly By _theWorkaffectingHotColdWaterValidation = By.XPath("//p-message[@id='isWorkAffectingWaterSystem-validation']");
        private readonly By _theWorkaffectingHotColdWaterDetails = By.XPath("//textarea[@id='affectedWaterSystemDetails']"); 
        private readonly By _theWorkaffectingHotColdWaterDetailsValidation = By.XPath("//p-message[@id='affectedWaterSystemDetails-validation']");

        //Does the building work involve modification to the Hotel Walkway
        private readonly By _theWorkInvolveModToHotelWalkway = By.XPath("//p-selectbutton[@id='doesWorkInvolveModificationToWalkWay']");
        private readonly By _theWorkInvolveModToHotelWalkwayValidation = By.XPath("//p-message[@id='doesWorkInvolveModificationToWalkWay-validation']");
        private readonly By _theWorkInvolveModToHotelWalkwayDetails = By.XPath("//textarea[@id='affactedWalkwayDetails']");
        private readonly By _theWorkInvolveModToHotelWalkwayDetailsValidation = By.XPath("//p-message[@id='affactedWalkwayDetails-validation']");

        //Hotel Offering GOGW
        private readonly By _hotelOfferingGoGw = By.XPath("//p-selectbutton[@id='isHotelOfferingGogw']");
        private readonly By _hotelOfferingGoGwValidation = By.XPath("//p-message[@id='isHotelOfferingGogw-validation']");
        private readonly By _hotelOfferingGoGwDetails = By.XPath("//textarea[@id='hotelOfferingGogwDetails']");
        private readonly By _hotelOfferingGoGwDetailsValidation = By.XPath("//p-message[@id='hotelOfferingGogwDetails-validation']");
        private readonly By _sendNotfContractingForGoGw=By.XPath("//p-selectbutton[@id='notificationToContractingForNotOfferingGogw']");

        public OnsiteGeneralQPage(IWebDriver driver, ILog log, IRunData rundata) : base(driver, log, rundata)
        { }

        public void VerifyOnsiteMandatoryMessage(Table table)
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
                    case "the work impacts any aspect of the fire detection system":
                        VerifyBwFieldValidationErrorMessage(_theWorkimpactsAspectOfFireDetectionValidation, expectedError);
                        break;
                    case "are the self closing fire doors affected":
                        VerifyBwFieldValidationErrorMessage(_selfClosingFireDoorsAffectedValidation, expectedError);
                        break;
                    case "is the sprinkler system affected":
                        VerifyBwFieldValidationErrorMessage(_sprinklerSystemAffectedValidation, expectedError);
                        break;
                    case "is the emergency lighting affected":
                        VerifyBwFieldValidationErrorMessage(_emergencyLightingAffectedValidation, expectedError);
                        break;
                    case "are the escape routes affected/dead end corridors":
                        VerifyBwFieldValidationErrorMessage(_escapeRouteAffectedValidation, expectedError);
                        break;
                    case "impact any aspect of the fire detection details":
                        VerifyBwFieldValidationErrorMessage(_workImpactDetailsValidation, expectedError);
                        break;
                    case "the work affecting the hot cold water system":
                        VerifyBwFieldValidationErrorMessage(_theWorkaffectingHotColdWaterValidation,expectedError);
                        break;
                    case "the work affecting the hot cold water system details":
                        VerifyBwFieldValidationErrorMessage(_theWorkaffectingHotColdWaterDetailsValidation, expectedError);
                        break;
                    case "the work involve modification to the hotel walkway":
                        VerifyBwFieldValidationErrorMessage(_theWorkInvolveModToHotelWalkwayValidation, expectedError);
                        break;
                    case "the work involve modification to the hotel walkway details":
                        VerifyBwFieldValidationErrorMessage(_theWorkInvolveModToHotelWalkwayDetailsValidation, expectedError);
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


        public void EnterOnSiteGeneralQAnswers(Table table)
        {
            foreach (var row in table.Rows)
            {
                var question = row["question"];
                var answer = row["answer"];

                switch (question.ToLower())
                {
                    case "can hotel commit allocating all rooms":
                        Driver.ClickPSelectOption(_canHotelCommitAllocating,answer);
                        break;
                    case "can hotel commit allocating all rooms details":
                        Driver.EnterText(_canHotelCommitAllocatingDetails, answer);
                        break;
                    case "send a notification to contracting for help":
                        Driver.ClickPSelectOption(_sendNotificationToContracting, answer);
                        break;
                    case "the work impacts any aspect of the fire detection system":
                        Driver.ClickPSelectOption(_theWorkimpactsAspectOfFireDetection, answer);
                        break;
                    case "are the self closing fire doors affected":
                        Driver.ClickPSelectOption(_selfClosingFireDoorsAffected, answer);
                        break;
                    case "is the sprinkler system affected":
                        Driver.ClickPSelectOption(_sprinklerSystemAffected, answer);
                        break;
                    case "is the emergency lighting affected":
                        Driver.ClickPSelectOption(_emergencyLightingAffected, answer);
                        break;
                    case "are the escape routes affected/dead end corridors":
                        Driver.ClickPSelectOption(_escapeRouteAffected, answer);
                        break;
                    case "impact any aspect of the fire detection details":
                        Driver.EnterText(_workImpactDetails,answer);
                        break;
                    case "the work affecting the hot cold water system":
                        Driver.ClickPSelectOption(_theWorkaffectingHotColdWater, answer);
                        break;
                    case "the work affecting the hot cold water system details":
                        Driver.EnterText(_theWorkaffectingHotColdWaterDetails, answer);
                        break;
                    case "the work involve modification to the hotel walkway":
                        Driver.ClickPSelectOption(_theWorkInvolveModToHotelWalkway,answer);
                        break;
                    case "the work involve modification to the hotel walkway details":
                        Driver.EnterText(_theWorkInvolveModToHotelWalkwayDetails,answer);
                        break;
                    case "is hotel offering any gogw":
                        Driver.ClickPSelectOption(_hotelOfferingGoGw, answer);
                        break;
                    case "is hotel offering any gogw details":
                        Driver.EnterText(_hotelOfferingGoGwDetails, answer);
                        break;
                    case "send a notification to contracting for help with gogw":
                        Driver.ClickPSelectOption(_sendNotfContractingForGoGw,answer);
                        break;
                    default:
                        Assert.Fail($"{question} is not a valid field");
                        break;
                }
            }
        }

        public void VerifyOnSiteGeneralQAnswers(Table table)
        {
            foreach (var row in table.Rows)
            {
                var question = row["question"];
                var answer = row["answer"];

                switch (question.ToLower())
                {
                    case "can hotel commit allocating all rooms":
                        Driver.VerifySingleSelectedPOption(_canHotelCommitAllocating, answer);
                        break;
                    case "can hotel commit allocating all rooms details":
                        Driver.VerifyInputBoxText(_canHotelCommitAllocatingDetails, answer);
                        break;
                    case "send a notification to contracting for help":
                        Driver.VerifySingleSelectedPOption(_sendNotificationToContracting, answer);
                        break;
                    case "the work impacts any aspect of the fire detection system":
                        Driver.VerifySingleSelectedPOption(_theWorkimpactsAspectOfFireDetection, answer);
                        break;
                    case "are the self closing fire doors affected":
                        Driver.VerifySingleSelectedPOption(_selfClosingFireDoorsAffected, answer);
                        break;
                    case "is the sprinkler system affected":
                        Driver.VerifySingleSelectedPOption(_sprinklerSystemAffected, answer);
                        break;
                    case "is the emergency lighting affected":
                        Driver.VerifySingleSelectedPOption(_emergencyLightingAffected, answer);
                        break;
                    case "are the escape routes affected/dead end corridors":
                        Driver.VerifySingleSelectedPOption(_escapeRouteAffected, answer);
                        break;
                    case "impact any aspect of the fire detection details":
                        Driver.VerifyInputBoxText(_workImpactDetails, answer);
                        break;
                    case "the work affecting the hot cold water system":
                        Driver.VerifySingleSelectedPOption(_theWorkaffectingHotColdWater, answer);
                        break;
                    case "the work affecting the hot cold water system details":
                        Driver.VerifyInputBoxText(_theWorkaffectingHotColdWaterDetails, answer);
                        break;
                    case "the work involve modification to the hotel walkway":
                        Driver.VerifySingleSelectedPOption(_theWorkInvolveModToHotelWalkway, answer);
                        break;
                    case "the work involve modification to the hotel walkway details":
                        Driver.VerifyInputBoxText(_theWorkInvolveModToHotelWalkwayDetails, answer);
                        break;
                    case "is hotel offering any gogw":
                        Driver.VerifySingleSelectedPOption(_hotelOfferingGoGw, answer);
                        break;
                    case "is hotel offering any gogw details":
                        Driver.VerifyInputBoxText(_hotelOfferingGoGwDetails, answer);
                        break;
                    case "send a notification to contracting for help with gogw":
                        Driver.VerifySingleSelectedPOption(_sendNotfContractingForGoGw, answer);
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
                    case "the work impacts any aspect of the fire detection system":
                        element = _theWorkimpactsAspectOfFireDetection;
                        break;
                    case "are the self closing fire doors affected":
                        element = _selfClosingFireDoorsAffected;
                        break;
                    case "is the sprinkler system affected":
                        element = _sprinklerSystemAffected;
                        break;
                    case "is the emergency lighting affected":
                        element = _emergencyLightingAffected;
                        break;
                    case "are the escape routes affected/dead end corridors":
                        element = _escapeRouteAffected;
                        break;
                    case "impact any aspect of the fire detection details":
                        element = _workImpactDetails;
                        break;
                    case "the work affecting the hot cold water system":
                        element=_theWorkaffectingHotColdWater;
                        break;
                    case "the work affecting the hot cold water system details":
                        element=_theWorkaffectingHotColdWaterDetails;
                        break;
                    case "the work involve modification to the hotel walkway":
                        element = _theWorkInvolveModToHotelWalkway;
                        break;
                    case "the work involve modification to the hotel walkway details":
                        element = _theWorkInvolveModToHotelWalkwayDetails;
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

