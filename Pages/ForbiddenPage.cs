using J2BIOverseasOps.Extensions;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;

namespace J2BIOverseasOps.Pages
{
    internal class ForbiddenPage : BasePage
    {
        private readonly By _forbiddenPage = By.CssSelector("app-forbidden");
        private readonly By _forbiddenPageBreadCrumb = By.XPath("//li[@role='menuitem']//a");
        private readonly By _h1Heading = By.XPath("//h1");

        public ForbiddenPage(IWebDriver driver, ILog log) : base(driver, log)
        {
        }

        public void VerifyForbiddenPageDisplayed()
        {
            Driver.WaitForPageToLoad();
            Assert.True(Driver.WaitForItem(_forbiddenPage), $"Could not verify the forbidden page displayed");
            var breadCrumbLinkText = Driver.FindElement(_forbiddenPageBreadCrumb).Text;
            Assert.True(breadCrumbLinkText.Contains("Forbidden"),
                $"Unable to verify 'forbidden' page contains the forbidden as breadcrumb. Actual link :{breadCrumbLinkText}");
            var h1HeadingText = Driver.FindElement(_h1Heading).Text;
            Assert.True(h1HeadingText.Contains("403"),
                $"Unable to verify 'forbidden' page heading contains the 403. Actual text :{h1HeadingText}");
        }
    }
}