using System.Collections.Generic;
using J2BIOverseasOps.Extensions;
using J2BIOverseasOps.Pages.Customer_Interaction;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.Customer_Interaction
{
    [Binding]
    public sealed class AssignCategoriesStepDefs : BaseStepDefs
    {
        private readonly HowAffectedPage _howAffectedPage;

        public AssignCategoriesStepDefs(IWebDriver driver, ILog log) : base(driver, log)
        {
            _howAffectedPage = new HowAffectedPage(driver, log);
        }

        [Then(@"I verify the title is '(.*)' on the How affected page")]
        public void ThenTheTitleIsOnTheHowAffectedPage(string title)
        {
            _howAffectedPage.VerifyTitle(title);
        }

        [Then(@"all of my chosen categories are displayed on the How affected page")]
        public void ThenAllOfMyChosenCategoriesAreDisplayedOnTheHowAffectedPage()
        {
            var categories = ScenarioContext.Current.Get<Table>("SelectedCategories");

            _howAffectedPage.VerifyCategoriesAreDisplayed(categories);
        }

        [Then(@"the 'Who was affected\?' listbox is displayed for each category on the How affected page")]
        public void ThenTheListboxIsDisplayedForEachCategoryOnTheHowAffectedPage()
        {
            var categoryCount = ScenarioContext.Current.Get<Table>("SelectedCategories").Rows.Count;

            _howAffectedPage.VerifyTheListBoxesAreDisplayed(categoryCount);
        }

        [Then(@"the 'Who was affected\?' listbox contains the selected booking references and customers")]
        public void ThenTheListboxContainsTheSelectedBookingReferencesAndCustomers(Table table)
        {
            var categoryCount = ScenarioContext.Current.Get<Table>("SelectedCategories").RowCount;

            _howAffectedPage.VerifyListBoxesArePopulated(table, categoryCount);
        }

        [When(@"I select the customers affected to the '(.*)' category on the How affected page")]
        [When(@"I select the booking affected to the '(.*)' category on the How affected page")]
        public void WhenISelectTheCustomersAffectedToTheCategory(string category, Table table)
        {
            var affectedCustomersList = ScenarioContext.Current.Get<List<string>>("affectedCustomersList");
            var affectedCustomers = table.Rows.ToColumnList("Selection");
            affectedCustomersList.AddRange(affectedCustomers);

            ScenarioContext.Current["affectedCustomersList"] = affectedCustomersList;

            _howAffectedPage.SelectCustomersForCategory(category, table);
        }

        [Then(@"the '(.*)' category will have the correct customers selected on the How affected page")]
        public void ThenTheCategoryWillHaveTheCorrectCustomersSelected(string category, Table table)
        {
            _howAffectedPage.VerifyCustomersAreSelected(category, table);
        }

        [When(@"I deselect the customers affected from the '(.*)' category on the How affected page")]
        [When(@"I deselect the booking affected from the '(.*)' category on the How affected page")]
        public void WhenIDeselectTheCustomersAffectedFromTheCategory(string category, Table table)
        {
            var affectedCustomersList = ScenarioContext.Current.Get<List<string>>("affectedCustomersList");
            var affectedCustomersToRemove = table.Rows.ToColumnList("Selection");

            affectedCustomersList.RemoveAll(x => affectedCustomersToRemove.Contains(x));

            ScenarioContext.Current["affectedCustomersList"] = affectedCustomersList;

            _howAffectedPage.DeselectCustomersForCategory(category, table);
        }

        [Then(@"the '(.*)' category will have no customers selected on the How affected page")]
        public void ThenTheCategoryWillHaveNoCustomersSelected(string category)
        {
            _howAffectedPage.VerifyNoCustomersSelectedForCategory(category);
        }

        [When(
            @"I enter the the customer name '(.*)' in the listbox filter for the '(.*)' category on the How affected page:")]
        public void WhenIEnterTheTheCustomerNameInTheListboxFilterForTheCategory(string filter, string category)
        {
            _howAffectedPage.EnterFilterTextForCategory(category, filter);
        }

        [Then(@"the 'Who was affected\?' listbox for the '(.*)' category contains the filtered customers")]
        public void ThenTheListboxForTheCategoryContainsTheFilteredCustomers(string category, Table table)
        {
            _howAffectedPage.GetFilteredOptionsForCategory(category, table);
        }

        [When(@"I select ALL bookings affected to the '(.*)' category on the How affected page")]
        public void WhenISelectAllBookingsAffectedToTheCategory(string category)
        {
            _howAffectedPage.SelectAllforCategory(category);
        }

        [When(@"I deselect ALL bookings affected to the '(.*)' category on the How affected page")]
        public void WhenIDeselectAllBookingsAffectedToTheCategory(string category)
        {
            _howAffectedPage.DeselectAllforCategory(category);
        }

        [Then(@"I verify the continue button is ""(.*)"" on the How affected page")]
        public void ThenIVerifyTheContinueButtonIsOnTheHowAffectedPage(string status)
        {
            _howAffectedPage.VerifyContinueButtonStatus(status);
        }

        [When(@"I click the continue button on the How affected page")]
        public void WhenIClickTheContinueButtonOnTheHowAffectedPage()
        {
            _howAffectedPage.ClickContinueButton();
        }

        [Then(@"a validation message '(.*)' is displayed on the how affected page")]
        public void ThenAValidationMessageIsDisplayedOnTheHowAffectedPage(string message)
        {
            _howAffectedPage.VerifyValidationMessage(message);
        }

    }
}