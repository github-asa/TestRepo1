using System;
using J2BIOverseasOps.Pages.PreSeasonChecklist;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.PreSeasonChecklist
{
    [Binding]
    public class DeleteAPreSeasonChecklistSteps : BaseStepDefs
    {
        private readonly PreSeasonChecklistPage _preSeasonChecklistPage;
        public DeleteAPreSeasonChecklistSteps(IWebDriver driver, ILog log) : base(driver, log)
        {
            _preSeasonChecklistPage = new PreSeasonChecklistPage(driver, log);
        }

        [When(@"I click the delete button on the Pre-Season Checklist page")]
        public void WhenIClickTheDeleteButtonOnThePre_SeasonChecklistPage()
        {
            _preSeasonChecklistPage.ClickDelete();
        }


    }
}
