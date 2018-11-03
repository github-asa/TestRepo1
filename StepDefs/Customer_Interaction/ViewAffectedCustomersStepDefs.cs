using J2BIOverseasOps.Pages.Customer_Interaction;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.Customer_Interaction
{
    [Binding]
    public sealed class ViewAffectedCustomersStepDefs : BaseStepDefs
    {
        private readonly HowAffectedPage _howAffectedPage;

        public ViewAffectedCustomersStepDefs(IWebDriver driver, ILog log) : base(driver, log)
        {
            _howAffectedPage = new HowAffectedPage(driver, log);
        }

        [Then(@"the '(.*)' category has '(.*)' beside it")]
        public void ThenTheCategoryHasBesideIt(string category, string message)
        {
            _howAffectedPage.VerifyMessageBesideCategory(category, message);
        }

        [When(@"I click the view affected customers link for the '(.*)' category")]
        public void WhenIClickTheViewAffectedCustomersLinkForTheCategory(string category)
        {
            _howAffectedPage.ClickViewAffectedCustomersLinkFor(category);
        }

        [Then(@"the customers affected list popup is displayed")]
        public void ThenTheCustomersAffectedListPopupIsDisplayed()
        {
            _howAffectedPage.VerifyAffectedListPopupIsDisplayed();
        }

        [Then(@"the customers affected list popup has a title of '(.*)'")]
        public void ThenTheCustomersAffectedListPopupHasATitleOf(string title)
        {
            _howAffectedPage.VerifyAffectedListPopupTitle(title);
        }

        [Then(@"the correct number of customers are listed on the view affected popup")]
        public void ThenTheCategoryWillHaveTheCorrectCustomersSelectedOnTheViewAffectedCustomers(Table table)
        {
            _howAffectedPage.VerifyAffectedListOfCustomersPopup(table);
        }

        [When(@"I close the customers affected list popup")]
        public void WhenICloseTheCustomersAffectedListPopup()
        {
            _howAffectedPage.CloseViewAffectedPopup();
        }
    }
}