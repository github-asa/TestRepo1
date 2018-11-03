using J2BIOverseasOps.Extensions;
using J2BIOverseasOps.Pages.Customer_Interaction;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.Customer_Interaction
{
    [Binding]
    public sealed class ReportedByStepDefs : BaseStepDefs
    {
        private readonly ReportedByPage _reportedBy;

        public ReportedByStepDefs(IWebDriver driver, ILog log) : base(driver, log)
        {
            _reportedBy = new ReportedByPage(driver, log);
        }

        [Then(@"I verify the Jet2holidays customer option is pre-selected on the reported by page")]
        public void ThenIVerifyTheJetHolidaysCustomerOptionIsPre_SelectedOnTheReportedByPage()
        {
            _reportedBy.VerifyJet2HolCustomerOptionPreselected();
        }

        [Then(@"I verify the Next button is ""(.*)"" on the reported by page")]
        public void ThenIVerifyTheNextButtonIsOnTheReportedByPage(string expectedStatus)
        {
            _reportedBy.VerifyBtnStatus(expectedStatus);
        }

        [Then(
            @"I verify the passengers on the previously selected booking are available for selection in the Jet 2 Holidays customer dropdown on the reported by page:")]
        public void
            ThenIVerifyThePassengersOnThePreviouslySelectedBookingAreAvailableForSelectionInTheJetHolidaysCustomerDropdownOnTheReportedByPage(
                Table table)
        {
            
            _reportedBy.VerifyNamesJet2HolidaysDrpDwn(table);
        }

        [Then(
            @"I verify that only one of the Jet 2 Holidays Customer, Other user and Other radio button can be selected on the reported by page")]
        public void
            ThenIVerifyThatOnlyOneOfTheJetHolidaysCustomerOtherUserAndOtherRadioButtonCanBeSelectedOnTheReportedByPage()
        {
            _reportedBy.VerifyRadBtnSelection();
        }

        [When(@"I select ""(.*)"" from the Jet 2 Holidays customer dropdown on the reported by page")]
        public void WhenISelectMrDonaldUnctiousFromTheJetHolidaysCustomerDropdownOnTheReportedByPage(
            string passengerName)
        {
            _reportedBy.SelectCustomerFromDrpDwn(passengerName);
        }

        [When(@"I click the Next button on the reported by page")]
        public void WhenIClickTheNextButtonOnTheReportedByPage()
        {
            _reportedBy.ClicktheNextBtn();
        }

        [Then(@"I verify the other user option is pre-selected on the reported by page")]
        public void ThenIVerifyTheOtherUserOptionIsPre_SelectedOnTheReportedByPage()
        {
            _reportedBy.VerifyOtherUserOptionSelection();
        }

        [When(@"I select the Jet2 Holidays customer option on the reported by page")]
        public void WhenISelectTheJetHolidaysCustomerOptionOnTheReportedByPage()
        {
            _reportedBy.SelectJet2HolidayCustomerRad();
        }

        [Then(@"I verify I ""(can|can not)"" see the Jet 2 Holidays customer dropdown and Search for Jet2Holidays customer button on the reported by page")]
        public void ThenIVerifyISeeTheJetHolidaysCustomerDropdownAndSearchForJetHolidaysCustomerButtonOnTheReportedByPage(string status)
        {
            _reportedBy.VerifyJet2HolDrpDwnBtn(status);
        }

        [Then(@"I verify the ""(.*)"" is selected from the Jet 2 Holidays customer dropdown on the reported by page")]
        public void ThenIVerifyTheIsSelectedFromTheJetHolidaysCustomerDropdownOnTheReportedByPage(string customerName)
        {
            _reportedBy.VerifySelectedNameJet2HolidaysDrpDwn(customerName);
        }

        [When(@"I select the Other option on the reported by page")]
        public void WhenISelectTheOtherUserOptionOnTheReportedByPage()
        {
            _reportedBy.SelectOtherRad();
        }

        [Then(@"I verify Jet2holidays customer radio button label text is '(.*)'")]
        public void ThenIVerifyJetholidaysCustomerRadioButtonLabelTextIs(string label)
        {
            _reportedBy.VerifyJet2HolCustomerRadBtnTxt(label);
        }

        [Then(@"I verify Jet2holidays colleague radio button label text is '(.*)'")]
        public void ThenIVerifyJetholidaysCollegueRadioButtonLabelTextIs(string label)
        {
            _reportedBy.VerifyJet2HolColleagueRadBtnTxt(label);
        }

        [Then(@"I verify Other radio button label text is '(.*)'")]
        public void ThenIVerifyOtherRadioButtonLabelTextIs(string label)
        {
            _reportedBy.VerifyOtherRadBtnTxt(label);
        }

        [When(@"I select the Jet2 Holidays colleague option on the reported by page")]
        public void WhenISelectTheJetHolidaysColleagueOptionOnTheReportedByPage()
        {
            _reportedBy.SelectJet2HolidayColleagueRad();
        }

        [Then(@"the option to select a department for a Jet2holidays colleague is displayed on the reported by page")]
        public void ThenTheOptionToSelectADepartmentForAJetholidaysColleagueIsDisplayedOnTheReportedByPage()
        {
            _reportedBy.VerifyDepartmentDropdownDisplayed();
        }

        [Then(@"I select the following Jet2holidays colleague on the reported by page:")]
        public void ThenISelectTheFollowingJetholidaysColleagueOnTheReportedByPage(Table table)
        {
            _reportedBy.SelectJet2HolidaysColleague(table);
        }

        [When(@"I select the ""(.*)"" option from the departments dropdown on the reported by page")]
        public void WhenISelectTheFromTheDepartmentsDropdownOnTheReportedByPage(string department)
        {
            _reportedBy.SelectDepartmentFromDropdown(department);
        }

        [Then(@"I verify the list of colleagues in the multiselect displays usernames below all colleagues whose first name and surname does exist on the reported by page")]
        public void ThenIVerifyTheListOfColleaguesInTheMultiselectDisplaysUsernamesBelowAllColleaguesWhoseFirstNameAndSurnameDoesExistOnTheReportedByPage()
        {
            _reportedBy.VerifyColleaguesListOrder();
        }

        [When(@"I select the ""(.*)"" from the colleagues dropdown on the reported by page")]
        public void WhenISelectTheFromTheColleaguesDropdownOnTheReportedByPage(string colleague)
        {
            _reportedBy.SelectColleagueFromDropdown(colleague);
        }

        [Then(@"I verify the following Jet2holidays colleague has been selected on the reported by page:")]
        public void ThenIVerifyTheFollowingJetholidaysColleagueHasBeenSelectedOnTheReportedByPage(Table table)
        {
            _reportedBy.VerifySelectedJet2HolidaysColleague(table);
        }

        [When(@"then I verify the colleagues dropdown is enabled on the reported by page")]
        public void WhenThenIVerifyTheColleaguesDropdownIsEnabledOnTheReportedByPage()
        {
            _reportedBy.VerifyColleaguesDropdownEnabled();
        }

        [When(@"then I verify the colleagues dropdown is disabled on the reported by page")]
        public void WhenThenIVerifyTheColleaguesDropdownIsDisabledOnTheReportedByPage()
        {
            _reportedBy.VerifyColleaguesDropdownDisabled();
        }

        [Then(@"I enter the phone number as ""(.*)"" for the Jet2holidays colleague on the reported by page")]
        public void ThenIEnterThePhoneNumberAsForTheJetholidaysColleagueOnTheReportedByPage(string phoneNumber)
        {
            _reportedBy.EnterColleaguePhoneNumber(phoneNumber);
        }

        [Then(@"I enter the email address as ""(.*)"" for the Jet2holidays colleague on the reported by page")]
        public void ThenIEnterTheEmailAddressAsForTheJetholidaysColleagueOnTheReportedByPage(string emailAddress)
        {
            _reportedBy.EnterColleagueEmailAddress(emailAddress);
        }

        [Then(@"I enter the phone number as ""(.*)"" for the Jet2holidays customer on the reported by page")]
        public void ThenIEnterThePhoneNumberAsForTheJetholidaysCustomerOnTheReportedByPage(string phoneNumber)
        {
            _reportedBy.EnterCustomersPhoneNumber(phoneNumber);
        }

        [Then(@"I enter the room number as ""(.*)"" for the Jet2holidays customer on the reported by page")]
        public void ThenIEnterTheRoomNumberAsForTheJetholidaysCustomerOnTheReportedByPage(string roomNumber)
        {
            _reportedBy.EnterCustomersRoomNumber(roomNumber);
        }

        [Then(@"the option to free type a third party user is displayed on the reported by page")]
        public void ThenTheOptionToFreeTypeAThirdPartyUserIsDisplayedOnTheReportedByPage()
        {
            _reportedBy.VerifyFreeTypeDisplayed();
        }

        [When(@"I enter the following text into the free type text field on the reported by page:")]
        public void WhenIEnterTheFollowingTextIntoTheFreeTypeTextFieldOnTheReportedByPage(Table table)
        {
            _reportedBy.EnterOtherOptionFreeText(table);
        }

        [Then(@"I verify the text entered in the into the free type text field on the reported by page:")]
        public void ThenIVerifyTheTextEnteredInTheIntoTheFreeTypeTextFieldOnTheReportedByPage(Table table)
        {
            _reportedBy.VerifyOtherOptionFreeText(table);
        }

        [Then(@"I verify only ""(.*)"" charactors can be entered in to the free type field on the reported by page")]
        public void ThenIVerifyOnlyCharactorsCanBeEnteredInToTheFreeTypeFieldOnTheReportedByPage(int charlimit)
        {
            _reportedBy.VerifyCharacterLimitFreeText(charlimit);
        }

        [Then(@"I enter the phone number as ""(.*)"" for the Other option on the reported by page")]
        public void ThenIEnterThePhoneNumberAsForTheOtherOptionOnTheReportedByPage(string phoneNumber)
        {
            _reportedBy.EnterOtherPhoneNumber(phoneNumber);
        }

        [Then(@"I enter the email address as ""(.*)"" for the Other option on the reported by page")]
        public void ThenIEnterTheEmailAddressAsForTheOtherOptionOnTheReportedByPage(string emailAddress)
        {
            _reportedBy.EnterOtherEmailAddress(emailAddress);
        }

        [When(@"I deselect the department drop down on the reported by page")]
        public void WhenIDeselectTheDepartmentDropDownOnTheReportedByPage()
        {
            _reportedBy.DeselectColleagueDepartmentDropdown();
        }

        [When(@"I clear the email address field for the Jet2holidays colleague on the reported by page")]
        public void WhenIClearTheEmailAddressFieldForTheJetholidaysColleagueOnTheReportedByPage()
        {
            _reportedBy.ClearColleagueEmailAddress();
        }

        [When(@"I clear the phone number field for the Jet2holidays colleague on the reported by page")]
        public void WhenIClearThePhoneNumberFieldForTheJetholidaysColleagueOnTheReportedByPage()
        {
            _reportedBy.ClearColleaguePhoneNumber();
        }

        [When(@"I deselect the customer drop down on the reported by page")]
        public void WhenIDeselectTheCustomerDropDownOnTheReportedByPage()
        {
            _reportedBy.DeselectCustomerDropdown();
        }

        [When(@"I clear the phone number field for the Jet2holidays customer on the reported by page")]
        public void WhenIClearThePhoneNumberFieldForTheJetholidaysCustomerOnTheReportedByPage()
        {
            _reportedBy.ClearCustomerPhoneNumber();
        }

        [When(@"I clear the room number field for the Jet2holidays customer on the reported by page")]
        public void WhenIClearTheRoomNumberFieldForTheJetholidaysCustomerOnTheReportedByPage()
        {
            _reportedBy.ClearCustomerRoomNumber();
        }

        [Then(@"I verify the Jet2holidays colleague option is pre-selected on the reported by page")]
        public void ThenIVerifyTheJetHolidaysColleagueOptionIsPre_SelectedOnTheReportedByPage()
        {
            _reportedBy.VerifyJet2HolColleagueOptionPreselected();
        }

        [Then(@"a validation message '(.*)' is displayed on the reported by page")]
        public void ThenAValidationMessageIsDisplayedOnTheReportedByPage(string message)
        {
            _reportedBy.VerifyValidationMessage(message);
        }

        [Then(@"a validation message '(.*)' is displayed in the other section on the reported by page")]
        public void ThenAValidationMessageIsDisplayedInTheOtherSectionOnTheReportedByPage(string message)
        {
            _reportedBy.VerifyValidationMessageOther(message);
        }

        [Then(@"the option to enter a department for a Jet2holidays colleague is displayed on the reported by page")]
        public void ThenTheOptionToEnterADepartmentForAJetholidaysColleagueIsDisplayedOnTheReportedByPage()
        {
            _reportedBy.VerifyDepartmentInputDisplayed();
        }

        [When(@"I enter the department name as ""(.*)"" for the Jet2holidays colleague on the reported by page")]
        public void WhenIEnterTheDepartmentNameAsForTheJetholidaysColleagueOnTheReportedByPage(string departmentName)
        {
            _reportedBy.EnterColleagueDepartmentName(departmentName);
        }



    }
}