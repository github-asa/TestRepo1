using System;
using System.Collections.Generic;
using System.Linq;
using J2BIOverseasOps.Extensions;
using J2BIOverseasOps.Helpers;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using static J2BIOverseasOps.Models.CategoryApi;

namespace J2BIOverseasOps.Pages.Customer_Interaction
{
    internal class WhatHappenedPage : CommonPageElements
    {
        private readonly IRunData _rundData;

        private readonly ApiCalls _apiCall;

        //buttons
        private readonly By _btnAddACategory = By.CssSelector("#what-happened-add-categories-button");
        private readonly By _btnContinue = By.CssSelector("#what-happened-continue-button");
        private readonly By _btnContinueDisabled = By.CssSelector("#what-happened-continue-button [disabled]");
        private readonly By _buttonBackSelectACategoryScreen = By.Id("select-case-categories-back-button");
        private readonly By _buttonBackToWhatHappened = By.Id("case-categories-display-parent-name");
        private readonly By _buttonContinueSelectACategoryScreen = By.Id("select-case-categories-continue-button");

        //inputs
        private readonly By _inputInitialSummary = By.CssSelector("#initial-summary-text-area");
        private readonly By _inputCaseNotes = By.CssSelector("#notes");

        //Selected categories panel
        private readonly By _categoryNames =
            By.CssSelector("#what-happened-case-categories .ui-multiselect-items-wrapper label");
        private readonly By _noCategoriesSelectedMessage = By.CssSelector("#no-categories");

        //dropdowns
        private readonly By _drpDwn = By.CssSelector("#what-happened-case-categories");
        private readonly By _drpDwnInput =
            By.CssSelector("#what-happened-case-categories .ui-multiselect-filter-container.ng-star-inserted input");
        private readonly By _drpDwnSelect =
            By.CssSelector("#what-happened-case-categories .ui-multiselect-items-wrapper");

        //Select A Category Screen
        private readonly By _headingSelectACategory = By.Id("select-case-categories-heading");
        private readonly By _listChildCategories = By.Id("child-categories");
        private readonly By _listGrandchildCategories = By.Id("grandchild-categories");
        private readonly By _listParentCategories = By.Id("parent-categories");
        private readonly By _noSearchResultsMessage = By.Id("search-result-message");
        private readonly By _removeAllbutton = By.CssSelector("#what-happened-remove-all-button");
        private readonly By _searchedCategories = By.CssSelector("[id*=search-results-category]");
        private readonly By _searchedCategoriesContainer = By.Id("case-categories-search-container");
        private readonly By _selectedCategories = By.CssSelector("tr[id*=category] td:nth-child(1)");
        private readonly By _selectedChildCategory = By.CssSelector("#child-categories .recent-selection");
        private readonly By _selectedParentCategory = By.CssSelector("#parent-categories .recent-selection");
        private readonly By _textBoxSearch = By.Id("case-categories-search");

        //labels
        private readonly By _characterLimitMessage = By.CssSelector("#characters-remaining");

        //labels
        private readonly By _validationMessage = By.CssSelector("div:not([hidden]) > p-message .ui-message-text");


        



        public WhatHappenedPage(IWebDriver driver, ILog log, IRunData runData) : base(driver, log)
        {
            _rundData = runData;
            _apiCall=new ApiCalls(runData);
        }

        public void VerifyButtonStatus(string expectedStatus)
        {
            switch (expectedStatus)
            {
                case "enabled":
                    Assert.True(!Driver.IsElementPresent(_btnContinueDisabled));
                    break;
                case "disabled":
                    Assert.True(Driver.IsElementPresent(_btnContinueDisabled));
                    break;
                default:
                    throw new Exception($"{expectedStatus} is not a valid button state.");
            }
        }

        public void SelectCategories(Table table)
        {
            var categories = table.Rows.ToColumnList("Categories");
            Driver.SelectMultiselectOption(_drpDwn, categories);
        }

        public void VerifySelectedCategories(Table table)
        {
            var categories = table.Rows.ToColumnList("Case Categories");
            var i = table.Rows.ToColumnList("Case Categories").Count;
            WaitForSpinnerToDisappear();
            Assert.IsTrue(
                Driver.WaitUntilNumberOfElementsPresent(By.CssSelector("tr[id*=category] td:nth-child(1)"), i));
            var selectedList = Driver.GetTexts(_selectedCategories);
            CollectionAssert.AreEqual(categories, selectedList, "The selected category list is not as expected");
        }

        public void ClickContinue()
        {
            WaitForSpinnerToDisappear();
            Driver.ClickItem(_btnContinue);
        }

        public void VerifyNoSelectedCategories()
        {
            Driver.WaitForItem(_noCategoriesSelectedMessage);
        }

