using J2BIOverseasOps.Extensions;
using J2BIOverseasOps.Pages;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs
{
    [Binding]
    public sealed class LoginStepDefs : BaseStepDefs
    {
        private readonly LoginPage _login;
        private readonly IRunData _runData;

        public LoginStepDefs(IWebDriver driver, ILog log, IRunData runData) : base(driver, log)
        {
            _login = new LoginPage(driver, log, runData);
            _runData = runData;
        }

        [Given(@"I navigate to the login page")]
        [When(@"I navigate to the login page")]
        public void GivenINavigateToPage()
        {
            _login.WaitForSpinnerToDisappear();
            for (var i = 0; i < 3; i++)
            {
                Driver.GoToUrl(_runData.BaseUrl + "/change-user");

                if (Driver.WaitForUrl("login", 5))
                {
                    break;
                }
            }
        }

        [When(@"I enter the username as ""(.*)""")]
        [Given(@"I enter the username as ""(.*)""")]
        public void GivenIEnterTheUsernameAs(string username)
        {
            _login.EnterLoginUsername(username);
        }

        [When(@"I enter the password as ""(.*)""")]
        [Given(@"I enter the password as ""(.*)""")]
        public void GivenIEnterThePasswordAs(string password)
        {
            _login.EnterLoginPassword(password);
        }

        [Then(@"I am redirected to the login screen")]
        public void ThenIAmRederictedToTheLoginScreen()
        {
            _login.VerifyLoginScreenDisplayed();
        }

        [When(@"I click the login button")]
        public void WhenIClickTheLoginButton()
        {
            _login.ClickLoginButton();
        }

        [Given(@"I log in as ""(admin|restricted|team1|team3)"" user")]
        [When(@"I log in as ""(admin|restricted|team1|team3)"" user")]
        public void GivenILogInAsUser(string userName)
        {
            _login.LoginUser(userName);
        }

        [When(@"I log in as ""(admin|restricted|)"" ADFS user")]
        [Given(@"I log in as ""(admin|restricted|)"" ADFS user")]
        public void GivenILogInADFSUser(string userName)
        {
            _login.LoginAdfsUser(userName);
        }


        [Then(@"I am displayed with Error ""(.*)""")]
        public void ThenIAmDisplayedWithError(string errorText)
        {
            _login.VerifyError(errorText);
        }

        [When(@"I logout of the system")]
        public void WhenILogoutOfTheSystem()
        {
            _login.LogOut();
        }

        [When(@"I logout of the system for the adfs user")]
        public void WhenILogoutOfTheSystemForTheAdfsUser()
        {
            _login.LogOut(true);
        }

        [When(@"I click on the Jet2Holidays icon")]
        public void WhenIClickOnTheJetHolidaysIcon()
        {
            _login.ClickJet2LoginIcon();
        }

    }
}