using System;
using System.Globalization;
using System.Linq;
using J2BIOverseasOps.Extensions;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Pages.Customer_Interaction
{
    internal class BookingDetailsPage : BasePage
    {
        private readonly By _alternativeHeader = By.Id("contact-details-dialog-alternative-header");
        private readonly By _alternativeNumber = By.Id("contact-details-dialog-alternative-number");
        private readonly By _bookingDetailsBoardType = By.CssSelector("td[id^=booking-summary-board-type]");

        //Booking Section Overview
        private readonly By _bookingDetailsBookingRef = By.CssSelector("[bookingref]");
        private readonly By _bookingDetailsBookingType = By.CssSelector("[bookingtype] > span");
        private readonly By _bookingDetailsClosePage = By.CssSelector("[label='Back']");
        private readonly By _bookingdetailsclosePnrDetailsSection =
            By.CssSelector("app-flights-summary .pi.pi-times");
        private readonly By _bookingDetailsCurrentProperty = By.CssSelector("app-property-summary .ui-card-body");
        private readonly By _bookingDetailsCustomerSection =
            By.CssSelector("app-booking-summary > p-card > div > div.ui-card-body");
        private readonly By _bookingDetailsExcursionExpandedClose =
            By.CssSelector("app-excursions-summary .pi.pi-times");
        private readonly By _bookingDetailsExcursionExpandedHeader =
            By.CssSelector("#excursions-summary-dialog-header");
        private readonly By _bookingDetailsExcursionsIcn = By.CssSelector("#excursions-summary-details-icon");
        private readonly By _bookingDetailsExcursionsSecHeader = By.CssSelector("#excursions-summary-heading");
        private readonly By _bookingDetailsExcursionsSection = By.CssSelector("app-excursions-summary > p-card");

        //Excursions Section Expanded View
        private readonly By _bookingDetailsExcursionsSummary = By.CssSelector("app-excursions-summary > p-card tr");
        private readonly By _bookingDetailsFlightsOutboundArrivalAirport =
            By.CssSelector("#flight-summary-outbound-arrival-airport-code");
        private readonly By _bookingDetailsFlightsOutboundArrivalDateTime =
            By.CssSelector("#flight-summary-outbound-arrival-date");
        private readonly By _bookingDetailsFlightsOutboundDepartDateTime =
            By.CssSelector("#flight-summary-outbound-departure-date");
        private readonly By _bookingDetailsFlightsOutboundFlightNumber = By.CssSelector(
            "app-flights-summary > p-card > div > div.ui-card-body > div > div:nth-child(1) > div:nth-child(2)");

        //Flights Section Overview  
        private readonly By _bookingDetailsFlightsOutboundHeader =
            By.CssSelector("#flight-summary-outbound-header > strong");
        private readonly By _bookingDetailsFlightsOutboundMoreIcon = By.CssSelector(
            "app-flights-summary > p-card > div > div.ui-card-body > div > div:nth-child(1) > div:nth-child(4) > button");
        private readonly By _bookingDetailsFlightsOutboundPlaneIcon = By.CssSelector(
            "app-flights-summary > p-card > div > div.ui-card-body > div > div:nth-child(2) > div:nth-child(3) > i");
        private readonly By _bookingDetailsFlightsOutboundPnr = By.CssSelector(
            "app-flights-summary > p-card > div > div.ui-card-body > div > div:nth-child(1) > div:nth-child(3)");
        private readonly By _bookingDetailsFlightsOutboundUkAirport =
            By.CssSelector("#flight-summary-outbound-departure-airport-code");
        private readonly By _bookingDetailsFlightsReturnArrivalAirport =
            By.CssSelector("#flight-summary-return-departure-airport-code");
        private readonly By _bookingDetailsFlightsReturnArrivalDateTime =
            By.CssSelector("#flight-summary-return-arrival-date");
        private readonly By _bookingDetailsFlightsReturnDepartDateTime =
            By.CssSelector("#flight-summary-return-departure-date");
        private readonly By _bookingDetailsFlightsReturnFlightNumber = By.CssSelector(
            "app-flights-summary > p-card > div > div.ui-card-body > div > div:nth-child(4) > div:nth-child(2)");
        private readonly By _bookingDetailsFlightsReturnHeader =
            By.CssSelector("#flight-summary-return-header > strong");

        //More Information Buttons
        private readonly By _bookingDetailsFlightsReturnMoreIcon = By.CssSelector(
            "app-flights-summary > p-card > div > div.ui-card-body > div > div:nth-child(4) > div:nth-child(4)  > button");
        private readonly By _bookingDetailsFlightsReturnPlaneIcon = By.CssSelector(
            "app-flights-summary > p-card > div > div.ui-card-body > div > div:nth-child(5) > div:nth-child(3) > i");
        private readonly By _bookingDetailsFlightsReturnPnr = By.CssSelector(
            "app-flights-summary > p-card > div > div.ui-card-body > div > div:nth-child(4) > div:nth-child(3)");
        private readonly By _bookingDetailsFlightsReturnUkAirport =
            By.CssSelector("#flight-summary-return-arrival-airport-code");
        private readonly By _bookingDetailsFlightsSecHeader = By.CssSelector("#flight-summary-heading");
        private readonly By _bookingDetailsFlightsSection = By.CssSelector("app-flights-summary > p-card");
        private readonly By _bookingDetailsFromToDate = By.CssSelector("#booking-summary-property-from-date > strong");

        //Customer Section Overview
        private readonly By _bookingDetailsLeadCustomerName = By.Id("contact-details-customer-name-1");
        private readonly By _bookingDetailsPnrDetailsLeadCustomer =
            By.CssSelector("#flight-summary-dialog-customer-name > strong");
        private readonly By _bookingDetailsPnrDetailsLeadPnr = By.CssSelector("#flight-summary-dialog-pnr");
        private readonly By _bookingDetailsPnrDetailsPassName = By.CssSelector("#flight-summary-dialog-customer-name");
        private readonly By _bookingDetailsPnrDetailsPassPnr = By.CssSelector("#flight-summary-dialog-pnr");

        //Flights Section Expanded View
        private readonly By _bookingDetailsPnrDetailsSection =
            By.CssSelector("app-flights-summary p-dialog");
        private readonly By _bookingDetailsPnrDetailsSectionHeader = By.CssSelector("#flight-summary-dialog-header");

        //Property Section Expanded View
        private readonly By _bookingDetailsPropertyDetailsSection =
            By.CssSelector("app-property-summary  p-dialog");
        private readonly By _bookingDetailsPropertyIcn =
            By.CssSelector("#booking-summary-details-icon > button");
        private readonly By _bookingDetailsPropertySecHeader = By.CssSelector("#booking-summary-header");

        private readonly By _bookingDetailsPropertySection = By.CssSelector("app-property-summary > p-card");
        private readonly By _bookingDetailsPropertySectionContent =
            By.CssSelector("[id^=booking-summary-dialog-row-expander]");
        private readonly By _bookingDetailsQuickLinksSecHeader = By.CssSelector("app-quick-links .ui-g-12 > h4");
        private readonly By _bookingDetailsQuickLinksSection = By.CssSelector("app-quick-links > p-card");
        private readonly By _bookingDetailsRefSecHeader = By.CssSelector("#contact-details-heading");

        //Overview Sections
        private readonly By _bookingDetailsRefSection = By.CssSelector("app-booking-summary > p-card");

        //Property Section Overview  
        private readonly By _bookingDetailsResortPropertyName =
            By.CssSelector("#booking-summary-property-resort-name > strong");
        private readonly By _bookingDetailsRoomType = By.CssSelector("td[id^=booking-summary-room-type]");
        private readonly By _bookingDetailsTimeLineNotesSection = By.CssSelector("app-booking-timeline-notes > p-card");
        //Overview Headers
        private readonly By _bookingDetailsTitle = By.CssSelector("h1");
        private readonly By _bookingDetailsTransferRef = By.CssSelector("[transferref]");
        private readonly By _bookingDetailsTransfersContent = By.CssSelector("[transferscotent]");
        private readonly By _bookingDetailsTransfersSecHeader = By.CssSelector("app-transfers-summary .ui-g-12 > h4");
        private readonly By _bookingDetailsTransfersSection = By.CssSelector("app-transfers-summary > p-card");

        //Transfers Section Overview  
        private readonly By _bookingDetailsTransferType = By.CssSelector("[transfertype]");
        private readonly By _closeContactDetailsDialog = By.CssSelector("[role=dialog] a");
        private readonly By _closePropertyDetailsSection = By.CssSelector("app-property-summary .pi.pi-times");

        //Contact Details Expanded View
        private readonly By _contactDetailsHeader = By.Id("contact-details-dialog-header");

        //tables
        private readonly By _customerOverviewDetailsTable = By.CssSelector("app-booking-summary [customers] table  ");
        private readonly By _emailAddress = By.Id("contact-details-dialog-email-address");
        private readonly By _emailHeader = By.Id("contact-details-dialog-email-header");
        private readonly By _emergencyHeader = By.Id("contact-details-dialog-emergency-header");
        private readonly By _emergencyNumber = By.Id("contact-details-dialog-emergency-number");
        private readonly By _expandPropertyIconMinus = By.CssSelector(".fa.fa-fw.fa-minus");
        private readonly By _expandPropertyIconPlus = By.CssSelector(".fa.fa-fw.fa-plus");
        private readonly By _primaryHeader = By.Id("contact-details-dialog-primary-header");
        private readonly By _primaryNumber = By.Id("contact-details-dialog-primary-number");
        private readonly By _propertyDetailsDatesHeader = By.CssSelector("[id=booking-summary-dialog-dates-header]");
        private readonly By _propertyDetailsHeader = By.CssSelector("app-property-summary  p-dialog div > span");
        private readonly By _propertyDetailsPropertyHeader =
            By.CssSelector("[id=booking-summary-dialog-property-header]");
        private readonly By _propertyDetailsPropertyRefHeader =
            By.CssSelector("[id=booking-summary-dialog-ref-header]");
        private readonly By _propertyDetailsResortHeader = By.CssSelector("[id=booking-summary-dialog-resort-header]");
        private readonly By _propertyDetailsStatusHeader = By.CssSelector("[id=booking-summary-dialog-status-header]");
        private readonly By _propertyDetailsSupplierHeader =
            By.CssSelector("[id=booking-summary-dialog-supplier-header]");

        public BookingDetailsPage(IWebDriver driver, ILog log) : base(driver, log)
        {
        }

        public void VerifyBookingDetailsDisplayed()
        {
            Driver.WaitForPageToLoad();
            var bookingDetailsHeader = Driver.GetText(_bookingDetailsTitle);
            Assert.True(Driver.WaitForItem(_bookingDetailsTitle), $"The Booking Details header is not displayed");
            Assert.AreEqual("Booking details", bookingDetailsHeader, $"The Booking Details header is incorrect");
        }

        internal void VerifyBookingRef(string bookingRef)
        {
            Driver.WaitUntilTextPresent(_bookingDetailsBookingRef);
            Assert.AreEqual(bookingRef, Driver.GetText(_bookingDetailsBookingRef),
                $"The Booking Reference is incorrect");
        }
        internal void VerifyBdFromToDatesFormat()
        {
            Driver.WaitUntilTextPresent(_bookingDetailsFromToDate);
            var bookingDetailsFromToDate = Driver.GetText(_bookingDetailsFromToDate);

            var fromToDate = bookingDetailsFromToDate;
            var fromDate = fromToDate.Substring(0, 13);
            var toDate = fromToDate.Substring(16, 13);

            string[] formats = { "MM/dd/yyyy", "M/d/yyyy", "M/dd/yyyy", "MM/d/yyyy", "ddd dd MMM y" };

            DateTime expectedDate;

            Assert.IsTrue(
                DateTime.TryParseExact(fromDate, formats, new CultureInfo("en-US"), DateTimeStyles.None,
                    out expectedDate), $"The format of the From date on the booking details page is incorrect");
            Assert.IsTrue(
                DateTime.TryParseExact(toDate, formats, new CultureInfo("en-US"), DateTimeStyles.None,
                    out expectedDate), $"The format of the To date on the booking details page is incorrect");
        }

        internal void VerifyBookingType(string bookingType)
        {
            Driver.WaitUntilTextPresent(_bookingDetailsBookingType);
            var bookingDetailsBookingType = Driver.GetText(_bookingDetailsBookingType);
            Assert.True(Driver.WaitForItem(_bookingDetailsBookingType), $"The Booking Type is not displayed");
            Assert.AreEqual(bookingType, bookingDetailsBookingType, $"The Booking Type is incorrect");
        }

        internal void VerifyBdPropertySectionHeader(string propertySecHeader)
        {
            Assert.True(Driver.WaitForItem(_bookingDetailsPropertySection),
                $"The Booking Details Reference Section is not displayed");
            var bookingDetailsPropertySecHeader = Driver.GetText(_bookingDetailsPropertySecHeader);
            Assert.AreEqual(propertySecHeader, bookingDetailsPropertySecHeader,
                $"The Booking Details Property Section header is incorrect");
        }

        internal void VerifyPropertyDetailsSectionDisplayed()
        {
            Assert.True(Driver.WaitForItem(_bookingDetailsPropertyDetailsSection),
                $"The property details section is not displayed");

            var bookingDetailsPropertyDetailsSection = Driver.GetText(_bookingDetailsPropertyDetailsSection);
            var propertyDetailsFullTitle = bookingDetailsPropertyDetailsSection;
            var propertyDetailsTitle = propertyDetailsFullTitle.Substring(0, 16);

            Assert.AreEqual("Property details", propertyDetailsTitle,
                $"The property details section title is incorrect");
        }

        internal void VerifyPropertyHeaderDetails(string bookingRef, string leadCustomer)
        {
            Assert.True(Driver.WaitForItem(_bookingDetailsPropertyDetailsSection),
                $"The property details section is not displayed");

            var bookingDetailsPropertyHeaderDetails = Driver.GetText(_propertyDetailsHeader);
            var propertyDetailsFullTitle = bookingDetailsPropertyHeaderDetails;

            var propertyDetails = propertyDetailsFullTitle.Split('-');
            var propertyDetailsBookingRef = propertyDetails[1].Trim();
            var propertyDetailsLeadCustomer = propertyDetails[2].Trim();

            Assert.AreEqual(bookingRef, propertyDetailsBookingRef,
                $"The Booking Ref in the property details section is incorrect");
            Assert.AreEqual(leadCustomer, propertyDetailsLeadCustomer,
                $"The Lead Customer Name in the property details section is incorrect");
        }

        internal void ExpandaProperty(string propertyNumber)
        {
            var expandaProperty =
                Driver.FindElement(By.CssSelector("#booking-summary-dialog-row-expander-" + propertyNumber + " > i"));

            Assert.True(Driver.WaitForItem(expandaProperty), $"The property expand icon is not displayed");
            Driver.ClickItem(expandaProperty);
        }

        internal void VerifyBirthdayDetailsAreDisplayed(Table table)
        {
            Assert.True(Driver.WaitForItem(_bookingDetailsCustomerSection),"The Booking Details Customer Section is not displayed");
            Assert.True(Driver.WaitForItem(_customerOverviewDetailsTable),"The Booking Details Customer Overview Details table is not displayed");
            var tableElement = Driver.FindElement(_customerOverviewDetailsTable);
            var rowNumberFlag = 0;

            foreach (var row in table.Rows)
            {
                var actualRows = tableElement.FindElements(By.CssSelector("tr"));

                if (rowNumberFlag.Equals(0))
                {
                    var leadCustomerName = actualRows[rowNumberFlag].FindElement(By.CssSelector("td"));
                    var actualResult = actualRows[rowNumberFlag].FindElements(By.CssSelector("td"));
                    Assert.AreEqual(row["Customer"], Driver.GetText(leadCustomerName), "The lead customer name is not correct.");

                    if (!string.IsNullOrWhiteSpace(row["Birthday"]))
                    {
                        var birthday = Driver.GetText(actualResult[0]);
                        Assert.AreEqual(row["Birthday"], birthday, "The birthday is not displayed");
                        Assert.IsTrue(Driver.WaitForItem(actualResult[1].FindElement(By.TagName("i"))));
                    }

                    var dob = DateTime.ParseExact(row["DOB"], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    var now = DateTime.Now;
                    var age = now.Year - dob.Year;
                    if (now.Month < dob.Month || (now.Month == dob.Month && now.Day < dob.Day)) age--;
                    Assert.AreEqual($"{row["DOB"]}({age})", Driver.GetText(actualResult[2]),"The customer name is not displayed.");
                    Assert.AreEqual(row["Telephone"], Driver.GetText(actualResult[3]),"The telelphone number is not Correct");
                }

                else
                {
                    var actualResult = actualRows[rowNumberFlag].FindElements(By.CssSelector("td"));
                    Assert.AreEqual(row["Customer"], Driver.GetText(actualResult[0]), "The customer name is not displayed.");

                    if (!string.IsNullOrWhiteSpace(row["Birthday"]))
                    {
                        var birthday = Driver.GetText(actualResult[1]);
                        Assert.AreEqual(row["Birthday"], birthday.Trim(), "The birthday is not displayed");
                        Assert.IsTrue(Driver.WaitForItem(actualResult[1].FindElement(By.TagName("i"))));
                    }

                    var dob = DateTime.ParseExact(row["DOB"], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    var now = DateTime.Now;
                    var age = now.Year - dob.Year;
                    if (now.Month < dob.Month || (now.Month == dob.Month && now.Day < dob.Day)) age--;
                    Assert.AreEqual($"{row["DOB"]}({age})", Driver.GetText(actualResult[2]),"The customer name is not displayed.");
                    Assert.AreEqual(row["Telephone"], Driver.GetText(actualResult[3]),"The telelphone number is not Correct");
                }

                //check the more button
                var tableData = actualRows[rowNumberFlag].FindElements(By.CssSelector("td"));
                var btnMore = tableData[tableData.Count - 1].FindElement(By.TagName("button"));
                Assert.IsTrue(Driver.WaitUntilClickable(btnMore), "The more button is not present on the customer overview details table.");

                rowNumberFlag++;
            }
        }

        internal void VerifyLeadCustomerNameIsBold()
        {
            Assert.IsTrue(Driver.IsElementBold(_bookingDetailsLeadCustomerName), "The lead customer name is not bold");
        }

        internal void ExpandProperties()
        {
            Assert.True(Driver.WaitForItem(_expandPropertyIconPlus), $"The property expand icon is not displayed");

            var tableElement = Driver.FindElement(By.CssSelector(".ui-dialog table"));
            var listOfPropertyPlusIcons = tableElement.FindElements(_expandPropertyIconPlus);

            foreach (var icon in listOfPropertyPlusIcons)
            {
                Driver.ClickItem(icon);
                Assert.True(Driver.WaitForItem(_expandPropertyIconMinus),
                    $"The property collapse icon is not displayed");
                var listOfPropertyMinusIcons = tableElement.FindElement(_expandPropertyIconMinus);
                Driver.ClickItem(listOfPropertyMinusIcons);
            }
        }

        internal void VerifyPropertyDetails(Table table)
        {
            var tableElement = Driver.FindElement(By.CssSelector(".ui-dialog table"));
            var rowNumberFlag = 0;
            foreach (var row in table.Rows)
            {
                var actualRows = tableElement.FindElements(By.CssSelector(".ui-dialog table tr"));
                var actualResult = actualRows[rowNumberFlag].FindElements(By.CssSelector("td"));
                Assert.True(actualResult[0].Text == row["Ref"],
                    $"The Property Reference should be {row["Ref"]} instead of {actualResult[0].Text}");
                Assert.True(actualResult[1].Text == row["Property"],
                    $"The Property should be {row["Property"]} instead of {actualResult[1].Text}");
                Assert.True(actualResult[2].Text == row["Resort"],
                    $"The Resort should be {row["Resort"]} instead of {actualResult[2].Text}");
                Assert.True(actualResult[3].Text == row["Status"],
                    $"The Destination should be {row["Status"]} instead of {actualResult[3].Text}");
                Assert.True(actualResult[4].Text == row["Supplier"],
                    $"The Supplier should be {row["Supplier"]} instead of {actualResult[4].Text}");
                Assert.True(actualResult[5].Text == row["Dates"],
                    $"The Dates should be {row["Dates"]} instead of {actualResult[5].Text}");
                rowNumberFlag++;
            }
        }

        internal void VerifyPropertyDetailsSectionHeaders(string propertyDetails, string propertyRef, string property,
            string resort, string status, string supplier, string dates)
        {
            Assert.True(Driver.WaitForItem(_bookingDetailsPropertyDetailsSection),
                $"The property details section header is not displayed");
            Assert.True(Driver.WaitForItem(_propertyDetailsPropertyRefHeader),
                $"The property details property ref header is not displayed");
            Assert.True(Driver.WaitForItem(_propertyDetailsPropertyHeader),
                $"The property details section property header is not displayed");
            Assert.True(Driver.WaitForItem(_propertyDetailsResortHeader),
                $"The property details section resort header is not displayed");
            Assert.True(Driver.WaitForItem(_propertyDetailsStatusHeader),
                $"The property details section status header is not displayed");
            Assert.True(Driver.WaitForItem(_propertyDetailsSupplierHeader),
                $"The property details section supplier header is not displayed");
            Assert.True(Driver.WaitForItem(_propertyDetailsDatesHeader),
                $"The property details section dates header is not displayed");


            var propertyDetailsPropertyRefHeader = Driver.GetText(_propertyDetailsPropertyRefHeader);
            var propertyDetailsPropertyHeader = Driver.GetText(_propertyDetailsPropertyHeader);
            var propertyDetailsResortHeader = Driver.GetText(_propertyDetailsResortHeader);
            var propertyDetailsStatusHeader = Driver.GetText(_propertyDetailsStatusHeader);
            var propertyDetailsSupplierHeader = Driver.GetText(_propertyDetailsSupplierHeader);
            var propertyDetailsDatesHeader = Driver.GetText(_propertyDetailsDatesHeader);
            var bookingDetailsPropertyDetailsSection = Driver.GetText(_bookingDetailsPropertyDetailsSection);

            var propertyDetailsFullTitle = bookingDetailsPropertyDetailsSection;
            var propertyDetailsTitle = propertyDetailsFullTitle.Substring(0, 16);

            Assert.AreEqual(propertyDetails, propertyDetailsTitle, $"The Property Details section header is incorrect");
            Assert.AreEqual(propertyRef, propertyDetailsPropertyRefHeader,
                $"The Property Details Reference header is incorrect");
            Assert.AreEqual(property, propertyDetailsPropertyHeader,
                $"The Property Details Property header is incorrect");
            Assert.AreEqual(resort, propertyDetailsResortHeader, $"The Property Details Resort header is incorrect");
            Assert.AreEqual(status, propertyDetailsStatusHeader, $"The Property Details Status header is incorrect");
            Assert.AreEqual(supplier, propertyDetailsSupplierHeader,
                $"The Property Details Supplier header is incorrect");
            Assert.AreEqual(dates, propertyDetailsDatesHeader, $"The Property Details Dates header is incorrect");
        }

        internal void VerifyNumberOfProperties(int numberOfProperties)
        {
            var bookingDetailsPropertySectionContent = Driver.FindElements(_bookingDetailsPropertySectionContent);

            Assert.True(Driver.WaitForItem(_bookingDetailsTransfersContent),
                $"The are no properties displayed for this booking");
            Assert.AreEqual(numberOfProperties, bookingDetailsPropertySectionContent.Count,
                $"The number of properties displayed in the property details section is incorrect");
        }

        internal void ClickBdPropertyMoreIcon()
        {
            Assert.True(Driver.WaitForItem(_bookingDetailsPropertyIcn), $"The Property more icon is not displayed");
            Driver.ClickItem(_bookingDetailsPropertyIcn);
        }

        internal void VerifyOnlyCurrentProperty()
        {
            Assert.True(Driver.WaitForItem(_bookingDetailsCurrentProperty),
                $"The Current Property is not displayed on the booking details page");

            var numberOfPropeties = 1;
            var bookingDetailsCurrentProperty = Driver.FindElements(_bookingDetailsCurrentProperty);
            Assert.AreEqual(numberOfPropeties, bookingDetailsCurrentProperty.Count,
                $"There is more than one property displayed on the booking details page");
        }

        internal void VerifyBdPropertyMoreIcon()
        {
            Assert.True(Driver.WaitForItem(_bookingDetailsPropertyIcn),
                $"The Booking Details more property information icon is not displayed");
        }

        internal void VerifyBdResortPropertyName(string resortName, string propertyName)
        {
            Assert.True(Driver.WaitForItem(_bookingDetailsResortPropertyName),
                $"The Booking Details Property and Resort Name is not displayed");

            var bookingDetailsResortPropertyName = Driver.GetText(_bookingDetailsResortPropertyName);
            var resortPropertyName = propertyName + ", " + resortName;
            Assert.AreEqual(resortPropertyName, bookingDetailsResortPropertyName,
                $"The Booking Details Property and Resort Name are incorrect");
        }

        internal void VerifyBdPropertyInformation(Table table)
        {
            Driver.WaitForItem(By.CssSelector("app-property-summary table"));
            var tableElement = Driver.FindElement(By.CssSelector("app-property-summary table"));
            var rowNumberFlag = 0;
            foreach (var row in table.Rows)
            {
                var actualRows = tableElement.FindElements(By.CssSelector("app-property-summary table tr"));
                var actualResult = actualRows[rowNumberFlag].FindElements(By.CssSelector("td"));
                Assert.True(actualResult[0].Text == row["Room Number"],
                    $"The Room Number should be {row["Room Number"]} instead of {actualResult[0].Text}");
                Assert.True(actualResult[1].Text == row["Room Type"],
                    $"The Room Type should be {row["Room Type"]} instead of {actualResult[1].Text}");
                Assert.True(actualResult[2].Text == row["Board Type"],
                    $"The Board Type should be {row["Board Type"]} instead of {actualResult[2].Text}");
                Assert.True(actualResult[3].Text == row["Room Reference"],
                    $"The |Room Reference should be {row["Room Reference"]} instead of {actualResult[3].Text}");
                rowNumberFlag++;
            }
        }

        internal void VerifyNumberOfTransfers(int numberOfTransfers)
        {
            Assert.True(Driver.WaitForItem(_bookingDetailsTransfersContent), $"The Transfers are not displayed");

            var bookingDetailsTransfersContent = Driver.FindElements(_bookingDetailsTransfersContent);
            Assert.AreEqual(numberOfTransfers, bookingDetailsTransfersContent.Count,
                $"The number of Transfers displayed is incorrect");
        }

        internal void VerifyTransferType(string transferType)
        {
            Assert.True(Driver.WaitForItem(_bookingDetailsTransferType), $"The Transfer Type is not displayed");
            Assert.True(Driver.IsElementPresent(_bookingDetailsTransferType), $"The Transfer Type is not displayed");
            Assert.True(Driver.WaitUntilTextPresent(_bookingDetailsTransferType));
            var bookingDetailsTransferType = Driver.GetText(_bookingDetailsTransferType);
            Assert.AreEqual(transferType, bookingDetailsTransferType, $"The Transfer Type is incorrect");
        }

        internal void VerifyTransferRef(string transferReference)
        {
            Assert.True(Driver.WaitForItem(_bookingDetailsTransferRef), $"The Transfer Reference is not displayed");

            var bookingDetailsTransferReference = Driver.GetText(_bookingDetailsTransferRef);
            Assert.AreEqual(transferReference, bookingDetailsTransferReference, $"The Transfer Reference is incorrect");
        }

        internal void VerifyBookingDetailsSections()
        {
            Assert.True(Driver.WaitForItem(_bookingDetailsCustomerSection),
                $"The Booking Details Customer Section is not displayed");
            Assert.True(Driver.WaitForItem(_bookingDetailsExcursionsSection),
                $"The Booking Details Excursions Section is not displayed");
            Assert.True(Driver.WaitForItem(_bookingDetailsFlightsSection),
                $"The Booking Details Flights Section is not displayed");
            Assert.True(Driver.WaitForItem(_bookingDetailsQuickLinksSection),
                $"The Booking Details Quick links Section is not displayed");
            Assert.True(Driver.WaitForItem(_bookingDetailsTransfersSection),
                $"The Booking Details Transfers Section is not displayed");
            Assert.True(Driver.WaitForItem(_bookingDetailsTimeLineNotesSection),
                $"The Booking Details Time Line and Notes Section is not displayed");
            Assert.True(Driver.WaitForItem(_bookingDetailsPropertySection),
                $"The Booking Details Property Section is not displayed");
            Assert.True(Driver.WaitForItem(_bookingDetailsRefSection),
                $"The Booking Details Reference Section is not displayed");
        }

        internal void VerifyBookingDetailsHeaders(string bookingRefSecHeader, string excursionsSecHeader,
            string quicklinksSecHeader, string flightSecHeader, string transfersSecHeader, string propertySecHeader)
        {
            Assert.True(Driver.WaitForItem(_bookingDetailsRefSecHeader),
                $"The Booking Details Booking Reference Section header is not displayed");
            Assert.True(Driver.WaitForItem(_bookingDetailsExcursionsSecHeader),
                $"The Booking Details Excursions Section header is not displayed");
            Assert.True(Driver.WaitForItem(_bookingDetailsQuickLinksSecHeader),
                $"The Booking Details Quick links Section header is not displayed");
            Assert.True(Driver.WaitForItem(_bookingDetailsFlightsSecHeader),
                $"The Booking Details Flights Section header is not displayed");
            Assert.True(Driver.WaitForItem(_bookingDetailsTransfersSecHeader),
                $"The Booking Details Transfers Section header is not displayed");
            Assert.True(Driver.WaitForItem(_bookingDetailsPropertySecHeader),
                $"The Booking Details Property Section header is not displayed");

            var bookingDetailsRefSecHeader = Driver.GetText(_bookingDetailsRefSecHeader);
            var bookingDetailsExcursionsSecHeader = Driver.GetText(_bookingDetailsExcursionsSecHeader);
            var bookingDetailsQuickLinksSecHeader = Driver.GetText(_bookingDetailsQuickLinksSecHeader);
            var bookingDetailsFlightsSecHeader = Driver.GetText(_bookingDetailsFlightsSecHeader);
            var bookingDetailsTransfersSecHeader = Driver.GetText(_bookingDetailsTransfersSecHeader);
            var bookingDetailsPropertySecHeader = Driver.GetText(_bookingDetailsPropertySecHeader);

            Assert.AreEqual(bookingRefSecHeader, bookingDetailsRefSecHeader,
                $"The Booking Details Booking Reference Section header is incorrect");
            Assert.AreEqual(excursionsSecHeader, bookingDetailsExcursionsSecHeader,
                $"The Booking Details Excursions Section header is incorrect");
            Assert.AreEqual(quicklinksSecHeader, bookingDetailsQuickLinksSecHeader,
                $"The Booking Details Quick links Section header is incorrect");
            Assert.AreEqual(flightSecHeader, bookingDetailsFlightsSecHeader,
                $"The Booking Details Flights Section header is incorrect");
            Assert.AreEqual(transfersSecHeader, bookingDetailsTransfersSecHeader,
                $"The Booking Details Transfers Section header is incorrect");
            Assert.AreEqual(propertySecHeader, bookingDetailsPropertySecHeader,
                $"The Booking Details Property Section header is incorrect");
        }

        internal void VerifyBookingDetailsPageTitle(string bookingDetailsTitle)
        {
            Assert.True(Driver.WaitForItem(_bookingDetailsTitle), $"The Booking Details Title is not displayed");

            var bookingDetailsPageTitle = Driver.GetText(_bookingDetailsTitle);
            Assert.AreEqual(bookingDetailsTitle, bookingDetailsPageTitle, $"The Booking Details Title is incorrect");
        }


        internal void VerifyPropertyDetailsExpanded(Table table)
        {
            var tableElement = Driver.FindElement(By.CssSelector(".ui-dialog table"));
            var rowNumberFlag = 1;
            foreach (var row in table.Rows)
            {
                var actualRows = tableElement.FindElements(By.CssSelector(".ui-dialog table tr"));
                var actualResult = actualRows[rowNumberFlag].FindElements(By.CssSelector("td"));
                Assert.True(actualResult[1].Text == row["Room Number"],
                    $"The Room Number should be {row["Room Number"]} instead of {actualResult[1].Text}");
                Assert.True(actualResult[2].Text == row["Room Type"],
                    $"The Room Type should be {row["Room Type"]} instead of {actualResult[2].Text}");
                Assert.True(actualResult[3].Text == row["Board Type"],
                    $"The Board Type should be {row["Board Type"]} instead of {actualResult[3].Text}");
                Assert.True(actualResult[4].Text == row["Room Reference"],
                    $"The |Room Reference should be {row["Room Reference"]} instead of {actualResult[4].Text}");
                rowNumberFlag++;
            }
        }

        internal void VerifyCustomerOverviewDetails(Table table)
        {
            Assert.True(Driver.WaitForItem(_bookingDetailsCustomerSection),
                $"The Booking Details Customer Section is not displayed");
            Assert.True(Driver.WaitForItem(_customerOverviewDetailsTable),
                $"The Booking Details Customer Overview Details table is not displayed");
            var tableElement = Driver.FindElement(_customerOverviewDetailsTable);
            var rowNumberFlag = 0;

            foreach (var row in table.Rows)
            {
                var actualRows = tableElement.FindElements(By.CssSelector("tr"));
                var actualResult = Driver.GetText(actualRows[rowNumberFlag]);

                var dob = DateTime.ParseExact(row["DOB"], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                var now = DateTime.Now;
                var age = now.Year - dob.Year;
                if (now.Month < dob.Month || (now.Month == dob.Month && now.Day < dob.Day))
                    age--;
                var expectedResult = string.IsNullOrWhiteSpace(row["Telephone"])
                    ? $"{row["Customer"]} {row["DOB"]}({age})\r\nui-btn"
                    : $"{row["Customer"]} {row["DOB"]}({age}) {row["Telephone"]}\r\nui-btn";
                Assert.AreEqual(expectedResult, actualResult, "The customer details row is not as expected.");

                //check the more button
                var tableData = actualRows[rowNumberFlag].FindElements(By.CssSelector("td"));
                var btnMore = tableData[tableData.Count - 1].FindElement(By.TagName("button"));
                Assert.IsTrue(Driver.WaitUntilClickable(btnMore),
                    "The more button is not present on the customer overview details table.");


                rowNumberFlag++;
            }
        }

        internal void ClickClosePropertyDetailsSection()
        {
            Assert.True(Driver.WaitForItem(_closePropertyDetailsSection),
                $"The close property details section icon is not displayed");
            Driver.ClickItem(_closePropertyDetailsSection);
        }

        internal void VerifyFlightOverviewSection()
        {
            Assert.True(Driver.WaitForItem(_bookingDetailsFlightsSection),
                $"The Booking Details Flights Section is not displayed");
        }

        internal void VerifyFlightsSectionHeader(string flightSecHeader)
        {
            Assert.True(Driver.WaitForItem(_bookingDetailsFlightsSecHeader),
                $"The Booking Details Flights Section header is not displayed");
            var bookingDetailsFlightsSecHeader = Driver.GetText(_bookingDetailsFlightsSecHeader);
            Assert.AreEqual(flightSecHeader, bookingDetailsFlightsSecHeader,
                $"The Booking Details Flights Section header is incorrect");
        }

        internal void VerifyOutboundReturnHeaders(string outboundHeader, string returnHeader)
        {
            Assert.True(Driver.WaitForItem(_bookingDetailsFlightsOutboundHeader),
                $"The Booking Details Flights Outbound header is not displayed");
            Assert.True(Driver.WaitForItem(_bookingDetailsFlightsReturnHeader),
                $"The Booking Details Flights Return header is not displayed");

            var bookingDetailsFlightsOutboundHeader = Driver.GetText(_bookingDetailsFlightsOutboundHeader);
            var bookingDetailsFlightsReturnHeader = Driver.GetText(_bookingDetailsFlightsReturnHeader);
            Assert.AreEqual(outboundHeader, bookingDetailsFlightsOutboundHeader,
                $"The Booking Details Flights Outbound header is incorrect");
            Assert.AreEqual(returnHeader, bookingDetailsFlightsReturnHeader,
                $"The Booking Details Flights Return header is incorrect");
        }

        internal void VerifyOutboundFlightDetails(string outboundPnr, string ukAirport, string departDateTime,
            string arrivalAirport, string outboundFlightNumber, string arrivalDateTime)
        {
            var bookingDetailsFlightsOutboundPnr = Driver.GetText(_bookingDetailsFlightsOutboundPnr, "PNR:");
            var bookingDetailsFlightsOutboundUkAirport = Driver.GetText(_bookingDetailsFlightsOutboundUkAirport);
            var bookingDetailsFlightsDepartDateTime = Driver.GetText(_bookingDetailsFlightsOutboundDepartDateTime);
            var bookingDetailsFlightsOutboundArrivalAirport =
                Driver.GetText(_bookingDetailsFlightsOutboundArrivalAirport);
            var bookingDetailsFlightsOutboundFlightNumber = Driver.GetText(_bookingDetailsFlightsOutboundFlightNumber);
            var bookingDetailsFlightsArrivalDateTime = Driver.GetText(_bookingDetailsFlightsOutboundArrivalDateTime);


            Assert.AreEqual(outboundPnr, bookingDetailsFlightsOutboundPnr,
                $"The Booking Details Flights Outbound PNR is incorrect");
            Assert.AreEqual(ukAirport, bookingDetailsFlightsOutboundUkAirport,
                $"The Booking Details Flights Outbound UK Airport is incorrect");
            Assert.AreEqual(departDateTime, bookingDetailsFlightsDepartDateTime,
                $"The Booking Details Flights Outbound Departure Date and Time is incorrect");
            Assert.AreEqual(arrivalAirport, bookingDetailsFlightsOutboundArrivalAirport,
                $"The Booking Details Flights Outbound Arrival Airport is incorrect");
            Assert.AreEqual(outboundFlightNumber, bookingDetailsFlightsOutboundFlightNumber,
                $"The Booking Details Flights Outbound Flight Number is incorrect");
            Assert.AreEqual(arrivalDateTime, bookingDetailsFlightsArrivalDateTime,
                $"The Booking Details Flights Outbound Arrival Date and Time is incorrect");
        }

        internal void VerifyReturnFlightDetails(string returnPnr, string ukAirport, string departDateTime,
            string arrivalAirport, string returnFlightNumber, string arrivalDateTime)
        {
            Driver.WaitUntilTextPresent(_bookingDetailsFlightsReturnPnr, 5, 5);
            Driver.WaitUntilTextPresent(_bookingDetailsFlightsReturnUkAirport);
            Driver.WaitUntilTextPresent(_bookingDetailsFlightsReturnDepartDateTime);
            Driver.WaitUntilTextPresent(_bookingDetailsFlightsReturnArrivalAirport);
            Driver.WaitUntilTextPresent(_bookingDetailsFlightsReturnFlightNumber);
            Driver.WaitUntilTextPresent(_bookingDetailsFlightsReturnArrivalDateTime);

            var bookingDetailsFlightsReturnPnr = Driver.GetText(_bookingDetailsFlightsReturnPnr);
            var bookingDetailsFlightsReturnUkAirport = Driver.GetText(_bookingDetailsFlightsReturnUkAirport);
            var bookingDetailsFlightsReturnDepartDateTime = Driver.GetText(_bookingDetailsFlightsReturnDepartDateTime);
            var bookingDetailsFlightsReturnArrivalAirport = Driver.GetText(_bookingDetailsFlightsReturnArrivalAirport);
            var bookingDetailsFlightsReturnFlightNumber = Driver.GetText(_bookingDetailsFlightsReturnFlightNumber);
            var bookingDetailsFlightsReturnArrivalDateTime =
                Driver.GetText(_bookingDetailsFlightsReturnArrivalDateTime);

            Assert.AreEqual(returnPnr, bookingDetailsFlightsReturnPnr,
                $"The Booking Details Flights Return PNR is incorrect");
            Assert.AreEqual(ukAirport, bookingDetailsFlightsReturnUkAirport,
                $"The Booking Details Flights Return UK Airport is incorrect");
            Assert.AreEqual(departDateTime, bookingDetailsFlightsReturnDepartDateTime,
                $"The Booking Details Flights Return Departure Date and Time is incorrect");
            Assert.AreEqual(arrivalAirport, bookingDetailsFlightsReturnArrivalAirport,
                $"The Booking Details Flights Return Arrival Airport is incorrect");
            Assert.AreEqual(returnFlightNumber, bookingDetailsFlightsReturnFlightNumber,
                $"The Booking Details Flights Return Flight Number is incorrect");
            Assert.AreEqual(arrivalDateTime, bookingDetailsFlightsReturnArrivalDateTime,
                $"The Booking Details Flights Return Arrival Date and Time is incorrect");
        }

        public void VerifyOutboundAirplaneIcon()
        {
            Assert.True(Driver.WaitForItem(_bookingDetailsFlightsOutboundFlightNumber),
                $"The Booking Details Flights Outbound Flight Number is not displayed");
            Assert.True(Driver.WaitForItem(_bookingDetailsFlightsOutboundPlaneIcon),
                $"The Booking Details Flights Outbound Airport Icon is not displayed");
        }

        public void VerifyBdOutboundFlightMoreIcon()
        {
            Assert.True(Driver.WaitForItem(_bookingDetailsFlightsOutboundPnr),
                $"The Booking Details Flights Outbound PNR is not displayed");
            Assert.True(Driver.WaitForItem(_bookingDetailsFlightsOutboundMoreIcon),
                $"The Booking Details Flights Outbound More Icon is not displayed");
        }

        public void VerifyReturnAirplaneIcon()
        {
            Assert.True(Driver.WaitForItem(_bookingDetailsFlightsReturnFlightNumber),
                $"The Booking Details Flights Return Flight Number is not displayed");
            Assert.True(Driver.WaitForItem(_bookingDetailsFlightsReturnPlaneIcon),
                $"The Booking Details Flights Return Airport Icon is not displayed");
        }

        public void VerifyBdReturnFlightMoreIcon()
        {
            Assert.True(Driver.WaitForItem(_bookingDetailsFlightsReturnPnr),
                $"The Booking Details Flights Return PNR is not displayed");
            Assert.True(Driver.WaitForItem(_bookingDetailsFlightsReturnMoreIcon),
                $"The Booking Details Flights Return More Icon is not displayed");
        }

        internal void VerifyPnrDetailsSectionDisplayed()
        {
            Assert.True(Driver.WaitForItem(_bookingDetailsPnrDetailsSection),
                $"The Outbound PNR details section is not displayed");
        }

        internal void VerifyPnrDetailsSectionHeader(string pnrHeader)
        {
            Assert.True(Driver.WaitForItem(_bookingDetailsPnrDetailsSectionHeader),
                $"The PNR details section header is not displayed");
            var bookingDetailsPnrDetailsSectionHeader = Driver.GetText(_bookingDetailsPnrDetailsSectionHeader);


            var pnrDetailsFullTitle = bookingDetailsPnrDetailsSectionHeader;
            var pnrDetailsTitle = pnrDetailsFullTitle.Split(' ');
            var sectionHeader = pnrDetailsTitle[0] + " " + pnrDetailsTitle[1] + " " + pnrDetailsTitle[2];

            Assert.AreEqual(pnrHeader, sectionHeader, $"The PNR details section title is incorrect");
        }

        internal void VerifyPnrDetailsSectionAirports(string ukAirport, string arrivalAirport)
        {
            Assert.True(Driver.WaitForItem(_bookingDetailsPnrDetailsSectionHeader),
                $"The PNR details section header is not displayed");

            var bookingDetailsPnrDetailsSectionHeader = Driver.GetText(_bookingDetailsPnrDetailsSectionHeader);
            var pnrDetailsFullTitle = bookingDetailsPnrDetailsSectionHeader;
            var flightDetails = pnrDetailsFullTitle.Split(' ');
            var pnrUkAirport = flightDetails[3].Trim();
            var pnrArrivalAirport = flightDetails[4].Trim();

            Assert.AreEqual(ukAirport, pnrUkAirport, $"The UK Airport is incorrect");
            Assert.AreEqual(arrivalAirport, pnrArrivalAirport, $"The Arrival Airport is incorrect");
        }

        internal void VerifyPnrDetailsLeadCustomer(string leadCustomer)
        {
            Assert.True(Driver.WaitForItem(_bookingDetailsPnrDetailsLeadCustomer),
                $"The PNR details section lead customer is not displayed");

            var bookingDetailsPnrDetailsLeadCustomer = Driver.GetText(_bookingDetailsPnrDetailsLeadCustomer);
            var pnrLeadCustomer = bookingDetailsPnrDetailsLeadCustomer;

            Assert.AreEqual(leadCustomer, pnrLeadCustomer, $"The PNR details section lead customer is incorrect");
            Assert.IsTrue(Driver.IsElementBold(_bookingDetailsPnrDetailsLeadCustomer),
                "The lead customer name is not bold");
        }

        internal void VerifyPnrDetailsLeadPnr(string leadPnr)
        {
            Assert.True(Driver.WaitForItem(_bookingDetailsPnrDetailsLeadPnr),
                $"The Outbound PNR details section lead PNR is not displayed");
            var bookingDetailsPnrDetailsLeadPnr = Driver.GetText(_bookingDetailsPnrDetailsLeadPnr);
            var PnrLeadCustomer = bookingDetailsPnrDetailsLeadPnr;

            Assert.AreEqual(leadPnr, "PNR: " + PnrLeadCustomer, $"The PNR details section lead PNR is incorrect");
        }

        internal void ClickBdOutboundFlightsMoreIcon()
        {
            Assert.True(Driver.WaitForItem(_bookingDetailsFlightsOutboundMoreIcon),
                $"The Flights outbound more icon is not displayed");
            Driver.ClickItem(_bookingDetailsFlightsOutboundMoreIcon);
        }

        internal void VerifyPnrDetailsPassengers(Table table)
        {
            Assert.True(Driver.WaitForItem(_bookingDetailsPnrDetailsSection),
                $"The Outbound PNR details section is not displayed");
            var i = 0;

            foreach (var row in table.Rows)
            {
                var actualPassengerName = Driver.FindElements(_bookingDetailsPnrDetailsPassName);
                var actualPassengerPnr = Driver.FindElements(_bookingDetailsPnrDetailsPassPnr);

                Assert.AreEqual(row["Passenger Name"], Driver.GetText(actualPassengerName[i + 1]),
                    $"The Passenger Name should be {row["Passenger Name"]} instead of {actualPassengerName[i].Text}.");
                Assert.AreEqual(row["Passenger PNR"], Driver.GetText(actualPassengerPnr[i + 1]),
                    $"The Passenger Name should be {row["Passenger PNR"]} instead of {actualPassengerPnr[i].Text}.");
                i++;
            }
        }

        internal void ClickClosePnrDetailsSection()
        {
            Assert.True(Driver.WaitForItem(_bookingdetailsclosePnrDetailsSection),
                $"The close outbound PNR details section icon is not displayed");
            Driver.ClickItem(_bookingdetailsclosePnrDetailsSection);
        }

        internal void ClickBdReturnFlightsMoreIcon()
        {
            Assert.True(Driver.WaitForItem(_bookingDetailsFlightsReturnMoreIcon),
                $"The Flights return more icon is not displayed");
            Driver.ClickItem(_bookingDetailsFlightsReturnMoreIcon);
        }


        public void VerifyExcursionsSectionDisplayed()
        {
            Assert.True(Driver.WaitForItem(_bookingDetailsExcursionsSection),
                $"The Booking Details Excursions Section is not displayed");
        }

        public void VerifyExcursionsHeader(string headerName)
        {
            var bookingDetailsExcursionsSecHeader = Driver.GetText(_bookingDetailsExcursionsSecHeader);
            Assert.AreEqual(headerName, bookingDetailsExcursionsSecHeader,
                $"The Booking Details Excursions Section header is incorrect");
        }

        public void VerifyExcursionsDetails(Table table)
        {
            var tableElement = Driver.FindElements(_bookingDetailsExcursionsSummary);
            var rowCounter = 0; // counter to loop through different rows
            foreach (var row in table.Rows)
            {
                var date = row["Date"];
                var description = row["Description"];
                var details = row["Details"];
                Assert.True(tableElement[rowCounter].Text.Contains(date),
                    $"Unable to verify date for the excursion. Expected Date {row["Date"]}");
                Assert.True(tableElement[rowCounter].Text.Contains(description),
                    $"Unable to verify description for the excursion. Expected description {row["Description"]}");
                Assert.True(tableElement[rowCounter].Text.Contains(details),
                    $"Unable to verify Details for the excursion. Expected Details {row["Details"]}");
                rowCounter++;
            }
        }

        public void ClickExcursionsMoreIcon()
        {
            Driver.ClickItem(_bookingDetailsExcursionsIcn);
            Driver.WaitForItem(_bookingDetailsExcursionExpandedHeader);
        }

        public void VerifyExcursionDialogDisplayed()
        {
            Assert.True(Driver.WaitForItem(_bookingDetailsExcursionExpandedHeader));
        }

        public void VerifyExcursionDialogHeader(string headerName, string reference, string leadPassenger)
        {
            var headerText = Driver.GetText(_bookingDetailsExcursionExpandedHeader);
            Assert.True(headerText.Contains(headerName),
                $"Unable to verify header name {headerName} in header {headerText} for the expanded excursions ");
            Assert.True(headerText.Contains(reference),
                $"Unable to verify reference {reference} in header {headerText} for the expanded excursions ");
            Assert.True(headerText.Contains(leadPassenger),
                $"Unable to verify lead passenger {leadPassenger} in header {headerText} for the expanded excursions ");
        }

        internal void VerifyExcursionsDetailsExpanded(Table table)
        {
            var tableElement = Driver.FindElement(By.CssSelector(".ui-dialog table"));
            var rowNumberFlag = 1;
            foreach (var row in table.Rows)
            {
                var actualRows = tableElement.FindElements(By.CssSelector(".ui-dialog table tr"));
                var actualResult = actualRows[rowNumberFlag].FindElements(By.CssSelector("td"));
                Assert.True(actualResult[0].Text == row["Start date"],
                    $"The Start date should be {row["Start date"]} instead of {actualResult[0].Text}");
                Assert.True(actualResult[1].Text == row["Description"],
                    $"The Description should be {row["Description"]} instead of {actualResult[1].Text}");
                Assert.True(actualResult[2].Text == row["Status"],
                    $"The Status should be {row["Status"]} instead of {actualResult[2].Text}");
                Assert.True(actualResult[3].Text == row["Excursion Ref"],
                    $"The |Excursion Reference should be {row["Excursion Ref"]} instead of {actualResult[3].Text}");
                Assert.True(actualResult[4].Text == row["Supplier"],
                    $"The Supplier should be {row["Supplier"]} instead of {actualResult[4].Text}");
                Assert.True(actualResult[5].Text == row["Booked Date"],
                    $"The Booked Date should be {row["Booked Date"]} instead of {actualResult[5].Text}");
                Assert.True(actualResult[6].Text == row["Details"],
                    $"The Details should be {row["Details"]} instead of {actualResult[6].Text}");
                rowNumberFlag++;
            }
        }

        internal void CloseExcursionSummaryExpandView()
        {
            Driver.ClickItem(_bookingDetailsExcursionExpandedClose);
        }

        internal void VerifyContactDetailsAreDisplayed(string header, Table table)
        {
            //Check the table and section are displayed
            Assert.True(Driver.WaitForItem(_bookingDetailsCustomerSection),
                $"The Booking Details Customer Section is not displayed");
            Assert.True(Driver.WaitForItem(_customerOverviewDetailsTable),
                $"The Booking Details Customer Overview Details table is not displayed");

            //Find the table
            var tableElement = Driver.FindElement(_customerOverviewDetailsTable);

            var expectedHeaders = table.Header.ToList();
            var i = 0;

            foreach (var row in table.Rows)
            {
                //Find the rows
                var actualRows = tableElement.FindElements(By.CssSelector("tr"));

                //Find the data
                var tableData = actualRows[i].FindElements(By.CssSelector("td"));

                //Click the more button to open contact details dialog
                var btnMore = tableData[tableData.Count - 1].FindElement(By.TagName("button"));
                Assert.IsTrue(Driver.WaitUntilClickable(btnMore),
                    "The more button is not present on the customer overview details table.");
                Driver.ClickItem(btnMore);

                //Verify the data on the contact details dialog
                Assert.AreEqual($"{header} - {row["Customer"]}", Driver.GetText(_contactDetailsHeader),
                    "The header and customer name is not correct.");
                //TODO WAITING ON BUG 466792
                //Assert.IsTrue(Driver.IsElementBold(_contactDetailsHeader), "The Contact Details Header is not bold.");
                Assert.AreEqual($"{expectedHeaders[1]}:", Driver.GetText(_primaryHeader),
                    $"The primary label is not as expected for {row["Customer"]}. ");
                Assert.AreEqual(row["Primary"], Driver.GetText(_primaryNumber),
                    $"The primary number is not as expected.");
                Assert.AreEqual($"{expectedHeaders[2]}:", Driver.GetText(_alternativeHeader),
                    $"The alternative label is not as expected for {row["Customer"]}.");
                Assert.AreEqual(row["Alternative"], Driver.GetText(_alternativeNumber),
                    $"The alternative number is not as expected for {row["Customer"]}.");
                Assert.AreEqual($"{expectedHeaders[3]}:", Driver.GetText(_emergencyHeader),
                    $"The emergency label is not as expected for {row["Customer"]}.");
                Assert.AreEqual(row["Emergency"], Driver.GetText(_emergencyNumber),
                    $"The emergency number is not as expected for {row["Customer"]}.");
                Assert.AreEqual($"{expectedHeaders[4]}:", Driver.GetText(_emailHeader),
                    $"The email label is not as expected for {row["Customer"]}.");
                Assert.AreEqual(row["Email"], Driver.GetText(_emailAddress),
                    $"The email address is not as expected for {row["Customer"]}.");

                //Close contact Details dialog
                Driver.ClickItem(_closeContactDetailsDialog);
                i++;
            }
        }

        internal void CloseBookingDetailsPage()
        {
            Driver.ClickItem(_bookingDetailsClosePage);
        }

        public void VerifyBoardType(string boardType)
        {
            Assert.True(Driver.WaitForItem(_bookingDetailsBoardType), $"The Board Type is not displayed");
            Assert.True(Driver.IsElementPresent(_bookingDetailsBoardType), $"The Board Type is not displayed");
            Assert.True(Driver.WaitUntilTextPresent(_bookingDetailsBoardType), $"The Board Type is not displayed");
            var actualBoardTypeListTexts = Driver.GetTexts(_bookingDetailsBoardType);

            CollectionAssert.Contains(actualBoardTypeListTexts, boardType, "The board type is wrong.");
        }

        public void VerifyRoomType(string roomType)
        {
            var actualRoomTypeListTexts = Driver.GetTexts(_bookingDetailsRoomType);
            CollectionAssert.Contains(actualRoomTypeListTexts, roomType, "The room type is wrong.");
        }
    }
}