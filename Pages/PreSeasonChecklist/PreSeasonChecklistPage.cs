using System;
using J2BIOverseasOps.Extensions;
using J2BIOverseasOps.Helpers;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Pages.PreSeasonChecklist
{
    public class PreSeasonChecklistPage : BasePage
    {
        private static int _index;
        private By _answerTextArea => By.CssSelector($"[id^='container'][id$='-{_index}'] textarea");
        private By _answerTextBox => By.CssSelector($"[id^='container'][id$='-{_index}'] input");
        private By _attachment => By.CssSelector($"[id^='container'][id$='-{_index}'] dropzone");
        private By _backButton => By.Id("psc-back-btn");
        private By _clearDropdown => By.CssSelector($"[id^='container'][id$='-{_index}'] p-dropdown .fa-close");
        private By _datepicker => By.CssSelector($"[id^='container-6-{_index}'] p-calendar");
        private By _datepickerInput => By.CssSelector($"[id^='container-6-{_index}'] p-calendar input");
        private By _dropdown => By.CssSelector($"[id^='container'][id$='-{_index}'] p-dropdown");
        private By _multiselect => By.CssSelector($"[id^='container'][id$='-{_index}'] p-multiselect");
        private By _question => By.CssSelector($"[id^='container'][id$='-{_index}']");
        private By _questionLabel => By.CssSelector($"[id^='container'][id$='-{_index}'] label");
        private By _radioButtons => By.CssSelector($"[id^='container'][id$='-{_index}']");
        private By _time => By.CssSelector($"[id^='container-5-{_index}'] p-calendar[placeholder=time] input");
        private By _saveButton => By.Id("psc-save-btn");
        private By _submitButton => By.Id("psc-submit-btn");
        private By _deleteButton => By.Id("psc-delete-btn");
        private By _validationMessage => By.CssSelector(".errorMessage .ui-message-text");
        private By _calendar => By.CssSelector($"[id^='container-6-{_index}'] p-calendar .ui-datepicker-title");
        private By _calendarButton => By.CssSelector($"[id^='container-6-{_index}'] p-calendar button");

        public PreSeasonChecklistPage(IWebDriver driver, ILog log) : base(driver, log)
        {
        }

        public void VerifyQuestionsAreDisplayed(Table table)
        {
            _index = 0;
            foreach (var row in table.Rows)
            {
                VerifyQuestionsIsDisplayed(row);
                _index++;
            }
        }

        private void VerifyQuestionsIsDisplayed(TableRow row)
        {
            var type = row["Type"].ToLower().Trim();
            switch (type)
            {
                case "single line text":
                    VerifySingleLineTextQuestionIsDisplayed(row["QuestionText"]);
                    break;
                case "multi line text":
                    VerifyMultiLineTextQuestionIsDisplayed(row["QuestionText"]);
                    break;
                case "drop down list (single choice)":
                    VerifyDropDownListSingleChoiceQuestionIsDisplayed(row["QuestionText"], row["Options"]);
                    break;
                case "drop down list (multi choice)":
                    VerifyDropDownListMultiChoiceQuestionIsDisplayed(row["QuestionText"], row["Options"]);
                    break;
                case "time":
                    VerifyTimeQuestionIsDisplayed(row["QuestionText"]);
                    break;
                case "date":
                    VerifyDateQuestionIsDisplayed(row["QuestionText"]);
                    break;
                case "attachment":
                    VerifyAttachmentIsDisplayed(row["QuestionText"]);
                    break;
                case "description":
                    VerifyDescriptionIsDisplayed(row["QuestionText"]);
                    break;
                case "radio button":
                    VerifyRadioButtonsAreDisplayed(row["QuestionText"], row["Options"]);
                    break;
            }
        }

        private void VerifyRadioButtonsAreDisplayed(string questionText, string options)
        {
            VerifyQuestionText(questionText);
            var radioOptions = Driver.GetRadioButtonsOptions(_radioButtons);
            var expectedOptions = options.ConvertStringIntoList();
            Assert.AreEqual(expectedOptions, radioOptions, $"The radio options are not as expected for Q{_index + 1}.");
        }

        private void VerifyDescriptionIsDisplayed(string questionText)
        {
            VerifyQuestionText(questionText);
        }

        private void VerifyAttachmentIsDisplayed(string questionText)
        {
            VerifyQuestionText(questionText);
            Assert.IsTrue(Driver.WaitForItem(_attachment),
                $"The attachment dropzone is not being displayed for Q{_index + 1}.");
        }

        private void VerifyDateQuestionIsDisplayed(string questionText)
        {
            VerifyQuestionText(questionText);
            Assert.IsTrue(Driver.WaitForItem(_datepicker), $"The datepicker is not being displayed for Q{_index + 1}.");
        }

        private void VerifyTimeQuestionIsDisplayed(string questionText)
        {
            VerifyQuestionText(questionText);
            Assert.IsTrue(Driver.WaitForItem(_time), $"The time field is not being displayed for Q{_index + 1}.");
        }

        private void VerifyDropDownListMultiChoiceQuestionIsDisplayed(string questionText, string options)
        {
            VerifyQuestionText(questionText);
            var optionsList = Driver.GetAllMultiselectOptions(_multiselect);
            var expectedOptions = options.ConvertStringIntoList();
            Assert.AreEqual(expectedOptions, optionsList,
                $"The multiselect options are not as expected for Q{_index + 1}.");
        }

        private void VerifyDropDownListSingleChoiceQuestionIsDisplayed(string questionText, string options)
        {
            VerifyQuestionText(questionText);
            var optionsList = Driver.GetAllDropDownOptions(_dropdown);
            var expectedOptions = options.ConvertStringIntoList();
            Assert.AreEqual(expectedOptions, optionsList,
                $"The dropdown options are not as expected for Q{_index + 1}.");
        }

        private void VerifyMultiLineTextQuestionIsDisplayed(string questionText)
        {
            VerifyQuestionText(questionText);
            Assert.IsTrue(Driver.WaitForItem(_answerTextArea),
                $"The answer text box is not being displayed for Q{_index + 1}");
        }

        private void VerifySingleLineTextQuestionIsDisplayed(string questionText)
        {
            VerifyQuestionText(questionText);
            Assert.IsTrue(Driver.WaitForItem(_answerTextBox),
                $"The answer text box is not being displayed for Q{_index + 1}");
        }

        private void VerifyQuestionText(string questionText)
        {
            Assert.IsTrue(Driver.WaitForItem(_question), "The question is not displayed.");
            var actualQText = Driver.GetText(_questionLabel);
            Assert.AreEqual(questionText, actualQText, $"The question text is not as expected for Q{_index + 1}");
        }

        public void ClickBack()
        {
            Driver.ClickItem(_backButton);
        }

        public void EnterAnswersToQuestions(Table table)
        {
            _index = 0;
            foreach (var row in table.Rows)
            {
                EnterAnswersToQuestion(row);
                _index++;
            }
        }

        private void EnterAnswersToQuestion(TableRow row)
        {
            var type = row["Type"].ToLower().Trim();

            switch (type)
            {
                case "single line text":
                    EnterSingleLineTextAnswer(row["QuestionText"], row["Answer"]);
                    break;
                case "multi line text":
                    if (row.ContainsKey("Length"))
                        EnterMultiLineTextAnswer(row["QuestionText"], row["Answer"], row["Length"]);
                    else
                        EnterMultiLineTextAnswer(row["QuestionText"], row["Answer"]);                                 
                    break;
                case "drop down list (single choice)":
                    EnterDropDownListSingleChoiceAnswer(row["QuestionText"], row["Answer"]);
                    break;
                case "drop down list (multi choice)":
                    EnterDropDownListMultiChoiceAnswer(row["QuestionText"], row["Answer"]);
                    break;
                case "time":
                    EnterTimeAnswer(row["QuestionText"], row["Answer"]);
                    break;
                case "date":
                    EnterDateAnswer(row["QuestionText"], row["Answer"]);
                    break;
                case "radio button":
                    EnterRadioButtonsAnswer(row["QuestionText"], row["Answer"]);
                    break;
            }
        }

        public void VerifyAnswersEntered(Table table)
        {
            _index = 0;
            foreach (var row in table.Rows)
            {
                VerifyAnswerEntered(row);
                _index++;
            }
        }

        private void VerifyAnswerEntered(TableRow row)
        {
            var type = row["Type"].ToLower().Trim();

            switch (type)
            {
                case "single line text":
                    VerifySingleLineTextAnswer(row["QuestionText"], row["Answer"]);
                    break;
                case "multi line text":
                    VerifyMultiLineTextAnswer(row["QuestionText"], row["Answer"]);
                    break;
                case "drop down list (single choice)":
                    VerifyDropDownListSingleChoiceAnswer(row["QuestionText"], row["Answer"]);
                    break;
                case "drop down list (multi choice)":
                    VerifyDropDownListMultiChoiceAnswer(row["QuestionText"], row["Answer"]);
                    break;
                case "time":
                    VerifyTimeAnswer(row["QuestionText"], row["Answer"]);
                    break;
                case "date":
                    VerifyDateAnswer(row["QuestionText"], row["Answer"]);
                    break;
                case "radio button":
                    VerifyRadioButtonsAnswer(row["QuestionText"], row["Answer"]);
                    break;
            }
        }

        private void VerifyRadioButtonsAnswer(string questionText, string answer)
        {
            VerifyQuestionText(questionText);
            Assert.AreEqual(answer, Driver.GetSelectedRadioButtonsOption(_radioButtons),
                $"The answer is not as expected for Q{_index + 1}.");
        }

        private void VerifyDateAnswer(string questionText, string answer)
        {
            VerifyQuestionText(questionText);
            var expectedDate = string.IsNullOrWhiteSpace(answer) ? answer : Driver.CalculateFutureOrPastDate(answer);
            Assert.AreEqual(expectedDate, Driver.GetInputBoxValue(_datepickerInput),
                $"The answer is not as expected for Q{_index + 1}.");
        }

        private void VerifyTimeAnswer(string questionText, string answer)
        {
            VerifyQuestionText(questionText);
            Assert.AreEqual(answer, Driver.GetInputBoxValue(_time),
                $"The answer is not as expected for Q{_index + 1}.");
        }

        private void VerifyDropDownListMultiChoiceAnswer(string questionText, string answer)
        {
            VerifyQuestionText(questionText);
            var answers = answer.ConvertStringIntoList();
            Assert.AreEqual(answers, Driver.GetAllSelectedMultiselectOptions(_multiselect),
                $"The answer is not as expected for Q{_index + 1}.");
        }

        private void VerifyDropDownListSingleChoiceAnswer(string questionText, string answer)
        {
            VerifyQuestionText(questionText);
            Assert.AreEqual(answer, Driver.GetSelectedDropDownOption(_dropdown),
                $"The selected dropdown option is not as expected for Q{_index + 1}.");
        }

        private void VerifyMultiLineTextAnswer(string questionText, string answer)
        {
            VerifyQuestionText(questionText);
            if (answer.ToLower().Equals("random"))
            {
                answer = ScenarioContext.Current.Get<string>("EnteredAnswer");
                answer = answer.Substring(0, answer.Length - 1);
            }

            Assert.AreEqual(answer, Driver.GetInputBoxValue(_answerTextArea),
                $"The answer is not as expected for Q{_index + 1}.");
        }

        private void VerifySingleLineTextAnswer(string questionText, string answer)
        {
            VerifyQuestionText(questionText);
            Assert.AreEqual(answer, Driver.GetInputBoxValue(_answerTextBox),
                $"The answer is not as expected for Q{_index + 1}.");
        }

        private void EnterRadioButtonsAnswer(string questionText, string answer)
        {
            VerifyQuestionText(questionText);
            Driver.SelectRadioButtonOption(_radioButtons, answer);
        }

        private void EnterDateAnswer(string questionText, string answer)
        {
            VerifyQuestionText(questionText);

            if (string.IsNullOrWhiteSpace(answer))
            {
                Driver.ClearUsingBackspace(_datepickerInput);

                if (Driver.WaitForItem(_calendar, 1))
                {
                    Driver.ClickItem(_calendarButton);
                }
            }
            else if (!string.IsNullOrWhiteSpace(answer))
            {
                var expectedDate = Driver.CalculateFutureOrPastDate(answer);
                var date = Driver.ParseDateTo_ddmmyyyy(expectedDate);
                Driver.SelectDateFromCalender(_datepicker, date);
            }
        }

        private void EnterTimeAnswer(string questionText, string answer)
        {
            VerifyQuestionText(questionText);

            if (string.IsNullOrWhiteSpace(answer))
            {
                Driver.ClearUsingBackspace(_time);
            }
            else
            {
                Driver.EnterText(_time, answer);
            }
        }

        private void EnterDropDownListMultiChoiceAnswer(string questionText, string answer)
        {
            VerifyQuestionText(questionText);
            var answers = answer.ConvertStringIntoList();

            if (string.IsNullOrWhiteSpace(answer))
            {
                var options = Driver.GetAllSelectedMultiselectOptions(_multiselect);
                Driver.DeselectMultiselectOption(_multiselect, options);
            }
            else
            {
                Driver.SelectMultiselectOption(_multiselect, answers);
            }
        }

        private void EnterDropDownListSingleChoiceAnswer(string questionText, string answer)
        {
            VerifyQuestionText(questionText);

            if (string.IsNullOrWhiteSpace(answer))
            {
                Driver.ClickItem(_clearDropdown);
            }
            else
            {
                Driver.SelectDropDownOption(_dropdown, answer);
            }
        }

        private void EnterMultiLineTextAnswer(string questionText, string answer, string length = null)
        {
            VerifyQuestionText(questionText);

            if (string.IsNullOrWhiteSpace(answer))
            {
                Driver.ClearUsingBackspace(_answerTextArea);
            }
            else
            {
                if (answer.ToLower().Equals("random"))
                {
                    answer = PageHelper.RandomString(Convert.ToInt32(length) + 1);
                    ScenarioContext.Current.Set(answer, "EnteredAnswer");

                }
                Driver.EnterText(_answerTextArea, answer);
            }          
        }

        private void EnterSingleLineTextAnswer(string questionText, string answer)
        {
            VerifyQuestionText(questionText);

            if (string.IsNullOrWhiteSpace(answer))
            {
                Driver.ClearUsingBackspace(_answerTextBox);
            }
            else
            {
                Driver.EnterText(_answerTextBox, answer);
            }
        }

        public void VerifySubmitButtonIsDisabled()
        {
           Assert.IsTrue(Driver.WaitForItem(_submitButton));
           Assert.IsTrue(Driver.WaitUntilItemDisabledAndDisplayed(_submitButton), "The submit button should not be enabled.");
        }

        public void VerifySubmitButtonIsEnabled()
        {
            Assert.IsTrue(Driver.WaitForItem(_submitButton));
            Assert.IsTrue(Driver.IsElementEnabled(_submitButton));
        }

        public void VerifyValidationMessageIsDisplayed(string message)
        {
            Assert.AreEqual(message, Driver.GetText(_validationMessage), "The validation message is not as expected.");
        }

        public void ClickSave()
        {
            Driver.ClickItem(_saveButton);
        }

        public void ClickDelete()
        {
            Driver.ClickItem(_deleteButton);
        }

        public void ClickSubmit()
        {
            Driver.ClickItem(_submitButton);
        }
    }
}