using System.Collections.Generic;
using J2BIOverseasOps.Pages.Customer_Interaction;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.Customer_Interaction
{
    [Binding]
    public sealed class WhatHappenedStepDefs : BaseStepDefs
    {
        private readonly WhatHappenedPage _whatHappened;

        public WhatHappenedStepDefs(IWebDriver driver, ILog log, IRunData runData) : base(driver, log)
        {
            _whatHappened = new WhatHappenedPage(driver, log, runData);
        }

        [When(@"I select the following categories from the categories dropdown on the what happened page:")]
        public void WhenISelectTheFollowingCategoriesFromTheCategoriesDropdownOnTheWhatHappenedPage(Table table)
        {
            var selectedCategories = new Table("SelectedCategories");

            _whatHappened.ClickAddACategory();
            _whatHappened.VerifyAddACategoryScreenIsDisplayed();
            foreach (var row in table.Rows)
            {
                _whatHappened.ClickParentCategory(row["Parent Categories"]);
                _whatHappened.VerifyChildCategoryListIsDisplayed();
                _whatHappened.ClickChildCategory(row["Child Categories"]);
                if (!string.IsNullOrWhiteSpace(row["Grandchild Categories"]))
                {
                    _whatHappened.VerifyGrandchildCategoryListIsDisplayed();
                    _whatHappened.ClickGrandchildCategory(row["Grandchild Categories"]);
                    selectedCategories.AddRow(row["Grandchild Categories"]);
                }
                else
                {
                    selectedCategories.AddRow(row["Child Categories"]);
                }
            }

            _whatHappened.ClickBackToWhatHappened();
            _whatHappened.VerifySelectACategoryScreenIsNotDisplayed();
            ScenarioContext.Current["SelectedCategories"] = selectedCategories;

            var affectedCustomersList = new List<string>();
            ScenarioContext.Current["affectedCustomersList"] = affectedCustomersList;
        }

        [When(@"I click the continue button on the what happened page")]
        public void WhenIClickTheContinueButtonOnTheWhatHappenedPage()
        {
            
            _whatHappened.ClickContinue();
        }

        [Then(@"I verify I can see the following selected case categories on the what happened page:")]
        public void ThenIVerifyICanSeeTheFollowingSelectedCaseCategoriesOnTheWhatHappenedPage(Table table)
        {
            _whatHappened.VerifySelectedCategories(table);
        }

        [Then(@"I verify that no categories have been selected on the what happened page")]
        public void ThenIVerifyThatNoCategoriesHaveBeenSelectedOnTheWhatHappenedPage()
        {
            _whatHappened.VerifyNoSelectedCategories();
        }

        [When(@"I click to remove ""(.*)"" from the selected categories on the what happened page")]
        public void WhenIClickToRemoveAFromTheSelectedCategoriesOnTheWhatHappenedPage(string category)
        {
            _whatHappened.RemoveASingleCategory(category);
        }

        [When(@"I click to remove the following categories from the selected categories section on the what happened page:")]
        public void WhenIClickToRemoveTheFollowingCategoriesFromTheSelectedCategoriesSectionOnTheWhatHappenedPage(Table table)
        {
            _whatHappened.RemoveMultipleCategories(table);
        }

        [When(@"I click add a category on the what happened page")]
        public void WhenIClickAddACategoryOnTheWhatHappenedPage()
        {
            _whatHappened.ClickAddACategory();
        }

        [Then(@"the select a category screen is displayed")]
        public void ThenTheAddACategoryScreenIsDisplayed()
        {
            _whatHappened.VerifyAddACategoryScreenIsDisplayed();
        }

        [Then(@"the parent category list is ordered alphabetically on the select a category screen")]
        public void ThenTheParentCategoryListIsOrderedAlphabeticallyOnTheSelectACategoryScreen()
        {
            _whatHappened.VerifyParentCategoryListIsAlphabetical();
        }

        [When(@"I click the parent category '(.*)' on the select a category screen")]
        public void WhenIClickTheParentCategoryOnTheSelectACategoryScreen(string parentCategory)
        {
            _whatHappened.ClickParentCategory(parentCategory);
        }

        [Then(@"the child category list is displayed on the select a category screen")]
        public void ThenTheChildCategoryListIsDisplayedOnTheSelectACategoryScreen()
        {
            _whatHappened.VerifyChildCategoryListIsDisplayed();
        }

        [Then(@"I verify that only '(.*)' is selected in the parent category list on the select a category screen")]
        public void ThenIVerifyThatOnlyIsSelectedInTheParentCategoryListOnTheSelectACategoryScreen(
            string parentCategory)
        {
            _whatHappened.VerifyOnlyOneParentCategoryIsSelected(parentCategory);
        }

        [Then(@"the child category list is ordered alphabetically on the select a category screen")]
        public void ThenTheChildCategoryListIsOrderedAlphabeticallyOnTheSelectACategoryScreen()
        {
            _whatHappened.VerifyChildCategoryListIsAlphabetical();
        }

        [When(@"I click the following child categories on the select a category screen:")]
        public void WhenIClickTheFollowingChildCategoriesOnTheSelectACategoryScreen(Table table)
        {
            _whatHappened.ClickChildCategory(table);
        }

        [Then(@"the grandchild category list is displayed on the select a category screen")]
        public void ThenTheGrandchildCategoryListIsDisplayedOnTheSelectACategoryScreen()
        {
            _whatHappened.VerifyGrandchildCategoryListIsDisplayed();
        }

        [Then(@"the grandchild category list is ordered alphabetically on the select a category screen")]
        public void ThenTheGrandchildCategoryListIsOrderedAlphabeticallyOnTheSelectACategoryScreen()
        {
            _whatHappened.VerifyGrandchildCategoryListIsAlphabetical();
        }

        [When(@"I click the following grandchild categories on the select a category screen:")]
        public void WhenIClickTheFollowingGrandchildCategoriesOnTheSelectACategoryScreen(Table table)
        {
            _whatHappened.ClickGrandchildCategory(table);
        }

        [Then(@"I verify that the following grandchild categories on the select a category screen will be ticked:")]
        public void ThenIVerifyThatTheFollowingGrandchildCategoriesOnTheSelectACategoryScreenWillBeTicked(Table table)
        {
            _whatHappened.VerifyGrandchildCategoriesAreTicked(table);
        }

        [Then(@"the grandchild category list is not displayed on the select a category screen")]
        public void ThenTheGrandchildCategoryListIsNotDisplayedOnTheSelectACategoryScreen()
        {
            _whatHappened.VerifyGrandChildCategoryListIsNotDisplayed();
        }

        [When(@"I click continue on the select a category screen")]
        public void WhenIClickContinueOnTheSelectACategoryScreen()
        {
            _whatHappened.ClickContinueOnSelectACategoryScreen();
        }

        [When(@"I click back on the select a category screen")]
        public void WhenIClickBackOnTheSelectACategoryScreen()
        {
            _whatHappened.ClickBackOnSelectACategoryScreen();
        }

        [When(@"I click back to what happened on the select a category screen")]
        public void WhenIClickBackToWhatHappenedOnTheSelectACategoryScreen()
        {
            _whatHappened.ClickBackToWhatHappened();
        }

        [Then(@"the select a category screen is not displayed")]
        public void ThenTheSelectACategoryScrenIsNotDisplayed()
        {
            _whatHappened.VerifySelectACategoryScreenIsNotDisplayed();
        }

        [Then(@"I verify that the following child categories on the select a category screen will be ticked:")]
        public void ThenIVerifyThatTheFollowingChildCategoriesOnTheSelectACategoryScreenWillBeTicked(Table table)
        {
            _whatHappened.VerifyChildCategoriesAreTicked(table);
        }

        [When(@"I untick the following grandchild categories on the select a category screen:")]
        public void WhenIUntickTheFollowingGrandchildCategoriesOnTheSelectACategoryScreen(Table table)
        {
            _whatHappened.UntickGrandchildCategories(table);
        }

        [When(@"I untick the following child categories on the select a category screen:")]
        public void WhenIUntickTheFollowingChildCategoriesOnTheSelectACategoryScreen(Table table)
        {
            _whatHappened.UntickChildCategories(table);
        }

        [Then(@"I verify the count on parent category '(.*)' is '(.*)' on the select a category screen")]
        public void ThenIVerifyTheCountOnParentCategoryIsOnTheSelectACategoryScreen(string parentCategory, int count)
        {
            _whatHappened.VerifyCountOnParentCategory(parentCategory, count);
        }

        [Then(@"I verify the count on child category '(.*)' is '(.*)' on the select a category screen")]
        public void ThenIVerifyTheCountOnChildCategoryIsOnTheSelectACategoryScreen(string childCategory, int count)
        {
            _whatHappened.VerifyCountOnChildCategory(childCategory, count);
        }

        [When(@"I enter '(.*)' into the search field on the select a category screen")]
        public void WhenIEnterIntoTheSearchFieldOnTheSelectACategoryScreen(string searchText)
        {
            _whatHappened.EnterSearchText(searchText);
            if (ScenarioContext.Current.ContainsKey("AllCategories"))
            {
                _whatHappened.StoreExpectedSearchResult(searchText);
            }
        }

        [When(@"I retrieve all the categories from the category API")]
        public void WhenIRetrieveAllTheCategoriesFromTheCategoryApi()
        {
            _whatHappened.GetAllCategories();
        }

        [Then(
            @"I verify that the search results are displayed with a parent and/or child and ordered alphabetically on the select a category screen")]
        public void ThenIVerifyThatTheCorrectSearchResultsAreDisplayedWithAParentAndOrChildOnTheSelectACategoryScreen()
        {
            _whatHappened.VerifySearchCategoriesDisplayed();
        }

        [When(@"I select the following searched categories:")]
        public void WhenISelectTheFollowingSearchedCategories(Table table)
        {
            _whatHappened.TickASearchedCategory(table);
        }

        [Then(@"I verify that the following searched categories should be selected:")]
        public void ThenIVerifyThatTheFollowingSearchedCategoriesShouldBeSelected(Table table)
        {
            _whatHappened.VerifySelectedSearchedCategories(table);
        }

        [When(@"I deselect the following searched categories:")]
        public void WhenIDeselectTheFollowingSearchedCategories(Table table)
        {
            _whatHappened.UntickASearchedCategory(table);
        }

        [Then(@"I verify that the search field is empty on the select a category screen")]
        public void ThenIVerifyThatTheSearchFieldIsEmptyOnTheSelectACategoryScreen()
        {
            _whatHappened.VerifySearchFieldIsEmpty();
        }

        [Then(@"I verify that a message stating '(.*)' is displayed on the select a category screen")]
        public void ThenIVerifyThatAMessageStatingIsDisplayedOnTheSelectACategoryScreen(string message)
        {
            _whatHappened.VerifyNoSearchResultsMessage(message);
        }

        [Then(@"I verify that there are no search results displayed on the select a category screen")]
        public void ThenIVerifyThatThereAreNoSearchResultsDisplayedOnTheSelectACategoryScreen()
        {
            _whatHappened.VerifyThereAreNoSearchResultsDisplayed();
        }

        [When(@"I click the remove all button on the what happened page")]
        public void WhenIClickTheRemoveAllButtonOnTheWhatHappenedPage()
        {
            _whatHappened.ClickRemoveAllButton();
        }

        [Then(@"I verify that no child categories have been ticked on the select a category screen")]
        public void ThenIVerifyThatNoChildCategoriesHaveBeenTickedOnTheSelectACategoryScreen()
        {
            _whatHappened.VerifyNoSelectedChildCategoriesTicked();
        }

        [Then(@"I verify that no grandchild categories have been ticked on the select a category screen")]
        public void ThenIVerifyThatNoGrandchildCategoriesHaveBeenTickedOnTheSelectACategoryScreen()
        {
            _whatHappened.VerifyNoSelectedGrandchildCategoriesTicked();
        }

        [When(@"I enter the following note in to the initial summary on the what happened page:")]
        public void WhenIEnterTheFollowingNoteInToTheInitialSummaryOnTheWhatHappenedPage(Table table)
        {
            _whatHappened.EnterInitialSummary(table);
        }

        [When(@"I enter the following note in to the case notes on the what happened page:")]
        public void WhenIEnterTheFollowingNoteInToTheCaseNotesOnTheWhatHappenedPage(Table table)
        {
            _whatHappened.EnterCaseNotes(table);
        }

        [Then(@"I verify the continue button is disabled on the what happened page")]
        public void ThenIVerifyTheContinueButtonIsDisabledOnTheWhatHappenedPage()
        {
            _whatHappened.VerifyContinueButtonDisabled();
        }

        [Then(@"I verify the continue button is enabled on the what happened page")]
        public void ThenIVerifyTheContinueButtonIsEnabledOnTheWhatHappenedPage()
        {
            _whatHappened.VerifyContinueButtonEnabled();
        }

        [Then(@"I verify the Initial summary text displayed on the what happened page as:")]
        public void ThenIVerifyTheInitialSummaryTextDisplayedOnTheWhatHappenedPageAs(Table table)
        {
            _whatHappened.VerifyInitialSummaryText(table);
        }

        [Then(@"I verify the note text displayed on the what happened page as:")]
        public void ThenIVerifyTheNoteTextDisplayedOnTheWhatHappenedPageAs(Table table)
        {
            _whatHappened.VerifyNotesText(table);
        }

        [Then(@"I verify the text on the character limit message on the what happened is ""(.*)""")]
        public void ThenIVerifyTheTextOnTheCharacterLimitMessageOnTheWhatHappenedIs(string message)
        {
            _whatHappened.VerifyCharacterLimitMessage(message);
        }

        [Then(@"I verify the count on the character limit message decreases as I enter characters")]
        public void ThenIVerifyTheCountOnTheCharacterLimitMessageDecreasesAsIEnterCharacters()
        {
            _whatHappened.VerifyCharactersRemainingBehaviour();
        }

        [Then(@"I verify only (.*) charactors can be entered in to the free type field on the what happened page")]
        public void ThenIVerifyOnlyCharactorsCanBeEnteredInToTheFreeTypeFieldOnTheWhatHappenedPage(int charlimit)
        {
            _whatHappened.VerifyCharacterLimitInitialSummary(charlimit);
        }

        [Then(@"a validation message '(.*)' is displayed on the what happened page")]
        public void ThenAValidationMessageIsDisplayedOnTheWhatHappenedPage(string message)
        {
            _whatHappened.VerifyValidationMessage(message);
        }


    }
}