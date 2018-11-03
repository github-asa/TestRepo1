using System.Collections.Generic;
using J2BIOverseasOps.Extensions;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Pages.BuildingWork.Nearby.OtherAdvertisedFacilities
{
    internal class NearbyOtherFacilitiesPage : BuildingWorkCommon
    {
        //other facilities

        private readonly By _affectedOtherFacilitiesDropDown = By.XPath("//p-dropdown[@id='otherAdvertisedFacility']");
        private readonly By _affectedOtherFacilitiesValidation = By.XPath("//p-message[@id='otherAdvertisedFacility-validation']//span[2]");

        private readonly By _otherFacilityName = By.XPath("//input[@id='otherFacilityName']");
        private readonly By _otherFacilityNameValidation = By.XPath("//p-message[@id='otherFacilityName-validation']//span[2]");

        //how facilities affected
        private readonly By _howOtherFacilitiesAffectedDrpDwn = By.XPath("//p-dropdown[@id='howIsTheOtherAdvertisedFacilityAffected']");
        private readonly By _howOtherFacilitiesAffectedValidation = By.XPath("//p-message[@id='howIsTheOtherAdvertisedFacilityAffected-validation']//span[2]");
        private readonly By _howOtherFacilitiesAffectedDetails = By.XPath("//textarea[@id='moreDetails']");
        private readonly By _howOtherFacilitiesAffectedDetailsValidation = By.XPath("//p-message[@id='moreDetails-validation']");

        //Hotel Providing alternative arrangements
        private readonly By _workProvidingAlternArrangements = By.XPath("//p-selectbutton[@id='alternativeArrangements']");
        private readonly By _workProvidingAlternArrangementsValidation = By.XPath("//p-message[@id='alternativeArrangements-validation']");
        private readonly By _detailsOfAlternativeArrangements = By.XPath("//textarea[@id='alternativeArrangementsDetails']");
        private readonly By _detailsOfAlternativeArrangementsValidation = By.XPath("//p-message[@id='alternativeArrangementsDetails-validation']");

        // Buttons  
        private readonly By _removeThisFacility = By.XPath("//p-button[@id='removeThisFacility']//button");
        private readonly By _addAnotherFacility = By.XPath("//p-button[@id='addAnotherFacility']//button");

        private readonly ApiCalls _apiCall;

        public NearbyOtherFacilitiesPage(IWebDriver driver, ILog log, IRunData rundata) : base(driver, log, rundata)
        {
            _apiCall = new ApiCalls(rundata);
        }

        public void VerifyOtherFacilitiesMandatoryMessage(Table table)
        {
            foreach (var row in table.Rows)
            {
                var field = row["field"];
                var expectedError = row["error"];

                switch (field.ToLower())
                {
                    case "select affected facilities":
                        VerifyBwFieldValidationErrorMessage(_affectedOtherFacilitiesValidation, expectedError);
                        break;
                    case "name of the facility":
                        VerifyBwFieldValidationErrorMessage(_otherFacilityNameValidation, expectedError);
                        break;
                    case "how are the other facilities affected":
                        VerifyBwFieldValidationErrorMessage(_howOtherFacilitiesAffectedValidation, expectedError);
                        break;
                    case "how are the other facilities affected details":
                        VerifyBwFieldValidationErrorMessage(_howOtherFacilitiesAffectedDetailsValidation, expectedError);
                        break;
                    case "hotel providing alternative arrangements":
                        VerifyBwFieldValidationErrorMessage(_workProvidingAlternArrangementsValidation, expectedError);
                        break;
                    case "details of alternative arrangements":
                        VerifyBwFieldValidationErrorMessage(_detailsOfAlternativeArrangementsValidation, expectedError);
                        break;
                    default:
                        Assert.Fail($"{field} is not a valid field");
                        break;
                }
            }
        }

        public void VerifyListHowOtherFacilitiesAffected(Table table)
        {
            var actualList = Driver.GetAllDropDownOptions(_howOtherFacilitiesAffectedDrpDwn);
            var expectedList = table.Rows.ToColumnList("how affected");
            Assert.AreEqual(actualList, expectedList, $"Expected list  {expectedList} was not the same as actual {actualList}");
        }

        public void EnterOnSiteOtherFacilitiesAnswers(Table table)
        {
            foreach (var row in table.Rows)
            {
                var question = row["question"];
                var answer = row["answer"];

                switch (question.ToLower())
                {
                    case "affected facility":
                        Driver.SelectDropDownOption(_affectedOtherFacilitiesDropDown, answer);
                        break;
                    case "how are the other facilities affected":
                        Driver.SelectDropDownOption(_howOtherFacilitiesAffectedDrpDwn, answer);
                        break;
                    case "how are the other facilities affected details":
                        Driver.EnterText(_howOtherFacilitiesAffectedDetails, answer);
                        break;
                    case "name of the facility":
                        Driver.EnterText(_otherFacilityName, answer);
                        break;
                    case "hotel providing alternative arrangements":
                        Driver.ClickPSelectOption(_workProvidingAlternArrangements, answer);
                        break;
                    case "details of alternative arrangements":
                        Driver.EnterText(_detailsOfAlternativeArrangements, answer);
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
                    case "name of the facility":
                        element = _otherFacilityName;
                        break;
                    case "how are the other facilities affected details":
                        element = _howOtherFacilitiesAffectedDetails;
                        break;
                    case "hotel providing alternative arrangements":
                        element = _workProvidingAlternArrangements;
                        break;
                    case "details of alternative arrangements":
                        element = _detailsOfAlternativeArrangements;
                        break;
                    default:
                        Assert.Fail($"{expectedField} is not a valid field");
                        break;
                }
                Assert.AreEqual(Driver.WaitForItem(element, 1), expectedState);
            }
        }

        public void VerifyOtherFacilitiesBtnEnabledDisabled(string btnName, string enabledDisabled)
        {
            VerifyElementState(enabledDisabled, _getAddRemoveBtn(btnName));
        }

        public void ClickFacilitiesButton(string btnName)
        {
            Driver.ClickItem(_getAddRemoveBtn(btnName));
        }

        internal By _getAddRemoveBtn(string btnName)
        {
            By btnElement = null;
            switch (btnName.ToLower())
            {
                case "remove this facility":
                    btnElement = _removeThisFacility;
                    break;
                case "add another facility":
                    btnElement = _addAnotherFacility;
                    break;
                default:
                    Assert.Fail($"{btnName} is not a valid button");
                    break;
            }
            return btnElement;
        }

        public void VerifyFacilityNotPresent(string excludesIncludes, string facility)
        {
            var excludeFacilityList = facility.ConvertStringIntoList(); // converts expected facilities to a List
            var listOfActualAffectedFacilities = Driver.GetAllDropDownOptions(_affectedOtherFacilitiesDropDown);
            switch (excludesIncludes.ToLower())
            {
                case "excludes":
                    foreach (var t in excludeFacilityList)
                    {
                        Assert.True(listOfActualAffectedFacilities.Contains(t) == false);
                    }
                    break;
                case "includes":
                    foreach (var t in excludeFacilityList)
                    {
                        Assert.True(listOfActualAffectedFacilities.Contains(t));
                    }
                    break;
            }
        }

        public void VerifyListOfOtherFacilities()
        {
            var otherAdvertisedFacilities = _apiCall.GetListOfOtherFacilities(Destination, Property);
            var expectedListOfFacilities = new List<string>();
            foreach (var facility in otherAdvertisedFacilities)
            {
                expectedListOfFacilities.Add(facility.Name);
            }
            expectedListOfFacilities.Add("Other");

            var actualListOfFacilites = Driver.GetAllDropDownOptions(_affectedOtherFacilitiesDropDown);
            Assert.AreEqual(actualListOfFacilites, expectedListOfFacilities, $"Expected list of facilities {expectedListOfFacilities} was not the same as actual {actualListOfFacilites}");
        }
    }
}

