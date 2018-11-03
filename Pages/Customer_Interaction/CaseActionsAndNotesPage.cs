using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using J2BIOverseasOps.Extensions;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Pages.Customer_Interaction
{
    internal class CaseActionsAndNotesPage : CommonPageElements
    {
        //Actions
        private readonly By _actionRows = By.CssSelector("#actions-table tr");
        private readonly By _actionsAndNotesPageTitle = By.CssSelector("#actions-and-notes-heading");
        private readonly By _actionsScreenTitle = By.Id("actions-heading");

        //Calendar Pickers
        private readonly By _addActionScreenDueByDate = By.Id("due-by");
        private readonly By _addActionScreenTitle = By.CssSelector("[title=add]");
        private readonly By _btnAddActionActionsScreenDialog = By.CssSelector("#add-action-button");
        private readonly By _btnBack = By.CssSelector("#actions-and-notes-back-button");
        private readonly By _btnCancelAddActionScreenDialog = By.CssSelector("#close-action-button");
        private readonly By _btnCloseActionsScreenDialog = By.CssSelector("#close-actions-button");

        //Buttons
        private readonly By _btnContinue = By.CssSelector("#actions-and-notes-continue-button");
        private readonly By _btnSaveAddActionScreenDialog = By.CssSelector("#save-action-button");
        private readonly By _btnsEditAction = By.CssSelector("#edit-actions-button");
        private readonly By _buttonCloseNote = By.Id("close-note-button");
        private readonly By _buttonDeleteNote = By.Id("delete-note-button");
        private readonly By _buttonSaveAndCloseAction = By.Id("save-action-button");
        private readonly By _buttonSaveAndCloseNote = By.Id("save-note-button");
        private readonly By _calCurrentDay = By.CssSelector(".ui-datepicker-current-day.ng-star-inserted > a");
        private readonly By _calCurrentMonth = By.CssSelector(".ui-datepicker-month");
        private readonly By _calCurrentYear = By.CssSelector(".ui-datepicker-year");
        private readonly By _calDueByDate = By.CssSelector("#due-by");

        //Dropdowns
        private readonly By _drpDwnActionType = By.Id("action-type");
        private readonly By _drpDwnAssignedTo = By.CssSelector("p-dropdown[departmentslist]");
        private readonly By _drpDwnRepNamedisabled = By.CssSelector("p-multiselect[userslist] > div.ui-state-disabled");
        private readonly By _editActionScreenTitle = By.CssSelector("[title=edit]");

        //Inputs
        private readonly By _inptDescription = By.CssSelector("#description-input");
        private readonly By _inptDueByDate = By.CssSelector("#due-by input");


        //Notes
        private readonly By _inputNotes = By.Id("case-notes");
        private readonly By _lblActionType =
            By.CssSelector("app-case-action > form > div > div:nth-child(2) > div:nth-child(1)");
        private readonly By _lblAssignedTo =
            By.CssSelector("app-case-action > form > div > div:nth-child(4) > div:nth-child(1)");

        //Labels
        private readonly By _lblBookingReferences = By.CssSelector("div[id*=actions-and-notes-booking-ref-]");
        private readonly By _lblCustomerNames = By.CssSelector("span[id*='actions-and-notes-name']");
        private readonly By _lblDescription =
            By.CssSelector("app-case-action > form > div > div:nth-child(3) > div:nth-child(1)");
        private readonly By _lblDueDate =
            By.CssSelector("app-case-action > form > div > div:nth-child(5) > div:nth-child(1)");
        private readonly By _listRepName = By.CssSelector("p-multiselect[userslist]");
        private readonly By _lnkActions = By.CssSelector("a[id*='actions-and-add-edit-action']");

        //Links
        private readonly By _lnkNotes = By.CssSelector("a[id*='actions-and-notes-add-edit-note']");

        //Titles
        private readonly By _notesScreenTitle = By.Id("notes-heading");
        private readonly By _selecteddrpDwnActionType = By.CssSelector("#action-type label");
        private readonly By _selecteddrpDwnAssignedTo = By.CssSelector("p-dropdown[departmentslist] label");
        private readonly By _validationMessage = By.CssSelector("div:not([hidden]) > p-message .ui-message-text");

        public CaseActionsAndNotesPage(IWebDriver driver, ILog log) : base(driver, log)
        {
        }

        internal void VerifyCasesAndCategories(Table table)
        {
            //TODO Add back in Customers and Categories when implemented
            /* Do not remove this
            var listOfActualCustomers = Driver.GetTexts(_lblCustomerNames);
            */

            //##################################
            /*
            var listOfActualBookingReferences = Driver.GetTexts(_lblBookingReferences);
            foreach (var row in table.Rows)
            {
                var i = GetBookingReferenceIndex(row["Booking Reference"]);
                Assert.AreEqual(row["Booking Reference"], listOfActualBookingReferences[i],
                    $"The Customer should be {row["Booking Reference"]} instead of {listOfActualBookingReferences[i]}");
            */
            //##################################


                /* Do not remove this
                var i = GetCustomerIndex(row["Customer"]);
                Assert.AreEqual(row["Customer"], listOfActualCustomers[i],
                    $"The Customer should be {row["Customer"]} instead of {listOfActualCustomers[i]}");
                var listOfExpectedCaseCategories = row["Categories"].Trim().ConvertStringIntoList();
                var listOfActualCaseCategories =
                    Driver.FindElements(By.CssSelector($"span[id*=actions-and-notes-{i}-case-category-]"));
                var listOfActualCaseCategoriesTexts = Driver.GetTexts(listOfActualCaseCategories);

                CollectionAssert.AreEqual(listOfExpectedCaseCategories, listOfActualCaseCategoriesTexts,
                    "The selected categories are wrong."); */
            //}
        }

        public void VerifyActionsandNotesPageTitle(string pageTitle)
        {
            Assert.True(Driver.WaitUntilTextPresent(_actionsAndNotesPageTitle),
                "The Actions and notes page title is not displayed");

            var actionsAndNotesPageTitle = Driver.GetText(_actionsAndNotesPageTitle);
            Assert.AreEqual(pageTitle, actionsAndNotesPageTitle, "The Actions and notes page title is incorrect");
        }

        public void VerifyNoteOptionPresent(string noteLabel)
        {
            var listOfNoteLinks = Driver.FindElements(_lnkNotes);

            foreach (var link in listOfNoteLinks)
            {
                Assert.AreEqual(noteLabel, link.Text,
                    $"The Note link text should be {noteLabel} instead of {link.Text}");
                Assert.True(link.TagName.Equals("a"), "The Note link text is not clickable");
            }
        }

        public void VerifyActionOptionPresent(string linkText)
        {
            var listOfActionLinks = Driver.FindElements(_lnkActions);

            foreach (var link in listOfActionLinks)
            {
                var actualText = Driver.GetText(link);
                Assert.AreEqual(linkText, actualText,
                    $"The Action link text should be {linkText} instead of {actualText}");
                Assert.True(link.TagName.Equals("a"), "The Action link text is not clickable");
            }
        }

        public void VerifyContinueBackButtonsVisible()
        {
            var btnBackDisplayed = Driver.FindElement(_btnBack).Displayed;
            var btnBackEnabled = Driver.FindElement(_btnBack).Enabled;
            var btnContinueDisplayed = Driver.FindElement(_btnContinue).Displayed;
            var btnContinueEnabled = Driver.FindElement(_btnContinue).Enabled;

            Assert.True(btnBackDisplayed, "The Back button is is not visible");
            Assert.True(btnBackEnabled, "The Back button is is not enabled");
            Assert.True(btnContinueDisplayed, "The Continue button is is not visible");
            Assert.True(btnContinueEnabled, "The Continue button is is not enabled");
        }

        public void VerifyNoteScreenDisplayed(string expectedStatus)
        {
            switch (expectedStatus)
            {
                case "displayed":
                    WaitForSpinnerToDisappear();
                    Assert.True(Driver.WaitForItem(_notesScreenTitle, 1));
                    break;
                case "not displayed":
                    WaitForSpinnerToDisappear();
                    Assert.True(Driver.WaitUntilElementNotDisplayed(_notesScreenTitle, 1));
                    break;
                default:
                    throw new Exception(expectedStatus + " :is not dialog state");
            }
        }

        public void ClickAddActionlink(string linkText, string customer)
        {
            var i = GetCustomerIndex(customer);
            Driver.WaitUntilContainedTextPresent(By.Id($"actions-and-add-edit-action-{i}"), linkText);

            var actionLinks = Driver.FindElements(_lnkActions);
            var actionLink = actionLinks[i];

            Assert.AreEqual(linkText, Driver.GetText(actionLink), "The action link text is not as expected.");
            Driver.ClickItem(actionLink);
        }

        public void VerifyActionScreenDisplayed(string expectedStatus)
        {
            switch (expectedStatus)
            {
                case "displayed":
                    WaitForSpinnerToDisappear();
                    Assert.True(Driver.WaitForItem(_actionsScreenTitle));
                    WaitForSpinnerToDisappear();
                    break;
                case "not displayed":
                    WaitForSpinnerToDisappear();
                    Assert.True(Driver.WaitUntilElementNotDisplayed(_actionsScreenTitle, 1));
                    WaitForSpinnerToDisappear();
                    break;
                default:
                    throw new Exception(expectedStatus + " :is not dialog state");
            }
        }

        public void VerifyActionScreenPageTitle(string pageTitle)
        {
            Assert.True(Driver.WaitUntilTextPresent(_actionsScreenTitle), "The Actions Screen is not displayed");
            var actionsScreenPageTitle = Driver.GetText(_actionsScreenTitle);
            Assert.AreEqual(pageTitle, actionsScreenPageTitle, "The Actions Screen page title is incorrect");
        }

        public void VerifyActionScreenButtonsEnabled(string addActionButton, string closeButton)
        {
            Assert.True(Driver.WaitForItem(_btnAddActionActionsScreenDialog), "The Add Action button is not displayed");
            Assert.True(Driver.WaitForItem(_btnCloseActionsScreenDialog),
                "The Action Screen Close button is not displayed");
            Assert.True(Driver.FindElement(_btnAddActionActionsScreenDialog).Enabled,
                "The Add Action button is not enabled");
            Assert.True(Driver.FindElement(_btnCloseActionsScreenDialog).Enabled,
                "The Action Screen Close button is not enabled");

            var addActionButtonText = Driver.GetText(_btnAddActionActionsScreenDialog);
            Assert.AreEqual(addActionButton, addActionButtonText, "The Add Action button name is incorrect");

            var closeButtonText = Driver.GetText(_btnCloseActionsScreenDialog);
            Assert.AreEqual(closeButton, closeButtonText, "The Action Screen Close button name is incorrect");
        }

        public void ClickAddActionButton()
        {
            WaitForSpinnerToDisappear();
            Driver.ClickItem(_btnAddActionActionsScreenDialog);
            WaitForSpinnerToDisappear();
        }

        public void VerifyAddActionScreenDisplayed(string expectedStatus)
        {
            switch (expectedStatus)
            {
                case "displayed":
                    WaitForSpinnerToDisappear();
                    Assert.True(Driver.WaitForItem(_addActionScreenTitle));
                    WaitForSpinnerToDisappear();
                    break;
                case "not displayed":
                    WaitForSpinnerToDisappear();
                    Assert.True(Driver.WaitUntilElementNotDisplayed(_addActionScreenTitle));
                    WaitForSpinnerToDisappear();
                    break;
                default:
                    throw new Exception(expectedStatus + " :is not dialog state");
            }
        }

        public void ClickActionScreenCloseButton()
        {
            WaitForSpinnerToDisappear();
            Driver.ClickItem(_btnCloseActionsScreenDialog);
            WaitForSpinnerToDisappear();
        }

        public void ClickActionScreenEditButton(Table table)
        {
            var row = table.Rows[0];
            var description = row["Description"];
            var i = GetActionIndex(description);
            var editButtons = Driver.FindElements(_btnsEditAction);
            Driver.ClickItem(editButtons[i]);
        }

        public void VerifyEditActionScreenDisplayed(string expectedStatus)
        {
            switch (expectedStatus)
            {
                case "displayed":
                    WaitForSpinnerToDisappear();
                    Assert.True(Driver.WaitForItem(_editActionScreenTitle));
                    WaitForSpinnerToDisappear();
                    break;
                case "not displayed":
                    WaitForSpinnerToDisappear();
                    Assert.True(Driver.WaitUntilElementNotDisplayed(_editActionScreenTitle));
                    WaitForSpinnerToDisappear();
                    break;
                default:
                    throw new Exception(expectedStatus + " :is not dialog state");
            }
        }

        public void VerifyEditActionScreenInformation(Table table)
        {
            var row = table.Rows[0];

            Driver.WaitUntilContainedTextPresent(_selecteddrpDwnActionType, row["Action type"]);
            var actualActionType = Driver.GetText(_selecteddrpDwnActionType);
            var actualDescription = Driver.GetInputBoxValue(_inptDescription);
            var actualAssignedTo = Driver.GetText(_selecteddrpDwnAssignedTo);
            var actualDueByDate = Driver.GetInputBoxValue(_inptDueByDate);

            var expectedRepNames = row["Rep name"].ConvertStringIntoList();

            var i = row["Due in Days"];
            var expectedDate = DateTime.Now.AddDays(Convert.ToDouble(i)).ToString("dd/MM/yyyy");

            Assert.AreEqual(row["Action type"], actualActionType,
                $"The Action type is {actualActionType} and should be {row["Action type"]}");
            Assert.AreEqual(row["Description"], actualDescription,
                $"The Description is: '{actualDescription}' and should be: '{row["Description"]}'");
            Assert.AreEqual(row["Assigned to"], actualAssignedTo,
                $"The Assigned to Department is {actualAssignedTo} and should be {row["Assigned to"]}");

            if (!string.IsNullOrWhiteSpace(row["Rep name"]))
            {
                var actualRepNames = Driver.GetAllSelectedMultiselectOptions(_listRepName);
                Assert.AreEqual(expectedRepNames, actualRepNames,
                    $"The Assigned to Rep name is {actualRepNames} and should be {expectedRepNames}");
            }
            else
            {
                VerifyRepsListBoxIsDisabled();
            }

            Assert.AreEqual(expectedDate, actualDueByDate,
                $"The Due by date is: '{actualDueByDate}' and should be: '{row["Due in Days"]}'");
        }

        public void EnterAddActionScreenInformation(Table table)
        {
            var row = table.Rows[0];

            if (row["Action type"].Length > 0)
            {
                Driver.SelectDropDownOption(_drpDwnActionType, row["Action type"]);
            }

            if (row["Description"].Length > 0)
            {
                Driver.EnterText(_inptDescription, row["Description"]);
            }

            if (row["Assigned to"].Length > 0)
            {
                Driver.SelectDropDownOption(_drpDwnAssignedTo, row["Assigned to"]);
            }

            if (row["Rep name"].Length > 0)
            {
                Driver.SelectMultiselectOption(_listRepName, row["Rep name"].ConvertStringIntoList());
            }

            if (row["Due in Days"].Length > 0)
            {
                var i = row["Due in Days"];

                var expectedDate = Driver.CalculateFutureOrPastDate(i);
                var date = Driver.ParseDateTo_ddmmyyyy(expectedDate);
                Driver.SelectDateFromCalender(_addActionScreenDueByDate, date);
            }
        }

        public void ClickAddActionScreenSaveButton()
        {
            Driver.ClickItem(_btnSaveAddActionScreenDialog);
        }

        public void ClickAddNoteFor(string linkText, string customer)
        {
            SelectNotesLink(linkText, customer);
        }

        private int GetCustomerIndex(string customer)
        {
            var customers = Driver.GetTexts(_lblCustomerNames);
            var i = 0;

            foreach (var cust in customers)
            {
                if (customer.Equals(cust))
                {
                    i = customers.IndexOf(cust);
                    break;
                }
            }

            return i;
        }

        private int GetBookingReferenceIndex(string customer)
        {
            var bookingReferences = Driver.GetTexts(_lblBookingReferences);
            var i = 0;

            foreach (var bookingReference in bookingReferences)
            {
                if (customer.Equals(bookingReference))
                {
                    i = bookingReferences.IndexOf(bookingReference);
                    break;
                }
            }

            return i;
        }

        private int GetActionIndex(string description)
        {
            var actions = Driver.GetTexts(_actionRows);

            var i = 0;
            foreach (var action in actions)
            {
                if (description.Equals(action))
                {
                    i = actions.IndexOf(action);
                    break;
                }
            }

            return i;
        }

        public void VerifyNotesScreenIsDisplayed()
        {
            Assert.IsTrue(Driver.WaitForItem(_notesScreenTitle), "The Notes popup is not displayed.");
        }

        public void VerifyNotesPopupElementsAreDisplayed(string delete, string saveAndClose, string close)
        {
            Assert.IsTrue(Driver.WaitForItem(_inputNotes), "The notes field is not displayed.");
            Assert.AreEqual(delete, Driver.GetText(_buttonDeleteNote), "The delete button text is not as expected.");
            Assert.AreEqual(saveAndClose, Driver.GetText(_buttonSaveAndCloseNote),
                "The save and close button text is not as expected.");
            Assert.AreEqual(close, Driver.GetText(_buttonCloseNote), "The close button text is not as expected.");
        }

        public void EnterNoteText(string note)
        {
            Driver.EnterText(_inputNotes, note);
        }

        public void ClickSaveAndClose()
        {
            Driver.ClickItem(_buttonSaveAndCloseNote);
        }

        public void ClickCloseNote()
        {
            Driver.ClickItem(_buttonCloseNote);
        }

        public void VerifyNotesPopupIsNotDisplayed()
        {
            WaitForSpinnerToDisappear();
            Assert.IsTrue(Driver.WaitUntilElementNotDisplayed(_notesScreenTitle),
                "The Notes popup is still displayed.");
            WaitForSpinnerToDisappear();
        }

        public void ClickViewNoteFor(string linkText, string customer)
        {
            SelectNotesLink(linkText, customer);
        }

        private void SelectNotesLink(string linkText, string customer)
        {
            var i = GetCustomerIndex(customer);
            var locator = By.Id($"actions-and-notes-add-edit-note-{i}");
            Driver.WaitUntilContainedTextPresent(locator, linkText);
            Assert.AreEqual(linkText, Driver.GetText(locator), "The note link text is not as expected.");
            Driver.ClickItem(locator);
        }

        public void VerifyNoteContains(string note)
        {
            Assert.AreEqual(note, Driver.GetInputBoxValue(_inputNotes), "The note text is not as expected.");
        }

        public void AddNoteFor(Table table)
        {
            var rows = table.Rows;

            foreach (var row in rows)
            {
                var customer = row["customer"];
                var note = row["note"];

                ClickAddNoteFor("add note", customer);
                VerifyNotesScreenIsDisplayed();
                EnterNoteText(note);
                ClickSaveAndClose();
                VerifyNotesPopupIsNotDisplayed();
            }
        }

        public void VerifyNoteFor(Table table)
        {
            var rows = table.Rows;

            foreach (var row in rows)
            {
                var customer = row["customer"];
                var note = row["note"];

                ClickViewNoteFor("view note", customer);
                VerifyNotesScreenIsDisplayed();
                VerifyNoteContains(note);
                ClickCloseNote();
                VerifyNotesPopupIsNotDisplayed();
            }
        }

        public void VerifySaveAndCloseButtonIsDisabled()
        {
            Assert.IsFalse(Driver.IsElementEnabled(_buttonSaveAndCloseNote),
                "The Save and Close button is not disabled.");
        }

        public void VerifySaveAndCloseButtonIsEnabled()
        {
            Assert.IsTrue(Driver.IsElementEnabled(_buttonSaveAndCloseNote),
                "The Save and Close button is not enabled.");
        }

        public void VerifyAddActionSaveButtonIsDisabled()
        {
            Assert.IsFalse(Driver.IsElementEnabled(_buttonSaveAndCloseAction),
                "The Save button is not disabled.");
        }

        public void VerifyAddActionSaveButtonIsEnabled()
        {
            Assert.IsTrue(Driver.IsElementEnabled(_buttonSaveAndCloseAction),
                "The Save button is not enabled.");
        }

        public void ClearNotesField()
        {
            Driver.Clear(_inputNotes);
            Assert.IsTrue(Driver.WaitUntilTextFieldClear(_inputNotes), "The notes field is not being cleared.");
        }

        public void VerifyNoteFieldIsEmpty()
        {
            Assert.IsEmpty(Driver.GetInputBoxValue(_inputNotes), "The notes field is not empty as expected.");
        }

        public void VerifyDeleteNoteButtonIsDisabled()
        {
            Assert.IsFalse(Driver.IsElementEnabled(_buttonDeleteNote), "The Delete note button is enabled.");
        }

        public void ClickDeleteNote()
        {
            Driver.ClickItem(_buttonDeleteNote);
        }

        public void ClickDeleteNoteFor(Table table)
        {
            var rows = table.Rows;

            foreach (var row in rows)
            {
                var customer = row["customer"];
                ClickViewNoteFor("view note", customer);
                VerifyNotesScreenIsDisplayed();
                ClickDeleteNote();
                Assert.IsTrue(Driver.WaitForItem(ConfirmPopup),
                    "The delete confirmation popup is not being displayed.");
                ClickOnConfirmationPopup("Delete note");
                VerifyConfirmationPopUpIsDismissed();
                VerifyNotesPopupIsNotDisplayed();
            }
        }

        public void VerifyAddActionScreenTitle(string title)
        {
            Assert.True(Driver.WaitUntilTextPresent(_addActionScreenTitle),
                "The Add Actions Screen Title is not displayed");
            var addActionScreenTitle = Driver.GetText(_addActionScreenTitle);
            Assert.AreEqual(title, addActionScreenTitle, "The Add Actions Screen title is incorrect");
        }

        public void VerifyAddActionScreenOptions(string actionType, string description, string assignee)
        {
            Assert.True(Driver.WaitUntilTextPresent(_lblActionType), "The Action Type label is not displayed");
            var lblActionType = Driver.GetText(_lblActionType);
            Assert.AreEqual(actionType + " *", lblActionType, "The Action Type label is incorrect");
            Assert.True(Driver.WaitForItem(_drpDwnActionType), "The Action Type dropdown is not displayed");

            Assert.True(Driver.WaitUntilTextPresent(_lblDescription), "The Description label is not displayed");
            var lblDescription = Driver.GetText(_lblDescription);
            Assert.AreEqual(description + " *", lblDescription, "The Description label is incorrect");
            Assert.True(Driver.WaitForItem(_inptDescription), "The Description input field is not displayed");

            Assert.True(Driver.WaitUntilTextPresent(_lblAssignedTo), "The Assigned To label is not displayed");
            var lblAssignedTo = Driver.GetText(_lblAssignedTo);
            Assert.AreEqual(assignee + " *", lblAssignedTo, "The Assigned To label is incorrect");
            Assert.True(Driver.WaitForItem(_drpDwnAssignedTo), "The Assigned To dropdown is not displayed");
        }

        public void VerifyDueDateOption(string dueDate)
        {
            Assert.True(Driver.WaitUntilTextPresent(_lblDueDate), "The Due Date label is not displayed");
            var lblDueDate = Driver.GetText(_lblDueDate);
            Assert.AreEqual(dueDate + " *", lblDueDate, "The Due Date label is incorrect");
            Assert.True(Driver.WaitForItem(_calDueByDate), "The Due Date input field is not displayed");

            Driver.ClickItem(_calDueByDate);
            var currentDay = Driver.GetText(_calCurrentDay);
            var currentMonth = Driver.GetText(_calCurrentMonth);
            var currentYear = Driver.GetText(_calCurrentYear);
            var date = currentDay + " " + currentMonth + " " + currentYear;


            var changeddate = Convert.ToDateTime(date);
            var actualDate = changeddate.ToString("dd/MM/yyyy");
            //var actualDate = DateTime.ParseExact(date, "dd MMMM yyyy", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");

            var today = DateTime.Now;
            var expectedDate = today.AddDays(1).Date.ToString("dd/MM/yyyy");
            Driver.ClickItem(_calDueByDate);

            Assert.AreEqual(actualDate, expectedDate, "The Due By prefilled date is not correct");
        }

        public void VerifyPastDueByDate(string date)
        {
            if (date.ToLower().Contains("yesterday"))
            {
                date = DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy");
            }

            var pastdate = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            Assert.IsFalse(Driver.IsDateInCalenderEnabled(_addActionScreenDueByDate, pastdate),
                "The past Due by Date is enabled");
        }

        public void ClickCancelButton()
        {
            Driver.ClickItem(_btnCancelAddActionScreenDialog);
        }

        public void VerifyEditActionScreenTitle(string title)
        {
            Assert.AreEqual(title, Driver.GetText(_editActionScreenTitle),
                "The edit action screen title is not as expected.");
        }

        public void VerifyActionTypesInDropDownList(Table table)
        {
            var actualActionTypesList = Driver.GetAllDropDownOptions(_drpDwnActionType);
            var expectedActionTypesList = table.Rows.ToColumnList("Action Type");
            CollectionAssert.AreEqual(actualActionTypesList, expectedActionTypesList,
                $"The actual Action Types: {actualActionTypesList} do not match the expected Action Types: {expectedActionTypesList}");
        }

        public void VerifyActionTypesDropDownListSelection(Table table)
        {
            var expectedActionTypesList = table.Rows.ToColumnList("Action Type");
            Driver.SelectDropDownOption(_drpDwnActionType, expectedActionTypesList[0]);
            Driver.SelectDropDownOption(_drpDwnActionType, expectedActionTypesList[1]);

            var selectedDropDownOption = Driver.GetText(_drpDwnActionType);
            Assert.AreEqual(expectedActionTypesList[1], selectedDropDownOption,
                "More than one option has been selected on the Action Types Drop down list");
        }

        public void VerifyActionsCreated(Table table)
        {
            var rowNumberFlag = 1;
            foreach (var row in table.Rows)
            {
                Driver.WaitForItem(By.CssSelector("#actions-table table tr td"));
                var tableElement = Driver.FindElement(By.CssSelector("#actions-table table"));
                var actualRows = tableElement.FindElements(By.CssSelector("tr"));
                var actualResult = actualRows[rowNumberFlag].FindElements(By.CssSelector("td"));
                Assert.True(actualResult[0].Text == row["Action type"],
                    $"The Action type should be {row["Action type"]} instead of {actualResult[0].Text}");
                Assert.True(actualResult[1].Text == row["Description"],
                    $"The Description should be {row["Description"]} instead of {actualResult[1].Text}");

                var findAssignees = actualResult[2].FindElements(By.TagName("p"));
                var actualAssigneesList = Driver.GetTexts(findAssignees);
                var expectedAssigneesList = row["Assigned to"].ConvertStringIntoList();
                CollectionAssert.AreEqual(actualAssigneesList, expectedAssigneesList,
                    $"The actual action assignees {actualAssigneesList} do not match expected action assignees {expectedAssigneesList}");

                var i = row["Due in Days"];
                var expectedDate = DateTime.Now.AddDays(Convert.ToDouble(i))
                    .ToString("dd MMM yy", CultureInfo.InvariantCulture);
                var date = DateTime.ParseExact(expectedDate, "dd MMM yy", CultureInfo.InvariantCulture);
                Assert.True(actualResult[3].Text == expectedDate,
                    $"The due by should be {date} instead of {actualResult[3].Text}");

                rowNumberFlag++;
            }
        }

        public void CreateActionsFor(Table table)
        {
            var rows = table.Rows;

            var actionsCount = 0;
            foreach (var row in rows)
            {
                var customer = row["Customer"];

                ClickActionLinkFor(customer, $"{actionsCount} action");
                var expectedStatus = "displayed";
                VerifyActionScreenDisplayed(expectedStatus);
                ClickAddActionButton();
                VerifyAddActionScreenDisplayed(expectedStatus);
                EnterAddActionScreenInformation(table);
                ClickAddActionScreenSaveButton();
                expectedStatus = "not displayed";
                VerifyAddActionScreenDisplayed(expectedStatus);
                ClickActionScreenCloseButton();
                actionsCount++;
            }
        }

        public void ClickActionLinkFor(string customer, string actionsCount = null)
        {
            var i = GetCustomerIndex(customer);
            Driver.WaitUntilContainedTextPresent(By.Id($"actions-and-add-edit-action-{i}"), actionsCount);
            Driver.ScrollElementIntoViewById($"actions-and-add-edit-action-{i}");
            Driver.ClickItem(By.Id($"actions-and-add-edit-action-{i}"));
        }

        public void SelectAssignedToDropDown(string option)
        {
            Driver.SelectDropDownOption(_drpDwnAssignedTo, option);
        }

        public void VerifyRepsListBoxIsEnabled()
        {
            Assert.IsTrue(Driver.IsElementEnabled(_listRepName), "The reps list box is disabled.");
        }

        public void VerifyRepsListBoxIsDisabled()
        {
            Assert.IsTrue(Driver.WaitForItem(_drpDwnRepNamedisabled), "The reps list box is enabled.");
        }

        public void SelectRepsListbox(Table table)
        {
            var reps = table.Rows.ToColumnList("Reps");
            Driver.SelectMultiselectOption(_listRepName, reps);
        }

        public void DeselectActionType()
        {
            Driver.DeselectDropDownOption(_drpDwnActionType);
        }

        public void VerifyValidationMessage(string message)
        {
            Assert.AreEqual(message, Driver.GetText(_validationMessage),
                "The validation message is not being displayed.");
        }

        public void VerifySaveActionButtonIsDisabled()
        {
            Assert.IsFalse(Driver.IsElementEnabled(_btnSaveAddActionScreenDialog));
        }

        public void ClearActionDescription()
        {
            Driver.WaitUntilTextPresent(_inptDescription, 1);
            Driver.Clear(_inptDescription);
        }

        public void ClearAssignedToDropDown()
        {
            Driver.DeselectDropDownOption(_drpDwnAssignedTo);
        }

        public void ClearRepsField()
        {
            Driver.SelectAllMultiselectOptions(_listRepName);
            Driver.DeselectAllMultiselectOptions(_listRepName);
        }

        public void ChangeActionInformationTo(Table table)
        {
            var row = table.Rows[0];

            Driver.DeselectDropDownOption(_drpDwnActionType);
            Driver.SelectDropDownOption(_drpDwnActionType, row["Action type"]);

            Driver.EnterText(_inptDescription, row["Description"]);

            Driver.DeselectDropDownOption(_drpDwnAssignedTo);
            Driver.SelectDropDownOption(_drpDwnAssignedTo, row["Assigned to"]);

            if (!string.IsNullOrWhiteSpace(row["Rep name"]))
            {
                Driver.DeselectAllMultiselectOptions(_listRepName);
                Driver.SelectMultiselectOption(_listRepName, row["Rep name"]);
            }

            if (row["Due in Days"].Length > 0)
            {
                var i = row["Due in Days"];

                var expectedDate = DateTime.Now.AddDays(Convert.ToDouble(i))
                    .ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                var date = DateTime.ParseExact(expectedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                Driver.SelectDateFromCalender(_addActionScreenDueByDate, date);
            }
        }

        public void VerifyActionsFor(string customer, Table table)
        {
            ClickActionLinkFor(customer);
            var expectedStatus = "displayed";
            VerifyActionScreenDisplayed(expectedStatus);
            VerifyActionsCreated(table);
            ClickActionScreenCloseButton();
        }

        public void SelectActionTypeFromDropDown(string actionType)
        {
            Driver.SelectDropDownOption(_drpDwnActionType, actionType);
        }

        public void EnterDescription(string description)
        {
            Driver.EnterText(_inptDescription, description);
        }

        public void DeselectAssignedTo()
        {
            Driver.DeselectDropDownOption(_drpDwnAssignedTo);
        }

        public void SelectReps(string repName)
        {
            Driver.SelectMultiselectOption(_listRepName, repName);
        }

        public void VerifyNoNotesFor(Table table)
        {
            var rows = table.Rows;

            foreach (var row in rows)
            {
                var customer = row["customer"];
                ClickViewNoteFor("add note", customer);
                VerifyNotesScreenIsDisplayed();
                VerifyNoteContains(string.Empty);
                ClickCloseNote();
                VerifyNotesPopupIsNotDisplayed();

            }
        }

        public void VerifyBookingReferencesAppearOnce()
        {
            
            var listOfActualBookingReferences = Driver.GetTexts(_lblBookingReferences);
            var isUnique = listOfActualBookingReferences.Distinct().Count() == listOfActualBookingReferences.Count();
            Assert.True(isUnique, "A booking reference in the list appears more than once");
            
        }


        public void ClickActionsAndNotesContinueButton()
        {
            Driver.ClickItem(_btnContinue);
        }

        public void VerifyOrderOfBookingReferences()
        {
            var actualOrderOfBookingReferences = Driver.GetTexts(_lblBookingReferences);
            var expectedOrderOfBookingReferences = actualOrderOfBookingReferences.OrderBy(x => x).ToList();
            CollectionAssert.AreEqual(actualOrderOfBookingReferences, expectedOrderOfBookingReferences, "The order of booking references on the actions and notes page is incorrect");
        }
    }
}