using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using J2BIOverseasOps.Extensions;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using J2BIOverseasOps.Models;
using TechTalk.SpecFlow;
using static J2BIOverseasOps.Models.CaseOverviewApi;
using static J2BIOverseasOps.Models.CaseAutoAssigneesApi;
using static J2BIOverseasOps.Models.AvailableAssigneesApi;
using static J2BIOverseasOps.Models.CaseSubjectsApi;



namespace J2BIOverseasOps.Pages.Customer_Interaction
{
    class CaseOverviewPage:CommonPageElements
    {
        public CaseOverviewPage(IWebDriver driver, ILog log, IRunData runData) : base(driver, log)
        {
            _runData = runData;
            _apiCall = new ApiCalls(runData);
        }

        private readonly IRunData _runData;
        private readonly ApiCalls _apiCall;

        //Multiselect
        private readonly By _assigneeMultiselect = By.Id("choose-assignees-multiselect"); 

        //Headers
        private readonly By _headerAssignee = By.CssSelector("#assignees-heading");
        private readonly By _headerInitialSummary = By.CssSelector("#initial-summary-heading");
        private readonly By _headerNotes = By.CssSelector("#notes-heading");

        //Labels
        private readonly By _lblAssigneesCaseOverview = By.CssSelector("div[id^=assignee] div[id^=assignee]");
        private readonly By _lblNoAssigneesDisplayed = By.CssSelector("#assignees > p");
        private readonly By _lblCaseId = By.CssSelector("#case-id");
        private readonly By _lblInitialSummary = By.CssSelector("#initial-summary");
        private readonly By _lblNotes = By.CssSelector("#notes");
        private readonly By _lblassociatedCategories = By.CssSelector("div[id^=categories]");
        private readonly By _lblDestinations = By.CssSelector("div[id^=destinations]");
        private readonly By _lblProperties = By.CssSelector("div[id^=properties]");
        private readonly By _lblResorts = By.CssSelector("div[id^=resorts]");
        private readonly By _lblReportedBy = By.CssSelector("div[id^=reported-by-display]");
        private readonly By _lblAffectedCustomers = By.CssSelector("div[id^=customers]");
        private readonly By _lblAffectedBookings = By.CssSelector("div[id^=booking]");

        //Expand Chevrons
        private readonly By _destinationsChevron = By.CssSelector("#destinations-heading i");
        private readonly By _propertiesChevron = By.CssSelector("#properties-heading i");
        private readonly By _resortsChevron = By.CssSelector("#resorts-heading i");
        private readonly By _categoriesChevron = By.CssSelector("#categories-heading i");
        private readonly By _affectedCustomersChevron = By.CssSelector("#customers-heading i");
        private readonly By _affectedBookingsChevron = By.CssSelector("#booking-references-heading i");
        private readonly By _assigneesChevron = By.CssSelector("#assignees-heading i");

        //Headings
        private readonly By _destinationsHeader = By.CssSelector("#destinations-heading");
        private readonly By _propertiesHeader = By.CssSelector("#properties-heading");
        private readonly By _resortsHeader = By.CssSelector("#resorts-heading");
        private readonly By _categoriesHeader = By.CssSelector("#categories-heading");
        private readonly By _affectedCustomersHeader = By.CssSelector("#customers-heading");
        private readonly By _affectedBookingsHeader = By.CssSelector("#booking-references-heading");
        private readonly By _assigneesHeader = By.CssSelector("#assignees-heading");

        //Buttons
        private readonly By _buttonAddAssigneeEllipsis = By.CssSelector("#assignees-button");
        private readonly By _editAssigneesScreenSaveAndCloseButton = By.CssSelector("#save-and-close-assignees");
        private readonly By _editAssigneesScreenSaveAndCloseButtonDisabled = By.CssSelector("#save-and-close-assignees[disabled]");
        private readonly By _editAssigneesScreenCloseButton = By.CssSelector("#close-assignees");
        private readonly By _buttonCreateACase = By.CssSelector("#create-case");
        private readonly By _buttonCreateACaseDisabled = By.CssSelector("#create-case[disabled]");
        private readonly By _lnkViewActions = By.Id("case-actions-add");
        private readonly By _kbiButton = By.Id("open-knowledge-base");
        private readonly By _kbiScreen = By.CssSelector("p-sidebar > div");
        private readonly By _kbiScreenClose = By.CssSelector("p-sidebar .ui-sidebar-close");

