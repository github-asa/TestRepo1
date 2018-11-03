using J2BIOverseasOps.Pages.Customer_Interaction;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.Customer_Interaction
{
    [Binding]
    public sealed class BookingDetailsFlightDetailsStepDefs : BaseStepDefs
    {
        private readonly BookingDetailsPage _bookingDetails;

        public BookingDetailsFlightDetailsStepDefs(IWebDriver driver, ILog log) : base(driver, log)
        {
            _bookingDetails = new BookingDetailsPage(driver, log);
        }

        [Then(@"I verify I can see the flight overview section on the booking details page")]
        public void ThenIVerifyICanSeeTheFlightOverviewSectionOnTheBookingDetailsPage()
        {
            _bookingDetails.VerifyFlightOverviewSection();
        }

        [Then(@"I verify the '(.*)' header is displayed on the booking details page")]
        public void ThenIVerifyTheHeaderIsDisplayedOnTheBookingDetailsPage(string flightSecHeader)
        {
            _bookingDetails.VerifyFlightsSectionHeader(flightSecHeader);
        }

        [Then(@"I verify the '(.*)' and '(.*)' header is displayed and in bold on the booking details page")]
        public void ThenIVerifyTheAndHeaderIsDisplayedAndInBoldOnTheBookingDetailsPage(string outboundHeader,
            string returnHeader)
        {
            _bookingDetails.VerifyOutboundReturnHeaders(outboundHeader, returnHeader);
        }

        [Then(
            @"I verify the ""(.*)"", ""(.*)"", ""(.*)"", ""(.*)"", ""(.*)"" and the ""(.*)"" is displayed for the outbound flight on the booking details page")]
        public void ThenIVerifyTheAndTheIsDisplayedForTheOutboundFlightOnTheBookingDetailsPage(string outboundPnr,
            string ukAirport, string departDateTime, string arrivalAirport, string outboundFlightNumber,
            string arrivalDateTime)
        {
            _bookingDetails.VerifyOutboundFlightDetails(outboundPnr, ukAirport, departDateTime, arrivalAirport,
                outboundFlightNumber, arrivalDateTime);
        }

        [Then(
            @"I verify the ""(.*)"", ""(.*)"", ""(.*)"", ""(.*)"", ""(.*)"" and the ""(.*)"" is displayed for the return flight on the booking details page")]
        public void ThenIVerifyTheAndTheIsDisplayedForTheReturnFlightOnTheBookingDetailsPage(string returnPnr,
            string ukAirport, string departDateTime, string arrivalAirport, string returnFlightNumber,
            string arrivalDateTime)
        {
            _bookingDetails.VerifyReturnFlightDetails(returnPnr, ukAirport, departDateTime, arrivalAirport,
                returnFlightNumber, arrivalDateTime);
        }

        [Then(@"I verify the airplane icon is displayed underneath the flight number for the outbound flight")]
        public void ThenIVerifyTheAirplaneIconIsDisplayedUnderneathTheFlightNumberForTheOutboundFlight()
        {
            _bookingDetails.VerifyOutboundAirplaneIcon();
        }

        [Then(@"I verify the more icon is displayed against the lead passenger PNR for the outbound flight")]
        public void ThenIVerifyTheIconIsDisplayedAgainstTheLeadPassengerPnrForTheOutboundFlight()
        {
            _bookingDetails.VerifyBdOutboundFlightMoreIcon();
        }

        [Then(@"I verify the airplane icon is displayed underneath the flight number for the return flight")]
        public void ThenIVerifyTheAirplaneIconIsDisplayedUnderneathTheFlightNumberForTheReturnFlight()
        {
            _bookingDetails.VerifyReturnAirplaneIcon();
        }

        [Then(@"I verify the more icon is displayed against the lead passenger PNR for the return flight")]
        public void ThenIVerifyTheIconIsDisplayedAgainstTheLeadPassengerPnrForTheReturnFlight()
        {
            _bookingDetails.VerifyBdReturnFlightMoreIcon();
        }

        [When(@"I click the more icon for the outbound flight")]
        public void WhenIClickTheMoreIconForTheOutboundFlight()
        {
            _bookingDetails.ClickBdOutboundFlightsMoreIcon();
        }

        [Then(@"the outbound PNR details section is displayed")]
        public void ThenTheOutboundPnrDetailsSectionIsDisplayed()
        {
            _bookingDetails.VerifyPnrDetailsSectionDisplayed();
        }

        [Then(@"I verify the outbound flight PNR details section contains the heading '(.*)'")]
        public void ThenIVerifyTheOutboundFlightPnrDetailsSectionContainsTheHeading(string pnrHeader)
        {
            _bookingDetails.VerifyPnrDetailsSectionHeader(pnrHeader);
        }

        [Then(@"I verify the outbound flight PNR details section contains the ""(.*)""and ""(.*)"" in the header")]
        public void ThenIVerifyTheOutboundFlightPnrDetailsSectionContainsTheAndInTheHeader(string ukAirport,
            string arrivalAirport)
        {
            _bookingDetails.VerifyPnrDetailsSectionAirports(ukAirport, arrivalAirport);
        }

        [Then(@"I verify the outbound flight PNR details section contains the ""(.*)"" in bold at the top of the list")]
        public void ThenIVerifyTheOutboundFlightPnrDetailsSectionContainsTheInBoldAtTheTopOfTheList(string leadCustomer)
        {
            _bookingDetails.VerifyPnrDetailsLeadCustomer(leadCustomer);
        }

        [Then(@"I verify the outbound flight PNR details section contains the ""(.*)""")]
        public void ThenIVerifyTheOutboundFlightPnrDetailsSectionContainsThe(string leadPnr)
        {
            _bookingDetails.VerifyPnrDetailsLeadPnr(leadPnr);
        }

        [Then(
            @"I verify if there are non-lead passengers on the booking, their name and PNR is displayed in the outbound flight PNR details section:")]
        public void
            ThenIVerifyIfThereAreNon_LeadPassengersOnTheBookingTheirNameAndPNRIsDisplayedInTheOutboundFlightPNRDetailsSection(
                Table table)
        {
            _bookingDetails.VerifyPnrDetailsPassengers(table);
        }


        [When(@"I close the PNR outbound details section")]
        public void WhenICloseThePnrOutboundDetailsSection()
        {
            _bookingDetails.ClickClosePnrDetailsSection();
        }

        [When(@"I click the more icon for the return flight")]
        public void WhenIClickTheIconForTheReturnFlight()
        {
            _bookingDetails.ClickBdReturnFlightsMoreIcon();
        }

        [Then(@"the return PNR details section is displayed")]
        public void ThenTheReturnPnrDetailsSectionIsDisplayed()
        {
            _bookingDetails.VerifyPnrDetailsSectionDisplayed();
        }

        [Then(@"I verify the return flight PNR details section contains the heading '(.*)'")]
        public void ThenIVerifyTheReturnFlightPnrDetailsSectionContainsTheHeading(string pnrHeader)
        {
            _bookingDetails.VerifyPnrDetailsSectionHeader(pnrHeader);
        }

        [Then(@"I verify the return flight PNR details section contains the ""(.*)""and ""(.*)"" in the header")]
        public void ThenIVerifyTheReturnFlightPnrDetailsSectionContainsTheAndInTheHeader(string ukAirport,
            string arrivalAirport)
        {
            _bookingDetails.VerifyPnrDetailsSectionAirports(ukAirport, arrivalAirport);
        }


        [Then(@"I verify the return flight PNR details section contains the ""(.*)"" in bold at the top of the list")]
        public void ThenIVerifyTheReturnFlightPnrDetailsSectionContainsTheInBoldAtTheTopOfTheList(string leadCustomer)
        {
            _bookingDetails.VerifyPnrDetailsLeadCustomer(leadCustomer);
        }

        [Then(@"I verify the return flight PNR details section contains the ""(.*)""")]
        public void ThenIVerifyTheReturnFlightPnrDetailsSectionContainsThe(string leadPnr)
        {
            _bookingDetails.VerifyPnrDetailsLeadPnr(leadPnr);
        }

        [Then(
            @"I verify if there are non-lead passengers on the booking, their name and PNR is displayed in the return flight PNR details section:")]
        public void
            ThenIVerifyIfThereAreNon_LeadPassengersOnTheBookingTheirNameAndPNRIsDisplayedInTheReturnFlightPNRDetailsSection(
                Table table)
        {
            _bookingDetails.VerifyPnrDetailsPassengers(table);
        }

        [When(@"I close the PNR return details section")]
        public void WhenICloseThePnrReturnDetailsSection()
        {
            _bookingDetails.ClickClosePnrDetailsSection();
        }
    }
}