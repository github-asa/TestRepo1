using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using J2BIOverseasOps.Extensions;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Pages.Customer_Interaction.CreateACaseActions
{
    public class CaseAddActionsPage: BasePage
    {
        private By _actionDropdown => By.Id("create-action-reasons-ddl");
        private By _descriptionTextBox => By.Id("create-action-description");
        private By _assignToDropdown => By.Id("create-action-departments-ddl");
        private By _assignToPersonDropdown => By.Id("create-action-persons-ddl");
        private By _dueDatePicker => By.Id("create-action-due-date-calender");
        private By _dueDateTextBox => By.CssSelector("#create-action-due-date-calender input");

        private By _readonlydueDateTextBox => By.CssSelector("#create-action-due-date-calender input[readonly]");

        private By _saveButton => By.Id("view-action-save-btn");
        private By Heading => By.Id("view-action-header");
        private By ActionHeader => By.Id("create-action-reasons-ddl-label");
        private By DescriptionHeader => By.Id("create-action-description-label");
        private By AssignToHeader => By.Id("create-action-departments-ddl-label");
        private By AssignToPersonHeader => By.Id("create-action-persons-ddl-label");
        private By DueDateHeader => By.Id("create-action-due-date-calender-label");
        private By BackToOverviewButton => By.Id("overview-button");
        private By TotalActionsCount => By.Id("actions-notes");

        public CaseAddActionsPage(IWebDriver driver, ILog log) : base(driver, log)
        {
        }

        public void EnterActionDetails(Table table)
        {
            var row = table.Rows[0];
            if (row.ContainsKey("Action"))
            {
                Driver.SelectDropDownOption(_actionDropdown, row["Action"]);
            }

            if (row.ContainsKey("Description"))
            {
                Driver.EnterText(_descriptionTextBox, row["Description"]);
            }

            if (row.ContainsKey("AssignToDepartment") && !string.IsNullOrWhiteSpace(row["AssignToDepartment"]))
            {
                Driver.SelectDropDownOption(_assignToDropdown, row["AssignToDepartment"]);
            }

            if (row.ContainsKey("AssignToPerson") && !string.IsNullOrWhiteSpace(row["AssignToPerson"]))
            {
                Driver.SelectDropDownOption(_assignToPersonDropdown, row["AssignToPerson"]);
            }

            if (row.ContainsKey("DueDate") && !string.IsNullOrWhiteSpace(row["DueDate"]))
            {
                var expectedDate = Driver.CalculateFutureOrPastDate(row["DueDate"]);
                var date = Driver.ParseDateTo_ddmmyyyy(expectedDate);
                Driver.SelectDateFromCalender(_dueDatePicker, date);
            }
        }

        public void ClickSave()
        {
            Driver.ClickItem(_saveButton);
        }

        public void VerifyActionsList(Table table)
        {
            var expectedActions = table.Rows.ToColumnList("Actions");
            var actions = Driver.GetAllDropDownOptions(_actionDropdown);

            CollectionAssert.AreEqual(expectedActions, actions, "The actions are not as expected.");
        }

        public void VerifyAssignToPersonIsDisabled()
        {
            Assert.IsTrue(Driver.IsElementDisabled(_assignToPersonDropdown),
                "The Assign to person dropdown is enabled.");
        }

        public void VerifyAssignToPersonIsEnabled()
        {
            Assert.IsFalse(Driver.IsElementDisabled(_assignToPersonDropdown),
                "The Assign to person dropdown is disabled.");
        }

        public void VerifyAddActionHeadings(string header, string action, string description, string assignTo, string assignToPerson, string dueDate)
        {
            Assert.AreEqual(header, Driver.GetText(Heading), "The header is not as expected");
            Assert.AreEqual(action, Driver.GetText(ActionHeader), "The header is not as expected");
            Assert.AreEqual(description, Driver.GetText(DescriptionHeader), "The header is not as expected");
            Assert.AreEqual(assignTo, Driver.GetText(AssignToHeader), "The header is not as expected");
            Assert.AreEqual(assignToPerson, Driver.GetText(AssignToPersonHeader), "The header is not as expected");
            Assert.AreEqual(dueDate, Driver.GetText(DueDateHeader), "The header is not as expected");
        }

        public void VerifyUsersCaseActions()
        {
            //Get actual order of assignees
            var actualOrderOfAssignees = Driver.GetAllDropDownOptions(_assignToPersonDropdown);
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
            var actual = Driver.GetAllDropDownOptions(_assignToPersonDropdown);

            CollectionAssert.IsSubsetOf(expectedUsernames, actual, "The expected usernames are not in the assign to person dropdown.");
        }

        public void VerifyDueDatesAreDisabled(string days)
        {
            var expectedDate = Driver.CalculateFutureOrPastDate(days);
            var date = Driver.ParseDateTo_ddmmyyyy(expectedDate);
            Assert.IsFalse(Driver.IsDateInCalenderEnabled(_dueDatePicker, date), "The dates in the past are not diabled.");
        }

        public void EnterADueDate(string date)
        {
            Driver.EnterText(_dueDateTextBox, date);
        }

        public void VerifyDueDate(string days)
        {
            var expectedDate = Driver.CalculateFutureOrPastDate(days);
            Assert.AreEqual(expectedDate, Driver.GetInputBoxValue(_dueDateTextBox), $"The due date is not set to {days} day(s) in the future.");
        }

        public void VerifyAssignToPersonIsEmpty()
        {
            Assert.AreEqual("Select a person", Driver.GetSelectedDropDownOption(_assignToPersonDropdown), "A user is still selected.");
        }

        public void ClearAssignToPerson()
        {
            var clearElement = Driver.FindElement(_assignToPersonDropdown).FindElement(By.TagName("i"));
            Driver.ClickItem(clearElement);
        }

        public void VerifyDueDateIsReadonly()
        {
            var duedate = Driver.FindElement(_dueDateTextBox);
            Assert.IsNotNull(duedate, "The duedate textbox is not readonly.");
        }

        public void ClickBackToOverview()
        {
            Driver.ClickItem(BackToOverviewButton);
        }

        public void VerifyTotalActionsCount(string total)
        {
            var count = Driver.GetText(TotalActionsCount);
            count = new string(count.SkipWhile(c => !char.IsDigit(c))
                .TakeWhile(char.IsDigit)
                .ToArray());

            Assert.AreEqual(total, count, "The total actions are not as expected.");
        }

        public void VerifyDueActionsCount(string total)
        {
            var count = Driver.GetText(TotalActionsCount);

            var numbers = ((from Match m in Regex.Matches(count, @"\d+") select m.Value).ToList());

            Assert.AreEqual(total, numbers[1], "The due actions are not as expected.");
        }
    }
}

