using System;
using J2BIOverseasOps.Extensions;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Pages.FormManagement
{
    internal class EditViewFormPage : CommonFormMgmtElements
    {
        //TODO
        private readonly By _inputType = By.XPath(".//td[3]");
        private readonly By _isActive = By.CssSelector("[id*=activeCol] p-checkbox");
        private readonly By _isMandatory = By.CssSelector("[id*=mandatoryCol] p-checkbox");
        private readonly By _qNameTextColumn = By.CssSelector("[id*=questionCol]");
        private readonly By _reOrderArrowDown = By.XPath(".//*[contains(@class, 'glyphicon-arrow-down')]");
        private readonly By _reOrderArrowUp = By.XPath(".//*[contains(@class, 'glyphicon-arrow-up')]");
        private readonly By _reOrderNavDisabled = By.XPath(".//*[contains(@class, 'navDisabled')]");
        private readonly By _reOrderNavEnabled = By.XPath(".//*[@class='navEnabled']");
        private readonly By _reOrderQuestions = By.CssSelector("[id*=orderCol]");
        protected readonly By AllQuestions = By.XPath("//tbody//tr/td/..");

        public EditViewFormPage(IWebDriver driver, ILog log) : base(driver, log)
        {
        }

        public void VerifyQuestionColumns(Table table)
        {
            throw new NotImplementedException();
        }

        public void VerifyFormName()
        {
            var actualFormName = Driver.GetInputBoxValue(FormNameInputBox);
            var expectedFormName = ScenarioContext.Current[FormNameKey];
            Assert.AreEqual(actualFormName, expectedFormName,
                $"Expected form name {expectedFormName} was not found in the form name input box. Actual form name {actualFormName}");
        }

        public void VerifyQuestionsOnForm(Table table)
        {
            foreach (var row in table.Rows)
            {
                var expectedQText = row["qtext"];
                var expectedQType = row["qtype"];
                var expectedMandatoryStatus = row["mandatory"];
                var expectedActive = row["active"];
               VerifyAQuestionOnForm(expectedQText, expectedQType, expectedMandatoryStatus, expectedActive);
            }
        }

        public void VerifyAQuestionOnForm(string expectedQText, string expectedQType, string expectedMandatoryStatus, string expectedActive)
        {
            var qRow = GetQuestionRow(expectedQText);
            var actualQText = qRow.FindElement(_qNameTextColumn).Text;
            var actualQType = qRow.FindElement(_inputType).Text;
            var isMandatoryElem = qRow.FindElement(_isMandatory);
            var isActiveElem = qRow.FindElement(_isActive);
            var actualMandatoryStatus = Driver.IsCheckBoxTicked(isMandatoryElem).ToString();
            var actualActiveStatus = Driver.IsCheckBoxTicked(isActiveElem).ToString();

            Assert.AreEqual(expectedQText, actualQText, $"Expected {expectedQText} Actual {actualQText}");
            Assert.AreEqual(expectedQType, actualQType, $"Expected {expectedQType} Actual {actualQType}");
            Assert.AreEqual(expectedMandatoryStatus, actualMandatoryStatus,
                $"Expected {expectedMandatoryStatus} Actual {actualMandatoryStatus}");
            Assert.AreEqual(expectedActive, actualActiveStatus,
                $"Expected {expectedActive} Actual {actualActiveStatus}");
        }


        public void VerifyDescriptionInputTypeOnForm(string description, string expectedQType, bool expectedActive)
        {
                var qRow = GetQuestionRow(description);
                var actualQText = Driver.GetText(qRow.FindElement(_qNameTextColumn));
                var actualQType = Driver.GetText(qRow.FindElement(_inputType));           
                var isActiveElem = qRow.FindElement(_isActive);
                var actualActiveStatus = Driver.IsCheckBoxTicked(isActiveElem);

                Assert.AreEqual(description, actualQText, $"Expected {description} Actual {actualQText}");
                Assert.AreEqual(expectedQType, actualQType, $"Expected {expectedQType} Actual {actualQType}");
                Assert.AreEqual(expectedActive, actualActiveStatus,
                    $"Expected {expectedActive} Actual {actualActiveStatus}");
        }

        public void VerifyQuestionNotDisplayed(string question)
        {
            var questionRowElem = By.XPath($"//*[contains(@class, '{question}')]");
            Assert.True(!Driver.IsElementPresent(questionRowElem),
                $"Was able to find question {question} while expecting it not to be there.");
        }

        public void VerifyTotalNumberOfQuestions(string expectedTotalNumberAllQuestions)
        {
            var actualTotalNumberAllQuestions = Driver.FindElements(AllQuestions).Count.ToString();
            Assert.AreEqual(expectedTotalNumberAllQuestions, actualTotalNumberAllQuestions,
                $"Expected number of questions {expectedTotalNumberAllQuestions} are not same as actual number of questions {actualTotalNumberAllQuestions} ");
        }

        public void VerifyQEditBtnState(string visibility, string enableState, string qText)
        {
            var questionRow = GetQuestionRow(qText);
            var editButton = questionRow.FindElement(EditQButton);
            VerifyElementVisibility(visibility, editButton);
            VerifyElementState(enableState, editButton);
        }

        public void VerifyQActiveCheckBoxState(string enableState, string qText)
        {
            var questionRow = GetQuestionRow(qText);
            var isActive = questionRow.FindElement(_isActive);
            VerifyCheckBoxState(enableState, isActive);
        }

        public void VerifyQMandatoryCheckBoxState(string enableState, string qText)
        {
            var questionRow = GetQuestionRow(qText);
            var isMandatory = questionRow.FindElement(_isMandatory);
            VerifyCheckBoxState(enableState, isMandatory);
        }


        public void VerifyAllQuestionsState(string visibility, string enabledDisabled)
        {
            var listOfAllQuestions = Driver.FindElements(AllQuestions);
            foreach (var questionRow in listOfAllQuestions)
            {
                VerifyElementState(enabledDisabled, questionRow);
                VerifyElementVisibility(visibility, questionRow);
            }
        }

        public void VerifyAllQuestionsResorderState(string visibility, string enabledDisabled)
        {
            var listOfAllQuestions = Driver.FindElements(AllQuestions);
            foreach (var questionRow in listOfAllQuestions)
            {
                var reOrderElement = questionRow.FindElement(_reOrderQuestions);
                VerifyElementState(enabledDisabled, reOrderElement);
                VerifyElementVisibility(visibility, reOrderElement);
            }
        }

        public void VerifyAllActiveCheckBoxesState(string visibility, string enabledDisabled)
        {
            var listOfAllQuestions = Driver.FindElements(AllQuestions);
            foreach (var questionRow in listOfAllQuestions)
            {
                var activeCheckBox = questionRow.FindElement(_isActive);
                VerifyElementState(enabledDisabled, activeCheckBox);
                VerifyElementVisibility(visibility, activeCheckBox);
            }
        }

        public void VerifyAllMandatoryCheckBoxesState(string visibility, string enabledDisabled)
        {
            var listOfAllQuestions = Driver.FindElements(AllQuestions);
            foreach (var questionRow in listOfAllQuestions)
            {
                var mandatoryCheckBox = questionRow.FindElement(_isActive);
                VerifyElementState(enabledDisabled, mandatoryCheckBox);
                VerifyElementVisibility(visibility, mandatoryCheckBox);
            }
        }

        public void VerifyReOrderButtonStatus(string upDownArrow, string enabledDisabled, string questionText)
        {
            var questionElement = GetQuestionRow(questionText);
            var arrowToVerify = _reOrderArrowDown;
            if (upDownArrow == "Up")
            {
                arrowToVerify = _reOrderArrowUp;
            }

            switch (enabledDisabled)
            {
                case "Enabled":
                    Assert.True(questionElement.FindElement(_reOrderQuestions).FindElement(_reOrderNavEnabled)
                            .IsElementWithinWebElementPresent(arrowToVerify),
                        $"Could not verify the reorder button is enabled");
                    return;
                case "Disabled":
                    Assert.True(questionElement.FindElement(_reOrderQuestions).FindElement(_reOrderNavDisabled)
                            .IsElementWithinWebElementPresent(arrowToVerify),
                        $"Could not verify the reorder button is disabled");
                    return;
                default:
                    Assert.Fail($"{enabledDisabled} is not a valid state. Expecting state Enabled or Disabled");
                    return;
            }
        }

        public void ClickReOrderArrow(string upDownArrow, string questionText)
        {
            var questionElement = GetQuestionRow(questionText);
            switch (upDownArrow)
            {
                case "Up":
                    Driver.ClickItem(questionElement.FindElement(_reOrderArrowUp));
                    return;
                case "Down":
                    Driver.ClickItem(questionElement.FindElement(_reOrderArrowDown));
                    return;
            }
        }
    }
}