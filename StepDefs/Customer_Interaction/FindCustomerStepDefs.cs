using System.Collections.Generic;
using J2BIOverseasOps.Extensions;
using J2BIOverseasOps.Pages.Customer_Interaction;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.Customer_Interaction
{
    [Binding]
    public sealed class FindCustomerStepDefs : BaseStepDefs
    {
        private readonly BookingDetailsPage _bookingDetails;
        private readonly FindCustomerPage _findCustomer;

        public FindCustomerStepDefs(IWebDriver driver, ILog log) : base(driver, log)
        {
            _findCustomer = new FindCustomerPage(driver, log);
            _bookingDetails = new BookingDetailsPage(driver, log);
        }

        [When(@"I type in the ""(.*)"" as the customer name on the find a customer page")]
        public void WhenITypeInAsTheCustomerName(string customerName)
        {
            _findCustomer.EnterCustomerName(customerName);
        }

        [When(@"I click the search button on the find a customer page")]
        public void WhenIClickTheSearchButton()
        {
            _findCustomer.ClickSearchButton();
        }

        [Then(@"I am presented with the search results on the search results page")]
        public void ThenIAmPresentedWithSearchResults()
        {
            var bookingReferencesList = new List<string>();
            ScenarioContext.Current["bookingReferencesList"] = bookingReferencesList;

            _findCustomer.WaitForSpinnerToDisappear();
            _findCustomer.VerifySearchResultsDisplayed();
        }

        [Then(@"I verify the search results as the following on the search results page:")]
        public void ThenIVerifyTheSearchResultsAsFollowing(Table table)
        {
            _findCustomer.VerifySearchResultData(table);
        }

        [When(@"I type in ""(.*)"" as the booking reference on the find a customer page")]
        public void WhenITypeInAsTheBookingReference(string bookingRef)
        {
            _findCustomer.EnterBookingRef(bookingRef);
        }

        [When(@"I select the destination as ""(.*)"" from the destinations filter on the find a customer page")]
        public void WhenISelectTheDestinationAs(string destination)
        {
            _findCustomer.FilterByDestination(destination);
        }

        [Then(@"I verify all the results display only contain the destination as ""(.*)""")]
        public void ThenIVerifyAllTheResultsDisplayOnlyContainTheDestinationAs(string destination)
        {
            _findCustomer.VerifyDestination(destination);
        }

        [When(
            @"I select the destination as ""(.*)"" and the resort as ""(.*)"" from the resorts filter on the find a customer page")]
        public void WhenISelectAndFromTheResortsFilter(string destination, string resort)
        {
            _findCustomer.FilterByResorts(destination, resort);
        }

        [When(
            @"I select the destination as ""(.*)"", and the resort as ""(.*)"" and the properties as ""(.*)"" from the properties filter on the find a customer page")]
        public void WhenISelectAndFromThePropertiesFilter(string destination, string resort, string properties)
        {
            _findCustomer.FilterByProperties(destination, resort, properties);
        }

        [Then(
            @"I verify destination value ""(.*)"", resort value ""(.*)"" and properties value ""(.*)"" are displayed as the search criteria")]
        public void VerifySearchCriteria(string destination, string resort, string properties)
        {
            _findCustomer.VerifySearchCriteria(destination, resort, properties);
        }

        [Then(@"I verify all the results display only contain the resorts as ""(.*)""")]
        public void ThenIVerifyAllTheResultsDisplayOnlyContainTheResortsAs(string resortName)
        {
            _findCustomer.VerifyResorts(resortName);
        }

        [Then(@"I verify all the results display only contain the properties as ""(.*)""")]
        public void ThenIVerifyAllTheResultsDisplayOnlyContainThePropertiesAs(string propertyName)
        {
            _findCustomer.VerifyProperties(propertyName);
        }

        [Then(@"I verify no results are found")]
        public void ThenIVerifyNoResultsAreFound()
        {
            _findCustomer.WaitForSpinnerToDisappear();
            _findCustomer.VerifyNoSearchResultsDisplayed();
        }

        [Then(@"I am presented with message ""(.*)"" on the search result page")]
        public void ThenIAmPresentedWithMessageOnTheSearchResultPage(string message)
        {
            _findCustomer.VerifyUiMessage(message);
        }

        [When(@"I click on the Refine Search button")]
        public void WhenIClickOnTheRefineSearchButton()
        {
            _findCustomer.ClickRefineSearchBtn();
        }

        [Then(@"I verify the booking is displayed in expanded view")]
        public void ThenIVerifyTheBookingIsDisplayedInExpandedView()
        {
            _findCustomer.VerifyBookingDisplayedExpanded();
        }

        [When(@"I click the expand icon for the result ""(.*)""")]
        public void WhenIClickTheExpandIconForTheResult(string resultNumber)
        {
            _findCustomer.ExpandPassengerTable(resultNumber);
        }

        [Then(@"I verify the passengers details as:")]
        public void ThenIVerifyThePassengersDetailsAs(Table table)
        {
            _findCustomer.VerifyPassengerData(table);
        }

        [When(@"I verify ""(.*)"" is displayed as one of the passengers")]
        public void WhenIVerifyIsDisplayedAsOneOfThePassengers(string passengerName)
        {
            _findCustomer.VerifyPassengerPresent(passengerName);
        }

        [Then(@"I verify all the check boxes are ""(unticked|ticked)"" on the search results page")]
        public void ThenIVerifyAllTheCheckBoxesAreTickedUnticked(string checkboxStatus)
        {
            _findCustomer.VerifyAllPassengerCheckBoxes(checkboxStatus);
        }

        [Then(@"I verify the select button is ""(enabled|disabled)"" on the search results page")]
        public void ThenIVerifyTheSelectButtonIs(string btnStatus)
        {
            _findCustomer.VerifyBtnStatus(btnStatus);
        }

        [When(@"I click select button on the search results page")]
        public void WhenIClickSelectButtonOnTheSearchResultsPage()
        {
            _findCustomer.ClickSelectButton();
        }

        [When(
            @"I ""(tick|untick)"" the checkbox on the booking number ""(.*)"" for the multiple customer search results page")]
        public void WhenITheCheckboxOnTheBookingNumberForTheMultipleCustomerSearchResultsPage(string tickBoxState,
            string tickBoxNumber)
        {
            _findCustomer.WaitForGrowlNotificationToDisappear();
            _findCustomer.TickUntickBooking(tickBoxState, tickBoxNumber);
        }

        [Then(@"I verify the select button is ""(.*)"" on the multiple customer search results page")]
        public void ThenIVerifyTheSelectButtonIsOnTheMultipleCustomerSearchResultsPage(string btnStatus)
        {
            _findCustomer.VerifyMultipleCustomerBtnStatus(btnStatus);
        }

        [When(@"I ""(.*)"" the checkbox number ""(.*)"" on the search results page")]
        public void WhenITheCheckboxNumber(string tickBoxState, string tickBoxNumber)
        {
            _findCustomer.WaitForGrowlNotificationToDisappear();
            _findCustomer.TickUntickSingleCustomer(tickBoxState, tickBoxNumber);
        }

        [When(@"I ""(tick|untick)"" the header checkbox on the search results page")]
        public void WhenITheHeaderCheckbox(string tickBoxState)
        {
            _findCustomer.TickUntickSingleCustomer(tickBoxState, string.Empty, true);
        }

        [When(@"I click the Select Button on the multiple customer search results page")]
        public void WhenIClickTheSelectButtonOnTheMultipleCustomerSearchResultsPage()
        {
            _findCustomer.CollectSearchResultsInformation();
            _findCustomer.ClickSelectMultipleCustButton();           
        }

        [Then(@"I verify only the checkbox number ""(.*)"" is ""(.*)"" on the search results page")]
        public void ThenIVerifyOnlyTheCheckboxNumberIs(string tickBoxNumber, string tickBoxState)
        {
            _findCustomer.VerifyCheckBoxTicked(tickBoxNumber, tickBoxState);
        }

        [When(@"I click the Select Button on the search results page")]
        public void ThenIClickTheSelectButton()
        {
            _findCustomer.ClickSelectButton();
        }

        [Then(@"I verify I ""(can|can not)"" see the '(.*)' checkbox")]
        public void ThenIVerifyICanSeeTheFilter(string status, string filterName)
        {
            _findCustomer.VerifyFilterDisplayed(status, filterName);
        }

        [Then(@"I verify the default destinations displayed are:")]
        public void ThenIVerifyTheDefaultDestinationsDisplayedAre(Table table)
        {
            _findCustomer.VerifyDefaultDestination(table);
        }
        
        [Then(@"I verify ""(.*)"" has been selected on the In Resort section")]
        public void ThenIVerifyHasBeenSelectedOnTheInResortSection(string expectedToggle)
        {
            _findCustomer.VerifySelectedResortToggle(expectedToggle);
        }

        [Then(@"I can see the dates selection box along with the calender widget")]
        public void ThenICanSeeTheDatesSelectionBoxAlongWithTheCalenderWidget()
        {
            _findCustomer.VerifyDateCalenderDisplayed();
        }

        [When(@"I click the calendar widget button")]
        public void WhenIClickTheCalendarWidgetButton()
        {
            _findCustomer.ClickCalendarWidgetBtn();
        }

        [Then(@"the date picker calendar is displayed")]
        public void ThenIAmDisplayedWithDatePickerCalendar()
        {
            _findCustomer.VerifyDatePickerDisplayed();
        }

        [Then(@"the date picker calendar is not displayed")]
        public void ThenTheDatePickerCalendarIsNotDisplayed()
        {
            _findCustomer.VerifyDatePickerNotDisplayed();
        }


        [Then(@"I select the in resort from date as ""(.*)"" of ""(.*)""")]
        public void ThenISelectTheInResortFromDateAsOf(string resortFromDay, string resortFromMonthYear)
        {
            _findCustomer.ClickResortDateFrom(resortFromDay, resortFromMonthYear);
        }

        [Then(@"I select the in resort to date as ""(.*)"" of ""(.*)""")]
        public void ThenISelectTheInResortToDateAsOf(string resortToDay, string resortToMonthYear)
        {
            _findCustomer.ClickResortDateTo(resortToDay, resortToMonthYear);
        }

        [Then(@"all the booking references are displayed as links on the search results page")]
        public void ThenAllTheBookingReferencesAreDisplayedAsLinksOnTheSearchResultsPage()
        {
            _findCustomer.VerifyAllBookingRefsDisplayedAsLinks();
        }

        [When(@"I click the No bookings affected button on the find multiple customers page")]
        public void WhenIClickTheNoBookingsAffectedButtonOnTheFindMultipleCustomersHomepage()
        {
            _findCustomer.NoBookingsAffectedBtn();
        }

        [Given(@"I verify that the No bookings affected button is active and visible on the find multiple customer page")]
        [When(@"I verify that the No bookings affected button is active and visible on the find multiple customer page")]
        [Then(@"I verify that the No bookings affected button is active and visible on the find multiple customer page")]
        public void ThenIVerifyThatTheNoBookingsAffectedButtonIsActiveAndVisibleOnTheFindMultipleCustomerPage()
        {
            _findCustomer.VerifyNoBookingsAffectedBtnActive();
        }

        [When(@"I select all the destinations from the destinations filter on the find a customer page")]
        public void WhenISelectAllTheDestinationsFromTheDestinationsFilterOnTheFindACustomerPage()
        {
            _findCustomer.SelectAllDestinations();
        }

        [When(@"I ""(.*)"" the checkbox on the booking reference ""(.*)"" for the multiple customer search results page")]
        public void WhenITheCheckboxOnTheBookingReferenceForTheMultipleCustomerSearchResultsPage(string tickBoxState, string bookingReference)
        {
            var bookingReferencesList = ScenarioContext.Current.Get<List<string>>("bookingReferencesList");
            bookingReferencesList.Add(bookingReference);
            ScenarioContext.Current["bookingReferencesList"] = bookingReferencesList;

            _findCustomer.TickUntickBookingReference(tickBoxState, bookingReference);
        }

        [When(@"I expand the plus icon for the booking reference ""(.*)"" on the search results page")]
        public void WhenIExpandThePlusIconForTheBookingReferenceOnTheSearchResultsPage(string bookingReference)
        {
            _findCustomer.ExpandBookingReference(bookingReference);
        }

        [When(@"I collapse the expanded booking on the search results page")]
        public void WhenICollapseTheExpandedBookingOnTheSearchResultsPage()
        {
            _findCustomer.CollapseBookingReference();
        }

        [When(@"I click the checkbox for the passenger ""(.*)"" on the search results page")]
        public void WhenIClickTheCheckboxForThePassengerOnTheSearchResultsPage(string passengerName)
        {
            _findCustomer.TickPassengerfromCustomerTable(passengerName);
        }

        [Then(@"I verify the results are in order of booking reference descending on the search results page")]
        public void ThenIVerifyTheResultsAreInOrderOfBookingReferenceDescendingOnTheSearchResultsPage()
        {
            _findCustomer.VerifySearchResultsOrder();
        }


        [Then(@"I verify I can see ""(.*)"" page numbers on the multiple customer search results page")]
        public void ThenIVerifyICanSeePageNumbersOnTheSearchResultsPage(int expectedPages)
        {
            _findCustomer.VerifySearchPageNumbers(expectedPages);
        }

        [When(@"I click on page ""(.*)"" on the paginator on the multiple customer search results page")]
        public void WhenIClickOnPageOnThePaginatorOnTheSearchResultsPage(string page)
        {
            _findCustomer.ClickOnASearchResultsPage(page);
        }

        [Then(@"I verify that I navigate to page ""(.*)"" of the multiple customer search results page")]
        public void ThenIVerifyThatINavigateToPageOfTheSearchResultsPage(int page)
        {
            _findCustomer.VerifyCurrentPageOfSearchResults(page);
        }

        [Then(@"I verify there are ""(.*)"" bookings on the multiple customer search results page")]
        public void ThenIVerifyThereAreBookingsOnEachSearchResultsPage(int bookingsCount)
        {
            _findCustomer.VerifyNumberOfBookingsOnPage(bookingsCount);
        }

        [Then(@"I verify there are ""(.*)"" or less bookings on the last multiple customer search results page")]
        public void ThenIVerifyThereAreOrLessBookingsOnTheLastSearchResultsPage(int bookingsCount)
        {
            _findCustomer.VerifyNumberOfBookingsOnLastPage(bookingsCount);
        }

        [Then(@"I verify the results message shows ""(.*)"" bookings on the multiple customer search results page")]
        public void ThenIVerifyTheResultsMessageShowsBookingsOnTheSearchResultsPage(string message)
        {
            _findCustomer.VerifySearchResultsMessage(message);
        }

        [When(@"I ""(.*)"" the select all bookings checkbox on the multiple customer search results page")]
        public void WhenITheSelectAllBookingsCheckboxOnTheMultipleCustomerSearchResultsPage(string tickBoxState)
        {
            _findCustomer.WaitForGrowlNotificationToDisappear();
            _findCustomer.TickUntickSelectAllBookingsReferences(tickBoxState);
        }

        [Then(@"I verify that all the bookings checkboxes are ""(.*)"" on the multiple customer search results page")]
        public void ThenIVerifyThatAllTheBookingsCheckboxesAreOnTheMultipleCustomerSearchResultsPage(string tickBoxState)
        {
            _findCustomer.VerifyStateOfSelectAllBookingsReferences(tickBoxState);
        }

        [When(@"I click the ""(.*)"" button in the In Resort section")]
        public void WhenIClickTheButtonInTheInResortSection(string option)
        {
            _findCustomer.ClickInResortSwitch(option);
        }

        [When(@"I click the Continue Button on the multiple customer search results page")]
        public void WhenIClickTheContinueButtonOnTheMultipleCustomerSearchResultsPage()
        {
            _findCustomer.ClickContinue();
        }

        [Then(@"I verify I can see the following selected bookings on the multiple customer search results page:")]
        public void ThenIVerifyICanSeeTheFollowingSelectedBookingsOnTheMultipleCustomerSearchResultsPage(Table table)
        {
            _findCustomer.VerifySelectedBookings(table);
        }

        [Then(@"I verify all the destinations from the destinations filter are selected on the multiple customer search results page")]
        public void ThenIVerifyAllTheDestinationsFromTheDestinationsFilterAreSelectedOnTheMultipleCustomerSearchResultsPage()
        {
            _findCustomer.VerifyAllDestinationsSelected();
        }

        [Then(@"I verify there are no bookings affected on the multiple customer search results page")]
        public void ThenIVerifyThereAreNoBookingsAffectedOnTheMultipleCustomerSearchResultsPage()
        {
            _findCustomer.VerifyNoAffectedBookingsSelected();
        }

        [When(@"I remove the affected booking ""(.*)"" from the multiple customer search results page")]
        public void WhenIRemoveTheAffectedBookingFromTheMultipleCustomerSearchResultsPage(string booking)
        {
            _findCustomer.RemoveAffectedBooking(booking);
        }

        [When(@"I ""(.*)"" the Filter by my properties checkbox on the find multiple customers search page")]
        public void WhenITheFilterByMyPropertiesCheckboxOnTheFindMultipleCustomersSearchPage(string tickBoxState)
        {
            _findCustomer.TickUntickFilterByMyProperties(tickBoxState);
        }

        [When(@"I click on the flag info exclamation icon for the booking on the search results page")]
        public void WhenIClickOnTheFlagInfoExclamationIconForOnTheSearchResultsPage()
        {
            _findCustomer.ClickFlagInfoForBookingReference();
        }

        [Then(@"I verify that the cases linked popup is displayed")]
        public void ThenIVerifyThatTheCasesLinkedPopupIsDisplayed()
        {
            _findCustomer.VerifyCasesLinkedPopupIsDisplayed();
        }

        [Then(@"I verify that the cases linked popup contains the case created:")]
        public void ThenIVerifyThatTheCasesLinkedPopupContainsTheCaseCreated(Table table)
        {
            _findCustomer.VerifyCasesLinkedPopupContains(table);
        }

        [When(@"I click the close button on the cases linked popup")]
        public void WhenIClickTheCloseButtonOnTheCasesLinkedPopup()
        {
            _findCustomer.ClickCloseButtonOnLinkedCasesPopup();
        }

        [Then(@"I verify that the cases linked popup is not displayed")]
        public void ThenIVerifyThatTheCasesLinkedPopupIsNotDisplayed()
        {
            _findCustomer.VerifyCasesLinkedPopupIsNotDisplayed();
        }

        [When(@"I add the following bookings to the affected page:")]
        public void WhenIAddTheFollowingBookingsToTheAffectedPage(Table table)
        {
            _findCustomer.AddbookingsToAffectedPage(table);
        }

        [Then(@"I verify that the cases linked with a status of '(.*)' are ordered by date created descending")]
        public void ThenIVerifyThatTheCasesLinkedWithAStatusOfAreOrderedByDateCreatedDescending(string status)
        {
            _findCustomer.VerifyLinkedCasesOrderedForStatus(status);
        }

    }
}