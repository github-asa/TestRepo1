using J2BIOverseasOps.Pages.Customer_Interaction;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.Customer_Interaction
{
    [Binding]
    public sealed class BookingDetailsPropertyDetailsStepDefs : BaseStepDefs
    {
        private readonly BookingDetailsPage _bookingDetails;

        public BookingDetailsPropertyDetailsStepDefs(IWebDriver driver, ILog log) : base(driver, log)
        {
            _bookingDetails = new BookingDetailsPage(driver, log);
        }

        [Then(
            @"I verify the property section is displayed with the ""(.*)"" section header on the booking details page")]
        public void ThenIVerifyThePropertySectionIsDisplayedWithTheSectionHeaderOnTheBookingDetailsPage(
            string propertySecHeader)
        {
            _bookingDetails.VerifyBdPropertySectionHeader(propertySecHeader);
        }

        [Then(@"I verify the From and To dates are in the format on the booking details page")]
        public void ThenIVerifyTheFromAndToDatesAreInTheFormatOnTheBookingDetailsPage()
        {
            _bookingDetails.VerifyBdFromToDatesFormat();
        }

        [Then(@"I verify the property details are displayed as the following on the booking details page:")]
        public void ThenIVerifyThePropertyDetailsAreDisplayedAsTheFollowingOnTheBookingDetailsPage(Table table)
        {
            _bookingDetails.VerifyBdPropertyInformation(table);
        }

        [Then(
            @"I verify the ""(.*)"" is displayed after the ""(.*)"" seperated with a comma on the booking details page")]
        public void ThenIVerifyTheIsDisplayedAfterTheSeperatedWithACommaOnTheBookingDetailsPage(string resortName,
            string propertyName)
        {
            _bookingDetails.VerifyBdResortPropertyName(resortName, propertyName);
        }

        [Then(@"I verify that only the current property is displayed on the booking details page")]
        public void ThenIVerifyThatOnlyTheCurrentPropertyIsDisplayedAsOnTheBookingDetailsPage()
        {
            _bookingDetails.VerifyOnlyCurrentProperty();
        }

        [Then(@"I verify the more information icon is displayed for the property section on the booking details page")]
        public void ThenIVerifyTheMoreInformationIconIsDisplayedForThePropertySectionOnTheBookingDetailsPage()
        {
            _bookingDetails.VerifyBdPropertyMoreIcon();
        }


        [When(@"I click the more icon on the property overview panel on the booking details page")]
        public void WhenIClickTheMoreIconOnThePropertyOverviewPanelOnTheBookingDetailsPage()
        {
            _bookingDetails.ClickBdPropertyMoreIcon();
        }

        [Then(@"the property details section is displayed")]
        public void ThenThePropertyDetailsSectionIsDisplayed()
        {
            _bookingDetails.VerifyPropertyDetailsSectionDisplayed();
        }

        [Then(
            @"I verify the number of properties associated with the booking is ""(.*)"" in the property details section")]
        public void ThenIVerifyTheNumberOfPropertiesAssociatedWithTheBookingIsInThePropertyDetailsSection(
            int numberOfProperties)
        {
            _bookingDetails.VerifyNumberOfProperties(numberOfProperties);
        }

        [Then(
            @"I verify the ""(.*)"",  ""(.*)"", ""(.*)"", ""(.*)"", ""(.*)"", ""(.*)"" and ""(.*)"" headers are displayed in the property details section")]
        public void ThenIVerifyThatTheAndHeadersAreDisplayedInThePropertyDetailsSection(string propertyDetails,
            string propertyRef, string property, string resort, string status, string supplier, string dates)
        {
            _bookingDetails.VerifyPropertyDetailsSectionHeaders(propertyDetails, propertyRef, property, resort, status,
                supplier, dates);
        }

        [Then(
            @"I verify the booking ref as ""(.*)"" and Lead Customer as ""(.*)"" is displayed in the property details section")]
        public void ThenIVerifyTheBookingRefAsAndLeadCustomerAsOnThePropertyDetailsSection(string bookingRef,
            string leadCustomer)
        {
            _bookingDetails.VerifyPropertyHeaderDetails(bookingRef, leadCustomer);
        }

        [Then(
            @"I verify the property reference, property, Resort, status, Supplier and Dates are displayed in the property details section:")]
        public void
            ThenIVerifyTheBookingReferenceLeadPassengerNamePropertyReferencePropertyResortStatusSupplierAndDatesAreDisplayedInThePropertyDetailsSection(
                Table table)
        {
            _bookingDetails.VerifyPropertyDetails(table);
        }

        [Then(@"I verify each property row can be expanded in the property details section")]
        public void ThenIVerifyEachPropertyRowCanBeExpandedInThePropertyDetailsSection()
        {
            _bookingDetails.ExpandProperties();
        }

        [When(@"I expand the row for a property ""(.*)"" in the property details section")]
        public void WhenIExpandTheRowForAPropertyInThePropertyDetailsSection(string propertyNumber)
        {
            _bookingDetails.ExpandaProperty(propertyNumber);
        }

        [Then(@"I verify the property details as the following when expanded in the property details section:")]
        public void ThenIVerifyThePropertyDetailsAsTheFollowingWhenExpandedInThePropertyDetailsSection(Table table)
        {
            _bookingDetails.VerifyPropertyDetailsExpanded(table);
        }

        [When(@"I close the property details section")]
        public void WhenICloseThePropertyDetailsSection()
        {
            _bookingDetails.ClickClosePropertyDetailsSection();
        }

        [Then(@"I verify the room type as ""(.*)"" on the booking details page")]
        public void ThenIVerifyTheRoomTypeAsOnTheBookingDetailsPage(string roomType)
        {
            _bookingDetails.VerifyRoomType(roomType);
        }

        [Then(@"I verify the board type as ""(.*)"" on the booking details page")]
        public void ThenIVerifyTheBoardTypeAsOnTheBookingDetailsPage(string boardType)
        {
            _bookingDetails.VerifyBoardType(boardType);
        }
    }
}