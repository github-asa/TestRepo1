using J2BIOverseasOps.Extensions;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;

namespace J2BIOverseasOps.Pages.FormManagement
{
    internal class LinkFormToCategoryPage : CommonFormMgmtElements
    {
        //TODO To be replaced when the mock UI is replaced
        private readonly By _headerCategory = By.Id("select-case-categories-heading");
        private readonly By _changesSummary=By.XPath("//textarea[@pinputtextarea]");
        private readonly By _parentCategory = By.Id("parent-categories");
        private readonly By _childCategory = By.Id("child-categories");
        private readonly By _saveFormButton = By.Id("btnSuccess");
        private readonly By _continueButton = By.Id("select-case-categories-continue-button");

        public LinkFormToCategoryPage(IWebDriver driver, ILog log) : base(driver, log)
        {
        }


        //TODO temp
        public void VerifyNavigatedToLinkFormToCatgPage()
        {
            Driver.WaitForItem(_headerCategory);
        }

        //TODO temp
        public void SelectCategory()
        {
            
            var selectedItem = Driver.GetAllTickedItemsInOrderedList(_parentCategory);
            if (!Driver.IsItemParentInOrderedList(_parentCategory, "Accommodation   (1 selected)"))
            {
                Driver.SelectItemInOrderedList(_parentCategory, "Accommodation");
                Driver.SelectItemInOrderedList(_childCategory, "Go Show");
            }

            Driver.ClickItem(_continueButton);
        }

        public void ClickSaveFormButton()
        {
            Driver.ClickItem(_saveFormButton);
        }

        public void EnterChangesSummary(string formChangeSummaryText= "Making Changes to the form")
        {
            Driver.EnterText(_changesSummary, formChangeSummaryText);
        }

        public void VerifyFormChangesSummary(string expectedText)
        {
            var actualText = Driver.GetInputBoxValue(_changesSummary).Trim();
            Assert.AreEqual(actualText,expectedText,$"Expected Text {expectedText} was not equal to Actual Text {actualText}");
        }

        public void VerifySaveFormBtnState(string enabledDisabled)
        {
            VerifyElementState(enabledDisabled, _saveFormButton);
        }

  
    }
}