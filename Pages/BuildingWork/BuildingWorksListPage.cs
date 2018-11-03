using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using J2BIOverseasOps.Extensions;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Pages.BuildingWork
{
    internal class BuildingWorksListPage : BuildingWorkCommon
    {
        public BuildingWorksListPage(IWebDriver driver, ILog log, IRunData rundata) : base(driver, log, rundata)
        {
        }

        private static string _bwIdNumber = "";
        private static string _columnName = "";
        private static string _orderClass = "";
        private readonly By _createNewRecord = By.XPath("//button//span[contains(text(),'Create new building works record')]");
        private static By BwRecordRow => By.XPath($"//tr[contains(@class, '{_bwIdNumber}')]");
        private readonly By _paginatorDrpDown = By.XPath("//p-paginator//p-dropdown");
        private readonly By _bwRecordsRow = By.XPath("//tr[contains(@class, 'BW')]");

        private readonly By _headerBwNumber = By.XPath("//th[@id='buildingWorksReference']");
        private readonly By _bwReferenceCell=By.XPath(".//td[@dataname='buildingWorksReference']//div");

        private readonly By _headerDestination = By.XPath("//th[@id='destination']");
        private readonly By _destinationCell = By.XPath(".//td[@dataname='destination']//div");

        private readonly By _headerResort = By.XPath("//th[@id='resort']");
        private readonly By _resortCell = By.XPath(".//td[@dataname='resort']//div");

        private readonly By _headerProperty = By.XPath("//th[@id='propertyName']");
        private readonly By _propertiesCell = By.XPath(".//td[@dataname='propertyName']//div");

        private readonly By _headerCreatedDate = By.XPath("//th[@id='createdOn']");
        private readonly By _createdDateCell = By.XPath(".//td[@dataname='createdOn']//div");

        private readonly By _headerCreatedBy = By.XPath("//th[@id='createdBy']");
        private readonly By _createdByCell = By.XPath(".//td[@dataname='createdBy']//div");

        private readonly By _headerRecordType = By.XPath("//th[@id='typeName']");
        private readonly By _recordTypesCell = By.XPath(".//td[@dataname='typeName']//div");

        private readonly By _viewLink = By.XPath(".//td[contains(@class, 'View')]");
        private readonly By _editLink = By.XPath(".//td[contains(@class, 'Edit')]");
        private readonly By _completionFormLink = By.XPath(".//td[contains(@class,'Completion')]");

        private readonly By _bwSearchBar = By.XPath("//*[contains(@class, 'searchBar')]//input");
        private  By columnSortDetection => By.XPath($"//p-sorticon//i[contains(@class,'{_orderClass}')]");

        public void ClickCreateNewBwRecord()
        {
            Driver.ClickItem(_createNewRecord);
        }

        // Keeps pressing enter until the BW record is found
        private void WaitUntilBwRecordFound(int numbOfTries=5)
        {
            var i = 0;
            do
            {
                WaitForSpinnerToDisappear();
                try
                {
                    Driver.FindElement(_bwSearchBar).SendKeys(Keys.Enter);
                }
                catch (StaleElementReferenceException)
                { }
                i++;
                if (i==numbOfTries)
                {
                    Assert.Fail($"Could not find the BW Record after {numbOfTries} tries");
                }
            } while (!Driver.WaitForItem(BwRecordRow, 2));
        }

        // Keeps pressing enter until the BW record is found
        public void WaitUntilOnlyRecordFound(int numbOfTries = 5)
        {
            var i = 0;
            do
            {
                WaitForSpinnerToDisappear();
                try
                {
                    Driver.FindElement(_bwSearchBar).SendKeys(Keys.Enter);
                }
                catch (StaleElementReferenceException)
                { }
                i++;
                if (i == numbOfTries)
                {
                    Assert.Fail($"Could not find the only record on the BW List after {numbOfTries} tries");
                }
            } while (Driver.FindElements(_bwRecordsRow).Count!= 1);
        }


        public void ClickEditOrViewLink(string editView)
        {
            _bwIdNumber = ScenarioContext.Current[BwIdScenarioContextKey].ToString();
            By elementToClick=null;
            switch (editView.ToLower())
            {
                case "edit":
                    elementToClick = _editLink;
                    break;
                case "view":
                    elementToClick = _viewLink;
                    break;
                case "completion form":
                    elementToClick = _completionFormLink;
                    break;
                default:
                    Assert.Fail($"{editView} is not a valid link to click");
                    break;
            }
            CloseGrowlNotification();
            Driver.WaitForItem(elementToClick);
            Assert.True(Driver.WaitForItemWithinWebElement(BwRecordRow, elementToClick),$"Unable to find {elementToClick} link on bw record list");
            var element = Driver.FindElement(BwRecordRow).FindElement(elementToClick);
            Driver.ClickItem(element);
        }

        public void SearchForRecord(string bwId)
        {
            _bwIdNumber = bwId=="bw_id" ? ScenarioContext.Current[BwIdScenarioContextKey].ToString() : bwId;
            Driver.EnterCharacters(_bwSearchBar, _bwIdNumber);
            WaitForSpinnerToDisappear();
            WaitForSpinnerToDisappear();
            WaitForSpinnerToDisappear();
            WaitUntilBwRecordFound();
        }

        public void EnterCharacters(string searchTerm)
        {
            Driver.EnterCharacters(_bwSearchBar, searchTerm);
            WaitForSpinnerToDisappear();
            WaitForSpinnerToDisappear();
            WaitForSpinnerToDisappear();
            WaitForSpinnerToDisappear();
            WaitForSpinnerToDisappear();
            Driver.WaitForItem(_bwSearchBar);
            Driver.FindElement(_bwSearchBar).SendKeys(Keys.Enter);
            WaitForSpinnerToDisappear();
            WaitForSpinnerToDisappear();
            WaitForSpinnerToDisappear();
            WaitForSpinnerToDisappear();
            WaitForSpinnerToDisappear();
            WaitForSpinnerToDisappear();
        }


        public void VerifyOnlyOneRecordDisplayed()
        {
            WaitUntilBwRecordFound();
        }


        public void NoteDownIdForBWwithDate(string date)
        {
            var found = false;
            var allBwRecords = GetAllBwRecords();
            switch (date.ToLower())
            {
                case "past":
                    var todaysDate =   Driver.ParseDateTo_ddmmyyyy(DateTime.Now.ToString("dd/MM/yyyy"));
                    for (var i=1;i< allBwRecords.Count;i++)
                    {
                        var currentDate = allBwRecords[i].FindElement(_createdDateCell).Text.Trim();
                        var recordCreationDate = Driver.ParseDateTo_ddmmyyyy(currentDate);
                        if (recordCreationDate< todaysDate)
                        {
                            found = true;
                            ScenarioContext.Current[BwIdScenarioContextKey] = allBwRecords[i].FindElement(_bwReferenceCell).Text.Trim(); // get the first item
                            break;
                        }
                    }
                    break;
                case "default":
                    found = true;
                    ScenarioContext.Current[BwIdScenarioContextKey] = allBwRecords[0].FindElement(_bwReferenceCell).Text.Trim();
                    break;
            }

            if (!found)
            {
                Assert.Ignore($"Unable to find a record with given date {date}, failing the Test");
            }
        }

        public void UpdateBuildingWorksResultsNumber(string numberOfResults)
        {
            Driver.SelectDropDownOption(_paginatorDrpDown, numberOfResults);
        }

        public void CountBuildingWorksResults(int expectedNumberOfResults)
        {
            Driver.WaitForPageToLoad();
            WaitForSpinnerToDisappear();
            Driver.WaitForItem(_bwRecordsRow);
            
            Assert.AreEqual(expectedNumberOfResults, Driver.FindElements(_bwRecordsRow).Count);
        }

        public void SortBuildingWorksListBy(string columnName, string order)
        {
            _columnName = columnName;
            switch (order) // select order class
            {
                case "ASC":
                    _orderClass = "pi-sort-up";
                    break;
                case "DESC":
                    _orderClass = "pi-sort-down";
                    break;
                default:
                    Assert.Fail($"{order} is not a valid class");
                    break;
            }

            // keep clicking until desired element state is displayed, tries about 4 times
            for (var i = 0; i < 4; i++)
            {
                if (!Driver.WaitForItemWithinWebElement(GetHeaderItem(columnName), columnSortDetection,2))
                {
                    Driver.ClickItem(GetHeaderItem(columnName));
                    continue;
                }
                break;
            }
        }


        public void CheckBuildingWorksListIsSortedBy(string columnName, string order)
        {
            By el = null;
            switch (columnName)
            {
                case "ID":
                    el = _bwReferenceCell;
                    break;
                case "Destination":
                    el = _destinationCell;
                    break;
                case "Resort":
                    el = _resortCell;
                    break;
                case "Property":
                    el = _propertiesCell;
                    break;
                case "Creation Date":
                    el = _createdDateCell;
                    break;
                case "Recorded By":
                    el = _createdByCell;
                    break;
                case "Record Type":
                    el = _recordTypesCell;
                    break;
                default:
                    Assert.Fail($"{columnName} is not a valid column name");
                    break;
            }

            WaitUntilHeaderDisplayed(GetHeaderItem(columnName), order);
            Driver.WaitUntilItemEnabledAndDisplayed(el);
            if (columnName!= "Creation Date")
            {
                WaitUntilHeaderDisplayed(GetHeaderItem(columnName), order);
                Assert.True(IsOrderedBy(order,Driver.FindElements(el).Select(x => x.Text)), $"Unable to verify order for column {columnName}. Expected {order}");
            }
            else
            {
                WaitUntilHeaderDisplayed(GetHeaderItem(columnName), order);
                var creationDateList =Driver.FindElements(el).Select(x => x.Text);
                WaitUntilHeaderDisplayed(GetHeaderItem(columnName), order);
                var listOfDatesInString = creationDateList.Select(date => date.Replace(" ", "")).Select(x => x.Substring(0,x.Length-5)).ToList();
                WaitUntilHeaderDisplayed(GetHeaderItem(columnName), order);
                var listOfDatesInDateFormat = listOfDatesInString.Select(x => Driver.ParseDateTo_ddmmyy(x)).ToList();
                WaitUntilHeaderDisplayed(GetHeaderItem(columnName), order);
                Assert.True(IsOrderedBy(order, listOfDatesInDateFormat),$"Unable to verify order for date. Expected {order}");
            }
        }

        private bool IsOrderedBy<T>(string order, IEnumerable<T> input)
        {
            var found = false;
            switch (order)
            {
                case "ASC":
                   found= Driver.IsListOrderedByAscending(input);
                    break;
                case "DESC":
                   found= Driver.IsListOrderedByDescending(input);
                    break;
                default:
                    Assert.Fail("Expected order of the list shoulb be either ASC or DESC");
                    break;
            }
            return found;
        }

        public void WaitUntilHeaderDisplayed(By parentHeader,string order)
        {
            switch (order)
            {
                case "ASC":
                    Driver.WaitForItemWithinWebElement(parentHeader, columnSortDetection, 2);
                    break;
                case "DESC":
                    Driver.WaitForItemWithinWebElement(parentHeader, columnSortDetection, 2);
                    break;
                default:
                    Assert.Fail("Expected order of the list shoulb be either ASC or DESC");
                    break;
            }
        }

        public void VerifyBuildingWorkListFormData(string bwId, Table table)
        {
            _bwIdNumber = bwId == "bw_id" ? ScenarioContext.Current[BwIdScenarioContextKey].ToString() : bwId;
            foreach (var row in table.Rows)
            {
                var expectedBwId = row["bwid"] == "bw_id" ? ScenarioContext.Current[BwIdScenarioContextKey].ToString() : row["bw_id"];
                var expectedDestination = row["destination"];
                var expectedResort = row["resort"];
                var expectedProperty = row["property"];
                var expectedCreationDate = Driver.CalculateDate(row["creationDate"]).ToString("dd/MM/yy");
                var expectedRecordedBy = row["recordedBy"]=="current_user" ? ScenarioContext.Current[CurrentUsername] : row["recordedBy"];
                var expectedRecordType = row["recordType"];

                Driver.WaitForItem(BwRecordRow);
                Driver.WaitForItemWithinWebElement(BwRecordRow, _bwReferenceCell);
                Driver.WaitForItemWithinWebElement(BwRecordRow, _destinationCell);
                Driver.WaitForItemWithinWebElement(BwRecordRow, _resortCell);
                Driver.WaitForItemWithinWebElement(BwRecordRow, _propertiesCell);
                Driver.WaitForItemWithinWebElement(BwRecordRow, _createdDateCell);
                Driver.WaitForItemWithinWebElement(BwRecordRow, _createdByCell);
                Driver.WaitForItemWithinWebElement(BwRecordRow, _recordTypesCell);

                var actualBwId = Driver.GetText(Driver.FindElement(BwRecordRow).FindElement(_bwReferenceCell));
                var actualDestination = Driver.GetText(Driver.FindElement(BwRecordRow).FindElement(_destinationCell));
                var actualResort = Driver.GetText(Driver.FindElement(BwRecordRow).FindElement(_resortCell));
                var actualProperty = Driver.GetText(Driver.FindElement(BwRecordRow).FindElement(_propertiesCell));
                var actualcreationDate = _formatRecordCreationDate(Driver.GetText(Driver.FindElement(BwRecordRow).FindElement(_createdDateCell)));
                var actualRecordedBy = Driver.GetText(Driver.FindElement(BwRecordRow).FindElement(_createdByCell));
                var actualRecordType = Driver.GetText(Driver.FindElement(BwRecordRow).FindElement(_recordTypesCell));

                Assert.AreEqual(expectedBwId, actualBwId);
                Assert.AreEqual(expectedDestination, actualDestination);
                Assert.AreEqual(expectedResort, actualResort);
                Assert.AreEqual(expectedCreationDate, actualcreationDate);
                Assert.AreEqual(expectedRecordedBy, actualRecordedBy);
                Assert.AreEqual(expectedRecordType, actualRecordType);
                Assert.AreEqual(expectedProperty, actualProperty);
            }
        }

        // converts date from 03/10/18 09:25 to 03/10/18
        private static string _formatRecordCreationDate(string input)
        {
          return input.Replace(" ", "").Substring(0, input.Length - 6);
        }

        private ReadOnlyCollection<IWebElement> GetAllBwRecords()
        {
            return Driver.FindElements(By.XPath("//table//tr"));
        }

        // returns the By element for the column name
        private By GetHeaderItem(string columnName)
        {
            By headerEl=null;
            switch (columnName)
            {
                case "ID":
                    headerEl= _headerBwNumber;
                    break;
                case "Destination":
                    headerEl= _headerDestination;
                    break;
                case "Resort":
                    headerEl= _headerResort;
                    break;
                case "Property":
                    headerEl= _headerProperty;
                    break;
                case "Creation Date":
                    headerEl= _headerCreatedDate;
                    break;
                case "Recorded By":
                    headerEl= _headerCreatedBy;
                    break;
                case "Record Type":
                    headerEl= _headerRecordType;
                    break;
                default:
                    Assert.Fail($"{columnName} is not a valid coulmn");
                break;
            }
            return headerEl;
        }


        public void SearchWithPartialBwId(string search)
        {
            var searchCriteria = "";
            switch (search.ToLower())
            {
                case "bw_destination":
                    searchCriteria = _bwIdNumber.Substring(0, _bwIdNumber.Length - 5);
                  break;
                case "bw_number":
                    searchCriteria = _bwIdNumber.Substring(_bwIdNumber.Length - 5);
                    break;
                 default:
                 Assert.Fail($"{searchCriteria} is not a valid criteria");
                     break;
            }
            EnterCharacters(searchCriteria);

        }

        public void VerifyBwRecordIdContains(string criteria)
        {
            var searchCriteria = "";
            switch (criteria.ToLower())
            {
                case "bw_destination":
                    searchCriteria = _bwIdNumber.Substring(0, _bwIdNumber.Length - 5);
                    break;
                case "bw_number":
                    searchCriteria = _bwIdNumber.Substring(_bwIdNumber.Length - 5);
                    break;
                default:
                    Assert.Fail($"{searchCriteria} is not a valid criteria");
                    break;
            }

            var listOfId = Driver.FindElements(_bwReferenceCell).Select(x => x.Text.Trim());
            foreach (var id in listOfId)
            {
                Assert.True(id.Contains(searchCriteria),$"{searchCriteria} could not be found in bw id");
            }
        }

        public void VerifySearchResultContains(string propDest, string expectedTerm)
        {
            By column = null;
            switch (propDest.ToLower())
            {
                case "destination":
                    column = _destinationCell;
                    break;
                case "property":
                    column = _propertiesCell;
                    break;
                default:
                    Assert.Fail($"{propDest} is not a valid criteria");
                    break;
            }
            var listOfRecords = Driver.FindElements(column).Select(x => x.Text.Trim());
            foreach (var record in listOfRecords)
            {
                Assert.AreEqual(expectedTerm,record, $"{expectedTerm} could not be found in bw id");
            }
        }
    }
}