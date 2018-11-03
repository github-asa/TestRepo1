using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using J2BIOverseasOps.Extensions;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using static System.StringComparison;
using static J2BIOverseasOps.Models.CaseOverviewApi;

namespace J2BIOverseasOps.Pages.Customer_Interaction
{
    internal class FindCustomerPage : SearchResultTable
    {

        private int _linkedCasesIndex;
        //Advanced panel
        private readonly string _advPanelFromDate = "//*[@id='panel-advanced']//p-calendar[@arrivalfromdate]";
        private readonly string _advPanelFromDrpDwn = "//*[@id='panel-advanced']//p-dropdown[@arrivalfromairport]";
        private readonly string _advPanelReturnToDrpDwn = "//*[@id='panel-advanced']//p-dropdown[@returntoairport]";
        private readonly string _advPanelToDate = "//*[@id='panel-advanced']//p-calendar[@returndateto]";
        private readonly By _advancedContentLabels = By.CssSelector("#panel-advanced label");
        private readonly By _advancedFlightInput = By.CssSelector("[flightnumber]");
        private readonly By _advancedPanelExpandBtn = By.CssSelector("#panel-advanced span.ui-accordion-toggle-icon.pi.pi-fw.pi-caret-right");
        private readonly By _advancedPanelMinimizeBtn = By.CssSelector("#panel-advanced span.ui-accordion-toggle-icon.pi.pi-fw.pi-caret-down");
        private readonly By _arrivalFromDate = By.XPath("//*[@arrivalfromdate]");
        private readonly By _arrivalFromDropDown = By.XPath("//p-dropdown[@arrivalfromairport]");
        private readonly By _boardTypeDropDown = By.CssSelector("p-dropdown[boardtype]");
        private readonly By _boardTypeDrpDwnLbl = By.CssSelector("p-dropdown[boardtype] label");
        private readonly By _bookingRef = By.XPath("//app-search-criteria//*[@reference]");
        private readonly By _calendarDateArrFrmPicker = By.XPath("//p-calendar[@arrivalfromdate]//table//tr");
        private readonly By _calendarDatePicker = By.XPath("//p-calendar//table//tr");
        private readonly By _calendarDateRetToPicker = By.XPath("//p-calendar[@returndateto]//table//tr");
        private readonly By _calendarDays = By.CssSelector("td:not(.ui-state-disabled) > a.ui-state-default");
        private readonly By _calendarMonth = By.CssSelector("span.ui-datepicker-month");
        private readonly By _calendarWidgetArrFrmBtn = By.XPath("//p-calendar[@arrivalfromdate]//button");
        private readonly By _calendarWidgetBtn = By.XPath("//p-calendar//button");
        private readonly By _calendarWidgetRetToBtn = By.XPath("//p-calendar[@returndateto]//button");
        private readonly By _calendarYear = By.CssSelector("span.ui-datepicker-year");
        private readonly By _returnToDate = By.XPath("//*[@returndateto]");
        private readonly By _returnToDropDown = By.XPath("//p-dropdown[@returntoairport]");
        private readonly By _rightCalendarArrow = By.CssSelector("a.ui-datepicker-next.ui-corner-all");
        private readonly By _roomTypeDrpDwn = By.CssSelector("p-multiselect[roomtypes]");
        private readonly By _roomTypeDrpDwnLbl = By.CssSelector("[roomTypes] [title=Choose] label");
        private readonly By _roomTypesListBox = By.CssSelector("p-multiselect[roomtypes]");
        private readonly By _transferTypeDrpDwn = By.XPath("//p-dropdown[@transfertype]");
        private readonly By _casesLinkedDialog = By.CssSelector("p-dialog[modal] > div");
        private readonly By _bookingReferencesLinks = By.CssSelector("[resultbookingref]");
        private By LinkedCaseIds => By.CssSelector("[id^=casesLinkedToBooking-dialog-caseid-val-]");
        private By LinkedCaseId => By.CssSelector($"[id=casesLinkedToBooking-dialog-caseid-val-{_linkedCasesIndex}]");
        private By LinkedCaseCategories => By.CssSelector($"[id^=casesLinkedToBooking-dialog-{_linkedCasesIndex}-category-val-]");
        private By LinkedCaseDateCreated => By.CssSelector($"[id=casesLinkedToBooking-dialog-date-created-val-{_linkedCasesIndex}]");
        private By LinkedCaseStatus => By.CssSelector($"[id=casesLinkedToBooking-dialog-status-{_linkedCasesIndex}]");
        private By LinkedCaseStatuses => By.CssSelector("p[id^=casesLinkedToBooking-dialog-status-]");
        private By LinkedCaseIntitialSummary => By.CssSelector($"[id=casesLinkedToBooking-dialog-initial-summary-val-{_linkedCasesIndex}]");
        private By LinkedCaseCloseButton => By.Id("casesLinkedToBooking-dialog-close-btn");

