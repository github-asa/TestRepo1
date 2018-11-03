using System;
using System.Threading;
using J2BIOverseasOps.Pages.PreSeasonChecklist;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.PreSeasonChecklist
{
    [Binding]
    public class DisplayPreSeasonChecklistSteps : BaseStepDefs
    {
        private readonly PreSeasonChecklistPage _preSeasonChecklistPage;

        public DisplayPreSeasonChecklistSteps(IWebDriver driver, ILog log) : base(driver, log)
        {
            _preSeasonChecklistPage = new PreSeasonChecklistPage(driver, log);
        }

        [Then(@"the following questions are displayed on the Pre-Season Checklist page:")]
        public void ThenTheFollowingQuestionsAreDisplayedOnThePre_SeasonChecklistPage(Table table)
        {
            _preSeasonChecklistPage.VerifyQuestionsAreDisplayed(table);
        }

        [When(@"I click back on the Pre-Season Checklist form page")]
        public void WhenIClickBackOnThePre_SeasonChecklistFormPage()
        {
            _preSeasonChecklistPage.ClickBack();
        }

        [When(@"I enter the following answers on the Pre-Season Checklist page:")]
        public void WhenIAnswerTheFollowingQuestionsOnThePre_SeasonChecklistPage(Table table)
        {
            _preSeasonChecklistPage.EnterAnswersToQuestions(table);
        }

        [When(@"I wait for (.*) seconds for auto save to happen")]
        public void WhenIWaitForSecondsForAutoSaveToHappen(int seconds)
        {
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
        }

        [Then(@"I verify that the questions have been filled with the following answers:")]
        public void ThenIVerifyThatTheQuestionsHaveBeenFilledWithTheFollowingAnswers(Table table)
        {
            _preSeasonChecklistPage.VerifyAnswersEntered(table);
        }

        [Then(@"the submit button is disabled on the Pre-Season Checklist page")]
        public void ThenTheSaveButtonIsDisabledOnThePre_SeasonChecklistPage()
        {
            _preSeasonChecklistPage.VerifySubmitButtonIsDisabled();
        }

        [Then(@"the submit button is enabled on the Pre-Season Checklist page")]
        public void ThenTheSaveButtonIsEnabledOnThePre_SeasonChecklistPage()
        {
            _preSeasonChecklistPage.VerifySubmitButtonIsEnabled();
        }

        [Then(@"a validation message is displayed stating ""(.*)""")]
        public void ThenAValidationMessageIsDisplayedStating(string message)
        {
            _preSeasonChecklistPage.VerifyValidationMessageIsDisplayed(message);
        }

        [When(@"I click the save button on the Pre-Season Checklist page")]
        public void WhenIClickTheSaveButtonOnThePre_SeasonChecklistPage()
        {
            _preSeasonChecklistPage.ClickSave();
        }

        [When(@"I click the submit button on the Pre-Season Checklist page")]
        public void WhenIClickTheSubmitButtonOnThePre_SeasonChecklistPage()
        {
            _preSeasonChecklistPage.ClickSubmit();
        }


    }
}