        //Validation messages
        private readonly By _validationMessage = By.CssSelector("div:not([hidden]) > p-message .ui-message-text");

        //Link Text
        private readonly By _editInitialSummaryLink = By.CssSelector("#initial-summary-edit");
        private readonly By _editReportedByLink = By.CssSelector("#reported-by-edit");
        private readonly By _editNotesLink = By.CssSelector("#notes-edit");
        private readonly By _editCategoriesLink = By.CssSelector("#categories-edit");

        //dialogs
        private readonly By _editAssingeesDialog = By.CssSelector("#assignees-popup > div");


        public void VerifyAssigneesOrderCaseOverview()
        {
            //Get actual order of assignees
            var actualOrderOfAssignees = Driver.GetTexts(_lblAssigneesCaseOverview);
            var assigneeListNames = new List<string>();

            //Retreive assignees that are not usernames
            foreach (var assignee in actualOrderOfAssignees)
            {
                if (!assignee.Contains("\\"))
                {
                    assigneeListNames.Add(assignee);
                }
            }

            //Order the list of non username assignees alphabetically
            var expectedOrderOfAssignees = assigneeListNames.OrderBy(x => x).ToList();
            var assigneeListUsernames = new List<string>();

            //Retreive assignees that are usernames
            foreach (var assignee in actualOrderOfAssignees)
            {
                if (assignee.Contains("\\"))
                {
                    assigneeListUsernames.Add(assignee);
                }
            }

            //Order the list of non username assignees alphabetically
            var orderedAssigneeListUsernames = assigneeListUsernames.OrderBy(x => x).ToList();

            //Add the username list to bottom the non username list
            expectedOrderOfAssignees.AddRange(orderedAssigneeListUsernames);

            CollectionAssert.AreEqual(actualOrderOfAssignees, expectedOrderOfAssignees, "The order of assignees on the case overview page is incorrect");
        }


        public void VerifyAssigneesDisplayed(Table table)
        {
            var actualAssignees = Driver.GetTexts(_lblAssigneesCaseOverview);
            var expectedAssignees = table.Rows.ToColumnList("Assignees");
            CollectionAssert.AreEqual(actualAssignees, expectedAssignees, "The assignees displayed on the case overview page are incorrect");
        }

        public void ClickAddAssigneesEllipsisButton()
        {
            Driver.ClickItem(_buttonAddAssigneeEllipsis);
        }

        public void VerifyEditAssigneesScreenDisplayed()
        {
            WaitForSpinnerToDisappear();
            Assert.True(Driver.WaitForItem(_editAssingeesDialog));
            WaitForSpinnerToDisappear();
        }

        public void VerifyAssigneesOrderEditAssignees()
        {
            //Get actual order of assignees
            var actualOrderOfAssignees = Driver.GetAllMultiselectOptions(_assigneeMultiselect);
            var assigneeListNames = new List<string>();

            //Retreive assignees that are not usernames
            foreach (var assignee in actualOrderOfAssignees)
            {
                if (!assignee.Contains("\\"))
                {
                    assigneeListNames.Add(assignee);
                }
            }

            //Order the list of non username assignees alphabetically
            var expectedOrderOfAssignees = assigneeListNames.OrderBy(x => x).ToList();
            var assigneeListUsernames = new List<string>();

            //Retreive assignees that are usernames
            foreach (var assignee in actualOrderOfAssignees)
            {
                if (assignee.Contains("\\"))
                {
                    assigneeListUsernames.Add(assignee);
                }
            }

            //Order the list of non username assignees alphabetically
            var orderedAssigneeListUsernames = assigneeListUsernames.OrderBy(x => x).ToList();

            //Add the username list to bottom of the non username list
            expectedOrderOfAssignees.AddRange(orderedAssigneeListUsernames);

            CollectionAssert.AreEqual(actualOrderOfAssignees, expectedOrderOfAssignees, "The order of assignees on the Edit Jet2Holidays Assignees listbox is incorrect");
        }

        public void SelectCaseAssignees(Table table)
        {
            var assigneesToSelect = table.Rows.ToColumnList("Assignees");
            Driver.SelectMultiselectOption(_assigneeMultiselect, assigneesToSelect, true);
        }