        //Checkboxes
        private readonly By _checkboxFilterByMyProperties = By.CssSelector("p-checkbox[name=chkFilterByMyProperties] div:nth-child(2)");
        private const string CheckBoxTicked = "ui-chkbox-icon ui-clickable pi pi-check";

        //Inputs
        private readonly By _customerNameInput = By.XPath("//app-search-criteria//*[@customername]");

        //Accordions
        private readonly By _advancedAccordion = By.CssSelector("[advancepanel]");
        private readonly By _advancedAccordionArrow = By.CssSelector("[advancepanel] span.ui-accordion-toggle-icon");

        //Multiselects
        private readonly By _destinationsMultiselect = By.CssSelector("[destinationslist]");
        private readonly By _resortsMultiselect = By.CssSelector("[resortslist]");
        private readonly By _propertiesMultiselect = By.CssSelector("[propertieslist]");

        //Calendar/date picker
        private readonly By _leftCalendarArrow = By.CssSelector("a.ui-datepicker-prev.ui-corner-all");
        private readonly By _dateCalender = By.XPath("//p-calendar");

        //Buttons
        private readonly By _resetButton = By.CssSelector("button[label=Reset]");
        private readonly By _buttonContinue = By.CssSelector("button[label='Continue']");
        private readonly By _searchButton = By.XPath("//span[@class='ui-button-text ui-clickable'][contains(text(),'Search')]");

        //P-Select button
        private readonly By _inResortPSelectButton = By.CssSelector("p-selectbutton");

        //Affected bookings panel
        private readonly By _affectedBookings = By.CssSelector("tr[id*=booking] td:nth-child(1)");
        private readonly By _noBookingsSelectedMessage = By.CssSelector("#no-bookings");
      
        //Labels
        private readonly By _lblMyProperties = By.CssSelector("#select-subject p-checkbox > label");
        private readonly string _myProperties = "#select-subject p-checkbox";

        public FindCustomerPage(IWebDriver driver, ILog log) : base(driver, log)
        {
        }

        public void EnterCustomerName(string customerName)
        {
            Assert.True(Driver.WaitForItem(_customerNameInput), "The Customer Name input field is not displayed");
            Driver.EnterText(_customerNameInput, customerName);
        }


        public void EnterBookingRef(string bookingRef)
        {
            Assert.True(Driver.WaitForItem(_bookingRef), "The Booking Reference input field is not displayed");
            Driver.EnterText(_bookingRef, bookingRef);
        }

        public void ClickSearchButton()
        {
            Assert.True(Driver.WaitForItem(_searchButton), "The Search button is not displayed");
            Driver.ClickItem(_searchButton);
        }

        public void FilterByDestination(string destination)
        {
            var destinationToSelectList = destination.ConvertStringIntoList();
            Driver.SelectMultiselectOption(_destinationsMultiselect, destinationToSelectList);
        }

