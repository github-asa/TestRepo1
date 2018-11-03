using J2BIOverseasOps.Extensions;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Pages.FormManagement
{
    internal class CommonFormMgmtElements : CommonPageElements
    {
        private readonly By _addNewQBtn = By.XPath("//*[@id='btnAddNewQuestion']//button");
        private readonly By _backButton = By.XPath("//*[@id='btnBack']//button");
        private readonly By _continueFormBtn = By.XPath("//*[@id='btnContinue']//button");
        private readonly By _showInactiveQChkBox = By.XPath("//p-checkbox[@name='chkShowActive']");
        protected readonly By EditQButton = By.XPath(".//*[@label='Edit']//button");
        protected readonly By FormNameInputBox = By.Id("txtNewForm");

        // keys for the scenario context containing the form values
        protected readonly string FormNameKey = "formname";

        public CommonFormMgmtElements(IWebDriver driver, ILog log) : base(driver, log)
        {
        }

        public void VerifyFormNameTextBoxState(string visibility, string enabledStatus)
        {
            VerifyElementVisibility(visibility, FormNameInputBox);
            VerifyElementState(enabledStatus, FormNameInputBox);
        }

        public void VerifyShowInactiveQuestionsTickBox(string visibility, string enabledStatus)
        {
            VerifyElementVisibility(visibility, _showInactiveQChkBox);
            VerifyCheckBoxState(enabledStatus, _showInactiveQChkBox);
        }

        public void VerifyAddNewQBtnState(string visibility, string enabledStatus)
        {
            VerifyElementVisibility(visibility, _addNewQBtn);
            VerifyElementState(enabledStatus, _addNewQBtn);
        }

        public void VerifyBackBtnState(string visibility, string enabledStatus)
        {
            VerifyElementVisibility(visibility, _backButton);
            VerifyElementState(enabledStatus, _backButton);
        }

        public void VerifyContinueFormBtnState(string visibility, string enabledStatus)
        {
            VerifyElementVisibility(visibility, _continueFormBtn);
            VerifyElementState(enabledStatus, _continueFormBtn);
        }

        public new void ClickBackButton()
        {
            Driver.ClickItem(_backButton);
        }

        public void ClickAddNewQBtn()
        {
            Driver.ClickItem(_addNewQBtn);
        }

        public void EnterTextFormNameInputBox(string text)
        {
            Driver.EnterText(FormNameInputBox, text);
        }

        public void ClearFormNameField()
        {
            Driver.Clear(FormNameInputBox);
        }

        public void ClickContinueFormButton()
        {
            Driver.ClickItem(_continueFormBtn);
        }

        public IWebElement GetQuestionRow(string questionText)
        {
            var questionsRows = Driver.FindElements(By.CssSelector("tr[id*=form-builder-grid]:not([id*=form-builder-grid-isActive])"));
            IWebElement result = null;
            foreach (var questionsRow in questionsRows)
            {
                Driver.WaitForItemWithinWebElement(By.CssSelector("tr[id*=form-builder-grid]:not([id*=form-builder-grid-isActive])"),
                    By.CssSelector("[id*=form-builder-questionCol]"));
                var row = questionsRow.FindElement(By.CssSelector("[id*=form-builder-questionCol]"));
                var actualText = Driver.GetText(row);
                if (actualText.ToLower().Equals(questionText.ToLower()))
                {
                    result = questionsRow;
                    break;
                }
            }

            //var questionRowElem = $"//*[contains(@class, '{questionText}')]";
            return result;
        }

        public void ClickEditQButton(string qtext)
        {
            var questionRow = GetQuestionRow(qtext);
            var editButton = questionRow.FindElement(EditQButton);
            Driver.ClickItem(editButton);
        }

        public void ShowInactiveQuestions(string status)
        {
            Driver.TickUntickCheckBox(_showInactiveQChkBox, status);
        }

        public void SaveFormNameInScenarioContext(string key)
        {
            ScenarioContext.Current[key] = FormNameKey;
        }
    }
}