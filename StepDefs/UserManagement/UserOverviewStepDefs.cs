using J2BIOverseasOps.Extensions;
using J2BIOverseasOps.Pages.UserManagement;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.UserManagement
{
    [Binding]
    public sealed class UserOverviewStepDefs : BaseStepDefs
    {
        private readonly UserOverviewPage _overViewPage;

        public UserOverviewStepDefs(IWebDriver driver, ILog log, IRunData rundata) : base(driver, log)
        {
            _overViewPage = new UserOverviewPage(driver, log, rundata);
        }

        [When(@"I click on the Edit ""(Department|Destination|Role)"" link on user overview page")]
        public void WhenIClickOnTheEditLinkOnUserOverviewPage(string link)
        {
            _overViewPage.ClickEditViewLink(link);
        }

        [Then(@"I verify the following details are correctly displayed for the user on the user overview page:")]
        public void ThenIVerifyTheFollowingDetailsAreCorrectlyDisplayedForTheUserOnTheIserOverviewPage(Table table)
        {
            _overViewPage.VerifyDetailsOnOverviewPage(table);
        }

        [Then(@"I verify the Role of the user as from the scenario context as ""(.*)""")]
        public void ThenIVerifyTheRoleOfTheUserAsFromTheScenarioContextAs(string key)
        {
            _overViewPage.VerifyExpectedRole(key);
        }

    }
}
