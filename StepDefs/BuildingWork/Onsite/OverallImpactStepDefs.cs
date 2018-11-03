using J2BIOverseasOps.Pages.BuildingWork.Onsite.OverallImpact;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.BuildingWork.Onsite
{
    [Binding]
    public sealed class OverallImpactStepDefs : BaseStepDefs
    {
        private readonly OverallImpactPage _overAllImpact;

        public OverallImpactStepDefs(IWebDriver driver, ILog log,IRunData rundata) : base(driver, log)
        {
            _overAllImpact = new OverallImpactPage(driver, log,rundata);
        }

        [Then(@"I can see the following mandatory error message on the following overall impact fields:")]
        public void ThenICanSeeTheFollowingMandatoryErrorMessageOnTheFollowingOnsiteGeneralQuestionsFields(Table table)
        {
            _overAllImpact.VerifyOnsiteMandatoryMessage(table);
        }

        [Then(@"I can enter the following answers for the following questions on the the Onsite overall impact page:")]
        [When(@"I enter the following answers for the following questions on the the Onsite overall impact page:")]
        public void WhenIEnterTheFollowingAnswersForTheFollowingQuestionsOnTheTheOnsiteGeneralQuestionsPage(Table table)
        {
            _overAllImpact.EnterOverallImpactQAnswers(table);
        }

        [Then(@"I verify the following answers for the following questions on the the Onsite overall impact page:")]
        public void ThenIVerifyTheFollowingAnswersForTheFollowingQuestionsOnTheTheOnsiteOverallImpactPage(Table table)
        {
            _overAllImpact.VerifyOverallImpactQAnswers(table);
        }


        [Then(@"I am ""(displayed|not displayed)"" the following fields on the overall impact page")]
        public void ThenIAmTheFollowingFieldsOnTheOnsiteGeneralQuestionsPage(string displayedOrNot, Table table)
        {
            _overAllImpact.VerifyFieldsDisplayedOrNot(displayedOrNot, table);
        }

        [Then(@"I am ""(displayed|not displayed)"" the following Pselect option fields on the Onsite overall impact page:")]
        public void ThenIAmTheFollowingPselectOptionFieldsOnTheOnsiteOverallImpactPage(string displayedOrNot, Table table)
        {
            _overAllImpact.VerifyPselectOptionDisplayedOrNot(displayedOrNot, table);
        }

        [Then(@"I can select the following Pselect options fields on the the Onsite overall impact page:")]
        public void ThenICanSelectTheFollowingPselectOptionsFieldsOnTheTheOnsiteOverallImpactPage(Table table)
        {
            _overAllImpact.VerifyCanSelectPOption(table);
        }



    }
}