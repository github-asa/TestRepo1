using System.Collections.Generic;
using System.Linq;
using J2BIOverseasOps.Extensions;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Pages.BuildingWork.Roadworks.Restaurants
{
    class RoadworksRestaurantsPage : BuildingWorkCommon
    {
        //affected restaurants
        private readonly By _affectedRestaurantsDropDown = By.XPath("//p-dropdown[@id='affectedRestaurants']");
        private readonly By _affectedRestaurantsMandatory = By.XPath("//p-message[@id='affectedRestaurants-validation']//span[2]");

        //other restaurants
        private readonly By _otherRestaurantName = By.XPath("//input[@id='otherRestaurantName']");
        private readonly By _otherRestaurantNameMandatory = By.XPath("//p-message[@id='otherRestaurantName-validation']//span[2]");

        //how is restaurant affected? | details
        private readonly By _howRestaurantsAffectedDrpDwn = By.XPath("//p-dropdown[@id='howIsRestaurantAffected']");
        private readonly By _howRestaurantsAffectedValidation = By.XPath("//p-message[@id='howIsRestaurantAffected-validation']//span[2]");
        private readonly By _howRestaurantsAffectedDetails = By.XPath("//textarea[@id='howIsRestaurantAffectedDetails']");
        private readonly By _howRestaurantsAffectedDetailsValidation = By.XPath("//p-message[@id='howIsRestaurantAffectedDetails-validation']");

        //will work affect board basis? | give details
        private readonly By _workAffectBoardBasis = By.XPath("//p-selectbutton[@id='doesItAffectBoardBasis']");
        private readonly By _workAffectBoardBasisValidation = By.XPath("//p-message[@id='doesItAffectBoardBasis-validation']");
        private readonly By _workAffectBoardBasisDetails = By.XPath("//textarea[@id='doesItAffectBoardBasisDetails']");
        private readonly By _workAffectBoardBasisDetailsValidation = By.XPath("//p-message[@id='doesItAffectBoardBasisDetails-validation']");

        //is hotel providing alternative arrangements? | give details
        private readonly By _workProvidingAlternArrangements = By.XPath("//p-selectbutton[@id='alternativeArrangements']");
        private readonly By _workProvidingAlternArrangementsValidation = By.XPath("//p-message[@id='alternativeArrangementsDetails-validation']");

        private readonly By _detailsOfAlternativeArrangements = By.XPath("//textarea[@id='alternativeArrangementsDetails']");
        private readonly By _detailsOfAlternativeArrangementsValidation = By.XPath("//p-message[@id='alternativeArrangementsDetails-validation']");

        private readonly By _capacityOfAllResidents = By.XPath("//p-selectbutton[@id='alternativeHasCapacityForAll']");
        private readonly By _capacityOfAllResidentsValidation = By.XPath("//p-message[@id='alternativeHasCapacityForAll-validation']");

        private readonly By _detailsOfCapacityOfAllResidents = By.XPath("//textarea[@id='alternativeHasCapacityForAllDetails']");
        private readonly By _detailsOfCapacityOfAllResidentsValidation = By.XPath("//p-message[@id='alternativeHasCapacityForAllDetails-validation']");

        // page buttons  
        private readonly By _removeThisRestaurant = By.XPath("//p-button[@id='btnRemoveThisRestaurant']//button");
        private readonly By _addThisRestaurant = By.XPath("//p-button[@id='btnAddAnotherRestaurant']//button");

        private readonly ApiCalls _apiCall;

        public RoadworksRestaurantsPage(IWebDriver driver, ILog log, IRunData rundata) : base(driver, log, rundata)
        {
            _apiCall = new ApiCalls(rundata);
        }

        public void VerifyRoadworksRestaurantsMandatoryMessage(Table table)
        {
            foreach (var row in table.Rows)
            {
                var field = row["field"];
                var expectedError = row["error"];

                switch (field.ToLower())
                {
                    case "select affected restaurants":
                        VerifyBwFieldValidationErrorMessage(_affectedRestaurantsMandatory, expectedError);
                        break;
                    case "name of the facility":
                        VerifyBwFieldValidationErrorMessage(_otherRestaurantNameMandatory, expectedError);
                        break;
                    case "how is restaurant affected":
                        VerifyBwFieldValidationErrorMessage(_howRestaurantsAffectedValidation, expectedError);
                        break;
                    case "how is restaurant affected details":
                        VerifyBwFieldValidationErrorMessage(_howRestaurantsAffectedDetailsValidation, expectedError);
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


        public void VerifyListOfRestaurants()
        {
            var restaurants = _apiCall.GetListOfRestaurants(Destination, Property);
            var expectedListOfRestaurants = new List<string>();
            foreach (var restaurant in restaurants)
            {
                expectedListOfRestaurants.Add(restaurant.Name);
            }
            expectedListOfRestaurants.Add("Other");
            var actualListOfRestaurants = Driver.GetAllDropDownOptions(_affectedRestaurantsDropDown);
            Assert.AreEqual(actualListOfRestaurants, expectedListOfRestaurants, $"Expected list of restaurants {expectedListOfRestaurants} was not the same as actual {actualListOfRestaurants}");
        }

        public void VerifyListHowRestaurantsAffected(Table table)
        {
            var actualList = Driver.GetAllDropDownOptions(_howRestaurantsAffectedDrpDwn);
            var expectedList = table.Rows.ToColumnList("how affected");
            Assert.AreEqual(actualList, expectedList, $"Expected list  {expectedList} was not the same as actual {actualList}");
        }


        public void EnterRoadworksRestaurantsAnswers(Table table)
        {
            foreach (var row in table.Rows)
            {
                var question = row["question"];
                var answer = row["answer"];

                switch (question.ToLower())
                {
                    case "affected restaurant":
                        Driver.SelectDropDownOption(_affectedRestaurantsDropDown, GetAffectedRestaurant(answer));
                        break;
                    case "how is restaurant affected":
                        Driver.SelectDropDownOption(_howRestaurantsAffectedDrpDwn, answer);
                        break;
                    case "how is restaurant affected details":
                        Driver.EnterText(_howRestaurantsAffectedDetails, answer);
                        break;
                    case "work affect board basis":
                        Driver.ClickPSelectOption(_workAffectBoardBasis, answer);
                        break;
                    case "work affect board basis details":
                        Driver.EnterText(_workAffectBoardBasisDetails, answer);
                        break;
                    case "name of the restaurant":
                        Driver.EnterText(_otherRestaurantName, answer);
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

        private string GetAffectedRestaurant(string restaurant)
        {
            string barName;
            if (restaurant.ToLower() != "other")
            {
                var barNumber = restaurant.GetIntOnly() - 1;
                var listOfRestaurants = _apiCall.GetListOfRestaurants(Destination, Property);
                barName = listOfRestaurants[barNumber].Name;
            }
            else
            {
                barName = restaurant;
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
                        element = _otherRestaurantName;
                        break;
                    case "how is restaurant affected details":
                        element = _howRestaurantsAffectedDetails;
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

        public void VerifyRestaurantsBtnEnabledDisabled(string btnName, string enabledDisabled)
        {
            VerifyElementState(enabledDisabled, _getAddRemoveBtn(btnName));
        }

        public void ClickRestaurantsButton(string btnName)
        {
            Driver.ClickItem(_getAddRemoveBtn(btnName));
        }


        internal By _getAddRemoveBtn(string btnName)
        {
            By btnElement = null;
            switch (btnName.ToLower())
            {
                case "remove this restaurant":
                    btnElement = _removeThisRestaurant;
                    break;
                case "add another restaurant":
                    btnElement = _addThisRestaurant;
                    break;
                default:
                    Assert.Fail($"{btnName} is not a valid button");
                    break;
            }
            return btnElement;
        }

        public void GetListOfRestaurants()
        {
            ListOfRestaurants = _apiCall   // get list of all restaurants for the given property
                .GetListOfRestaurants(Destination, Property)
                .Select(r => r.Name)
                .ToList();
            ListOfRestaurants.Add("Other");
        }

        public void VerifyRestaurantsNotPresent(string excludesIncludes, string restaurant)
        {
            var barList = restaurant.ConvertStringIntoList(); // converts expected restaurants to a List
            var barIndex = barList           // get the indexes to ignore
                .Select(StringExtensions.GetIntOnly)
                .ToList();
            var listOfActualRestaurants = Driver.GetAllDropDownOptions(_affectedRestaurantsDropDown);
            var excludeRestaurants = barIndex.Select(i => ListOfRestaurants[i - 1]).ToList();
            if (excludesIncludes.ToLower() == "excludes")
            {
                for (var i = 0; i < excludeRestaurants.Count(); i++)
                {
                    Assert.True(listOfActualRestaurants.Contains(excludeRestaurants[i]) == false);
                }
            }
            else if (excludesIncludes.ToLower() == "includes")
            {
                for (var i = 0; i < excludeRestaurants.Count(); i++)
                {
                    Assert.True(listOfActualRestaurants.Contains(excludeRestaurants[i]));
                }
            }
        }
    }
}
