using System;
using System.Collections.Generic;
using System.Linq;
using J2BIOverseasOps.Extensions;
using log4net;
using Newtonsoft.Json.Bson;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Pages.FormManagement
{
    internal class AddEditQuestionElement : CommonFormMgmtElements
    {
        private const string InactiveQuestion = "isInactive";
        private readonly By _addNewQValidationError = By.XPath("//p-message[@severity='error']");
        private readonly By _btnCancelQuestion = By.XPath("//button[@id='cancelBtn']");
        private readonly By _btnSaveQuestion = By.XPath("//button[@id='saveBtn']");
        private readonly By _editAddQPanel = By.Id("questionEditor");
        private readonly By _qCharLimit = By.XPath("//input[@id='questionMaxLength']");
        private readonly By _qDrpDwnOptionsAddBtn = By.CssSelector("[id='optionsPod'] [id*='addBtn']");
        private readonly By _qDrpDwnOptionsInp = By.XPath("//*[@id='optionsPod']");
        private readonly By _qDrpDwnOptionsRemoveBtn = By.CssSelector("[id='optionsPod'] [id*='removeBtn']");
        private readonly By _qIsActiveTickBox = By.CssSelector("[id*='activeCol'] p-checkbox");
        private readonly By _triggers = By.CssSelector("[id*=option_]");
        private readonly By _qMandatoryTickBox = By.XPath("//p-checkbox[@id='questionEditorIsRequired']");
        private readonly By _qTextField = By.Id("questionText");
        private readonly By _helpTextField = By.Id("helpText");
        private readonly By _qTypeDrpDwn = By.XPath("//p-dropdown[@id='questionType']");
        private readonly By _questionOptions = By.CssSelector("[id*=questionOption]");
        private readonly By _scrollableChildTable = By.XPath("//div[@class='ui-table-scrollable-body']");
        private readonly By _qHeader = By.CssSelector("#questionEditor h5 strong");
        private readonly By _qTip = By.CssSelector("#questionEditor .ui-float-label label");

        private readonly By _viewQComponentTitle = By.XPath("//*[@id='questionEditor']//strong");

        public AddEditQuestionElement(IWebDriver driver, ILog log) : base(driver, log)
        {
        }

        public void VerifyAddNewQElementDisplayed()
        {
            Assert.True(Driver.WaitForItem(_editAddQPanel, 5),
                "Could not verify add new question component displayed");
        }

        public void VerifyQElementTitle(string expectedTitle)
        {
            var actualTitle = Driver.GetText(_viewQComponentTitle);
            Assert.AreEqual(actualTitle, expectedTitle,
                $"Expected Title {expectedTitle} was not equal to Actual Title {actualTitle}");
        }

        public void AddNewQuestions(Table table)
        {
            foreach (var row in table.Rows)
            {
                ClickAddNewQBtn();
                VerifyAddNewQElementDisplayed();
                var questionType = row["qtype"];
                if (questionType == string.Empty)
                {
                    Assert.Fail("questionType is Empty");
                }

                var qtext = row["qtext"];            

                Log.Debug($"Creating a question of type {questionType}");
                switch (questionType)
                {
                    case "Single Line Text":
                        CreateLineTextQuestion(row);
                        break;
                    case "Multi Line Text":
                        CreateLineTextQuestion(row);
                        break;
                    case "Drop Down List (Single Choice)":
                        CreateDrpDwnListQuestion(row);
                        break;
                    case "Drop Down List (Multi Choice)":
                        CreateDrpDwnListQuestion(row);
                        break;
                    case "Radio Button":
                        CreateRadioButtonQuestion(row);
                        break;
                    case "Description":
                        CreateDescriptionInput(row);
                        break;
                    case "Numerical Input":
                        CreateNumericalInputQuestion(row);
                        break;
                    default:
                        SelectQType(questionType);
                        EnterQuestionText(qtext);
                        var isMandatory = row["mandatory"];
                        SelectMandatoryTickBox(isMandatory);
                        if (row.ContainsKey("helptext") && !string.IsNullOrWhiteSpace(row["helptext"]))
                        {
                            var helptext = row["helptext"];
                            EnterHelpText(helptext);
                        }
                        break;
                }

                Driver.ClickItem(_btnSaveQuestion);
                Driver.WaitUntilElementNotDisplayed(_editAddQPanel);
            }
        }

        private void CreateDescriptionInput(TableRow row)
        {
            var questionType = row["qtype"];
            var qtext = row["qtext"];
            SelectQType(questionType);
            EnterQuestionText(qtext);
            if (row.ContainsKey("helptext") && !string.IsNullOrWhiteSpace(row["helptext"]))
            {
                var helptext = row["helptext"];
                EnterHelpText(helptext);
            }
        }

        public void UpdateExistingQuestions(Table table)
        {
            foreach (var row in table.Rows)
            {
                var qtext = row["qtext"];
                ClickEditQButton(qtext);
                VerifyAddNewQElementDisplayed();

                if (row.ContainsKey("update_mandatory") && !string.IsNullOrWhiteSpace(row["update_mandatory"]))
                {
                    var updateIsMandatory = row["update_mandatory"];
                    SelectMandatoryTickBox(updateIsMandatory);
                }

                if (row.ContainsKey("update_qtext") && !string.IsNullOrWhiteSpace(row["update_qtext"]))
                {
                    var updateQText = row["update_qtext"];
                    Driver.Clear(_qTextField);
                    EnterQuestionText(updateQText);
                }

                if (row.ContainsKey("update_qtype") && !string.IsNullOrWhiteSpace(row["update_qtype"]))
                {
                    var updateQuestionType = row["update_qtype"];
                    Driver.SelectDropDownOption(_qTypeDrpDwn, updateQuestionType);
                }

                if (row.ContainsKey("update_char_limit") && !string.IsNullOrWhiteSpace(row["update_char_limit"]))
                {
                    var updateCharLimit = row["update_char_limit"];
                    EnterCharacterLimit(updateCharLimit);
                }

                if (row.ContainsKey("update_options") && !string.IsNullOrWhiteSpace(row["update_options"]))
                {
                    var updateDrpDwnOptions = row["update_options"];
                    CreateUpdateQuestionOptions(updateDrpDwnOptions);
                }

                if (row.ContainsKey("failedoptions") && !string.IsNullOrWhiteSpace(row["failedoptions"]))
                {
                    var updateFailedOptions = row["failedoptions"].ConvertStringIntoList();
                    SelectFailureTriggerForAnswers(updateFailedOptions);
                }

                if (row.ContainsKey("update_helptext") && !string.IsNullOrWhiteSpace(row["update_helptext"]))
                {
                    var helptext = row["update_helptext"];
                    EnterHelpText(helptext);
                }

                Driver.ClickItem(_btnSaveQuestion);
                Driver.WaitUntilElementNotDisplayed(_editAddQPanel);
            }
        }

        public void VerifyQTextBoxDisplayed()
        {
            Assert.True(Driver.WaitForItem(_qTextField, 2), "Could not verify Question text field as displayed");
        }

        public void VerifySaveButtonState(string visibility, string enableState)
        {
            VerifyElementVisibility(visibility, _btnSaveQuestion);
            VerifyElementState(enableState, _btnSaveQuestion);
        }

        public void VerifyCancelButtonState(string visibility, string enableState)
        {
            VerifyElementVisibility(visibility, _btnCancelQuestion);
            VerifyElementState(enableState, _btnCancelQuestion);
        }

        public void VerifyIsMandatoryCheckBoxState(string expectedState)
        {
            switch (expectedState)
            {
                case "Ticked":
                    Assert.True(Driver.IsCheckBoxTicked(_qMandatoryTickBox), "Is Mandatory Checkbox was not ticked");
                    return;
                case "Unticked":
                    Assert.True(!Driver.IsCheckBoxTicked(_qMandatoryTickBox),
                        "Is Mandatory Checkbox was ticked,while expecting to be unticked");
                    return;
                default:
                    Assert.Fail($"{expectedState} is not a valid tickbox state");
                    return;
            }
        }

        public void SelectQType(string questionType)
        {
            Driver.SelectDropDownOption(_qTypeDrpDwn, questionType);
        }

        public void ClickSaveQButton()
        {
            Driver.ClickItem(_btnSaveQuestion);
        }

        public void VerifyAddQValidationErrorText(string expectedText)
        {
            var actualText = Driver.GetText(_addNewQValidationError);
            Assert.AreEqual(actualText, expectedText, $"{expectedText} was not same as {actualText}");
        }

        public void ClickCancelQButton()
        {
            Driver.ClickItem(_btnCancelQuestion);
            Driver.WaitUntilElementNotDisplayed(_editAddQPanel);
        }

        public void VerifyRemoveDrpDwnOptionsBtnState(string visibility, string enableState)
        {
            var listOfAllRemoveBtns = Driver.FindElements(_qDrpDwnOptionsRemoveBtn);
            foreach (var button in listOfAllRemoveBtns)
            {
                VerifyElementVisibility(visibility, button);
                VerifyElementState(enableState, button);
            }
        }

        public void ChangeQActiveStatus(string qText, string activeStatus)
        {
            var questionElement = GetQuestionRow(qText);
            var isActive = questionElement.FindElement(_qIsActiveTickBox);
            Driver.TickUntickCheckBox(isActive, activeStatus);
        }

        public void VerifyQGreyedOut(string qText)
        {
            var qElement = GetQuestionRow(qText);
            Assert.True(qElement.GetAttribute("class").Contains(InactiveQuestion),
                "Unable to verify the question as Greyed Out");
        }

        public void VerifyQNotGreyedOut(string qText)
        {
            var qElement = GetQuestionRow(qText);
            Assert.True(!qElement.GetAttribute("class").Contains(InactiveQuestion),
                "Unable to verify the question as NOT Greyed Out");
        }

        public void VerifyQuestionPresentOnARow(string qText, int rowNumber)
        {
            var allRows = Driver.FindElements(By.XPath("//tbody//td/.."));
            var qElement = allRows[rowNumber - 1]; // get element at given row number
            Assert.True(Driver.GetText(qElement).Contains(qText),
                $"Could not find question {qText} at row number {rowNumber}");
        }

        public void SelectFailureTriggerForAnswers(List<string> failedOptions)
        {
            VerifyAddNewQElementDisplayed();

            var values = Driver.GetInputBoxValues(_questionOptions);

            foreach (var value in values)
            {
                if (failedOptions.Contains(value, StringComparer.OrdinalIgnoreCase))
                {
                    var i = values.IndexOf(value);
                    Driver.TickCheckBox(By.Id($"option_{i}"));
                }
                else
                {
                    var i = values.IndexOf(value);
                    Driver.UntickCheckBox(By.Id($"option_{i}"));
                }
            }
        }

        public List<string> GetFailureTriggerForAnswers()
        {
            VerifyAddNewQElementDisplayed();
            var values = Driver.GetInputBoxValues(_questionOptions);
            var triggers = Driver.FindElements(_triggers);
            var result = new List<string>(); 

            var i = 0;
            foreach (var value in values)
            {
                if (Driver.IsCheckBoxTicked(triggers[i]))
                {
                    result.Add(value);
                }

                i++;
            }

            return result;
        }

        public void VerifyAnswersSelectedAsFailureTriggers(Table table)
        {
            var rows = table.Rows;

            foreach (var row in rows)
            {
                ClickEditQButton(row["qtext"]);
                var actualTriggers = GetFailureTriggerForAnswers();
                var expectedTriggers = row["failedoptions"].ConvertStringIntoList();
                Assert.AreEqual(expectedTriggers, actualTriggers, $"The Triggers Fail options are not selected as expected for question {row["qtext"]}");
                ClickCancelQButton();
            }
        }

        #region Helpers

        public void EnterQuestionText(string questionText)
        {
            Driver.EnterText(_qTextField, questionText);
        }

        public void EnterHelpText(string text)
        {
            Driver.EnterText(_helpTextField, text);
        }

        public void EnterCharacterLimit(string characterLimit)
        {
            Driver.Clear(_qCharLimit);
            Driver.EnterText(_qCharLimit, characterLimit);
        }

        private void SelectMandatoryTickBox(string isMandatory)
        {
            Driver.ClickItem(_scrollableChildTable);
            Driver.TickUntickCheckBox(_qMandatoryTickBox, isMandatory);
        }

        private void CreateLineTextQuestion(TableRow row)
        {
            var questionType = row["qtype"];
            var qtext = row["qtext"];
            var isMandatory = row["mandatory"];
            var charLimit = row["char_limit"];            

            SelectQType(questionType);
            EnterQuestionText(qtext);
            EnterCharacterLimit(charLimit);
            SelectMandatoryTickBox(isMandatory);
            if (row.ContainsKey("helptext") && !string.IsNullOrWhiteSpace(row["helptext"]))
            {
                var helptext = row["helptext"];
                EnterHelpText(helptext);
            }           
        }

        private void CreateNumericalInputQuestion(TableRow row)
        {
            var questionType = row["qtype"];
            var qtext = row["qtext"];
            var isMandatory = row["mandatory"];
            var charLimit = row["char_limit"];

            SelectQType(questionType);
            EnterQuestionText(qtext);
            EnterCharacterLimit(charLimit);
            SelectMandatoryTickBox(isMandatory);
            if (row.ContainsKey("helptext") && !string.IsNullOrWhiteSpace(row["helptext"]))
            {
                var helptext = row["helptext"];
                EnterHelpText(helptext);
            }
        }


        private void CreateRadioButtonQuestion(TableRow row)
        {
            var questionType = row["qtype"];
            var qtext = row["qtext"];
            var isMandatory = row["mandatory"];
            var options = row["options"];

            SelectQType(questionType);
            EnterQuestionText(qtext);
            SelectMandatoryTickBox(isMandatory);
            CreateUpdateQuestionOptions(options);

            if (row.ContainsKey("failedoptions") && !string.IsNullOrWhiteSpace(row["failedoptions"]))
            {
                var failedOptions = row["failedoptions"].ConvertStringIntoList();
                SelectFailureTriggerForAnswers(failedOptions);
            }

            if (row.ContainsKey("helptext") && !string.IsNullOrWhiteSpace(row["helptext"]))
            {
                var helptext = row["helptext"];
                EnterHelpText(helptext);
            }
        }

        private void CreateDrpDwnListQuestion(TableRow row)
        {
            var questionType = row["qtype"];
            var qtext = row["qtext"];
            var isMandatory = row["mandatory"];
            var options = row["options"];

            SelectQType(questionType);
            EnterQuestionText(qtext);
            SelectMandatoryTickBox(isMandatory);
            CreateUpdateQuestionOptions(options);

            if (row.ContainsKey("failedoptions") && !string.IsNullOrWhiteSpace(row["failedoptions"]))
            {
                var failedOptions = row["failedoptions"].ConvertStringIntoList();
                SelectFailureTriggerForAnswers(failedOptions);
            }

            if (row.ContainsKey("helptext") && !string.IsNullOrWhiteSpace(row["helptext"]))
            {
                var helptext = row["helptext"];
                EnterHelpText(helptext);
            }
        }

        private void CreateUpdateQuestionOptions(string options)
        {
            var drpDownOptionsList = options.ConvertStringIntoList(); // convert drop down options into list 
            for (var i = 0;
                i < drpDownOptionsList.Count;
                i++) // loop for the options list and creating new input box if not present already 
            {
                if (drpDownOptionsList[i] == "")
                {
                    continue; // continue if the drop down list is empty
                }

                var inputElement = By.Id($"questionOption_{i}"); //input element
                if (!Driver.IsElementWithinWebElementPresent(_qDrpDwnOptionsInp, inputElement))
                {
                    var buttonNumber = i;
                    ClickAddDrpDwnOptions(buttonNumber);
                    Driver.WaitForItemWithinWebElement(_qDrpDwnOptionsInp, inputElement);
                }

                var optionInputWebElem = Driver.FindElement(_qDrpDwnOptionsInp).FindElement(inputElement);
                Driver.Clear(optionInputWebElem);
                Driver.EnterText(optionInputWebElem, drpDownOptionsList[i]); //input text
            }
        }

        public void EnterTextInOption(int optionNumber, string textToEnter)
        {
            var inputElement = By.Id($"questionOption_{optionNumber - 1}"); //input element
            var optionInputWebElem = Driver.FindElement(_qDrpDwnOptionsInp).FindElement(inputElement);
            Driver.Clear(optionInputWebElem);
            Driver.EnterText(optionInputWebElem, textToEnter); //input text
        }

        public void VerifyTextInOption(int optionNumber, string expectedText)
        {
            var inputElement = By.Id($"questionOption_{optionNumber - 1}"); //input element
            var optionInputWebElem = Driver.FindElement(_qDrpDwnOptionsInp).FindElement(inputElement);
            var actualText = Driver.GetInputBoxValue(optionInputWebElem).Trim();
            Assert.True(expectedText == actualText,
                $"Expected text on Options {expectedText} was not equal to Actual Text {actualText}");
        }

        public void ClearOptionsField(int optionNumber)
        {
            var inputElement = By.Id($"questionOption_{optionNumber - 1}"); //input element
            Driver.Clear(inputElement);
        }

        // Multiple options can have add button depending on the number of options, so this logic clicks on the certain button
        public void ClickAddDrpDwnOptions(int buttonNumber)
        {
            var addButtonsList = Driver.FindElements(_qDrpDwnOptionsAddBtn);
            Driver.ClickItem(addButtonsList[buttonNumber - 1]);
        }

        // Multiple options can have add button depending on the number of options, so this logic clicks on the certain button
        public void ClickRemoveDrpDwnOptions(int buttonNumber)
        {
            var removeButtonsList = Driver.FindElements(_qDrpDwnOptionsRemoveBtn);
            Driver.ClickItem(removeButtonsList[buttonNumber - 1]);
        }

        #endregion

        public void VerifyQuestionHeaderAndTip(string header, string tip)
        {
            var actualHeader = Driver.GetText(_qHeader);
            var actualTip = Driver.GetText(_qTip);

            Assert.AreEqual(header, actualHeader, "The question header is not as expected.");
            Assert.AreEqual(tip, actualTip, "The question textbox tip is not as expected.");
        }

        public void VerifyQuestionText(string enteredQuestionText)
        {
            var text = Driver.GetInputBoxValue(_qTextField);

            Assert.AreEqual(enteredQuestionText, text, "The text in the question text box is not as expected.");
        }

        public new void VerifyHelpText(string text)
        {
            var actualText = Driver.GetInputBoxValue(_helpTextField);

            Assert.AreEqual(text, actualText, "The helptext in the help text box for a description is not as expected.");
        }

        public void VerifyHelpTextForQuestions(Table table)
        {
            foreach (var row in table.Rows)
            {
                var qText = row["qtext"];
                var helpText = row["helptext"];
                ClickEditQButton(qText);
                VerifyHelpText(helpText);
                ClickCancelQButton();
            }                       
        }
    }
}