        public void FilterByResorts(string destination, string resort)
        {
            var destinationsToSelectList = destination.ConvertStringIntoList();
            Driver.SelectMultiselectOption(_destinationsMultiselect, destinationsToSelectList);
            var resortToSelectList = resort.ConvertStringIntoList();
            Driver.SelectMultiselectOption(_resortsMultiselect, resortToSelectList);
        }

        public void FilterByProperties(string destination, string resort, string properties)
        {
            var destinationsToSelectList = destination.ConvertStringIntoList();
            Driver.SelectMultiselectOption(_destinationsMultiselect, destinationsToSelectList);
            var resortToSelectList = resort.ConvertStringIntoList();
            Driver.SelectMultiselectOption(_resortsMultiselect, resortToSelectList);
            var propertiesToSelectList = properties.ConvertStringIntoList();
            Driver.SelectMultiselectOption(_propertiesMultiselect, propertiesToSelectList);
        }

        public void VerifySearchCriteria(string destination, string resort, string properties)
        {
            VerifySelectedProperties(properties);
            VerifySelectedDestinations(destination);
            VerifySelectedResorts(resort);
        }

        public void VerifyFilterDisplayed(string status, string filterName)
        {
            switch (status)
            {
                case "can":
                    var lblMyProperties = Driver.GetText(_lblMyProperties);
                    Assert.AreEqual(filterName, lblMyProperties, "The My Properties filter label is not correct");
                    break;
                case "can not":
                    Assert.True(!Driver.IsElementPresent(By.CssSelector(_myProperties)));
                    break;
            }
        }

        public void VerifyDefaultDestination(Table table)
        {
            var row = table.Rows[0];
            var defaultDestination = row["destinations"];
            VerifyAvialableDestinations(defaultDestination);
        }

        public void VerifySelectedResortToggle(string expectedToggle)
        {
            var actualToggle = Driver.GetSelectedPOption(_inResortPSelectButton);
            Assert.AreEqual(expectedToggle, actualToggle, "The expected In Resort button option is not selected");
        }

        public void ClickInResortSwitch(string option)
        {
            Driver.ClickPSelectOption(_inResortPSelectButton, option);
        }

        public void VerifyDateCalenderDisplayed()
        {
            Assert.True(Driver.WaitForItem(_dateCalender, 2));
        }

        public void ClickCalendarWidgetBtn()
        {
            Driver.ClickItem(_calendarWidgetBtn);
        }

        public void VerifyDatePickerDisplayed()
        {
            Driver.WaitForItem(_calendarDatePicker);
            Assert.True(Driver.FindElements(_calendarDatePicker).Count > 0);
        }

        public void VerifyDatePickerNotDisplayed()
        {
            Driver.WaitUntilElementNotDisplayed(_calendarDatePicker);
        }

        internal void ClickResetButton()
        {
            Driver.ClickItem(_resetButton);
        }

        internal void ClickResortDateFrom(string resortFromDay, string resortFromMonthYear)
        {
            SelectDateFromCalender(resortFromDay, resortFromMonthYear);
        }

        internal void ClickResortDateTo(string resortToDay, string resortFromToYear)
        {
            SelectDateFromCalender(resortToDay, resortFromToYear, true);
        }

        public void NoBookingsAffectedBtn()
        {
            Assert.True(Driver.WaitForItem(By.CssSelector("#no-bookings-affected-button")),
                $"The No Bookings Affected button is not displayed");
            Driver.ClickItem(By.CssSelector("#no-bookings-affected-button"));
        }

        public void VerifyNoBookingsAffectedBtnActive()
        {
            Assert.True(Driver.WaitForItem(By.CssSelector("#no-bookings-affected-button > span")),
                $"The No Bookings Affected button is not displayed");
            Assert.True(Driver.WaitUntilClickable(By.CssSelector("#no-bookings-affected-button > span")),
                $"The No Bookings Affected button is not clickable");
        }

        public void VerifySelectedValueInBoardType(string expectedValue)
        {
            var actualValue = Driver.GetText(_boardTypeDrpDwnLbl);
            Assert.AreEqual(expectedValue, actualValue,
                "Expected board type filter value :" + expectedValue + " was not equal to actual filter value :" +
                actualValue);
        }

