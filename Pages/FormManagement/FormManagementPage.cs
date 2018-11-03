using System;
using J2BIOverseasOps.Extensions;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Pages.FormManagement
{
    internal class FormManagementPage : CommonFormMgmtElements
    {

        public FormManagementPage(IWebDriver driver, ILog log) : base(driver, log)
        {
           
        }

        private static string _name;

        private readonly By _pageTitle = By.XPath("//app-forms-list-viewer//h2");
        private readonly By _formNameFilter = By.Id("form-name-filter");
        private readonly By _inactiveFormsChkBox = By.XPath("//p-checkbox[@name='chkInactive']");
        private readonly By _lockedFormsFormsChkBox = By.XPath("//p-checkbox[@name='chkLocked']");
        private readonly By _formMgmtTableHeader = By.XPath("//p-table//th");
        private readonly By _backBtn = By.Id("btnBack");
        private readonly By _allForms = By.XPath("//tbody//tr");
        private readonly By _editBtn = By.XPath(".//*[@label='Edit']//button");
        
        // Individual form elements
        private readonly By _formMgmtTableRow = By.XPath("//tbody//tr[1]");
        private readonly By _formName = By.XPath(".//*[@dataname='formName']");
        private readonly string _inactiveForm = "isInactive";
        private readonly By _isActiveChecbox = By.XPath(".//*[@dataname='isActive']//p-checkbox");
        private readonly By _isLockedChkBox = By.XPath(".//*[@dataname='isLocked']//p-checkbox");
        private readonly By _modifiedBy = By.XPath(".//*[@dataname='modifiedBy']");
        private readonly By _userSignOffChkBox = By.XPath(".//*[@dataname='requiresUserSignOff']//p-checkbox");
        private readonly By _versionNumber = By.CssSelector("[id*=versionCol-]");
        private readonly By _viewBtn = By.XPath(".//*[@label='View']//button");

        //Create new form
        private readonly By _createNewFormBtn = By.Id("btnSuccess");
        private readonly By _popupFormCreation = By.CssSelector("#form-management-choose-form-type-dialog > div");
        private readonly By _buttonsCreatePopup = By.CssSelector("#form-management-choose-form-type-dialog button");
        private readonly By _buttonCancelCreatePopup = By.CssSelector("#form-management-choose-form-type-dialog .ui-dialog-titlebar-close");
        private readonly By _fName = By.CssSelector($"[class^={_name}]");
        private readonly By _formNameCol = By.CssSelector("[id*=nameCol-");

        public void VerifyPageTitle(string expectedTitle)
        {
            var actualTitle = Driver.FindElement(_pageTitle).Text;
            Assert.AreEqual(expectedTitle, actualTitle);
        }

        public void VerifySearchTextBoxState(string visibility, string enbaledState)
        {
            VerifyElementVisibility(visibility, _formNameFilter);
            VerifyElementState(enbaledState, _formNameFilter);
        }

        public void VerifyInactiveFormsCheckBoxState(string visibilityState, string enbaledState, string checkedState)
        {
            VerifyElementVisibility(visibilityState, _inactiveFormsChkBox);
            VerifyElementState(enbaledState, _inactiveFormsChkBox);
            VerifyCheckBoxTickState(checkedState, _inactiveFormsChkBox);
        }

        public void VerifyLockedFormsCheckBoxState(string visibilityState, string enbaledState, string checkedState)
        {
            VerifyElementVisibility(visibilityState, _lockedFormsFormsChkBox);
            VerifyElementState(enbaledState, _lockedFormsFormsChkBox);
            VerifyCheckBoxTickState(checkedState, _lockedFormsFormsChkBox);
        }


        public void VerifyFormManagementColumns(Table table)
        {
            var loopCounter = 0;
            foreach (var row in table.Rows)
            {
                var expectedColumnName = row["form_mgmt_column_headers"];
                var listOfColumnHeaders = Driver.FindElements(_formMgmtTableHeader);
                var actualColumnName = listOfColumnHeaders[loopCounter].Text;
                Assert.True(actualColumnName.Contains(expectedColumnName),
                    $"Could not verify {expectedColumnName} is present within the table. Actual found {actualColumnName}");
                loopCounter++;
            }
        }

        public void VerifyCreateNewFormButtonState(string visibilityState, string enbaledState)
        {
            VerifyElementVisibility(visibilityState, _createNewFormBtn);
            VerifyElementState(enbaledState, _createNewFormBtn);
        }

        public void VerifyBackButtonState(string visibilityState, string enbaledState)
        {
            VerifyElementVisibility(visibilityState, _backBtn);
            VerifyElementState(enbaledState, _backBtn);
        }

        public void ClickCreateFormBtn()
        {
            Driver.ClickItem(_createNewFormBtn);
        }

        /// <summary>
        ///     Clicks on the edit link for the given form, If form name not given then selects the newly created form
        /// </summary>
        /// <param name="formName"></param>
        /// <param name="version">Form version , 1 by default</param>
        public void ClickEditLink(string formName = "", string version = "1")
        {
            var formRow = GetFormRow(formName, version);
            Assert.True(formRow.IsElementWithinWebElementPresent(_editBtn),
                $"Could not find the edit button on the form row");
            var editBtnElement = formRow.FindElement(_editBtn);
            Driver.ClickItem(editBtnElement);
        }

        public void ClickViewLink(string formName = "", string version = "1")
        {
            var formRow = GetFormRow(formName, version);
            Assert.True(formRow.IsElementWithinWebElementPresent(_viewBtn),
                $"Could not find the view button on the form row");
            var viewBtnElement = formRow.FindElement(_viewBtn);
            Driver.ClickItem(viewBtnElement);
        }

        public void VerifyFormName(string expectedName = null)
        {
            expectedName = string.IsNullOrWhiteSpace(expectedName) ? 
                ScenarioContext.Current[FormNameKey].ToString() : expectedName;        
            GetFormRow(expectedName); //updates the list of 
            var actualName = ScenarioContext.Current[FormNameKey].ToString();
            Assert.True(actualName == expectedName,
                $"Could not verify the Actual name {actualName} same as Expected name {expectedName}");
        }

        public void VerifyFormDisplayed(string formName = "")
        {
            if (formName == "")
            {
                formName = ScenarioContext.Current[FormNameKey].ToString();
            }

            VerifyFormName(formName);
        }

        public void VerifyFormNotDisplayed(string formName = "")
        {

            _name = string.IsNullOrWhiteSpace(formName)
                ? ScenarioContext.Current[FormNameKey].ToString()
                : formName;

            Assert.True(Driver.WaitUntilElementNotDisplayed(_fName, 2),
                $"Was able to find form {_name} while expecting it not to be found");
        }

        public void VerifyFormVersionNotDisplayed(string expectedVersion, string formName = "")
        {
            if (formName == "")
            {
                formName = ScenarioContext.Current[FormNameKey].ToString();
            }

            var formRowElement = By.XPath($"//*[contains(@class, '{formName}')]");
            var actualVersion = Driver.FindElement(formRowElement).FindElement(_versionNumber).Text;

            Assert.True(expectedVersion != actualVersion,
                $"Was able to find version {expectedVersion} for the form {formName} while expecting it not to be there");
        }

        public void VerifyNumberOfResultsDisplayed(int expectedNumbOfResults)
        {
            var numberOfResults = Driver.FindElements(By.XPath("//tbody/tr")).Count;
            Assert.True(numberOfResults == expectedNumbOfResults,
                $"Expected number of results was not equal to {expectedNumbOfResults}. Actual Number {numberOfResults}");
        }

        public void VerifyFormValues(Table table, string expectedFormName = "", string versionNumber = "1")
        {
            CloseGrowlNotification();
            if (expectedFormName == "")
            {
                expectedFormName = ScenarioContext.Current[FormNameKey].ToString();
            }

            var formWebElement = GetFormRow(expectedFormName, versionNumber);
            foreach (var row in table.Rows)
            {
                var expectedVersion = row["version"];
                var expectedLockedStatus = row["locked"];
                var expectedSignOffStatus = row["user_sign_off"];
                var expectedActiveStaus = row["active"];
                var expectedModifiedBy = row["last_modified_by"];

                if (expectedModifiedBy.ToLower()=="user_name")
                {
                    expectedModifiedBy = ScenarioContext.Current[CurrentUserDisplayName].ToString();
                }

                var actualVersion = formWebElement.FindElement(_versionNumber).Text;
                var actualFormName = formWebElement.FindElement(_formName).Text;
                var actualIsLocked = Driver.IsCheckBoxTicked(formWebElement.FindElement(_isLockedChkBox)).ToString();
                var actualUserSignOff =
                    Driver.IsCheckBoxTicked(formWebElement.FindElement(_userSignOffChkBox)).ToString();
                var actualIsActive = Driver.IsCheckBoxTicked(formWebElement.FindElement(_isActiveChecbox)).ToString();
                var actualModifiedBy= formWebElement.FindElement(_modifiedBy).Text;

                Assert.AreEqual(expectedFormName, actualFormName,$"Expected Form Name {expectedFormName} Actual Form Name {actualFormName}");
                Assert.AreEqual(expectedVersion,actualVersion, $"ExpectedVersion {expectedVersion} Actual Version {actualVersion}");
                StringAssert.AreEqualIgnoringCase(expectedModifiedBy, actualModifiedBy,   $"Expected {expectedModifiedBy} Actual {actualModifiedBy}");
                Assert.AreEqual(expectedLockedStatus, actualIsLocked, $"Expected {expectedLockedStatus} Actual {actualIsLocked}");
                Assert.AreEqual(expectedSignOffStatus, actualUserSignOff, $"Expected {expectedSignOffStatus} Actual {actualUserSignOff}");
                Assert.AreEqual(expectedActiveStaus, actualIsActive,  $"Expected {expectedActiveStaus} Actual {actualIsActive}");

                Assert.AreEqual(expectedFormName, actualFormName,
                    $"Expected Form Name {expectedFormName} Actual Form Name {actualFormName}");
                Assert.AreEqual(expectedVersion, actualVersion,
                    $"ExpectedVersion {expectedVersion} Actual Version {actualVersion}");
                Assert.AreEqual(expectedLockedStatus, actualIsLocked,
                    $"Expected {expectedLockedStatus} Actual {actualIsLocked}");
                Assert.AreEqual(expectedSignOffStatus, actualUserSignOff,
                    $"Expected {expectedSignOffStatus} Actual {actualUserSignOff}");
                Assert.AreEqual(expectedActiveStaus, actualIsActive,
                    $"Expected {expectedActiveStaus} Actual {actualIsActive}");
            }
        }

        public void UpdateFormValues(Table table, string expectedFormName = "", string version = "1")
        {
            if (expectedFormName == "")
            {
                expectedFormName = ScenarioContext.Current[FormNameKey].ToString();
            }

            foreach (var row in table.Rows)
            {
                var updateLockedStatus = row["locked"];
                var updateSignOffStatus = row["user_sign_off"];                

                var isLockedWebElement = GetFormRow(expectedFormName, version).FindElement(_isLockedChkBox);
                Driver.TickUntickCheckBox(isLockedWebElement, updateLockedStatus);
                CloseGrowlNotification();

                var userSignOffWebElement = GetFormRow(expectedFormName, version).FindElement(_userSignOffChkBox);
                Driver.TickUntickCheckBox(userSignOffWebElement, updateSignOffStatus);
                CloseGrowlNotification();

                if (row.ContainsKey("active"))
                {
                    var updateActiveStatus = row["active"];
                    var isActiveWebElement = GetFormRow(expectedFormName, version).FindElement(_isActiveChecbox);
                    var checkBoxSelected = Driver.IsCheckBoxTicked(isActiveWebElement).ToString().ToLower();
                    Driver.TickUntickCheckBox(isActiveWebElement, updateActiveStatus);


                    if (checkBoxSelected != updateActiveStatus.ToLower())
                    {
                        if (updateActiveStatus.ToLower().Equals("false"))
                        {
                            ConfirmationPopupDisplayedWithMessage("Deactivate selected form",
                                "Are you sure you want to deactivate this form?");
                            ClickOnConfirmationPopup("Deactivate form");
                        }
                        else if (updateActiveStatus.ToLower().Equals("true"))
                        {
                            ConfirmationPopupDisplayedWithMessage("Activate form",
                                "An active version of this form already exists. By continuing, the existing form will be de-activated. Do you wish to continue?");
                            ClickOnConfirmationPopup("yes");
                        }
                    }
                }                

                WaitForSpinnerToDisappear();
                CloseGrowlNotification();
            }
        }

        public void ActivateForm(string version, string formName = null)
        {
            formName = string.IsNullOrEmpty(formName) ? ScenarioContext.Current[FormNameKey].ToString() : formName;
            var isActiveWebElement = GetFormRow(formName, version).FindElement(_isActiveChecbox);
            Driver.TickUntickCheckBox(isActiveWebElement, "true");
        }

        public void ShowLockedForms(string tickBoxOption)
        {
            Driver.TickUntickCheckBox(_lockedFormsFormsChkBox, tickBoxOption);
        }


        public void ShowInactiveForms(string tickBoxOption)
        {
            Driver.TickUntickCheckBox(_inactiveFormsChkBox, tickBoxOption);
        }

        public void SearchFormName(string formName = null)
        {
            formName =  string.IsNullOrWhiteSpace(formName) ? ScenarioContext.Current[FormNameKey].ToString() : formName;
            if (!Driver.GetInputBoxValue(_formNameFilter).Equals(formName))
            {
                Driver.Clear(_formNameFilter);
                Driver.EnterText(_formNameFilter, formName);
            }
        }

        /// <summary>
        ///     Finds the form with given name from the list of forms
        /// </summary>
        /// <param name="formName"> form to look for </param>
        /// <param name="versionNumber">version number to find, default value is 1</param>
        /// <returns></returns>
        private IWebElement GetFormRow(string formName = "", string versionNumber = "1")
        {
            if (formName == "ANY")
            {
                return Driver.FindElement(_formMgmtTableRow);
            }

            var form = formName == "" ? ScenarioContext.Current[FormNameKey].ToString() : formName;
            SearchFormName(form);
            var forRowElem = $"//*[contains(@class, '{form}')]";
            var listOfForms = Driver.FindElements(By.XPath(forRowElem));
            foreach (var row in listOfForms)
            {
                var actualFormName = Driver.GetText(row.FindElement(_formNameCol));
                if (actualFormName.Equals(form))
                {
                    if (Driver.GetText(row.FindElement(_versionNumber)) == versionNumber)
                    {
                        AddOrUpdateFormName(row);
                        return row;
                    }
                }
            }

            throw new Exception($"could not find form {form}");
        }

        //adds or updates the form name in a scenario context
        private void AddOrUpdateFormName(IWebElement formRow)
        {
            var formName = formRow.FindElement(_formName).Text;
            ScenarioContext.Current[FormNameKey] = formName;
        }

        public void VerifyFormDisplayedAtBottom(string formName)
        {
            var lastRowNumber = Driver.FindElements(_allForms).Count;
            var lastFormWebElement = Driver.FindElement(By.XPath($"//tbody//tr[{lastRowNumber}]"));
            var lastFormOnTheList = lastFormWebElement.GetAttribute("Class");
            Assert.True(lastFormOnTheList.Contains(formName),
                $"Was not able to find {formName} at the bottom of the list. Actual {lastFormOnTheList}");
        }

        public void VerifyFormGreyedOut(string formName = null)
        {
            formName = string.IsNullOrWhiteSpace(formName) ? ScenarioContext.Current[FormNameKey].ToString() : formName;
            var formElement = GetFormRow(formName);
            Assert.True(formElement.GetAttribute("class").Contains(_inactiveForm),
                "Unable to verify the Form as Greyed Out");
        }

        public void VerifyFormNotGreyedOut(string formName = null)
        {
            formName = string.IsNullOrWhiteSpace(formName) ? ScenarioContext.Current[FormNameKey].ToString() : formName;
            var formElement = GetFormRow(formName);
            Assert.True(!formElement.GetAttribute("class").Contains(_inactiveForm),
                "Unable to verify the Form as Greyed Out");
        }

        public void VerifyButtonDisplayed(string buttonType, string formName = null)
        {
            formName = string.IsNullOrWhiteSpace(formName) ? ScenarioContext.Current[FormNameKey].ToString() : formName;
            CloseGrowlNotification();
            var formRow = GetFormRow(formName);
            switch (buttonType)
            {
                case "View":
                    Assert.True(formRow.IsElementWithinWebElementPresent(_viewBtn),
                        "Could not find the view button on the form row");
                    return;
                case "Edit":
                    Assert.True(formRow.IsElementWithinWebElementPresent(_editBtn),
                        "Could not find the view button on the form row");
                    return;
            }
        }

        public void VerifyCreationPopupIsDisplayed()
        {
            Assert.IsTrue(Driver.WaitForItem(_popupFormCreation), "The form creation popup is not being displayed.");
        }

        public void ClickCreateNewFormButtonOnPopUp(string buttonText)
        {
            var buttons = Driver.FindElements(_buttonsCreatePopup);
            foreach (var button in buttons)
            {
                if (Driver.GetText(button).Equals(buttonText))
                {
                    Driver.ClickItem(button);
                    break;
                }
            }
        }

        public void VerifyThatTheFormCreationPopupIsNotDisplayed()
        {
            Assert.IsTrue(Driver.WaitUntilElementNotDisplayed(_popupFormCreation), "The form creation popup is still being displayed.");
        }

        public void ClickCancelOnFormsCreationPopup()
        {
            Driver.ClickItem(_buttonCancelCreatePopup);
        }

        public void VerifyFormName()
        {
            var form = ScenarioContext.Current[FormNameKey].ToString();

            SearchFormName(form);
            var formName = Driver.GetText(_formNameCol);

            Assert.AreEqual(form.TrimEnd(), formName, "The form name is not as expected");
        }
    }
}