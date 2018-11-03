using J2BIOverseasOps.Pages.BuildingWork.GeneralQuestions;
using J2BIOverseasOps.Pages.BuildingWork.NewBuild;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.BuildingWork.NewBuild
{
    [Binding]
    public sealed class KeyStagesStepDefs : BaseStepDefs
    {
        private readonly KeyStagesPage _keyStages;

        public KeyStagesStepDefs(IWebDriver driver, ILog log,IRunData rundata) : base(driver, log)
        {
            _keyStages= new KeyStagesPage(driver, log,rundata);
        }

        [Then(@"I verify the data within the following fields on the keystages page for newbuild page")]
        public void ThenIVerifyTheDataWithinTheFollowingFieldsOnTheKeystagesPageForNewbuildPage(Table table)
        {
            _keyStages.VerifyBuildingItemAndCurrentStatusFieldData(table);
        }

        [When(@"I enter the following answers for the following questions on the keystages page for newbuild")]
        public void WhenIEnterTheFollowingAnswersForTheFollowingQuestionsOnTheKeystagesPageForNewbuild(Table table)
        {
            _keyStages.EnterKeyStagesAnswer(table);
        }

        [Then(@"I am ""(displayed|not displayed)"" the following fields on the keystages page for newbuild")]
        public void ThenIAmTheFollowingFieldsOnTheKeystagesPageForNewbuild(string displayedOrNot, Table table)
        {
            _keyStages.VerifyFieldsDisplayedOrNot(displayedOrNot, table);
        }


        [When(@"I click on the ""(add|remove)"" record button on the keystages item number ""(.*)"" on the new build page")]
        public void WhenIClickOnTheRecordButtonOnTheKeystagesItemNumberOnTheNewBuildPage(string addremove, int index)
        {
            _keyStages.ClickAddRemoveButton(addremove, index);
        }

        [Then(@"I can see the following mandatory error message on the following New Build fields:")]
        public void ThenICanSeeTheFollowingMandatoryErrorMessageOnTheFollowingNewBuildFields(Table table)
        {
            _keyStages.VerifyNewBuildValidationErrorMessage(table);
        }

        [Then(@"I verify the answers for the following Newbuild Questions:")]
        public void ThenIVerifyTheAnswersForTheFollowingNewbuildQuestions(Table table)
        {
            _keyStages.VerifyAnswers(table);
        }




    }
}