        public void ClickEditAssigneesSaveAndCloseButton()
        {
            Driver.ClickItem(_editAssigneesScreenSaveAndCloseButton);
        }

        public void VerifyEditAssigneesScreenNotDisplayed()
        {
            WaitForSpinnerToDisappear();
            Assert.True(Driver.WaitUntilElementNotDisplayed(_editAssingeesDialog));
            WaitForSpinnerToDisappear();
        }

        public void VerifySelectedCaseAssignees(Table table)
        {
            var expectedAssignees = table.Rows.ToColumnList("Assignees");

            var selectedAssignees = Driver.GetAllSelectedMultiselectOptions(_assigneeMultiselect);
            CollectionAssert.AreEqual(expectedAssignees, selectedAssignees, $"The selected assignees {selectedAssignees} does not match the expected assignees {expectedAssignees}");
        }

        public void DeselectCaseAssignees(Table table)
        {
            var assigneesToSelect = table.Rows.ToColumnList("Assignees");
            Driver.DeselectMultiselectOption(_assigneeMultiselect, assigneesToSelect, true);
        }

        public void VerifyNoAssigneesAssigned()
        {
            Driver.WaitUntilTextPresent(_lblNoAssigneesDisplayed);
            var noAssigneesMessage = Driver.GetText(_lblNoAssigneesDisplayed);
            Assert.AreEqual(noAssigneesMessage, "There is nobody assigned to this case", "The no assignee message is incorrect");
        }

        public void VerifyAssigneeHeader(string assigneeHeader)
        {
            Driver.WaitForItem(_headerAssignee);
            var lblAssigneeHeader = Driver.GetText(_headerAssignee);
            Assert.AreEqual(lblAssigneeHeader, assigneeHeader, $"The assignee header is {lblAssigneeHeader} but should be {assigneeHeader} on the case overview page");

        }
        public void VerifyEditAssigneesSaveAndCloseButtonDisabled()
        {
            Driver.WaitForItem(_editAssigneesScreenSaveAndCloseButtonDisabled);
        }

        public void VerifyEditAssigneesSaveAndCloseButtonEnabled()
        {
            Driver.WaitForItem(_editAssigneesScreenSaveAndCloseButton);
        }

        public void ClickEditAssigneesCloseButton()
        {
            Driver.ClickItem(_editAssigneesScreenCloseButton);
        }

        public void ClickCreateACaseButton()
        {
            Driver.ClickItem(_buttonCreateACase);
        }

        public void VerifyValidationMessage(string message)
        {
            Assert.AreEqual(message, Driver.GetText(_validationMessage),
                "The validation message is not being displayed.");
        }

        public void VerifyCreateACaseButtonEnabled()
        {
            Assert.True(Driver.IsElementPresent(_buttonCreateACase));
        }

        public void VerifyCreateACaseButtonDisabled()
        {
            Assert.True(Driver.IsElementPresent(_buttonCreateACaseDisabled));
        }

        public void VerifyCreateACaseButtonText(string buttonText)
        {
            Assert.True(Driver.IsElementPresent(_buttonCreateACase));
            var actualText = Driver.GetText(_buttonCreateACase);
            Assert.AreEqual(buttonText, actualText, "The create a case button text is incorrect");
        }

        public void GetCaseOverviewApiCall()
        {
            var guid = Driver.Url.GetGuid();
            var caseOverviewResponse = _apiCall.GetCaseOverviewByGuid(guid, Token);
            ScenarioContext.Current["CaseOverviewResponse"] = caseOverviewResponse;
        }

        public void GetCaseSubjectsApiCall()
        {
            var guid = Driver.Url.GetGuid();
            var caseSubjectsResponse = _apiCall.GetCaseSubjectsByGuid(guid, Token);
            ScenarioContext.Current["CaseSubjectsResponse"] = caseSubjectsResponse;
        }

        public void CheckCaseState(int state)
        {
            var caseOverviewResponse = ScenarioContext.Current.Get<GetCaseOverviewResponse>("CaseOverviewResponse");
            var actualstate = Convert.ToInt32(caseOverviewResponse.CaseOverview.StateMode.Id);
            Assert.AreEqual(state, actualstate, $"The state of the case should be {state} not {actualstate}");
        }

