using System.Collections.Generic;
using System.Linq;
using J2BIOverseasOps.Extensions;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using static System.StringComparison;

namespace J2BIOverseasOps.Pages.KnowledgeBaseManagement
{
    public class KBMSearchPage : BasePage
    {
        private static int _index;
        private readonly By _categoryMultiselect = By.Id("kb-search-categories-ms");
        private readonly By _nameLabels = By.CssSelector("[id^='kb-search-item-headng-name-']");
        private readonly By _typeLabels = By.CssSelector("[id^='kb-search-type-']");

        public KBMSearchPage(IWebDriver driver, ILog log) : base(driver, log)
        {
        }

        public By GlobalIcon => By.CssSelector($"#ui-panel-{_index} .pi-globe");
        public By PhoneLabel => By.Id($"kb-search-phonenumber-{_index}");
        public By EmailLabel => By.Id($"kb-search-emailaddress-{_index}");
        public By CategoryLabels => By.CssSelector($"[id^='kb-search-category-']");
        public By CategoryLabel => By.Id($"kb-search-category-{_index}");
        public By WebsiteLabel => By.Id($"kb-search-website-{_index}");
        public By OpeningHoursLabel => By.Id($"kb-search-opening-hours-{_index}");
        public By Address1Label => By.Id($"kb-search-addressLine1-{_index}");
        public By Address2Label => By.Id($"kb-search-addressLine2-{_index}");
        public By CityTownLabel => By.Id($"kb-search-cityTown-{_index}");
        public By PostCodeLabel => By.Id($"kb-search-postZipCode-{_index}");
        public By TypeLabel => By.Id($"kb-search-type-{_index}");
        public By SearchButton => By.Id("kb-search-submit-btn");
        public By NoteButton => By.Id($"kb-serach-notes-btn-{_index}");
        public By NotePopup => By.CssSelector("[role=\'dialog\']");
        public By NoteText => By.Id("dialog-note");
        public By AddNewItemButton => By.Id("kb-add-new-btn");
        public By DestinationDdl => By.Id("kb-search-destinations-ddl");
        public By ResortDdl => By.Id("kb-search-resorts-ddl");
        public By PropertyDdl => By.Id("kb-search-properties-ddl");
        public By ResultsNotfoundLabel => By.Id("kb-search-no-result-found");
        public By FilterTextBox => By.Id("kb-search-search-text");

        public By ClearFilterTextBox => By.CssSelector("#kb-search-search-text + button");
        public By EditButton => By.Id($"kb-search-edit-btn-{_index}");
        public By EditButtons => By.CssSelector($"[id='kb-search-edit-btn-']");
        public By CloseNoteButton => By.CssSelector(".ui-dialog-titlebar-close");
        public By ClearButton => By.Id("kb-search-clear-btn");
        public By LocationLink => By.Id($"kb-search-location-{_index}");


        public void SelectCategory(string category)
        {
            var categories = category.ConvertStringIntoList();

            Driver.SelectMultiselectOptionWithOverlay(_categoryMultiselect, categories);
        }

        public void ClickSearch()
        {
            Driver.ClickItem(SearchButton);
        }

