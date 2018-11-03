using J2BIOverseasOps.Extensions;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Pages.Navigation.ErosNavigation
{
    internal class ErosNavigationPage : CommonPageElements
    {
        private string _linkText = "";
        private string _headerLinkText = "";

        private  By _erosNavigationLink=>By.XPath($"//span[@class='ui-menuitem-text' and contains(text(),'{_linkText}')]");
        private  By _erosNavigationMenu => By.XPath($"//span[@class='ng-star-inserted' and contains(text(),'{_headerLinkText}')]");

        private readonly By _erosLogoutLink = By.Id("navbar-logout");

        public ErosNavigationPage(IWebDriver driver, ILog log) : base(driver, log)
        {
        }

        public void VerifyErosMenuIsVisible()
        {
            _headerLinkText = "Menu";
            Assert.IsTrue(Driver.WaitForItem(_erosNavigationMenu, 1), "The Eros menu is not visible");
        }

        public void VerifyLogoutLinkIsVisible()
        {
            Assert.IsTrue(Driver.WaitForItem(_erosLogoutLink, 1), "The Logout link is not visible");
        }

        public void OpenCloseErosMenu(string openClose)
        {
            //close the growl message if visible as it will overlay the eros menu
            CloseGrowlNotification();
            CloseGrowlNotification();
            CloseGrowlNotification();
            switch (openClose.ToLower())
            {
                case "open":
                    _headerLinkText = "Menu";
                    Driver.ClickItem(_erosNavigationMenu, true);
                    WaitForErosMenuToClose();
                    break;
                case "close":
                    _headerLinkText = "Close";
                    Driver.ClickItem(_erosNavigationMenu, true);
                    WaitForErosMenuToClose();
                    break;
                default:
                    Assert.Fail($"{openClose} is not a valid menu option");
                    break;
            }

        }

        public void WaitForErosMenuToClose()
        {
            _linkText = "Home";
            Assert.IsTrue(Driver.WaitUntilElementNotDisplayed(_erosNavigationLink, 1), "Eros menu did not close in time.");
        }

        public void ClickErosMenuItem(string link)
        {
            _linkText = link;
            Driver.ClickItem(_erosNavigationLink);
            WaitForSpinnerToDisappear();
        }

        public void ClickLogoutLink()
        {
            Driver.ClickItem(_erosLogoutLink, true);
        }

        public void VerifyMenuItemsDisplayedOrNot(string displayedOrNot, Table table)
        {
            var expectedState = displayedOrNot == "displayed";
            foreach (var row in table.Rows)
            {
                var menuOption = row["menu_option"];
                var optionToVerify = By.XPath($"//span[@class='ui-menuitem-text' and contains(text(),'{menuOption}')]");

                Assert.AreEqual(expectedState, Driver.WaitForItem(optionToVerify, 1),
                    $"Visibility of Menu Item '{menuOption}' should be {expectedState} but is {Driver.WaitForItem(optionToVerify, 1)}");
            }
        }
    }
}