        public void VerifyBoardTypeDropDownDisplayed()
        {
            var boardTypeDropDownDisplayed = Driver.FindElement(_boardTypeDropDown).Displayed;
            Assert.True(boardTypeDropDownDisplayed, "The Board Type drop down is not visible.");
        }

        public void VerifyBoardTypeDropDownSelection(string expectedValue)
        {
            var actualValue = Driver.GetText(_boardTypeDrpDwnLbl, "Choose");
            Assert.AreEqual(expectedValue, actualValue,
                "Expected board type filter value :" + expectedValue + " was not equal to actual filter value :" +
                actualValue);
        }

        public void VerifyRoomTypeDropDownDisplayed()
        {
            var roomTypeDropDownDisplayed = Driver.FindElement(_roomTypeDrpDwn).Displayed;
            Assert.True(roomTypeDropDownDisplayed, "The Room Type drop down is not visible.");
        }

        public void VerifyRoomTypeDropDownDefaultSelection(string expectedValue)
        {
            var actualValue = Driver.GetText(_roomTypeDrpDwnLbl);
            Assert.AreEqual(expectedValue, actualValue,
                "Expected room type filter value :" + expectedValue + " was not equal to actual filter value :" +
                actualValue);
        }

        public void VerifyRoomTypeDropDownSelection(string roomTypes)
        {
            var roomTypesList = roomTypes.ConvertStringIntoList();
            var selectedOptions = Driver.GetAllSelectedMultiselectOptions(_roomTypeDrpDwn);
            Assert.AreEqual(roomTypesList, selectedOptions,
                $"The expected room types {roomTypes} are not equal to the selcted room types {selectedOptions}");
        }

        #region Helper Functions

        internal void VerifyListOfPermittedDestinations(string permittedDestinations)
        {
            var listOfDestinations = Driver.FindElements(By.CssSelector("[destinationslist] label[class*=ng-star-inserted]"));
            var listOfExpectedDestinations = permittedDestinations.ConvertStringIntoList();
            switch (permittedDestinations)
            {
                case "All":
                    Assert.True(listOfDestinations.Count > 40);
                    break;
                default:
                    Assert.True(listOfDestinations.Count == listOfExpectedDestinations.Count); // verify the list size
                    foreach (var destination in (listOfDestinations)) // verify list content
                    {
                        Assert.True(listOfExpectedDestinations.Contains(destination.Text));
                        listOfExpectedDestinations.RemoveIndexAt(destination.Text); // update verification list
                    }

                    break;
            }
        }


        internal void VerifySelectedDestinations(string destinationXpath, string expectedDestination)
        {
            var selectedDestination = By.XPath(destinationXpath + "//label[contains(.,'" + expectedDestination + "')]");
            Assert.True(Driver.WaitForItem(selectedDestination, 5));
        }

        private void SelectDateFromCalender(string dayOfThMonth, string monthYear, bool arrowForward = false)
        {
            //var calendarMonth = Driver.FindElement(_calendarMonth);
            //var calendarYear = Driver.FindElement(_calendarYear);
            var monthYearToSelect = monthYear;
            var calenderArrow = _leftCalendarArrow;
            if (arrowForward)
            {
                calenderArrow = _rightCalendarArrow;
            }

            var loopCounter = 0;
            //select the month and year via the calendar picker
            while ($"{Driver.GetText(_calendarMonth)} {Driver.GetText(_calendarYear)}" != monthYearToSelect)
            {
                Driver.ClickItem(calenderArrow);
                Driver.WaitForItem(_calendarMonth);
                Driver.WaitForItem(_calendarDays);
                loopCounter++;
                if (loopCounter == 100)
                {
                    Assert.Fail($"Could not find month after 100 iterations");
                }
            }

            //select the day from the list of days via the calendar picker
            var calendarDays = Driver.FindElements(_calendarDays);
            var dayToSelect = dayOfThMonth;

            foreach (var day in calendarDays)
            {
                if (day.Text.Equals(dayToSelect))
                {
                    day.Click();
                    break;
                }
            }
        }


