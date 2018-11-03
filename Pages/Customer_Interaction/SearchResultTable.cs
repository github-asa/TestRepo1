using System;
using System.Collections.Generic;
using System.Linq;
using J2BIOverseasOps.Extensions;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Pages.Customer_Interaction
{
    internal class SearchResultTable : CommonPageElements
    {
        private const string CheckBoxUnticked = "ui-chkbox-icon ui-clickable";
        private const string CheckBoxTicked = "ui-chkbox-icon ui-clickable pi pi-check";
        private readonly string _bookingPassengersTable = "//table//table";
        private readonly By _bookingReferencesLinks = By.CssSelector("[resultbookingref]");
        private readonly By _checkBoxMultipleCustomer = By.CssSelector("table p-checkbox");
        private readonly By _expandTableIconsPlus = By.CssSelector("tbody .fa.fa-fw.fa-plus");
        private readonly By _passengersCustomersTable = By.CssSelector("[customerstable] tbody td:nth-child(1)");
        private readonly By _passengersCheckboxes = By.CssSelector("[customerstable] tbody td:nth-child(3) p-checkbox");

        private readonly By _expandTableIconsMinus = By.CssSelector("tbody .fa.fa-fw.fa-minus");
        private readonly By _disabledButtonPassengerTbl = By.XPath("//*[@class='select-column']//button[@disabled]");
        private readonly By _expandedTableIconMinus = By.XPath("//*[@class='fa fa-fw fa-minus']");
        private readonly string _expandedTableIconPlus =
            "//tbody/tr[resultnumber]/td[1]//a//*[@class='fa fa-fw fa-plus']";
        private readonly By _refineSearchBtn =
            By.XPath("//span[@class='ui-button-text ui-clickable'][contains(text(),'Refine search')]");
        private readonly By _resultsFound = By.CssSelector("[severity=info] .ui-message-text");
        private readonly By _searchResultsDisplayed = By.XPath("//table/tbody/tr/td");
        private readonly By _selectBtnMultCust = By.XPath("//button[@selectbookings]");
        private readonly By _selectButton =
            By.XPath("//span[@class='ui-button-text ui-clickable'][contains(text(),'Select')]");
        private readonly By _slectBtnDisabledMultCust = By.XPath("//button[@disabled]");
        private readonly By _tableHead = By.XPath("//thead//th");
        private readonly By _flagInfo = By.CssSelector("[resultstable] i.fa-exclamation");

        //checkboxes
        private readonly By _selectAllCheckBox = By.CssSelector("p-checkbox[value*=Select] div > div:nth-child(2)");

        //messages
        private readonly By _uiMessageText = By.CssSelector("span.ui-message-text");

        //labels
        private readonly By _lblDestinations = By.CssSelector("td:nth-child(5) > span");
        private readonly By _lblResorts = By.CssSelector("td:nth-child(6) > span");
        private readonly By _lblProperties = By.CssSelector("td:nth-child(7) > span");
        




        //paginator
        private readonly By _paginatorPageNumbers = By.CssSelector(".ui-paginator-page");
        private readonly By _paginatorElement = By.CssSelector("p-paginator .ui-paginator");
        private readonly By _paginatorSkipToFirstPage = By.CssSelector(".pi.pi-step-backward");
        private readonly By _paginatorSkipToLastPage = By.CssSelector(".pi.pi-step-forward");
        private readonly By _paginatorNextPage = By.CssSelector(".ui-paginator-icon.pi.pi-caret-right");
        private readonly By _paginatorPreviousPage = By.CssSelector(".ui-paginator-icon.pi.pi-caret-left");
        private readonly By _paginatorCurrentPage = By.CssSelector(".ui-paginator-pages .ui-state-active");

        public SearchResultTable(IWebDriver driver, ILog log) : base(driver, log)
        {
        }

        public void VerifySearchResultsDisplayed()
        {
            Assert.True(Driver.WaitForItem(_bookingReferencesLinks),"The booking reference links are not displayed on the search results page");
        }

        public void VerifyNoSearchResultsDisplayed()
        {
            Assert.False(Driver.WaitForItem(_searchResultsDisplayed, 5));
        }

        public void VerifyUiMessage(string expectedText)
        {
            var actualMessage = Driver.GetText(_uiMessageText);
            Assert.True(actualMessage.Contains(expectedText), $"The expected search results message is incorrect");
        }


        public void VerifySearchResultData(Table table)
        {
            Assert.True(Driver.WaitForItem(_bookingReferencesLinks), "The booking reference links are not displayed on the search results page");
            Assert.IsTrue(Driver.WaitForItem(By.XPath("//table/tbody/tr/td")), "The search results table is not present");

            var tableElement = Driver.FindElement(By.CssSelector("app-search-results p-table table > tbody"));
            var expectedBookingRefs = table.Rows.ToColumnList("Booking Ref");
            var rowCounter = 0;

            foreach (var expectedBookingRef in expectedBookingRefs)
            {
                var i = GetBookingRefIndexMultipleSearch(expectedBookingRef);
                var row = table.Rows[rowCounter];
                var actualResult = tableElement.FindElements(By.CssSelector($"tr:nth-child({i}) > td"));

                Assert.True(actualResult[1].Text == row["Booking Ref"],
                    $"The booking ref should be {row["Booking Ref"]} instead of {actualResult[1].Text}");
                Assert.True(actualResult[2].Text == row["Lead Customer"],
                    $"The lead customer should be {row["Lead Customer"]} instead of {actualResult[2].Text}");
                Assert.True(actualResult[3].Text == row["Outbound"],
                    $"The outbound should be {row["Outbound"]} instead of {actualResult[3].Text}");
                Assert.True(actualResult[4].Text == row["Destination"],
                    $"The property ref should be {row["Property"]} instead of {actualResult[5].Text}");
                Assert.True(actualResult[5].Text == row["Resort"],
                    $"The destination should be {row["Destination"]} instead of {actualResult[4].Text}");
                Assert.True(actualResult[6].Text == row["Property"]);
                rowCounter++;
            }
        }

        internal void VerifyThereAreNoResultsDisplayed(string message)
        {
            Assert.IsTrue(Driver.WaitUntilElementNotPresent(By.CssSelector("table tr")), "There should be no results.");

            //Check Result text is correct
            Assert.AreEqual(message, Driver.GetText(_resultsFound), "The count is not as expected.");
        }

        // verify the list of destinations match with expected destination
        public void VerifyDestination(string destinationCode)
        {
            var listOfExpectedDestinations = destinationCode.ConvertStringIntoList();
            var listOfActualDestinations = By.XPath("//table//tr//td[" + GetColumnIndex("Destination") + "]");
            VerifyColumnData(listOfActualDestinations, listOfExpectedDestinations);
        }

        public void VerifyResorts(string resortName)
        {
            var listOfExpectedResorts = resortName.ConvertStringIntoList();
            var listOfActualResorts = By.XPath("//table//tr//td[" + GetColumnIndex("Resort") + "]");
            VerifyColumnData(listOfActualResorts, listOfExpectedResorts);
        }


        public void VerifyProperties(string propertyName)
        {
            var listOfExpectedProperties = propertyName.ConvertStringIntoList();
            var listOfActualProperties = By.XPath("//table//tr//td[" + GetColumnIndex("Property") + "]");
            VerifyColumnData(listOfActualProperties, listOfExpectedProperties);
        }

        public void ClickRefineSearchBtn()
        {
            Driver.ClickItem(_refineSearchBtn);
        }


        public void VerifyBookingDisplayedExpanded()
        {
            Driver.WaitForItem(_searchResultsDisplayed);
            Assert.True(Driver.IsElementPresent(_expandedTableIconMinus));
            Assert.True(Driver.IsElementPresent(By.XPath(_bookingPassengersTable)));
        }

        //to be removed
        public void ExpandPassengerTable(string resultNumber)
        {
            Driver.ClickItem(By.XPath(_expandedTableIconPlus.Replace("resultnumber", resultNumber)));
        }
        

        public void ExpandBookingReference(string bookingReference)
        {
            var listOfExpandIcons = Driver.FindElements(_expandTableIconsPlus);

            var bookingNumber = GetBookingRefIndexSingleSearch(bookingReference);

            Driver.ClickItem(listOfExpandIcons[bookingNumber]);
        }

        public void CollapseBookingReference()
        {
            var collapseIcon = Driver.FindElement(_expandTableIconsMinus);
            Driver.ClickItem(collapseIcon);
        }

        public void TickPassengerfromCustomerTable(string passengerName)
        {
            var passengerCheckboxes = Driver.FindElements(_passengersCheckboxes);
            var passengerNumber = GetPassengerNameIndex(passengerName);

            Driver.ClickItem(passengerCheckboxes[passengerNumber]);
        }

        public void VerifyPassengerPresent(string passengerName) // verifies if passenger is present in given table
        {
            var actualResult = Driver.FindElements(By.XPath(_bookingPassengersTable + "/tbody/tr/td"));
            var passengerFound = false;
            foreach (var element in actualResult)
            {
                if (element.Text.Contains(passengerName))
                {
                    passengerFound = true;
                    break;
                }
            }

            Assert.True(passengerFound, "Unable to find :" + passengerName + " in the passengers table");
        }

        public void VerifyPassengerData(Table table)
        {
            var rowNumberFlag = 1;
            foreach (var row in table.Rows)
            {
                var actualResult =
                    Driver.FindElements(By.XPath(_bookingPassengersTable + "/tbody/tr[" + rowNumberFlag + "]/td"));

                switch (row["Name"])
                {
                    case "ANY_NAME":
                        Assert.True(actualResult[0].Text.Length > 0);
                        break;
                    default:
                        Assert.True(actualResult[0].Text == row["Name"]);
                        break;
                }

                switch (row["DOB"])
                {
                    case "ANY_DOB":
                        Assert.True(actualResult[1].Text.Length > 0);
                        break;
                    default:
                        Assert.True(actualResult[1].Text.Replace(" ", "") == row["DOB"].Replace(" ", ""));
                        break;
                }

                rowNumberFlag++;
            }
        }

        public void VerifyAllPassengerCheckBoxes(string checkboxStatus)
        {
            switch (checkboxStatus)
            {
                case "ticked":
                    VerifyAllTickBoxesChecked();
                    break;
                case "unticked":
                    VerifyAllTickBoxesUnchecked();
                    break;
                default:
                    throw new Exception(checkboxStatus + " :is not a valid checkbox state ");
            }
        }

        public void VerifyBtnStatus(string expectedStatus)
        {
            switch (expectedStatus)
            {
                case "enabled":
                    Assert.True(!Driver.IsElementPresent(_disabledButtonPassengerTbl));
                    break;
                case "disabled":
                    Assert.True(Driver.IsElementPresent(_disabledButtonPassengerTbl));
                    break;
                default:
                    throw new Exception(expectedStatus + " :is not a valid button state ");
            }
        }

        public void TickUntickSingleCustomer(string tickBoxExpectedState, string checkBoxNumber,
            bool selectHeader = false)
        {
            var listOfCheckBoxes = Driver.FindElements(_checkBoxMultipleCustomer);
            // var bookingNumber = int.Parse(1) - 1;
            var bookingNumber = 0;
            var currentTickState = listOfCheckBoxes[bookingNumber].FindElement(By.XPath(".//span"));
            switch (tickBoxExpectedState)
            {
                case "tick":
                    if (currentTickState.GetAttribute("class") != CheckBoxTicked)
                    {
                        listOfCheckBoxes[bookingNumber].Click();
                    }

                    break;
                case "untick":
                    if (currentTickState.GetAttribute("class") == CheckBoxTicked)
                    {
                        listOfCheckBoxes[bookingNumber].Click();
                    }

                    break;
                default:
                    throw new Exception(tickBoxExpectedState + " :is not a valid tickbox state ");
            }
        }

        public void VerifyCheckBoxTicked(string tickBoxNumber, string tickBoxState)
        {
            var listOfCheckBoxes = Driver.FindElements(_checkBoxMultipleCustomer);
            var bookingNumber = int.Parse(tickBoxNumber) - 1;
            var currentTickState = listOfCheckBoxes[bookingNumber].FindElement(By.XPath(".//span"));
            switch (tickBoxState)
            {
                case "ticked":
                    Assert.True(currentTickState.GetAttribute("class") == CheckBoxTicked,
                        "Unable to verify tick box number " + tickBoxNumber + " as " + tickBoxState);
                    break;
                case "unticked":
                    Assert.True(currentTickState.GetAttribute("class") == CheckBoxUnticked,
                        "Unable to verify tick box number " + tickBoxNumber + " as " + tickBoxState);
                    break;
                default:
                    throw new Exception(tickBoxState + " :is not a valid tickbox state ");
            }
        }

        public void ClickSelectButton()
        {
            Driver.ClickItem(_selectButton);
        }

        #region multiple customer

        public void ClickSelectMultipleCustButton()
        {
            Driver.ClickItem(_selectBtnMultCust);
        }

        public void TickUntickBooking(string tickBoxState, string tickBoxNumber)
        {
            var listOfCheckBoxes = Driver.FindElements(_checkBoxMultipleCustomer);
            var bookingNumber = int.Parse(tickBoxNumber);
            var currentTickState = listOfCheckBoxes[bookingNumber].FindElement(By.XPath(".//span"));
            switch (tickBoxState)
            {
                case "tick":

                    if (currentTickState.GetAttribute("class") != CheckBoxTicked)
                    {
                        listOfCheckBoxes[bookingNumber].Click();
                    }

                    break;
                case "untick":
                    if (currentTickState.GetAttribute("class") == CheckBoxTicked)
                    {
                        listOfCheckBoxes[bookingNumber].Click();
                    }

                    break;
                default:
                    throw new Exception(tickBoxState + " :is not a valid tickbox state ");
            }
        }

        public void VerifyMultipleCustomerBtnStatus(string btnStatus)
        {
            switch (btnStatus)
            {
                case "enabled":
                    Assert.True(!Driver.IsElementPresent(_slectBtnDisabledMultCust));
                    break;
                case "disabled":
                    Assert.True(Driver.IsElementPresent(_slectBtnDisabledMultCust));
                    break;
                default:
                    throw new Exception(btnStatus + " :is not a valid button state ");
            }
        }

        #endregion


        #region Helpers

        /// <summary>
        ///     Takes two parameters and asserts the results
        /// </summary>
        /// <param name="actualValuePath"> By element containing the XPath for the result table column</param>
        /// <param name="expectedValueList">list of values to be verified against</param>
        internal void VerifyColumnData(By actualValuePath, ICollection<string> expectedValueList)
        {
            var listOfActualData = Driver.FindElements(actualValuePath);
            foreach (var value in listOfActualData)
            {
                Assert.True(expectedValueList.Contains(value.Text),
                    "Expected value :" + expectedValueList + " did not match with actual value :" + value.Text);
            }

            var verifiedValues = 0;
            foreach (var expectedValue in expectedValueList
            ) // verifies all the expected destinations are displayed atleast once on expected list
            {
                foreach (var t in listOfActualData)
                {
                    if (t.Text == expectedValue)
                    {
                        Log.Debug("Verified  :" + expectedValue);
                        verifiedValues++;
                        break;
                    }
                }
            }

            Assert.True(verifiedValues == expectedValueList.Count,
                "Could not verify all the values. Could only verify :" + verifiedValues + " out of total" +
                expectedValueList.Count +
                " values. Check logs for more info"); // verify all the destinations are displayed
        }

        internal void VerifyAllBookingRefsDisplayedAsLinks()
        {
            var links = Driver.FindElements(_bookingReferencesLinks);

            foreach (var link in links)
            {
                Assert.IsTrue(link.TagName.Equals("a"), "The booking reference is not a link");
            }
        }

        /// <summary>
        ///     Gets the index of the column header. e.g. If column header 'Lead Customer' is on third row then this will return 3
        /// </summary>
        /// <param name="columnName"> column header to look for</param>
        /// <returns></returns>
        internal string GetColumnIndex(string columnName)
        {
            var listOfTableHeaders = Driver.FindElements(_tableHead);

            var indexNumber = "";

            for (var i = 0; i < listOfTableHeaders.Count; i++)
            {
                if (listOfTableHeaders[i].Text == columnName) // if column name matches what we looking for
                {
                    indexNumber = (i + 1).ToString();
                    break;
                }
            }

            return indexNumber;
        }

        internal void VerifyAllTickBoxesUnchecked()
        {
            var listOfCheckBoxes = Driver.FindElements(_checkBoxMultipleCustomer);
            foreach (var checkbox in listOfCheckBoxes)
            {
                var tickbox = checkbox.FindElement(By.XPath(".//span"));
                Assert.True(tickbox.GetAttribute("class") == CheckBoxUnticked,
                    "Unable to verify checkbox as unticked on search result :" + listOfCheckBoxes.IndexOf(checkbox));
            }
        }

        internal void VerifyAllTickBoxesChecked()
        {
            var listOfCheckBoxes = Driver.FindElements(_checkBoxMultipleCustomer);
            foreach (var checkbox in listOfCheckBoxes)
            {
                var tickbox = checkbox.FindElement(By.XPath(".//span"));
                Assert.True(tickbox.GetAttribute("class") == CheckBoxTicked,
                    "Unable to verify checkbox as ticked on search result :" + listOfCheckBoxes.IndexOf(checkbox));
            }
        }


        public void TickUntickBookingReference(string tickBoxState, string bookingReference)
        {
            var bookingNumber = GetBookingRefIndexMultipleSearch(bookingReference);
            var listOfCheckBoxes = Driver.FindElements(_checkBoxMultipleCustomer);
            var currentTickState = listOfCheckBoxes[bookingNumber].FindElement(By.CssSelector("span"));
            switch (tickBoxState)
            {
                case "tick":

                    if (currentTickState.GetAttribute("class") != CheckBoxTicked)
                    {
                        Driver.ClickItem(listOfCheckBoxes[bookingNumber], true);
                    }

                    break;
                case "untick":
                    if (currentTickState.GetAttribute("class") == CheckBoxTicked)
                    {
                        Driver.ClickItem(listOfCheckBoxes[bookingNumber], true);
                    }

                    break;
                default:
                    throw new Exception(tickBoxState + " :is not a valid tickbox state ");
            }
        }


        internal int GetBookingRefIndexMultipleSearch(string booking)
        {
            Assert.IsTrue(Driver.WaitForItem(_bookingReferencesLinks), "The booking references are not being displayed.");
            var bookingRefs = Driver.FindElements(_bookingReferencesLinks);

            var i = 50;
            foreach (var bookingRef in bookingRefs)
            {
                if (Driver.GetText(bookingRef).Equals(booking))
                {
                    i = bookingRefs.IndexOf(bookingRef);
                    break;
                }
            }

            if (i.Equals(50))
            {
                Driver.NavigateToPaginatorPageUntilTextPresent(_bookingReferencesLinks, booking);

                foreach (var bookingRef in bookingRefs)
                {
                    if (Driver.GetText(bookingRef).Equals(booking))
                    {
                        i = bookingRefs.IndexOf(bookingRef);
                        break;
                    }
                }
            }

            return i+1;

        }

        internal int GetBookingRefIndexSingleSearch(string booking)
        {
            Assert.IsTrue(Driver.WaitForItem(_bookingReferencesLinks), "The booking references are not being displayed.");
            var bookingRefs = Driver.FindElements(_bookingReferencesLinks);

            var i = 0;
            foreach (var bookingRef in bookingRefs)
            {
                if (Driver.GetText(bookingRef).Equals(booking))
                {
                    i = bookingRefs.IndexOf(bookingRef);
                }
            }

            return i;
        }

        internal int GetPassengerNameIndex(string passengerName)
        {
            Assert.IsTrue(Driver.WaitForItem(_passengersCustomersTable), "The passengers are not being displayed.");
            var passengerNames = Driver.FindElements(_passengersCustomersTable);

            var i = 0;
            foreach (var passenger in passengerNames)
            {
                if (Driver.GetText(passenger).Equals(passengerName))
                {
                    i = passengerNames.IndexOf(passenger);
                }
            }

            return i;
        }

        public void VerifySearchResultsOrder()
        {
            var actualBookingRefOrder = Driver.GetTexts(_bookingReferencesLinks);
            var expectedBookingRefOrder = actualBookingRefOrder.OrderByDescending(x => x);
            CollectionAssert.AreEqual(expectedBookingRefOrder, actualBookingRefOrder, "The order of the bookings on the search results page is incorrect");
        }


        public void VerifySearchPageNumbers(int expectedPages)
        {
            Assert.True(Driver.WaitForItem(_paginatorElement), "The paginator is not displayed");
            var actualPages = Driver.FindElements(_paginatorPageNumbers).Count;

            Assert.AreEqual(expectedPages, actualPages, "The number of expected pages is incorrect");
        }

        public void ClickOnASearchResultsPage(string page)
        {
            var pages = Driver.FindElements(_paginatorPageNumbers);
            var pagesTexts = Driver.GetTexts(_paginatorPageNumbers);

            foreach (var text in pagesTexts)
            {
                if (text.Equals(page))
                {
                    var x = pagesTexts.IndexOf(text);

                    Driver.ClickItem(pages[x], true);
                }

            }
        }

        public void VerifyCurrentPageOfSearchResults(int page)
        {
            var actualPage = Driver.GetPaginatorCurrentPage();
            Assert.AreEqual(actualPage, page, $"Currently navigated to {actualPage} but should be {page}");
        }

        public void VerifyNumberOfBookingsOnPage(int bookingsCount)
        {
            var actualBookingsCount = Driver.FindElements(_bookingReferencesLinks).Count;
            Assert.AreEqual(bookingsCount, actualBookingsCount, $"The number of bookings on the page is {actualBookingsCount} but should be {bookingsCount}");
        }

        public void VerifyNumberOfBookingsOnLastPage(int bookingsCount)
        {
            var actualBookingsCount = Driver.FindElements(_bookingReferencesLinks).Count;
            Assert.LessOrEqual(bookingsCount, actualBookingsCount, $"The number of bookings on the page is {actualBookingsCount} but should be {bookingsCount}");
        }

        public void VerifySearchResultsMessage(string message)
        {
            var actualMessage = Driver.GetText(_uiMessageText);
            Assert.True(actualMessage.Contains(message), " The search results message is incorrect");
        }

        public void TickUntickSelectAllBookingsReferences(string tickBoxState)
        {
            var selectAllCheckBox = Driver.FindElement(_selectAllCheckBox);
            var currentTickState = selectAllCheckBox.FindElement(By.CssSelector("span"));

            switch (tickBoxState)
            {
                case "tick":

                    if (currentTickState.GetAttribute("class") != CheckBoxTicked)
                    {
                        Driver.ClickItem(selectAllCheckBox);
                    }

                    break;
                case "untick":
                    if (currentTickState.GetAttribute("class") == CheckBoxTicked)
                    {
                        Driver.ClickItem(selectAllCheckBox);
                    }

                    break;
                default:
                    throw new Exception(tickBoxState + " :is not a valid tickbox state ");
            }
        }

        public void VerifyStateOfSelectAllBookingsReferences(string tickBoxState)
        {
            var listOfCheckBoxes = Driver.FindElements(_checkBoxMultipleCustomer);

            switch (tickBoxState)
            {
                case "ticked":
                    foreach (var checkBox in listOfCheckBoxes)
                    {
                        var currentTickState = checkBox.FindElement(By.CssSelector("p-checkbox > div > div:nth-child(2) > span"));

                        if (currentTickState.GetAttribute("class") != CheckBoxTicked)
                        {
                            var x = listOfCheckBoxes.IndexOf(checkBox);
                            Assert.Fail($"Checkbox {x} is not ticked");
                        }
                    }
                    break;
                case "unticked":
                    foreach (var checkBox in listOfCheckBoxes)
                    {
                        var currentTickState = checkBox.FindElement(By.CssSelector("p-checkbox > div > div:nth-child(2) > span"));

                        if (currentTickState.GetAttribute("class") == CheckBoxTicked)
                        {
                            var x = listOfCheckBoxes.IndexOf(checkBox);
                            Assert.Fail($"Checkbox {x} is ticked");
                        }
                    }
                    break;

                default:
                    throw new Exception(tickBoxState + " :is not a valid tickbox state ");
            }

        }


        public void CollectSearchResultsInformation()
        {
            var bookings = ScenarioContext.Current.Get<List<string>>("bookingReferencesList");
            var table = Driver.FindElement(By.CssSelector("[resultstable] table"));
            var bookingReferences = Driver.GetTexts(table.FindElements(_bookingReferencesLinks)); //Driver.GetTexts(_bookingReferencesLinks);
            var destinations = Driver.GetTexts(table.FindElements(_lblDestinations));
            var resorts = Driver.GetTexts(table.FindElements(_lblResorts));
            var properties = Driver.GetTexts(table.FindElements(_lblProperties));

            var destinationsList = new List<string>();
            var resortsList = new List<string>();
            var propertiesList = new List<string>();

            foreach (var bookingReference in bookingReferences)
            {
                foreach (var booking in bookings)
                {
                    if (bookingReference.Equals(booking))
                    {
                        var i = bookingReferences.IndexOf(bookingReference);
                        destinationsList.Add(destinations[i]);
                        resortsList.Add(resorts[i]);
                        propertiesList.Add(properties[i]);
                    }
                }
            }

            ScenarioContext.Current["destinationsList"] = destinationsList;
            ScenarioContext.Current["resortsList"] = resortsList;
            ScenarioContext.Current["propertiesList"] = propertiesList;
        }

        public void ClickFlagInfoForBookingReference()
        {
            Assert.IsTrue(Driver.WaitForItem(_flagInfo), "The flag info exclamation icon is not displayed.");
            Driver.ClickItem(_flagInfo);
        }

        #endregion
    }
}