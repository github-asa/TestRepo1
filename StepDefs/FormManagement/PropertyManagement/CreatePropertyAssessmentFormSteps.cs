using J2BIOverseasOps.Extensions;
using J2BIOverseasOps.Pages.FormManagement;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.FormManagement.PropertyManagement
{
    [Binding]
    public class CreatePropertyAssessmentFormSteps : BaseStepDefs
    {
        private readonly FormManagementPage _formManagementPage;

        private readonly CreateFormPage _createFormPage;

        public CreatePropertyAssessmentFormSteps(IWebDriver driver, ILog log) : base(driver, log)
        {
            _formManagementPage = new FormManagementPage(driver, log);
            _createFormPage = new CreateFormPage(driver, log);          
        }

        [Then(@"I verify that the forms creation popup is displayed on the form management page")]
        public void ThenIVerifyThatTheFormCreationPopupIsDisplayedOnTheFormManagementPage()
        {
            _formManagementPage.VerifyCreationPopupIsDisplayed();
        }

        [When(@"I click the '(.*)' button on the forms creation popup")]
        public void WhenIClickTheButtonOnTheForCreationPopup(string buttonText)
        {
            _formManagementPage.ClickCreateNewFormButtonOnPopUp(buttonText);
        }

        [Then(@"I verify that the form creation popup is dismissed on the form management page")]
        public void ThenIVerifyThatTheFormCreationPopupIsDismissedOnTheFormManagementPage()
        {
            _formManagementPage.VerifyThatTheFormCreationPopupIsNotDisplayed();
        }

        [When(@"I click cancel on the forms creation popup")]
        public void WhenIClickCancelOnTheFormsCreationPopup()
        {
            _formManagementPage.ClickCancelOnFormsCreationPopup();
        }

        [When(@"I verify that the '(.*)' save button is displayed on the property assessment form page")]
        public void WhenIVerifyThatTheSaveButtonIsDisplayedOnThePropertyAssessmentFormPage(string buttonText)
        {
            _createFormPage.VerifySaveButtonIsDisplayed(buttonText);
        }

        [When(@"I click the save button on the property assessment form page")]
        public void WhenIClickTheSaveButtonOnThePropertyAssessmentFormPage()
        {
            _createFormPage.ClickSaveButton();
        }

        [Then(@"the save form button is disabled on the property assessment form page")]
        public void ThenTheSaveFormButtonIsDisabled()
        {
            _createFormPage.VerifySaveButtonIsDisabled();
        }

    }
}