using J2BIOverseasOps.Pages.FormManagement;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.FormManagement
{
    [Binding]
    public sealed class LinkFormToCatgStepDefs : BaseStepDefs
    {
        private readonly LinkFormToCategoryPage _linkFormToCatgPage;

        public LinkFormToCatgStepDefs(IWebDriver driver, ILog log) : base(driver, log)
        {
            _linkFormToCatgPage = new LinkFormToCategoryPage(driver, log);
        }

        [Then(@"I should be navigated to Link form to Categories page")]
        public void ThenIShouldBeNavigatedToLinkFormToCategoriesPage()
        {
            _linkFormToCatgPage.VerifyNavigatedToLinkFormToCatgPage();
        }

        [When(@"I select the form category")]
        public void WhenISelectTheFormCategory()
        {
            _linkFormToCatgPage.SelectCategory();
        }

        [When(@"I click the save form button on the form to categories page")]
        public void WhenIClickTheSaveFormButtonOnTheFormToCategoriesPage()
        {
            _linkFormToCatgPage.ClickSaveFormButton();
        }

        [When(@"I enter the form changes summary")]
        public void WhenIEnterTheFormChangesSummary()
        {
            _linkFormToCatgPage.EnterChangesSummary();
        }

        [Then(@"I verify the Save form button is ""(enabled|disabled)"" on the categories page")]
        public void ThenIVerifyTheSaveFormButtonIsOnTheCategoriesPage(string enabledDisabled)
        {
            _linkFormToCatgPage.VerifySaveFormBtnState(enabledDisabled);
        }

        [When(@"I enter the form changes summary as ""(.*)""")]
        public void WhenIEnterTheFormChangesSummaryAs(string formChangesSummary)
        {
            _linkFormToCatgPage.EnterChangesSummary(formChangesSummary);
        }

        [Then(@"I verify the form changes summary as ""(.*)""")]
        public void ThenIVerifyTheFormChangesSummaryAs(string expected)
        {
            _linkFormToCatgPage.VerifyFormChangesSummary(expected);
        }

        [When(@"I click the Back button on the select Categories page")]
        public void WhenIClickTheBackButtonOnTheSelectCategoriesPage()
        {
            _linkFormToCatgPage.ClickBackButton();
        }

    }
}