        private void VerifySelectedResorts(string expectedResorts)
        {
            var actualResorts = Driver.GetAllSelectedMultiselectOptions(_resortsMultiselect);
            if (actualResorts.Count.Equals(0))
            {
                actualResorts.Add("Choose");
            }
            var expectedDestinationsList = expectedResorts.ConvertStringIntoList().OrderBy(x => x);
            CollectionAssert.AreEqual(actualResorts, expectedDestinationsList, "The selected destinations in the destinations multiselect are incorrect");
        }

        internal void VerifySelectedProperties(string expectedProperties)
        {
            var actualProperties = Driver.GetAllSelectedMultiselectOptions(_propertiesMultiselect);
            if (actualProperties.Count.Equals(0))
            {
                actualProperties.Add("Choose");
            }
            var expectedPropertiesList = expectedProperties.ConvertStringIntoList().OrderBy(x => x);
            CollectionAssert.AreEqual(actualProperties, expectedPropertiesList, "The selected properties in the properties multiselect are incorrect");
        }

        internal void VerifySelectedDestinations(string expectedDestinations)
        {
            var actualDestinations = Driver.GetAllSelectedMultiselectOptions(_destinationsMultiselect);
            if (actualDestinations.Count.Equals(0))
            {
                actualDestinations.Add("Choose");
            }
            var expectedDestinastionsList = expectedDestinations.ConvertStringIntoList().OrderBy(x => x);
            CollectionAssert.AreEqual(actualDestinations, expectedDestinastionsList, "The selected destinations in the destinations multiselect are incorrect");
        }

        internal void VerifyAvialableDestinations(string expectedDestinations)
        {
            var actualDestinations = Driver.GetAllMultiselectOptions(_destinationsMultiselect);
            var expectedDestinastionsList = expectedDestinations.ConvertStringIntoList().OrderBy(x => x);
            CollectionAssert.AreEqual(actualDestinations, expectedDestinastionsList, "The selected destinations in the destinations multiselect are incorrect");
        }

        internal void ExpandCollapseAdvancedFilter()
        {
            Assert.True(Driver.WaitForItem(_advancedAccordion), "The advanced accordion is not displayed");
            Driver.ClickItem(_advancedAccordionArrow, true);
        }


        #endregion

        #region Advanced Filters

        public void ExpandAdvancedPanel()
        {
            Driver.ClickItem(_advancedPanelExpandBtn);
            Driver.WaitForItem(_advancedPanelMinimizeBtn);
            Driver.WaitForItem(_boardTypeDropDown);
        }

        public void MinimiseAdvaPanel()
        {
            Driver.ClickItem(_advancedPanelMinimizeBtn);
            Driver.WaitForItem(_advancedPanelExpandBtn);
            Driver.WaitUntilElementNotPresent(_boardTypeDropDown);
        }

        public void VerifyAdvancedDatePanel()
        {
            Assert.True(Driver.FindElements(By.XPath(_advPanelFromDrpDwn + "//option")).Count >
                        0); // verify options are populated
            Assert.True(Driver.FindElements(By.XPath(_advPanelReturnToDrpDwn + "//option")).Count > 0);
            Driver.ClickItem(By.XPath(_advPanelFromDate + "//button"));
            Assert.True(Driver.WaitForItem(By.XPath(_advPanelFromDate + "//tr")));
            Driver.ClickItem(By.XPath(_advPanelFromDate + "//button"));
            Driver.ClickItem(By.XPath(_advPanelToDate + "//button"));
            Assert.True(Driver.WaitForItem(By.XPath(_advPanelToDate + "//tr")));
        }

