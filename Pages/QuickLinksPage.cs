using System.Threading;
using J2BIOverseasOps.Extensions;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;

namespace J2BIOverseasOps.Pages
{
    internal class QuickLinksPage : BasePage
    {
        public QuickLinksPage(IWebDriver driver, ILog log) : base(driver, log)
        {
        }

        internal void ClickLink(string linkText)
        {
            Driver.ClickItem(By.XPath("//a[contains(text(),'" + linkText + "')]"), true);
        }

        public void VerifyNavigatedToQuickLinksPage(string url)
        {
            Driver.VerifyNavigatedToPage(url);
        }


        //TODO - This needs to be changed
        public void ClickSaveBtn(string id = "3")
        {
            var x = Driver.FindElement(By.XPath("//input[@id='input']"));
            x.Clear();
            Driver.EnterText(By.XPath("//input[@id='input']"), id);
            Driver.ClickItem(By.CssSelector("p-button[label*=Save] > button > .ui-button-text.ui-clickable"));
            // because the element does not have a spinner or any other ui indicator that clicking the button completes the request, this thread has been used as a temporary solution
            Thread.Sleep(3000);
            Driver.WaitForPageToLoad();
        }

        public void ChangeUserId(string userId)
        {
            ClickSaveBtn(userId);
        }


        internal void ClickSearchForJet2HolidaysCustomerBtn()
        {
            Assert.True(Driver.WaitForItem(By.CssSelector("#reported-by-search-jet2-customer-button > button > span")),
                $"The Search for Jet2Holiodays customer button is not displayed");
            Driver.ClickItem(By.CssSelector("#reported-by-search-jet2-customer-button > button > span"));
        }

        internal void ClickJetHolCustRad()
        {
            Assert.True(Driver.WaitForItem(By.CssSelector("#reported-by-radio-jet2-customer")),
                $"The Jet 2 Holidays customer radio button is not displayed");
            Driver.ClickItem(By.CssSelector("#reported-by-radio-jet2-customer"));
        }
    }
}