        public void VerifyCorrectCaseIdDisplayed()
        {
            var caseOverviewResponse = ScenarioContext.Current.Get<GetCaseOverviewResponse>("CaseOverviewResponse");
            var expectedCaseId = caseOverviewResponse.CaseOverview.CaseReference;
            var actualCaseId = Driver.GetText(_lblCaseId);
            Assert.AreEqual(expectedCaseId, actualCaseId, $"The ID of the case should be {expectedCaseId} not {actualCaseId}");
        }

        public void GetCaseAutoAssigneesApiCall()
        {
            var guid = Driver.Url.GetGuid();
            var caseAutoAssignees = _apiCall.GetCaseAutoAssigneesByGuid(guid, Token);
            ScenarioContext.Current["CaseAutoAssignees"] = caseAutoAssignees;

        }

        public void GetAvailableAssigneesApiCall()
        {
            var availableAssignees = _apiCall.GetAvailableAssignees(Token);
            ScenarioContext.Current["AvailableAssignees"] = availableAssignees;
        }

        public void VerifyAssigneesMatchGetCaseAutoAssigneesApi()
        {
            var caseAutoAssignees = ScenarioContext.Current.Get<GetUsersAutoAssignedToCaseResponse>("CaseAutoAssignees");

            var caseAutoAssigneesIdList = ObtainAssigneeIdList(caseAutoAssignees);
            var expectedAssignees = ObtainAssigneesById(caseAutoAssigneesIdList).OrderBy(x => x).ToList(); 
            var actualAssignees = Driver.GetTexts(_lblAssigneesCaseOverview);
            
            CollectionAssert.AreEqual(expectedAssignees, actualAssignees, "The assignees displayed on the case overview page are incorrect");
        }

        public List<string> ObtainAssigneesById(List<int> assigneeIds)
        {
            var autoAssignees = new List<string>();
            var assignees = ScenarioContext.Current.Get<List<CaseAssignee>>("AvailableAssignees");

            var caseAssignees =
                assignees.Where(item => assigneeIds.Any(id => id.ToString().Equals(item.Identifier.Id.ToString()))).ToList();

            foreach (var caseAssignee in caseAssignees)
            {
                if (string.IsNullOrWhiteSpace(caseAssignee.FirstName) || string.IsNullOrWhiteSpace(caseAssignee.LastName))
                {
                    var assigneeName = caseAssignee.UserName;
                    autoAssignees.Add(caseAssignee.UserDomain + "\\" + assigneeName);
                }
                else
                {
                    var assigneeName = caseAssignee.FirstName + " " + caseAssignee.LastName;
                    autoAssignees.Add($"{assigneeName}");
                }
            }

            return autoAssignees;
        }

        public List<int> ObtainAssigneeIdList(GetUsersAutoAssignedToCaseResponse caseAutoAssignees)
        {
            var idList = new List<int>();

            foreach (var assignees in caseAutoAssignees.AutoAssignedUsers)
            {
                idList.Add(Convert.ToInt32(assignees.Id));
            }
            return idList;
        }

        public void VerifyInitialSummaryText(Table table)
        {
            var row = table.Rows[0];
            var expectedInitialSummary = row["Initial Summary"];
            var actualInitialSummary = Driver.GetText(_lblInitialSummary);

            Assert.AreEqual(expectedInitialSummary, actualInitialSummary, "The initial summary text is incorrect on the case overview page");
        }

        public void VerifyNotesText(Table table)
        {
            var row = table.Rows[0];
            var expectedCaseNotes = row["Case Notes"];
            var actualCaseNotes = Driver.GetText(_lblNotes);

            Assert.AreEqual(expectedCaseNotes, actualCaseNotes, "The notes text is incorrect on the case overview page");

        }

        public void VerifyInitialSummaryHeader(string initialSummaryHeader)
        {
            var actualInitialSummaryHeader = Driver.GetText(_headerInitialSummary);
            Assert.AreEqual(initialSummaryHeader, actualInitialSummaryHeader, "The initial summary header text is incorrect");
        }

        public void VerifyNotesHeader(string notesHeader)
        {
            var actualNotesHeader = Driver.GetText(_headerNotes);
            Assert.AreEqual(notesHeader, actualNotesHeader, "The Notes header text is incorrect");

        }

