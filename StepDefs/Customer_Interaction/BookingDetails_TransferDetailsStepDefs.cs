using J2BIOverseasOps.Pages.Customer_Interaction;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.Customer_Interaction
{
    [Binding]
    public sealed class BookingDetailsTransferDetailsStepDefs : BaseStepDefs
    {
        private readonly BookingDetailsPage _bookingDetails;

        public BookingDetailsTransferDetailsStepDefs(IWebDriver driver, ILog log) : base(driver, log)
        {
            _bookingDetails = new BookingDetailsPage(driver, log);
        }

        [Then(@"I verify the number of transfers displayed is ""(.*)"" on the booking details page")]
        public void ThenIVerifyTheNumberOfTransfersDisplayedIsOnTheBookingDetailsPage(int numberOfTransfers)
        {
            _bookingDetails.VerifyNumberOfTransfers(numberOfTransfers);
        }

        [Then(@"I verify the transfer type as ""(.*)"" on the booking details page")]
        public void ThenIVerifyTheTransferTypeAsOnTheBookingDetailsPage(string transferType)
        {
            _bookingDetails.VerifyTransferType(transferType);
        }

        [Then(@"I verify the transfer reference as ""(.*)"" on the booking details page")]
        public void ThenIVerifyTheTransferReferenceAsOnTheBookingDetailsPage(string transferReference)
        {
            _bookingDetails.VerifyTransferRef(transferReference);
        }
    }
}