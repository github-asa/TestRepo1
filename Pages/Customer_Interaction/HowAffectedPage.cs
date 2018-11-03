using System;
using System.Collections.Generic;
using System.Linq;
using J2BIOverseasOps.Extensions;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Pages.Customer_Interaction
{
    internal class HowAffectedPage : BasePage
    {
        private readonly By _affectedListBoxes = By.CssSelector("[categorylistcontainer] .ui-multiselect label");
        private readonly By _affectedListPopup = By.TagName("p-dialog");
        private readonly By _affectedListPopupCustomersList = By.CssSelector("#list-container div div");
        private readonly By _affectedListPopupHeader = By.CssSelector("p-dialog p-header");
        private readonly By _categoryLabels = By.CssSelector("[categorylistcontainer] > div > div:first-child > label");
        private readonly By _closeAffectedListPopup = By.Id("close-button");
        private readonly By _continueButton = By.Id("save-button");
        private readonly By _validationMessage = By.CssSelector("div:not([hidden]) p-message .ui-message-text");

        private readonly By _title = By.Id("eros-page-heading");

        public HowAffectedPage(IWebDriver driver, ILog log) : base(driver, log)
        {
        }

        internal void VerifyTitle(string title)
        {
            Assert.AreEqual(title, Driver.GetText(_title), "The title is not being displayed");
        }

        internal void VerifyCategoriesAreDisplayed(Table categories)
        {
            Assert.IsTrue(Driver.WaitForItem(_categoryLabels), "The category labels are not being displayed.");

            var expectedcategories = categories.Rows.ToColumnList("SelectedCategories");
            var actualcategories = Driver.GetTexts(_categoryLabels);

            CollectionAssert.AreEqual(expectedcategories.OrderBy(x => x), actualcategories, "The categories on the how affected page are incorrect");
            //var i = 0;
            //foreach (var row in rows)
            //{
            //    Assert.AreEqual(row["SelectedCategories"], Driver.GetText(labels[i]),
            //        $"The label for {row["SelectedCategories"]} is not being displayed.");
            //    i++;
            //}
        }

        internal void VerifyTheListBoxesAreDisplayed(int categoryCount)
        {
            Assert.IsTrue(Driver.WaitForItem(_affectedListBoxes),
                "The 'Who was affected?' list boxes are not being displayed.");
            var affectedListBoxes = Driver.FindElements(_affectedListBoxes);
            Assert.AreEqual(categoryCount, affectedListBoxes.Count,
                $"The number of 'Who was affected?' listboxes are not being displayed.");
        }

        internal void VerifyListBoxesArePopulated(Table table, int categoryCount)
        {
            //Check if the 'Who was affected?' listbox has been displayed
            Assert.IsTrue(Driver.WaitForItem(_affectedListBoxes),
                "The 'Who was affected?' list boxes are not being displayed.");

            //Retrieve the number of expected customers           
            var customers = table.Rows.ToColumnList("BookingSelected");

            //Iterate through each listbox and read the options
            for (var i = 0; i < categoryCount; i++)
            {
                // var options =
                //     Driver.GetAllMultiselectOptions(
                //         By.CssSelector($"[categorylistcontainer] > div:nth-child({i + 1}) p-multiselect"));
                 var options =
                     Driver.GetAllMultiselectOptions(
                         By.CssSelector($"div:nth-child({i + 1})[categorylistcontainer] p-multiselect"));

                //Verify that the text for each option is as expected
                CollectionAssert.AreEqual(customers, options,
                    "The 'Who was affected?' list box does not have correct selected booking/customer.");
            }
        }

        internal void SelectCustomersForCategory(string category, Table table)
        {
            //Check if the 'Who was affected?' listbox has been displayed
            Assert.IsTrue(Driver.WaitForItem(_affectedListBoxes),
                "The 'Who was affected?' list boxes are not being displayed.");

            //Identify the listbox
            var i = GetListBoxIndex(category);

            //Retrieve the number of expected customers          
            var customers = table.Rows.ToColumnList("Selection");

            //select each customer in the listbox
            Driver.SelectMultiselectOption(By.CssSelector($"div:nth-child({i})[categorylistcontainer] p-multiselect > div"),
                customers);
        }

        private int GetListBoxIndex(string category)
        {
            Assert.IsTrue(Driver.WaitForItem(_categoryLabels), "The category labels are not being displayed.");
            var labels = Driver.FindElements(_categoryLabels);

            var i = 0;
            foreach (var label in labels)
            {
                if (Driver.GetText(label).Equals(category))
                {
                    i = labels.IndexOf(label);
                    break;
                }
            }

            return i + 1;
        }

        public void VerifyCustomersAreSelected(string category, Table table)
        {
            //Check if the 'Who was affected?' listbox has been displayed
            Assert.IsTrue(Driver.WaitForItem(_affectedListBoxes),
                "The 'Who was affected?' list boxes are not being displayed.");

            //Identify the listbox
            var i = GetListBoxIndex(category);

            //Retrieve the number of expected customers            
            var customers = table.Rows.ToColumnList("Selection");

            //Iterate through each listbox and read the options
            var selectedOptions =
                Driver.GetAllSelectedMultiselectOptions(
                    By.CssSelector($"div:nth-child({i})[categorylistcontainer] p-multiselect > div"));

            //Verify that the text for each option is as expected
            CollectionAssert.AreEqual(customers, selectedOptions,
                "The 'Who was affected?' list box does not have correct selected booking/customer.");
        }

        public void DeselectCustomersForCategory(string category, Table table)
        {
            //Check if the 'Who was affected?' listbox has been displayed
            Assert.IsTrue(Driver.WaitForItem(_affectedListBoxes),
                "The 'Who was affected?' list boxes are not being displayed.");

            //Identify the listbox
            var i = GetListBoxIndex(category);

            //Retrieve the number of expected customers           
            var customers = table.Rows;

            //Iterate through the and select each customer in the listbox
            foreach (var customer in customers)
            {
                Driver.DeselectMultiselectOption(
                    By.CssSelector($"div:nth-child({i})[categorylistcontainer] p-multiselect > div"),
                    customer["Selection"]);
            }
        }

        public void VerifyNoCustomersSelectedForCategory(string category)
        {
            //Check if the 'Who was affected?' listbox has been displayed
            Assert.IsTrue(Driver.WaitForItem(_affectedListBoxes),
                "The 'Who was affected?' list boxes are not being displayed.");

            //Identify the listbox
            var i = GetListBoxIndex(category);

            //Iterate through each listbox and read the options
            var selectedOptions =
                Driver.GetAllSelectedMultiselectOptions(
                    By.CssSelector($"div:nth-child({i})[categorylistcontainer] p-multiselect > div"));

            Assert.IsEmpty(selectedOptions, "There are options selected in the listbox.");
        }

        public void EnterFilterTextForCategory(string category, string filter)
        {
            //Identify the listbox
            var i = GetListBoxIndex(category);
            var locator = By.CssSelector($"div:nth-child({i})[categorylistcontainer] p-multiselect > div");

            ScenarioContext.Current["FilteredOptions"] = Driver.GetFilteredMultiselectOptions(filter, locator);
        }

        public void GetFilteredOptionsForCategory(string category, Table table)
        {
            var options = ScenarioContext.Current.Get<List<string>>("FilteredOptions");

            //Retrieve the number of expected customers            
            var customers = table.Rows.ToColumnList("Selection");

            //Verify that the text for each option is as expected
            CollectionAssert.AreEqual(customers, options,
                "The 'Who was affected?' list box does not have correct selected booking/customer.");
        }

        public void SelectAllforCategory(string category)
        {
            //Identify the listbox
            var i = GetListBoxIndex(category);
            var locator = By.CssSelector($"div:nth-child({i})[categorylistcontainer] p-multiselect > div");
            Driver.SelectAllMultiselectOptions(locator);
        }

        public void DeselectAllforCategory(string category)
        {
            //Identify the listbox
            var i = GetListBoxIndex(category);
            var locator = By.CssSelector($"div:nth-child({i})[categorylistcontainer] p-multiselect > div");

            Driver.DeselectAllMultiselectOptions(locator);

        }

        public void VerifyMessageBesideCategory(string category, string message)
        {
            //Identify the listbox
            var i = GetListBoxIndex(category);
            var locator =
                By.CssSelector($"div:nth-child({i})[categorylistcontainer] .text-right > .ng-star-inserted");
            Assert.AreEqual(message, Driver.GetText(locator), "The no affected message is not as expected.");
        }

        public void ClickViewAffectedCustomersLinkFor(string category)
        {
            var i = GetListBoxIndex(category);
            var locator = By.CssSelector($"div:nth-child({i})[categorylistcontainer] .text-right a");
            Driver.ClickItem(locator);
        }

        public void VerifyAffectedListPopupIsDisplayed()
        {
            Assert.IsTrue(Driver.WaitForItem(_affectedListPopup), "The Customers affected list has been displayed.");
        }

        public void VerifyAffectedListPopupTitle(string title)
        {
            Assert.AreEqual(title, Driver.GetText(_affectedListPopupHeader), "The title of the popup is incorrect.");
        }

        public void VerifyAffectedListOfCustomersPopup(Table table)
        {
            var customers = Driver.GetTexts(_affectedListPopupCustomersList);
            var customersList = table.Rows.ToColumnList("Selection");

            //Verify that the text for each option is as expected
            CollectionAssert.AreEqual(customers, customersList,
                "The 'Who was affected?' list box does not have correct selected booking/customer.");
        }

        public void CloseViewAffectedPopup()
        {
            Driver.ClickItem(_closeAffectedListPopup);

            Assert.IsTrue(Driver.WaitUntilElementNotPresent(_affectedListPopup));
        }

        public void VerifyContinueButtonStatus(string status)
        {
            switch (status.ToLower())
            {
                case "enabled":
                    Assert.IsTrue(Driver.IsElementEnabled(_continueButton), "");
                    break;
                case "disabled":
                    Assert.IsFalse(Driver.IsElementEnabled(_continueButton), "");
                    break;
                default:
                    throw new Exception($"{status} is not a valid button state.");
            }
        }

        public void ClickContinueButton()
        {
            Driver.ClickItem(_continueButton);
        }

        public void VerifyValidationMessage(string message)
        {
            Assert.AreEqual(message, Driver.GetText(_validationMessage),
                "The validation message is not being displayed.");
        }
    }
}