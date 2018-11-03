using J2BIOverseasOps.Pages;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs
{
    [Binding]
    public sealed class QuickLinksStepDefs : BaseStepDefs
    {
        private readonly QuickLinksPage _quickLinksPage;
        private readonly IRunData _runData;

        public QuickLinksStepDefs(IWebDriver driver, ILog log, IRunData runData) : base(driver, log)
        {
            _runData = runData;
            _quickLinksPage = new QuickLinksPage(driver, log);
        }

        [When(@"I click the ""(.*)"" link")]
        public void WhenIClickTheLink(string linkText)
        {
            _quickLinksPage.ClickLink(linkText);
        }

        [Given(@"I am navigated to quick links page")]
        [Then(@"I am navigated to quick links page")]
        public void GivenIAmNavigatedToQuickLinksPage()
        {
            _quickLinksPage.VerifyNavigatedToQuickLinksPage(_runData.BaseUrl);
        }

        [Given(@"I change the user Id to ""(.*)""")]
        [Then(@"I change the user Id to ""(.*)""")]
        public void GivenIChangeTheUserIdTo(string userId)
        {
            _quickLinksPage.ChangeUserId(userId);
        }

        [When(@"I click the Search for Jet2Holidays customer button on the reported by page")]
        public void WhenIClickTheSearchForJetHolidaysCustomerButtonOnTheReportedByPage()
        {
            _quickLinksPage.ClickSearchForJet2HolidaysCustomerBtn();
        }

        [Then(@"I select the Jet2holidays customer radio button")]
        public void ThenISelectTheJetHolidaysCustomerRadioButton()
        {
            _quickLinksPage.ClickJetHolCustRad();
        }

    }
}