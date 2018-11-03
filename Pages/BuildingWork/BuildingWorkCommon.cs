using System;
using System.Collections.Generic;
using System.Linq;
using J2BIOverseasOps.Extensions;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Pages.BuildingWork
{
    internal class BuildingWorkCommon : CommonPageElements
    {

        public BuildingWorkCommon(IWebDriver driver, ILog log, IRunData runData) : base(driver, log)
        {
        }

        protected const string BwIdScenarioContextKey = "bw_Id_Key";
        protected const string RecordCreationDateContextKey = "record_Date_Key";
        protected const string RecordedByContextKey = "recorded_By_Key";
        protected const string AirportContextKey = "airport_Key";
        protected const string ResortContextKey = "resort_Key";
        protected const string PropertyContextKey = "property_Key";
        protected const string WrkStartDateConextKey = "startDate_Key";
        protected const string WrkCompletionDateContextKey = "workCompletion_Key";
        protected const string CatgOfWrkContextKey = "catgOfWrk_key";
        public static string Destination = "";
        protected static string Resort = "";
        protected static string Property = "";
        protected static List<string> ListOfRestaurants = new List<string> {""};
        protected static List<string> ListOfPools = new List<string> { "" };
        protected static List<string> ListOfBars= new List<string> { "" };
        protected static List<string> ListOfRoomsAffected = new List<string> { "" };

        private readonly By _backBtn = By.XPath("//p-button[@label='Back']");
        private readonly By _continueBtn = By.XPath("//p-button[@label='Continue']//button");
        private readonly By _confirmDeleteDialog=By.XPath("//p-confirmdialog[@header='Confirmation']");
        protected readonly By _saveAndClose = By.XPath("//p-button[@id='save']//button");


        public void VerifyContinueFormBtnState(string visibility, string enabledStatus)
        {
            VerifyElementVisibility(visibility, _continueBtn);
            VerifyElementState(enabledStatus, _continueBtn);
        }

        public void VerifySaveCloseFormBtnState(string visibility, string enabledStatus)
        {
            VerifyElementVisibility(visibility, _saveAndClose);
            VerifyElementState(enabledStatus, _saveAndClose);
        }

        public void ClickContinueButton()
        {
            Driver.ClickItem(_continueBtn);
        }


        public void ClickBackBtn()
        {
            Driver.ClickItem(_backBtn);

        }


        /// <summary>
        ///     Verifies user is not allowed to enter a date in the past or future depending on the pastFuture param
        /// </summary>
        /// <param name="pastFuture">past or future date to verify</param>
        /// <param name="locator">By element of calendar</param>
        public void VerifyCanNotSelectDate(string pastFuture, By locator)
        {
            DateTime dateFormat;

            bool isCalendarEnabled;
            switch (pastFuture.ToLower())
            {
                case "future":
                    var date = Driver.CalculateFutureOrPastDate("05");
                    dateFormat = Driver.ParseDateTo_ddmmyyyy(date);
                    isCalendarEnabled = Driver.IsDateInCalenderEnabled(locator, dateFormat);
                    Assert.True(!isCalendarEnabled,
                        "Unable to verify the calendar being disabled for the future dates. ");
                    break;
                case "past":
                    dateFormat = Driver.ParseDateTo_ddmmyyyy("02/07/2018");
                    isCalendarEnabled = Driver.IsDateInCalenderEnabled(locator, dateFormat);
                    Assert.True(!isCalendarEnabled,
                        "Unable to verify the calendar being disabled for the past dates. ");
                    break;

                default:
                     dateFormat = Driver.ParseDateTo_ddmmyyyy(pastFuture);
                     isCalendarEnabled = Driver.IsDateInCalenderEnabled(locator, dateFormat);
                    Assert.True(!isCalendarEnabled,  "Unable to verify the calendar being disabled for the past dates. ");
                    break;
            }
        }

        /// <summary>
        ///     Selects Date from building work calendar picker
        /// </summary>
        /// <param name="calendarDatePicker"> by element for the calendar picker</param>
        /// <param name="dateToSelect">Date to select</param>
        /// <param name="dateInFuture">Calculate future date</param>
        public void SelectBwDateFromCalendar(By calendarDatePicker, string dateToSelect, string dateInFuture = "0")
        {
            var date = Driver.CalculateDate(dateToSelect, dateInFuture);
            Driver.SelectDateFromCalender(calendarDatePicker, date);
        }

        /// <summary>
        /// Verifies the validation error message is displayed on the fields 
        /// </summary>
        /// <param name="locator">Field locator to look for </param>
        /// <param name="expectedMessage">Expected Error message text</param>
        protected void VerifyBwFieldValidationErrorMessage( By locator, string expectedMessage)
        {
            var actualMessage = Driver.GetText(locator);
            Assert.AreEqual(expectedMessage, actualMessage, $"Unable to verify expected message {expectedMessage} same as actual message {actualMessage} ");
        }

       


    }
}