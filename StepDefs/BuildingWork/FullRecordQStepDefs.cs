using J2BIOverseasOps.Pages.BuildingWork.GeneralQuestions;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.BuildingWork
{
    [Binding]
    public sealed class FullRecordQStepDefs : BaseStepDefs
    {
        private readonly FullRecordGeneralQuestions _generalQuestions;

        public FullRecordQStepDefs(IWebDriver driver, ILog log,IRunData rundata) : base(driver, log)
        {
            _generalQuestions = new FullRecordGeneralQuestions(driver, log,rundata);
        }

        [When(@"I select the Destination as ""(.*)"", Resort as ""(.*)"" and property as ""(.*)"" on building works property search")]
        public void SelectBwProperty(string destination ,string resort, string property)
        {
            _generalQuestions.SelectBwProperty(destination, resort, property);
        }

        [When(@"I select the building work record as ""(Full|Skeleton)""")]
        public void WhenISelectTheBuildingWorkRecordAs(string fullSkeleton)
        {
            _generalQuestions.SelectBwRecordType(fullSkeleton);
        }


        [Then(@"I am presented with a help text containing the text ""(.*)""")]
        public void ThenIAmPresentedWithAHelpTextContainingTheText(string expectedText)
        {
            _generalQuestions.VerifyHelpText(expectedText);
        }


        [Then(@"I can enter the following answers for the following General Questions:")]
        [When(@"I enter the following answers for the following General Questions:")]
        public void WhenIEnterTheFollowingAnswersForTheFollowingGeneralQuestions(Table table)
        {
            _generalQuestions.EnterAnswerToGeneralQ(table);
        }

        [Then(@"I verify the answers for the following General Questions:")]
        public void ThenIVerifyTheAnswersForTheFollowingGeneralQuestions(Table table)
        {
            _generalQuestions.VerifyAnswerToGeneralQuestions(table);
        }


        [When(@"I deselect the following answers for the following General Questions:")]
        public void WhenIDeselectTheFollowingAnswersForTheFollowingGeneralQuestions(Table table)
        {
            _generalQuestions.DeselectAnswertoGeneralQ(table);
        }

        [Then(@"I verify the data for the following General Questions:")]
        public void ThenIVerifyTheDataForTheFollowingGeneralQuestions(Table table)
        {
            _generalQuestions.VerifyAnswersToQuestions(table);
        }


        [Then(@"I can see the following mandatory error message on the following General Question fields:")]
        public void ThenICanSeeTheFollowingMandatoryErrorMessageOnTheFollowingGeneralQuestionFields(Table table)
        {
            _generalQuestions.VerifyGeneralQValidationErrorMessage(table);
        }

        [Then(@"I can not see the following mandatory error message on the following General Question fields:")]
        public void ThenICanNotSeeTheFollowingMandatoryErrorMessageOnTheFollowingGeneralQuestionFields(Table table)
        {
            _generalQuestions.VerifyGeneralQValidationErrorMessageNotDisplayed(table);
        }


        [When(@"I clear the field ""(.*)"" on the General Questions page")]
        public void WhenIClearTheFieldOnTheGeneralQuestionsPage(string question)
        {
            _generalQuestions.ClearGeneralQField(question);
        }

        [Then(
            @"I verify the Continue form button is ""(visible|invisible)"" and ""(disabled|enabled)"" on the General Questions page")]
        public void ThenIVerifyTheContinueFormButtonIsAndOnTheGeneralQuestionsPage(string visibility, string isEnabled)
        {
            _generalQuestions.VerifyContinueFormBtnState(visibility, isEnabled);
        }

        [Then(@"I verify the save and close form button is ""(.*)"" and ""(.*)"" on the General Questions page")]
        public void ThenIVerifyTheSaveAndContinueFormButtonIsAndOnTheGeneralQuestionsPage(string visibility, string isEnabled)
        {
            _generalQuestions.VerifySaveCloseFormBtnState(visibility, isEnabled);
        }


        [When(@"I click the continue button on the building work page")]
        public void WhenIClickTheContinueButtonOnTheBuildingWorkPage()
        {
            _generalQuestions.ClickContinueButton();
        }

        [When(@"I click the back button on the building work page")]
        public void WhenIClickTheBackButtonOnTheBuildingWorkPage()
        {
            _generalQuestions.ClickBackBtn();
        }



        [Then(@"I verify I can not enter a date in the ""(past|future)"" for the ""(.*)"" general question")]
        public void ThenIVerifyICanNotEnterADateInTheForTheCalenderPicker(string pastFuture, string question)
        {
            _generalQuestions.VerifyGeneralQuestionCalendarValidation(pastFuture, question);
        }

        [Then(@"I verify I can not enter the phase ""(from|until)"" date for the date ""(.*)"" for the ""(.*)""")]
        public void ThenIVerifyICanNotEnterThePhaseDateBeforeTheDateForThe(string fromUntil, string date, string phaseName)
        {
            _generalQuestions.VerifyPhasesDateCanNotBeSelected( fromUntil,  date,  phaseName);
        }

        [Then(@"I can see the mandatory error message ""(.*)"" on the phases ""(.*)"" date for the ""(.*)""")]
        public void ThenICanSeeTheMandatoryErrorMessageOnThePhasesDateForThe(string text,string field, string phaseName)
        {
            _generalQuestions.VerifyValidationOnPhaseFields(text,field, phaseName);
        }

        [Then(@"I verify the options to add the phases information ""(is|is not)"" displayed")]
        public void ThenIVerifyTheOptionsToAddThePhasesInformationDisplayed(string isDisplayed)
        {
            _generalQuestions.VerifyPhasesDisplayed(isDisplayed);
        }

        [Then(@"I verify the following phases are displayed for the Building works phases:")]
        public void ThenIVerifyTheFollowingPhasesAreDisplayedForTheBuildingWorksPhases(Table table)
        {
            _generalQuestions.VerifyPhasesInfo(table);
        }

        [Then(@"I verify I am unable to make any changes to the ""(from|until)"" date on ""(.*)""")]
        public void ThenIVerifyIAmUnableToMakeAnyChangesToTheStartDateOn(string startEndDate,string phaseName)
        {
            _generalQuestions.VerifyUnableToEditPhaseStartDate(startEndDate,phaseName);
        }



        [Then(@"I can enter the following information for the phases:")]
        [When(@"I enter the following information for the phases:")]
        public void ThenIEnterTheFollowingInformationForThePhases(Table table)
        {
            _generalQuestions.EnterPhasesInfo(table);
        }

        [When(@"I click the ""(add|remove)"" phase button for the ""(.*)""")]
        public void WhenIClickThePhaseButtonForThe(string addRemove, string phaseName)
        {
            _generalQuestions.ClickAddRemovePhaseBtn(addRemove, phaseName);
        }


        [Then(@"I verify the ""(add|remove)"" button is ""(enabled|disabled)"" on ""(.*)""")]
        public void ThenIVerifyTheButtonIsOn(string button, string enabledState, string phaseName)
        {
            _generalQuestions.VerifyAddRemovePhaseButtonsState(button, enabledState, phaseName);
        }

        [Then(@"I verify all of the remove buttons are ""(enabled|disabled)"" for the building work phases")]
        public void ThenIVerifyAllOfTheRemoveButtonsAreForTheBuildingWorkPhases(string enabledDisabled)
        {
            _generalQuestions.VerifyAllRemovePhaseButtonsState(enabledDisabled);
        }

        [When(@"I note down the following values on the building work page")]
        public void WhenINoteDownTheFollowingValuesOnTheBuildingWorkPage(Table table)
        {
            _generalQuestions.NoteDownFieldValues(table);
        }


        [When(@"I click the ""(add|remove)"" button for the BW time and days for BW time schedule on slot ""(.*)""")]
        public void WhenIClickTheButtonForTheBwTimeAndDaysToAbwTimeScheduleOnSlot(string addRemove, int slotNumber)
        {
            _generalQuestions.AddRemoveTimeDayBwSchedule(addRemove,slotNumber);
        }


        [Then(@"I verify the ""(add|remove)"" button is ""(enabled|disabled)"" for the BW time and day slot ""(.*)""")]
        public void ThenIVerifyTheButtonIsForTheBwTimeAndDaySlot(string addRemoveBtn, string btnEnabledDisabled,int slotNumber)
        {
            _generalQuestions.VerifyAddRemoveBwTimeStatus(addRemoveBtn, btnEnabledDisabled, slotNumber);
        }


        [Then(@"I can enter the following answers for the approximate time and days of work:")]
        public void ThenICanEnterTheFollowingAnswersForTheApproximateTimeAndDaysOfWork(Table table)
        {
            _generalQuestions.EnterTimeDaysOfBw(table);
        }

        [Then(@"I am ""(displayed|not displayed)"" with the options to enter the hotel closing times")]
        public void ThenIAmWithTheOptionsToEnterTheHotelClosingTimes(string displayedNotDisplayed)
        {
            _generalQuestions.HotelClosedDateDisplayed(displayedNotDisplayed);
        }

        [Then(@"I am ""(displayed|not displayed)"" with the question on the areas of the property are affected")]
        public void ThenIAmWithTheQuestionOnTheAreasOfThePropertyAreAffected(string displayedNotDisplayed)
        {
            _generalQuestions.VerifyAreasOfPropertyQDisplayedOrNot(displayedNotDisplayed);
        }


        [Then(@"I verify I can not enter the hotel closed ""(from|until)"" date ""(before|after)"" the date ""(.*)""")]
        public void ThenIVerifyICanNotEnterTheHotelClosedDateTheDate(string fromUntil, string beforeAfter, string dateTime)
        {
            _generalQuestions.VerifyHotelClosedDatesCantBeSelected(fromUntil, dateTime);
        }

        [Then(@"I verify the following options are present for the Areas of the properties affected question:")]
        public void ThenIVerifyTheFollowingOptionsArePresentForTheAreasOfThePropertiesAffectedQuestion(Table table)
        {
            _generalQuestions.VerifyListOfPropertyAreasAffected(table);
        }

        [Then(@"I am ""(displayed|not displayed)"" with warning message ""(.*)"" for the building works form")]
        public void ThenIAmWithWarningMessageForTheBuildingWorksForm(string displayedNotDisplayed, string message)
        {
            _generalQuestions.VerifyBwPhasesWarningDisplayed(displayedNotDisplayed,message);
        }


        [When(@"I click the ""(.*)"" icon on the top navigation bar for building works")]
        public void WhenIClickTheIconOnTheTopNavigationBarForBuildingWorks(string navItem)
        {
            _generalQuestions.ClickNavItem(navItem);
        }

        [Then(@"I verify the correct commitment levels displayed on overview and summary for different periods for the current property")]
        public void ThenIVerifyTheCorrectCommitmentLevelsDisplayedOnOverviewAndSummaryForDifferentPeriodsForTheCurrentProperty()
        {
            _generalQuestions.VerifyCommitmentLevels();
        }

    }
}