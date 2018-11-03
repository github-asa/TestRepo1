using J2BIOverseasOps.Pages.PreSeasonChecklist;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.PreSeasonChecklist
{
    [Binding]
    public class CreatePreSeasonChecklistSteps : BaseStepDefs
    {
        private readonly PreSeasonChecklistLandingPage _landingPage;

        public CreatePreSeasonChecklistSteps(IWebDriver driver, ILog log) : base(driver, log)
        {
            _landingPage = new PreSeasonChecklistLandingPage(driver, log);
        }

        [When(@"I enter the property id and I select the Pre-Season Checklist form")]
        public void GivenIEnterThePropertyIdAndISelectThePre_SeasonChecklistForm()
        {
            _landingPage.EnterPropertyId();
            _landingPage.SelectFormName();
        }

        [When(@"I enter the same property id and I select the Pre-Season Checklist form")]
        public void WhenIEnterTheSamePropertyIdAndISelectThePre_SeasonChecklistForm()
        {
            _landingPage.EnterSamePropertyId();
            _landingPage.SelectFormName();
        }


        [When(@"I click the Create Pre-Season Checklist button")]
        public void WhenIClickTheCreatePre_SeasonChecklistButton()
        {
            _landingPage.ClickCreatePreSeasonCheclistButton();
        }
    }
}