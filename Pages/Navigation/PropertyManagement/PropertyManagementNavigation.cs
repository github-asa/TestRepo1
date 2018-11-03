using J2BIOverseasOps.Extensions;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Pages.Navigation.PropertyManagement
{
    internal class PropertyManagementNavigation : CommonPageElements
    {
        // property search details
        private readonly By _destinationDrpDwn = By.XPath("//p-dropdown[@id='destinationSearch']");
        private readonly By _resortDrpDwn = By.XPath("//p-dropdown[@id='resortSearch']");
        private readonly By _propertyDropdwn = By.XPath("//p-dropdown[@id='propertySearch']");

        // buttons
        private readonly By _btnCompletePropertAssesment = By.XPath("//p-button[@id='btnPreSeasonCheckList']");
        private readonly By _btnNewBuildingWorkRecord = By.XPath("//p-button[@id='btnNewBuildingWorkRecord']");

        public PropertyManagementNavigation(IWebDriver driver, ILog log) : base(driver, log)
        {
        }

        public void SelectAndSearchPropertyToManage(string dest, string resrt, string prop)
        {
            Driver.SelectDropDownOption(_destinationDrpDwn, dest);
            Driver.SelectDropDownOption(_resortDrpDwn, resrt);

            // selecting from property p-dropdown will start the search
            Driver.SelectDropDownOption(_propertyDropdwn, prop);
        }

        public void VerifyButtonIsDisplayed(string buttonText)
        {
            By buttonLocator = null;
            switch (buttonText.ToLower())
            {
                case "complete property assessment":
                    buttonLocator = _btnCompletePropertAssesment;
                    break;
                case "create building work form":
                    buttonLocator = _btnNewBuildingWorkRecord;
                    break;
                default:
                    Assert.Fail($"{buttonText} is not a valid button");
                    break;
            }
            Assert.IsTrue(Driver.WaitForItem(buttonLocator), $"The '{buttonText}' button is not being displayed.");
        }

        public void ClickPropertyManagementButton(string buttonText)
        {
            By buttonLocator = null;
            switch (buttonText.ToLower())
            {
                case "complete property assessment":
                    buttonLocator = _btnCompletePropertAssesment;
                    break;
                case "create building work form":
                    buttonLocator = _btnNewBuildingWorkRecord;
                    break;
                default:
                    Assert.Fail($"{buttonText} is not a valid button");
                    break;
            }
            Driver.ClickItem(buttonLocator);
        }
    }
}