        public void VerifySearchResultsContains(TableRow row, int i)
        {
            var name = row["Name"].Equals("Random") ? ScenarioContext.Current.Get<string>($"KBIName{i}") : row["Name"];
            SetIndex(name);

            if (row.ContainsKey("Phone"))
            {
                Assert.AreEqual(row["Phone"], Driver.GetText(PhoneLabel), "The phone number is not as expected.");
            }

            if (row.ContainsKey("Email"))
            {
                Assert.AreEqual(row["Email"], Driver.GetText(EmailLabel), "The Email is not as expected.");
            }

            if (row.ContainsKey("Website"))
            {
                Assert.AreEqual(row["Website"], Driver.GetText(WebsiteLabel), "The Website is not as expected.");
            }

            if (row.ContainsKey("Category"))
            {
                var category = Driver.GetText(CategoryLabel);

                category = category.Split(' ')[0];

                Assert.AreEqual(row["Category"], category, "The Category is not as expected.");
            }

            if (row.ContainsKey("OpeningHours"))
            {
                Assert.AreEqual(row["OpeningHours"], Driver.GetText(OpeningHoursLabel),
                    "The Opening Hours is not as expected.");
            }

            if (row.ContainsKey("Address1"))
            {
                Assert.AreEqual(row["Address1"], Driver.GetText(Address1Label),
                    "The Address1 is not as expected.");
            }

            if (row.ContainsKey("Address2"))
            {
                Assert.AreEqual(row["Address2"], Driver.GetText(Address2Label),
                    "The Address2 is not as expected.");
            }

            if (row.ContainsKey("CityTown"))
            {
                Assert.AreEqual(row["CityTown"], Driver.GetText(CityTownLabel),
                    "The CityTown is not as expected.");
            }

            if (row.ContainsKey("PostCode"))
            {
                Assert.AreEqual(row["PostCode"], Driver.GetText(PostCodeLabel),
                    "The PostCode is not as expected.");
            }

            if (row.TryGetValue("Global", out var global) && global.Equals("true", InvariantCultureIgnoreCase))
            {
                Assert.IsTrue(Driver.WaitForItem(GlobalIcon), "The global icon is not displayed for the global item.");
            }
            else if (global != null && global.Equals("false", InvariantCultureIgnoreCase))
            {
                Assert.IsFalse(Driver.WaitForItem(GlobalIcon, 1),
                    "The global icon should not be displayed for a non-global item.");
            }

            if (row.ContainsKey("Type") && !string.IsNullOrWhiteSpace(row["Type"]))
            {
                var type = Driver.GetText(TypeLabel);
                if (!string.IsNullOrWhiteSpace(type))
                {
                    type = type.Replace("(", string.Empty).Replace(")", string.Empty);
                }

                Assert.AreEqual(row["Type"], type, "The Type is not as expected.");
            }
            else
            {
                Assert.IsFalse(Driver.WaitForItem(TypeLabel, 1), "The type is being displayed.");
            }

            if (row.ContainsKey("Location") && !string.IsNullOrWhiteSpace(row["Location"]))
            {
                var link = Driver.FindElement(LocationLink);
                Assert.IsTrue(link.TagName.Equals("a"), "The location map is not a link");
                Assert.AreEqual(row["Location"], link.GetAttribute("href"), "The location map link href value is not as expected.");
            }
            else if (row.ContainsKey("Location") && string.IsNullOrWhiteSpace(row["Location"]))
            {                
                Assert.IsFalse(Driver.WaitForItem(LocationLink, 1), "The location map link should not be displayed");
            }

            if (row.ContainsKey("Note") && !string.IsNullOrWhiteSpace(row["Note"]))
            {
                Driver.ClickItem(NoteButton);
                VerifyNotePopupIsDisplayed();
                VerifyNoteIsDisplayed(row["Note"]);
                ClickCloseNotePopup();
            }
            else if (row.ContainsKey("Note") && string.IsNullOrWhiteSpace(row["Note"]))
            {
                Assert.IsFalse(Driver.IsElementEnabled(NoteButton), "The note button is not disabled.");
            }            
        }

        public void VerifyItemNotInResults()
        {
            var name = ScenarioContext.Current.Get<string>("Name");

            if (Driver.WaitForItem(ResultsNotfoundLabel, 2))
            {
                Assert.IsTrue(Driver.WaitForItem(ResultsNotfoundLabel), "There are search results displayed.");
            }
            else
            {
                var names = Driver.GetTexts(_nameLabels);

                Assert.IsFalse(names.Contains(name),
                    "The item is in the search results for the wrong search criteria.");
            }
        }

        public void SetIndex(string name)
        {
            Assert.IsTrue(Driver.WaitForItem(_nameLabels), "The results are not being displayed");
            var titles = Driver.FindElements(_nameLabels);

            _index = 0;
            foreach (var title in titles)
            {
                if (Driver.GetText(title).Equals(name, OrdinalIgnoreCase))
                {
                    return;
                }

                _index++;
            }

            Assert.Fail($"The Knowledge base item {name} was not found in the KBM search results.");
        }

        public void ClickNote()
        {
            var name = ScenarioContext.Current.Get<string>("Name");
            SetIndex(name);
            Driver.ClickItem(NoteButton);
        }

        public void VerifyNotePopupIsDisplayed()
        {
            Assert.IsTrue(Driver.WaitForItem(NotePopup), "The note popup is not displayed.");
        }

        public void VerifyNoteIsDisplayed(string note)
        {
            Assert.AreEqual(note, Driver.GetText(NoteText), "The note text is not as expected.");
        }

        public void VerifyNoteButtonIsDisabled()
        {
            var name = ScenarioContext.Current.Get<string>("Name");
            SetIndex(name);

            Assert.IsFalse(Driver.IsElementEnabled(NoteButton), "The note button is not disabled.");
        }

        public void AddNewItem()
        {
            Driver.ClickItem(AddNewItemButton);
        }

        public void SelectAllCategories()
        {
            Driver.SelectAllMultiselectOptionsWithOverlay(_categoryMultiselect);
        }

        public void VerifySearchResultsAreOrderedByName()
        {
            Assert.IsTrue(Driver.WaitForItem(_nameLabels), "The results are not being displayed");
            var titles = Driver.FindElements(_nameLabels);
            var names = Driver.GetTexts(titles);
            var expectedNames = names.OrderBy(x => x);

            CollectionAssert.AreEqual(expectedNames, names, "The KBM search results are not ordered alphabetically.");
        }

