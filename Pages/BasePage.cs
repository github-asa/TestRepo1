using J2BIOverseasOps.Helpers;
using log4net;
using OpenQA.Selenium;

namespace J2BIOverseasOps.Pages
{
    public class BasePage
    {
        protected readonly IWebDriver Driver;
        protected ILog Log;
        protected string CurrentUsername = "user_name";
        protected string CurrentPassword = "password";
        protected string CurrentUserRole = "role";
        protected string CurrentUserFullname = "fullname";
        protected string CurrentUserDisplayName = "displayname";

        public BasePage(IWebDriver driver, ILog log)
        {
            Driver = driver;
            Log = log;
        }

        public void WaitForSpinnerToDisappear()
        {
            PageHelper.WaitForSpinnerToDisappear(Driver);
        }
    }
}