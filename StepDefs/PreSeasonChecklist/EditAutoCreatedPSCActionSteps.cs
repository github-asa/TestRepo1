using System;
using System.Runtime.InteropServices.WindowsRuntime;
using J2BIOverseasOps.Pages.PreSeasonChecklist;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.PreSeasonChecklist
{
    [Binding]
    public class EditAutoCreatedPscActionSteps : BaseStepDefs
    {
        private readonly ViewActionPage _viewActionPage;

        [When(@"I edit the following Assignment details on the view action page:")]
        public void WhenIEditTheFollowingAssignmentDetailsOnTheViewActionPage(Table table)
        {
            _viewActionPage.UpdateTheAssignmentDetails(table);
        }
        
        [When(@"I click the save button on the view Action page")]
        public void WhenIClickTheSaveButtonOnActionPage()
        {
            _viewActionPage.ClickSave();
        }

        public EditAutoCreatedPscActionSteps(IWebDriver driver, ILog log) : base(driver, log)
        {
            _viewActionPage = new ViewActionPage(driver, log);
        }
    }
}
