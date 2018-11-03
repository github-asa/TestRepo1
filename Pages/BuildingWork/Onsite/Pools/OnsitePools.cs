using System.Collections.Generic;
using System.Linq;
using J2BIOverseasOps.Extensions;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Pages.BuildingWork.Onsite.Pools
{
    internal class OnsitePools : BuildingWorkCommon
    {
        private readonly By _affectedPoolsMandatory =  By.XPath("//p-message[@id='affectedPools-validation']//span[2]");
        private readonly By _affectedPoolsDropDown = By.XPath("//p-dropdown[@id='affectedPools']");

        //other pools
        private readonly By _isThisNewFacility = By.XPath("//p-selectbutton[@id='isThisFacilityNew']");
        private readonly By _isThisNewFacilityMandatory = By.XPath("//p-message[@id='isThisFacilityNew-validation']//span[2]");
        private readonly By _otherPoolName = By.XPath("//input[@id='otherPoolName']");
        private readonly By _otherPoolNameMandatory =  By.XPath("//p-message[@id='otherPoolName-validation']//span[2]");

        // is the work
        private readonly By _typeOfWork = By.XPath("//p-selectbutton[@id='typeOfWork']");
        private readonly By _typeOfWorkValidation = By.XPath("//p-message[@id='typeOfWork-validation']//span[2]");

        // details of work
        private readonly By _detailsOfWork = By.XPath("//textarea[@id='detailsAboutTheWork']");
        private readonly By _detailsOfWorkValidation = By.XPath("//p-message[@id='detailsAboutTheWork-validation']");

        //how pools affected
        private readonly By _howPoolsAffectedDrpDwn = By.XPath("//p-dropdown[@id='howIsThePoolAffected']");
        private readonly By _howPoolsAffectedValidation =  By.XPath("//p-message[@id='howIsThePoolAffected-validation']//span[2]");
        private readonly By _howPoolsAffectedDetails = By.XPath("//textarea[@id='moreDetailsTxt']");
        private readonly By _howPoolsAffectedDetailsValidation =  By.XPath("//p-message[@id='moreDetailsTxt-validation']");

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
        private readonly By _removeThisPool = By.XPath("//p-button[@id='btnRemoveThisPool']//button");
        private readonly By _addThisPool = By.XPath("//p-button[@id='addAnotherPool']//button");

        private readonly ApiCalls _apiCall;


        public OnsitePools(IWebDriver driver, ILog log, IRunData rundata) : base(driver, log, rundata)
        {
            _apiCall = new ApiCalls(rundata);

        }

        public void VerifyPoolsMandatoryMessage(Table table)
        {
            foreach (var row in table.Rows)
            {
                var field = row["field"];
                var expectedError = row["error"];

                switch (field.ToLower())
                {
                    case "select affected pools":
                        VerifyBwFieldValidationErrorMessage(_affectedPoolsMandatory, expectedError);
                        break;
                    case "is this a new facility":
                        VerifyBwFieldValidationErrorMessage(_isThisNewFacilityMandatory, expectedError);
                        break;
                    case "name of the facility":
                        VerifyBwFieldValidationErrorMessage(_otherPoolNameMandatory, expectedError);
                        break;
                    case "is the pool work":
                        VerifyBwFieldValidationErrorMessage(_typeOfWorkValidation, expectedError);
                        break;
                    case "details of work":
                        VerifyBwFieldValidationErrorMessage(_detailsOfWorkValidation, expectedError);
                        break;
                    case "how is pool affected":
                        VerifyBwFieldValidationErrorMessage(_howPoolsAffectedValidation, expectedError);
                        break;
                    case "how is pool affected details":
                        VerifyBwFieldValidationErrorMessage(_howPoolsAffectedDetailsValidation, expectedError);
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


        public void VerifyListOfPools()
        {
            var pools = _apiCall.GetListOfPools(Destination, Property);
            var expectedListOfPools = new List<string>();
            foreach (var pool in pools)
            {
                expectedListOfPools.Add(pool.Name);
            }
            expectedListOfPools.Add("Other");
            var actualListOfPools = Driver.GetAllDropDownOptions(_affectedPoolsDropDown);
            Assert.AreEqual(actualListOfPools, expectedListOfPools, $"Expected list of pools {expectedListOfPools} was not the same as actual {actualListOfPools}");
        }

        public void VerifyListHowPoolsAffected(Table table)
        {
            var actualList = Driver.GetAllDropDownOptions(_howPoolsAffectedDrpDwn);
            var expectedList = table.Rows.ToColumnList("how affected");
            Assert.AreEqual(actualList, expectedList, $"Expected list  {expectedList} was not the same as actual {actualList}");
        }

        public void EnterOnSitePoolsAnswers(Table table)
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
                    case "affected pool":
                        Driver.SelectDropDownOption(_affectedPoolsDropDown, GetAffectedPool(answer));
                        break;
                    case "is the pool work":
                        Driver.ClickPSelectOption(_typeOfWork, answer);
                        break;
                    case "details of work":
                        Driver.EnterText(_detailsOfWork, answer);
                        break;
                    case "how is pool affected":
                        Driver.SelectDropDownOption(_howPoolsAffectedDrpDwn, answer);
                        break;
                    case "how is pool affected details":
                        Driver.EnterText(_howPoolsAffectedDetails, answer);
                        break;
                    case "name of the pool":
                        Driver.EnterText(_otherPoolName, answer);
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

        public void VerifyOnSitePoolsAnswers(Table table)
        {
            foreach (var row in table.Rows)
            {
                var question = row["question"];
                var answer = row["answer"];

                switch (question.ToLower())
                {
                    case "is this a new facility":
                        Driver.VerifySingleSelectedPOption(_isThisNewFacility, answer);
                        break;
                    case "affected pool":
                        Driver.VerifySelectedDropDownOption(_affectedPoolsDropDown, GetAffectedPool(answer));
                        break;
                    case "is the pool work":
                        Driver.VerifyMultiSelectedPOption(_typeOfWork, answer.ConvertStringIntoList());
                        break;
                    case "details of work":
                        Driver.VerifyInputBoxText(_detailsOfWork, answer);
                        break;
                    case "how is pool affected":
                        Driver.SelectDropDownOption(_howPoolsAffectedDrpDwn, answer);
                        break;
                    case "how is pool affected details":
                        Driver.VerifyInputBoxText(_howPoolsAffectedDetails, answer);
                        break;
                    case "name of the pool":
                        Driver.VerifyInputBoxText(_otherPoolName, answer);
                        break;
                    case "hotel providing alternative arrangements":
                        Driver.VerifySingleSelectedPOption(_workProvidingAlternArrangements, answer);
                        break;
                    case "details of alternative arrangements":
                        Driver.VerifyInputBoxText(_detailsOfAlternativeArrangements, answer);
                        break;
                    case "does the alternative have capacity for all residents":
                        Driver.VerifySingleSelectedPOption(_capacityOfAllResidents, answer);
                        break;
                    case "details of capacity for all residents":
                        Driver.VerifyInputBoxText(_detailsOfCapacityOfAllResidents, answer);
                        break;
                    default:
                        Assert.Fail($"{question} is not a valid field");
                        break;
                }
            }
        }
        
        private string GetAffectedPool(string pool)
        {
            var poolName = "";
            if (pool.ToLower() != "other")
            {
                var poolNumber = pool.GetIntOnly() - 1;
                var listOfPools = _apiCall.GetListOfPools(Destination, Property);
                poolName = listOfPools[poolNumber].Name;
            }
            else
            {
                poolName = pool;
            }

            return poolName;
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
                        element = _otherPoolName;
                        break;
                    case "how is pool affected details":
                        element = _howPoolsAffectedDetails;
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
        

        public void VerifyPoolsBtnEnabledDisabled(string btnName, string enabledDisabled)
        {
            VerifyElementState(enabledDisabled, _getAddRemoveBtn(btnName));
        }

        public void ClickPoolsButton(string btnName)
        {
            Driver.ClickItem(_getAddRemoveBtn(btnName));
        }

        internal By _getAddRemoveBtn(string btnName)
        {
            By btnElement = null;
            switch (btnName.ToLower())
            {
                case "remove this pool":
                    btnElement = _removeThisPool;
                    break;
                case "add another pool":
                    btnElement = _addThisPool;
                    break;
                default:
                    Assert.Fail($"{btnName} is not a valid button");
                    break;
            }
            return btnElement;
        }

        public void GetListOfPools()
        {
            ListOfPools = _apiCall   // get list of all restaurants for the given property
                .GetListOfPools(Destination, Property)
                .Select(r => r.Name)
                .ToList();
            ListOfPools.Add("Other");
        }

        public void VerifyPoolsNotPresent(string excludesIncludes, string pool)
        {
            var poolList = pool.ConvertStringIntoList(); // converyts expected restaurants to a List
            var poolIndex = poolList           // get the indexes to ignore
                .Select(StringExtensions.GetIntOnly)
                .ToList();
            var listOfActualPools = Driver.GetAllDropDownOptions(_affectedPoolsDropDown);
            var excludedPools = poolIndex.Select(i => ListOfPools[i - 1]).ToList();
            if (excludesIncludes.ToLower() == "excludes")
            {
                for (var i = 0; i < excludedPools.Count(); i++)
                {
                    Assert.True(listOfActualPools.Contains(excludedPools[i]) == false);
                }
            }
            else if (excludesIncludes.ToLower() == "includes")
            {
                for (var i = 0; i < excludedPools.Count(); i++)
                {
                    Assert.True(listOfActualPools.Contains(excludedPools[i]));
                }
            }
        }
    }

}