        public void VerifyAssociatedCategoriesOrder()
        {
            var actualAssociatedCategories = Driver.GetTexts(_lblassociatedCategories);
            var expectedAssociatedCategories = Driver.GetTexts(_lblassociatedCategories).OrderBy(x => x).ToList();
            CollectionAssert.AreEqual(expectedAssociatedCategories, actualAssociatedCategories,"The associated categories are not in alphabetical order");
        }

        public void VerifyCaseCategoriesPrefixDisplayed(Table table)
        {
            var expectedCategories = table.Rows.ToColumnList("Categories");
            var actualCategories = Driver.GetTexts(_lblassociatedCategories);
            CollectionAssert.AreEqual(expectedCategories, actualCategories, "The associated categories are not displayed on the case overview page");
        }

        public void VerifyDestinationsOrder()
        {
            var actualDestinations = Driver.GetTexts(_lblDestinations);
            var expectedDestinations = Driver.GetTexts(_lblDestinations).OrderBy(x => x).ToList();
            CollectionAssert.AreEqual(expectedDestinations, actualDestinations, "The associated destinations are not in alphabetical order");
        }

        public void VerifyDestinationsMatchCaseSubjectsApi()
        {
            var caseSubjectsResponse = ScenarioContext.Current.Get<GetCaseSubjectsResponse>("CaseSubjectsResponse");
            var expectedDestinations = caseSubjectsResponse.Subjects
                .Where(x => x.Type.Equals(CaseSubjectsApi.SubjectType.Destination))
                .Select(x => x.Name)
                .OrderBy(x => x).ToList();
            var actualDestinations = Driver.GetTexts(_lblDestinations);
            CollectionAssert.AreEqual(expectedDestinations, actualDestinations, "The associated destinations are not correct");
        }

        public void VerifyPropertiesOrder()
        {
            var actualProperties = Driver.GetTexts(_lblProperties);
            var expectedProperties = Driver.GetTexts(_lblProperties).OrderBy(x => x).ToList();
            CollectionAssert.AreEqual(expectedProperties, actualProperties, "The properties on the overview are not in alphabetical order");
        }

        public void VerifyPropertiesMatchCaseSubjectsApi()
        {
            var caseSubjectsResponse = ScenarioContext.Current.Get<GetCaseSubjectsResponse>("CaseSubjectsResponse");
            var expectedProperties = caseSubjectsResponse.Subjects
                .Where(x => x.Type.Equals(CaseSubjectsApi.SubjectType.Property))
                .Select(x => x.Name)
                .OrderBy(x => x).ToList();
            var actualProperties = Driver.GetTexts(_lblProperties);
            CollectionAssert.AreEqual(expectedProperties, actualProperties, "The associated properties are not correct");
        }

        public void ClicktoExpandDestinations()
        {
            Driver.ClickItem(_destinationsChevron);
        }

        public void ClicktoExpandProperties()
        {
            Driver.ClickItem(_propertiesChevron);
        }

        public void ClicktoExpandResorts()
        {
            Driver.ClickItem(_resortsChevron);
        }

        public void VerifyResortsMatchCaseSubjectsApi()
        {
            var caseSubjectsResponse = ScenarioContext.Current.Get<GetCaseSubjectsResponse>("CaseSubjectsResponse");
            var expectedResorts = caseSubjectsResponse.Subjects
                .Where(x => x.Type.Equals(CaseSubjectsApi.SubjectType.Resort))
                .Select(x => x.Name)
                .OrderBy(x => x).ToList();
            var actualResorts = Driver.GetTexts(_lblResorts);
            CollectionAssert.AreEqual(expectedResorts, actualResorts, "The associated resorts are not correct");
        }

        public void VerifyResortsOrder()
        {
            var actualResorts = Driver.GetTexts(_lblResorts);
            var expectedResorts = Driver.GetTexts(_lblResorts).OrderBy(x => x).ToList();
            CollectionAssert.AreEqual(expectedResorts, actualResorts, "The resorts on the overview are not in alphabetical order");

        }

        public void ClicktoExpandCategories()
        {
            Driver.ClickItem(_categoriesChevron);
        }

