using J2BIOverseasOps.Pages.RoleManagement;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.RoleManagement
{
    [Binding]
    internal class EmployeeAssignmentReportStepDefs : BaseStepDefs
    {
        private readonly EmployeeAssignmentReportPage _empAssignmentReport;

        public EmployeeAssignmentReportStepDefs(IWebDriver driver, ILog log) : base(driver, log)
        {
            _empAssignmentReport = new EmployeeAssignmentReportPage(driver, log);
        }

        [Then(@"I verify the employee name ""(.*)"" is displayed on the table of mapped employees")]
        public void ThenIVerifyTheEmployeeNameIsDisplayedOnTheTableOfMappedEmployees(string expectedName)
        {
            _empAssignmentReport.VerifyNameAppearsOnTable(expectedName);
        }

        [When(@"I select the role from drop down on the assigned empoyees page")]
        public void WhenISelectTheRoleFromDropDownOnTheAssignedEmpoyeesPage()
        {
            _empAssignmentReport.SelectRoleFromDrpDwn();
        }

        [Then(@"I verify the username column displays the username on the assigned empoyees page")]
        public void ThenIVerifyTheUsernameColumnDisplaysTheUsernameOnTheAssignedEmpoyeesPage()
        {
            _empAssignmentReport.VerifyUsernameDisplayed();
        }

        [When(@"I enter ""(.*)"" in the filter employees by name on the assigned empoyees page")]
        public void WhenIEnterInTheFilterEmployeesByNameOnTheAssignedEmpoyeesPage(string employeeName)
        {
            _empAssignmentReport.EnterEmployeeName(employeeName);
        }

        [When(@"I click the Reset filter button on the assigned employees page")]
        public void WhenIClickTheResetFilterButtonOnTheAssignedEmployeesPage()
        {
            _empAssignmentReport.ClickResetFilterButton();
        }

        [Then(@"I verify the filter by name is reset on the assigned employees page")]
        public void ThenIVerifyTheFilterByNameIsResetOnTheAssignedEmployeesPage()
        {
            _empAssignmentReport.VerifyFilterByNameReset();
        }

        [Then(@"I verify the select role filter displays the first role on the drop down")]
        public void ThenIVerifyTheSelectRoleFilterDisplaysTheFirstRoleOnTheDropDown()
        {
            _empAssignmentReport.VerifyFirstRoleDisplayedOnDropDown();
        }

        [When(@"I click the employee name ""(.*)"" on the assigned employees table")]
        public void WhenIClickTheEmployeeNameOnTheAssignedEmployeesTable(string employeeName)
        {
            _empAssignmentReport.ClickEmployeeName(employeeName);
        }

        [When(@"I click the Assign Employee to Role button on the assigned employees page")]
        public void WhenIClickTheAssignEmployeeToRoleButtonOnTheAssignedEmployeesPage()
        {
            _empAssignmentReport.ClickAssignEmpToRoleButton();
        }
    }
}