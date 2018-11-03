using J2BIOverseasOps.Extensions;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Pages.BuildingWork.Onsite.OtherPublicAreas
{
     internal class OnsiteOtherPublicAreasPage:BuildingWorkCommon
    {
        private readonly By _affectedOtherPublicAreasMandatory = By.XPath("//p-message[@id='otherPublicArea-validation']//span[2]");
        private readonly By _affectedOtherPublicAreasDropDown = By.XPath("//p-dropdown[@id='otherPublicArea']");
       
        //other public areas
        private readonly By _isThisNewOtherPublicArea = By.XPath("//p-selectbutton[@id='isThisFacilityNew']");
        private readonly By _isThisNewOtherPublicAreaMandatory = By.XPath("//p-message[@id='isThisFacilityNew-validation']//span[2]"); 
        private readonly By _otherOtherPublicAreaName = By.XPath("//input[@id='otherAreaName']");
        private readonly By _otherOtherPublicAreaNameMandatory = By.XPath("//p-message[@id='otherAreaName-validation']//span[2]");
        // is the work
        private readonly By _typeOfWork = By.XPath("//p-selectbutton[@id='typeOfWork']");
        private readonly By _typeOfWorkValidation = By.XPath("//p-message[@id='typeOfWork-validation']//span[2]");

        // details of work
        private readonly By _detailsOfWork = By.XPath("//textarea[@id='detailsAboutTheWork']");
        private readonly By _detailsOfWorkValidation = By.XPath("//p-message[@id='detailsAboutTheWork-validation']");

        // impact on customer
        private readonly By _impactOnCustomers = By.XPath("//textarea[@id='possibleImpactOnCustomer']");
        private readonly By _impactOnCustomersValidation=By.XPath("//p-message[@id='possibleImpactOnCustomer-validation']");

        //alternative or luggage assistance
        private readonly By _alternativeOrLuggageAssistance = By.XPath("//p-selectbutton[@id='isThereAnAlternative']");
        private readonly By _alternativeOrLuggageAssistanceValidation = By.XPath("//p-message[@id='isThereAnAlternative-validation']");

        private readonly By _detailsOfAlternativeOrLuggageAssistance = By.XPath("//textarea[@id='alternativeArrangementFurtherDetails']");
        private readonly By _detailsOfAlternativeOrLuggageAssistanceValidation = By.XPath("//p-message[@id='alternativeArrangementFurtherDetails-validation']");

        // Buttons  
        private readonly By _removeThisOtherPublicArea = By.XPath("//p-button[@id='removeThisOtherPublicArea']//button");
        private readonly By _addAnotherOtherPublicArea = By.XPath("//p-button[@id='addAnotherOtherPublicArea']//button");

        public OnsiteOtherPublicAreasPage(IWebDriver driver, ILog log, IRunData runData) : base(driver, log, runData)
        {
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
                    VerifyBwFieldValidationErrorMessage(_affectedOtherPublicAreasMandatory, expectedError);
                    break;
                case "is this a new facility":
                    VerifyBwFieldValidationErrorMessage(_isThisNewOtherPublicAreaMandatory, expectedError);
                    break;
                case "name of the facility":
                    VerifyBwFieldValidationErrorMessage(_otherOtherPublicAreaNameMandatory, expectedError);
                    break;
                case "is the public areas work":
                    VerifyBwFieldValidationErrorMessage(_typeOfWorkValidation, expectedError);
                    break;
                case "details of work":
                    VerifyBwFieldValidationErrorMessage(_detailsOfWorkValidation, expectedError);
                    break;
                case "possible impact on customers":
                     VerifyBwFieldValidationErrorMessage(_impactOnCustomersValidation, expectedError);
                     break;
                case "alternative or luggage assistance":
                    VerifyBwFieldValidationErrorMessage(_alternativeOrLuggageAssistanceValidation, expectedError);
                    break;
                case "details of alternative or luggage assistance":
                    VerifyBwFieldValidationErrorMessage(_detailsOfAlternativeOrLuggageAssistanceValidation, expectedError);
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

    public void EnterOnSiteOtherPublicAreasAnswers(Table table)
    {
        foreach (var row in table.Rows)
        {
            var question = row["question"];
            var answer = row["answer"];

            switch (question.ToLower())
            {
                case "is this a new facility":
                    Driver.ClickPSelectOption(_isThisNewOtherPublicArea, answer);
                    break;
                case "affected other public areas":
                    Driver.SelectDropDownOption(_affectedOtherPublicAreasDropDown, answer);
                    break;
                case "is the public area work":
                    Driver.ClickPSelectOption(_typeOfWork, answer);
                    break;
                case "details of work":
                    Driver.EnterText(_detailsOfWork, answer);
                    break;
                case "possible impact on customers":
                    Driver.EnterText(_impactOnCustomers, answer);
                    break;
                case "name of the facility":
                    Driver.EnterText(_otherOtherPublicAreaName, answer);
                    break;
                case "alternative or luggage assistance":
                    Driver.ClickPSelectOption(_alternativeOrLuggageAssistance, answer);
                    break;
                case "details of alternative or luggage assistance":
                    Driver.EnterText(_detailsOfAlternativeOrLuggageAssistance, answer);
                    break;
                default:
                    Assert.Fail($"{question} is not a valid field");
                    break;
            }
        }
    }

        public void VerifyOnSiteOtherPublicAreasAnswers(Table table)
        {
            foreach (var row in table.Rows)
            {
                var question = row["question"];
                var answer = row["answer"];

                switch (question.ToLower())
                {
                    case "is this a new facility":
                        Driver.VerifySingleSelectedPOption(_isThisNewOtherPublicArea, answer);
                        break;
                    case "affected other public areas":
                        Driver.VerifySelectedDropDownOption(_affectedOtherPublicAreasDropDown, answer);
                        break;
                    case "is the public area work":
                        Driver.VerifyMultiSelectedPOption(_typeOfWork, answer.ConvertStringIntoList());
                        break;
                    case "details of work":
                        Driver.VerifyInputBoxText(_detailsOfWork, answer);
                        break;
                    case "possible impact on customers":
                        Driver.VerifyInputBoxText(_impactOnCustomers, answer);
                        break;
                    case "name of the facility":
                        Driver.VerifyInputBoxText(_otherOtherPublicAreaName, answer);
                        break;
                    case "alternative or luggage assistance":
                        Driver.VerifySingleSelectedPOption(_alternativeOrLuggageAssistance, answer);
                        break;
                    case "details of alternative or luggage assistance":
                        Driver.VerifyInputBoxText(_detailsOfAlternativeOrLuggageAssistance, answer);
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
                    element = _isThisNewOtherPublicArea;
                    break;
                case "name of the facility":
                    element = _otherOtherPublicAreaName;
                    break;
                case "alternative or luggage assistance":
                    element = _alternativeOrLuggageAssistance;
                    break;
                case "details of alternative or luggage assistance":
                    element = _detailsOfAlternativeOrLuggageAssistance;
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

        public void VerifyOtherPublicAreaNotPresent(string excludesIncludes, string publicarea)
        {
                var excludeFacilityList = publicarea.ConvertStringIntoList(); // converts expected restaurants to a List
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

