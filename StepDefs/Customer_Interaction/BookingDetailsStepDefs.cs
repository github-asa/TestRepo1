using J2BIOverseasOps.Pages.Customer_Interaction;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.Customer_Interaction
{
    [Binding]
    public sealed class BookingDetailsStepDefs : BaseStepDefs
    {
        private readonly BookingDetailsPage _bookingDetails;

        public BookingDetailsStepDefs(IWebDriver driver, ILog log) : base(driver, log)
        {
            _bookingDetails = new BookingDetailsPage(driver, log);
        }

        [Then(@"I verify the booking type as ""(.*)"" on the booking details page")]
        public void ThenIVerifyTheBookingTypeAsOnTheBookingDetailsPage(string bookingType)
        {
            _bookingDetails.VerifyBookingType(bookingType);
        }

        [Then(@"I verify the booking reference as ""(.*)"" on the booking details page")]
        public void ThenIVerifyTheBookingReferenceAsOnTheBookingDetailsPage(string bookingRef)
        {
            _bookingDetails.VerifyBookingRef(bookingRef);
        }

        [Then(
            @"I verify the Booking reference, Customer details, Excursion, Quick links, Flights, Transfers, Property and Timeline/Notes sections are displayed on the booking details page")]
        public void
            ThenIVerifyTheBookingReferenceCustomerDetailsExcursionQuickLinksFlightsTransfersPropertyAndTimelineNotesSectionIsDisplayedOnTheBookingDetailsPage()
        {
            _bookingDetails.VerifyBookingDetailsSections();
        }

        [Then(
            @"I verify that the sections will have the following headers ""(.*)"", ""(.*)"", ""(.*)"", ""(.*)"", ""(.*)"" and ""(.*)"" on the booking details page")]
        public void ThenIVerifyThatTheSectionsWillHaveTheFollowingHeadersAndOnTheBookingDetailsPage(
            string bookingRefSecHeader, string excursionsSecHeader, string quicklinksSecHeader, string flightSecHeader,
            string transfersSecHeader, string propertySecHeader)
        {
            _bookingDetails.VerifyBookingDetailsHeaders(bookingRefSecHeader, excursionsSecHeader, quicklinksSecHeader,
                flightSecHeader, transfersSecHeader, propertySecHeader);
        }

        [Then(@"I verify that the booking details page has a ""(.*)"" title")]
        public void ThenIVerifyThePageWillHaveATitleOnTheBookingDetailsPage(string bookingDetailsTitle)
        {
            _bookingDetails.VerifyBookingDetailsPageTitle(bookingDetailsTitle);
        }

        [When(@"I close the Booking Details page")]
        public void WhenICloseTheBookingDetailsPage()
        {
            _bookingDetails.CloseBookingDetailsPage();
        }
    }
}