        private int GetSelectedCategoryIndex(string category)
        {
            var categories = Driver.GetTexts(_selectedCategories);
            var i = 0;

            foreach (var cat in categories)
            {
                if (category.Equals(cat))
                {
                    i = categories.IndexOf(cat);
                    break;
                }
            }

            return i;
        }

        public void RemoveASingleCategory(string category)
        {
            var i = GetSelectedCategoryIndex(category);
            Driver.ClickItem(By.CssSelector($"button[id*=remove-{i}]"));
        }

        public void RemoveMultipleCategories(Table table)
        {
            var rows = table.Rows;
            var i = Driver.FindElements(_selectedCategories).Count;
            foreach (var row in rows)
            {
                var category = row["Case Categories"];
                Assert.IsTrue(
                    Driver.WaitUntilNumberOfElementsPresent(By.CssSelector("tr[id*=category] td:nth-child(1)"), i));
                RemoveASingleCategory(category);
                ClickOnConfirmationPopup("Remove");
                VerifyConfirmationPopUpIsDismissed();
                i--;
            }
        }

        public void ClickAddACategory()
        {
            WaitForSpinnerToDisappear();
            Driver.ClickItem(_btnAddACategory);
            WaitForSpinnerToDisappear();
        }

        public void VerifyAddACategoryScreenIsDisplayed()
        {
            Assert.IsTrue(Driver.WaitForItem(_headingSelectACategory),
                "The Select A Category screen is not displayed.");
        }

        public void VerifyParentCategoryListIsAlphabetical()
        {
            var actualItems = Driver.GetAllItemsInOrderedList(_listParentCategories);
            var expectedItems = actualItems.OrderBy(x => x).ToList();
            CollectionAssert.AreEqual(actualItems, expectedItems,
                "The items in the Parent Category are not ordered alphabetically.");
        }

        public void VerifyChildCategoryListIsAlphabetical()
        {
            var actualItems = Driver.GetAllItemsInOrderedList(_listChildCategories);
            var expectedItems = actualItems.OrderBy(x => x).ToList();
            CollectionAssert.AreEqual(expectedItems, actualItems,
                "The items in the Child Category are not ordered alphabetically.");
        }

        public void VerifyGrandchildCategoryListIsAlphabetical()
        {
            var actualItems = Driver.GetAllItemsInOrderedList(_listGrandchildCategories);
            var expectedItems = actualItems.OrderBy(item => item).ToList();
            CollectionAssert.AreEqual(expectedItems, actualItems,
                "The items in the Grandchild Category are not ordered alphabetically.");
        }

        public void ClickParentCategory(string parentCategory)
        {
            Driver.SelectItemInOrderedList(_listParentCategories, parentCategory);
        }

        public void ClickChildCategory(Table childCategories)
        {
            var childCatsToSelect = childCategories.Rows.ToColumnList("Child Categories");
            Driver.SelectItemInOrderedList(_listChildCategories, childCatsToSelect);
        }

        public void ClickGrandchildCategory(Table grandchildCategories)
        {
            var grandChildCatsToSelect = grandchildCategories.Rows.ToColumnList("Grandchild Categories");
            Driver.SelectItemInOrderedList(_listGrandchildCategories, grandChildCatsToSelect);
        }

        public void ClickChildCategory(string childCategory)
        {
            Driver.SelectItemInOrderedList(_listChildCategories, childCategory);
        }

        public void ClickGrandchildCategory(string grandchildCategory)
        {
            Driver.SelectItemInOrderedList(_listGrandchildCategories, grandchildCategory);
        }

        public void VerifyChildCategoryListIsDisplayed()
        {
            Assert.IsTrue(Driver.WaitForItem(_listChildCategories), "The Child category list is not displayed.");
        }

        public void VerifyOnlyOneParentCategoryIsSelected(string parentCategory)
        {
            var selectedParentCategories = Driver.GetTexts(_selectedParentCategory);
            Assert.AreEqual(1, selectedParentCategories.Count,
                "There should be no more than one selected parent category.");
            Assert.AreEqual(parentCategory, selectedParentCategories[0],
                "The selected parent category is not as expected.");
        }

        public void VerifyGrandchildCategoryListIsDisplayed()
        {
            Assert.IsTrue(Driver.WaitForItem(_listGrandchildCategories),
                "The Grandchild category list is not displayed");
        }

        public void VerifyOnlyOneChildCategoryIsSelected(string childCategory)
        {
            var selectedParentCategories = Driver.GetTexts(_selectedChildCategory);
            Assert.AreEqual(1, selectedParentCategories.Count,
                "There should be no more than one selected parent category.");
            Assert.AreEqual(childCategory, selectedParentCategories[0],
                "The selected parent category is not as expected.");
        }

