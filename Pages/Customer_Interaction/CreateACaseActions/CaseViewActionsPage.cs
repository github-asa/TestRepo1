using J2BIOverseasOps.Extensions;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Pages.Customer_Interaction.CreateACaseActions
{
    public class CaseViewActionsPage : BasePage
    {
        private int _index;


        public CaseViewActionsPage(IWebDriver driver, ILog log) : base(driver, log)
        {
        }

        private By AddActionButton => By.Id("add-action-button");
        private By ActiveCategories => By.Id($"active-actions-category-{_index}");
        private By ActiveStatus => By.Id($"active-actions-status-{_index}");
        private By ActiveDueDate => By.Id($"active-actions-dueDate-{_index}");
        private By ActiveAssignedDept => By.Id($"active-actions-assignedToDept-{_index}");
        private By ActiveAssignedUser => By.Id($"active-actions-assignedTo-{_index}");
        private By ClosedCategories => By.Id($"closed-actions-category-{_index}");
        private By ClosedStatus => By.Id($"closed-actions-status-{_index}");
        private By ClosedDueDate => By.Id($"closed-actions-dueDate-{_index}");
        private By ClosedAssignedDept => By.Id($"closed-actions-assignedToDept-{_index}");
        private By ClosedAssignedUser => By.Id($"closed-actions-assignedTo-{_index}");
        private By Heading => By.Id("eros-page-heading");
        private By Subheading1 => By.Id("actions-heading");
        private By Subheading2 => By.CssSelector("p-card + p-card #actions-heading");
        private By ActiveActionIdHeading => By.Id("active-actions-header-actionid");
        private By ActiveCategoryHeading => By.Id("active-actions-header-category");
        private By ActiveStatusHeading => By.Id("active-actions-header-status");
        private By ActiveDueDateHeading => By.Id("active-actions-header-dueDate");
        private By ActiveAssignedDeptHeading => By.Id("active-actions-header-assignedToDept");
        private By ActiveAssignedUserHeading => By.Id("active-actions-header-assignedTo");
        private By ClosedCaseIdHeading => By.Id("closed-actions-header-actionid");
        private By ClosedCategoryHeading => By.Id("closed-actions-header-category");
        private By ClosedStatusHeading => By.Id("closed-actions-header-status");
        private By ClosedDueDateHeading => By.Id("closed-actions-header-dueDate");
        private By ClosedAssignedDeptHeading => By.Id("closed-actions-header-assignedToDept");
        private By ClosedAssignedUserHeading => By.Id("closed-actions-header-assignedTo");
        private By ActionsIdLink => By.CssSelector($"#active-actions-actionid-{_index} a");
        private By ClosedActionsId => By.Id($"closed-actions-actionid-{_index}");
        private By ActiveActionsId => By.Id($"active-actions-actionid-{_index}");

        public void ClickAddAction()
        {
            Driver.ClickItem(AddActionButton);
        }

        public void VerifyActiveActionsTable(Table table)
        {
            var rows = table.Rows;

            _index = 0;
            foreach (var row in rows)
            {
                if (row.ContainsKey("ActionId"))
                {
                    Assert.AreEqual(row["ActionId"], Driver.GetText(ActiveActionsId),
                        "The action action id is not being displayed as expected.");
                }               
                Assert.AreEqual(row["Category"], Driver.GetText(ActiveCategories),
                    "The category is not being displayed as expected.");
                Assert.AreEqual(row["Status"], Driver.GetText(ActiveStatus),
                    "The Status is not being displayed as expected.");
                var expectedDate = Driver.CalculateFutureOrPastDate(row["DueDate"]);
                Assert.AreEqual(expectedDate, Driver.GetText(ActiveDueDate),
                    "The Due Date is not being displayed as expected.");
                Assert.AreEqual(row["AssignedDepartment"], Driver.GetText(ActiveAssignedDept),
                    "The Assigned Department is not being displayed as expected.");
                Assert.AreEqual(row["AssignedUser"], Driver.GetText(ActiveAssignedUser),
                    "The Assigned User is not being displayed as expected.");
                _index++;
            }
        }

        public void VerifyClosedActionsTable(Table table)
        {
            var rows = table.Rows;

            _index = 0;
            foreach (var row in rows)
            {
                Assert.AreEqual(row["ActionId"], Driver.GetText(ClosedActionsId),
                    "The closed action id is not being displayed as expected.");
                Assert.AreEqual(row["Category"], Driver.GetText(ClosedCategories),
                    "The category is not being displayed as expected.");
                Assert.AreEqual(row["Status"], Driver.GetText(ClosedStatus),
                    "The Status is not being displayed as expected.");
                var expectedDate = Driver.CalculateFutureOrPastDate(row["DueDate"]);
                Assert.AreEqual(expectedDate, Driver.GetText(ClosedDueDate),
                    "The Due Date is not being displayed as expected.");
                Assert.AreEqual(row["AssignedDepartment"], Driver.GetText(ClosedAssignedDept),
                    "The Assigned Department is not being displayed as expected.");
                Assert.AreEqual(row["AssignedUser"], Driver.GetText(ClosedAssignedUser),
                    "The Assigned User is not being displayed as expected.");
                _index++;
            }
        }

        public void VerifyPageHeadings(string heading, string subheading1, string subheading2)
        {
            Assert.AreEqual(heading, Driver.GetText(Heading), "The heading is not as expected.");
            Assert.AreEqual(subheading1, Driver.GetText(Subheading1),
                "The sub heading for active actions is not as expected.");
            Assert.AreEqual(subheading2, Driver.GetText(Subheading2),
                "The heading for closed actions is not as expected.");
        }

        public void VerifyActiveTableActions(string actionId, string category, string status, string dueDate,
            string assignedDept, string assignedUser)
        {
            Assert.AreEqual(actionId, Driver.GetText(ActiveActionIdHeading), "The heading is not as expected.");
            Assert.AreEqual(category, Driver.GetText(ActiveCategoryHeading), "The heading is not as expected.");
            Assert.AreEqual(status, Driver.GetText(ActiveStatusHeading), "The heading is not as expected.");
            Assert.AreEqual(dueDate, Driver.GetText(ActiveDueDateHeading), "The heading is not as expected.");
            Assert.AreEqual(assignedDept, Driver.GetText(ActiveAssignedDeptHeading), "The heading is not as expected.");
            Assert.AreEqual(assignedUser, Driver.GetText(ActiveAssignedUserHeading), "The heading is not as expected.");
        }

        public void VerifyClosedTableActions(string caseId, string category, string status, string dueDate,
            string assignedDept, string assignedUser)
        {
            Assert.AreEqual(caseId, Driver.GetText(ClosedCaseIdHeading), "The heading is not as expected.");
            Assert.AreEqual(category, Driver.GetText(ClosedCategoryHeading), "The heading is not as expected.");
            Assert.AreEqual(status, Driver.GetText(ClosedStatusHeading), "The heading is not as expected.");
            Assert.AreEqual(dueDate, Driver.GetText(ClosedDueDateHeading), "The heading is not as expected.");
            Assert.AreEqual(assignedDept, Driver.GetText(ClosedAssignedDeptHeading), "The heading is not as expected.");
            Assert.AreEqual(assignedUser, Driver.GetText(ClosedAssignedUserHeading), "The heading is not as expected.");
        }

        public void ClickEditActionFor(Table table)
        {
            var rows = table.Rows;
            SetActionIndex(rows);
            Driver.ClickItem(ActionsIdLink);
        }

        private void SetActionIndex(TableRows rows)
        {
            _index = 0;
            foreach (var row in rows)
            {
                var expectedDate = Driver.CalculateFutureOrPastDate(row["DueDate"]);

                if (row["Category"].Equals(Driver.GetText(ActiveCategories))
                    && row["Status"].Equals(Driver.GetText(ActiveStatus))
                    && expectedDate.Equals(Driver.GetText(ActiveDueDate))
                    && row["AssignedDepartment"].Equals(Driver.GetText(ActiveAssignedDept))
                    && row["AssignedUser"].Equals(Driver.GetText(ActiveAssignedUser)))
                {
                    return;
                }
                _index++;
            }
        }
    }
}