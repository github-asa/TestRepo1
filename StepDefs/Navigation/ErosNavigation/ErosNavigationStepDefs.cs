using J2BIOverseasOps.Pages.Navigation.ErosNavigation;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.Navigation.ErosNavigation
{
    [Binding]
    public sealed class ErosNavigationStepDefs : BaseStepDefs
    {

        private readonly ErosNavigationPage _erosNavigation;
        private readonly IRunData _runData;

        public ErosNavigationStepDefs(IWebDriver driver, ILog log, IRunData runData) : base(driver, log)
        {
            _runData = runData;
            _erosNavigation = new ErosNavigationPage(driver, log);
        }

        [Given(@"I can see the eros menu icon")]
        public void GivenICanSeeTheErosMenuIcon()
        {
            _erosNavigation.VerifyErosMenuIsVisible();
        }

        [Given(@"I can see the logout link")]
        [Then(@"I should be able to see the logout link")]
        public void GivenICanSeeTheLogoutLink()
        {
            _erosNavigation.VerifyLogoutLinkIsVisible();
        }

        [When(@"I ""(open|close)"" the eros menu")]
        public void WhenITheErosMenu(string openClose)
        {
            _erosNavigation.OpenCloseErosMenu(openClose);
        }

        [Then(@"I am ""(displayed|not displayed)"" the following menu options:")]
        public void ThenIAmTheFollowingMenuOptions(string displayedNotDisplayed, Table menuOptions)
        {
            _erosNavigation.VerifyMenuItemsDisplayedOrNot(displayedNotDisplayed, menuOptions);
        }

        [When(@"I click the ""(.*)"" eros menu item")]
        public void WhenIClickTheErosMenuItem(string menuItem)
        {
            _erosNavigation.ClickErosMenuItem(menuItem);
        }

        [When(@"I click the eros logout link")]
        public void WhenIClickTheErosLogoutLink()
        {
            _erosNavigation.ClickLogoutLink();
        }
    }
}
