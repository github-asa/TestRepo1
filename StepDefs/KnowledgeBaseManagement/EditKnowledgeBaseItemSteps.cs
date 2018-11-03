using J2BIOverseasOps.Pages.KnowledgeBaseManagement;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.KnowledgeBaseManagement
{
    [Binding]
    public class EditKnowledgeBaseItemSteps : BaseStepDefs
    {
        private readonly KbmEditItemPage _kbmEditItemPage;
        public EditKnowledgeBaseItemSteps(IWebDriver driver, ILog log) : base(driver, log)
        {
            _kbmEditItemPage = new KbmEditItemPage(driver, log);
        }

        [Then(@"I verify that the Edit Knowledge Base Item page contains the following:")]
        public void ThenIVerifyThatTheEditKnowledgeBaseItemPageContainsTheFollowing(Table table)
        {
            _kbmEditItemPage.VerifyContactDetails(table);
        }

        [Then(@"I verify that the knowledge base address fields on the Edit Knowledge Base Item page contains:")]
        public void ThenIVerifyThatTheKnowledgeBaseAddressFieldsOnTheEditNewKnowledgeBaseItemPageContains(Table table)
        {
            _kbmEditItemPage.VerifyAddressDetails(table);
        }

        [Then(@"I verify that the following relationships on the Edit Knowledge Base Item relationship dialog:")]
        public void ThenIVerifyThatTheFollowingRelationshipsOnTheEditNewKnowledgeBaseItemRelationshipDialog(Table table)
        {
            _kbmEditItemPage.VerifyRelationshipDetails(table);
        }

        [Then(@"I verify the Note is '(.*)' on the Edit Knowledge Base Item page")]
        public void ThenIVerifyTheTheNotePopupDisplaysOnTheEditNewKnowledgeBaseItemPage(string note)
        {
            _kbmEditItemPage.VerifyNote(note);
        }

        [When(@"I update the Edit Knowledge Base Item page with:")]
        public void WhenIUpdateTheEditKnowledgeBaseItemPageWith(Table table)
        {
            _kbmEditItemPage.UpdateContactDetails(table);
        }

        [When(@"I update the knowledge base address fields on the Edit Knowledge Base Item page with:")]
        public void WhenIUpdateTheKnowledgeBaseAddressFieldsOnTheEditKnowledgeBaseItemPageWith(Table table)
        {
            _kbmEditItemPage.UpdateKbAddressDetails(table);
        }

        [When(@"I click the add relationship button on the Edit Knowledge Base Item page")]
        public void WhenIClickTheAddRelationshipButtonOnTheEditKnowledgeBaseItemPage()
        {
            _kbmEditItemPage.ClickAdd();
        }

        [When(@"I update the following relationships on the Edit Knowledge Base Item relationship dialog:")]
        public void WhenIUpdateTheFollowingRelationshipsOnTheEditKnowledgeBaseItemRelationshipDialog(Table table)
        {
            _kbmEditItemPage.UpdatetRelationship(table);
        }

        [When(@"I click save on the Edit Knowledge Base Item page")]
        public void WhenIClickSaveOnTheEditKnowledgeBaseItemPage()
        {
            _kbmEditItemPage.ClickSave();
        }

        [When(@"I update the Note to '(.*)' on the Edit Knowledge Base Item page")]
        public void WhenIUpdateTheNoteToOnTheEditKnowledgeBaseItemPage(string note)
        {
            _kbmEditItemPage.UpdateKbNote(note);
        }

        [When(@"I click the Add button on the Edit Knowledge Base Item relationship dialog")]
        public void WhenIClickTheAddButtonOnTheEditKnowledgeBaseItemRelationshipDialog()
        {
            _kbmEditItemPage.ClickAddOnTheRelationshipDialog();
        }

        [Then(@"I verify that the following relationships are displayed on the Edit Knowledge Base Item page:")]
        public void ThenIVerifyThatTheFollowingRelationshipsAreDisplayedOnTheEditKnowledgeBaseItemPage(Table table)
        {
            _kbmEditItemPage.VerifyRelationships(table);
        }

        [Then(@"I verify that the page headers are '(.*)', '(.*)' on the Edit Knowledge Base Item page")]
        public void ThenIVerifyThatThePageHeadersAreOnTheEditKnowledgeBaseItemPage(string header, string subheader)
        {
            _kbmEditItemPage.VerifyPageHeadings(header, subheader);
        }

        [Then(@"I verify that the knowledge base field titles are displayed '(.*)', '(.*)', '(.*)', '(.*)', '(.*)', '(.*)', '(.*)', '(.*)', '(.*)' on the Edit Knowledge Base Item page")]
        public void ThenIVerifyThatTheKnowledgeBaseFieldTitlesAreDisplayedOnTheEditKnowledgeBaseItemPage(string name, string phone, string email, string website, string note, string location, string category, string openingHours, string type)
        {
            _kbmEditItemPage.VerifyContactDetailTitles(name, phone, email, website, note, location, category, openingHours, type);
        }

        [Then(@"I verify that the knowledge base address field titles are displayed '(.*)', '(.*)', '(.*)', '(.*)' on the edit Knowledge Base Item page")]
        public void ThenIVerifyThatTheKnowledgeBaseAddressFieldTitlesAreDisplayedOnTheEditKnowledgeBaseItemPage(string address1, string address2, string cityTown, string postCode)
        {
            _kbmEditItemPage.VerifyAddressDetailTitles(address1, address2, cityTown, postCode);
        }

        [Then(@"I verify that the knowledge base relationship titles are displayed '(.*)', '(.*)', '(.*)', '(.*)' on the Edit Knowledge Base Item page")]
        public void ThenIVerifyThatTheKnowledgeBaseRelationshipTitlesAreDisplayedOnTheEditKnowledgeBaseItemPage(string relationships, string destinations, string resorts, string property)
        {
            _kbmEditItemPage.VerifyRelationshipTitles(relationships, destinations, resorts, property);
        }

        [When(@"I tick the assign to all destinations tick box on the Edit Knowledge Base Item page")]
        public void WhenITickTheAssignToAllDestinationsTickBoxOnTheEditKnowledgeBaseItemPage()
        {
            _kbmEditItemPage.TickAssignToAllDestinations();
        }

        [When(@"I click the Delete button on the Edit Knowledge Base Item page")]
        public void WhenIClickTheDeleteButtonOnTheEditKnowledgeBaseItemPage()
        {
            _kbmEditItemPage.ClickDelete();
        }

        [When(@"I click the Back button on the Edit Knowledge Base Management Search page")]
        public void WhenIClickTheBackButtonOnTheEditKnowledgeBaseManagementSearchPage()
        {
            _kbmEditItemPage.ClickBack();
        }
    }
}