        public void VerifyReportedBy(Table table)
        {
            var row = table.Rows[0];
            var expectedReportedBy = row["Reported by"];
            var actualReportedBy = Driver.GetText(_lblReportedBy);
            Assert.AreEqual(expectedReportedBy, actualReportedBy, "The expected reported by person is not displayed");
        }
		
        public void ClickViewActions()
        {
            Driver.ClickItem(_lnkViewActions);
		}
		
        public void ClicktoExpandAffectedCustomers()
        {
            Driver.ClickItem(_affectedCustomersChevron);
        }

        public void VerifyAffectedCustomersOrder()
        {
            var actualCustomers = Driver.GetTexts(_lblAffectedCustomers);
            var expectedCustomers = Driver.GetTexts(_lblAffectedCustomers).OrderBy(x => x).ToList();
            CollectionAssert.AreEqual(expectedCustomers, actualCustomers, "The resorts on the overview are not in alphabetical order");

        }

        public void VerifyCustomersMatchCaseSubjectsApi()
        {
            var caseOverviewResponse = ScenarioContext.Current.Get<GetCaseOverviewResponse>("CaseOverviewResponse");
            var caseSubjectsResponse = ScenarioContext.Current.Get<GetCaseSubjectsResponse>("CaseSubjectsResponse");

            //get list of id for affected customer from caseoverview response
            var affectedCustomerIdList = ObtainAffectedCustomerIdList(caseOverviewResponse);

            //get list of customer subjects from casesubjects response
            var customersSubjects = caseSubjectsResponse.Subjects
                .Where(x => x.Type.Equals(CaseSubjectsApi.SubjectType.Customer))
                .Select(x => x).ToList();

            //get affected customers from customerSujects based on affectedCustomerIdList
            var expectedCustomers =
                customersSubjects.Where(x => affectedCustomerIdList.Any(y => y.ToString().Equals(x.Id.Id.ToString())))
                    .Select(x => x.Name.ToString())
                    .OrderBy(x => x).ToList();

            //get actual affected customers
            var actualCustomers = Driver.GetTexts(_lblAffectedCustomers);

            //compare the lists
            CollectionAssert.AreEqual(expectedCustomers, actualCustomers, "The affected customers are not correct");

        }


        public List<string> ObtainAffectedCustomerIdList(GetCaseOverviewResponse caseOverviewResponse)
        {
            var idList = new List<string>();

            foreach (var subject in caseOverviewResponse.CaseOverview.AffectedSubjects)
            {
                idList.Add(subject.Id);
            }
            return idList;
        }

        public void ClicktoExpandAffectedBookings()
        {
            Driver.ClickItem(_affectedBookingsChevron);
        }

        public void VerifyAffectedBookingsOrder()
        {
            var actualBookings = Driver.GetTexts(_lblAffectedBookings);
            var expectedBookings = Driver.GetTexts(_lblAffectedBookings).OrderByDescending(x => x).ToList();
            CollectionAssert.AreEqual(expectedBookings, actualBookings, "The resorts on the overview are not in alphabetical order");
        }

        public void VerifyBookingsMatchCaseOverviewApi()
        {
            var caseOverviewResponse = ScenarioContext.Current.Get<GetCaseOverviewResponse>("CaseOverviewResponse");
            var caseSubjectsResponse = ScenarioContext.Current.Get<GetCaseSubjectsResponse>("CaseSubjectsResponse");

            //get list of id for affected customer from caseoverview response
            var affectedCustomerIdList = ObtainAffectedCustomerIdList(caseOverviewResponse);

            //get list of customer subjects from casesubjects response
            var customersSubjects = caseSubjectsResponse.Subjects
                .Where(x => x.Type.Equals(CaseSubjectsApi.SubjectType.Customer))
                .Select(x => x).ToList();

            //get affected bookings from customerSujects based on affectedCustomerIdList and ensure its distinct
            var expectedBookings =
                customersSubjects.Where(x => affectedCustomerIdList.Any(y => y.ToString().Equals(x.Id.Id.ToString())))
                .Select(x => x.RelatedBookingRef.ToString())
                .Distinct().OrderByDescending(x => x).ToList();

            //get actual affected customers
            var actualBookings = Driver.GetTexts(_lblAffectedBookings);

            //compare the lists
            CollectionAssert.AreEqual(expectedBookings, actualBookings, "The affected bookings are not correct");
        }

