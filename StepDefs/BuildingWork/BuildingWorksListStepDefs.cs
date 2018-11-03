using J2BIOverseasOps.Pages.BuildingWork;
using log4net;
using NUnit.Framework.Constraints;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.BuildingWork
{
    [Binding]
    public sealed class BuildingWorksListStepDefs : BaseStepDefs
    {
        private readonly BuildingWorksListPage _bwListRecord;

        public BuildingWorksListStepDefs(IWebDriver driver, ILog log, IRunData rundata) : base(driver, log)
        {
            _bwListRecord = new BuildingWorksListPage(driver, log, rundata);
        }


        [When(@"I click the create a new bw record button")]
        public void WhenIClickTheCreateANewBwRecordButton()
        {
            _bwListRecord.ClickCreateNewBwRecord();
        }

        [When(@"I click the ""(Edit|View|Completion Form)"" link for the building work record")]
        [When(@"I click the ""(Edit|View|Completion Form)"" link building work record for the newly created building work record")]
        public void WhenIClickTheBuildingWorkRecordWithBwNumberOnBuildingWorkListPage(string editView)
        {
            _bwListRecord.ClickEditOrViewLink(editView);
        }

        [When(@"I search for Building work record with id ""(.*)""")]
        public void WhenISearchForBuildingWorkRecordWithId(string bwId)
        {
            _bwListRecord.SearchForRecord(bwId);
        }

        [Then(@"I verify only one record is displayed")]
        public void ThenIVerifyOnlyOneRecordIsDisplayedWithCorrectBwid()
        {
            _bwListRecord.WaitUntilOnlyRecordFound();
        }

        [When(@"I search for Building work record with ""(.*)"" from Bwid")]
        public void WhenISearchForBuildingWorkRecordWithFromBwid(string searchCriteria)
        {
            _bwListRecord.SearchWithPartialBwId(searchCriteria);
        }

        [Then(@"I verify all the record contains the ""(.*)"" from Bwid")]
        public void ThenIVerifyAllTheRecordContainsTheFromBwid(string searchCriteria)
        {
            _bwListRecord.VerifyBwRecordIdContains(searchCriteria);
        }

        [When(@"I search for Building work record with ""(Destination|Property)"" as ""(.*)""")]
        public void WhenISearchForBuildingWorkRecordWith(string destinationProp, string searchTerm)
        {
            _bwListRecord.EnterCharacters(searchTerm);
        }

        [Then(@"I verify all the records have ""(Destination|Property)"" as ""(.*)""")]
        public void ThenIVerifyAllTheRecordsHaveAs(string propDest, string expectedTerm)
        {
            _bwListRecord.VerifySearchResultContains(propDest, expectedTerm);
        }

        [Given(@"I note down the BwId for the record with the record creation date as ""(.*)""")]
        public void GivenISelectTheRecordWithRecordCreationDateAs(string date)
        {
            _bwListRecord.NoteDownIdForBWwithDate(date);
        }

        [When(@"I update the maximum number of results to ""(.*)"" to display on the building works list")]
        public void WhenIUpdateTheNumberOfResultsTo(string numberOfResults)
        {
            _bwListRecord.UpdateBuildingWorksResultsNumber(numberOfResults);
        }

        [Then(@"I count the number of results is ""(.*)"" on the bw list form")]
        public void ThenICountTheNumberOfResultsIs(int expectedNumberOfResults)
        {
            _bwListRecord.CountBuildingWorksResults(expectedNumberOfResults);
        }

        [When(@"I sort the Building Works List by ""(.*)"" and the order is ""(.*)""")]
        public void WhenISortTheBuildingWorksListByAndTheOrderIs(string columnName, string order)
        {
            _bwListRecord.SortBuildingWorksListBy(columnName, order);
        }

        [Then(@"I check the Building Works List is sorted by ""(.*)"" and the order is ""(.*)""")]
        public void ThenICheckTheBuildingWorksListIsSortedByAndTheOrderIs(string columnName, string order)
        {
            _bwListRecord.CheckBuildingWorksListIsSortedBy(columnName, order);
        }

        [Then(@"I verify the following values for the bw form id ""(.*)"" on the building works list page:")]
        public void ThenIVerifyTheFollowingValuesForTheBwFormIdOnTheBuildingWorksListPage(string bwId, Table table)
        {
            _bwListRecord.VerifyBuildingWorkListFormData(bwId, table);
        }

    }

}