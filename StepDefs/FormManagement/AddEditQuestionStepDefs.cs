using System;
using J2BIOverseasOps.Helpers;
using J2BIOverseasOps.Pages.FormManagement;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.FormManagement
{
    [Binding]
    public sealed class AddEditQuestionStepDefs : BaseStepDefs
    {
        private readonly AddEditQuestionElement _addEditQuestion;

        public AddEditQuestionStepDefs(IWebDriver driver, ILog log) : base(driver, log)
        {
            _addEditQuestion = new AddEditQuestionElement(driver, log);
        }

        [When(@"I enter the question text as ""(.*)"" on the add new question component")]
        [When(@"I enter a description of '(.*)' on the form")]
        public void WhenIEnterTheQuestionTextAsOnTheQuestionElement(string questionText)
        {
            _addEditQuestion.EnterQuestionText(questionText);
        }

        [Then(@"I verify the row is expanded to display the options for adding a new question")]
        public void ThenIVerifyTheRowIsExpandedToDisplayTheOptionsForAddingANewQuestion()
        {
            _addEditQuestion.VerifyAddNewQElementDisplayed();
        }

        [When(@"I add the following questions:")]
        [When(@"I add the following input types to the form:")]
        public void WhenIAddNewQuestionsWithFollowingParameters(Table table)
        {
            _addEditQuestion.AddNewQuestions(table);
        }

        [When(@"I update the following questions:")]
        public void WhenIUpdateTheFollowingQuestions(Table table)
        {
            _addEditQuestion.UpdateExistingQuestions(table);
        }

        [Then(@"I verify the Question text textbox is displayed on the add new question element")]
        public void ThenIVerifyTheQuestionTextTextboxIsOnTheAddNewQuestionElement()
        {
            _addEditQuestion.VerifyQTextBoxDisplayed();
        }

        [Then(
            @"I verify the Save question button is ""(visible|invisible)"" and ""(enabled|disabled)"" on the add new question element")]
        public void ThenIVerifyTheSaveQuestionButtonIsAndOnTheAddNewQuestionElement(string visibility,
            string enableState)
        {
            _addEditQuestion.VerifySaveButtonState(visibility, enableState);
        }

        [Then(
            @"I verify the Cancel question button is ""(visible|invisible)"" and ""(enabled|disabled)"" on the add new question element")]
        public void ThenIVerifyTheCancelQuestionButtonIsAndOnTheAddNewQuestionElement(string visibility,
            string enableState)
        {
            _addEditQuestion.VerifyCancelButtonState(visibility, enableState);
        }

        [Then(@"I verify the Is Mandatory checkbox is ""(Ticked|Unticked)""")]
        public void ThenIVerifyTheIsMandatoryCheckboxIs(string tickState)
        {
            _addEditQuestion.VerifyIsMandatoryCheckBoxState(tickState);
        }

        [When(@"I select the question type as ""(.*)""")]
        public void WhenISelectTheQuestionTypeAs(string questionType)
        {
            _addEditQuestion.SelectQType(questionType);
        }

        [When(@"I click the save question button")]
        public void WhenIClickTheSaveQuestionButton()
        {
            _addEditQuestion.ClickSaveQButton();
        }

        [Then(@"I should be presented with ""(.*)"" error message")]
        public void ThenIShouldBePresentedWithErrorMessage(string errorText)
        {
            _addEditQuestion.VerifyAddQValidationErrorText(errorText);
        }

        [When(@"I enter the option number ""(.*)"" as ""(.*)"" on the Add new question pop up box")]
        public void WhenIEnterTheDropDownOptionNumberAs(int optionNumber, string text)
        {
            _addEditQuestion.EnterTextInOption(optionNumber, text);
        }

        [Then(@"I verify the text on option number ""(.*)"" as ""(.*)""")]
        public void ThenIVerifyTheTextOnOptionNumberAs(int optionNumber, string text)
        {
            _addEditQuestion.VerifyTextInOption(optionNumber, text);
        }

        [When(
            @"I clear the drop down option field number ""(.*)"" for Drop Down list on the Add new Question component")]
        public void WhenIClearTheDropDownOptionFieldNumberForDropDownListOnAddNewQuestionComponent(int optionNumber)
        {
            _addEditQuestion.ClearOptionsField(optionNumber);
        }

        [When(@"I enter the character limit as ""(.*)"" on the add new question component")]
        public void WhenIEnterTheCharacterLimitAsOnTheAddNewQuestionComponent(string characterLimit)
        {
            _addEditQuestion.EnterCharacterLimit(characterLimit);
        }

        [When(@"I click the Cancel button  on the add new question component")]
        public void WhenIClickTheCancelButtonOnTheAddNewQuestionComponent()
        {
            _addEditQuestion.ClickCancelQButton();
        }

        [Then(@"I verify the new question has the title of ""(.*)"" on the add new question component")]
        public void ThenIVerifyTheNewQuestionHasTheTitleOfOnTheAddNewQuestionComponent(string expectedTitle)
        {
            _addEditQuestion.VerifyQElementTitle(expectedTitle);
        }

        [Then(@"I verify remove buttons on dropdown options are ""(visible|invisible)"" and ""(enabled|disabled)""")]
        public void ThenIVerifyRemoveButtonsOnDropdownOptionsAreAnd(string visibility, string enableState)
        {
            _addEditQuestion.VerifyRemoveDrpDwnOptionsBtnState(visibility, enableState);
        }

        [When(@"I click the add option button number ""(.*)"" on the add new question component")]
        public void WhenIClickTheAddDropDownOptionButtonNumberOnTheAddNewQuestionComponent(int optionNumber)
        {
            _addEditQuestion.ClickAddDrpDwnOptions(optionNumber);
        }

        [When(@"I click the remove drop down option button number ""(.*)"" on the add new question component")]
        public void WhenIClickTheRemoveDropDownOptionButtonNumberOnTheAddNewQuestionComponent(int optionNumber)
        {
            _addEditQuestion.ClickRemoveDrpDwnOptions(optionNumber);
        }

        [When(@"I click the Edit link for question ""(.*)""")]
        public void WhenIClickTheEditLinkForQuestion(string questionText)
        {
            _addEditQuestion.ClickEditQButton(questionText);
        }

        [When(@"I change the active status of question ""(.*)"" as ""(True|False)""")]
        public void WhenIChangeTheActiveStatusOfQuestionAs(string qText, string activeStatus)
        {
            _addEditQuestion.ChangeQActiveStatus(qText, activeStatus);
        }

        [Then(@"I verify the question ""(.*)"" is greyed out")]
        public void ThenIVerifyTheQuestionIsGreyedOut(string qText)
        {
            _addEditQuestion.VerifyQGreyedOut(qText);
        }

        [When(@"I verify the question ""(.*)"" is not greyed out")]
        public void WhenIVerifyTheQuestionIsNotGreyedOut(string qText)
        {
            _addEditQuestion.VerifyQNotGreyedOut(qText);
        }

        [Then(@"I verify the question ""(.*)"" is displayed on the row number ""(.*)""")]
        public void ThenIVerifyTheQuestionIsDisplayedOnTheRowNumber(string qText, int rowNumber)
        {
            _addEditQuestion.VerifyQuestionPresentOnARow(qText, rowNumber);
        }

        [When(@"I select the Show Inactive questions tickbox as ""(True|False)""")]
        public void WhenISelectTheShowInactiveQuestionsTickboxAs(string tickBoxStatus)
        {
            _addEditQuestion.ShowInactiveQuestions(tickBoxStatus);
        }

        [Then(@"I verify that the following answers have been selected as answers that will trigger a fail:")]
        public void ThenIVerifyThatTheFollowingAnswersHaveBeenSelectedAsAnswersThatWillTriggerAFail(Table table)
        {
            _addEditQuestion.VerifyAnswersSelectedAsFailureTriggers(table);
        }

        [Then(@"I verify that the '(.*)' header and the '(.*)' tip and textbox are displayed on the question panel")]
        public void ThenIVerifyThatTheHeaderAndTheTipAndTextboxAreDisplayedOnTheQuestionPanel(string header, string tip)
        {
            _addEditQuestion.VerifyQuestionHeaderAndTip(header, tip);
            _addEditQuestion.VerifyQTextBoxDisplayed();            
        }

        [When(@"I enter a description with a length of '(.*)' on the form")]
        public void WhenIEnterADescriptionWithALengthOfOnTheForm(int length)
        {
            var text = PageHelper.RandomString(length);
            ScenarioContext.Current.Set(text, "Description");
            _addEditQuestion.EnterQuestionText(text);
        }

        [Then(@"I verify that the description textbox contains no more than '(.*)' characters")]
        public void ThenIVerifyThatTheDescriptionTextboxContainsNoMoreThanCharacters(int limit)
        {
            var enteredQuestionText = ScenarioContext.Current.Get<string>("Description");

            if (enteredQuestionText.Length > limit)
            {
                enteredQuestionText = enteredQuestionText.Substring(0, limit);
            }

            ScenarioContext.Current.Set(enteredQuestionText, "Description");

            _addEditQuestion.VerifyQuestionText(enteredQuestionText);
        }

        [Then(@"I verify that the description textbox contains '(.*)' on the form")]
        public void ThenIVerifyThatTheDescriptionTextboxContainsOnTheForm(string questionText)
        {
            _addEditQuestion.VerifyQuestionText(questionText);
        }

        [Then(@"I verify the following questions are displayed on the form with the helptext:")]
        public void ThenIVerifyTheFollowingQuestionsAreDisplayedOnTheFormWithTheHelptext(Table table)
        {
            _addEditQuestion.VerifyHelpTextForQuestions(table);
        }

    }
}