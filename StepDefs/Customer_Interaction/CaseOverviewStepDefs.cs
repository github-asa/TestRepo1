using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using J2BIOverseasOps.Pages.Customer_Interaction;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.Customer_Interaction
{
    [Binding]
    public sealed class CaseOverviewStepDefs : BaseStepDefs
    {
        private readonly CaseOverviewPage _caseOverview;

        public CaseOverviewStepDefs(IWebDriver driver, ILog log, IRunData runData) : base(driver, log)
        {
            _caseOverview = new CaseOverviewPage(driver, log, runData);
        }
        
        [Then(@"I verify the assignees are displayed in alphabetical order by first name and then surname, and then username on the case overview page")]
        public void ThenIVerifyTheAssigneesAreDisplayedInAlphabeticalOrderByFirstNameAndThenSurnameAndThenUsername()
        {
            _caseOverview.VerifyAssigneesOrderCaseOverview();
        }

        [Then(@"I verify I can see the following assignees in the assignee list:")]
        public void ThenIVerifyICanSeeTheFollowingAssigneesInTheAssigneeList(Table table)
        {
            _caseOverview.VerifyAssigneesDisplayed(table);
        }

        [When(@"I click the ellipsis button to add more assignees on the on the case overview page")]
        public void WhenIClickTheEllipsisButtonToAddMoreAssigneesOnTheOnTheCaseOverviewPage()
        {
            _caseOverview.ClickAddAssigneesEllipsisButton();
        }

        [Then(@"the edit Jet2Holidays assignees screen is displayed")]
        public void ThenTheEditJetHolidaysAssigneesScreenIsDisplayed()
        {
            _caseOverview.VerifyEditAssigneesScreenDisplayed();
        }

        [Then(
            @"I verify the list of assignees displays usernames below all users whose first name and surname does exist on the edit Jet2Holidays assignees screen")]
        public void
            ThenIVerifyTheListOfAssigneesDisplaysUsernamesBelowAllUsersWhoseFirstNameAndSurnameDoesExistOnTheEditJetHolidaysAssigneesScreen()
        {
            _caseOverview.VerifyAssigneesOrderEditAssignees();
        }

        [When(@"I select the following assignees from the edit Jet2Holidays assignees screen:")]
        [Then(@"I select the following assignees from the edit Jet2Holidays assignees screen:")]
        public void ThenISelectTheFollowingAssigneesFromTheEditJetHolidaysAssigneesScreen(Table table)
        {
            _caseOverview.SelectCaseAssignees(table);
        }

        [When(@"I click the save and close button on the edit Jet2Holidays assignees screen")]
        public void WhenIClickTheSaveAndCloseButtonOnTheEditJetHolidaysAssigneesScreen()
        {
            _caseOverview.ClickEditAssigneesSaveAndCloseButton();
        }

        [Then(@"the edit Jet2Holidays assignees screen is not displayed")]
        public void ThenTheEditJetHolidaysAssigneesScreenIsNotDisplayed()
        {
            _caseOverview.VerifyEditAssigneesScreenNotDisplayed();
        }

        [Then(@"I verify the assignees already assigned to the case are:")]
        public void ThenIVerifyTheAssigneesAlreadyAssignedToTheCaseAre(Table table)
        {
            _caseOverview.VerifySelectedCaseAssignees(table);
        }

        [Then(@"I deselect the following assignees from the edit Jet2Holidays assignees screen:")]
        public void ThenIDeselectTheFollowingAssigneesFromTheEditJetHolidaysAssigneesScreen(Table table)
        {
            _caseOverview.DeselectCaseAssignees(table);
        }

        [Then(@"I verify there are no assignees assigned to the case")]
        public void ThenIVerifyThereAreNoAssigneesAssignedToTheCase()
        {
            _caseOverview.VerifyNoAssigneesAssigned();
        }

        [Then(@"I verify that the heading for assigning a case is ""(.*)"" on the case overview page")]
        public void ThenIVerifyThatTheHeadingForAssigningACaseIsOnTheCaseOverviewPage(string assigneeHeader)
        {
            _caseOverview.VerifyAssigneeHeader(assigneeHeader);
        }

        [Then(@"I verify the Save and Close button is disabled on the edit Jet2Holidays assignees screen")]
        public void ThenIVerifyTheSaveAndCloseButtonIsDisabledOnTheEditJetHolidaysAssigneesScreen()
        {
            _caseOverview.VerifyEditAssigneesSaveAndCloseButtonDisabled();
        }

        [Then(@"I verify the Save and Close button is enabled on the edit Jet2Holidays assignees screen")]
        public void ThenIVerifyTheSaveAndCloseButtonIsEnabledOnTheEditJetHolidaysAssigneesScreen()
        {
            _caseOverview.VerifyEditAssigneesSaveAndCloseButtonEnabled();
        }

        [When(@"I click the close button on the edit Jet2Holidays assignees screen")]
        public void WhenIClickTheCloseButtonOnTheEditJetHolidaysAssigneesScreen()
        {
            _caseOverview.ClickEditAssigneesCloseButton();
        }

        [When(@"I click the create a case button on the overview page")]
        public void WhenIClickTheCreateACaseButtonOnTheOverviewPage()
        {
            _caseOverview.ClickCreateACaseButton();
        }

        [Then(@"a validation message '(.*)' is displayed on the overview page")]
        public void ThenAValidationMessageIsDisplayedOnTheOverviewPage(string message)
        {
            _caseOverview.VerifyValidationMessage(message);
        }

        [Then(@"I verify the create a case button is enabled on the overview page")]
        public void ThenIVerifyTheCreateACaseButtonIsEnabledOnTheOverviewPage()
        {
            _caseOverview.VerifyCreateACaseButtonEnabled();
        }

        [Then(@"I verify the create a case button is disabled on the overview page")]
        public void ThenIVerifyTheCreateACaseButtonIsDisabledOnTheOverviewPage()
        {
            _caseOverview.VerifyCreateACaseButtonDisabled();
        }

        [Then(@"I verify the text on the create a case button is ""(.*)"" on the overview page")]
        public void ThenIVerifyTheTextOnTheCreateACaseButtonIsOnTheOverviewPage(string buttonText)
        {
            _caseOverview.VerifyCreateACaseButtonText(buttonText);
        }

        [When(@"I retrieve the overview of the case from the getcaseoverview API")]
        public void WhenIRetrieveTheOverviewOfTheCaseFromTheGetcaseoverviewAPI()
        {
            _caseOverview.GetCaseOverviewApiCall();
        }

        [Then(@"I verify the status of the case is ""(.*)""")]
        public void ThenIVerifyTheStatusOfTheCaseIs(int state)
        {
            _caseOverview.CheckCaseState(state);
        }

        [Then(@"I verify the correct Case ID is displayed on the overview page")]
        public void ThenIVerifyTheCorrectCaseIDIsDisplayedOnTheOverviewPage()
        {
            _caseOverview.VerifyCorrectCaseIdDisplayed();
        }

        [When(@"I retrieve the assignees from the getcaseautoassignees API")]
        public void WhenIRetrieveTheAssigneesFromTheGetcaseautoassigneesAPI()
        {
            _caseOverview.GetCaseAutoAssigneesApiCall();
        }

        [Then(@"I verify the assignees assigned to the case match the assignees retreived from the getcaseautoassignees API")]
        public void ThenIVerifyTheAssigneesAssignedToTheCaseMatchTheAssigneesRetreivedFromTheGetcaseautoassigneesAPI()
        {
            _caseOverview.VerifyAssigneesMatchGetCaseAutoAssigneesApi();
        }

        [When(@"I retrieve the assignees from the availableassignees API")]
        public void WhenIRetrieveTheAssigneesFromTheAvailableassigneesAPI()
        {
            _caseOverview.GetAvailableAssigneesApiCall();
        }

        [Then(@"I verify the Initial summary text displayed on the case overview page as:")]
        public void ThenIVerifyTheInitialSummaryTextDisplayedOnTheCaseOverviewPageAs(Table table)
        {
            _caseOverview.VerifyInitialSummaryText(table);
        }

        [Then(@"I verify the case notes text is displayed on the case overview page as:")]
        public void ThenIVerifyTheCaseNotesTextIsDisplayedOnTheCaseOverviewPageAs(Table table)
        {
            _caseOverview.VerifyNotesText(table);
        }

        [Then(@"I verify the Intial summary header is displayed as ""(.*)"" on the case overview page")]
        public void ThenIVerifyTheIntialSummaryHeaderIsDisplayedAsOnTheCaseOverviewPage(string initialSummaryHeader)
        {
            _caseOverview.VerifyInitialSummaryHeader(initialSummaryHeader);
        }

        [Then(@"I verify the case notes header is displayed as ""(.*)"" on the case overview page")]
        public void ThenIVerifyTheCaseNotesHeaderIsDisplayedAsOnTheCaseOverviewPage(string notesHeader)
        {
            _caseOverview.VerifyNotesHeader(notesHeader);
        }

        [Then(@"I verify the associated categories for the case are displayed in alphabetical order on the case overview page")]
        public void ThenIVerifyTheAssociatedCategoriesForTheCaseAreDisplayedInAlphabeticalOrderOnTheCaseOverviewPage()
        {
            _caseOverview.VerifyAssociatedCategoriesOrder();
        }

        [Then(@"I verify the case categories are displayed as:")]
        public void ThenIVerifyTheCaseCategoriesAreDisplayedAs(Table table)
        {
            ScenarioContext.Current["selectedCategoriesCount"] = table.RowCount;
            _caseOverview.VerifyCaseCategoriesPrefixDisplayed(table);
        }

        [Then(@"I verify the destinations for the case are displayed in alphabetical order on the case overview page")]
        public void ThenIVerifyTheDestinationsForTheCaseAreDisplayedInAlphabeticalOrderOnTheCaseOverviewPage()
        {
            _caseOverview.VerifyDestinationsOrder();
        }

        [Then(@"I verify the destinations in the casesubjects api match the destinations on the case overview page")]
        public void ThenIVerifyTheDestinationsInTheCasesubjectsApiMatchTheDestinationsOnTheCaseOverviewPage()
        {
            _caseOverview.GetCaseSubjectsApiCall();
            _caseOverview.VerifyDestinationsMatchCaseSubjectsApi();
        }

        [Then(@"I verify the properties for the case are displayed in alphabetical order on the case overview page")]
        public void ThenIVerifyThePropertiesForTheCaseAreDisplayedInAlphabeticalOrderOnTheCaseOverviewPage()
        {
            _caseOverview.VerifyPropertiesOrder();
        }

        [When(@"I click to expand the destinations on the case overview page")]
        public void WhenIClickToExpandTheDestinationsOnTheCaseOverviewPage()
        {
            _caseOverview.ClicktoExpandDestinations();
        }

        [Then(@"I verify the properties in the casesubjects api matches the properties on the case overview page")]
        public void ThenIVerifyThePropertiesInTheCasesubjectsApiMatchesThePropertiesOnTheCaseOverviewPage()
        {
            _caseOverview.GetCaseSubjectsApiCall();
            _caseOverview.VerifyPropertiesMatchCaseSubjectsApi();
        }

        [When(@"I click to expand the properties on the case overview page")]
        public void WhenIClickToExpandThePropertiesOnTheCaseOverviewPage()
        {
            _caseOverview.ClicktoExpandProperties();
        }

        [Then(@"I verify the resorts for the case are displayed in alphabetical order on the case overview page")]
        public void ThenIVerifyTheResortsForTheCaseAreDisplayedInAlphabeticalOrderOnTheCaseOverviewPage()
        {
            _caseOverview.VerifyResortsOrder();
        }

        [Then(@"I verify the resorts in the casesubjects api matches the properties on the case overview page")]
        public void ThenIVerifyTheResortsInTheCasesubjectsApiMatchesThePropertiesOnTheCaseOverviewPage()
        {
            _caseOverview.GetCaseSubjectsApiCall();
            _caseOverview.VerifyResortsMatchCaseSubjectsApi();
        }

        [When(@"I click to expand the resorts on the case overview page")]
        public void WhenIClickToExpandTheResortsOnTheCaseOverviewPage()
        {
            _caseOverview.ClicktoExpandResorts();
        }

        [When(@"I click to expand the categories on the case overview page")]
        public void WhenIClickToExpandTheCategoriesOnTheCaseOverviewPage()
        {
            _caseOverview.ClicktoExpandCategories();
        }

        [Then(@"I verify the case was reported by the following on the case overview page:")]
        public void ThenIVerifyTheCaseWasReportedByTheFollowingOnTheCaseOverviewPage(Table table)
        {
            _caseOverview.VerifyReportedBy(table);
        }

        [When(@"I click the view actions button on the Case Overview page")]
        public void WhenIClickTheViewActionsButtonOnTheCaseOverviewPage()
        {
            _caseOverview.ClickViewActions();
        }
		
        [When(@"I click to expand the affected customers on the case overview page")]
        public void WhenIClickToExpandTheAffectedCustomersOnTheCaseOverviewPage()
        {
            _caseOverview.ClicktoExpandAffectedCustomers();
        }

        [Then(@"I verify the affected customers for the case are displayed in alphabetical order on the case overview page")]
        public void ThenIVerifyTheAffectedCustomersForTheCaseAreDisplayedInAlphabeticalOrderOnTheCaseOverviewPage()
        {
            _caseOverview.VerifyAffectedCustomersOrder();
        }

        [Then(@"I verify the case overview api matches the affected customers on the case overview page")]
        public void ThenIVerifyTheCaseOverviewApiMatchesTheAffectedCustomersOnTheCaseOverviewPage()
        {
            _caseOverview.GetCaseOverviewApiCall();
            _caseOverview.GetCaseSubjectsApiCall();
            _caseOverview.VerifyCustomersMatchCaseSubjectsApi();
        }

        [Then(@"I verify the case overview api matches the affected bookings on the case overview page")]
        public void ThenIVerifyTheCaseOverviewApiMatchesTheAffectedBookingsOnTheCaseOverviewPage()
        {
            _caseOverview.GetCaseOverviewApiCall();
            _caseOverview.GetCaseSubjectsApiCall();
            _caseOverview.VerifyBookingsMatchCaseOverviewApi();
        }

        [When(@"I click to expand the affected bookings on the case overview page")]
        public void WhenIClickToExpandTheAffectedBookingsOnTheCaseOverviewPage()
        {
            _caseOverview.ClicktoExpandAffectedBookings();
        }

        [Then(@"I verify the affected bookings for the case are displayed in descending order on the case overview page")]
        public void ThenIVerifyTheAffectedBookingsForTheCaseAreDisplayedInDescendingOrderOnTheCaseOverviewPage()
        {
            _caseOverview.VerifyAffectedBookingsOrder();
        }

        [Then(@"I verify the count on the booking references heading on the case overview page")]
        public void ThenIVerifyTheCountOnTheBookingReferencesHeadingOnTheCaseOverviewPage()
        {
            var bookingReferencesList = ScenarioContext.Current.Get<List<string>>("bookingReferencesList");
            _caseOverview.VerifyCountOfAffectedBookingReferences(bookingReferencesList);
        }

        [Then(@"I verify the correct affected bookings are displayed on the case overview page")]
        public void ThenIVerifyTheCorrectAffectedBookingsAreDisplayedOnTheCaseOverviewPage()
        {
            var expectedBookings = ScenarioContext.Current.Get<List<string>>("bookingReferencesList");
            _caseOverview.VerifyAffectedBookingReferences(expectedBookings);
        }

        [Then(@"I verify the count on the affected customers heading on the case overview page")]
        public void ThenIVerifyTheCountOnTheAffectedCustomersHeadingOnTheCaseOverviewPage()
        {
            var affectedCustomersList = ScenarioContext.Current.Get<List<string>>("affectedCustomersList");
            _caseOverview.VerifyCountOfAffectedCustomers(affectedCustomersList);
        }

        [Then(@"I verify the correct affected customers are displayed on the case overview page")]
        public void ThenIVerifyTheCorrectAffectedCustomersAreDisplayedOnTheCaseOverviewPage()
        {
            var expectedAffectedCustomers = ScenarioContext.Current.Get<List<string>>("affectedCustomersList");
            _caseOverview.VerifyAffectedCustomers(expectedAffectedCustomers);
        }

        [Then(@"I verify the count on the destinations heading on the case overview page")]
        public void ThenIVerifyTheCountOnTheDestinationsHeadingOnTheCaseOverviewPage()
        {
            var destinationsList = ScenarioContext.Current.Get<List<string>>("destinationsList");
            _caseOverview.VerifyCountOfDestinations(destinationsList);
        }

        [Then(@"I verify the correct destinations are displayed on the case overview page")]
        public void ThenIVerifyTheCorrectDestinationsAreDisplayedOnTheCaseOverviewPage()
        {
            var expectedDestinations = ScenarioContext.Current.Get<List<string>>("destinationsList");
            _caseOverview.VerifyDestinations(expectedDestinations);
        }

        [Then(@"I verify the correct properties are displayed on the case overview page")]
        public void ThenIVerifyTheCorrectPropertiesAreDisplayedOnTheCaseOverviewPage()
        {
            var expectedProperties = ScenarioContext.Current.Get<List<string>>("propertiesList");
            _caseOverview.VerifyProperties(expectedProperties);
        }

        [Then(@"I verify the count on the properties heading on the case overview page")]
        public void ThenIVerifyTheCountOnThePropertiesHeadingOnTheCaseOverviewPage()
        {
            var propertiesList = ScenarioContext.Current.Get<List<string>>("propertiesList");
            _caseOverview.VerifyCountOfProperties(propertiesList);
        }

        [Then(@"I verify the correct resorts are displayed on the case overview page")]
        public void ThenIVerifyTheCorrectResortsAreDisplayedOnTheCaseOverviewPage()
        {
            var expectedResorts = ScenarioContext.Current.Get<List<string>>("resortsList");
            _caseOverview.VerifyResorts(expectedResorts);
        }

        [Then(@"I verify the count on the resorts heading on the case overview page")]
        public void ThenIVerifyTheCountOnTheResortsHeadingOnTheCaseOverviewPage()
        {
            var resortsList = ScenarioContext.Current.Get<List<string>>("resortsList");
            _caseOverview.VerifyCountOfResorts(resortsList);
        }

        [Then(@"I verify the count on the categories heading on the case overview page")]
        public void ThenIVerifyTheCountOnTheCategoriesHeadingOnTheCaseOverviewPage()
        {
            var expectedCategoriesCount = ScenarioContext.Current.Get<int>("selectedCategoriesCount");
            _caseOverview.VerifyCountOfCategories(expectedCategoriesCount);
        }

        [When(@"I click to expand the assignees on the case overview page")]
        public void WhenIClickToExpandTheAssigneesOnTheCaseOverviewPage()
        {
            _caseOverview.ClicktoExpandAssignees();
        }

        [When(@"I click to edit the initial summary on the case overview page")]
        public void WhenIClickToEditTheInitialSummaryOnTheCaseOverviewPage()
        {
            _caseOverview.ClicktoEditInitialSummary();
        }

        [When(@"I click to edit the reported by person on the case overview page")]
        public void WhenIClickToEditTheReportedByPersonOnTheCaseOverviewPage()
        {
            _caseOverview.ClicktoEditReportedBy();
        }

        [When(@"I click to edit the note on the case overview page")]
        public void WhenIClickToEditTheNoteOnTheCaseOverviewPage()
        {
            _caseOverview.ClicktoEditNotes();
        }

        [When(@"I click to edit the associated categories on the case overview page")]
        public void WhenIClickToEditTheAssociatedCategoriesOnTheCaseOverviewPage()
        {
            _caseOverview.ClicktoEditCategories();
        }

        [Then(@"I verify the format of the case ID displayed on the case overview page follows business rules")]
        public void ThenIVerifyTheFormatOfTheCaseIdDisplayedOnTheCaseOverviewPageFollowsBusinessRules()
        {
            _caseOverview.VerifyCaseIdFormat();
        }

        [Then(@"I verify that the knowledge base button is displayed on the Case Overview page")]
        public void ThenIVerifyThatTheKnowledgeBaseButtonIsDisplayedOnTheCaseOverviewPage()
        {
            _caseOverview.VerifyKbiButtonDisplayed();
        }

        [When(@"I click the knowledge base button on the Case Overview page")]
        public void WhenIClickTheKnowledgeBaseButtonOnTheCaseOverviewPage()
        {
            _caseOverview.ClickKbiButton();
        }

        [Then(@"I verify that the Knowledge Base Search Screen is displayed")]
        public void ThenIVerifyThatTheKnowledgeBaseSearchScreenIsDisplayed()
        {
            _caseOverview.VerifyKbiScreenIsDisplayed();
        }

        [When(@"I click close on the Knowledge Base Search Screen")]
        public void WhenIClickCloseOnTheKnowledgeBaseSearchScreen()
        {
            _caseOverview.ClickCloseKbiScreen();
        }

        [Then(@"I verify that the Knowledge Base Search Screen is not displayed")]
        public void ThenIVerifyThatTheKnowledgeBaseSearchScreenIsNotDisplayed()
        {
            _caseOverview.VerifyKbiScreenIsNotDisplayed();
        }

        [When(@"I add the following assignees to the overview page:")]
        public void WhenIAddTheFollowingAssigneesToTheOverviewPage(Table table)
        {
            _caseOverview.ClickAddAssigneesEllipsisButton();
            _caseOverview.VerifyEditAssigneesScreenDisplayed();
            _caseOverview.AddAssigneeToCase(table);
            _caseOverview.ClickEditAssigneesSaveAndCloseButton();
            _caseOverview.VerifyEditAssigneesScreenNotDisplayed();
            _caseOverview.VerifyCreateACaseButtonEnabled();
        }


    }
}
