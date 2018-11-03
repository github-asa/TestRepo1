using J2BIOverseasOps.Pages.FormManagement;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.FormManagement
{
    [Binding]
    public sealed class CreateFormStepDefs : BaseStepDefs
    {
        private readonly CreateFormPage _createForm;

        public CreateFormStepDefs(IWebDriver driver, ILog log) : base(driver, log)
        {
            _createForm = new CreateFormPage(driver, log);
        }

        [Then(@"I verify the page title as ""(.*)"" on the create new form page")]
        public void GivenIVerifyThePageTitleAsOnTheCreateNewFormPage(string expectedTitle)
        {
            _createForm.VerifyPageTitle(expectedTitle);
        }

        [Then(
            @"I verify the Form name textbox is ""(visible|invisible)"" and ""(enabled|disabled)"" on the create new form page")]
        public void GivenIVerifyTheFormNameTextboxIsAndOnTheAddNewQuestionElement(string visibility,
            string enabledStatus)
        {
            _createForm.VerifyFormNameTextBoxState(visibility, enabledStatus);
        }

        [Then(
            @"I verify the Show inactive questions checkbox is ""(visible|invisible)"" and ""(enabled|disabled)"" on the create new form page")]
        public void GivenIVerifyTheShowInactiveQuestionsCheckboxIsAndOnTheCreateNewFormPage(string visibility,
            string enabledStatus)
        {
            _createForm.VerifyShowInactiveQuestionsTickBox(visibility, enabledStatus);
        }

        [Then(
            @"I verify the Add new question button is ""(visible|invisible)"" and ""(enabled|disabled)"" on the create new form page")]
        public void GivenIVerifyTheAddNewQuestionButtonIsAndOnTheCreateNewFormPage(string visibility,
            string enabledStatus)
        {
            _createForm.VerifyAddNewQBtnState(visibility, enabledStatus);
        }

        [Then(
            @"I verify the Back button is ""(visible|invisible)"" and ""(enabled|disabled)"" on the create new form page")]
        public void GivenIVerifyTheBackButtonIsAndOnTheCreateNewFormPage(string visibility, string enabledStatus)
        {
            _createForm.VerifyBackBtnState(visibility, enabledStatus);
        }

        [Then(
            @"I verify the Continue form button is ""(visible|invisible)"" and ""(enabled|disabled)"" on the create new form page")]
        public void GivenIVerifyTheSaveFormButtonIsAndOnTheCreateNewFormPage(string visibility, string enabledStatus)
        {
            _createForm.VerifyContinueFormBtnState(visibility, enabledStatus);
        }

        [When(@"I click the Add new question button")]
        public void WhenIClickTheAddNewQuestionButton()
        {
            _createForm.ClickAddNewQBtn();
        }

        [Then(@"I verify the view form columns as:")]
        public void ThenIVerifyTheViewFormColumnsAs(Table table)
        {
            _createForm.VerifyFormColumnsHeader(table);
        }

        [When(@"I enter a unique name for the form")]
        public void WhenIEnterAUniqueNameForTheForm()
        {
            _createForm.EnterUniqueFormName();
        }
        
        [When(@"I enter a form name with '(.*)' alpha, numeric and special characters with an expected max of '(.*)'")]
        public void WhenIEnterAFormNameWithAlphaNumericAndSpecialCharacters(int length, int max)
        {

            _createForm.EnterRandomFormName(length, max);
        }

        [When(@"I tick the per customer flag on the form page")]
        public void WhenITickThePerCustomerFlagOnTheFormPage()
        {
            _createForm.TickPerCustomerCheckBox();
        }

        [When(@"I untick the per customer flag on the form page")]
        public void WhenIUntickThePerCustomerFlagOnTheFormPage()
        {
            _createForm.UntickPerCustomerCheckBox();
        }

        [Then(@"I verify that the per customer flag is ticked on the form page")]
        public void ThenIVerifyThatThePerCustomerFlagIsTickedOnTheFormPage()
        {
            _createForm.VerifyPerCustomerIsTicked();
        }

        [Then(@"I verify that the per customer flag is unticked on the form page")]
        public void ThenIVerifyThatThePerCustomerFlagIsUntickedOnTheFormPage()
        {
            _createForm.VerifyPerCustomerIsUnticked();
        }

    }
}