        public void VerifySeachResultsAreOfCategories(string categories)
        {
            var cats = categories.ConvertStringIntoList();

            Assert.IsTrue(Driver.WaitForItem(CategoryLabels), "The results are not being displayed");
            var titles = Driver.FindElements(CategoryLabels);
            var catgs = Driver.GetTexts(titles);
            catgs = catgs.Distinct().ToList();

            CollectionAssert.AreEquivalent(cats, catgs, "The category results are not as expected.");
        }

        public void SelectFilters(Table table)
        {
            var row = table.Rows[0];

            if (row.ContainsKey("Categories") && !string.IsNullOrWhiteSpace(row["Categories"]))
            {
                var cats = row["Categories"].ConvertStringIntoList();
                Driver.SelectMultiselectOptionWithOverlay(_categoryMultiselect, cats);
            }

            if (row.ContainsKey("Destination"))
            {
                Driver.ScrollToTheTop();
                Driver.SelectDropDownOption(DestinationDdl, row["Destination"]);
            }

            if (row.ContainsKey("Resort"))
            {
                Driver.SelectDropDownOption(ResortDdl, row["Resort"]);
            }

            if (row.ContainsKey("Property"))
            {
                Driver.SelectDropDownOption(PropertyDdl, row["Property"]);
            }
        }

        public void VerifyResortsIsFilteredByDestinationAndOrdered()
        {
            var actualItems = Driver.GetAllDropDownOptions(ResortDdl);
            actualItems.Remove("Select");
            var expectedItems = ScenarioContext.Current.Get<List<string>>("AllResorts").OrderBy(x => x).ToList();
            CollectionAssert.AreEqual(expectedItems, actualItems,
                "The items in the resorts are not all present according to the property API.");
        }

        public void VerifyPropertiesIsFilteredByDestinationAndOrdered()
        {
            var actualItems = Driver.GetAllDropDownOptions(PropertyDdl);
            actualItems.Remove("Select");
            var expectedItems = ScenarioContext.Current.Get<List<string>>("AllProperties").OrderBy(x => x).ToList();
            CollectionAssert.AreEqual(expectedItems, actualItems,
                "The items in the properties are not all present according to the property API.");
        }

        public void VerifyPropertiesIsFilteredByResortAndOrdered()
        {
            var actualItems = Driver.GetAllDropDownOptions(PropertyDdl);
            actualItems.Remove("Select");
            var expectedItems = ScenarioContext.Current.Get<List<string>>("AllPropertiesByDestinationAndResort")
                .OrderBy(x => x).ToList();
            CollectionAssert.AreEqual(expectedItems, actualItems,
                "The items in the properties are not all present according to the property API.");
        }

        public void VerifyFiltersSelected(Table table)
        {
            var row = table.Rows[0];

            if (row.ContainsKey("Category"))
            {
                var categories = row["Category"].ConvertStringIntoList();
                Assert.AreEqual(categories, Driver.GetAllSelectedMultiselectOptions(_categoryMultiselect),
                    "The Category selection is not as expected.");
            }

            if (row.ContainsKey("Destination"))
            {
                Assert.AreEqual(row["Destination"], Driver.GetSelectedDropDownOption(DestinationDdl),
                    "The Destination selection is not as expected.");
            }

            if (row.ContainsKey("Resort"))
            {
                Assert.AreEqual(row["Resort"], Driver.GetSelectedDropDownOption(ResortDdl),
                    "The resort selection is not as expected.");
            }

            if (row.ContainsKey("Property"))
            {
                Assert.AreEqual(row["Property"], Driver.GetSelectedDropDownOption(PropertyDdl),
                    "The Property selection is not as expected.");
            }

            if (row.ContainsKey("FilterSearch"))
            {
                var text = row["FilterSearch"].Equals("Name")
                    ? ScenarioContext.Current.Get<string>("Name")
                    : row["FilterSearch"];

                Assert.AreEqual(text, Driver.GetInputBoxValue(FilterTextBox),
                    "The Filter Search text is not as expected.");
            }
        }

        public void SelectDestinationUsingFilter(string destination)
        {
            Driver.SelectDropDownOption(DestinationDdl, destination, true);
        }

        public void VerifyDestinationDropdownSearchFieldIsEmpty()
        {
            var dropdown = Driver.FindElement(DestinationDdl);
            Driver.ClickItem(dropdown.FindElement(By.CssSelector("label")));
            Assert.IsTrue(Driver.WaitForItem(dropdown.FindElement(By.CssSelector(".ui-dropdown-filter"))),
                "The destination filter input box is not displayed.");
            Assert.IsTrue(
                string.IsNullOrWhiteSpace(
                    Driver.GetInputBoxValue(dropdown.FindElement(By.CssSelector(".ui-dropdown-filter")))),
                "The destination search filter is not empty.");
        }

