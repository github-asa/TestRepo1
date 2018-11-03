using J2BIOverseasOps.Pages.Navigation.PropertyManagement;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.Navigation.PropertyManagement
{
    [Binding]
    public sealed class PropertyManagementNavigationStepDefs : BaseStepDefs
    {
        private readonly PropertyManagementNavigation _propertyManagementNavigation;
        private readonly IRunData _runData;

        public PropertyManagementNavigationStepDefs(IWebDriver driver, ILog log, IRunData runData) : base(driver, log)
        {
            _runData = runData;
            _propertyManagementNavigation = new PropertyManagementNavigation(driver, log);
        }

        [When(@"I select the Destination as ""(.*)"", Resort as ""(.*)"" and property as ""(.*)"" on Property Management property search")]
        public void WhenISelectTheDestinationAsResortAsAndPropertyAsOnPropertyManagementPropertySearch(
            string destination, string resort, string property)
        {
            _propertyManagementNavigation.SelectAndSearchPropertyToManage(destination, resort, property);
        }

        [When(@"I click the ""(.*)"" button on the property management page")]
        public void WhenIClickTheButtonOnThePropertyManagementPage(string buttonName)
        {
            _propertyManagementNavigation.ClickPropertyManagementButton(buttonName);
        }

        [Then(@"I should see the ""(.*)"" button on the property management page")]
        public void ThenIShouldSeeTheButtonOnThePropertyManagementPage(string buttonName)
        {
            _propertyManagementNavigation.VerifyButtonIsDisplayed(buttonName);
        }
    }
}