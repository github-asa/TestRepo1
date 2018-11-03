using System.Linq;
using J2BIOverseasOps.Extensions;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Pages.BuildingWork.Onsite.OtherAdvertisedFacilities
{
    internal class OnsiteOtherFacilitiesPage : BuildingWorkCommon
    {
        private readonly By _affectedBarsMandatory = By.XPath("//p-message[@id='affectedOtherAdvertisedFacilities-validation']//span[2]"); 
        private readonly By _affectedOtherFacilitiesDropDown = By.XPath("//p-dropdown[@id='affectedOtherAdvertisedFacilities']");

        //other facilities
        private readonly By _isThisNewFacility = By.XPath("//p-selectbutton[@id='isThisFacilityNew']");
        private readonly By _isThisNewFacilityMandatory = By.XPath("//p-message[@id='isThisFacilityNew-validation']//span[2]");
        private readonly By _otherFacilityName = By.XPath("//input[@id='otherFacilityName']");
        private readonly By _otherFacilityNameMandatory = By.XPath("//p-message[@id='otherFacilityName-validation']//span[2]"); 


        // is the work
        private readonly By _typeOfWork = By.XPath("//p-selectbutton[@id='typeOfWork']");
        private readonly By _typeOfWorkValidation = By.XPath("//p-message[@id='typeOfWork-validation']//span[2]"); 

        // details of work
        private readonly By _detailsOfWork = By.XPath("//textarea[@id='detailsAboutTheWork']");
        private readonly By _detailsOfWorkValidation = By.XPath("//p-message[@id='detailsAboutTheWork-validation']");

        //how facilities affected
        private readonly By _howOtherFacilitiesAffectedDrpDwn = By.XPath("//p-dropdown[@id='howIsTheOtherAdvertisedFacilityAffected']");
        private readonly By _howOtherFacilitiesAffectedValidation = By.XPath("//p-message[@id='howIsTheOtherAdvertisedFacilityAffected-validation']//span[2]"); //TODO
        private readonly By _howOtherFacilitiesAffectedDetails = By.XPath("//textarea[@id='moreDetails']");
        private readonly By _howOtherFacilitiesAffectedDetailsValidation = By.XPath("//p-message[@id='moreDetails-validation']");

        //Hotel Providing alternative arrangements
        private readonly By _workProvidingAlternArrangements = By.XPath("//p-selectbutton[@id='alternativeArrangements']");
        private readonly By _workProvidingAlternArrangementsValidation = By.XPath("//p-message[@id='alternativeArrangements-validation']");
        private readonly By _detailsOfAlternativeArrangements = By.XPath("//textarea[@id='alternativeArrangementsDetails']");
        private readonly By _detailsOfAlternativeArrangementsValidation = By.XPath("//p-message[@id='alternativeArrangementsDetails-validation']");

        // Buttons  
        private readonly By _removeThisFacility = By.XPath("//p-button[@id='btnRemoveThisFacility']//button");
        private readonly By _addAnotherFacility = By.XPath("//p-button[@id='addAnotherFacility']//button");

        public OnsiteOtherFacilitiesPage(IWebDriver driver, ILog log, IRunData rundata) : base(driver, log, rundata)
        {
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
                        VerifyBwFieldValidationErrorMessage(_affectedBarsMandatory, expectedError);
                        break;
                    case "is this a new facility":
                        VerifyBwFieldValidationErrorMessage(_isThisNewFacilityMandatory, expectedError);
                        break;
                    case "name of the facility":
                        VerifyBwFieldValidationErrorMessage(_otherFacilityNameMandatory, expectedError);
                        break;
                    case "is the facilities work":
                        VerifyBwFieldValidationErrorMessage(_typeOfWorkValidation, expectedError);
                        break;
                    case "details of work":
                        VerifyBwFieldValidationErrorMessage(_detailsOfWorkValidation, expectedError);
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


        public void VerifyListOfFacilities(Table table)
        {
            var actualListOfFacilities = Driver.GetAllDropDownOptions(_affectedOtherFacilitiesDropDown);
            var expectedListOfFacilities = table.Rows.ToColumnList("facility");
            Assert.AreEqual(actualListOfFacilities, expectedListOfFacilities, $"Expected list of bars {expectedListOfFacilities} was not the same as actual {actualListOfFacilities}");
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
                    case "is this a new facility":
                        Driver.ClickPSelectOption(_isThisNewFacility, answer);
                        break;
                    case "affected facility":
                        Driver.SelectDropDownOption(_affectedOtherFacilitiesDropDown,answer);
                        break;
                    case "is the facility work":
                        Driver.ClickPSelectOption(_typeOfWork, answer);
                        break;
                    case "details of work":
                        Driver.EnterText(_detailsOfWork, answer);
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
                    case "is this a new facility":
                        element = _isThisNewFacility;
                        break;
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

        public void VerifyOnSiteOtherFacilitiesAnswers(Table table)
        {
            foreach (var row in table.Rows)
            {
                var question = row["question"];
                var answer = row["answer"];

                switch (question.ToLower())
                {
                    case "is the facility work":
                      Driver.VerifyMultiSelectedPOption(_typeOfWork,answer.ConvertStringIntoList());
                        break;
                    default:
                        Assert.Fail($"{question} is not a valid field");
                        break;
                }

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
            var excludeFacilityList = facility.ConvertStringIntoList(); // converts expected restaurants to a List
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
    }

}