        public void SelectResortUsingFilter(string resort)
        {
            Driver.SelectDropDownOption(ResortDdl, resort, true);
        }

        public void VerifyResortDropdownSearchFieldIsEmpty()
        {
            var dropdown = Driver.FindElement(ResortDdl);
            Driver.ClickItem(dropdown.FindElement(By.CssSelector("label")));
            Assert.IsTrue(Driver.WaitForItem(dropdown.FindElement(By.CssSelector(".ui-dropdown-filter"))),
                "The resort filter input box is not displayed.");
            Assert.IsTrue(
                string.IsNullOrWhiteSpace(
                    Driver.GetInputBoxValue(dropdown.FindElement(By.CssSelector(".ui-dropdown-filter")))),
                "The resort search filter is not empty.");
        }

        public void SelectPropertyUsingFilter(string property)
        {
            Driver.SelectDropDownOption(PropertyDdl, property, true);
        }

        public void VerifyPropertyDropdownSearchFieldIsEmpty()
        {
            var dropdown = Driver.FindElement(PropertyDdl);
            Driver.ClickItem(dropdown.FindElement(By.CssSelector("label")));
            Assert.IsTrue(Driver.WaitForItem(dropdown.FindElement(By.CssSelector(".ui-dropdown-filter"))),
                "The proeprty filter input box is not displayed.");
            Assert.IsTrue(
                string.IsNullOrWhiteSpace(
                    Driver.GetInputBoxValue(dropdown.FindElement(By.CssSelector(".ui-dropdown-filter")))),
                "The property search filter is not empty.");
        }

        public void VerifyRelationshipDropDownsAreDisabled()
        {
            Assert.IsTrue(Driver.IsElementDisabled(DestinationDdl), "The destination dropdown is enabled.");
            Assert.IsTrue(Driver.IsElementDisabled(ResortDdl), "The resort dropdown is enabled.");
            Assert.IsTrue(Driver.IsElementDisabled(PropertyDdl), "The property dropdown is enabled.");
        }

        public void EnterFilterSearchText(string text)
        {
            if (text.ToLower().Equals("name"))
            {
                text = ScenarioContext.Current.Get<string>("Name");
            }

            Driver.EnterText(FilterTextBox, text);
        }

        public void VerifySearchResultsContainsItemsOfType(string type)
        {
            var names = Driver.GetTexts(_nameLabels);
            var types = Driver.GetTexts(_typeLabels);
            var typesDeDuped = types.Distinct().ToList();
            var actualTypes = new List<string> {$"({type})"};
            Assert.AreEqual(actualTypes, typesDeDuped,
                $"The filtered search results have knowledg base items that do not havea type of {type}.");
            Assert.AreEqual(names.Count, types.Count, "The number of search results are not as expected.");
        }

        public void VerifySearchResultsDisplayedIsTheItemCreated()
        {
            Assert.IsTrue(Driver.WaitForItem(_nameLabels), "The search results are not being dsiplayed.");
            var names = Driver.GetTexts(_nameLabels);
            var name = ScenarioContext.Current.Get<string>("Name");
            Assert.IsTrue(names.Contains(name), "The KBM Item is not displayed in the search results.");
            Assert.IsTrue(names.Count == 1, "The are more then the expected results.");
        }

        public void ClearFilterSearchField()
        {
            Driver.ClickItem(ClearFilterTextBox);
        }

        public void VerifyFilterSearchFieldText(string text)
        {
            var actual = Driver.GetText(FilterTextBox);
            Assert.AreEqual(text, actual, "The filter text is not as expected.");
        }

        public void ClickEdit()
        {
            var name = ScenarioContext.Current.Get<string>("Name");
            SetIndex(name);
            Driver.ClickItem(EditButton);
        }

        public void ClickEditFor(int i)
        {
            var name = ScenarioContext.Current.Get<string>($"KBIName{i}");
            SetIndex(name);
            Driver.ClickItem(EditButton);
        }

        public void ClickCloseNotePopup()
        {
            Driver.ClickItem(CloseNoteButton);
            Assert.IsTrue(Driver.WaitUntilElementNotDisplayed(NotePopup), "The note popup is still being displayed");
        }

        public void ClickClear()
        {
            Driver.ScrollToTheTop();
            Driver.ClickItem(ClearButton);
        }

        public void VerifyAddAndEditItemButtonsAreNotDisplayed()
        {
            Assert.IsFalse(Driver.WaitForItem(AddNewItemButton, 1),
                "The add new item button should not be displayed from KBI.");
            Assert.IsFalse(Driver.WaitForItem(EditButtons, 1),
                "The add new item button should not be displayed from KBI.");
        }
    }
}