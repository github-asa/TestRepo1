using J2BIOverseasOps.Pages.FormManagement;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.FormManagement
{
    [Binding]
    public sealed class FormManagementStepDefs : BaseStepDefs
    {
        private readonly FormManagementPage _formMgmtPage;

        public FormManagementStepDefs(IWebDriver driver, ILog log) : base(driver, log)
        {
            _formMgmtPage = new FormManagementPage(Driver, Log);
        }

        [Then(@"I verify the page title as ""(.*)"" on the form management page")]
        public void GivenIVerifyThePageTitleAsOnTheFormManagementPage(string expectedTitle)
        {
            _formMgmtPage.VerifyPageTitle(expectedTitle);
        }

        [Then(
            @"I verify a search textbox is ""(visible|invisible)"" and ""(enabled|disabled)"" on the form management page")]
        public void GivenIVerifyASearchTextboxIsAndOnTheFormManagementPage(string visibility, string able)
        {
            _formMgmtPage.VerifySearchTextBoxState(visibility, able);
        }

        [Then(
            @"I verify show inactive forms checkbox is ""(visible|invisible)"",""(enabled|disabled)"" and ""(checked|unchecked)"" on the form management page")]
        public void GivenIVerifyShowInactiveFormsCheckboxIsAndOnTheFormManagementPage(string visibilityState,
            string enbaledState, string checkedState)
        {
            _formMgmtPage.VerifyInactiveFormsCheckBoxState(visibilityState, enbaledState, checkedState);
        }

        [Then(
            @"I verify show locked forms checkbox is ""(visible|invisible)"",""(enabled|disabled)"" and ""(checked|unchecked)"" on the form management page")]
        public void GivenIVerifyShowLockedFormsCheckboxIsAndOnTheFormManagementPage(string visibilityState,
            string enbaledState, string checkedState)
        {
            _formMgmtPage.VerifyLockedFormsCheckBoxState(visibilityState, enbaledState, checkedState);
        }

        [Then(@"I verify the following columns are displayed in following order on the form management page:")]
        public void GivenIVerifyTheFollowingColumnsAreDisplayedOnTheFormManagementPage(Table table)
        {
            _formMgmtPage.VerifyFormManagementColumns(table);
        }

        [Then(
            @"I verify the create a new form button is ""(visible|invisible)"" and ""(enabled|disabled)"" on the form management page")]
        public void GivenIVerifyTheCreateANewFormButtonIsAndOnTheFormManagementPage(string visibilityState,
            string enbaledState)
        {
            _formMgmtPage.VerifyCreateNewFormButtonState(visibilityState, enbaledState);
        }

        [Then(
            @"I verify the Back button is ""(visible|invisible)"" and ""(enabled|disabled)"" on the form management page")]
        public void GivenIVerifyTheCreateANewBackButtonIsAndOnTheFormManagementPage(string visibilityState,
            string enbaledState)
        {
            _formMgmtPage.VerifyBackButtonState(visibilityState, enbaledState);
        }

        [When(@"I click the create a new form button")]
        public void WhenIClickTheCreateANewForm()
        {
            _formMgmtPage.ClickCreateFormBtn();
        }

        [When(@"I click the edit link for the form name ""(.*)"" on the form management page")]
        public void WhenIClickTheEditLinkForTheFormNameOnTheFormManagementPage(string formName)
        {
            _formMgmtPage.ClickEditLink(formName);
        }

        [When(@"I click the View button for the form ""(.*)"" on the form management page")]
        public void WhenIClickTheViewButtonForTheForm(string formName)
        {
            _formMgmtPage.ClickViewLink(formName);
        }

        [When(@"I click the View button for the form on the form management page")]
        public void WhenIClickTheViewButtonForTheForm()
        {
            _formMgmtPage.ClickViewLink();
        }

        [When(@"I click the edit link for the newly created form on the form management page")]
        public void WhenIClickTheEditLinkForTheNewlyCreatedFormOnTheFormManagementPage()
        {
            _formMgmtPage.ClickEditLink();
        }

        [When(@"I click the view link for the form on the form management page")]
        public void ClickViewLink()
        {
            _formMgmtPage.ClickViewLink();
        }

        [When(@"I click the edit link for the form version ""(.*)"" on the form management page")]
        public void WhenIClickTheEditLinkForTheFormVersionOnTheFormManagementPage(string version)
        {
            _formMgmtPage.ClickEditLink(string.Empty, version);
        }

        [Then(@"I verify the form name as ""(.*)""")]
        public void ThenIVerifyTheFormNameAs(string expectedName)
        {
            _formMgmtPage.VerifyFormName(expectedName);
        }

        [Then(@"I verify the form name")]
        public void ThenIVerifyTheFormNameAs()
        {
            _formMgmtPage.VerifyFormName();
        }

        [Then(@"I verify the newly created form is displayed with following:")]
        public void ThenIVerifyTheNewlyCreatedFormIsDisplayedWithFollowing(Table table)
        {
            _formMgmtPage.VerifyFormValues(table);
        }

        [Then(@"I verify the newly updated form is displayed with following:")]
        public void ThenIVerifyTheNewlyUpdatedFormIsDisplayedWithFollowing(Table table)
        {
            var version = table.Rows[0]["version"];
            _formMgmtPage.VerifyFormValues(table, versionNumber: version);            
        }

        [Then(@"I verify the newly has name of the correct length")]
        public void ThenIVerifyTheNewlyCreatedFormIsDisplayedWithANameThatHasAMaximumOfCharacters()
        {
            _formMgmtPage.VerifyFormName();
        }

        [Then(@"I verify the form version ""(.*)"" is displayed with following:")]
        public void ThenIVerifyTheNewlyCreatedFormVersionIsDisplayedWithFollowing(string versionNumber, Table table)
        {
            _formMgmtPage.VerifyFormValues(table, string.Empty, versionNumber);
        }

        [When(@"I update the following fields for the newly created form:")]
        public void ThenIUpdateTheFollowingFielsForTheNewlyCreatedForm(Table table)
        {
            _formMgmtPage.UpdateFormValues(table);
        }

        [When(@"I update the following fields for the form ""(.*)"":")]
        public void WhenIUpdateTheFollowingFieldsForTheForm(string formName, Table table)
        {
            _formMgmtPage.UpdateFormValues(table, formName);
        }

        [When(@"I update the following fields for version ""(.*)"" of the form:")]
        public void WhenIUpdateTheFollowingFieldsForTheFormVersion(string version, Table table)
        {
            _formMgmtPage.UpdateFormValues(table, version: version);
        }

        [When(@"I select the Show locked forms tick box as ""(True|False)""")]
        public void WhenISelectTheShowLockedFormsTickBoxAs(string tickBoxOption)
        {
            _formMgmtPage.ShowLockedForms(tickBoxOption);
        }

        [When(@"I select the Show inactive forms tick box as ""(True|False)""")]
        public void WhenISelectTheShowInactiveFormsTickBoxAs(string tickBoxOption)
        {
            _formMgmtPage.ShowInactiveForms(tickBoxOption);
        }

        [When(@"I tick the checkbox to activate version ""(.*)"" of the form on the form management page")]
        public void WhenITickTheCheckboxToActivateVersionOfTheFormOnTheFormManagementPage(string version)
        {
            _formMgmtPage.ActivateForm(version);
        }

        [When(@"I tick the checkbox to activate version ""(.*)"" of form ""(.*)"" on the form management page")]
        public void WhenITickTheCheckboxToActivateVersionOfTheFormOnTheFormManagementPage(string version, string formName)
        {
            _formMgmtPage.ActivateForm(version, formName);
        }

        [When(@"I tick the checkbox to activate version ""(.*)"" of form on the form management page")]
        public void WhenITickTheCheckboxToActivateVersionOfTheUniqueFormOnTheFormManagementPage(string version)
        {
            _formMgmtPage.ActivateForm(version);
        }

        [When(@"I enter ""(.*)"" in the search form input box")]
        public void WhenIEnterInTheSearchFormInputBox(string formName)
        {
            _formMgmtPage.SearchFormName(formName);
        }

        [When(@"I search for the form on the form management page")]
        public void WhenIEnterInTheUniqueSearchFormInputBox()
        {
            _formMgmtPage.SearchFormName();
        }

        [Then(@"I verify I can only see the ""(.*)"" on the list")]
        public void ThenIVerifyICanOnlySeeTheOnTheList(string formName)
        {
            _formMgmtPage.VerifyNumberOfResultsDisplayed(1);
            _formMgmtPage.VerifyFormDisplayed(formName);
        }

        [Then(@"I verify I can only see the ""(.*)"" on the list")]
        [Then(@"I verify I can only see the form on the list")]
        public void ThenIVerifyICanOnlySeeTheUniqueOnTheList()
        {
            _formMgmtPage.VerifyNumberOfResultsDisplayed(1);
            _formMgmtPage.VerifyFormDisplayed();
        }

        [Then(@"I verify I can not see any result on the search result")]
        public void ThenIVerifyICanNotSeeAnyResultOnTheSearchResult()
        {
            _formMgmtPage.VerifyNumberOfResultsDisplayed(0);
        }

        [Then(@"I verify the form is not displayed on the list")]
        public void ThenIVerifyTheFormIsNotDisplayedOnTheList()
        {
            _formMgmtPage.VerifyFormNotDisplayed();
        }

        [Then(@"I verify I can not see the form ""(.*)"" on the list of forms displayed")]
        public void ThenIVerifyICanNotSeeTheFormOnTheListOfFormsDisplayed(string formName)
        {
            _formMgmtPage.VerifyFormNotDisplayed(formName);
        }

        [Then(@"I verify I can not see the form on the list of forms displayed")]
        public void ThenIVerifyICanNotSeeTheFormOnTheListOfFormsDisplayed()
        {
            _formMgmtPage.VerifyFormNotDisplayed();
        }

        [When(@"I verify I cannot see the version ""(.*)"" for the newly updated form")]
        public void WhenIVerifyICannotSeeTheVersionForTheNewlyUpdatedForm(string version)
        {
            _formMgmtPage.VerifyFormVersionNotDisplayed(version);
        }

        [Then(@"I verify I cannot see the version ""(.*)"" for the newly created form")]
        public void WhenIVerifyICannotSeeTheVersionForTheNewlyCreatedForm(string version)
        {
            _formMgmtPage.VerifyFormVersionNotDisplayed(version);
        }

        [Then(@"I verify I can see the form ""(.*)"" on the list of forms displayed")]
        public void ThenIVerifyICanSeeTheFormOnTheListOfFormsDisplayed(string formName)
        {
            _formMgmtPage.VerifyFormDisplayed(formName);
        }

        [Then(@"I verify the form ""(.*)"" is greyed out")]
        public void ThenIVerifyTheFormIsGreyedOut(string formName)
        {
            _formMgmtPage.VerifyFormGreyedOut(formName);
        }

        [Then(@"I verify the form ""(.*)"" is not greyed out")]
        public void ThenIVerifyTheFormIsNotGreyedOut(string formName)
        {
            _formMgmtPage.VerifyFormNotGreyedOut(formName);
        }

        [Then(@"I verify the form is greyed out")]
        public void ThenIVerifyTheUniqueFormIsGreyedOut()
        {
            _formMgmtPage.VerifyFormGreyedOut();
        }

        [Then(@"I verify the form is not greyed out")]
        public void ThenIVerifyTheUniqueFormIsNotGreyedOut()
        {
            _formMgmtPage.VerifyFormNotGreyedOut();
        }

        [Then(@"I verify I can see the ""(View|Edit)"" button for the form ""(.*)""")]
        public void ThenIVerifyICanSeeTheButtonForTheForm(string buttonType, string formName)
        {
            _formMgmtPage.VerifyButtonDisplayed(buttonType, formName);
        }

        [Then(@"I verify I can see the ""(View|Edit)"" button for the form")]
        public void ThenIVerifyICanSeeTheButtonForTheForm(string buttonType)
        {
            _formMgmtPage.VerifyButtonDisplayed(buttonType);
        }

        [When(@"I clear the form name input field")]
        public void WhenIClearTheFormNameInputField()
        {
            _formMgmtPage.ClearFormNameField();
        }
    }
}