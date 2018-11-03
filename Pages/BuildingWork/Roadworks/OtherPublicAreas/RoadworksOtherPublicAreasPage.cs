using System.Collections.Generic;
using System.Linq;
using J2BIOverseasOps.Extensions;
using J2BIOverseasOps.Models;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Pages.BuildingWork.Roadworks.OtherPublicAreas
{
    internal class RoadworksOtherPublicAreasPage : BuildingWorkCommon
    {
        //Please select the affected other public area
        private readonly By _affectedOtherPublicAreasDropDown = By.XPath("//p-dropdown[@id='otherPublicArea']");
        private readonly By _affectedOtherPublicAreasValidation = By.XPath("//p-message[@id='otherPublicArea-validation']//span[2]");

        //What is the name of the facility? 
        private readonly By _otherOtherPublicAreaName = By.XPath("//input[@id='otherAreaName']");
        private readonly By _otherOtherPublicAreaNameMandatory = By.XPath("//p-message[@id='otherAreaName-validation']//span[2]");

        //Please give details of possible impact on customer
        private readonly By _impactOnCustomers = By.XPath("//textarea[@id='possibleImpactOnCustomer']");
        private readonly By _impactOnCustomersValidation = By.XPath("//p-message[@id='possibleImpactOnCustomer-validation']");

        // Buttons  
        private readonly By _removeThisOtherPublicArea = By.XPath("//p-button[@id='removeThisItem']//button");
        private readonly By _addAnotherOtherPublicArea = By.XPath("//p-button[@id='addAnotherItem']//button");

        private readonly ApiCalls _apiCall;

        public RoadworksOtherPublicAreasPage(IWebDriver driver, ILog log, IRunData runData) : base(driver, log, runData)
        {
            _apiCall = new ApiCalls(runData);
        }

        public void VerifyOtherPublicAreasMandatoryMessage(Table table)
        {
            foreach (var row in table.Rows)
            {
                var field = row["field"];
                var expectedError = row["error"];
                switch (field.ToLower())
                {
                    case "select affected other public areas":
                        VerifyBwFieldValidationErrorMessage(_affectedOtherPublicAreasValidation, expectedError);
                        break;
                    case "name of the facility":
                        VerifyBwFieldValidationErrorMessage(_otherOtherPublicAreaNameMandatory, expectedError);
                        break;
                    case "possible impact on customers":
                        VerifyBwFieldValidationErrorMessage(_impactOnCustomersValidation, expectedError);
                        break;
                    default:
                        Assert.Fail($"{field} is not a valid field");
                        break;
                }
            }
        }

        public void VerifyListOfOtherPublicAreas(Table table)
        {
            var actualListOfFacilities = Driver.GetAllDropDownOptions(_affectedOtherPublicAreasDropDown);
            var expectedListOfFacilities = table.Rows.ToColumnList("public area");
            Assert.AreEqual(actualListOfFacilities, expectedListOfFacilities, $"Expected list of areas {expectedListOfFacilities} was not the same as actual {actualListOfFacilities}");
        }

        public void EnterRoadworksOtherPublicAreasAnswers(Table table)
        {
            foreach (var row in table.Rows)
            {
                var question = row["question"];
                var answer = row["answer"];

                switch (question.ToLower())
                {
                    case "affected other public areas":
                        Driver.SelectDropDownOption(_affectedOtherPublicAreasDropDown, answer);
                        break;
                    case "possible impact on customers":
                        Driver.EnterText(_impactOnCustomers, answer);
                        break;
                    case "name of the facility":
                        Driver.EnterText(_otherOtherPublicAreaName, answer);
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
                        element = _otherOtherPublicAreaName;
                        break;
                    default:
                        Assert.Fail($"{expectedField} is not a valid field");
                        break;
                }
                Assert.AreEqual(Driver.WaitForItem(element, 1), expectedState);
            }
        }


        public void VerifyOtherPublicAreasBtnEnabledDisabled(string btnName, string enabledDisabled)
        {
            VerifyElementState(enabledDisabled, _getAddRemoveBtn(btnName));
        }

        public void ClickOtherPublicAreasButton(string btnName)
        {
            Driver.ClickItem(_getAddRemoveBtn(btnName));
        }

        internal By _getAddRemoveBtn(string btnName)
        {
            By btnElement = null;
            switch (btnName.ToLower())
            {
                case "remove this other public area":
                    btnElement = _removeThisOtherPublicArea;
                    break;
                case "add another other public area":
                    btnElement = _addAnotherOtherPublicArea;
                    break;
                default:
                    Assert.Fail($"{btnName} is not a valid button");
                    break;
            }
            return btnElement;
        }

        public void VerifyOtherPublicAreaNotPresent(string excludesIncludes, string publicArea)
        {
            var excludeFacilityList = publicArea.ConvertStringIntoList(); // converts expected restaurants to a List
            var listOfActualAffectedFacilities = Driver.GetAllDropDownOptions(_affectedOtherPublicAreasDropDown);
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

