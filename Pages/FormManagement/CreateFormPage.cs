using J2BIOverseasOps.Extensions;
using J2BIOverseasOps.Helpers;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Pages.FormManagement
{
    internal class CreateFormPage : CommonFormMgmtElements
    {
        private readonly By _addEditformMgmtTableHeader = By.XPath("//p-table//th");
        private readonly By _pageTitle = By.Id("eros-page-heading");
        private readonly By _buttonSaveForm = By.CssSelector("#btnSavePropertyManagement button");
        private readonly By _checkBoxPerCustomer = By.Id("chkPerCustomerForm");

        public CreateFormPage(IWebDriver driver, ILog log) : base(driver, log)
        {
        }

        public void VerifyPageTitle(string expectedTitle)
        {
            var actualTitle = Driver.FindElement(_pageTitle).Text;
            Assert.AreEqual(expectedTitle, actualTitle,
                $"Actual title {actualTitle} was not equal to Expected title {expectedTitle}");
        }

        public void VerifyFormColumnsHeader(Table table)
        {
            var loopCounter = 0;
            foreach (var row in table.Rows)
            {
                var expectedColumnName = row["add_view_form_title"];
                var listOfColumnHeaders = Driver.FindElements(_addEditformMgmtTableHeader);
                var actualColumnName = listOfColumnHeaders[loopCounter].Text;
                Assert.True(actualColumnName.Contains(expectedColumnName),
                    $"Could not verify {expectedColumnName} is present within the table.Actual found {actualColumnName}");
                loopCounter++;
            }
        }

        public void EnterUniqueFormName()
        {
            var formName = $"AUTO{StringExtensions.GenerateRandomUniqueString()}";
            Log.Debug($"Form created :" + StringExtensions.GenerateRandomUniqueString());
            ScenarioContext.Current[FormNameKey] = formName; //UPDATE SCENARIO CONTEXT
            EnterTextFormNameInputBox(formName);
        }

        public void VerifySaveButtonIsDisplayed(string buttonText)
        {
            Assert.IsTrue(Driver.WaitForItem(_buttonSaveForm), "The save button is not being displayed.");
            Assert.AreEqual(buttonText, Driver.GetText(_buttonSaveForm), "The save button text is not as expected.");
        }

        public void ClickSaveButton()
        {
            Driver.ClickItem(_buttonSaveForm);
        }

        public void VerifySaveButtonIsDisabled()
        {
            Assert.IsFalse(Driver.IsElementEnabled(_buttonSaveForm), "The save button should not be enabled.");
        }

        public void EnterRandomFormName(int length, int max)
        {
            var formName = PageHelper.RandomString(length);
            ScenarioContext.Current[FormNameKey] = length > max ? formName.Substring(0, max): formName;
            EnterTextFormNameInputBox(formName);
        }

        public void TickPerCustomerCheckBox()
        {
            Driver.TickCheckBox(_checkBoxPerCustomer);
        }

        public void UntickPerCustomerCheckBox()
        {
            Driver.UntickCheckBox(_checkBoxPerCustomer);
        }

        public void VerifyPerCustomerIsTicked()
        {
            Assert.IsTrue(Driver.IsCheckBoxTicked(_checkBoxPerCustomer), "The per customer flag is not checked.");
        }

        public void VerifyPerCustomerIsUnticked()
        {
            Assert.IsFalse(Driver.IsCheckBoxTicked(_checkBoxPerCustomer), "The per customer flag is checked.");
        }
    }
}