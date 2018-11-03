using J2BIOverseasOps.Pages.BuildingWork.Onsite.OnsiteGeneralQ;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.BuildingWork.Onsite
{
    [Binding]
    public sealed class OnSiteGeneralQStepDefs : BaseStepDefs
    {
        private readonly OnsiteGeneralQPage _onsiteGeneralQ;

        public OnSiteGeneralQStepDefs(IWebDriver driver, ILog log, IRunData rundata) : base(driver, log)
        {
            _onsiteGeneralQ = new OnsiteGeneralQPage(driver, log,rundata);
        }

        [Then(@"I can see the following mandatory error message on the following Onsite General Questions fields:")]
        public void ThenICanSeeTheFollowingMandatoryErrorMessageOnTheFollowingOnsiteGeneralQuestionsFields(Table table)
        {
            _onsiteGeneralQ.VerifyOnsiteMandatoryMessage(table);
        }

        [Then(@"I can enter the following answers for the following questions on the the Onsite General Questions page:")]
        [When(@"I enter the following answers for the following questions on the the Onsite General Questions page:")]
        public void WhenIEnterTheFollowingAnswersForTheFollowingQuestionsOnTheTheOnsiteGeneralQuestionsPage(Table table)
        {
            _onsiteGeneralQ.EnterOnSiteGeneralQAnswers(table);
        }

        [Then(@"I verify the following answers for the following questions on the the Onsite General Questions page:")]
        public void WhenIVerifyTheFollowingAnswersForTheFollowingQuestionsOnTheTheOnsiteGeneralQuestionsPage(Table table)
        {
            _onsiteGeneralQ.VerifyOnSiteGeneralQAnswers(table);

        }


        [Then(@"I am ""(displayed|not displayed)"" the following fields on the Onsite General Questions page")]
        public void ThenIAmTheFollowingFieldsOnTheOnsiteGeneralQuestionsPage(string displayedOrNot, Table table)
        {
            _onsiteGeneralQ.VerifyFieldsDisplayedOrNot(displayedOrNot,table);
        }









    }
}