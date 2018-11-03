using System;
using System.Collections.Generic;
using System.Linq;
using J2BIOverseasOps.Extensions;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Pages.KnowledgeBaseManagement
{
    public class KbmAddNewItemPage : BasePage
    {
        private readonly By _address1Textbox = By.Id("knowledge-base-address-line-1");
        private readonly By _address1Title = By.Id("knowledge-base-address-line-1-lbl");
        private readonly By _address2Textbox = By.Id("knowledge-base-address-line-2");
        private readonly By _address2Title = By.Id("knowledge-base-address-line-2-lbl");
        private readonly By _cityTownTextbox = By.Id("knowledge-base-city-town");
        private readonly By _cityTownTitle = By.Id("knowledge-base-city-town-lbl");
        private readonly By _emailTextbox = By.Id("knowledge-base-email");
        private readonly By _emailTitle = By.Id("knowledge-base-email-lbl");
        private readonly By _errorMessage = By.CssSelector("p-message .ui-message-text");
        private readonly By _header = By.Id("knowledge-base-header");
        private readonly By _nameTextbox = By.Id("knowledge-base-name");
        private readonly By _nameTitle = By.Id("knowledge-base-name-lbl");
        private readonly By _noteTextArea = By.Id("knowledge-base-post-note");
        private readonly By _noteTitle = By.Id("knowledge-base-post-note-lbl");
        private readonly By _phoneTextbox = By.Id("knowledge-base-phone-number");
        private readonly By _phoneTitle = By.Id("knowledge-base-phone-number-lbl");
        private readonly By _postCodeTextbox = By.Id("knowledge-base-post-zip-code");
        private readonly By _postCodeTitle = By.Id("knowledge-base-post-zip-code-lbl");
        private readonly By _saveBtn = By.Id("view-kb-save-btn");
        private readonly By _subHeading = By.Id("knowledge-base-sub-header");
        private readonly By _websiteTitle = By.Id("knowledge-base-website-lbl");
        private readonly By _websiteTextbox = By.Id("knowledge-base-website");
        private readonly By _locationTitle = By.Id("knowledge-base-location-map-lbl");
        private readonly By _locationTextbox = By.Id("knowledge-base-location-map");
        private readonly By _categoryTitle = By.Id("knowledge-base-category-lbl");
        private readonly By _categoryDdl = By.Id("knowledge-base-category-ddl");
        private readonly By _openingHoursTitle = By.Id("knowledge-base-opening-hours-lbl");
        private readonly By _openingHoursTextbox = By.Id("knowledge-base-opening-hours");
        private readonly By _typeTitle = By.Id("knowledge-base-type-lbl");
        private readonly By _typeDdl = By.Id("knowledge-base-types-ddl");
        private readonly By _relationshipsTitle = By.Id("knowledge-base-relationship-header");
        private readonly By _relationshipsDialogTitle = By.Id("ui-dialog-0-label");
        private readonly By _destinationsTitle = By.Id("knowledge-base-destinations-list-lbl");
        private readonly By _destinationsMultiSelect = By.Id("knowledge-base-destinations-list-ms");
        private readonly By _resortsTitle = By.Id("knowledge-base-resorts-list-lbl");
        private readonly By _resortsMultiSelect = By.Id("knowledge-base-resorts-list-ms");
        private readonly By _propertiesTitle = By.Id("knowledge-base-properties-list-lbl");
        private readonly By _propertiesMultiSelect = By.Id("knowledge-base-properties-list-ms");
        private readonly By _allDestinationsCheckbox = By.Id("knowledge-base-relationship-chk-all");
        private readonly By _addButton = By.Id("knowledge-base-open-dialog-btn");
        private readonly By _relationshipsDialog = By.CssSelector("#knowledge-base-realationship-dialog > div");
        private readonly By _addRelationshipButton = By.Id("knowledge-base-dialog-add-btn");
        private readonly By _cancelDialog = By.Id("knowledge-base-dialog-cancel-link");
        private readonly By _relationshipsPanel = By.CssSelector("p-scrollpanel p");



        public KbmAddNewItemPage(IWebDriver driver, ILog log) : base(driver, log)
        {
        }

        public void VerifyPageHeadings(string heading, string subHeading)
        {
            Assert.AreEqual(heading, Driver.GetText(_header), "The page header is not as expected.");
            Assert.AreEqual(subHeading, Driver.GetText(_subHeading), "The subheader is not as expected.");
        }

        public void VerifyContactDetailTitles(string name, string phone, string email, string website, string note, string location, string category, string openingHours, string type)
        {
            Assert.AreEqual(name, Driver.GetText(_nameTitle), "The name title is not as expected");
            Assert.AreEqual(phone, Driver.GetText(_phoneTitle), "The phone title is not as expected");
            Assert.AreEqual(email, Driver.GetText(_emailTitle), "The email title is not as expected");
            Assert.AreEqual(website, Driver.GetText(_websiteTitle), "The website title is not as expected");
            Assert.AreEqual(note, Driver.GetText(_noteTitle), "The note title is not as expected");
            Assert.AreEqual(location, Driver.GetText(_locationTitle), "The location title is not as expected");
            Assert.AreEqual(category, Driver.GetText(_categoryTitle), "The category title is not as expected");
            Assert.AreEqual(openingHours, Driver.GetText(_openingHoursTitle), "The opening hours title is not as expected");
            Assert.AreEqual(type, Driver.GetText(_typeTitle), "The opening hours title is not as expected");
        }

        public void VerifyAddressDetailTitles(string address1, string address2, string cityTown, string postCode)
        {
            Assert.AreEqual(address1, Driver.GetText(_address1Title), "The address1 title is not as expected");
            Assert.AreEqual(address2, Driver.GetText(_address2Title), "The address2 title is not as expected");
            Assert.AreEqual(cityTown, Driver.GetText(_cityTownTitle), "The city/Town title is not as expected");
            Assert.AreEqual(postCode, Driver.GetText(_postCodeTitle), "The post code title is not as expected");
        }

        public void VerifyRelationshipTitles(string relationships, string destinations, string resorts, string property)
        {
            Assert.AreEqual(relationships, Driver.GetText(_relationshipsTitle), "The relationships title is not as expected");
            Driver.ClickItem(_addButton);
            Assert.IsTrue(Driver.WaitForItem(_relationshipsDialog), "The relationship popup is not being displayed.");
            Assert.AreEqual(relationships, Driver.GetText(_relationshipsDialogTitle), "The relationships title on the add dialog is not as expected");
            Assert.AreEqual(destinations, Driver.GetText(_destinationsTitle), "The destinations title is not as expected");
            Assert.AreEqual(resorts, Driver.GetText(_resortsTitle), "The resorts title is not as expected");
            Assert.AreEqual(property, Driver.GetText(_propertiesTitle), "The properties title is not as expected");
        }

        public void EnterKbDetails(TableRow row, int i)
        {
            if (row.ContainsKey("Name"))
                {
                    var name = row["Name"].Equals("Random") ? Guid.NewGuid().ToString() : row["Name"];
                    ScenarioContext.Current["Name"] = name;
                    ScenarioContext.Current[$"KBIName{i}"] = name;
                    Driver.EnterText(_nameTextbox, name);
                }

                if (row.ContainsKey("Phone"))
                {
                    ScenarioContext.Current["Phone"] = row["Phone"];
                    Driver.EnterText(_phoneTextbox, row["Phone"]);
                }

                if (row.ContainsKey("Email"))
                {
                    ScenarioContext.Current["Email"] = row["Email"];
                    Driver.EnterText(_emailTextbox, row["Email"]);
                }

                if (row.ContainsKey("Website"))
                {
                    ScenarioContext.Current["Website"] = row["Website"];
                    Driver.EnterText(_websiteTextbox, row["Website"]);
                }

                if (row.ContainsKey("Location"))
                {
                    ScenarioContext.Current["Location"] = row["Location"];
                    Driver.EnterText(_locationTextbox, row["Location"]);
                }

                if (row.ContainsKey("Category"))
                {
                    ScenarioContext.Current["Category"] = row["Category"];
                    Driver.SelectDropDownOption(_categoryDdl, row["Category"]);
                }

                if (row.ContainsKey("OpeningHours"))
                {
                    ScenarioContext.Current["OpeningHours"] = row["OpeningHours"];
                    Driver.EnterText(_openingHoursTextbox, row["OpeningHours"]);
                }

                if (row.ContainsKey("Type"))
                {
                    ScenarioContext.Current["Type"] = row["Type"];
                    Assert.IsTrue(Driver.WaitForItem(_typeDdl), "The Type dropdown is not displayed.");
                    Driver.SelectDropDownOption(_typeDdl, row["Type"]);
                }
        }

        public void EnterKbAddressDetails(TableRow row)
        {
            Driver.EnterText(_address1Textbox, row["Address1"]);
            Driver.EnterText(_address2Textbox, row["Address2"]);
            Driver.EnterText(_cityTownTextbox, row["CityTown"]);
            Driver.EnterText(_postCodeTextbox, row["PostCode"]);

            ScenarioContext.Current["Address1"] = row["Address1"];
            ScenarioContext.Current["Address2"] = row["Address2"];
            ScenarioContext.Current["CityTown"] = row["CityTown"];
            ScenarioContext.Current["PostCode"] = row["PostCode"];
        }

        public void ClickSave()
        {
            Driver.ClickItem(_saveBtn);
        }

        public void EnterKbNote(string note)
        {
            ScenarioContext.Current["Note"] = note;
            Driver.EnterText(_noteTextArea, note);
        }

        public void VerifyValidationMessageIsDisplayed(string message)
        {
            Assert.AreEqual(message, Driver.GetText(_errorMessage), "The error message is not being displayed");
        }

        public void VerifyCategoryListAndOrder(Table table)
        {
            var categories = table.Rows.ToColumnList("Category");
            var actualItems = Driver.GetAllDropDownOptions(_categoryDdl);
            actualItems.Remove("Please select");
            var expectedItems = categories.OrderBy(x => x).ToList();
            CollectionAssert.AreEqual(expectedItems, actualItems,
                "The items in the Category are not all present and ordered alphabetically.");
        }

        public void TickAssignToAllDestinations()
        {
            ScenarioContext.Current["Global"] = true;
            Driver.TickCheckBox(_allDestinationsCheckbox);
        }

        public void UntickAssignToAllDestinations()
        {
            Driver.UntickCheckBox(_allDestinationsCheckbox);
        }

        public void SelectRelationship(TableRow row)
        {

            if (row.ContainsKey("Destinations") && !string.IsNullOrWhiteSpace(row["Destinations"]))
            {
                var destinations = row["Destinations"].ConvertStringIntoList();
                ScenarioContext.Current["Destinations"] = destinations;
                Driver.SelectMultiselectOption(_destinationsMultiSelect, destinations);
            }

            if (row.ContainsKey("Resorts") && !string.IsNullOrWhiteSpace(row["Resorts"]))
            {
                var resorts = row["Resorts"].ConvertStringIntoList();
                ScenarioContext.Current["Resorts"] = resorts;
                Driver.SelectMultiselectOption(_resortsMultiSelect, resorts);
            }

            if (row.ContainsKey("Properties") && !string.IsNullOrWhiteSpace(row["Properties"]))
            {
                var properties = row["Properties"].ConvertStringIntoList();
                ScenarioContext.Current["Properties"] = properties;
                Driver.SelectMultiselectOption(_propertiesMultiSelect, properties);
            }
        }

        public void DeselectRelationship(TableRow row)
        {
            if (row.ContainsKey("Destinations"))
            {
                var destinations = row["Destinations"].ConvertStringIntoList();
                Driver.DeselectMultiselectOption(_destinationsMultiSelect, destinations);
            }

            if (row.ContainsKey("Resorts"))
            {
                var resorts = row["Resorts"].ConvertStringIntoList();
                Driver.DeselectMultiselectOption(_resortsMultiSelect, resorts);
            }

            if (row.ContainsKey("Properties"))
            {
                var properties = row["Properties"].ConvertStringIntoList();
                Driver.SelectMultiselectOption(_propertiesMultiSelect, properties);
            }
        }

        public void ClickAdd()
        {
            Driver.ClickItem(_addButton);
            Assert.IsTrue(Driver.WaitForItem(_relationshipsDialog), "The relationship dialog is not being displayed");
        }

        public void ClickAddOnTheRelationshipDialog()
        {
            Driver.ClickItem(_addRelationshipButton);
        }

        public void VerifyRelationshipDialogIsDismissed()
        {
            Assert.IsTrue(Driver.WaitUntilElementNotDisplayed(_relationshipsDialog), "The relationship dialog is still being displayed.");
        }

        public void VerifyDesintationsIsInAlphabeticalOrder()
        {
            var actualItems = Driver.GetAllMultiselectOptions(_destinationsMultiSelect);
            actualItems.Remove("Select");
            var expectedItems = actualItems.OrderBy(x => x).ToList();
            CollectionAssert.AreEqual(expectedItems, actualItems,
                "The items in the Destinations are not all present and ordered alphabetically.");
        }

        public void ClickCancelOnTheRelationshipDialog()
        {
            Driver.ClickItem(_cancelDialog);
        }

        public void VerifyDestinationsSelectedOnTheRelationshipDialog(Table table)
        {
            var destinations = table.Rows[0]["Destinations"].ConvertStringIntoList();
            var actualDestinations = Driver.GetAllSelectedMultiselectOptions(_destinationsMultiSelect);
            CollectionAssert.AreEqual(destinations, actualDestinations, "The destinations selected are not as expected.");
        }

        public void VerifyDestinationsListIsCorrect()
        {
            var actualItems = Driver.GetAllMultiselectOptions(_destinationsMultiSelect);
            actualItems.Remove("Select");
            var expectedItems = ScenarioContext.Current.Get<List<string>>("AllDestinations").OrderBy(x => x).ToList();
            CollectionAssert.AreEqual(expectedItems, actualItems,
                "The items in the Destinations are not all present according to the property API.");
        }

        public void VerifyResortsListIsCorrect()
        {
            var actualItems = Driver.GetAllMultiselectOptions(_resortsMultiSelect);
            actualItems.Remove("Select");
            var expectedItems = ScenarioContext.Current.Get<List<string>>("AllResorts").OrderBy(x => x).ToList();
            CollectionAssert.AreEqual(expectedItems, actualItems,
                "The items in the resorts are not all present according to the property API.");
        }

        public void VerifyResortsListIsDisabled()
        {
            Assert.IsTrue(Driver.IsElementDisabled(_resortsMultiSelect), "The resorts list is not disabled.");
        }

        public void VerifyResortsListIsEnabled()
        {
            Assert.IsTrue(Driver.IsElementEnabled(_resortsMultiSelect), "The resorts list is not enabled.");
        }

        public void VerifyResortsSelectedOnTheRelationshipDialog(Table table)
        {
            var resorts = table.Rows[0]["Resorts"].ConvertStringIntoList();
            var actualResorts = Driver.GetAllSelectedMultiselectOptions(_resortsMultiSelect);
            CollectionAssert.AreEqual(resorts, actualResorts, "The resorts selected are not as expected.");
        }

        public void VerifyPropertiesSelectedOnTheRelationshipDialog(Table table)
        {
            var properties = table.Rows[0]["Properties"].ConvertStringIntoList();
            var actualProperties = Driver.GetAllSelectedMultiselectOptions(_propertiesMultiSelect);
            CollectionAssert.AreEqual(properties, actualProperties, "The resorts selected are not as expected.");
        }

        public void VerifyPropertiesListIsCorrect()
        {
            var actualItems = Driver.GetAllMultiselectOptions(_propertiesMultiSelect);
            actualItems.Remove("Select");
            var expectedItems = ScenarioContext.Current.Get<List<string>>("AllPropertiesByDestinationAndResort").OrderBy(x => x).ToList();
            CollectionAssert.AreEqual(expectedItems, actualItems,
                "The items in the properties list are not all present according to the property API.");
        }

        public void VerifyItemsSelectedOnTheRelationshipDialog(Table table)
        {
            var row = table.Rows[0];
            if (row.ContainsKey("Destinations"))
            {
                VerifyDestinationsSelectedOnTheRelationshipDialog(table);
            }

            if (row.ContainsKey("Resorts"))
            {
                VerifyResortsSelectedOnTheRelationshipDialog(table);
            }

            if (row.ContainsKey("Properties"))
            {
                VerifyPropertiesSelectedOnTheRelationshipDialog(table);
            }
        }

        public void VerifyTypeFieldIsNotDisplayed()
        {
            Assert.IsFalse(Driver.WaitForItem(_typeDdl, 1), "The type field is being displayed.");
        }

        public void VerifyPropertiesListIsDisabled()
        {
            Assert.IsTrue(Driver.IsElementDisabled(_propertiesMultiSelect), "The properties list is not disabled.");
        }

        public void VerifyPropertiesListIsEnabled()
        {
            Assert.IsTrue(Driver.IsElementEnabled(_propertiesMultiSelect), "The properties list is not enabled.");
        }

        public void VerifyAddRelationshipButtonIsDisabled()
        {
            Assert.IsFalse(Driver.IsElementEnabled(_addButton), "The add open relationship dialog button is enabled.");
        }

        public void VerifyAddRelationshipButtonIsEnabled()
        {
            Assert.IsTrue(Driver.IsElementEnabled(_addButton), "The add open relationship dialog button is disabled.");
        }

        public void VerifyRelationships(Table table)
        {
            var expectedRelations = table.Rows[0]["Relationships"].ConvertStringIntoList();
            var relations = Driver.GetTexts(_relationshipsPanel);

            Assert.AreEqual(expectedRelations, relations, "The realtionships displayed are not as expected.");
        }
    }
}