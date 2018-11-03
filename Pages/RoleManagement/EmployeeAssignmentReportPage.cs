using J2BIOverseasOps.Extensions;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Pages.RoleManagement
{
    internal class EmployeeAssignmentReportPage : RoleMgmtCommon
    {
        private readonly By _assignEmpToRoleBtn = By.Id("gotoAssignEmployeeToRoleButton");
        private readonly By _filterByName = By.XPath("//input[@id='employee-name-filter']");
        private readonly By _resetButton = By.XPath("//button//*[contains(text(),'Reset')]");
        private readonly By _roleName = By.XPath(".//span[@rolename]");
        private readonly By _selectedRoleOnDrpDwn = By.XPath(".//*[@selected]");
        private readonly By _selectRoleDrpDwn = By.XPath("//p-dropdown[@inputid='select-role']");
        private readonly By _selectRoleDrpDwnList = By.XPath("//p-dropdown[@inputid='select-role']//li");
        private readonly By _tableCellFullName = By.XPath(".//a[@userfullname]");
        private readonly By _tableCellUserName = By.XPath(".//tbody//td[3]");
        private readonly By _userTable = By.XPath("//*[@id='user-table']//table");

        public EmployeeAssignmentReportPage(IWebDriver driver, ILog log) : base(driver, log)
        {
        }

        public void SelectRoleFromDrpDwn()
        {
            var rolename = ScenarioContext.Current["rolename"].ToString();
            Driver.SelectDropDownOption(_selectRoleDrpDwn, rolename);
        }

        public void EnterEmployeeName(string employeeName)
        {
            Driver.EnterText(_filterByName, employeeName);
        }

        public void VerifyNameAppearsOnTable(string names)
        {
            var expectedNamesList = names.ConvertStringIntoList();
            Driver.WaitForItem(_userTable);
            var listOfNames = Driver.FindElements(_tableCellFullName);

            foreach (var name in listOfNames)
            {
                Assert.True(expectedNamesList.Contains(name.Text.Trim()),
                    name.Text.Trim() + " was not found in the list of expected names on assigned employees table");
            }
        }

        public void VerifyUsernameDisplayed()
        {
            Driver.WaitForItem(_userTable);
            var listOfUserNames = Driver.FindElement(_userTable).FindElements(_tableCellUserName);
            foreach (var userName in listOfUserNames)
            {
                Assert.True(userName.Text != "", "Username is empty within the assigned employees page");
            }
        }

        public void ClickResetFilterButton()
        {
            for (var i = 0; i < 5; i++)
            {
                Driver.ClickItem(_resetButton);
                if (Driver.WaitUntilTextFieldClear(_filterByName, 2))
                {
                    return;
                }
            }
        }

        public void VerifyFilterByNameReset()
        {
            var textFieldFlear = Driver.WaitUntilTextFieldClear(_filterByName);
            Assert.True(textFieldFlear, "Unable to verify filter by name input text as blank");
        }

        public void VerifyFirstRoleDisplayedOnDropDown()
        {
            Driver.ClickItem(_selectRoleDrpDwn);
            Driver.WaitForItem(_selectRoleDrpDwnList);
            var listOfRoles = Driver.FindElements(_selectRoleDrpDwnList);
            var firstRoleOnList = listOfRoles[0].FindElement(_roleName).GetAttribute("rolename");
            Driver.ClickItem(_selectRoleDrpDwn); // close dropdown
            var selectedRoleName = Driver.FindElement(_selectRoleDrpDwn).FindElement(_selectedRoleOnDrpDwn)
                .GetAttribute("rolename");
            Assert.True(selectedRoleName == firstRoleOnList);
        }

        public void ClickEmployeeName(string employeeName)
        {
            Driver.ClickItem(By.LinkText(employeeName));
        }

        public void ClickAssignEmpToRoleButton()
        {
            Driver.ClickItem(_assignEmpToRoleBtn);
        }
    }
}