        public void VerifyCountOfAffectedBookingReferences(List<string> bookingReferencesList)
        {
            Driver.WaitUntilTextPresent(_affectedBookingsHeader);
            var expectedBookingReferencesCount = bookingReferencesList.Distinct().Count().ToString();
            var affectedBookingsHeader = Driver.GetText(_affectedBookingsHeader).Split(' ');
            var actualBookingReferencesCount = affectedBookingsHeader[0];
            Assert.AreEqual(expectedBookingReferencesCount,actualBookingReferencesCount, "The affected booking references count on the header is incorrect");

        }

        public void VerifyAffectedBookingReferences(List<string> expectedBookings)
        {
            var actualBookings = Driver.GetTexts(_lblAffectedBookings);
            CollectionAssert.AreEqual(expectedBookings, actualBookings, "The affected booking references on the case overview page are incorrect");
        }

        public void VerifyCountOfAffectedCustomers(List<string> affectedCustomersList)
        {
            Driver.WaitUntilTextPresent(_affectedCustomersHeader);
            var expectedAffectedCustomersListCount = affectedCustomersList.Distinct().Count().ToString();
            var affectedCustomersHeader = Driver.GetText(_affectedCustomersHeader).Split(' ');
            var actualaffectedCustomersCount = affectedCustomersHeader[0];
            Assert.AreEqual(expectedAffectedCustomersListCount, actualaffectedCustomersCount, "The affected customers count on the header is incorrect");
        }

        public void VerifyAffectedCustomers(List<string> expectedAffectedCustomers)
        {
            var actualAffectedCustomers = Driver.GetTexts(_lblAffectedCustomers);
            CollectionAssert.AreEqual(expectedAffectedCustomers.OrderBy(x => x), actualAffectedCustomers, "The affected customers on the case overview page are incorrect");
        }

        public void VerifyCountOfDestinations(List<string> destinationsList)
        {
            Driver.WaitUntilTextPresent(_destinationsHeader);
            var expectedDestinationsListCount = destinationsList.Distinct().Count().ToString();
            Driver.WaitUntilContainedTextPresent(_destinationsHeader, $"{expectedDestinationsListCount}");
            var destinationsHeader = Driver.GetText(_destinationsHeader).Split(' ');
            var actualDestinationsListCount = destinationsHeader[0];
            Assert.AreEqual(expectedDestinationsListCount, actualDestinationsListCount, "The associated destinations count on the header is incorrect");
        }

        public void VerifyDestinations(List<string> expectedDestinations)
        {
            var actualDestinations = Driver.GetTexts(_lblDestinations);
            CollectionAssert.AreEqual(expectedDestinations.Distinct().OrderBy(x => x), actualDestinations, "The affected customers on the case overview page are incorrect");
        }

        public void VerifyProperties(List<string> expectedProperties)
        {
            var actualProperties = Driver.GetTexts(_lblProperties);
            CollectionAssert.AreEqual(expectedProperties.Distinct().OrderBy(x => x), actualProperties, "The affected customers on the case overview page are incorrect");
        }

        public void VerifyCountOfProperties(List<string> propertiesList)
        {
            Driver.WaitUntilTextPresent(_destinationsHeader);
            var expectedPropertiesListCount = propertiesList.Count.ToString();
            Driver.WaitUntilContainedTextPresent(_propertiesHeader, $"{expectedPropertiesListCount}");
            var propertiesHeader = Driver.GetText(_propertiesHeader).Split(' ');
            var actualPropertiesListCount = propertiesHeader[0];
            Assert.AreEqual(expectedPropertiesListCount, actualPropertiesListCount, "The associated properties count on the header is incorrect");
        }

        public void VerifyResorts(List<string> expectedResorts)
        {
            var actualResorts = Driver.GetTexts(_lblResorts);
            CollectionAssert.AreEqual(expectedResorts.Distinct().OrderBy(x => x), actualResorts, "The affected customers on the case overview page are incorrect");

        }