        internal void VerifyFlightNumberFieldIsDisplayed(string flightLabel)
        {
            Assert.IsTrue(Driver.WaitForItem(_advancedContentLabels),
                "The labels are not dis played on the Advanced Search panel.");
            var labels = Driver.FindElements(_advancedContentLabels);
            Assert.AreEqual(flightLabel, Driver.GetText(labels[6]), "The flight label is not worded as expected.");
            Assert.IsTrue(Driver.WaitForItem(_advancedFlightInput),
                "The Flight Number text box is not displayed in Adanced search.");
        }

        internal void VerifyFlightNumberFieldIsEmpty()
        {
            Assert.IsEmpty(Driver.GetText(_advancedFlightInput), "The flight number field is not empty by default.");
        }

        internal void EnterFlightNumber(string flightNumber)
        {
            Driver.EnterText(_advancedFlightInput, flightNumber);
        }

        internal void VerifyFlightNumberFieldContains(string flightNumber)
        {
            Assert.AreEqual(flightNumber, Driver.GetInputBoxValue(_advancedFlightInput),
                "The flight number is not as expected in the flight field.");
        }

        public void VerifySelectedValueInTransferType(string expectedValue)
        {
            Driver.WaitUntilTextPresent(_transferTypeDrpDwn);
            var actualValue = Driver.FindElement(_transferTypeDrpDwn).FindElement(By.XPath(".//label")).Text;
            Assert.True(expectedValue == actualValue,
                "Expected transfer type filter value :" + expectedValue + " was not equal to actual filter value :" +
                actualValue);
        }


        public void VerifySelectedValueInArrivalFrom(string expectedValue)
        {
            Driver.WaitUntilTextPresent(_arrivalFromDropDown);
            var actualValue = Driver.FindElement(_arrivalFromDropDown).FindElement(By.XPath(".//label")).Text;
            Assert.True(expectedValue == actualValue,
                "Expected Arrival from  filter value :" + expectedValue + " was not equal to actual filter value :" +
                actualValue);
        }

        public void VerifySelectedValueInRetTo(string expectedValue)
        {
            Driver.WaitUntilTextPresent(_returnToDropDown);
            var actualValue = Driver.FindElement(_returnToDropDown).FindElement(By.XPath(".//label")).Text;
            Assert.True(expectedValue == actualValue,
                "Expected Return To filter value :" + expectedValue + " was not equal to actual filter value :" +
                actualValue);
        }

        public void SelectTransferType(string transferType)
        {
            Driver.SelectDropDownOption(_transferTypeDrpDwn, transferType);
        }

        public void SelectArrivalFrom(string arrivalFrom)
        {
            Driver.SelectDropDownOption(_arrivalFromDropDown, arrivalFrom);
        }

        public void SelectReturnTo(string returnTo)
        {
            Driver.SelectDropDownOption(_returnToDropDown, returnTo);
        }

        public void SelectBoardType(string boardType)
        {
            Driver.SelectDropDownOption(_boardTypeDropDown, boardType);
        }

        public void SelectRoomTypes(string roomType)
        {
            var roomTypesList = roomType.ConvertStringIntoList();
            Driver.SelectMultiselectOption(_roomTypesListBox, roomTypesList);
        }

        public void SelectAllRoomTypes()
        {
            Driver.SelectAllMultiselectOptions(_roomTypesListBox);
        }

        internal void SelectArrivalFromDate(string arrivalFromDay, string arrivalFromMonthYear)
        {
            var dateformat = $"{arrivalFromDay} {arrivalFromMonthYear}";
            var date = Convert.ToDateTime(dateformat);

            Driver.SelectDateFromCalender(_arrivalFromDate, date);
        }

        internal void SelectReturnToDate(string returnToDay, string returnToMonthYear)
        {
            var dateformat = $"{returnToDay} {returnToMonthYear}";
            var date = Convert.ToDateTime(dateformat);

            Driver.SelectDateFromCalender(_returnToDate, date);
        }


        public void ArrivalFromDateBlank()
        {
            Driver.WaitForItem(_arrivalFromDate);
            var arrFromElem = Driver.FindElement(_arrivalFromDate);
            Assert.True(!arrFromElem.GetAttribute("class").Contains("ui - inputwrapper - filled"),
                "Arrival from date input was not blank");
        }

