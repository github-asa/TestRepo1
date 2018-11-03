using J2BIOverseasOps.Extensions;
using J2BIOverseasOps.Pages;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs
{
    [Binding]
    public sealed class CommonStepDefs : BaseStepDefs
    {
        private readonly BasePage _base;
        private readonly CommonPageElements _commonPageElements;
        private readonly ForbiddenPage _forbiddenPage;
        private readonly IRunData _runData;

        public CommonStepDefs(IWebDriver driver, ILog log, IRunData runData) : base(driver, log)
        {
            _runData = runData;
            _base = new BasePage(driver, log);
            _forbiddenPage = new ForbiddenPage(driver, log);
            _commonPageElements = new CommonPageElements(driver, log);
        }

        [Given(@"I should be navigated to '(.*)' page")]
        [When(@"I should be navigated to '(.*)' page")]
        [Then(@"I should be navigated to '(.*)' page")]
        public void ThenIShouldBeNavigatedToPage(string pageUrl)
        {
            _base.WaitForSpinnerToDisappear();
            Driver.VerifyNavigatedToPage(pageUrl);
            _base.WaitForSpinnerToDisappear();
        }

        [Then(@"I should not be navigated to '(.*)' page")]
        public void ThenIShouldNotBeNavigatedToPage(string pageUrl)
        {
            _base.WaitForSpinnerToDisappear();
            Driver.VerifyNotNavigatedToPage(pageUrl);
            _base.WaitForSpinnerToDisappear();
        }


        [When(@"I navigate to the home page")]
        [Given(@"I navigate to the home page")]
        [Then(@"I navigate to the home page")]
        public void GivenINavigateToHomePage()
        {
            _base.WaitForSpinnerToDisappear();
            Driver.GoToUrl(_runData.BaseUrl);
            _base.WaitForSpinnerToDisappear();
        }

        [Given(@"I navigate to the ""(.*)"" page")]
        [When(@"I navigate to the ""(.*)"" page")]
        [Then(@"I navigate to the ""(.*)"" page")]
        public void GivenINavigateToPage(string url)
        {
            _base.WaitForSpinnerToDisappear();
            Driver.GoToUrl(_runData.BaseUrl + "/" + url);
            _base.WaitForSpinnerToDisappear();
        }

        [Given(@"I refresh the current page")]
        [When(@"I refresh the current page")]
        [Then(@"I refresh the current page")]
        public void GivenIRefreshTheCurrentPage()
        {
            _base.WaitForSpinnerToDisappear();
            Driver.Navigate().Refresh();
            Driver.WaitForPageToLoad();
        }

        [Then(@"the '(.*)' page is displayed")]
        public void ThenThePageIsDisplayed(string url)
        {
            _base.WaitForSpinnerToDisappear();
            Driver.VerifyNavigatedToPage(url);
            _base.WaitForSpinnerToDisappear();
        }

        [Then(@"I verify I am presented with forbidden page")]
        public void ThenIVerifyIAmPresentedWithForbiddenPage()
        {
            _forbiddenPage.VerifyForbiddenPageDisplayed();
        }

        [Then(@"a '(.*)' confirmation popup is displayed stating '(.*)'")]
        public void ThenAConfirmationPopupIsDisplayedStating(string title, string message)
        {
            _commonPageElements.ConfirmationPopupDisplayedWithMessage(title, message);
        }

        [When(@"I click '(.*)' on the confirmation popup")]
        public void WhenIClickOnTheNotesConfirmationDialog(string buttonText)
        {
            _commonPageElements.ClickOnConfirmationPopup(buttonText);
        }

        [Then(@"the confirmation popup is dismissed")]
        public void ThenTheNotesConfirmationDialogIsDismissed()
        {
            _commonPageElements.VerifyConfirmationPopUpIsDismissed();
        }

        [Then(@"I close the Growl Notification")]
        [When(@"I close the Growl Notification")]
        public void ThenICloseTheGrowlNotification()
        {
            _commonPageElements.CloseGrowlNotification();
        }

        [Then(@"I wait for the Growl notifications to disappear")]
        public void ThenIWaitForTheGrowlNotificationsToDisappear()
        {
            _commonPageElements.WaitForGrowlNotificationToDisappear();
        }

        [Then(@"I verify that no popup is displayed")]
        public void ThenIVerifyThatNoPopupIsDisplayed()
        {
            _commonPageElements.VerifyConfirmationPopupNotDisplayed();
        }

        [Then(@"I verify the following status on the navigation bar for the following pages:")]
        public void ThenIVerifyTheFollowingStatusOnTheNavigationBarForTheFollowingPages(Table table)
        {
            _commonPageElements.VerifyNavigationIconStatus(table);
        }

        [Given(@"I verify I am navigated to following pages when I click the following navigation icons:")]
        [When(@"I verify I am navigated to following pages when I click the following navigation icons:")]
        [Then(@"I verify I am navigated to following pages when I click the following navigation icons:")]
        public void ThenIVerifyIAmNavigatedToFollowingPagesWhenIClickTheFollowingNavigationIcons(Table table)
        {
            _commonPageElements.ClickAndNavigatetoPages(table);
        }

        [When(@"I click the Back button")]
        public void WhenIClickTheBackButton()
        {
            _commonPageElements.ClickBackButton();
        }

        [Then(@"I am displayed with forbidden error message ""(.*)""")]
        public void ThenIAmDisplayedWithForbiddenErrorMessage(string errorMessage)
        {
            _commonPageElements.VerifyForbiddenPageErrorMessage(errorMessage);
        }

        [Given(@"I create a role called ""(.*)"" if it doesnt exist already using the API")]
        public void GivenICreateARoleCalledIfItDoesntExistAlreadyUsingTheAPI(string rolename)
        {
            _commonPageElements.CreateRoleAPI(rolename);
        }

        [Given(@"I map the following permissions to the role ""(.*)"" using the API")]
        public void GivenIAssignTheFollowingPermissionsToTheRoleUsingTheAPI(string role, Table table)
        {
            _commonPageElements.MapPermissionsToRole(role,table);
        }

        [Given(@"I map the role ""(.*)"" to the username ""(.*)"" using the API")]
        public void GivenIAssignTheRoleToTheUsernameUsingTheAPI(string role, string username)
        {
            _commonPageElements.MapARoleToUser(role, username);
        }

        [Given(@"I map the following destinations to the username ""(.*)"" using the API")]
        public void GivenIAssignTheFollowingDestinationsToTheUsernameUsingTheAPI(string username, Table table)
        {
            _commonPageElements.MapDestinationsToUsers(username, table);
        }

        [Given(@"I unmap all the destinations from the username ""(.*)"" using the API")]
        public void GivenIRemoveAllTheDestinationsFromTheUsernameUsingTheAPI(string username)
        {
            _commonPageElements.UnmapAllDestinationsFromUser(username);
        }

        [Given(@"I map the following properties to the username ""(.*)"" using the API")]
        public void GivenIAssignTheFollowingPropertiesToTheUsernameUsingTheAPI(string username, Table table)
        {
            _commonPageElements.MapPropertiesToAUser(username, table);
        }

        [Given(@"I unmap the following properties to the username ""(.*)"" using the API")]
        public void GivenIUnmapTheFollowingPropertiesToTheUsernameUsingTheAPI(string username, Table table)
        {
            _commonPageElements.UnMapPropertiesToAUser(username, table);
        }

        [When(@"I set the current user as ""(.*)""")]
        public void WhenISetTheCurrentUserAs(string username)
        {
            _commonPageElements.SetCurrentUser(username);
        }


    }
}