        public void VerifyCountOfResorts(List<string> resortsList)
        {
            Driver.WaitUntilTextPresent(_resortsHeader);
            var expectedResortsListCount = resortsList.Count.ToString();
            Driver.WaitUntilContainedTextPresent(_resortsHeader, $"{expectedResortsListCount}");
            var resortsHeader = Driver.GetText(_resortsHeader).Split(' ');
            var actualResortsListCount = resortsHeader[0];
            Assert.AreEqual(expectedResortsListCount, actualResortsListCount, "The associated resorts count on the header is incorrect");
        }

        public void VerifyCountOfCategories(int expectedCategoriesCount)
        {
            Driver.WaitUntilTextPresent(_resortsHeader);
            var categoriesHeader = Driver.GetText(_categoriesHeader).Split(' ');
            var actualCategoriesCount = categoriesHeader[0];
            Assert.AreEqual(expectedCategoriesCount.ToString(), actualCategoriesCount, "The associated categories count on the header is incorrect");
        }

        public void ClicktoExpandAssignees()
        {
            Driver.ClickItem(_assigneesChevron);
        }

        public void ClicktoEditInitialSummary()
        {
            Driver.ClickItem(_editInitialSummaryLink);
        }

        public void ClicktoEditReportedBy()
        {
            Driver.ClickItem(_editReportedByLink);
        }

        public void ClicktoEditNotes()
        {
            Driver.ClickItem(_editNotesLink);
        }

        public void ClicktoEditCategories()
        {
            Driver.ClickItem(_editCategoriesLink);
        }

        public string GenerateCaseId(IEnumerable<string> destinationIataCodes, DateTime creationDate)
        {
            var season = GetSeason(creationDate);
            var year = GetYear(creationDate);
            var destination = GetDestination(destinationIataCodes);

            return $"{ season }{ year }{ destination }";
        }

        private static string GetDestination(IEnumerable<string> destinationIataCodes)
        {
            var destinationSubjects = destinationIataCodes.ToArray();

            if (destinationSubjects.Length == 0)
            {
                return "ZZZ";
            }

            if (destinationSubjects.Length == 1)
            {
                return destinationSubjects.First();
            }

            return "MD";
        }

        private static string GetSeason(DateTime date)
        {
            if (date.Day >= 1 && date.Month >= 5 && date.Day <= 31 && date.Month <= 10)
            {
                return "S";
            }

            return "W";
        }

        private static string GetYear(DateTime date)
        {
            if (date.Month >= 1 && date.Month <= 4)
            {
                return date.AddYears(-1).ToString("yy");
            }

            return date.ToString("yy");
        }
        
        public void VerifyCaseIdFormat()
        {
            var creationdate = DateTime.Now;
            var destinationIataCodes = Driver.GetTexts(_lblDestinations);
            var actualCaseId = Driver.GetText(_lblCaseId);
            var expectedCaseId = GenerateCaseId(destinationIataCodes, creationdate);
            Assert.True(actualCaseId.Contains(expectedCaseId), "The Case ID format is incorrect");

            var expectedCaseIdEnd = Regex.Match(actualCaseId, @"\d+$").Value;

            var actualCaseIdEnd = actualCaseId.Substring(actualCaseId.Length - 6);


            Assert.AreEqual(expectedCaseIdEnd, actualCaseIdEnd, "The last 6 digits of the Case ID are incorrect");

        }

        public void VerifyKbiButtonDisplayed()
        {
            Assert.IsTrue(Driver.WaitForItem(_kbiButton), "The KBI Button is not displayed on the case overview page.");
        }

        public void ClickKbiButton()
        {
            Driver.ClickItem(_kbiButton);
        }

        public void VerifyKbiScreenIsDisplayed()
        {
           Assert.IsTrue(Driver.WaitForItem(_kbiScreen), "The KBI screen is not displayed.");
        }

        public void ClickCloseKbiScreen()
        {
            Driver.ClickItem(_kbiScreenClose);
        }

        public void VerifyKbiScreenIsNotDisplayed()
        {
            Assert.IsTrue(Driver.WaitUntilElementNotDisplayed(_kbiScreen));
        }

        public void AddAssigneeToCase(Table table)
        {
            var row = table.Rows[0];
            var assignees = row["Assignees"].ConvertStringIntoList();

            foreach (var assignee in assignees)
            {
                Driver.SelectMultiselectOption(_assigneeMultiselect, assignee, true);
            }
        }
    }
}
