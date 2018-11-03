using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using J2BIOverseasOps.Extensions;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Pages.Customer_Interaction.CreateACaseActions
{
    public class CaseEditActionPage : BasePage
    {
        private static int _index;

        public CaseEditActionPage(IWebDriver driver, ILog log) : base(driver, log)
        {
        }
        private By Heading => By.Id("eros-page-heading");
        private By ActionHeader => By.Id("edit-action-reasons-ddl-label");
        private By DescriptionHeader => By.Id("edit-action-description-label");
        private By AssignToHeader => By.Id("edit-action-departments-ddl-label");
        private By AssignToPersonHeader => By.Id("edit-action-persons-ddl-label");
        private By DueDateHeader => By.Id("edit-action-due-date-calender-label");
        private By CreatedBy => By.Id("view-action-creater-name-label");
        private By CreatedByDate => By.Id("view-action-created-date-label");
        private By ModifiedBy => By.Id("view-action-last-updated-by-label");
        private By ModifiedByDate => By.Id("view-action-created-date-label");
        private By CategoryLbl => By.Id("edit-action-reason-text");
        private By DescriptionLbl => By.Id("edit-action-description-text");
        private By AssignToDropdown => By.Id("edit-action-departments-ddl");
        private By ClearAssignToDropdown => By.CssSelector("#edit-action-departments-ddl i");
        private By AssignToPersonDropdown => By.Id("edit-action-persons-ddl");
        private By ClearAssignToPersonDropdown => By.CssSelector("#edit-action-persons-ddl i");
        private By DueDatePicker => By.Id("edit-action-due-date-calender");
        private By DueDateTextBox => By.CssSelector("#edit-action-due-date-calender input");
        private By SaveButton => By.Id("edit-action-save-btn");
        private By NoteTextArea => By.Id("edit-action-new-note");
        private By Note => By.CssSelector($"[id^='edit-action-previous-note-{_index}']");
        private By NoteUser => By.CssSelector($"[id^='edit-action-note-created-by-{_index}']");
        private By NoteDate => By.CssSelector($"[id^='edit-action-note-edit-date-{_index}']");
        private By StatusDropdown => By.CssSelector("#edit-action-status-ddl");

        public void VerifyAddActionHeadings(string header, string action, string description, string assignTo, string assignToPerson, string dueDate)
        {
            Assert.AreEqual(header, Driver.GetText(Heading), "The header is not as expected");
            Assert.AreEqual(action, Driver.GetText(ActionHeader), "The header is not as expected");
            Assert.AreEqual(description, Driver.GetText(DescriptionHeader), "The header is not as expected");
            Assert.AreEqual(assignTo, Driver.GetText(AssignToHeader), "The header is not as expected");
            Assert.AreEqual(assignToPerson, Driver.GetText(AssignToPersonHeader), "The header is not as expected");
            Assert.AreEqual(dueDate, Driver.GetText(DueDateHeader), "The header is not as expected");
        }

        public void VerifyEditDetails(Table table)
        {
            var row = table.Rows[0];

            var createdDate = Driver.CalculateFutureOrPastDate(row["CreatedDate"]);
            var lastUpdatedOn = Driver.CalculateFutureOrPastDate(row["LastUpdatedOn"]);
            var dueDate = Driver.CalculateFutureOrPastDate(row["DueDate"]);

            var actualCreatedDate = Driver.GetText(CreatedByDate).Split(' ');
            var actualModifiedByDate = Driver.GetText(ModifiedByDate).Split(' ');
            var actualCreatedTime = DateTime.ParseExact(actualCreatedDate[1], "HH:mm", CultureInfo.InvariantCulture);
            var createdbyTimeDiff = DateTime.Now.TimeOfDay.TotalMinutes - actualCreatedTime.TimeOfDay.TotalMinutes;
            var actualModifiedTime = DateTime.ParseExact(actualModifiedByDate[1], "HH:mm", CultureInfo.InvariantCulture);
            var modifiedTimeDiff = DateTime.Now.TimeOfDay.TotalMinutes - actualModifiedTime.TimeOfDay.TotalMinutes;

            Assert.AreEqual(row["CreatedBy"], Driver.GetText(CreatedBy), "The Created By is not as expected");
            Assert.AreEqual(createdDate, actualCreatedDate[0], "The Created Date is not as expected");
            Assert.AreEqual(row["LastUpdatedBy"], Driver.GetText(ModifiedBy), "The Last Updated By is not as expected");
            Assert.AreEqual(lastUpdatedOn, actualModifiedByDate[0], "The Last Updated Date is not as expected");
            Assert.AreEqual(row["Category"], Driver.GetText(CategoryLbl), "The Category is not as expected");
            Assert.AreEqual(row["Description"], Driver.GetText(DescriptionLbl), "The Description is not as expected");
            Assert.AreEqual(row["AssignedDepartment"], Driver.GetSelectedDropDownOption(AssignToDropdown), "The Assigned Department is not as expected");
            Assert.LessOrEqual(createdbyTimeDiff, 05, "The created by time is over 5 minutes");
            Assert.LessOrEqual(modifiedTimeDiff, 05, "The last updated by time is over 5 minutes");

            if (row.ContainsKey("AssignedUsers"))
            {
               Assert.AreEqual(row["AssignedUsers"], Driver.GetSelectedDropDownOption(AssignToPersonDropdown), "The Assigned Users is not as expected");
            }
            Assert.AreEqual(dueDate, Driver.GetInputBoxValue(DueDateTextBox), "The Due Date is not as expected");           
        }

        public void UpdateAction(Table table)
        {
            var row = table.Rows[0];

            if (row.ContainsKey("Status") && !string.IsNullOrWhiteSpace(row["Status"]))
            {
                Driver.SelectDropDownOption(StatusDropdown, row["Status"]);
            }

            if (row.ContainsKey("AssignToDepartment") && !string.IsNullOrWhiteSpace(row["AssignToDepartment"]) && !row["AssignToDepartment"].Equals("MyDepartment"))
            {
                Driver.SelectDropDownOption(AssignToDropdown, row["AssignToDepartment"]);
            }
            else if(row.ContainsKey("AssignToDepartment") && string.IsNullOrWhiteSpace(row["AssignToDepartment"]) && !row["AssignToDepartment"].Equals("MyDepartment"))
            {
                Driver.ClickItem(ClearAssignToDropdown);
            }
            else if (row.ContainsKey("AssignToDepartment") && row["AssignToDepartment"].Equals("MyDepartment"))
            {               
                Driver.SelectDropDownOption(AssignToDropdown, CommonPageElements._currentUser.department);
            }

            if (row.ContainsKey("AssignToPerson") && !string.IsNullOrWhiteSpace(row["AssignToPerson"]))
            {
                Driver.SelectDropDownOption(AssignToPersonDropdown, row["AssignToPerson"]);
            }
            else if (row.ContainsKey("AssignToPerson") && string.IsNullOrWhiteSpace(row["AssignToPerson"]) && !Driver.IsElementDisabled(AssignToPersonDropdown))
            {
                Driver.ClickItem(ClearAssignToPersonDropdown);
            }

            if (row.ContainsKey("DueDate") && !string.IsNullOrWhiteSpace(row["DueDate"]))
            {
                var expectedDate = Driver.CalculateFutureOrPastDate(row["DueDate"]);
                var date = Driver.ParseDateTo_ddmmyyyy(expectedDate);
                Driver.SelectDateFromCalender(DueDatePicker, date);
            }
        }

        public void ClickSave()
        {
            Driver.ClickItem(SaveButton);
        }

        public void VerifyUsersCaseActions()
        {
            //Get actual order of assignees
            var actualOrderOfAssignees = Driver.GetAllDropDownOptions(AssignToPersonDropdown);
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

        public void VerifyUserListContains(Table table)
        {
            var expectedUsernames = table.Rows.ToColumnList("AssignToPerson");
            var actual = Driver.GetAllDropDownOptions(AssignToPersonDropdown);

            CollectionAssert.IsSubsetOf(expectedUsernames, actual, "The expected usernames are not in the assign to person dropdown.");
        }

        public void VerifyAssignToPersonIsDisabled()
        {
            Assert.IsTrue(Driver.IsElementDisabled(AssignToPersonDropdown),
                "The Assign to person dropdown is enabled.");
        }

        public void VerifyAssignToPersonIsEnabled()
        {
            Assert.IsFalse(Driver.IsElementDisabled(AssignToPersonDropdown),
                "The Assign to person dropdown is disabled.");
        }

        public void VerifyAssignToPersonIsEmpty()
        {
            Assert.AreEqual("Select a person", Driver.GetSelectedDropDownOption(AssignToPersonDropdown), "A user is still selected.");
        }

        public void ClearAssignToPerson()
        {
            var clearElement = Driver.FindElement(AssignToDropdown).FindElement(By.TagName("i"));
            Driver.ClickItem(clearElement);
        }

        public void VerifyDueDatesAreDisabled(string days)
        {
            var expectedDate = Driver.CalculateFutureOrPastDate(days);
            var date = Driver.ParseDateTo_ddmmyyyy(expectedDate);
            Assert.IsFalse(Driver.IsDateInCalenderEnabled(DueDatePicker, date), "The dates in the past are not diabled.");
        }

        public void EnterADueDate(string date)
        {
            Driver.EnterText(DueDateTextBox, date);
        }

        public void AddNote(string note)
        {
            Driver.EnterText(NoteTextArea, note);
        }

        public void VerifyNotes(Table table)
        {
            var expectedDate = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            _index = 0;
            foreach (var row in table.Rows)
            {
                Assert.AreEqual(row["User"], Driver.GetText(NoteUser), "The user assigned to the action note is not as expected.");
                Assert.AreEqual(row["Note"], Driver.GetText(Note), "The action note is not as expected.");
                var date = Driver.GetText(NoteDate);
                var isFormatCorrect = DateTime.TryParseExact(date, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var saveDateTime);
                Assert.IsTrue(date.Contains(expectedDate), "The user assigned to the action note is not as expected.");
                Assert.IsTrue(isFormatCorrect, "The date time format is not as expected.");
                var mins = (DateTime.Now - saveDateTime).TotalMinutes;
                Assert.LessOrEqual(mins, 5, "The time is not as expected.");
                _index++;
            }           
        }

        public void VerifyDueDateIsReadonly()
        {
            var duedate = Driver.FindElement(DueDateTextBox);
            Assert.IsNotNull(duedate, "The duedate textbox is not readonly.");
        }
    }
}