using J2BIOverseasOps.Pages.Customer_Interaction;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.Customer_Interaction
{
    [Binding]
    public sealed class AdvancedSearchStepDefs : BaseStepDefs
    {
        private readonly FindCustomerPage _findCustomerPage;

        public AdvancedSearchStepDefs(IWebDriver driver, ILog log) : base(driver, log)
        {
            _findCustomerPage = new FindCustomerPage(driver, log);
        }

        [When(@"I expand the Advanced panel at the bottom")]
        public void WhenIClickTheAdvancedPanelAtTheBottom()
        {
            _findCustomerPage.ExpandCollapseAdvancedFilter();
            //_findCustomerPage.ExpandAdvancedPanel();
        }

        [When(@"I minimise the expand panel at the bottom")]
        public void WhenIMinimiseTheExpandPanelAtTheBottom()
        {
            _findCustomerPage.ExpandCollapseAdvancedFilter();
            //_findCustomerPage.MinimiseAdvaPanel();
        }

        [Then(@"the '(.*)' search field is displayed on the find a customer page")]
        public void ThenTheFlightNumberSearchFieldIsDisplayedOnTheFindACustomerPage(string flightLabel)
        {
            _findCustomerPage.VerifyFlightNumberFieldIsDisplayed(flightLabel);
        }

        [Then(@"the Flight number search field is empty on the find a customer page")]
        public void ThenTheFlightNumberSearchFieldIsEmptyOnTheFindACustomerPage()
        {
            _findCustomerPage.VerifyFlightNumberFieldIsEmpty();
        }

        [When(@"I enter the a Flight number ""(.*)"" on the find a customer page")]
        public void WhenIEnterTheAFlightNumberOnTheFindACustomerPage(string flightNumber)
        {
            _findCustomerPage.EnterFlightNumber(flightNumber);
        }

        [Then(@"the Flight number field contains the flight number ""(.*)""")]
        public void ThenThenFlightNumberFieldContainsTheFlightNumber(string flightNumber)
        {
            _findCustomerPage.VerifyFlightNumberFieldContains(flightNumber);
        }

        [When(@"I click the Reset button on the find a customer page")]
        public void WhenIClickTheResetButtonOnTheFindACustomerPage()
        {
            _findCustomerPage.ClickResetButton();
        }

        [Then(@"there is message stating that '(.*)' on the search results page")]
        public void ThenThereAreNoResultsDisplayedOnTheSearchResultsPage(string message)
        {
            _findCustomerPage.VerifyThereAreNoResultsDisplayed(message);
        }

        [Then(@"the Transfer Type drop down field has ""(.*)"" as selected option")]
        public void ThenTheTransferTypeDropDownFieldHasAsSelectedOption(string expectedValue)
        {
            _findCustomerPage.VerifySelectedValueInTransferType(expectedValue);
        }

        [Then(@"I verify the Board Type drop down field has ""(.*)"" as selected option on the search page")]
        public void ThenTheBoardTypeDropDownFieldHasAsSelectedOption(string expectedValue)
        {
            _findCustomerPage.VerifySelectedValueInBoardType(expectedValue);
        }

        [Then(@"the Arrival from drop down field has ""(.*)"" as selected option")]
        public void ThenTheArrivalFromDropDownFieldHasAsSelectedOption(string expectedValue)
        {
            _findCustomerPage.VerifySelectedValueInArrivalFrom(expectedValue);
        }

        [Then(@"the Return to drop down field has ""(.*)"" as selected option")]
        public void ThenTheReturnToDropDownFieldHasAsSelectedOption(string expectedValue)
        {
            _findCustomerPage.VerifySelectedValueInRetTo(expectedValue);
        }

        [Then(@"I verify the Arrival from date field as blank")]
        public void ThenIVerifyTheArrivalFromDateFieldAsBlank()
        {
            _findCustomerPage.ArrivalFromDateBlank();
        }

        [Then(@"I verify the Return to date field as blank")]
        public void ThenIVerifyTheReturnToDateFieldAsBlank()
        {
            _findCustomerPage.RetToDateBlank();
        }

        [When(@"I select the transfer type as ""(.*)""")]
        public void WhenISelectTheTransferTypeAs(string transferType)
        {
            _findCustomerPage.SelectTransferType(transferType);
        }

        [When(@"I select the arrival from as ""(.*)""")]
        public void WhenISelectTheArrivalFromAs(string arrivalFrom)
        {
            _findCustomerPage.SelectArrivalFrom(arrivalFrom);
        }

        [When(@"I select the return to as ""(.*)""")]
        public void WhenISelectTheReturnToAs(string returnTo)
        {
            _findCustomerPage.SelectReturnTo(returnTo);
        }

        [Then(@"the date picker calendar for arrival from is displayed")]
        public void ThenTheDatePickerCalendarForArrivalFromIsDisplayed()
        {
            _findCustomerPage.VerifyArrivalFromDatePickerDisplayed();
        }

        [Then(@"the date picker calendar for return to is displayed")]
        public void ThenTheDatePickerCalendarForReturnToIsDisplayed()
        {
            _findCustomerPage.VerifyReturnToDatePickerDisplayed();
        }

        [When(@"I click the arrival from calendar widget button")]
        public void WhenIClickTheArrivalFromCalendarWidgetButton()
        {
            _findCustomerPage.ClickArrivalFromCalenderWidget();
        }

        [When(@"I click the return to calendar widget button")]
        public void WhenIClickTheReturnToCalendarWidgetButton()
        {
            _findCustomerPage.ClickReturnToCalenderWidget();
        }

        [When(@"I select the arrival from date as ""(.*)"" of ""(.*)""")]
        public void WhenISelectTheArrivalFromDateAsOf(string day, string monthYear)
        {
            _findCustomerPage.SelectArrivalFromDate(day, monthYear);
        }

        [When(@"I select the return to date as ""(.*)"" of ""(.*)""")]
        public void WhenISelectTheReturnToDateAsOf(string day, string monthYear)
        {
            _findCustomerPage.SelectReturnToDate(day, monthYear);
        }

        [When(@"I select the Board type as ""(.*)""")]
        public void WhenISelectTheBoardTypeAs(string boardType)
        {
            _findCustomerPage.SelectBoardType(boardType);
        }

        [When(@"I select the Room type as ""(.*)""")]
        public void WhenISelectTheRoomTypeAs(string roomType)
        {
            _findCustomerPage.SelectRoomTypes(roomType);
        }

        [Then(@"the board type drop down list is visible on the find a customer page")]
        public void ThenTheBoardTypeDropDownListIsVisibleOnTheFindMultipleCustomersPage()
        {
            _findCustomerPage.VerifyBoardTypeDropDownDisplayed();
        }

        [Then(@"I verify ""(.*)"" has been selected as the Board type")]
        public void ThenIVerifyHasBeenSelectedAsTheBoardType(string expectedValue)
        {
            _findCustomerPage.VerifyBoardTypeDropDownSelection(expectedValue);
        }

        [Then(@"the Room Type drop down list is visible on the find a customer page")]
        public void ThenTheRoomTypeDropDownListIsVisibleOnTheFindACustomerPage()
        {
            _findCustomerPage.VerifyRoomTypeDropDownDisplayed();
        }

        [Then(@"I verify the Room Type drop down field has ""(.*)"" as selected option on the search page")]
        public void ThenIVerifyTheRoomTypeDropDownFieldHasAsSelectedOptionOnTheSearchPage(string expectedValue)
        {
            _findCustomerPage.VerifyRoomTypeDropDownDefaultSelection(expectedValue);
        }

        [Then(@"I verify ""(.*)"" has been selected as the Room Type")]
        public void ThenIVerifyHasBeenSelectedAsTheRoomType(string roomTypes)
        {
            _findCustomerPage.VerifyRoomTypeDropDownSelection(roomTypes);
        }

        [When(@"I select all the Room type options")]
        public void WhenISelectAllTheRoomTypeOptions()
        {
            _findCustomerPage.SelectAllRoomTypes();
        }
    }
}