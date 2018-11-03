using J2BIOverseasOps.Pages.Customer_Interaction;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.Customer_Interaction
{
    [Binding]
    public sealed class BookingDetailsExcursionsStepDefs : BaseStepDefs
    {
        private readonly BookingDetailsPage _bookingDetails;

        public BookingDetailsExcursionsStepDefs(IWebDriver driver, ILog log) : base(driver, log)
        {
            _bookingDetails = new BookingDetailsPage(driver, log);
        }

        [Then(@"I verify I can see the excursions section on the booking details page")]
        public void ThenIVerifyICanSeeTheExcursionsSectionOnTheBookingDetailsPage()
        {
            _bookingDetails.VerifyExcursionsSectionDisplayed();
        }

        [Then(@"I verify the excursions header is displayed as '(.*)' on the booking details page")]
        public void ThenIVerifyTheExcursionsHeaderIsDisplayedAsOnTheBookingDetailsPage(string headerName)
        {
            _bookingDetails.VerifyExcursionsHeader(headerName);
        }

        [Then(@"I verify the excursions details on the excursions section as following:")]
        public void ThenIVerifyTheExcursionsDetailsOnTheExcursionsSectionAsFollowing(Table table)
        {
            _bookingDetails.VerifyExcursionsDetails(table);
        }

        [When(@"I click more button on excursions sections")]
        public void GivenIClickMoreButtonOnExcursionsSections()
        {
            _bookingDetails.ClickExcursionsMoreIcon();
        }

        [Then(@"I am displayed the excursions details dialog box")]
        public void ThenIAmDisplayedTheExcursionsDetailsDialogBox()
        {
            _bookingDetails.VerifyExcursionDialogDisplayed();
        }

        [Then(
            @"I verify the excursions dialog header contains title as ""(.*)"",booking ref as ""(.*)"" and passenger name as ""(.*)""")]
        public void ThenIVerifyTheExcursionsDialogHeaderContainsTitleAsBookingRefAsAndPassengerNameAs(string title,
            string reference, string name)
        {
            _bookingDetails.VerifyExcursionDialogHeader(title, reference, name);
        }

        [Then(@"I verify the excursions details as the following when expanded in the excursion details section:")]
        public void ThenIVerifyTheExcursionsDetailsAsTheFollowingWhenExpandedInTheExcursionDetailsSection(Table table)
        {
            _bookingDetails.VerifyExcursionsDetailsExpanded(table);
        }

        [When(@"I close the excursions summary expand view")]
        public void WhenICloseTheExcursionsSummaryExpandView()
        {
            _bookingDetails.CloseExcursionSummaryExpandView();
        }
    }
}