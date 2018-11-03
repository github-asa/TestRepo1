using J2BIOverseasOps.Pages.FormManagement;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.FormManagement
{
    [Binding]
    public sealed class EditViewFormStepDefs : BaseStepDefs
    {
        private readonly EditViewFormPage _editViewForm;

        public EditViewFormStepDefs(IWebDriver driver, ILog log) : base(driver, log)
        {
            _editViewForm = new EditViewFormPage(driver, log);
        }

        [Then(@"I verify the Form name textbox contains the form name on the view form page")]
        public void ThenIVerifyTheFormNameTextboxContainsTheFormNameOnTheViewFormPage()
        {
            _editViewForm.VerifyFormName();
        }

        [Then(
            @"I verify the Form name textbox is ""(visible|invisible)"" and ""(enabled|disabled)"" on the view form page")]
        public void ThenIVerifyTheFormNameTextboxIsAndOnTheViewFormPage(string visibility, string enabledStatus)
        {
            _editViewForm.VerifyFormNameTextBoxState(visibility, enabledStatus);
        }

        [Then(
            @"I verify the Add new question button is ""(visible|invisible)"" and ""(enabled|disabled)"" on the view form page")]
        public void ThenIVerifyTheAddNewQuestionButtonIsAndOnTheViewFormPage(string visibility, string enabledStatus)
        {
            _editViewForm.VerifyAddNewQBtnState(visibility, enabledStatus);
        }

        [Then(@"I verify the Back button is ""(.*)"" and ""(.*)"" on the view form page")]
        public void ThenIVerifyTheBackButtonIsAndOnTheViewFormPage(string visibility, string enabledStatus)
        {
            _editViewForm.VerifyBackBtnState(visibility, enabledStatus);
        }

        [Then(@"I verify the Continue form button is ""(.*)"" and ""(.*)"" on the view form page")]
        public void ThenIVerifyTheSaveFormButtonIsAndOnTheViewFormPage(string visibility, string enabledStatus)
        {
            _editViewForm.VerifyContinueFormBtnState(visibility, enabledStatus);
        }

        [When(@"I click the Back button on the view form page")]
        public void WhenIClickTheBackButtonOnTheViewFormPage()
        {
            _editViewForm.ClickBackButton();
        }

        [When(@"I enter the text ""(.*)"" in form name input box")]
        public void WhenIEnterTheTextInFormNameInputBox(string text)
        {
            _editViewForm.EnterTextFormNameInputBox(text);
        }

        [Then(@"I verify the following questions are displayed on the form:")]
        public void ThenIVerifyTheFollowingQuestionsAreDisplayedOnTheForm(Table table)
        {
            _editViewForm.VerifyQuestionsOnForm(table);
        }

        [Then(@"I verify that the description input type is added to the form")]
        public void ThenIVerifyThatTheDescriptionInputTypeIsAddedToTheForm()
        {
            var text = ScenarioContext.Current.Get<string>("Description");
            _editViewForm.VerifyDescriptionInputTypeOnForm(text, "Description", true);
        }

        [Then(@"I verify the question ""(.*)"" is not displayed on the form")]
        public void ThenIVerifyTheQuestionIsNotDisplayedOnTheForm(string questionText)
        {
            _editViewForm.VerifyQuestionNotDisplayed(questionText);
        }

        [Then(@"I verify the total number of questions on the form are ""(.*)""")]
        public void ThenIVerifyTheTotalNumberOfQuestionsOnTheFormAre(string totalNumberOfQuestions)
        {
            _editViewForm.VerifyTotalNumberOfQuestions(totalNumberOfQuestions);
        }

        [When(@"I click the continue form button")]
        [Then(@"I click the continue form button")]
        public void ThenIClickTheSaveFormButton()
        {
            _editViewForm.ClickContinueFormButton();
        }

        [Then(@"I verify the Edit button is ""(visible|invisible)"" and ""(enabled|disabled)"" for question ""(.*)""")]
        public void ThenIVerifyTheEditButtonIsAndForQuestion(string visibility, string enableState, string qText)
        {
            _editViewForm.VerifyQEditBtnState(visibility, enableState, qText);
        }

        [Then(@"I verify the Active checkbox is ""(enabled|disabled)"" for question ""(.*)""")]
        public void ThenIVerifyTheActiveCheckboxIsForQuestion(string enableState, string qText)
        {
            _editViewForm.VerifyQActiveCheckBoxState(enableState, qText);
        }

        [Then(@"I verify the Mandatory checkbox is ""(enabled|disabled)"" for question ""(.*)""")]
        public void ThenIVerifyTheMandatoryCheckboxIsForQuestion(string enableState, string qText)
        {
            _editViewForm.VerifyQMandatoryCheckBoxState(enableState, qText);
        }

        [Then(@"I verify all the questions displayed are ""(visible|invisible)"" and ""(enabled|disabled)""")]
        public void ThenIVerifyAllTheQuestionsDisplayedAreAnd(string visibility, string activeStatus)
        {
            _editViewForm.VerifyAllQuestionsState(visibility, activeStatus);
        }

        [Then(
            @"I verify all the questions displayed, option to reorder question is ""(visible|invisible)"" and ""(enabled|disabled)""")]
        public void ThenIVerifyAllTheQuestionsDisplayedOptionToReorderQuestionIsAnd(string visibility,
            string activeStatus)
        {
            _editViewForm.VerifyAllQuestionsResorderState(visibility, activeStatus);
        }

        [Then(
            @"I verify the Active checkboxes for all the questions are ""(visible|invisible)"" and ""(enabled|disabled)""")]
        public void ThenIVerifyTheActiveCheckboxesForAllTheQuestionsAreAnd(string visibility, string enabledDisabled)
        {
            _editViewForm.VerifyAllActiveCheckBoxesState(visibility, enabledDisabled);
        }

        [Then(
            @"I verify the Mandatory checkboxes for all the questions are ""(visible|invisible)"" and ""(enabled|disabled)""")]
        public void ThenIVerifyTheMandatoryCheckboxesForAllTheQuestionsAreAnd(string visibility, string enabledDisabled)
        {
            _editViewForm.VerifyAllMandatoryCheckBoxesState(visibility, enabledDisabled);
        }

        [Then(@"I verify the reorder ""(Up|Down)"" arrow is ""(Enabled|Disabled)"" for the ""(.*)""")]
        public void ThenIVerifyTheReorderArrowIsForThe(string upDownArrow, string enabledDisabled, string questionText)
        {
            _editViewForm.VerifyReOrderButtonStatus(upDownArrow, enabledDisabled, questionText);
        }

        [When(@"I click the redorder ""(Up|Down)"" arrow for the ""(.*)""")]
        public void WhenIClickTheRedorderArrowForThe(string upDownArrow, string questionText)
        {
            _editViewForm.ClickReOrderArrow(upDownArrow, questionText);
        }

        [Then(@"I verify the following question is displayed on the form '(.*)', '(.*)', '(.*)', '(.*)'")]
        public void ThenIVerifyTheFollowingQuestionIsDisplayedOnTheFormTrue(string text, string type, string mandatory, string active)
        {
            _editViewForm.VerifyAQuestionOnForm(text, type, mandatory, active);
        }
    }
}