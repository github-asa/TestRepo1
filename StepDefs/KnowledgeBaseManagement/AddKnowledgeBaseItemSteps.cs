using System;
using J2BIOverseasOps.Pages.KnowledgeBaseManagement;
using J2BIOverseasOps.Pages.RoleManagement;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.KnowledgeBaseManagement
{
    [Binding]
    public class AddKnowledgeBaseItemSteps : BaseStepDefs
    {
        private readonly KbmAddNewItemPage _addNewItem;
        private readonly CommonStepDefs _commonStepDefs;
        private readonly CreateUpdateRolesPage _createUpdateRoles;

        public AddKnowledgeBaseItemSteps(IWebDriver driver, ILog log) : base(driver, log)
        {
            _addNewItem = new KbmAddNewItemPage(driver, log);
            _commonStepDefs = new CommonStepDefs(driver, log, new RunData());
            _createUpdateRoles = new CreateUpdateRolesPage(driver, log);
        }

        [Then(@"I verify that the page headers are '(.*)', '(.*)' on the Add New Knowledge Base Item page")]
        public void GivenIVerifyThatThePageHeadersAreOnTheAddNewKnowledgeBaseItemPage(string heading, string subHeading)
        {
            _addNewItem.VerifyPageHeadings(heading, subHeading);
        }

        [Then(@"I verify that the knowledge base field titles are displayed '(.*)', '(.*)', '(.*)', '(.*)', '(.*)', '(.*)', '(.*)', '(.*)', '(.*)' on the Add New Knowledge Base Item page")]
        public void ThenIVerifyThatTheContactFieldTitlesAreDisplayedOnTheAddNewKnowledgeBaseItemPage(string name, string phone, string email, string website, string note, string location, string category, string openingHours, string type)
        {
            _addNewItem.VerifyContactDetailTitles(name, phone, email, website, note, location, category, openingHours, type);
        }

        [Then(@"I verify that the knowledge base address field titles are displayed '(.*)', '(.*)', '(.*)', '(.*)' on the Add New Knowledge Base Item page")]
        public void ThenIVerifyThatTheAddressFieldTitlesAreDisplayedOnTheAddNewKnowledgeBaseItemPage(string address1, string address2, string cityTown, string postCode)
        {
            _addNewItem.VerifyAddressDetailTitles(address1, address2, cityTown, postCode);
        }

        [Then(@"I verify that the knowledge base relationship titles are displayed '(.*)', '(.*)', '(.*)', '(.*)'")]
        public void ThenIVerifyThatTheKnowledgeBaseRelationshipTitlesAreDisplayed(string relationships, string destinations, string resorts, string property)
        {
            _addNewItem.VerifyRelationshipTitles(relationships, destinations, resorts, property);
        }

        [When(@"I enter the knowledge base details on the Add New Knowledge Base Item page:")]
        public void WhenIEnterTheContactFieldTitlesOnTheAddNewKnowledgeBaseItemPage(Table table)
        {
            var i = 0;
            var row = table.Rows[i];
            _addNewItem.EnterKbDetails(row, i);
        }

        [When(@"I enter the knowledge base address field titles on the Add New Knowledge Base Item page:")]
        public void WhenIEnterTheKnowledgeBaseAddressFieldTitlesOnTheAddNewKnowledgeBaseItemPage(Table table)
        {
            var row = table.Rows[0];
            _addNewItem.EnterKbAddressDetails(row);
        }

        [When(@"I select the following relationships on the Add New Knowledge Base Item relationship dialog:")]
        public void WhenISelectTheFollowingRelationshipsOnTheAddNewKnowledgeBaseItemPage(Table table)
        {
            var row = table.Rows[0];
            _addNewItem.SelectRelationship(row);
        }

        [When(@"I deselect the following relationships on the Add New Knowledge Base Item relationship dialog:")]
        public void ThenIDeselectTheFollowingRelationshipsOnTheAddNewKnowledgeBaseItemrelationshipDialog(Table table)
        {
            var row = table.Rows[0];
            _addNewItem.DeselectRelationship(row);
        }

        [When(@"I tick the assign to all destinations tick box on the Add New Knowledge Base Item page")]
        public void WhenITickTheAssignToAllDestinationsTickBoxOnTheAddNewKnowledgeBaseItemPage()
        {
            _addNewItem.TickAssignToAllDestinations();
        }

        [When(@"I untick the assign to all destinations tick box on the Add New Knowledge Base Item page")]
        public void WhenIUntickTheAssignToAllDestinationsTickBoxOnTheAddNewKnowledgeBaseItemPage()
        {
            _addNewItem.UntickAssignToAllDestinations();
        }

        [When(@"I add a note of '(.*)' on the Add New Knowledge Base Item page")]
        public void WhenIAddANoteOfOnTheAddNewKnowledgeBaseItemPage(string note)
        {
            _addNewItem.EnterKbNote(note);
        }


        [When(@"I click on the save button on the Add New Knowledge Base Item page")]
        public void WhenIClickOnTheSaveButtonOnTheAddNewKnowledgeBaseItemPage()
        {
            _addNewItem.ClickSave();
        }

        [Then(@"I verify the validation error is displayed stating '(.*)'")]
        public void ThenIVerifyTheValidationErrorIsDisplayedStating(string message)
        {
            _addNewItem.VerifyValidationMessageIsDisplayed(message);
        }

        [Then(@"I verify that the category list contains the following items in alphabetical order:")]
        public void ThenIVerifyThatTheCategoryListContainsTheFollowingItemsInAlphabeticalOrder(Table table)
        {
            _addNewItem.VerifyCategoryListAndOrder(table);
        }

        [Then(@"I verify that the Destinations list is in alphabetical order")]
        public void ThenIVerifyThatTheDestinationsListIsInAlphabeticalOrder()
        {
            _addNewItem.VerifyDesintationsIsInAlphabeticalOrder();
        }

        [When(@"I click the add relationship button on the Add New Knowledge Base Item page")]
        public void WhenIClickTheAddRelationshipButtonOnTheAddNewKnowledgeBaseItemPage()
        {
            _addNewItem.ClickAdd();
        }

        [When(@"I click the Add button on the Add New Knowledge Base Item relationship dialog")]
        public void WhenIClickTheAddButtonOnTheAddNewKnowledgeBaseItemPage()
        {
            _addNewItem.ClickAddOnTheRelationshipDialog();
        }

        [Then(@"I verify that the relationship dialog is dismissed on the Add New Knowledge Base Item page")]
        public void ThenTheRelationshipDialogIsIsDismissedOnTheAddNewKnowledgeBaseItemPage()
        {
            _addNewItem.VerifyRelationshipDialogIsDismissed();
        }

        [When(@"I click cancel on the Add New Knowledge Base Item relationship dialog")]
        public void WhenIClickCancelOnTheAddNewKnowledgeBaseItemrelationshipDialog()
        {
            _addNewItem.ClickCancelOnTheRelationshipDialog();
        }

        [Then(@"I verify that the items selected on the Add New Knowledge Base Item relationship dialog are:")]
        public void ThenIVerifyThatTheItemsSelectedOnTheAddNewKnowledgeBaseItemrelationshipDialogAre(Table table)
        {
            _addNewItem.VerifyItemsSelectedOnTheRelationshipDialog(table);
        }

        [Then(@"I verify that the destination list content is complete on the Add New Knowledge Base Item relationship dialog")]
        public void ThenIVerifyThatTheDestinationListIsCorrectOnTheAddNewKnowledgeBaseItemrelationshipDialog()
        {
            _addNewItem.VerifyDestinationsListIsCorrect();
        }

        [Then(@"I verify that the resorts list is filtered by the destination selected on the Add New Knowledge Base Item relationship dialog")]
        public void ThenIVerifyThatTheResortsListIsFilteredByTheDestinationSelectedOnTheAddNewKnowledgeBaseItemrelationshipDialog()
        {
            _addNewItem.VerifyResortsListIsCorrect();
        }

        [Then(@"I verify that the properties list is filtered by the resort selected on the Add New Knowledge Base Item relationship dialog")]
        public void ThenIVerifyThatThePropertiesListIsFilteredByTheResortSelectedOnTheAddNewKnowledgeBaseItemRelationshipDialog()
        {
            _addNewItem.VerifyPropertiesListIsCorrect();
        }


        [Then(@"the resorts option is disabled on the Add New Knowledge Base Item relationship dialog")]
        public void ThenTheResortsOptionIsDisabledOnTheAddNewKnowledgeBaseItemrelationshipDialog()
        {
            _addNewItem.VerifyResortsListIsDisabled();
        }

        [Then(@"the resorts option is enabled on the Add New Knowledge Base Item relationship dialog")]
        public void ThenTheResortsOptionIsEnabledOnTheAddNewKnowledgeBaseItemRelationshipDialog()
        {
            _addNewItem.VerifyResortsListIsEnabled();
        }

        [When(@"I verify the type field is not displayed")]
        public void WhenIVerifyTheTypeFieldIsNotDisplayed()
        {
            _addNewItem.VerifyTypeFieldIsNotDisplayed();
        }

        [Then(@"the property option is disabled on the Add New Knowledge Base Item relationship dialog")]
        public void ThenThePropertyOptionIsDisabledOnTheAddNewKnowledgeBaseItemRelationshipDialog()
        {
            _addNewItem.VerifyPropertiesListIsDisabled();
        }

        [Then(@"the property option is enabled on the Add New Knowledge Base Item relationship dialog")]
        public void ThenThePropertyOptionIsEnabledOnTheAddNewKnowledgeBaseItemRelationshipDialog()
        {
            _addNewItem.VerifyPropertiesListIsEnabled();
        }

        [Then(@"I verify that the Add relationship button is disabled")]
        public void ThenIVerifyThatTheAddRelationsipButtonIsDisabled()
        {
            _addNewItem.VerifyAddRelationshipButtonIsDisabled();
        }

        [Then(@"I verify that the Add relationship button is enabled")]
        public void ThenIVerifyThatTheAddRelationshipButtonIsEnabled()
        {
            _addNewItem.VerifyAddRelationshipButtonIsEnabled();
        }

        [Then(@"I verify that the following relationships are displayed on the Add New Knowledge Base Item page:")]
        public void ThenIVerifyThatTheFollowingRelationshipsAreDisplayedOnTheAddNewKnowledgeBaseItemPage(Table table)
        {
            _addNewItem.VerifyRelationships(table);
        }

        [When(@"I add the following Knowledge base items:")]
        public void WhenIAddTheFollowingKnowledgeBaseItems(Table table)
        {
            var rows = table.Rows;
            var i = 0;
            foreach (var row in rows)
            {
                _commonStepDefs.GivenINavigateToPage("knowledge-base/create");
                _commonStepDefs.ThenIShouldBeNavigatedToPage("knowledge-base/create");
                _addNewItem.EnterKbDetails(row, i);
                _addNewItem.EnterKbAddressDetails(row);
                _addNewItem.EnterKbNote(row["Note"]);
                if (row.ContainsKey("Global") && !string.IsNullOrWhiteSpace(row["Global"]))
                {
                    if (row["Global"].Equals("true", StringComparison.InvariantCultureIgnoreCase))
                    {
                        _addNewItem.TickAssignToAllDestinations();
                    }
                    else if (row["Global"].Equals("false", StringComparison.InvariantCultureIgnoreCase))
                    {
                        _addNewItem.UntickAssignToAllDestinations();
                    }
                }
                else
                {
                    _addNewItem.ClickAdd();
                    _addNewItem.SelectRelationship(row);
                    _addNewItem.ClickAddOnTheRelationshipDialog();
                    _addNewItem.VerifyRelationshipDialogIsDismissed();
                }

                _addNewItem.ClickSave();
                _commonStepDefs.ThenIShouldBeNavigatedToPage("knowledge-base");
                _createUpdateRoles.VerifyTextOnGrowlNotification("Success");                

                i++;
            }
        }
    }
}