using System.Collections.Generic;
using System.Linq;
using J2BIOverseasOps.Pages.KnowledgeBaseManagement;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.KnowledgeBaseManagement
{
    [Binding]
    public sealed class KnowledgeBaseManagementSearchSteps : BaseStepDefs
    {
        private readonly KBMSearchPage _kbmSearchPage;
        private readonly CommonStepDefs _commonStepDefs;
        private readonly KbmEditItemPage _kbmEditItemPage;

        public KnowledgeBaseManagementSearchSteps(IWebDriver driver, ILog log) : base(driver, log)
        {
            _kbmSearchPage = new KBMSearchPage(driver, log);
            _commonStepDefs = new CommonStepDefs(driver, log, new RunData());
            _kbmEditItemPage = new KbmEditItemPage(driver, log);
        }

        [When(@"I select the category '(.*)' on the Knowledge Base Management Search page")]
        public void WhenISelectTheCategoryOnTheKnowledgeBaseManagementSearchPage(string category)
        {
            _kbmSearchPage.SelectCategory(category);
        }

        [When(@"I click search button on the Knowledge Base Management Search page")]
        public void WhenIClickSearchKnowledgeBaseManagementSearchPage()
        {
            _kbmSearchPage.ClickSearch();
        }

        [Then(@"I verify that the Knowledge Base Management search results contains:")]
        public void ThenIVerifyThatTheKnowledgeBaseManagementSearchResultsContains(Table table)
        {
            var rows = table.Rows;
            var i = 0;
            foreach (var row in rows)
            {
                _kbmSearchPage.VerifySearchResultsContains(row, i);
                i++;
            }
        }

        [When(@"I delete all the knowledge base items I created for this test scenario")]
        public void WhenIDeleteAllTheKnowledgeBaseItemsICreatedForThisTestScenario()
        {
            var items = ScenarioContext.Current.Keys.Where(k => k.StartsWith("KBIName")).ToList();
            for (var i = 0; i < items.Count; i++)
            {
                _commonStepDefs.GivenINavigateToPage("knowledge-base");
                _commonStepDefs.ThenIShouldBeNavigatedToPage("knowledge-base");
                _kbmSearchPage.SelectAllCategories();
                _kbmSearchPage.ClickSearch();
                _kbmSearchPage.ClickEditFor(i);
                _kbmEditItemPage.ClickDelete();
                _commonStepDefs.ThenAConfirmationPopupIsDisplayedStating("Delete item",
                    "Do you want to delete this item?");
                _commonStepDefs.WhenIClickOnTheNotesConfirmationDialog("Delete");
                _commonStepDefs.ThenTheNotesConfirmationDialogIsDismissed();
                _commonStepDefs.ThenIShouldBeNavigatedToPage("knowledge-base");
            }
        }

        [When(@"I click on the Note button on the Knowledge Base Management Search page")]
        public void WhenIClickOnTheNoteButtonOnTheKnowledgeBaseManagementSearchPage()
        {
            _kbmSearchPage.ClickNote();
        }

        [Then(@"I verify that the Note popup is displayed on the Knowledge Base Management Search page")]
        public void ThenIVerifyThatTheNotePopupIsDisplayedOnTheKnowledgeBaseManagementSearchPage()
        {
            _kbmSearchPage.VerifyNotePopupIsDisplayed();
        }

        [Then(@"I verify the the Note popup displays '(.*)' on the Knowledge Base Management Search page")]
        public void ThenIVerifyTheTheNotePopupDisplaysOnTheKnowledgeBaseManagementSearchPage(string note)
        {
            _kbmSearchPage.VerifyNoteIsDisplayed(note);
        }

        [Then(@"I click close note popup on the Knowledge Base Management Search page")]
        public void ThenIClickCloseNotePopupOnTheKnowledgeBaseManagementSearchPage()
        {
            _kbmSearchPage.ClickCloseNotePopup();
        }

        [Then(@"I verify that the Note button is disabled")]
        public void ThenIVerifyThatTheNoteButtonIsDisabled()
        {
            _kbmSearchPage.VerifyNoteButtonIsDisabled();
        }

        [When(@"I click the add new item button on the Knowledge Base Management Search page")]
        public void WhenIClickTheAddNewItemButtonOnTheKnowledgeBaseManagementSearchPage()
        {
            _kbmSearchPage.AddNewItem();
        }

        [When(@"I select all the categories on the Knowledge Base Management Search page")]
        public void WhenISelectAllCatageoriesOnTheAddNewKnowledgeBaseItemPage()
        {
            _kbmSearchPage.SelectAllCategories();
        }

        [Then(@"I verify that all the knowledge base items are ordered alphabetically by name")]
        public void ThenIVerifyThatAllTheKnowledgeBaseItemsAreOrderedAlphabeticallyByName()
        {
            _kbmSearchPage.VerifySearchResultsAreOrderedByName();
        }

        [Then(@"I verify that all the categories in the search results are '(.*)'")]
        public void ThenIVerifyThatAllTheCategoriesInTheSearchResultsAreDoctorEhic(string categories)
        {
            _kbmSearchPage.VerifySeachResultsAreOfCategories(categories);
        }

        [When(@"I select the following search filters on the Knowledge Base Management Search page:")]
        public void WhenISelectTheFollowingSearchFiltersOnTheKnowledgeBaseManagementSearchPage(Table table)
        {
            _kbmSearchPage.SelectFilters(table);
        }

        [Then(@"I verify that the following search filters are set on the Knowledge Base Management Search page:")]
        public void ThenIVerifyThatTheFollowingSearchFiltersAreSetOnTheKnowledgeBaseManagementSearchPage(Table table)
        {
            _kbmSearchPage.VerifyFiltersSelected(table);
        }


        [Then(
            @"I verify that the resorts list is filtered by the destination selected on the Knowledge Base Management Search page")]
        public void
            ThenIVerifyThatTheResortsListIsFilteredByTheDestinationSelectedOnTheKnowledgeBaseManagementSearchPage()
        {
            _kbmSearchPage.VerifyResortsIsFilteredByDestinationAndOrdered();
        }

        [Then(
            @"I verify that the properties list is filtered by the destination selected on the Knowledge Base Management Search page")]
        public void
            ThenIVerifyThatThePropertiesListIsFilteredByTheDestinationSelectedOnTheAddNewKnowledgeBaseItemRelationshipDialog()
        {
            _kbmSearchPage.VerifyPropertiesIsFilteredByDestinationAndOrdered();
        }

        [Then(
            @"I verify that the properties list is filtered by the resort selected on the Knowledge Base Management Search page")]
        public void
            ThenIVerifyThatThePropertiesListIsFilteredByTheResortSelectedOnTheKnowledgeBaseManagementSearchPage()
        {
            _kbmSearchPage.VerifyPropertiesIsFilteredByResortAndOrdered();
        }

        [Then(@"I verify that the Knowledge Base Management search results does not contain the item created")]
        public void ThenIVerifyThatTheKnowledgeBaseManagementSearchResultsDoesNotContainTheItemCreated()
        {
            _kbmSearchPage.VerifyItemNotInResults();
        }

        [When(
            @"I search and select the '(.*)' item from the destination list on the Knowledge Base Management Search page")]
        public void WhenISearchAndSelectTheItemFromTheDestinationListOnTheKnowledgeBaseManagementSearchPage(
            string destination)
        {
            _kbmSearchPage.SelectDestinationUsingFilter(destination);
        }

        [Then(
            @"I verify that the destination dropdown search field is empty on the Knowledge Base Management Search page")]
        public void ThenIVerifyThatTheDestinationDropdownSearchFieldIsEmptyOnTheKnowledgeBaseManagementSearchPage()
        {
            _kbmSearchPage.VerifyDestinationDropdownSearchFieldIsEmpty();
        }

        [When(@"I search and select the '(.*)' item from the resort list on the Knowledge Base Management Search page")]
        public void WhenISearchAndSelectTheItemFromTheResortListOnTheKnowledgeBaseManagementSearchPage(string resort)
        {
            _kbmSearchPage.SelectResortUsingFilter(resort);
        }

        [Then(@"I verify that the resort dropdown search field is empty on the Knowledge Base Management Search page")]
        public void ThenIVerifyThatTheResortDropdownSearchFieldIsEmptyOnTheKnowledgeBaseManagementSearchPage()
        {
            _kbmSearchPage.VerifyResortDropdownSearchFieldIsEmpty();
        }

        [When(
            @"I search and select the '(.*)' item from the property list on the Knowledge Base Management Search page")]
        public void WhenISearchAndSelectTheItemFromThePropertyListOnTheKnowledgeBaseManagementSearchPage(
            string property)
        {
            _kbmSearchPage.SelectPropertyUsingFilter(property);
        }

        [Then(
            @"I verify that the property dropdown search field is empty on the Knowledge Base Management Search page")]
        public void ThenIVerifyThatThePropertyDropdownSearchFieldIsEmptyOnTheKnowledgeBaseManagementSearchPage()
        {
            _kbmSearchPage.VerifyPropertyDropdownSearchFieldIsEmpty();
        }

        [Then(@"I verify that the destination, resort and property are disabled on the Knowledge Base Management Search page")]
        public void ThenIVerifyThatTheDestinationResortAndPropertyAreDisabled()
        {
            _kbmSearchPage.VerifyRelationshipDropDownsAreDisabled();
        }

        [When(@"I enter the '(.*)' into the Filter Search on the Knowledge Base Management Search page")]
        public void WhenIEnterTheIntoTheFilterSearchOnTheKnowledgeBaseManagementSearchPage(string text)
        {
            _kbmSearchPage.EnterFilterSearchText(text);
        }

        [Then(@"I verify that the Knowledge Base Management search results only contains items of Type '(.*)'")]
        public void ThenIVerifyThatTheKnowledgeBaseManagementSearchResultsOnlyContainsItemsOfType(string type)
        {
            _kbmSearchPage.VerifySearchResultsContainsItemsOfType(type);
        }

        [Then(@"I verify that the Knowledge Base Management search results only contains the item created")]

        public void ThenIVerifyThatTheKnowledgeBaseManagementSearchResultsOnlyContainsTheItemCreated()
        {
            _kbmSearchPage.VerifySearchResultsDisplayedIsTheItemCreated();
        }

        [When(@"I enter the name and type '(.*)' into the Filter Search on the Knowledge Base Management Search page")]
        public void WhenIEnterTheNameAndTypeIntoTheFilterSearchOnTheKnowledgeBaseManagementSearchPage(string type)
        {
            var name = ScenarioContext.Current["Name"];
            _kbmSearchPage.EnterFilterSearchText($"{name} {type}");
        }

        [When(@"I clear the Filter Search on the Knowledge Base Management Search page")]
        public void WhenIClearTheFilterSearchOnTheKnowledgeBaseManagementSearchPage()
        {
            _kbmSearchPage.ClearFilterSearchField();
        }

        [Then(@"I verify that the Filter Search is empty on the Knowledge Base Management Search page")]
        public void ThenIVerifyThatTheFilterSearchIsEmptyOnTheKnowledgeBaseManagementSearchPage()
        {
            _kbmSearchPage.VerifyFilterSearchFieldText(string.Empty);
        }

        [When(@"I click the edit button for the item created on the Knowledge Base Management Search page")]
        public void WhenIClickTheEditButtonForTheItemCreatedOnTheKnowledgeBaseManagementSearchPage()
        {
            _kbmSearchPage.ClickEdit();
        }

        [When(@"I click the clear button on the Knowledge Base Management Search page")]
        public void WhenIClickTheClearButtonOnTheKnowledgeBaseManagementSearchPage()
        {
            _kbmSearchPage.ClickClear();
        }

        [Then(@"I verify the add item and edit item buttons are not displayed")]
        public void ThenIVerifyTheAddItemAndEditItemButtonsAreNotDisplayed()
        {
            _kbmSearchPage.VerifyAddAndEditItemButtonsAreNotDisplayed();
        }


    }
}