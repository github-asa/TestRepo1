using J2BIOverseasOps.Extensions;
using J2BIOverseasOps.Pages.RoleManagement;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Pages.PreSeasonChecklist
{
    public class ViewActionPage : BasePage
    {
        private readonly By _hotelName = By.Id("view-action-hotel-name-label");
        private readonly By _destinationName = By.Id("view-action-destination-name-label");
        private readonly By _resortName = By.Id("view-action-resort-name-label");
        private readonly By _creatorName = By.Id("view-action-creater-name-label");
        private readonly By _createdDate = By.Id("view-action-created-date-label");
        private readonly By _summaryTitle = By.Id("view-action-summary-text");
        private readonly By _actionTitle = By.Id("view-action-action-header-label");
        private readonly By _action = By.Id("view-action-action-reason-label");
        private readonly By _descriptionTitle= By.Id("view-action-action-reason-description-header");
        private readonly By _description = By.Id("view-action-action-reason-description-label");
        private readonly By _questionsTitle = By.Id("view-action-failed-q-header");
        private readonly By _questions = By.CssSelector("[id*='view-action-failed-q-text']");
        private readonly By _questionsNotesTitle = By.CssSelector("[id*='view-action-failed-q-notes-header'");
        private readonly By _questionsNotes = By.CssSelector("[id*='view-action-failed-q-notes-text'");
        private readonly By _assignmentTitle = By.Id("view-action-assignment-header");
        private readonly By _assignedToTitle = By.Id("view-action-assignmet-assigned-to-label");
        private readonly By _assignedTo = By.Id("view-action-assignmet-assigned-to-text");
        private readonly By _personTitle = By.Id("view-action-assignmet-assigned-to-person-label");
        private readonly By _person = By.CssSelector("#view-action-assignmet-assigned-to-list p-dropdown");
        private readonly By _personSelected = By.CssSelector("#view-action-assignmet-assigned-to-list p-dropdown .ui-dropdown-label");
        private readonly By _progressStatusTitle = By.Id("view-action-progress-status-header");
        private readonly By _progressStatus = By.Id("view-action-progress-status");
        private readonly By _dueDateTitle = By.Id("view-action-progress-due-date-label");
        private readonly By _dueDate = By.Id("view-action-progress-due-date-calander");
        private readonly By _dueDateInput = By.CssSelector("#view-action-progress-due-date-calander input");
        private readonly By _notesTitle = By.Id("view-action-notes-section-header");
        private readonly By _actionNotes = By.Id("view-action-notes-section-text");
        private readonly By _btnAddNote = By.Id("view-action-notes-section-add-note-btn");
        private readonly By _linkedActionsTitle = By.Id("view-action-linked-action-header");
        private readonly By _linkedActions = By.CssSelector("[id*='view-action-linked-action-text']");
        private readonly By _addLinkedAction = By.Id("view-action-linked-action-add-new-linked-action-btn");
        private readonly By _save = By.Id("view-action-save-btn");

        public ViewActionPage(IWebDriver driver, ILog log) : base(driver, log)
        {
        }
        public void VerifyHeaderDetails(Table table)
        {
            var row = table.Rows[0];

            Assert.AreEqual(row["Hotel"], Driver.GetText(_hotelName), "The hotel name is not as expected.");
            Assert.AreEqual(row["Destination"], Driver.GetText(_destinationName), "The destination name is not as expected.");
            Assert.AreEqual(row["Resort"], Driver.GetText(_resortName), "The resort name is not as expected.");
            Assert.AreEqual(row["Creator"], Driver.GetText(_creatorName), "The creator name is not as expected.");
            Assert.AreEqual(row["Created"], Driver.GetText(_createdDate), "The created date is not as expected.");
        }

        public void VerifySummaryDetailsAreDisplayed(Table table)
        {
            var row = table.Rows[0];
            Assert.AreEqual(row["Action"], Driver.GetText(_action), "The action is not as expected.");
            Assert.AreEqual(row["Description"], Driver.GetText(_description), "The description is not as expected.");
        }

        public void VerifySummaryAndFailedQuestionsTitlesAreDisplayed(string summary, string action, string description, string failedquestions, string notes)
        {
            Assert.AreEqual(summary, Driver.GetText(_summaryTitle).Trim(), "The summary title is not as expected.");
            Assert.AreEqual(action, Driver.GetText(_actionTitle).Trim(), "The action title is not as expected.");
            Assert.AreEqual(description, Driver.GetText(_descriptionTitle).Trim(), "The description title is not as expected.");
            Assert.AreEqual(failedquestions, Driver.GetText(_questionsTitle).Trim(), "The failed questions title is not as expected.");
            Assert.AreEqual(notes, Driver.GetText(_questionsNotesTitle).Trim(), "The questions notes title is not as expected.");            
        }

        public void VerifyAssignmentHeadingsAreDisplayed(string assignment, string assignedto, string person, string progressstatus, string duedate, string notes, string linkedactions)
        {
            Assert.AreEqual(assignment, Driver.GetText(_assignmentTitle).Trim(), "The assignment title is not as expected.");
            Assert.AreEqual(assignedto, Driver.GetText(_assignedToTitle).Trim(), "The assigned to title is not as expected.");
            Assert.AreEqual(person, Driver.GetText(_personTitle).Trim(), "The person title is not as expected.");
            Assert.AreEqual(progressstatus, Driver.GetText(_progressStatusTitle).Trim(), "The progress status title is not as expected.");
            Assert.AreEqual(duedate, Driver.GetText(_dueDateTitle).Trim(), "The due date title is not as expected.");
            Assert.AreEqual(notes, Driver.GetText(_notesTitle).Trim(), "The notes title is not as expected.");
            Assert.AreEqual(linkedactions, Driver.GetText(_linkedActionsTitle).Trim(), "The linked actions title is not as expected.");
        }

        public void VerifySummaryAndFailedQuestionsDetailsAreDisplayed(Table table)
        {
            var rows = table.Rows;
            var questions = Driver.GetTexts(_questions);
            var notes = Driver.GetTexts(_questionsNotes);
            var i = 0;
            foreach (var row in rows)
            {
                Assert.AreEqual(row["Question"], questions[i], $"The question text is not as expected for failed Q{i}");
                Assert.AreEqual(row["Notes"], notes[i], $"The notes text is not as expected for failed Q{i}");
                i++;
            }
        }

        public void VerifyAssignmentDetailsAreDisplayed(Table table)
        {
            var row = table.Rows[0];
            if (row.ContainsKey("AssignedTo"))
            {
                Assert.AreEqual(row["AssignedTo"], Driver.GetText(_assignedTo), "The assigned to field is not as expected.");
            }

            if (row.ContainsKey("Person"))
            {
                Assert.AreEqual(row["Person"], Driver.GetText(_personSelected),
                    "The person selected is not as expected.");
            }

            if (row.ContainsKey("ProgressStatus"))
            {
                Assert.AreEqual(row["ProgressStatus"], Driver.GetSelectedRadioButtonsOption(_progressStatus),
                    "The progress status selected is not as expected.");
            }

            if (row.ContainsKey("DueDate"))
            {
                var expectedDate = string.IsNullOrWhiteSpace(row["DueDate"])
                    ? row["DueDate"] : Driver.CalculateFutureOrPastDate(row["DueDate"]);
                Driver.WaitUntilValuePresent(_dueDateInput);
                Assert.AreEqual(expectedDate, Driver.GetInputBoxValue(_dueDateInput),
                    "The due date field is not as expected.");
            }
        }

        public void VerifyNotesAreDisplayed(Table table)
        {
            var rows = table.Rows;

            var notes = Driver.GetTexts(_actionNotes);

            var i = 0;
            foreach (var row in rows)
            {
                Assert.AreEqual(row["Notes"], notes[i], "The note is not as expected.");
                i++;
            }
        }

        public void VerifyLinkedActionsAreDisplayed(Table table)
        {
            var rows = table.Rows;
            var linkedActions = Driver.GetTexts(_linkedActions);

            var i = 0;
            foreach (var row in rows)
            {
                Assert.AreEqual(row["Link"], linkedActions[i], "The linked action is not as expected.");
                i++;
            }
        }

        public void UpdateTheAssignmentDetails(Table table)
        {
            var row = table.Rows[0];
            if (row.ContainsKey("Person"))
            {
                Driver.SelectDropDownOption(_person, row["Person"], true);
            }

            if (row.ContainsKey("ProgressStatus"))
            {
                Driver.SelectRadioButtonOption(_progressStatus, row["ProgressStatus"]);
            }

            if (row.ContainsKey("DueDate"))
            {
                var expectedDate = Driver.CalculateFutureOrPastDate(row["DueDate"]);
                var date = Driver.ParseDateTo_ddmmyyyy(expectedDate);
                Driver.SelectDateFromCalender(_dueDate, date);
            }
        }

        public void ClickSave()
        {
            Driver.ClickItem(_save);
        }
    }
}
