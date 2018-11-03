using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using J2BIOverseasOps.Extensions;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using static System.StringComparison;

namespace J2BIOverseasOps.Pages.KnowledgeBaseManagement
{
    public class KbmEditItemPage : BasePage
    {

        private readonly By _address1Textbox = By.Id("knowledge-base-address-line-1");
        private readonly By _address1Title = By.Id("knowledge-base-address-line-1-lbl");
        private readonly By _address2Textbox = By.Id("knowledge-base-address-line-2");
        private readonly By _address2Title = By.Id("knowledge-base-address-line-2-lbl");
        private readonly By _cityTownTextbox = By.Id("knowledge-base-city-town");
        private readonly By _cityTownTitle = By.Id("knowledge-base-city-town-lbl");
        private readonly By _emailTextbox = By.Id("knowledge-base-email");
        private readonly By _emailTitle = By.Id("knowledge-base-email-lbl");
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
        private readonly By _relationshipsDialogTitle = By.Id("ui-dialog-1-label");
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
        private readonly By _relationshipsPanel = By.CssSelector("p-scrollpanel p");
        private readonly By _deleteButton = By.Id("view-kb-delete-btn");
        private readonly By _backButton = By.Id("view-kb-back-btn");


        public KbmEditItemPage(IWebDriver driver, ILog log) : base(driver, log)
        {

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

        public void VerifyContactDetails(Table table)
        {
            var row = table.Rows[0];
            var name = row["Name"].Equals("Random", OrdinalIgnoreCase) ? ScenarioContext.Current.Get<string>("Name"): row["Name"];
            Assert.AreEqual(name, Driver.GetInputBoxValue(_nameTextbox), "The name is not as expected");
            Assert.AreEqual(row["Phone"], Driver.GetInputBoxValue(_phoneTextbox), "The phone is not as expected");
            Assert.AreEqual(row["Email"], Driver.GetInputBoxValue(_emailTextbox), "The email is not as expected");
            Assert.AreEqual(row["Website"], Driver.GetInputBoxValue(_websiteTextbox), "The website is not as expected");;
            Assert.AreEqual(row["Location"], Driver.GetInputBoxValue(_locationTextbox), "The location is not as expected");
            Assert.AreEqual(row["Category"], Driver.GetSelectedDropDownOption(_categoryDdl), "The category is not as expected");
            Assert.AreEqual(row["OpeningHours"], Driver.GetInputBoxValue(_openingHoursTextbox), "The opening hours is not as expected");

            if (row.ContainsKey("Type"))
            {
                Assert.AreEqual(row["Type"], Driver.GetSelectedDropDownOption(_typeDdl), "The type is not as expected");
            }
            else
            {
                Assert.IsFalse(Driver.WaitForItem(_typeDdl, 1), "The type is being displayed.");
            }
        }

        public void VerifyAddressDetails(Table table)
        {
            var row = table.Rows[0];
            Assert.AreEqual(row["Address1"], Driver.GetInputBoxValue(_address1Textbox), "The address1 is not as expected");
            Assert.AreEqual(row["Address2"], Driver.GetInputBoxValue(_address2Textbox), "The address2 is not as expected");
            Assert.AreEqual(row["CityTown"], Driver.GetInputBoxValue(_cityTownTextbox), "The city/Town is not as expected");
            Assert.AreEqual(row["PostCode"], Driver.GetInputBoxValue(_postCodeTextbox), "The post code is not as expected");
        }

        public void VerifyRelationshipDetails(Table table)
        {
            var row = table.Rows[0];
            var destinations = row["Destinations"].ConvertStringIntoList();
            var resorts = row["Resorts"].ConvertStringIntoList();
            var properties = row["Properties"].ConvertStringIntoList();

            Driver.ClickItem(_addButton);
            Assert.IsTrue(Driver.WaitForItem(_relationshipsDialog), "The relationship popup is not being displayed.");
            Assert.AreEqual(destinations, Driver.GetAllSelectedMultiselectOptions(_destinationsMultiSelect), "The destinations is not as expected");
            Assert.AreEqual(resorts, Driver.GetAllSelectedMultiselectOptions(_resortsMultiSelect), "The resorts is not as expected");
            Assert.AreEqual(properties, Driver.GetAllSelectedMultiselectOptions(_propertiesMultiSelect), "The properties is not as expected");
        }

        public void VerifyNote(string note)
        {
            Assert.AreEqual(note, Driver.GetInputBoxValue(_noteTextArea), "The note text is not as expected.");
        }

        public void UpdateContactDetails(Table table)
        {
            var i = 0;
            foreach (var row in table.Rows)
            {
                if (row.ContainsKey("Name"))
                {
                    if (string.IsNullOrWhiteSpace(row["Name"]))
                    {
                        Driver.ClearUsingBackspace(_nameTextbox);
                    }
                    else
                    {
                        var name = row["Name"].Equals("Random") ? Guid.NewGuid().ToString() : row["Name"];
                        ScenarioContext.Current["Name"] = name;
                        ScenarioContext.Current[$"KBIName{i}"] = name;
                        Driver.EnterText(_nameTextbox, name);
                    }
                }

                if (row.ContainsKey("Phone"))
                {
                    if (string.IsNullOrWhiteSpace(row["Phone"]))
                    {
                        Driver.ClearUsingBackspace(_phoneTextbox);
                    }
                    else
                    {
                        ScenarioContext.Current["Phone"] = row["Phone"];
                        Driver.EnterText(_phoneTextbox, row["Phone"]);
                    }
                }

                if (row.ContainsKey("Email"))
                {
                    if (string.IsNullOrWhiteSpace(row["Email"]))
                    {
                        Driver.ClearUsingBackspace(_emailTextbox);
                    }
                    else
                    {
                        ScenarioContext.Current["Email"] = row["Email"];
                        Driver.EnterText(_emailTextbox, row["Email"]);
                    }
                }

                if (row.ContainsKey("Website"))
                {
                    if (string.IsNullOrWhiteSpace(row["Website"]))
                    {
                        Driver.ClearUsingBackspace(_websiteTextbox);
                    }
                    else
                    {
                        ScenarioContext.Current["Website"] = row["Website"];
                        Driver.EnterText(_websiteTextbox, row["Website"]);
                    }
                }

                if (row.ContainsKey("Location"))
                {
                    if (string.IsNullOrWhiteSpace(row["Location"]))
                    {
                        Driver.ClearUsingBackspace(_locationTextbox);
                    }
                    else
                    {
                        ScenarioContext.Current["Location"] = row["Location"];
                        Driver.EnterText(_locationTextbox, row["Location"]);
                    }
                }

                if (row.ContainsKey("Category"))
                {
                    ScenarioContext.Current["Category"] = row["Category"];
                    Driver.SelectDropDownOption(_categoryDdl, row["Category"]);
                }

                if (row.ContainsKey("OpeningHours"))
                {
                    if (string.IsNullOrWhiteSpace(row["OpeningHours"]))
                    {
                        Driver.ClearUsingBackspace(_openingHoursTextbox);
                    }
                    else
                    {
                        ScenarioContext.Current["OpeningHours"] = row["OpeningHours"];
                        Driver.EnterText(_openingHoursTextbox, row["OpeningHours"]);
                    }
                }

                if (row.ContainsKey("Type"))
                {
                    ScenarioContext.Current["Type"] = row["Type"];
                    Assert.IsTrue(Driver.WaitForItem(_typeDdl), "The Type dropdown is not displayed.");
                    Driver.SelectDropDownOption(_typeDdl, row["Type"]);
                }

                i++;
            }
        }

        public void UpdateKbAddressDetails(Table table)
        {
            var row = table.Rows[0];
            if (string.IsNullOrWhiteSpace(row["Address1"]))
            {
                Driver.ClearUsingBackspace(_address1Textbox);
            }
            else
            {
                Driver.EnterText(_address1Textbox, row["Address1"]);
            }

            if (string.IsNullOrWhiteSpace(row["Address2"]))
            {
                Driver.ClearUsingBackspace(_address2Textbox);
            }
            else
            {
                Driver.EnterText(_address2Textbox, row["Address2"]);
            }

            if (string.IsNullOrWhiteSpace(row["CityTown"]))
            {
                Driver.ClearUsingBackspace(_cityTownTextbox);
            }
            else
            {
                Driver.EnterText(_cityTownTextbox, row["CityTown"]);
            }

            if (string.IsNullOrWhiteSpace(row["PostCode"]))
            {
                Driver.ClearUsingBackspace(_postCodeTextbox);
            }
            else
            {
                Driver.EnterText(_postCodeTextbox, row["PostCode"]);
            }

            ScenarioContext.Current["Address1"] = row["Address1"];
            ScenarioContext.Current["Address2"] = row["Address2"];
            ScenarioContext.Current["CityTown"] = row["CityTown"];
            ScenarioContext.Current["PostCode"] = row["PostCode"];
        }

        public void ClickAdd()
        {
            Driver.ClickItem(_addButton);
            Assert.IsTrue(Driver.WaitForItem(_relationshipsDialog), "The relationship dialog is not being displayed");
        }

        public void ClickSave()
        {
            Driver.ClickItem(_saveBtn);
        }

        public void ClickAddOnTheRelationshipDialog()
        {
            Driver.ClickItem(_addRelationshipButton);
        }

        public void UpdateKbNote(string note)
        {
            if (string.IsNullOrWhiteSpace(note))
            {
                Driver.ClearUsingBackspace(_noteTextArea);
            }
            else
            {
                ScenarioContext.Current["Note"] = note;
                Driver.EnterText(_noteTextArea, note);
            }
        }

        public void UpdatetRelationship(Table table)
        {
            var row = table.Rows[0];

            if (row.ContainsKey("Destinations"))
            {
                var destinations = row["Destinations"].ConvertStringIntoList();
                ScenarioContext.Current["Destinations"] = destinations;
                Driver.SelectAllMultiselectOptions(_destinationsMultiSelect);
                Driver.DeselectAllMultiselectOptions(_destinationsMultiSelect);
                Driver.SelectMultiselectOption(_destinationsMultiSelect, destinations);
            }

            if (row.ContainsKey("Resorts"))
            {
                var resorts = row["Resorts"].ConvertStringIntoList();
                ScenarioContext.Current["Resorts"] = resorts;
                Driver.SelectAllMultiselectOptions(_resortsMultiSelect);
                Driver.DeselectAllMultiselectOptions(_resortsMultiSelect);
                Driver.SelectMultiselectOption(_resortsMultiSelect, resorts);
            }

            if (row.ContainsKey("Properties"))
            {
                var properties = row["Properties"].ConvertStringIntoList();
                ScenarioContext.Current["Properties"] = properties;
                Driver.SelectAllMultiselectOptions(_propertiesMultiSelect);
                Driver.DeselectAllMultiselectOptions(_propertiesMultiSelect);
                Driver.SelectMultiselectOption(_propertiesMultiSelect, properties);
            }
        }
        public void VerifyRelationships(Table table)
        {
            var expectedRelations = table.Rows[0]["Relationships"].ConvertStringIntoList();
            var relations = Driver.GetTexts(_relationshipsPanel);

            Assert.AreEqual(expectedRelations, relations, "The realtionships displayed are not as expected.");
        }

        public void VerifyPageHeadings(string heading, string subHeading)
        {
            Assert.AreEqual(heading, Driver.GetText(_header), "The page header is not as expected.");
            Assert.AreEqual(subHeading, Driver.GetText(_subHeading), "The subheader is not as expected.");
        }

        public void TickAssignToAllDestinations()
        {
            ScenarioContext.Current["Global"] = true;
            Driver.TickCheckBox(_allDestinationsCheckbox);
        }

        public void ClickDelete()
        {
            Driver.ClickItem(_deleteButton);
        }

        public void ClickBack()
        {
            Driver.ClickItem(_backButton);
        }
    }
}