        public void VerifyGrandchildCategoriesAreTicked(Table table)
        {
            var categories = table.Rows.ToColumnList("Grandchild Categories");
            var actualTickedCategories = Driver.GetAllTickedItemsInOrderedList(_listGrandchildCategories);
            Assert.AreEqual(categories, actualTickedCategories,
                "The expected grandchild categories have not been ticked.");
        }

        public void VerifyGrandChildCategoryListIsNotDisplayed()
        {
            Assert.IsTrue(Driver.WaitUntilElementNotDisplayed(_listGrandchildCategories), "The grandchild categories");
        }

        public void ClickContinueOnSelectACategoryScreen()
        {
            WaitForSpinnerToDisappear();
            Driver.ClickItem(_buttonContinueSelectACategoryScreen);
            WaitForSpinnerToDisappear();
        }

        public void VerifySelectACategoryScreenIsNotDisplayed()
        {
            Assert.IsTrue(Driver.WaitUntilElementNotDisplayed(_headingSelectACategory),
                "The select a category screen is still displayed.");
        }

        public void VerifyChildCategoriesAreTicked(Table table)
        {
            var categories = table.Rows.ToColumnList("Child Categories");
            var actualTickedCategories = Driver.GetAllTickedItemsInOrderedList(_listChildCategories);
            Assert.AreEqual(categories, actualTickedCategories, "The expected child categories have not been ticked.");
        }

        public void UntickGrandchildCategories(Table table)
        {
            var categories = table.Rows.ToColumnList("Grandchild Categories");
            Driver.UntickCheckboxItemInOrderedList(_listGrandchildCategories, categories);
        }

        public void UntickChildCategories(Table table)
        {
            var categories = table.Rows.ToColumnList("Child Categories");
            Driver.UntickCheckboxItemInOrderedList(_listChildCategories, categories);
        }

        public void ClickBackOnSelectACategoryScreen()
        {
            WaitForSpinnerToDisappear();
            Driver.ClickItem(_buttonBackSelectACategoryScreen);
        }

        public void ClickBackToWhatHappened()
        {
            WaitForSpinnerToDisappear();
            Driver.ClickItem(_buttonBackToWhatHappened);
        }

        public void VerifyCountOnParentCategory(string parentCategory, int count)
        {
            Driver.CheckCategorySelectionCount(_listParentCategories, parentCategory, count);
        }

        public void VerifyCountOnChildCategory(string childCategory, int count)
        {
            Driver.CheckCategorySelectionCount(_listChildCategories, childCategory, count);
        }

        public void EnterSearchText(string searchText)
        {
            Driver.EnterText(_textBoxSearch, searchText);
        }

        public void GetAllCategories()
        {
            var allCategories = FlattenCategories(_apiCall.GetListOfAllCategories());
            ScenarioContext.Current["AllCategories"] = allCategories;
        }

        public List<string> FlattenCategories(List<Category> categories)
        {
            var allCategories = new List<string>();

            foreach (var category in categories)
            {
                if (category.IsParent)
                {
                    foreach (var child in category.Children)
                    {
                        if (child.IsParent)
                        {
                            foreach (var grandchild in child.Children)
                            {
                                allCategories.Add($"{category.Name} - {child.Name} - {grandchild.Name.TrimEnd()}");
                            }
                        }
                        else
                        {
                            allCategories.Add($"{category.Name} - {child.Name.TrimEnd()}");
                        }
                    }
                }
            }

            return allCategories;
        }

        public void StoreExpectedSearchResult(string searchText)
        {
            var allCategories = ScenarioContext.Current.Get<List<string>>("AllCategories");

            var expectedCategories = new List<string>();
            foreach (var category in allCategories)
            {
                if (category.ToLower().Contains(searchText.ToLower()))
                {
                    expectedCategories.Add(category);
                }
            }
            ScenarioContext.Current["ExpectedCategories"] = expectedCategories;
        }

        public void VerifySearchCategoriesDisplayed()
        {
            var searchResults = Driver.GetTexts(_searchedCategories).OrderBy(x => x);

            var expectedCategories = ScenarioContext.Current.Get<List<string>>("ExpectedCategories");

            Assert.AreEqual(expectedCategories, searchResults, "The search results are not as expected.");
        }

        public void TickASearchedCategory(Table table)
        {
            var grandChildCatsToSelect = table.Rows.ToColumnList("Categories");
            Driver.SelectItemInOrderedList(_searchedCategoriesContainer, grandChildCatsToSelect);
        }