        public void RetToDateBlank()
        {
            Driver.WaitForItem(_returnToDate);
            var retToElem = Driver.FindElement(_returnToDate);
            Assert.True(!retToElem.GetAttribute("class").Contains("ui - inputwrapper - filled"),
                "Return To date input was not blank");
        }

        public void ClickArrivalFromCalenderWidget()
        {
            Driver.ClickItem(_calendarWidgetArrFrmBtn);
        }

        public void VerifyArrivalFromDatePickerDisplayed()
        {
            Driver.WaitForItem(_calendarDateArrFrmPicker);
        }

        public void ClickReturnToCalenderWidget()
        {
            Driver.ClickItem(_calendarWidgetRetToBtn);
        }

        public void VerifyReturnToDatePickerDisplayed()
        {
            Driver.ClickItem(_calendarDateRetToPicker);
        }

        #endregion

        public void SelectAllDestinations()
        {
            Driver.SelectAllMultiselectOptions(_destinationsMultiselect);
        }

        public void ClickContinue()
        {
            Driver.ClickItem(_buttonContinue);
        }

        public void VerifySelectedBookings(Table table)
        {
            var bookings = table.Rows.ToColumnList("Affected Bookings");
            var i = table.Rows.ToColumnList("Affected Bookings").Count;
            WaitForSpinnerToDisappear();
            Assert.IsTrue(
                Driver.WaitUntilNumberOfElementsPresent(By.CssSelector("tr[id*=booking] td:nth-child(1)"), i));
            var selectedList = Driver.GetTexts(_affectedBookings);
            CollectionAssert.AreEqual(bookings, selectedList, "The selected affected bookings list is not as expected");
        }

        public void VerifyAllDestinationsSelected()
        {
            var allDestinations = Driver.GetAllMultiselectOptions(_destinationsMultiselect);
            var selectedDestinations = Driver.GetAllSelectedMultiselectOptions(_destinationsMultiselect);

            Assert.AreEqual(allDestinations, selectedDestinations, "All of the destinations are not selected");

        }

        public void VerifyNoAffectedBookingsSelected()
        {
            Driver.WaitForItem(_noBookingsSelectedMessage);
        }

        private int GetSelectedCategoryIndex(string booking)
        {
            var affectedBookings = Driver.GetTexts(_affectedBookings);
            var i = 0;

            foreach (var affectedBooking in affectedBookings)
            {
                if (booking.Equals(affectedBooking))
                {
                    i = affectedBookings.IndexOf(affectedBooking);
                    break;
                }
            }

            return i;
        }

        public void RemoveAffectedBooking(string booking)
        {
            var i = GetSelectedCategoryIndex(booking);
            Driver.ClickItem(By.CssSelector($"button[id*=remove-{i}]"));
        }

        public void TickUntickFilterByMyProperties(string tickBoxState)
        {
            var checkbox = Driver.FindElement(_checkboxFilterByMyProperties);
            var currentTickState = checkbox.FindElement(By.CssSelector("span"));

            switch (tickBoxState)
            {
                case "tick":

                    if (currentTickState.GetAttribute("class") != CheckBoxTicked)
                    {
                        Driver.ClickItem(_checkboxFilterByMyProperties, true);
                    }

                    break;
                case "untick":
                    if (currentTickState.GetAttribute("class") == CheckBoxTicked)
                    {
                        Driver.ClickItem(_checkboxFilterByMyProperties, true);
                    }

                    break;
                default:
                    throw new Exception(tickBoxState + " :is not a valid tickbox state ");
            }
        }

        public void VerifyCasesLinkedPopupIsDisplayed()
        {
            Assert.IsTrue(Driver.WaitForItem(_casesLinkedDialog), "The cases linked popup is not being displayed.");
        }

