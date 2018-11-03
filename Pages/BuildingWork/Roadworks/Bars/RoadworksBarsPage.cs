using System.Collections.Generic;
using System.Linq;
using J2BIOverseasOps.Extensions;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Pages.BuildingWork.Roadworks.Bars
{
    internal class RoadworksBarsPage : BuildingWorkCommon
    {
        private readonly By _affectedBarsMandatory = By.XPath("//p-message[@id='affectedBars-validation']//span[2]");
        private readonly By _affectedBarsDropDown = By.XPath("//p-dropdown[@id='affectedBars']");

        //other bars
        private readonly By _otherBarName = By.XPath("//input[@id='otherBarName']");
        private readonly By _otherBarNameMandatory = By.XPath("//p-message[@id='otherBarName-validation']//span[2]");


        //how bars affected
        private readonly By _howBarsAffectedDrpDwn = By.XPath("//p-dropdown[@id='howIsTheBarAffected']");
        private readonly By _howBarsAffectedValidation = By.XPath("//p-message[@id='affectedBars-validation']");
        private readonly By _howBarsAffectedDetails = By.XPath("//textarea[@id='howIsTheBarAffectedDetails']");
        private readonly By _howBarsAffectedDetailsValidation = By.XPath("//p-message[@id='howIsTheBarAffectedDetails-validation']");

        // work affect board basis
        private readonly By _workAffectBoardBasis = By.XPath("//p-selectbutton[@id='doesItAffectBoardBasis']");
        private readonly By _workAffectBoardBasisValidation = By.XPath("//p-message[@id='doesItAffectBoardBasis-validation']");
        private readonly By _workAffectBoardBasisDetails = By.XPath("//textarea[@id='doesItAffectBoardBasisDetails']");
        private readonly By _workAffectBoardBasisDetailsValidation = By.XPath("//p-message[@id='doesItAffectBoardBasisDetails-validation']");

        //Hotel Providing alternative arrangements
        private readonly By _workProvidingAlternArrangements = By.XPath("//p-selectbutton[@id='alternativeArrangements']");
        private readonly By _detailsOfAlternativeArrangements = By.XPath("//textarea[@id='alternativeArrangementsDetails']");
        private readonly By _capacityOfAllResidents = By.XPath("//p-selectbutton[@id='alternativeHasCapacityForAll']");
        private readonly By _detailsOfCapacityOfAllResidents = By.XPath("//textarea[@id='alternativeHasCapacityForAllDetails']");
        private readonly By _workProvidingAlternArrangementsValidation = By.XPath("//p-message[@id='alternativeArrangementsDetails-validation']");
        private readonly By _detailsOfAlternativeArrangementsValidation = By.XPath("//p-message[@id='alternativeArrangementsDetails-validation']");
        private readonly By _capacityOfAllResidentsValidation = By.XPath("//p-message[@id='alternativeHasCapacityForAll-validation']");
        private readonly By _detailsOfCapacityOfAllResidentsValidation = By.XPath("//p-message[@id='alternativeHasCapacityForAllDetails-validation']");

        // Buttons  
        private readonly By _removeThisBar = By.XPath("//p-button[@id='removeThisItem']//button");
        private readonly By _addThisBar = By.XPath("//p-button[@id='addAnotherItem']//button");
        private readonly ApiCalls _apiCall;

        public RoadworksBarsPage(IWebDriver driver, ILog log, IRunData rundata) : base(driver, log, rundata)
        {
            _apiCall = new ApiCalls(rundata);

        }

        public void VerifyBarsMandatoryMessage(Table table)
        {
            foreach (var row in table.Rows)
            {
                var field = row["field"];
                var expectedError = row["error"];

                switch (field.ToLower())
                {
                    case "select affected bars":
                        VerifyBwFieldValidationErrorMessage(_affectedBarsMandatory, expectedError);
                        break;
                    case "name of the facility":
                        VerifyBwFieldValidationErrorMessage(_otherBarNameMandatory, expectedError);
                        break;
                    case "how is bar affected":
                        VerifyBwFieldValidationErrorMessage(_howBarsAffectedValidation, expectedError);
                        break;
                    case "how is bar affected details":
                        VerifyBwFieldValidationErrorMessage(_howBarsAffectedDetailsValidation, expectedError);
                        break;
                    case "work affect board basis":
                        VerifyBwFieldValidationErrorMessage(_workAffectBoardBasisValidation, expectedError);
                        break;
                    case "work affect board basis details":
                        VerifyBwFieldValidationErrorMessage(_workAffectBoardBasisDetailsValidation, expectedError);
                        break;
                    case "hotel providing alternative arrangements":
                        VerifyBwFieldValidationErrorMessage(_workProvidingAlternArrangementsValidation, expectedError);
                        break;
                    case "details of alternative arrangements":
                        VerifyBwFieldValidationErrorMessage(_detailsOfAlternativeArrangementsValidation, expectedError);
                        break;
                    case "does the alternative have capacity for all residents":
                        VerifyBwFieldValidationErrorMessage(_capacityOfAllResidentsValidation, expectedError);
                        break;
                    case "details of capacity for all residents":
                        VerifyBwFieldValidationErrorMessage(_detailsOfCapacityOfAllResidentsValidation, expectedError);
                        break;
                    default:
                        Assert.Fail($"{field} is not a valid field");
                        break;
                }
            }
        }


        public void VerifyListOfBars()
        {
            var bars = _apiCall.GetListOfBars(Destination, Property);
            var expectedListOfBars = new List<string>();
            foreach (var bar in bars)
            {
                expectedListOfBars.Add(bar.Name);
            }
            expectedListOfBars.Add("Other");
            var actualListOfBars = Driver.GetAllDropDownOptions(_affectedBarsDropDown);
            Assert.AreEqual(actualListOfBars, expectedListOfBars, $"Expected list of bars {expectedListOfBars} was not the same as actual {actualListOfBars}");
        }

        public void EnterRoadworksBarsAnswers(Table table)
        {
            foreach (var row in table.Rows)
            {
                var question = row["question"];
                var answer = row["answer"];

                switch (question.ToLower())
                {
                    case "affected bar":
                        Driver.SelectDropDownOption(_affectedBarsDropDown, GetAffectedBar(answer));
                        break;
                    case "name of the bar":
                        Driver.EnterText(_otherBarName, answer);
                        break;
                    case "how is bar affected":
                        Driver.SelectDropDownOption(_howBarsAffectedDrpDwn, answer);
                        break;
                    case "how is bar affected details":
                        Driver.EnterText(_howBarsAffectedDetails, answer);
                        break;
                    case "work affect board basis":
                        Driver.ClickPSelectOption(_workAffectBoardBasis, answer);
                        break;
                    case "work affect board basis details":
                        Driver.EnterText(_workAffectBoardBasisDetails, answer);
                        break;
                    case "hotel providing alternative arrangements":
                        Driver.ClickPSelectOption(_workProvidingAlternArrangements, answer);
                        break;
                    case "details of alternative arrangements":
                        Driver.EnterText(_detailsOfAlternativeArrangements, answer);
                        break;
                    case "does the alternative have capacity for all residents":
                        Driver.ClickPSelectOption(_capacityOfAllResidents, answer);
                        break;
                    case "details of capacity for all residents":
                        Driver.EnterText(_detailsOfCapacityOfAllResidents, answer);
                        break;
                    default:
                        Assert.Fail($"{question} is not a valid field");
                        break;
                }
            }
        }

        private string GetAffectedBar(string bar)
        {
            string barName;
            if (bar.ToLower() != "other")
            {
                var barNumber = bar.GetIntOnly() - 1;
                var listOfBars = _apiCall.GetListOfBars(Destination, Property);
                barName = listOfBars[barNumber].Name;
            }
            else
            {
                barName = bar;
            }
            return barName;
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
                        element = _otherBarName;
                        break;
                    case "how is bar affected details":
                        element = _howBarsAffectedDetails;
                        break;
                    case "work affect board basis details":
                        element = _workAffectBoardBasisDetails;
                        break;
                    case "hotel providing alternative arrangements":
                        element = _workProvidingAlternArrangements;
                        break;
                    case "details of alternative arrangements":
                        element = _detailsOfAlternativeArrangements;
                        break;
                    case "does the alternative have capacity for all residents":
                        element = _capacityOfAllResidents;
                        break;
                    case "details of capacity for all residents":
                        element = _detailsOfCapacityOfAllResidents;
                        break;
                    default:
                        Assert.Fail($"{expectedField} is not a valid field");
                        break;
                }
                Assert.AreEqual(Driver.WaitForItem(element, 1), expectedState);
            }
        }

        public void VerifyBarsBtnEnabledDisabled(string btnName, string enabledDisabled)
        {
            VerifyElementState(enabledDisabled, _getAddRemoveBtn(btnName));
        }

        public void ClickBarsButton(string btnName)
        {
            Driver.ClickItem(_getAddRemoveBtn(btnName));
        }

        internal By _getAddRemoveBtn(string btnName)
        {
            By btnElement = null;
            switch (btnName.ToLower())
            {
                case "remove this bar":
                    btnElement = _removeThisBar;
                    break;
                case "add another bar":
                    btnElement = _addThisBar;
                    break;
                default:
                    Assert.Fail($"{btnName} is not a valid button");
                    break;
            }
            return btnElement;
        }
    
        public void GetListOfBars()
        {
            ListOfBars = _apiCall   // get list of all bars for the given property
                .GetListOfBars(Destination, Property)
                .Select(r => r.Name)
                .ToList();
            ListOfBars.Add("Other");
        }

        public void VerifyBarsNotPresent(string excludesIncludes, string bar)
        {
            var barList = bar.ConvertStringIntoList(); // converts expected bars to a List
            var barIndex = barList           // get the indexes to ignore
                .Select(StringExtensions.GetIntOnly)
                .ToList();
            var listOfActualBars = Driver.GetAllDropDownOptions(_affectedBarsDropDown);
            var excludeBars = barIndex.Select(i => ListOfBars[i - 1]).ToList();
            if (excludesIncludes.ToLower() == "excludes")
            {
                for (var i = 0; i < excludeBars.Count(); i++)
                {
                    Assert.True(listOfActualBars.Contains(excludeBars[i]) == false);
                }
            }
            else if (excludesIncludes.ToLower() == "includes")
            {
                for (var i = 0; i < excludeBars.Count(); i++)
                {
                    Assert.True(listOfActualBars.Contains(excludeBars[i]));
                }
            }
        }
    }

}

