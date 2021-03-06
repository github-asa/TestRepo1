﻿using J2BIOverseasOps.Extensions;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Pages.BuildingWork.Nearby.OverallImpact
{
    internal class NearbyOverallImpactPage : BuildingWorkCommon
    {
        //noise can be heard from public areas or rooms
        private readonly By _noiseCanBeHeard = By.XPath("//p-selectbutton[@id='canNoiseBeHeardFromPublicAreasOrRooms']");
        private readonly By _noiseCanBeHeardValidation = By.XPath("//p-message[@id='canNoiseBeHeardFromPublicAreasOrRooms-validation']");
        private readonly By _noiseImpactAssessment = By.XPath("//p-selectbutton[@id='noiseImpactAssessment']");
        private readonly By _noiseCanBeHeardImpactValidation = By.XPath("//p-message[@id='noiseImpactAssessment-validation']");

        //Are there any visual disturbances?
        private readonly By _visualDisturbances = By.XPath("//p-selectbutton[@id='areThereAnyVisualDisturbances']");
        private readonly By _visualDisturbancesValidation = By.XPath("//p-message[@id='areThereAnyVisualDisturbances-validation']");
        private readonly By _visualImpactAssessment = By.XPath("//p-selectbutton[@id='visualImpactAssessment']");
        private readonly By _visualDisturbancesImpactValidation = By.XPath("//p-message[@id='visualImpactAssessment-validation']");

        //any Tools or Machinery are in use due to the building work
        private readonly By _anyToolsMachineryUsed = By.XPath("//p-selectbutton[@id='areToolsAndMachineryInPublicAreas']");
        private readonly By _anyToolsMachineryUsedValidation = By.XPath("//p-message[@id='areToolsAndMachineryInPublicAreas-validation']");
        private readonly By _anyToolsMachineryUsedImpact = By.XPath("//p-selectbutton[@id='toolsAndMachineryImpactAssessment']");
        private readonly By _anyToolsMachineryUsedImpactValidation = By.XPath("//p-message[@id='toolsAndMachineryImpactAssessment-validation']");

        //Dust Levels
        private readonly By _dustBeingCreated = By.XPath("//p-selectbutton[@id='isAnyDustFromTheWorksPresent']");
        private readonly By _dustBeingCreatedValidation = By.XPath("//p-message[@id='isAnyDustFromTheWorksPresent-validation']");
        private readonly By _dustBeingCreatedImpact = By.XPath("//p-selectbutton[@id='dustImpactAssessment']");
        private readonly By _dustBeingCreatedImpactValidation = By.XPath("//p-message[@id='dustImpactAssessment-validation']");


        //Smell
        private readonly By _smellBeingCreated = By.XPath("//p-selectbutton[@id='isTheWorkCreatingAnySmell']");
        private readonly By _smellBeingCreatedValidation = By.XPath("//p-message[@id='isTheWorkCreatingAnySmell-validation']");
        private readonly By _smellBeingCreatedImpact = By.XPath("//p-selectbutton[@id='smellImpactAssessment']");
        private readonly By _smellBeingCreatedImpactValidation = By.XPath("//p-message[@id='smellImpactAssessment-validation']");

        //Customer Feedback
        private readonly By _customerComplaints = By.XPath("//p-selectbutton[@id='haveThereBeenAnyCustomerComplaints']");
        private readonly By _customerComplaintsValidation = By.XPath("//p-message[@id='haveThereBeenAnyCustomerComplaints-validation']");
        private readonly By _customerComplaintsImpact = By.XPath("//p-selectbutton[@id='customerComplaintsImpactAssessment']");
        private readonly By _customerComplaintsImpactValidation = By.XPath("//p-message[@id='customerComplaintsImpactAssessment-validation']");

        public NearbyOverallImpactPage(IWebDriver driver, ILog log, IRunData rundata) : base(driver, log, rundata)
        { }

        public void VerifyNearbyMandatoryMessage(Table table)
        {
            foreach (var row in table.Rows)
            {
                var field = row["field"];
                var expectedError = row["error"];

                switch (field.ToLower())
                {
                    case "building work noise can be heard from public areas or rooms":
                        VerifyBwFieldValidationErrorMessage(_noiseCanBeHeardValidation, expectedError);
                        break;
                    case "building work noise can be heard from public areas or rooms impact":
                        VerifyBwFieldValidationErrorMessage(_noiseCanBeHeardImpactValidation, expectedError);
                        break;
                    case "there is visual disturbance due to the building work":
                        VerifyBwFieldValidationErrorMessage(_visualDisturbancesValidation, expectedError);
                        break;
                    case "there is visual disturbance due to the building work impact":
                        VerifyBwFieldValidationErrorMessage(_visualDisturbancesImpactValidation, expectedError);
                        break;
                    case "any tools or machinery are in use due to the building work":
                        VerifyBwFieldValidationErrorMessage(_anyToolsMachineryUsedValidation, expectedError);
                        break;
                    case "any tools or machinery are in use due to the building work impact":
                        VerifyBwFieldValidationErrorMessage(_anyToolsMachineryUsedImpactValidation, expectedError);
                        break;
                    case "dust is being created by the building work":
                        VerifyBwFieldValidationErrorMessage(_dustBeingCreatedValidation, expectedError);
                        break;
                    case "dust is being created by the building work impact":
                        VerifyBwFieldValidationErrorMessage(_dustBeingCreatedImpactValidation, expectedError);
                        break;
                    case "a smell is being created due to the building work":
                        VerifyBwFieldValidationErrorMessage(_smellBeingCreatedValidation, expectedError);
                        break;
                    case "a smell is being created due to the building work impact":
                        VerifyBwFieldValidationErrorMessage(_smellBeingCreatedImpactValidation, expectedError);
                        break;
                    case "customer complaints have been caused by the building work":
                        VerifyBwFieldValidationErrorMessage(_customerComplaintsValidation, expectedError);
                        break;
                    case "customer complaints have been caused by the building work impact":
                        VerifyBwFieldValidationErrorMessage(_customerComplaintsImpactValidation, expectedError);
                        break;
                    default:
                        Assert.Fail($"{field} is not a valid field");
                        break;
                }
            }
        }

        public void EnterOverallImpactQAnswers(Table table)
        {
            foreach (var row in table.Rows)
            {
                var question = row["question"];
                var answer = row["answer"];

                switch (question.ToLower())
                {
                    case "building work noise can be heard from public areas or rooms":
                        Driver.ClickPSelectOption(_noiseCanBeHeard, answer);
                        break;
                    case "there is visual disturbance due to the building work":
                        Driver.ClickPSelectOption(_visualDisturbances, answer);
                        break;
                    case "any tools or machinery are in use due to the building work":
                        Driver.ClickPSelectOption(_anyToolsMachineryUsed, answer);
                        break;
                    case "dust is being created by the building work":
                        Driver.ClickPSelectOption(_dustBeingCreated, answer);
                        break;
                    case "a smell is being created due to the building work":
                        Driver.ClickPSelectOption(_smellBeingCreated, answer);
                        break;
                    case "customer complaints have been caused by the building work":
                        Driver.ClickPSelectOption(_customerComplaints, answer);
                        break;
                    default:
                        Assert.Fail($"{question} is not a valid field");
                        break;
                }
            }
        }

        public void VerifyPselectOptionDisplayedOrNot(string displayedOrNot, Table table)
        {
            var expectedState = displayedOrNot == "displayed";
            foreach (var row in table.Rows)
            {
                var question = row["question"];
                var option = row["option"];
                By element = null;
                switch (question.ToLower())
                {
                    case "building work noise can be heard from public areas or rooms":
                        element = _noiseImpactAssessment;
                        break;
                    case "there is visual disturbance due to the building work":
                        element = _visualImpactAssessment;
                        break;
                    case "any tools or machinery are in use due to the building work":
                        element = _anyToolsMachineryUsedImpact;
                        break;
                    case "dust is being created by the building work":
                        element = _dustBeingCreatedImpact;
                        break;
                    case "a smell is being created due to the building work":
                        element = _smellBeingCreatedImpact;
                        break;
                    case "customer complaints have been caused by the building work":
                        element = _customerComplaintsImpact;
                        break;
                    default:
                        Assert.Fail($"{question} is not a valid field");
                        break;
                }

                if (expectedState == false)
                {
                    Assert.AreEqual(false, Driver.WaitForItem(element, 1), "Was able to find overall impact element, while expecting it not to be there ");
                }
                else
                {
                    Assert.AreEqual(true, Driver.IsPOptionDisplayed(element, option));
                }
            }
        }

        public void VerifyCanSelectPOption(Table table)
        {
            foreach (var row in table.Rows)
            {
                var question = row["question"];
                var option = row["option"];
                By element = null;
                switch (question.ToLower())
                {
                    case "building work noise can be heard from public areas or rooms":
                        element = _noiseImpactAssessment;
                        break;
                    case "there is visual disturbance due to the building work":
                        element = _visualImpactAssessment;
                        break;
                    case "any tools or machinery are in use due to the building work":
                        element = _anyToolsMachineryUsedImpact;
                        break;
                    case "dust is being created by the building work":
                        element = _dustBeingCreatedImpact;
                        break;
                    case "a smell is being created due to the building work":
                        element = _smellBeingCreatedImpact;
                        break;
                    case "customer complaints have been caused by the building work":
                        element = _customerComplaintsImpact;
                        break;
                    default:
                        Assert.Fail($"{question} is not a valid field");
                        break;
                }
                Driver.ClickPSelectOption(element, option);
            }
        }
    }
}

