using System;
using System.Collections.Generic;
using System.Linq;
using J2BIOverseasOps.Extensions;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Pages.BuildingWork.Summary
{
    class BuildingWorkSummaryPage : BuildingWorkCommon
    {
        private readonly ApiCalls _apiCall;
        private readonly By _workStartDate = By.XPath("//span[@id='workStartDate']");
        private readonly By _workEndDate = By.XPath("//span[@id='workEstimatedCompletionDate']");
        private readonly By _catgOfwrk = By.XPath("//span[@id='whereTakingPlace']");
        private readonly By _phaseDates = By.Id("phase-1");
        private readonly By _areasAffectedSummary = By.XPath("//*[@id='areasAffected']//span");
        private readonly By _numberOfAreasAffected = By.XPath("//*[@id='areasAffected']//a");
        private readonly By _affectedAreasOverLayPanel = By.XPath("//div[@class='ui-overlaypanel-content']");
        private readonly By _markAsUrgent = By.XPath("//p-selectbutton[@id='markAsUrgent']");
        private readonly By _agreeWithGrade = By.XPath("//p-selectbutton[@id='agreeWithGrade']");
        private readonly By _otherPropertiesAffected = By.XPath("//p-selectbutton[@id='otherPropertiesAffected']");
        private readonly By _markAsUrgentValidation=By.XPath("//p-message[@id='markAsUrgent-validation']");
        private readonly By _summaryOfWork=By.XPath("//textarea[@id='summaryOfTheWork']");
        private readonly By _summaryOfWorkValidation = By.XPath("//p-message[@id='summaryOfTheWork-validation']");
        private static int _phaseNumberIndex = 1;
        private By PhasesSummary => By.Id($"phase-{_phaseNumberIndex}");
        private readonly By _hotelStatus = By.Id("HotelStatus");
        private readonly By _bookingsAffected = By.XPath("//span[@id='bookingsAffected']");
        public BuildingWorkSummaryPage(IWebDriver driver, ILog log, IRunData rundata) : base(driver, log, rundata)
        {
            _apiCall = new ApiCalls(rundata);

        }

        public void VerifyFieldsOnSummaryPage(Table table)
        {
            foreach (var row in table.Rows)
            {
                var fieldName = row["field"];
                var value = row["value"];
                string expectedValue;
                string actualValue;
                switch (fieldName.ToLower())
                {
                    case "category of work":
                        expectedValue = value;
                        actualValue = Driver.GetText(_catgOfwrk);
                        Assert.AreEqual(expectedValue, actualValue,
                            $"Expected value {expectedValue} was not same as Actual Value {actualValue}");
                        break;
                    case "work_start_date":
                        expectedValue = ScenarioContext.Current[WrkStartDateConextKey].ToString();
                        actualValue = Driver.GetText(_workStartDate);
                        Assert.AreEqual(expectedValue, actualValue,
                            $"Expected value {expectedValue} was not same as Actual Value {actualValue}");
                        break;
                    case "completion_date":
                        expectedValue = ScenarioContext.Current[WrkCompletionDateContextKey].ToString();
                        actualValue = Driver.GetText(_workEndDate);
                        Assert.AreEqual(expectedValue, actualValue,
                            $"Expected value {expectedValue} was not same as Actual Value {actualValue}");
                        break;
                    case "areas affected":
                        _verifyHotelAreasAffectedSummary(value.ConvertStringIntoList());
                        break;
                    case "hotel openclosed":
                        expectedValue = value;
                        actualValue = Driver.GetText(_hotelStatus);
                        Assert.True(actualValue.Contains(expectedValue),
                            $"Expected value for the Hotel open/closed field {expectedValue} was not same as Actual Value {actualValue}");
                        break;
                    case "mark as urgent":
                        _verifyMarkAsUrgentField(value);
                        break;
                    default:
                        Assert.Fail($"{fieldName} is not a valid field name");
                        break;
                }

            }
        }

        private void _verifyMarkAsUrgentField(string expected)
        {

            if (expected.ToLower() == "not selected")
            {
                Assert.False(Driver.IsPOptionSelected(_markAsUrgent),
                    "Unable to verify the mark as urgent field as unselected");
            }
            else
            {
                var actualAnswer = Driver.GetSelectedPOption(_markAsUrgent);
                Assert.AreEqual(actualAnswer, expected,
                    $"Expected answer {expected} for the 'mark as urgent' question is not the same as Actual answer {actualAnswer}");
            }
        }

        public void VerifyPhasesDates(Table table)
        {
            foreach (var row in table.Rows)
            {
                var phaseName = row["phase"];
                var dates = row["dates"];

                var toFromDates = dates.Split('-');

                var fromDate = toFromDates[0].Trim();
                var toDate = toFromDates[1].Trim();

                var expectedFromDate = Driver.CalculateDate(fromDate).ToString("dd/MM/yyyy");
                var expectedToDate = Driver.CalculateDate(toDate).ToString("dd/MM/yyyy");
                var expectedPhaseDates = $"{expectedFromDate}-{expectedToDate}".RemoveAllSpaces();

                var actualPhasesDates = Driver.GetText(PhasesSummary).RemoveAllSpaces();

                Assert.True(actualPhasesDates.Contains(expectedPhaseDates),
                    $"Expected phases dates {expectedPhaseDates} was not present in Actual phases dates {actualPhasesDates}");
                Assert.True(actualPhasesDates.Contains(phaseName.RemoveAllSpaces()),
                    $"Phase name {phaseName} was not present in phases dates {actualPhasesDates}");
                _phaseNumberIndex++;
            }

        }

        public void VerifyPhaseDatesNotDisplayed()
        {
            Assert.True(!Driver.WaitForItem(_phaseDates, 1),
                $"Was able to find phases dates on summary page while expecting it not to be there.");
        }


        private void _verifyHotelAreasAffectedSummary(IReadOnlyCollection<string> expectedValues)
        {
            var numberOfExpectedAreas = expectedValues.Count;
            List<string> actualValues;
            if (numberOfExpectedAreas > 2)
            {
                var actualText = Driver.GetText(_numberOfAreasAffected);
                var expectedText = $"{numberOfExpectedAreas} areas affected";
                Assert.AreEqual(expectedText, actualText,
                    $"Expected text {expectedText} was not equal to {actualText}");
                Driver.ClickItem(_numberOfAreasAffected,true);
                actualValues = Driver.GetText(_affectedAreasOverLayPanel).Trim().ConvertStringIntoList();
            }
            else
            {
                actualValues = (Driver.GetText(_areasAffectedSummary).Trim()).ConvertStringIntoList();
            }

            Assert.AreEqual(expectedValues, expectedValues,
                $"Expected values {expectedValues} were not equal to Actual values {actualValues}");

        }

        public void VerifyCustomersAndBookingsAffected(string userId, string dateFrom, string dateTo, string destination,
            string resort, string property)
        {
            var inresortFrom = Driver.CalculateDate(dateFrom);
            var inresortTo = Driver.CalculateDate(dateTo);
            var destinationId = _apiCall.GetDestinationId(destination);
            var propertyId = _apiCall.GetPropertyId(destinationId, property);
            var uId = 0;
            uId = userId == "current_loggedin_user" ? _apiCall.GetUserId(ScenarioContext.Current[CurrentUsername].ToString()) : Convert.ToInt32(userId);
            var bookings = _apiCall.GetListOfBookings(uId, inresortFrom, inresortTo, propertyId);

            var expectedNumberOfBookingsAffected = "0";
            var expectedNumberOfCustomersAffected = "0";
            if (bookings.TotalMatches > 0)
            {
                expectedNumberOfBookingsAffected = bookings.Bookings.Length.ToString();
                expectedNumberOfCustomersAffected = bookings.Bookings.Select(
                    bookingObject => // count total number of customers affected
                        bookingObject.Customers.Length).Aggregate(0, (current, numberOfCustomersInBooking) =>
                    current + numberOfCustomersInBooking).ToString();
            }

            var bookingText = expectedNumberOfBookingsAffected == "0" ? "booking" : "bookings";
            var customerText = expectedNumberOfCustomersAffected == "0" ? "customer" : "customers";

            var expectedBookingsAffectedText =
                $"{expectedNumberOfBookingsAffected} {bookingText} {expectedNumberOfCustomersAffected} {customerText}";
            var actualBookingsAffectedText = Driver.GetText(_bookingsAffected).Trim();
            Assert.AreEqual(expectedBookingsAffectedText, actualBookingsAffectedText,
                $"Expected bookings affected text {expectedBookingsAffectedText} was not same as actual {actualBookingsAffectedText}");
        }

        public void VerifyCustomersAndBookingsAffectedDisplay()
        {
            Assert.True(Driver.GetText(_bookingsAffected).Length>0,"Unable to verify if number of bookings/customers displayed");
        }

        public void EnterSummaryPageAnswers(Table table)
        {
            foreach (var row in table.Rows)
            {
                var fieldName = row["field"];
                var value = row["value"];
                switch (fieldName.ToLower())
                {
                    case "agree with grade?":
                        Driver.ClickPSelectOption(_agreeWithGrade, value);
                        break;
                    case "other properties affected?":
                        Driver.ClickPSelectOption(_otherPropertiesAffected, value);
                        break;
                    case "mark as urgent":
                        Driver.ClickPSelectOption(_markAsUrgent, value);
                        break;
                    case "summary of work":
                        Driver.EnterText(_summaryOfWork,value);
                        break;
                    default:
                        Assert.Fail($"{fieldName} is not a valid field");
                        break;
                }
            }
        }

        public void VerifySummaryPageAnswers(Table table)
        {
            foreach (var row in table.Rows)
            {
                var fieldName = row["field"];
                var value = row["value"];
                switch (fieldName.ToLower())
                {
                    case "mark as urgent":
                        Driver.VerifySingleSelectedPOption(_markAsUrgent, value);
                        break;
                    case "summary of work":
                        Driver.VerifyInputBoxText(_summaryOfWork, value);
                        break;
                    default:
                        Assert.Fail($"{fieldName} is not a valid field");
                        break;
                }
            }
        }

        public void ClickSaveAndCloseButton()
        {
            Driver.ClickItem(_saveAndClose);
        }

        public void VerifSummaryValidationErrorMessage(Table table)
        {
            foreach (var row in table.Rows)
            {
                var field = row["field"];
                var expectedError = row["error"];
                switch (field.ToLower())
                {
                    case "mark as urgent":
                        VerifyBwFieldValidationErrorMessage(_markAsUrgentValidation, expectedError);
                        break;
                    case "summary of work":
                        VerifyBwFieldValidationErrorMessage(_summaryOfWorkValidation, expectedError);
                        break;
                    default:
                        Assert.Fail($"{field} is not a valid field");
                        return;
                }
            }
        }
    }
}
