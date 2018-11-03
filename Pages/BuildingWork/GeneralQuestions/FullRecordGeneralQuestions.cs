using System;
using System.Globalization;
using System.Linq;
using J2BIOverseasOps.Extensions;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Pages.BuildingWork.GeneralQuestions
{
    internal class FullRecordGeneralQuestions : FullRecordOverviewComponent
    {
        private readonly ApiCalls _apiCall;

        private readonly By _bwRecordTypePSelect = By.XPath("//p-selectbutton[@id='fullOrSkeletonRecord']");

        // select property
        private readonly By _destinationDrpDwn = By.XPath("//p-dropdown[@id='destinationSearch']");
        private readonly By _resortDrpDwn = By.XPath("//p-dropdown[@id='resortSearch']");
        private readonly By _propertyDropdwn = By.XPath("//p-dropdown[@id='propertySearch']");

        //Full/Skeleton record
        private readonly By _skeletonMandaroyValidation = By.XPath("//p-message[@id='fullOrSkeletonRecord-validation']");

        // info by
        private readonly By _infoProvidedByJobTitle = By.Id("providedByRole");
        private readonly By _infoProvidedByJobTitleValidation = By.Id("providedByRole-validation");
        private readonly By _infoProvidedByName = By.Id("providedByName");
        private readonly By _infoProvidedByNameValidation = By.Id("providedByName-validation");

        //work dates
        private readonly By _hasTheWorkStarted = By.XPath("//p-selectbutton[@id='hasWorkStarted']");
        private readonly By _hasTheWorkStartedValidation = By.Id("hasWorkStarted-validation");
        private readonly By _workStartDateValidation = By.Id("workStartDate-validation");
        private readonly By _workCompletionDateValidation = By.Id("workEstimatedCompletionDate-validation");
        private readonly By _estimatedCompletionCalendar = By.Id("workEstimatedCompletionDate");
        private readonly By _estimatedCompletionInput = By.XPath("//*[@id='workEstimatedCompletionDate']//input");

        private readonly By _whereTheWorkTakingPlaceDrpDwn = By.XPath("//p-dropdown[@id='whereTakingPlace']");
        private readonly By _whereIsTheWorkValidation = By.Id("whereTakingPlace-validation");

        // phases
        private readonly By _phasesComponent = By.XPath("//p-card[@class='Phase1']");
        private readonly By _removePhasesButtons = By.XPath("//*[contains(@class,'Phase')]//button[@id='removeBtn']");
        private readonly string _phasePcardParentXpath = "//p-card[@class='phasenumber']";
        private readonly string _phaseDetailsXpath = "//textarea";
        private readonly string _phaseFromDateXpath = "//*[contains(@class,'fromDate')]";
        private readonly string _phaseFromDateInputXpath = "//*[contains(@class,'fromDate')]//input";
        private readonly string _phaseFromDateValidationXpath = "//p-message[@id='fromDate-validation']//span[2]";
        private readonly string _phaseUntilDateXpath = "//*[contains(@class,'toDate')]";
        private readonly string _phaseUntilDateInputXpath = "//*[contains(@class,'toDate')]//input";
        private readonly string _phaseUntilDateValidationXpath = "//p-message[@id='toDate-validation']//span[2]";
        private readonly string _removePhaseButtonXpath = "//*[@id='removeBtn']";
        private readonly string _addPhaseButtonXpath = "//*[@id='addBtn']";
        private readonly By _theWorkStartedCalendar = By.Id("workStartDate");
        private readonly By _theWorkStartedInput = By.XPath("//*[@id='workStartDate']//input");
        private readonly By _workCompletedInPhasesPselect = By.Id("hasBuildingWorkStartedInPhases");

        // time and day of work
        private readonly string _bwTimeWorkHourPCardClass = "ApproximateWorkHours";
        private const string BwTimeFrom = "//p-calendar[contains(@class,'fromTime')]";
        private const string BwTimeUntil = "//p-calendar[contains(@class,'toTime')]";
        private const string BwDayFrom = "//p-dropdown[contains(@class,'fromDay')]";
        private const string BwDayUntil = "//p-dropdown[contains(@class,'toDay')]";
        private const string AddBwTimeAndDaySchedule = "//button[@id='addBtn']";
        private const string RemoveBwTimeAndDaySchedule = "//button[@id='removeBtn']";

        // Hotel OpenClosed
        private readonly By _pSelectButtonIsHotelOpen = By.XPath("//p-selectbutton[@id='isHotelOpenDuringWorkOptions']");

        private readonly By _hotelClosedFromCalendar = By.XPath("//p-calendar[@id='hotel_closed_fromDate']");
        private readonly By _hotelClosedFromValidation = By.XPath("//p-message[@id='fromDate-validation']//span[2]");
        private readonly By _hotelClosedUntil = By.XPath("//p-calendar[@id='hotel_closed_toDate']");
        private readonly By _hotelClosedUntilValidation = By.XPath("//p-message[@id='toDate-validation']//span[2]");

        //Areas of Property Affected
        private readonly By _drpDwnAreaOfPropertyAffected = By.XPath("//p-multiselect[@id='areasAffected']");

        private readonly By _areasOfPropertyAffectedValidation =
            By.XPath("//p-message[@id='areasAffected-validation']//span[2]");

        //invalid range
        private readonly By _dateRangInvalidWarning = By.XPath("//*[@id='dataRangeInvalid']//span[@class='ui-message-text']");

        // mark as urgent
        private readonly By _pSelectUrgent = By.XPath("//p-selectbutton[@id='markAsUrgent']");
        private readonly By _markAsUrgentValidation = By.XPath("//p-message[@id='markAsUrgent-validation']//span[2]");

        // description of work
        private readonly By _descriptionOfWork = By.XPath("//textarea[@id='descriptionOfBuildingWork']");

        private readonly By _descriptionOfWorkValidation = By.XPath("//p-message[@id='descriptionOfBuildingWork-validation']//span[2]");


        public FullRecordGeneralQuestions(IWebDriver driver, ILog log, IRunData rundata) : base(driver, log, rundata)
        {
            _apiCall = new ApiCalls(rundata);
        }

        // select property
        public void SelectBwProperty(string dest, string resrt, string prop)
        {
            Destination = dest;
            Resort = resrt;
            Property = prop;
            Driver.SelectDropDownOption(_destinationDrpDwn, dest);
            Driver.SelectDropDownOption(_resortDrpDwn, Resort);
            Driver.SelectDropDownOption(_propertyDropdwn, Property);
        }

        public void SelectBwRecordType(string fullSkeleton)
        {
            Driver.ClickPSelectOption(_bwRecordTypePSelect, fullSkeleton);
        }

        public void EnterAnswerToGeneralQ(Table table)
        {
            foreach (var row in table.Rows)
            {
                var question = row["question"];
                var answer = row["answer"];
                EnterBwGeneralQuestionsAnswer(question, answer);
            }
        }


        public void VerifyAnswerToGeneralQuestions(Table table)
        {
            foreach (var row in table.Rows)
            {
                var question = row["question"];
                var answer = row["expected_answer"];
                VerifyBwGeneralQuestionsAnswer(question, answer);
            }
        }

        public void DeselectAnswertoGeneralQ(Table table)
        {
            foreach (var row in table.Rows)
            {
                var question = row["question"];
                var answer = row["answer"];

                switch (question.ToLower())
                {
                    case "areas of property affected":
                        Driver.DeselectMultiselectOption(_drpDwnAreaOfPropertyAffected, answer.ConvertStringIntoList());
                        return;
                    default:
                        Assert.Fail($"{question} is not a valid general question");
                        return;
                }

            }
        }

        public void VerifyGeneralQValidationErrorMessage(Table table)
        {
            foreach (var row in table.Rows)
            {
                var field = row["field"];
                var expectedError = row["error"];
                switch (field.ToLower())
                {
                    case "info provided by name":
                        VerifyBwFieldValidationErrorMessage(_infoProvidedByNameValidation, expectedError);
                        break;
                    case "info provided by job title":
                        VerifyBwFieldValidationErrorMessage(_infoProvidedByJobTitleValidation, expectedError);
                        break;
                    case "has the work started":
                        VerifyBwFieldValidationErrorMessage(_hasTheWorkStartedValidation, expectedError);
                        break;
                    case "work start date":
                        VerifyBwFieldValidationErrorMessage(_workStartDateValidation, expectedError);
                        break;
                    case "estimated completion date":
                        VerifyBwFieldValidationErrorMessage(_workCompletionDateValidation, expectedError);
                        break;
                    case "will the work be completed in more than one phases":
                        break;
                    case "where is the building work":
                        VerifyBwFieldValidationErrorMessage(_whereIsTheWorkValidation, expectedError);
                        break;
                    case "areas of property affected":
                        VerifyBwFieldValidationErrorMessage(_areasOfPropertyAffectedValidation, expectedError);
                        break;
                    case "hotel closed from":
                        VerifyBwFieldValidationErrorMessage(_hotelClosedFromValidation, expectedError);
                        break;
                    case "hotel closed to":
                        VerifyBwFieldValidationErrorMessage(_hotelClosedUntilValidation, expectedError);
                        break;
                    case "mark as urgent":
                        VerifyBwFieldValidationErrorMessage(_markAsUrgentValidation, expectedError);
                        break;
                    case "description of work":
                        VerifyBwFieldValidationErrorMessage(_descriptionOfWorkValidation, expectedError);
                        break;
                    case "full or skeleton":
                        VerifyBwFieldValidationErrorMessage(_skeletonMandaroyValidation, expectedError);
                        break;
                    default:
                        Assert.Fail($"{field} is not a valid field");
                        return;

                }
            }
        }

        public void ClearGeneralQField(string question)
        {
            switch (question.ToLower())
            {
                case "info provided by name":
                    Driver.Clear(_infoProvidedByName);
                    return;
                case "info provided by job title":
                    Driver.Clear(_infoProvidedByJobTitle);
                    return;
                default:
                    Assert.Fail($"{question} is not a valid general question");
                    return;
            }
        }

        public void VerifyGeneralQuestionCalendarValidation(string pastFuture, string question)
        {
            switch (question.ToLower())
            {
                case "work start date":
                    VerifyCanNotSelectDate(pastFuture, _theWorkStartedCalendar);
                    break;
                case "estimated completion date":
                    VerifyCanNotSelectDate(pastFuture, _estimatedCompletionCalendar);
                    break;
                default:
                    Assert.Fail($"{question} is not a valid general question");
                    return;
            }
        }


        public void VerifyPhasesDisplayed(string isDisplayed)
        {
            switch (isDisplayed.ToLower())
            {
                case "is":
                    Assert.True(Driver.WaitForItem(_phasesComponent, 5),
                        $"Unable to verify if the phases are displayed");
                    break;
                case "is not":
                    Assert.True(!Driver.WaitForItem(_phasesComponent, 1),
                        $"Phases are displayed while expecting it not to be there");
                    break;
                default:
                    Assert.Fail($"{isDisplayed} not a valid option, expecting options is, is not");
                    break;
            }
        }

        // verify data in the phases
        public void VerifyPhasesInfo(Table table)
        {
            foreach (var row in table.Rows)
            {
                var expectedPhaseName = row["phase"];
                var expectedFromDate = row["from"];
                var expectedToDate = row["until"];
                var expectedPhaseDetails = row["details"];


                // calculate date and convert to string
                var expectedFromDateString = "";
                var expectedToDateString = "";
                if (expectedFromDate != "")
                {
                    expectedFromDateString = Driver.CalculateDate(expectedFromDate).ToString("dd/MM/yyyy");
                }

                if (expectedToDate != "")
                {
                    expectedToDateString = Driver.CalculateDate(expectedToDate).ToString("dd/MM/yyyy");
                }

                // Generate dynamic xpath
                var phaseFromDate = CreateDynamicXpathForPhase(expectedPhaseName, _phaseFromDateInputXpath);
                var phaseToDate = CreateDynamicXpathForPhase(expectedPhaseName, _phaseUntilDateInputXpath);
                var phaseDetails = CreateDynamicXpathForPhase(expectedPhaseName, _phaseDetailsXpath);

                // get text from the fields
                var actualPhaseFromDate = Driver.GetInputBoxValue(phaseFromDate);
                var actualPhaseToDate = Driver.GetInputBoxValue(phaseToDate);
                var actualPhaseDetails = Driver.GetInputBoxValue(phaseDetails);

                //Assert
                Assert.AreEqual(expectedFromDateString, actualPhaseFromDate,
                    $"Expected phase from date {expectedFromDate} was not equal to Actual from date {actualPhaseFromDate}");
                Assert.AreEqual(expectedToDateString, actualPhaseToDate,
                    $"Expected phase to date {expectedToDate} was not equal to Actual phase to date {actualPhaseToDate}");
                Assert.AreEqual(expectedPhaseDetails, actualPhaseDetails,
                    $"Expected phase details {expectedPhaseDetails} was not equal to Actual phase details {actualPhaseDetails}");
            }
        }

        public void VerifyUnableToEditPhaseStartDate(string startEndDateField, string phaseName)
        {
            switch (startEndDateField)
            {
                case "from":
                    CreateDynamicXpathForPhase(phaseName, _phaseFromDateXpath);
                    break;
                case "until":
                    CreateDynamicXpathForPhase(phaseName, _phaseUntilDateXpath);
                    break;
            }
        }

        /// <summary>
        /// verifies the from/until date on phases can not be selected
        /// </summary>
        /// <param name="fromUntil">from or until date to select</param>
        /// <param name="date"> date to be verified,e.g.  10/07/2017 or 10 days in future</param>
        /// <param name="phaseName">phase name tp select</param>
        public void VerifyPhasesDateCanNotBeSelected(string fromUntil, string date, string phaseName)
        {
            By xpathForPhaseDate = null;
            switch (fromUntil.ToLower())
            {
                case "from":
                    xpathForPhaseDate = CreateDynamicXpathForPhase(phaseName, _phaseFromDateXpath);
                    break;
                case "until":
                    xpathForPhaseDate = CreateDynamicXpathForPhase(phaseName, _phaseUntilDateXpath);
                    break;
            }

            var dateToSelect = Driver.CalculateDate(date).ToString("dd/MM/yyyy", CultureInfo.CurrentCulture);
            VerifyCanNotSelectDate(dateToSelect, xpathForPhaseDate);
        }

        public void VerifyValidationOnPhaseFields(string expectedText, string field, string phaseName)
        {
            By xpathForPhaseDateValidation = null;
            switch (field.ToLower())
            {
                case "from":
                    xpathForPhaseDateValidation = CreateDynamicXpathForPhase(phaseName, _phaseFromDateValidationXpath);
                    break;
                case "until":
                    xpathForPhaseDateValidation = CreateDynamicXpathForPhase(phaseName, _phaseUntilDateValidationXpath);
                    break;
            }

            var actualText = Driver.GetText(xpathForPhaseDateValidation);

            Assert.AreEqual(expectedText, actualText,
                $"Unable to verify the validation error message {expectedText} is displayed for field {field}, and {phaseName} ");
        }



        public void EnterPhasesInfo(Table table)
        {
            foreach (var row in table.Rows)
            {
                var phaseName = row["phase"];
                var fromDate = row["from"];
                var untilDate = row["until"];
                var details = row["details"];

                if (fromDate != string.Empty)
                {
                    var phaseFromDate = CreateDynamicXpathForPhase(phaseName, _phaseFromDateXpath);
                    Driver.ScrollElementToTheMiddle(phaseFromDate);
                    SelectBwDateFromCalendar(phaseFromDate, fromDate);

                }

                if (untilDate != string.Empty)
                {
                    var phaseToDate = CreateDynamicXpathForPhase(phaseName, _phaseUntilDateXpath);
                    Driver.ScrollElementToTheMiddle(phaseToDate);
                    SelectBwDateFromCalendar(phaseToDate, untilDate);
                }

                if (details != string.Empty)
                {
                    var phaseDetails = CreateDynamicXpathForPhase(phaseName, _phaseDetailsXpath);
                    Driver.ScrollElementToTheMiddle(phaseDetails);
                    Driver.EnterText(phaseDetails, details);
                }
            }
        }


        public void ClickAddRemovePhaseBtn(string addRemove, string phaseName)
        {
            var btnXpath = "";
            switch (addRemove.ToLower())
            {
                case "add":
                    btnXpath = _addPhaseButtonXpath;
                    break;
                case "remove":
                    btnXpath = _removePhaseButtonXpath;
                    break;
            }

            var btnToClick = CreateDynamicXpathForPhase(phaseName, btnXpath);
            Driver.ClickItem(btnToClick,true);
        }

        public void VerifyAddRemovePhaseButtonsState(string button, string enabledState, string phaseName)
        {
            By buttonName = null;
            switch (button.ToLower())
            {
                case "add":
                    buttonName = CreateDynamicXpathForPhase(phaseName, _addPhaseButtonXpath);
                    break;
                case "remove":
                    buttonName = CreateDynamicXpathForPhase(phaseName, _removePhaseButtonXpath);
                    break;

            }

            VerifyElementState(enabledState, buttonName);
        }

        public void VerifyAllRemovePhaseButtonsState(string enabledDisabled)
        {
            var removeButtonsList = Driver.FindElements(_removePhasesButtons);
            foreach (var removeButton in removeButtonsList)
            {
                VerifyElementState(enabledDisabled, removeButton);
            }
        }

        public void NoteDownFieldValues(Table table)
        {
            foreach (var row in table.Rows)
            {
                var fieldName = row["field"];
                switch (fieldName.ToLower())
                {
                    case "work_start_date":
                        ScenarioContext.Current[WrkStartDateConextKey] =
                            Driver.GetInputBoxValue(_theWorkStartedInput).Trim();
                        break;
                    case "completion_date":
                        ScenarioContext.Current[WrkCompletionDateContextKey] =
                            Driver.GetInputBoxValue(_estimatedCompletionInput).Trim();
                        break;
                    default:
                        Assert.Fail($"{fieldName} is not a valid fieldname");
                        break;
                }
            }
        }

        public void EnterTimeDaysOfBw(Table table)
        {
            var timeSlotNumber = 1;
            foreach (var row in table.Rows)
            {
                var timeFrom = row["time_from"];
                var timeUntil = row["time_until"];
                var dayFrom = row["day_from"];
                var dayUntil = row["day_until"];

                // create xpath
                var bwTimeFromElem = CreateDynamicXpathForTimeSlot(timeSlotNumber, BwTimeFrom);
                var bwTimeToElem = CreateDynamicXpathForTimeSlot(timeSlotNumber, BwTimeUntil);
                var bwDayFromElem = CreateDynamicXpathForTimeSlot(timeSlotNumber, BwDayFrom);
                var bwDayToElem = CreateDynamicXpathForTimeSlot(timeSlotNumber, BwDayUntil);

                var timeFromUpdated = DateTime.ParseExact(timeFrom, "HH:mm", CultureInfo.CurrentCulture);
                var timeUntilUpdated = DateTime.ParseExact(timeUntil, "HH:mm", CultureInfo.CurrentCulture);

                var timeSlotParentString = _bwTimeWorkHourPCardClass + timeSlotNumber;
                var timeSlotParentXpath = $"//*[contains(@class,'{timeSlotParentString}')]";
                var timeSlotParentElem = By.XPath(timeSlotParentXpath);

                Driver.SelectTimeFromCalender(bwTimeFromElem, timeSlotParentElem, timeFromUpdated);
                Driver.SelectTimeFromCalender(bwTimeToElem, timeSlotParentElem, timeUntilUpdated);

                Driver.SelectDropDownOption(bwDayFromElem, dayFrom);
                Driver.SelectDropDownOption(bwDayToElem, dayUntil);
                timeSlotNumber++;
            }
        }

        public void AddRemoveTimeDayBwSchedule(string addRemove, int slotNumber)
        {
            var btnToClick = "";
            switch (addRemove.ToLower())
            {
                case "add":
                    btnToClick = AddBwTimeAndDaySchedule;
                    break;
                case "remove":
                    btnToClick = RemoveBwTimeAndDaySchedule;
                    break;

            }

            var btnXpath = CreateDynamicXpathForTimeSlot(slotNumber, btnToClick);
            Driver.ClickItem(btnXpath, true);

        }

        public void VerifyAddRemoveBwTimeStatus(string addRemoveBtn, string btnEnabledDisabled, int slotNumber)
        {
            var btnToVerify = "";
            switch (addRemoveBtn.ToLower())
            {
                case "add":
                    btnToVerify = AddBwTimeAndDaySchedule;
                    break;
                case "remove":
                    btnToVerify = RemoveBwTimeAndDaySchedule;
                    break;
            }

            var btnXpath = CreateDynamicXpathForTimeSlot(slotNumber, btnToVerify);
            VerifyElementState(btnEnabledDisabled, btnXpath);

        }

        public void HotelClosedDateDisplayed(string displayedNotDisplayed)
        {
            switch (displayedNotDisplayed)
            {
                case "displayed":
                    Assert.True(Driver.WaitForItem(_hotelClosedFromCalendar, 5),
                        "Could not find Hotel closed calendar items.");
                    break;
                case "not displayed":
                    Assert.True(!Driver.WaitForItem(_hotelClosedFromCalendar, 2),
                        "Found Hotel closed calendar items, while expecting it to be there.");
                    break;
            }
        }

        public void VerifyAreasOfPropertyQDisplayedOrNot(string displayedNotDisplayed)
        {
            var status = displayedNotDisplayed == "displayed";
            Assert.AreEqual(status,Driver.WaitForItem(_drpDwnAreaOfPropertyAffected,1));
        }

        public void VerifyHotelClosedDatesCantBeSelected(string fromUntil, string date)
        {
            By datePicker = null;
            switch (fromUntil)
            {
                case "from":
                    datePicker = _hotelClosedFromCalendar;
                    break;
                case "until":
                    datePicker = _hotelClosedUntil;
                    break;
            }

            var dateToSelect = Driver.CalculateDate(date).ToString("dd/MM/yyyy");
            VerifyCanNotSelectDate(dateToSelect, datePicker);
        }

        public void SelectHotelClosedDates(string fromUntil, string date)
        {
            By datePicker = null;
            switch (fromUntil)
            {
                case "from":
                    datePicker = _hotelClosedFromCalendar;
                    break;
                case "until":
                    datePicker = _hotelClosedUntil;
                    break;
            }

            var dateToSelect = Driver.CalculateDate(date).ToString("dd/MM/yyyy");
            SelectBwDateFromCalendar(datePicker, dateToSelect);

        }


        public void VerifyListOfPropertyAreasAffected(Table table)
        {
            var actualList = Driver.GetAllMultiselectOptions(_drpDwnAreaOfPropertyAffected);
            var expectedList = table.Rows.ToColumnList("expected_options");
            CollectionAssert.AreEqual(actualList, expectedList,
                $"The actual Areas of Property: {actualList} do not match the expected Areas of Property: {expectedList}");
        }


        public void VerifyAnswersToQuestions(Table table)
        {
            foreach (var row in table.Rows)
            {
                var question = row["question"];
                var expectedAnswer = row["expectedanswer"];
                switch (question.ToLower())
                {
                    case "mark as urgent":
                        var actualAnswer = Driver.GetSelectedPOption(_pSelectUrgent);
                        Assert.AreEqual(actualAnswer, expectedAnswer,
                            $"Expected answer {expectedAnswer} for the 'mark as urgent' question is not the same as Actual answer {actualAnswer}");
                        return;
                    case "mark as urgent selected":
                        var isSelected = Driver.IsPOptionSelected(_pSelectUrgent);
                        Assert.AreEqual(isSelected.ToString(), expectedAnswer,
                            $"Expected answer {expectedAnswer} for the 'mark as urgent selected' question is not the same as Actual answer {isSelected}");
                        return;
                    default:
                        Assert.Fail($"{question} is not a valid general question");
                        return;
                }
            }
        }

        #region Helpers

        private void EnterBwGeneralQuestionsAnswer(string question, string answer)
        {
            switch (question.ToLower())
            {
                case "info provided by name":
                    Driver.EnterText(_infoProvidedByName, answer);
                    return;
                case "info provided by job title":
                    Driver.EnterText(_infoProvidedByJobTitle, answer);
                    return;
                case "has the work started":
                    SelectHastheWorkStarted(answer);
                    return;
                case "work start date":
                    SelectWorkStartDate(answer);
                    return;
                case "estimated completion date":
                    SelectEstimatedCompletionDate(answer);
                    return;
                case "will the work be completed in more than one phases":
                    SelectWillTheWorkBeCompletedInPhases(answer);
                    return;
                case "where is the building work":
                    Driver.SelectDropDownOption(_whereTheWorkTakingPlaceDrpDwn, answer);
                    return;
                case "areas of property affected":
                    Driver.SelectMultiselectOption(_drpDwnAreaOfPropertyAffected, answer.ConvertStringIntoList());
                    return;
                case "hotel open during work":
                    Driver.ClickPSelectOption(_pSelectButtonIsHotelOpen, answer);
                    return;
                case "hotel closed from":
                    SelectHotelClosedDates("from", answer);
                    break;
                case "hotel closed until":
                    SelectHotelClosedDates("until", answer);
                    return;
                case "mark as urgent":
                    Driver.ClickPSelectOption(_pSelectUrgent, answer);
                    return;
                case "description of work":
                    Driver.EnterText(_descriptionOfWork, answer);
                    break;
                default:
                    Assert.Fail($"{question} is not a valid general question");
                    return;
            }
        }

        private void VerifyBwGeneralQuestionsAnswer(string question, string answer)
        {
            switch (question.ToLower())
            {
                case "record type":
                    Driver.VerifySingleSelectedPOption(_bwRecordTypePSelect,answer);
                    break;
                case "info provided by name":
                    return;
                case "info provided by job title":
                    return;
                case "has the work started":
                    return;
                case "work start date":
                    return;
                case "estimated completion date":
                    return;
                case "will the work be completed in more than one phases":
                    Driver.VerifySingleSelectedPOption(_workCompletedInPhasesPselect, answer);
                    return;
                case "where is the building work" :
                    Driver.VerifySelectedDropDownOption(_whereTheWorkTakingPlaceDrpDwn,answer);
                    return;
                case "areas of property affected":
                   Driver.VerifyAllSelectedMultiselectOptions(_drpDwnAreaOfPropertyAffected, answer.ConvertStringIntoList());
                    return;
                case "hotel open during work":
                    return;
                case "hotel closed from":
                    break;
                case "hotel closed until":
                    return;
                case "mark as urgent":
                    return;
                case "description of work":
                    break;
                default:
                    Assert.Fail($"{question} is not a valid general question");
                    return;
            }
        }



        private void SelectWorkStartDate(string dateToSelect)
        {
            Driver.ClickItem(_theWorkStartedCalendar, true);
            SelectBwDateFromCalendar(_theWorkStartedCalendar, dateToSelect);
        }


        private void SelectEstimatedCompletionDate(string dateToSelect)
        {
            Driver.ClickItem(_estimatedCompletionCalendar, true);
            SelectBwDateFromCalendar(_estimatedCompletionCalendar, dateToSelect);
        }

        private void SelectHastheWorkStarted(string hasTheWorkStarted)
        {
            switch (hasTheWorkStarted.ToLower())
            {
                case "yes":
                        Driver.ClickPSelectOption(_hasTheWorkStarted, "Yes");
                    break;
                case "no":
                        Driver.ClickPSelectOption(_hasTheWorkStarted, "No");
                    break;
                default:
                    Assert.Fail($"{hasTheWorkStarted} is not a valid option, valid options: yes/no");
                    break;
            }
        }

        private void SelectWillTheWorkBeCompletedInPhases(string yesNo)
        {
                switch (yesNo.ToLower())
                {
                    case "yes":
                        Driver.ClickPSelectOption(_workCompletedInPhasesPselect, "Yes");
                        Driver.WaitForItem(_phasesComponent);
                        break;
                    case "no":
                        Driver.ClickPSelectOption(_workCompletedInPhasesPselect, "No");
                        break;
                    default:
                        Assert.Fail($"{yesNo} is not a valid option, valid options: yes/no");
                        break;
                }
        }

        /// <summary>
        /// Creates the dynamic xpath for the phases 
        /// </summary>
        /// <param name="phaseName"> phase name e.g, Phase 1</param>
        /// <param name="childElement">child element xpath within the phase e.g. //p-calendar[@id='toDate'] </param>
        /// Return will be e.g. //p-card[@class='Phase1']//p-calendar[@id='toDate']
        /// <returns></returns>
        private By CreateDynamicXpathForPhase(string phaseName, string childElement)
        {
            var phaseNameTrimmed = phaseName.Trim().Replace(" ", "");
            var phaseXpath = _phasePcardParentXpath.Replace("phasenumber", phaseNameTrimmed); // parent xpath
            return By.XPath(phaseXpath + childElement);
        }


        /// <summary>
        /// Creates the dynamic xpath for the the Time slot 
        /// </summary>
        /// <param name="timeSlotNumber"> phase name e.g, 1</param>
        /// <param name="childElement">child element xpath within the phase e.g. [@id='toDate'] </param>
        /// Return will be e.g. //*[@class='ApproximateWorkHours1'//*[@class='toDate']
        /// <returns></returns>
        private By CreateDynamicXpathForTimeSlot(int timeSlotNumber, string childElement)
        {
            var timeSlotParent = _bwTimeWorkHourPCardClass + timeSlotNumber;
            var timeSlotParentXpath = $"//*[contains(@class,'{timeSlotParent}')]";
            var timeSlot = By.XPath(timeSlotParentXpath + childElement);
            return timeSlot;
        }

        #endregion

        public void VerifyBwPhasesWarningDisplayed(string displayedNotDisplayed, string message)
        {
            switch (displayedNotDisplayed)
            {
                case "displayed":
                    var actualText = Driver.GetText(_dateRangInvalidWarning);
                    Assert.AreEqual(actualText, message,
                        $"Expected text {message} was not equal to Actual Text {actualText}");
                    break;
                case "not displayed":
                    Assert.True(!Driver.WaitForItem(_dateRangInvalidWarning, 2),
                        $"Was able to find {_dateRangInvalidWarning} when expecting it not to be there");
                    break;
            }

        }

        public void VerifyGeneralQValidationErrorMessageNotDisplayed(Table table)
        {
            foreach (var row in table.Rows)
            {
                var field = row["field"];

                switch (field.ToLower())
                {
                    case "work start date":
                        Assert.True(!Driver.WaitForItem(_workStartDateValidation, 1),
                            $"Was able to find mandatory error message for {field} when expecting it not to be there");
                        break;
                    default:
                        Assert.Fail($"{field} is not a valid field");
                        return;
                }
            }
        }

        // verifies the results for a random hotel from the data
        public void VerifyCommitmentLevels()
        {
            var listOfCommitments = _apiCall.GetListOfCommitmentsForProperty(Destination, Property);
            Assert.True(listOfCommitments.Count > 0,"Number of commitments returned back for the given property are 0");
                var commitment = listOfCommitments[4];
                var commitmentStartDate = commitment.StartDate;
                var commitmentEndDate = commitment.EndDate;
                SelectHastheWorkStarted(commitmentStartDate > DateTime.Today.ToLocalTime() ? "No" : "Yes"); // select Yes No if the date is in future
                SelectWorkStartDate(commitmentStartDate.ToString("dd/MM/yyyy"));
                SelectEstimatedCompletionDate(commitmentEndDate.ToString("dd/MM/yyyy"));
                var expectedCommitmentLevel = ($"{commitment.Period}-{commitment.Applicability}").Replace(" ","");
                var actualCommitmentLevelOverview = Driver.GetText(_commitmentLevelField).Replace(" ", "");
                Assert.AreEqual(expectedCommitmentLevel, actualCommitmentLevelOverview, $"Expected commitment level {expectedCommitmentLevel} was not same as {actualCommitmentLevelOverview}");

                ClickNavItem("Summary");
                Driver.VerifyNavigatedToPage("/summary");
                var actualCommitmentLevelSummary = Driver.GetText(_commitmentLevelField).Replace(" ", "");
                Assert.AreEqual(expectedCommitmentLevel, actualCommitmentLevelSummary, $"Expected commitment level {expectedCommitmentLevel} was not same as {actualCommitmentLevelSummary} on summary page");
                ClickNavItem("Start");
                Driver.VerifyNavigatedToPage("building-works/form");
        }
}
}