        public void VerifyCasesLinkedPopupContains(Table table)
        {
            var caseOverviewResponse = ScenarioContext.Current.Get<GetCaseOverviewResponse>("CaseOverviewResponse");
            var expectedCaseId = caseOverviewResponse.CaseOverview.CaseReference;
            var caseIds = Driver.GetTexts(LinkedCaseIds);
            _linkedCasesIndex = caseIds.IndexOf(expectedCaseId);
            var expectedDate = DateTime.Now.ToString("dd/MM/yyyy");
            var row = table.Rows[0];

            Assert.AreEqual(expectedCaseId, Driver.GetText(LinkedCaseId), "The case id is as expected.");
            Assert.AreEqual(expectedDate, Driver.GetText(LinkedCaseDateCreated), "The linked case date created is as expected.");
            Assert.AreEqual(row["Status"], Driver.GetText(LinkedCaseStatus), "The linked case status is as expected.");
            Assert.AreEqual(row["CaseCategories"], Driver.GetText(LinkedCaseCategories), "The linked case categories is as expected.");
            Assert.AreEqual(row["InitialSummary"], Driver.GetText(LinkedCaseIntitialSummary), "The linked case initial summary is as expected.");
        }

        public void ClickCloseButtonOnLinkedCasesPopup()
        {
            Driver.ClickItem(LinkedCaseCloseButton);
        }

        public void VerifyCasesLinkedPopupIsNotDisplayed()
        {
            Assert.IsTrue(Driver.WaitUntilElementNotDisplayed(_casesLinkedDialog), "The cases linked popup is still being displayed.");
        }

        public void AddbookingsToAffectedPage(Table table)
        {
            var row = table.Rows[0];
            var bookings = row["Booking Reference"].ConvertStringIntoList();

            foreach (var booking in bookings)
            {
                //When I type in "booking reference" as the booking reference on the find a customer page
                Assert.True(Driver.WaitForItem(_bookingRef), "The Booking Reference input field is not displayed");
                Driver.EnterText(_bookingRef, booking);
                Assert.True(Driver.WaitForItem(_searchButton), "The Search button is not displayed");
                Driver.ClickItem(_searchButton);

                //And I click the search button on the find a customer page
                var bookingReferencesList = new List<string>();
                ScenarioContext.Current["bookingReferencesList"] = bookingReferencesList;

                //Then I am presented with the search results on the search results page
                WaitForSpinnerToDisappear();
                Assert.True(Driver.WaitForItem(_bookingReferencesLinks),
                    "The booking reference links are not displayed on the search results page");

                //When I "tick" the checkbox on the booking reference "booking reference" for the multiple customer search results page
                bookingReferencesList.Add(booking);
                ScenarioContext.Current["bookingReferencesList"] = bookingReferencesList;

                //When I click the Select Button on the multiple customer search results page
                TickUntickBookingReference("tick", booking);
                CollectSearchResultsInformation();
                ClickSelectMultipleCustButton();

                //Then the '/find-multiple-customer' page is displayed
                WaitForSpinnerToDisappear();
                Driver.VerifyNavigatedToPage("/find-multiple-customer");
                WaitForSpinnerToDisappear();

                //clear the reference field
                Driver.Clear(_bookingRef);
            }
        }

        public void VerifyLinkedCasesOrderedForStatus(string status)

        {            
            var statuses = Driver.GetTexts(LinkedCaseStatuses);

            var linkedCaseDates = new List<DateTime>();

            _linkedCasesIndex = 0;
            foreach (var s in statuses)
            {
                if (s.Equals(status))
                {
                    var date = Driver.GetText(LinkedCaseDateCreated);
                    var dateParsed =  Driver.ParseDateTo_ddmmyyyy(date);

                    linkedCaseDates.Add(dateParsed);
                }
                _linkedCasesIndex++;
            }

            var expected = linkedCaseDates.OrderByDescending(x => x);

            CollectionAssert.AreEqual(expected, linkedCaseDates, "The dates are not ordered correctly.");

        }
    }
}