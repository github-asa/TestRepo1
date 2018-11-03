using J2BIOverseasOps.Extensions;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Pages.PreSeasonChecklist
{
    public class PreSeasonChecklistLandingPage : BasePage
    {
        private readonly By _propertIdTextbox = By.Name("selectedProperty");
        private readonly By _formDropdown = By.Id("select-form");
        private readonly By _createButton = By.Id("create-form-btn");
        private readonly By _formDropdownFilter = By.CssSelector("#select-form input.ui-dropdown-filter");

        public PreSeasonChecklistLandingPage(IWebDriver driver, ILog log) : base(driver, log)
        {
        }

        public void EnterPropertyId()
        {
            var id = StringExtensions.GenerateRandomInt();
            ScenarioContext.Current.Set(id, "PropertyId");
            Driver.EnterText(_propertIdTextbox, id);
        }

        public void SelectFormName()
        {
            var formName = ScenarioContext.Current.Get<string>("formname");
           // Driver.SelectDropDownOption(_formDropdown, formName);
            //Driver.ClickItem(_formDropdown);       

            //Find dropdown
            Assert.IsTrue(Driver.WaitUntilElementIsPresent(_formDropdown), "The dropdown is not displayed.");
            var dropdown = Driver.FindElement(_formDropdown);

            //Open dropdown
            if (!Driver.WaitForItemWithinWebElement(_formDropdown,
                By.CssSelector(".ui-dropdown-open"), 1))
            {
                Driver.ClickItem(dropdown.FindElement(By.CssSelector(".ui-dropdown-trigger-icon")));
            }

            //Filter Forms
            Driver.EnterText(_formDropdownFilter, formName);

            //Find all options in the dropdown
            Assert.IsTrue(Driver.WaitForItemWithinWebElement(_formDropdown, By.CssSelector(".ui-dropdown-item")),
                "The multiselect options are not being displayed.");
            var options = dropdown.FindElements(By.CssSelector(".ui-dropdown-item"));
            var texts = Driver.GetTexts(options);

            //Identify the checkbox and select it for each customer         
            var i = 0;
            foreach (var txt in texts)
            {
                if (txt.ToLower().Equals(formName.ToLower()))
                {
                    options[i].Click();
                    break;
                }

                i++;
            }

            //Close dropdown
            if (Driver.WaitForItemWithinWebElement(_formDropdown,
                By.CssSelector(".ui-dropdown-open"), 1))
            {
                Driver.ClickItem(dropdown.FindElement(By.CssSelector(".ui-dropdown-trigger-icon")));
            }

            //Close dropdown
            if (Driver.WaitForItemWithinWebElement(_formDropdown,
                By.CssSelector(".ui-dropdown-open"), 1))
            {
                Driver.ClickItem(dropdown.FindElement(By.CssSelector(".ui-dropdown-trigger-icon")));
            }

        }

        public void ClickCreatePreSeasonCheclistButton()
        {
            Driver.ClickItem(_createButton);
        }

        public void EnterSamePropertyId()
        {
            var id = ScenarioContext.Current.Get<string>("PropertyId");
            Driver.EnterText(_propertIdTextbox, id);
        }
    }
}
