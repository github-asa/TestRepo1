using J2BIOverseasOps.Pages.BuildingWork;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.BuildingWork
{
    [Binding]
    public sealed class CompletionFormStepDefs : BaseStepDefs
    {
        private readonly CompletionFormPage _completionFormPage;

        public CompletionFormStepDefs(IWebDriver driver, ILog log, IRunData rundata) : base(driver, log)
        {
            _completionFormPage = new CompletionFormPage(driver, log, rundata);
        }

        [When(@"I click the submit button on the work completion form")]
        public void WhenIClickTheSubmitButtonOnTheWorkCompletionForm()
        {
            _completionFormPage.ClickSubmitButton();
        }

        [Then(@"I can see the following mandatory error message on the following completion form fields:")]
        public void ThenICanSeeTheFollowingMandatoryErrorMessageOnTheFollowingCompletionFormFields(Table table)
        {
            _completionFormPage.VerifyCompletionFormValidationErrorMessage(table);
        }

        [When(@"I enter the following answers for the following questions on the the completion form page:")]
        public void WhenIEnterTheFollowingAnswersForTheFollowingQuestionsOnTheTheCompletionFormPage(Table table)
        {
            _completionFormPage.EnterCompletionFormAnswerToGeneralQ(table);
        }

        [Then(@"I verify  the following fields on the completion form page:")]
        public void ThenIVerifyTheFollowingFieldsOnTheCompletionFormPage(Table table)
        {
            _completionFormPage.VerifyCompletionFormFieldValues(table);
        }

    }

}