        public void VerifySelectedSearchedCategories(Table table)
        {
            var selectedGrandchildCategories = table.Rows.ToColumnList("Categories");
            var tickedCategories = Driver.GetAllTickedItemsInOrderedList(_searchedCategoriesContainer);
            Assert.AreEqual(selectedGrandchildCategories, tickedCategories,
                "The expected selected categories from the search results are not being displayed.");
        }

        public void UntickASearchedCategory(Table table)
        {
            var grandChildCatsToSelect = table.Rows.ToColumnList("Categories");
            Driver.UntickCheckboxItemInOrderedList(_searchedCategoriesContainer, grandChildCatsToSelect);
        }

        public void VerifySearchFieldIsEmpty()
        {
            var searchText = Driver.GetInputBoxValue(_textBoxSearch);

            Assert.IsEmpty(searchText, "The search field is not empty.");
        }

        public void VerifyNoSearchResultsMessage(string message)
        {
            var actualMessage = Driver.GetText(_noSearchResultsMessage);

            Assert.AreEqual(message, actualMessage, "No search results message is not displayed.");
        }

        public void VerifyThereAreNoSearchResultsDisplayed()
        {
            Assert.IsFalse(Driver.WaitForItem(_searchedCategories, 5),
                "There should not be any search results displayed.");
        }

        public void ClickRemoveAllButton()
        {
            WaitForSpinnerToDisappear();
            Driver.ClickItem(_removeAllbutton);
        }

        public void VerifyNoSelectedChildCategoriesTicked()
        {
            var listChildCategories = Driver.GetAllTickedItemsInOrderedList(_listChildCategories);
            var count = listChildCategories.Count;
            Assert.True(count.Equals(0), $"The count of ticked child categories should be 0 but is {count}");
        }

        public void VerifyNoSelectedGrandchildCategoriesTicked()
        {
            var listGrandchildCategories = Driver.GetAllTickedItemsInOrderedList(_listGrandchildCategories);
            var count = listGrandchildCategories.Count;
            Assert.True(count.Equals(0), $"The count of ticked grandchild categories should be 0 but is {count}");
        }

        public void EnterInitialSummary(Table table)
        {
            var row = table.Rows[0];
            Assert.IsTrue(Driver.WaitForItem(_inputInitialSummary), "The initial summary input field is not displayed");
            Driver.EnterText(_inputInitialSummary, row["Initial Summary"]);
        }

        public void EnterCaseNotes(Table table)
        {
            var row = table.Rows[0];
            Assert.IsTrue(Driver.WaitForItem(_inputCaseNotes), "The case notes input field is not displayed");
            Driver.EnterText(_inputCaseNotes, row["Case Notes"]);
        }

        public void VerifyContinueButtonDisabled()
        {
            Assert.IsTrue(Driver.WaitForItem(_btnContinueDisabled));
        }

        public void VerifyContinueButtonEnabled()
        {
            Assert.IsTrue(Driver.WaitForItem(_btnContinue));
        }

        public void VerifyInitialSummaryText(Table table)
        {
            var row = table.Rows[0];
            var expectedInitialSummary = row["Initial Summary"];
            var actualInitialSummary = Driver.GetInputBoxValue(_inputInitialSummary);

            Assert.AreEqual(expectedInitialSummary, actualInitialSummary, "The initial summary text is incorrect on the case overview page");
        }

        public void VerifyNotesText(Table table)
        {
            var row = table.Rows[0];
            var expectedNotes= row["Case Notes"];
            var actualNotes = Driver.GetInputBoxValue(_inputCaseNotes);

            Assert.AreEqual(expectedNotes, actualNotes, "The case notes is incorrect on the case overview page");
        }

        public void VerifyCharacterLimitMessage(string message)
        {
           var actualMessage = Driver.GetText(_characterLimitMessage);
            Assert.AreEqual(message, actualMessage, "The character limit message is incorrect");
        }

        public void VerifyCharactersRemainingBehaviour()
        {
            var random = new Random();
            var i = random.Next(0, 500);

            const int characterlimit = 500;

            var expectedCount = characterlimit - i;
            var text = PageHelper.RandomString(i);

            Driver.EnterText(_inputInitialSummary, text);

            var message = Driver.GetText(_characterLimitMessage).Split(' ');
            var actualCount = message[0];

            Assert.AreEqual(expectedCount.ToString(), actualCount, "The character limit count is not correct");
        }

        public void VerifyCharacterLimitInitialSummary(int charlimit)
        {
            Assert.True(Driver.CheckMaxLength(_inputInitialSummary, charlimit));
        }

        public void VerifyValidationMessage(string message)
        {
            Assert.AreEqual(message, Driver.GetText(_validationMessage),
                "The validation message is not being displayed.");
        }
    }
}