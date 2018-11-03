using System;
using J2BIOverseasOps.Extensions;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Pages.BuildingWork.NewBuild
{
    internal class KeyStagesPage : BuildingWorkCommon
    {
        public KeyStagesPage(IWebDriver driver, ILog log, IRunData rundata) : base(driver, log, rundata)
        {
        }
        private int _index = 0;
        private  By BuildingItemDrpown => By.XPath($"//p-dropdown[@id='buildingItem_{_index}']");
        private By BuildingItemDrpownValidation => By.XPath($"//p-message[@id='buildingItem-validation_{_index}']");
        private By PlannedCompletionDate => By.XPath($"//p-calendar[@id='plannedCompletionDate_{_index}']");
        private By PlannedCompletionDateValidation => By.XPath($"//p-message[@id='plannedCompletionDate-validation_{_index}']");
        private By CurrentStatusDrpDwn => By.XPath($"//p-dropdown[@id='currentStatus_{_index}']");
        private By CurrentStatusDrpDwnValidation => By.XPath($"//p-message[@id='currentStatus-validation_{_index}']");
        private By OtherName => By.XPath($"//input[@id='otherName_{_index}']");
        private By OtherNameValidation => By.XPath($"//p-message[@id='otherName-validation_{_index}']");
        private By NewCompletionDate => By.XPath($"//p-calendar[@id='newCompletionDate_{_index}']");
        private By NewCompletionDateValidation => By.XPath($"//p-message[@id='newCompletionDate-validation_{_index}']");
        private By CommentsOnTheDelay =>By.XPath($"//textarea[@id='commentsOnTheDelay_{_index}']");
        private By CommentsOnTheDelayValidation => By.XPath($"//p-message[@id='commentsOnTheDelay-validation_{_index}']");
        private By RemoveBwRecord => By.XPath($"//button[@id='removeBtn_{_index}']");
        private By AddBwRecord => By.XPath($"//button[@id='addBtn_{_index}']");

        public void VerifyBuildingItemAndCurrentStatusFieldData(Table table)
        {
            foreach (var row in table.Rows)
            {
                var question = row["field"];
                var answer = row["value"].ConvertStringIntoList();
                switch (question.ToLower())
                {
                    case "building item":
                        var actualList = Driver.GetAllDropDownOptions(BuildingItemDrpown);
                        Assert.AreEqual(actualList, answer,"Unable to verify the drop down options for the field building item");
                        break;
                    case "current status":
                        var actualLis = Driver.GetAllDropDownOptions(CurrentStatusDrpDwn);
                        Assert.AreEqual(actualLis, answer, "Unable to verify the drop down options for the field current status");
                        break;
                    default:
                        Assert.Fail($"{question} is not a valid field");
                        break;
                }
            }
        }

        public void EnterKeyStagesAnswer(Table table)
        {
            foreach (var row in table.Rows)
            {
                var question = row["question"];
                var answer = row["answer"];
                _index = Convert.ToInt32(row["item number"]);
                switch (question.ToLower())
                {
                    case "building item":
                        Driver.SelectDropDownOption(BuildingItemDrpown,answer);
                        break;
                    case "planned completion date":
                        SelectBwDateFromCalendar(PlannedCompletionDate, answer);
                        break;
                    case "current status":
                        Driver.SelectDropDownOption(CurrentStatusDrpDwn, answer);
                        break;
                    case "name":
                        Driver.EnterText(OtherName, answer);
                        break;
                    case "new completion date":
                        SelectBwDateFromCalendar(NewCompletionDate, answer);
                        break;
                    case "comments on the delay":
                        Driver.EnterText(CommentsOnTheDelay, answer);
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
                _index = Convert.ToInt32(row["item number"]);
                By element = null;
                switch (expectedField.ToLower())
                {
                    case "building item":
                       element =BuildingItemDrpown;
                        break;
                    case "planned completion date":
                        element = PlannedCompletionDate;
                        break;
                    case "current status":
                        element = CurrentStatusDrpDwn;
                        break;
                    case "name":
                        element = OtherName;
                        break;
                    case "new completion date":
                        element = NewCompletionDate;
                        break;
                    case "comments on the delay":
                        element =CommentsOnTheDelay;
                        break;
                    default:
                        Assert.Fail($"{expectedField} is not a valid field");
                        break;
                }
                Assert.AreEqual(Driver.WaitForItem(element, 1), expectedState);
            }
        }

        public void VerifyNewBuildValidationErrorMessage(Table table)
        {
            foreach (var row in table.Rows)
            {
                var field = row["field"];
                var expectedError = row["error"];
                switch (field.ToLower())
                {
                    case "building item":
                        VerifyBwFieldValidationErrorMessage(BuildingItemDrpownValidation, expectedError); 
                        break;
                    case "planned completion date":
                        VerifyBwFieldValidationErrorMessage (PlannedCompletionDateValidation, expectedError);
                        break;
                    case "current status":
                        VerifyBwFieldValidationErrorMessage(CurrentStatusDrpDwnValidation, expectedError);
                        break;
                    case "name":
                        VerifyBwFieldValidationErrorMessage(OtherNameValidation, expectedError);
                        break;
                    case "new completion date":
                        VerifyBwFieldValidationErrorMessage(NewCompletionDateValidation, expectedError);
                        break;
                    case "comments on the delay":
                        VerifyBwFieldValidationErrorMessage(CommentsOnTheDelayValidation, expectedError);
                        break;
                    default:
                        Assert.Fail($"{field} is not a valid field");
                        return;
                }
            }
        }

        public void ClickAddRemoveButton(string addremove, int index)
        {
            _index = index;
            By buttonToClick = null;
            switch (addremove.ToLower())
            {
                case "add":
                    buttonToClick = AddBwRecord;
                    break;
                case "remove":
                    buttonToClick = RemoveBwRecord;
                    break;
                default:
                    Assert.Fail($"{addremove} is not a valid button");
                    break;
            }
            Driver.ClickItem(buttonToClick);
        }

        public void VerifyAnswers(Table table)
        {
            foreach (var row in table.Rows)
            {
                var field = row["question"];
                var expectedAnswer = row["expected_answer"];
                _index= Convert.ToInt32(row["item number"]);
                switch (field.ToLower())
                {
                    case "building item":
                      Driver.VerifySelectedDropDownOption(BuildingItemDrpown,expectedAnswer);
                        break;
                    case "planned completion date":
                        break;
                    case "current status":
                        Driver.VerifySelectedDropDownOption(CurrentStatusDrpDwn, expectedAnswer);
                        break;
                    case "name":
                        Driver.VerifyInputBoxText(OtherName, expectedAnswer);
                        break;
                    case "new completion date":
                        break;
                    case "comments on the delay":
                        Driver.VerifyInputBoxText(CommentsOnTheDelay, expectedAnswer);
                        break;
                    default:
                        Assert.Fail($"{field} is not a valid field");
                        return;
                }


            }


        }
    }



}