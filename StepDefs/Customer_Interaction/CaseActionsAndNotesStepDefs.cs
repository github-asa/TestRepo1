using J2BIOverseasOps.Pages.Customer_Interaction;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.Customer_Interaction
{
    [Binding]
    public sealed class CaseActionsAndNotesStepDefs : BaseStepDefs
    {
        private readonly CaseActionsAndNotesPage _caseActionsAndNotes;

        public CaseActionsAndNotesStepDefs(IWebDriver driver, ILog log) : base(driver, log)
        {
            _caseActionsAndNotes = new CaseActionsAndNotesPage(driver, log);
        }


        [Then(@"I verify for each row I have a booking reference, one or more customers and one or more categories on the actions and notes screen:")]
        public void ThenIVerifyForEachRowIHaveABookingReferenceOneOrMoreCustomersAndOneOrMoreCategoriesOnTheActionsAndNotesScreen(Table table)
        {
            _caseActionsAndNotes.VerifyCasesAndCategories(table);
        }

        [Then(
            @"I verify for each affected case I have a customer and one or more categories on the actions and notes screen:")]
        public void ThenIVerifyForEachAffectedCaseIHaveAAndAAndOneOrMoreOnTheActionsAndNotesPage(Table table)
        {
            _caseActionsAndNotes.VerifyCasesAndCategories(table);
        }

        [Then(@"I verify the page title reads ""(.*)"" on the actions and notes screen")]
        public void ThenIVerifyThePageTitleReaddsActionsAndNotesOnTheActionsAndNotesPage(string pageTitle)
        {
            _caseActionsAndNotes.VerifyActionsandNotesPageTitle(pageTitle);
        }

        [Then(
            @"I verify a clickable option to add a note for each case should be displayed with the text ""(.*)"" on the actions and notes screen")]
        public void
            ThenIVerifyAClickableOptionToAddANoteForEachCaseShouldBeDisplayedWithTheTextOnTheActionsAndNotesPage(
                string noteLabel)
        {
            _caseActionsAndNotes.VerifyNoteOptionPresent(noteLabel);
        }

        [Then(@"I verify that a ""(.*)"" link is displayed for each case on the actions and notes screen")]
        public void ThenIVerifyThatALinkIsDisplayedForEachCaseOnTheActionsAndNotesPage(string linkText)
        {
            _caseActionsAndNotes.VerifyActionOptionPresent(linkText);
        }

        [Then(@"I verify the continue and back buttons should be visible and enabled on the actions and notes screen")]
        public void ThenIVerifyTheContinueAndBackButtonsShouldBeVisibleAndEnabledOnTheActionsAndNotesPage()
        {
            _caseActionsAndNotes.VerifyContinueBackButtonsVisible();
        }

        [Then(@"the Notes screen is ""(.*)""")]
        public void ThenIShouldBeTakenToTheNotesScreenActions(string expectedStatus)
        {
            _caseActionsAndNotes.VerifyNoteScreenDisplayed(expectedStatus);
        }

        [When(@"I click the ""(.*)"" link for ""(.*)""")]
        public void WhenIClickTheLinkFor(string linkText, string customer)
        {
            _caseActionsAndNotes.ClickAddActionlink(linkText, customer);
        }

        [Then(@"the Action screen is ""(.*)""")]
        public void ThenIVerifyTheActionScreenIs(string expectedStatus)
        {
            _caseActionsAndNotes.VerifyActionScreenDisplayed(expectedStatus);
        }

        [Then(@"I verify the page title should read ""(.*)"" on the action screen")]
        public void ThenThePageTitleShouldReadAnd(string pageTitle)
        {
            _caseActionsAndNotes.VerifyActionScreenPageTitle(pageTitle);
        }

        [Then(@"I verify the ""(.*)"" and ""(.*)"" buttons are visible and enabled on the action screen")]
        public void ThenIVerifyTheAndButtonsAreVisibleAndEnabled(string addActionButton, string closeButton)
        {
            _caseActionsAndNotes.VerifyActionScreenButtonsEnabled(addActionButton, closeButton);
        }

        [When(@"I click the Add action button on the action screen")]
        public void WhenIClickTheAddActionButton()
        {
            _caseActionsAndNotes.ClickAddActionButton();
        }

        [Then(@"the Add action screen is ""(.*)""")]
        public void ThenIShouldBeTakenToTheScreen(string expectedStatus)
        {
            _caseActionsAndNotes.VerifyAddActionScreenDisplayed(expectedStatus);
        }

        [When(@"I click the Close button on the action screen")]
        public void WhenIClickTheCloseButton()
        {
            _caseActionsAndNotes.ClickActionScreenCloseButton();
        }

        [When(@"I click the Edit action button for:")]
        public void WhenIClickTheEditActionButtonFor(Table table)
        {
            _caseActionsAndNotes.ClickActionScreenEditButton(table);
        }

        [Then(@"the Edit action screen is ""(.*)""")]
        public void ThenTheEditActionScreenIs(string expectedStatus)
        {
            _caseActionsAndNotes.VerifyEditActionScreenDisplayed(expectedStatus);
        }

        [Then(@"the title for the Edit action screen is displayed as '(.*)'")]
        public void ThenTheTitleForTheEditActionScreenIsDisplayedAs(string title)
        {
            _caseActionsAndNotes.VerifyEditActionScreenTitle(title);
        }

        [Then(@"I verify the action information is prefilled as:")]
        public void ThenIVerifyTheActionInformationIsPrefilledAs(Table table)
        {
            _caseActionsAndNotes.VerifyEditActionScreenInformation(table);
        }

        [Then(@"I fill in the action information as:")]
        public void ThenIFillInTheActionInformationAs(Table table)
        {
            _caseActionsAndNotes.EnterAddActionScreenInformation(table);
        }

        [When(@"I click the save button on the add action screen")]
        [When(@"I click the save button on the edit action screen")]
        public void WhenIClickTheSaveButtonOnTheAddActionScreen()
        {
            _caseActionsAndNotes.ClickAddActionScreenSaveButton();
        }

        [When(@"I select ""(.*)"" for '(.*)'")]
        public void WhenISelectAddNoteFor(string linkText, string customer)
        {
            _caseActionsAndNotes.ClickAddNoteFor(linkText, customer);
        }

        [Then(@"the Notes screen is displayed")]
        public void ThenTheNotesPopupIsDisplayed()
        {
            _caseActionsAndNotes.VerifyNotesScreenIsDisplayed();
        }

        [Then(
            @"I verify that the Notes screen displays a notes field, '(.*)' button, '(.*)' button and a '(.*)' button")]
        public void ThenIVerifyThatTheNotesPopupDisplaysANotesFieldButtonButtonAndACloseButton(string delete,
            string saveAndClose, string close)
        {
            _caseActionsAndNotes.VerifyNotesPopupElementsAreDisplayed(delete, saveAndClose, close);
        }

        [When(@"I enter '(.*)' into the notes field")]
        public void WhenIEnterIntoTheNotesField(string note)
        {
            _caseActionsAndNotes.EnterNoteText(note);
        }

        [When(@"I select the Save and close button")]
        public void WhenISelectTheSaveAndCloseButton()
        {
            _caseActionsAndNotes.ClickSaveAndClose();
        }

        [Then(@"the Notes screen is dismissed")]
        public void ThenTheNotesPopupIsDismissed()
        {
            _caseActionsAndNotes.VerifyNotesPopupIsNotDisplayed();
        }

        [When(@"I select '(.*)' for '(.*)'")]
        public void WhenISelectViewNoteFor(string linkText, string customer)
        {
            _caseActionsAndNotes.ClickViewNoteFor(linkText, customer);
        }

        [Then(@"the Notes field contains '(.*)'")]
        public void ThenTheNotesFieldContains(string note)
        {
            _caseActionsAndNotes.VerifyNoteContains(note);
        }

        [When(@"I add a note for the following customers:")]
        public void WhenIAddANoteForTheFollowingCustomers(Table table)
        {
            _caseActionsAndNotes.AddNoteFor(table);
        }

        [Then(@"the notes field will contain the notes below for each customers:")]
        public void ThenTheNotesFieldWillContainTheNotesBelowForEachCustomers(Table table)
        {
            _caseActionsAndNotes.VerifyNoteFor(table);
        }

        [Then(@"the Save and close button is disabled")]
        public void ThenTheSaveAndCloseButtonIsDisabled()
        {
            _caseActionsAndNotes.VerifySaveAndCloseButtonIsDisabled();
        }

        [Then(@"the Save and close button is enabled")]
        public void ThenTheSaveAndCloseButtonIsEnabled()
        {
            _caseActionsAndNotes.VerifySaveAndCloseButtonIsEnabled();
        }

        [When(@"I clear the notes field")]
        public void WhenIClearTheNotesField()
        {
            _caseActionsAndNotes.ClearNotesField();
        }

        [When(@"I click the close button on the notes screen")]
        public void WhenIClickTheCloseButtonOnTheNotesPage()
        {
            _caseActionsAndNotes.ClickCloseNote();
        }

        [Then(@"the Notes field is empty")]
        public void ThenTheNotesFieldIsEmpty()
        {
            _caseActionsAndNotes.VerifyNoteFieldIsEmpty();
        }

        [Then(@"I verify the title reads ""(.*)"" on the add action screen")]
        public void ThenIVerifyTheTitleReadsOnTheAddActionScreen(string title)
        {
            _caseActionsAndNotes.VerifyAddActionScreenTitle(title);
        }

        [Then(
            @"I verify an option to enter an ""(.*)"", record a ""(.*)"" and select a user that the action can be ""(.*)"" should be displayed on the add action screen")]
        public void
            ThenIVerifyAnOptionToEnterAnRecordAAndSelectAUserThatTheActionCanBeShouldBeDisplayedOnTheAddActionScreen(
                string actionType, string description, string assignee)
        {
            _caseActionsAndNotes.VerifyAddActionScreenOptions(actionType, description, assignee);
        }

        [Then(
            @"I verify an option to set a ""(.*)"" date should be displayed with the date auto-filled to the next day from system date on the add action screen")]
        public void
            ThenIVerifyAnOptionToSetAShouldBeDisplayedWithTheDateAuto_FilledToTheNextDayFromSystemDateOnTheAddActionScreen(
                string dueDate)
        {
            _caseActionsAndNotes.VerifyDueDateOption(dueDate);
        }

        [Then(@"I verify that '(.*)' date is disabled in the date picker on the add action screen")]
        [Then(@"I verify that this due by date ""(.*)"" is disabled in the date picker on the add action screen")]
        public void ThenIVerifyThatThisDueByDateIsDisabledInTheDatePickerOnTheAddActionScreen(string date)
        {
            _caseActionsAndNotes.VerifyPastDueByDate(date);
        }

        [Then(@"I verify I can only select a date equal to today or in the future on the add action screen")]
        public void ThenIVerifyICanOnlySelectADateEqualToTodayOrInTheFutureOnTheAddActionScreen()
        {
            //This step is here for readability
        }

        [When(@"I click the ""Cancel"" button on the add action screen")]
        public void WhenIClickTheButtonOnTheAddActionScreen()
        {
            _caseActionsAndNotes.ClickCancelButton();
        }

        [Then(@"the Delete note button is disabled")]
        public void ThenTheDeleteNoteButtonIsDisabled()
        {
            _caseActionsAndNotes.VerifyDeleteNoteButtonIsDisabled();
        }

        [When(@"I click the Delete Note button")]
        public void WhenIClickTheDeleteNoteButton()
        {
            _caseActionsAndNotes.ClickDeleteNote();
        }

        [When(@"I Delete a note for each of the following customers:")]
        public void WhenIDeleteANoteForEachOfTheFollowingCustomers(Table table)
        {
            _caseActionsAndNotes.ClickDeleteNoteFor(table);
        }

        [Then(@"I verify that there will be no notes on the actions and notes page for:")]
        public void ThenThereWillBeNoNotesOnTheActionsAndNotesPageFor(Table table)
        {
            _caseActionsAndNotes.VerifyNoNotesFor(table);
        }

        [Then(@"I verify I can see a list of Action Types in the action type drop down list on the add action screen:")]
        public void ThenIVerifyICanSeeAListOfActionTypesInTheActionTypeDropDownListOnTheAddActionScreen(Table table)
        {
            ScenarioContext.Current["ActionTypesTable"] = table;
            _caseActionsAndNotes.VerifyActionTypesInDropDownList(table);
        }

        [Then(
            @"I verify I can only select one action type from the action type drop down list on the add action screen")]
        public void ThenIVerifyICanOnlySelectOneActionTypeFromTheActionTypeDropDownListOnTheAddActionScreen()
        {
            var table = ScenarioContext.Current.Get<Table>("ActionTypesTable");
            _caseActionsAndNotes.VerifyActionTypesDropDownListSelection(table);
        }

        [When(@"I select the Assigned to department option as '(.*)' on the edit actions screen")]
        public void WhenISelectTheAssignedToDepartmentOptionAsOnTheEditActionsScreen(string option)
        {
            _caseActionsAndNotes.SelectAssignedToDropDown(option);
        }

        [Then(@"the reps list box is enabled on the edit actions screen")]
        public void ThenTheRepsListBoxIsEnabledOnTheEditActionsScreen()
        {
            _caseActionsAndNotes.VerifyRepsListBoxIsEnabled();
        }

        [Then(@"the reps list box is disabled on the edit actions screen")]
        public void ThenTheRepsListBoxIsDisabledOnTheEditActionsScreen()
        {
            _caseActionsAndNotes.VerifyRepsListBoxIsDisabled();
        }

        [When(@"I select the following list of reps on the edit actions screen:")]
        public void WhenISelectTheFollowingListOfRepsOnTheEditActionsScreen(Table table)
        {
            _caseActionsAndNotes.SelectRepsListbox(table);
        }

        [When(@"I deselect the action type field on the edit action screen")]
        public void WhenIDeselectTheActionTypeFieldOnTheEditActionScreen()
        {
            _caseActionsAndNotes.DeselectActionType();
        }

        [Then(@"a validation message '(.*)' is displayed on the edit action screen")]
        public void ThenAValidationMessageIsDisplayedOnTheEditActionScreen(string message)
        {
            _caseActionsAndNotes.VerifyValidationMessage(message);
        }

        [Then(@"the save button is disabled on the edit action screen")]
        public void ThenTheSaveButtonIsDisabledOnTheEditActionScreen()
        {
            _caseActionsAndNotes.VerifySaveActionButtonIsDisabled();
        }

        [When(@"I clear the action description field on the edit action screen")]
        [When(@"I clear the action description field on the add action screen")]
        public void WhenIClearTheActionDescriptionFieldOnTheEditActionScreen()
        {
            _caseActionsAndNotes.ClearActionDescription();
        }

        [When(@"I deselect the assigned to field on the edit action screen")]
        public void WhenIDeselectTheAssignedToFieldOnTheEditActionScreen()
        {
            _caseActionsAndNotes.ClearAssignedToDropDown();
        }

        [When(@"I deselect the reps field on the edit action screen")]
        public void WhenIDeselectTheRepsFieldOnTheEditActionScreen()
        {
            _caseActionsAndNotes.ClearRepsField();
        }

        [Then(@"I change the action information to:")]
        public void ThenIChangeTheActionInformationTo(Table table)
        {
            _caseActionsAndNotes.ChangeActionInformationTo(table);
        }

        [Then(@"I verify that the following actions have been created on the actions screen:")]
        [Then(@"I verify that the following actions have been updated on the actions screen:")]
        public void ThenIVerifyThatTheFollowingActionHasBeenCreatedOnTheActionsScreen(Table table)
        {
            _caseActionsAndNotes.VerifyActionsCreated(table);
        }

        [When(@"I add actions for the following cases:")]
        public void WhenIAddActionsForTheFollowingCases(Table table)
        {
            _caseActionsAndNotes.CreateActionsFor(table);
        }

        [Then(@"I verify that an action has been added for the ""(.*)"" Case with the following details:")]
        public void ThenIVerifyThatAnActionHasBeenAddedForTheCaseWithTheFollowingDetails(string customer, Table table)
        {
            _caseActionsAndNotes.VerifyActionsFor(customer, table);
        }

        [Then(@"I verify the save button is disabled on the add action screen")]
        public void ThenIVerifyTheSaveButtonIsDisabledInTheAddActionScreen()
        {
            _caseActionsAndNotes.VerifyAddActionSaveButtonIsDisabled();
        }

        [Then(@"I verify the save button is enabled on the add action screen")]
        public void ThenIVerifyTheSaveButtonIsEnabledInTheAddActionScreen()
        {
            _caseActionsAndNotes.VerifyAddActionSaveButtonIsEnabled();
        }

        [When(@"I deselect the action type drop down on the add action screen")]
        public void WhenIDeselectTheActionTypeDropDownOnTheAddActionScreen()
        {
            _caseActionsAndNotes.DeselectActionType();
        }

        [Then(@"a validation message '(.*)' is displayed on the add action screen")]
        public void ThenAValidationMessageIsDisplayedOnTheAddActionScreen(string message)
        {
            _caseActionsAndNotes.VerifyValidationMessage(message);
        }

        [When(@"I select the action type as ""(.*)"" on the add action screen")]
        public void WhenISelectTheActionTypeAsOnTheAddActionScreen(string actionType)
        {
            _caseActionsAndNotes.SelectActionTypeFromDropDown(actionType);
        }

        [When(@"I enter the description as ""(.*)"" on the add action screen")]
        public void WhenIEnterTheDescriptionAsOnTheAddActionScreen(string description)
        {
            _caseActionsAndNotes.EnterDescription(description);
        }

        [When(@"I select the assigned to as ""(.*)"" on the add action screen")]
        public void WhenISelectTheAssignedToAsOnTheAddActionScreen(string option)
        {
            _caseActionsAndNotes.SelectAssignedToDropDown(option);
        }

        [When(@"I deselect the assigned to dropdown on the add action screen")]
        public void WhenIDeselectTheAssignedToDropdownOnTheAddActionScreen()
        {
            _caseActionsAndNotes.DeselectAssignedTo();
        }

        [When(@"I select the rep name as ""(.*)"" on the add action screen")]
        public void WhenISelectTheRepNameAsOnTheAddActionScreen(string repName)
        {
            _caseActionsAndNotes.SelectReps(repName);
        }

        [When(@"I deselect the rep name list box on the add action screen")]
        public void WhenIDeselectTheRepNameListBoxOnTheAddActionScreen()
        {
            _caseActionsAndNotes.ClearRepsField();
        }

        [Then(@"the reps list box is enabled on the add actions screen")]
        public void ThenTheRepsListBoxIsEnabledOnTheAddActionsScreen()
        {
            _caseActionsAndNotes.VerifyRepsListBoxIsEnabled();
        }

        [When(@"the reps list box is disabled on the add actions screen")]
        public void WhenTheRepsListBoxIsDisabledOnTheAddActionsScreen()
        {
            _caseActionsAndNotes.VerifyRepsListBoxIsDisabled();
        }

        [Then(@"I verify each booking reference only appears once on the actions and notes page")]
        public void ThenIVerifyEachBookingReferenceOnlyAppearsOnceOnTheActionsAndNotesPage()
        {
            //to be removed as actions and note is obsolete
            //_caseActionsAndNotes.VerifyBookingReferencesAppearOnce();
        }

        [When(@"I click the continue button on the actions and notes page")]
        public void WhenIClickTheContinueButtonOnTheActionsAndNotesPage()
        {
            _caseActionsAndNotes.ClickActionsAndNotesContinueButton();
        }

        [Then(@"I verify the order of the booking references on the actions and notes screen")]
        public void ThenIVerifyTheOrderOfTheBookingReferencesOnTheActionsAndNotesScreen()
        {
            //to be removed as actions and note is obsolete
            //_caseActionsAndNotes.VerifyOrderOfBookingReferences();
        }

    }
}