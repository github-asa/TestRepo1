using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using J2BIOverseasOps.Extensions;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Pages.Customer_Interaction
{
    internal class ReportedByPage : BasePage
    {
        //buttons
        private readonly By _btnNext = By.CssSelector("#reported-by-next-button");
        private readonly By _btnNextDisabled = By.CssSelector("#reported-by-next-button[disabled]");
        private readonly By _btnSearchForJet2HolCustomer =
            By.CssSelector("#reported-by-search-jet2-customer-button > button");

        //dropdowns
        private readonly By _drpDwnCustomers = By.CssSelector("[customers]");
        private readonly By _drpDwnCustomersInput = By.CssSelector(".ui-dropdown-filter");
        private readonly By _drpDwnCustomersAvailablePassengerNames = By.CssSelector(".ui-dropdown li > span");
        private readonly By _drpDwnCustomersSelectFirstPassengerName = By.CssSelector(".ui-dropdown-panel.ui-widget-content.ui-corner-all li:nth-child(1)");
        private readonly By _drpDwnCustomersSelectedPassengerName = By.CssSelector("p-dropdown label");
        private readonly By _drpDwnDepartment = By.CssSelector("[departmentslist]");
        private readonly By _drpDwnColleagues = By.CssSelector("[userslist]");
        private readonly By _drpDwnColleaguesDisabled = By.CssSelector("[userslist] .ui-state-disabled");

        //Input Fields
        private readonly By _inputColleaguesPhoneNumber = By.CssSelector("#colleagueSelectedPhone");
        private readonly By _inputColleaguesEmailAddress = By.CssSelector("#colleagueSelectedEmail");
        private readonly By _inputColleaguesDepartmentName = By.CssSelector("#otherDepartmentName");
        private readonly By _inputCustomersPhoneNumber = By.CssSelector("#customerSelectedPhone");
        private readonly By _inputCustomersRoomNumber = By.CssSelector("#customerSelectedRoomNumber");
        private readonly By _inputFreeType = By.CssSelector("#reported-by-other-person");
        private readonly By _inputOtherEmailAddress = By.CssSelector("#reported-by-other-email");
        private readonly By _inputOtherPhoneNumber = By.CssSelector("#reported-by-other-phone");


        //Labels
        private readonly By _lblJet2HolidaysCustomer = By.CssSelector("#reported-by-radio-jet2-customer > label");
        private readonly By _lblJet2HolidaysColleague = By.CssSelector("#reported-by-radio-jet2-colleague > label");
        private readonly By _lblOther = By.CssSelector("#reported-by-radio-other > label");
        private readonly By _validationMessage = By.CssSelector("div:not([hidden]) > p-message .ui-message-text");
        private readonly By _validationMessageOther = By.CssSelector("#other-no-free-text-provided-validation div:not([hidden]) .ui-message-text");

        //radio buttons
        private readonly By _radOther = By.CssSelector("#reported-by-radio-other > div > div.ui-radiobutton-box");
        private readonly By _radOtherActive = By.CssSelector("#reported-by-radio-other .ui-state-active");
        private readonly By _radJet2HolidaysColleague = By.CssSelector("#reported-by-radio-jet2-colleague > div > div.ui-radiobutton-box");
        private readonly By _radJet2HolidaysColleagueActive = By.CssSelector("#reported-by-radio-jet2-colleague .ui-state-active");
        private readonly By _radJet2HolidaysCustomer = By.CssSelector("#reported-by-radio-jet2-customer > div > div.ui-radiobutton-box");
        private readonly By _radJet2HolidaysCustomerActive = By.CssSelector("#reported-by-radio-jet2-customer .ui-state-active");

        public ReportedByPage(IWebDriver driver, ILog log) : base(driver, log)
        {
        }

        public void VerifyJet2HolCustomerOptionPreselected()
        {
            Assert.True(Driver.IsElementPresent(_radJet2HolidaysCustomerActive),
                $"The Jet 2 Holidays Customer radio button is not selected");
        }

        public void VerifyBtnStatus(string expectedStatus)
        {
            switch (expectedStatus)
            {
                case "enabled":
                    Assert.True(!Driver.IsElementPresent(_btnNextDisabled));
                    break;
                case "disabled":
                    Assert.True(Driver.IsElementPresent(_btnNextDisabled));
                    break;
                default:
                    throw new Exception(expectedStatus + " :is not a valid button state ");
            }
        }

        public void VerifyNamesJet2HolidaysDrpDwn(Table table)
        {
            Driver.ClickItem(_drpDwnCustomers);

            var i = 0;
            foreach (var row in table.Rows)
            {
                var actualDrpDwnPassengerNames = Driver.FindElements(_drpDwnCustomersAvailablePassengerNames);

                Assert.AreEqual(row["Passenger Names"], Driver.GetText(actualDrpDwnPassengerNames[i]),
                    $"The Passenger Name should be {row["Passenger Names"]} instead of {actualDrpDwnPassengerNames[i].Text}.");
                i++;
            }

            Driver.ClickItem(_drpDwnCustomers);
        }

        public void VerifyRadBtnSelection()
        {
            Driver.ClickItem(_radJet2HolidaysColleague);
            Assert.True(!Driver.IsElementPresent(_radJet2HolidaysCustomerActive),
                $"The Jet 2 Holidays Customer radio button is selected");
            Assert.True(Driver.IsElementPresent(_radJet2HolidaysColleagueActive), $"The Other user radio button is not selected");
            Assert.True(!Driver.IsElementPresent(_radOtherActive), $"The Other radio button is selected");

            Driver.ClickItem(_radOther);
            Assert.True(!Driver.IsElementPresent(_radJet2HolidaysCustomerActive),
                $"The Jet 2 Holidays Customer radio button is selected");
            Assert.True(!Driver.IsElementPresent(_radJet2HolidaysColleagueActive), $"The Other user radio button is selected");
            Assert.True(Driver.IsElementPresent(_radOtherActive), $"The Other radio button is not selected");

            Driver.ClickItem(_radJet2HolidaysCustomer);
            Assert.True(Driver.IsElementPresent(_radJet2HolidaysCustomerActive),
                $"The Jet 2 Holidays Customer radio button is not selected");
            Assert.True(!Driver.IsElementPresent(_radJet2HolidaysColleagueActive), $"The Other user radio button is selected");
            Assert.True(!Driver.IsElementPresent(_radOtherActive), $"The Other radio button is selected");
        }

        internal void SelectCustomerFromDrpDwn(string passengerName)
        {
            Driver.SelectDropDownOption(_drpDwnCustomers, passengerName);
        }

        internal void ClicktheNextBtn()
        {
            Driver.ClickItem(_btnNext);
        }

        internal void VerifyOtherUserOptionSelection()
        {
            Assert.True(Driver.IsElementPresent(_radJet2HolidaysColleagueActive), $"The Other user radio button is not selected");
        }

        internal void SelectJet2HolidayCustomerRad()
        {
            Driver.ClickItem(_radJet2HolidaysCustomer);
            Assert.True(Driver.IsElementPresent(_radJet2HolidaysCustomerActive), $"The Jet 2 Holidays Customer radio button is not selected");
            Assert.True(!Driver.IsElementPresent(_radJet2HolidaysColleagueActive), $"The Other user radio button is selected");
            Assert.True(!Driver.IsElementPresent(_radOtherActive), $"The Other radio button is selected");
        }

        internal void VerifyJet2HolDrpDwnBtn(string status)
        {
            switch (status)
            {
                case "can":
                    Assert.True(Driver.IsElementPresent(_btnSearchForJet2HolCustomer),
                        $"The Jet 2 Holidays Customer search button is not displayed");
                    Assert.True(Driver.IsElementPresent(_drpDwnCustomers),
                        $"The Jet 2 Holidays Customer dropdown is not displayed");
                    break;
                case "can not":
                    Assert.True(!Driver.WaitUntilElementNotPresent(_btnSearchForJet2HolCustomer, 1),
                        $"The Jet 2 Holidays Customer search button is displayed");
                    Assert.True(!Driver.WaitUntilElementNotPresent(_drpDwnCustomers, 1),
                        $"The Jet 2 Holidays Customer dropdown is displayed");
                    break;
            }
        }

        public void VerifyJet2HolCustomerRadBtnTxt(string label)
        {
            var lblJet2HolidaysCustomer = Driver.GetText(_lblJet2HolidaysCustomer);
            Assert.AreEqual(label, lblJet2HolidaysCustomer,
                $"The Jet2holidays customer radio button label is incorrect");
        }

        public void VerifySelectedNameJet2HolidaysDrpDwn(string customerName)
        {
            var drpDwnSelectedTxt = Driver.FindElement(_drpDwnCustomersSelectedPassengerName).Text;
            Assert.AreEqual(customerName, drpDwnSelectedTxt,
                $"The customer selected in the Jet 2 Customer Holidays dropdown is incorrect");
        }

        public void SelectOtherRad()
        {
            Driver.ClickItem(_radOther);
            Assert.True(!Driver.IsElementPresent(_radJet2HolidaysCustomerActive), $"The Jet 2 Holidays Customer radio button is selected");
            Assert.True(!Driver.IsElementPresent(_radJet2HolidaysColleagueActive), $"The Other radio button is not selected");
            Assert.True(Driver.IsElementPresent(_radOtherActive), $"The Other radio button is selected");
        }

        public void VerifyJet2HolColleagueRadBtnTxt(string label)
        {
            var lblJet2HolidaysColleague = Driver.GetText(_lblJet2HolidaysColleague);
            Assert.AreEqual(label, lblJet2HolidaysColleague,
                $"The Jet2holidays colleague radio button label is incorrect");
        }

        public void VerifyOtherRadBtnTxt(string label)
        {
            var lblOther = Driver.GetText(_lblOther);
            Assert.AreEqual(label, lblOther,
                $"The Other radio button label is incorrect");
        }

        public void SelectJet2HolidayColleagueRad()
        {
            Driver.ClickItem(_radJet2HolidaysColleague);
            Assert.True(Driver.IsElementPresent(_radJet2HolidaysColleagueActive), $"The Jet2holidays Colleague radio button is not selected");
            Assert.True(!Driver.IsElementPresent(_radJet2HolidaysCustomerActive), $"The Jet2holidays Customer radio button is selected");
            Assert.True(!Driver.IsElementPresent(_radOtherActive), $"The Other radio button is selected");
        }

        public void VerifyDepartmentDropdownDisplayed()
        {
            Driver.WaitForItem(_drpDwnDepartment);
        }


        public void SelectJet2HolidaysColleague(Table table)
        {
            var row = table.Rows[0];
            
            if (row["Departments"].Length > 0)
            {
                Driver.SelectDropDownOption(_drpDwnDepartment, row["Departments"]);
            }

            if (row["Colleagues"].Length > 0)
            {
                Driver.SelectMultiselectOption(_drpDwnColleagues, row["Colleagues"].ConvertStringIntoList());
            }
            
        }

        public void SelectDepartmentFromDropdown(string department)
        {
            Driver.SelectDropDownOption(_drpDwnDepartment, department);
        }

        public void VerifyColleaguesListOrder()
        {
            //Get actual order of colleagues
            var actualOrderOfColleagues = Driver.GetAllDropDownOptions(_drpDwnColleagues);
            var colleagueListNames = new List<string>();

            //Retreive colleagues that are not usernames
            foreach (var colleague in actualOrderOfColleagues)
            {
                if (!colleague.Contains("/"))
                {
                    colleagueListNames.Add(colleague);
                }
            }

            //Order the list of non username colleagues alphabetically
            var expectedOrderOfColleagues = colleagueListNames.OrderBy(x => x).ToList();
            var colleagueListUsernames = new List<string>();

            //Retreive colleagues that are usernames
            foreach (var assignee in actualOrderOfColleagues)
            {
                if (assignee.Contains("/"))
                {
                    colleagueListUsernames.Add(assignee);
                }
            }

            //Order the list of non username colleagues alphabetically
            var orderedColleagueListUsernames = colleagueListUsernames.OrderBy(x => x).ToList();

            //Add the username list to bottom of the non username list
            expectedOrderOfColleagues.AddRange(orderedColleagueListUsernames);

            CollectionAssert.AreEqual(actualOrderOfColleagues, expectedOrderOfColleagues, "The order of colleagues in the colleagues dropdown is incorrect");
        }

        public void SelectColleagueFromDropdown(string colleague)
        {
            Driver.SelectDropDownOption(_drpDwnColleagues, colleague);
        }

        public void VerifySelectedJet2HolidaysColleague(Table table)
        {
            var row = table.Rows[0];
            
            if (row["Departments"].Length > 0)
            {
                var actualDepartment = Driver.GetSelectedDropDownOption(_drpDwnDepartment);
                var expectedDepartment = row["Departments"];
                Assert.AreEqual(actualDepartment, expectedDepartment, $"The selected depertment is {actualDepartment} and should be {expectedDepartment}");
            }

            if (row["Colleagues"].Length > 0)
            {
                var actualColleague = Driver.GetSelectedDropDownOption(_drpDwnColleagues);
                var expectedColleague = row["Colleagues"];
                Assert.AreEqual(actualColleague, expectedColleague, $"The selected colleague is {actualColleague} and should be {expectedColleague}");
            }
        }

        public void VerifyColleaguesDropdownEnabled()
        {
            Assert.IsTrue(Driver.IsElementEnabled(_drpDwnColleagues), "The reps list box is disabled.");
        }

        public void VerifyColleaguesDropdownDisabled()
        {
            Assert.IsTrue(Driver.WaitForItem(_drpDwnColleaguesDisabled), "The reps list box is enabled.");
        }

        public void EnterColleaguePhoneNumber(string phoneNumber)
        {
            Assert.IsTrue(Driver.WaitForItem(_inputColleaguesPhoneNumber), "The Jet2holidays colleague phone number is not displayed");
            Driver.EnterText(_inputColleaguesPhoneNumber, phoneNumber);
        }

        public void EnterColleagueEmailAddress(string emailAddress)
        {
            Assert.IsTrue(Driver.WaitForItem(_inputColleaguesEmailAddress), "The Jet2holidays colleague email address is not displayed");
            Driver.EnterText(_inputColleaguesEmailAddress, emailAddress);
        }

        public void EnterCustomersPhoneNumber(string phoneNumber)
        {
            Assert.IsTrue(Driver.WaitForItem(_inputCustomersPhoneNumber), "The Jet2holidays customer phone number is not displayed");
            Driver.EnterText(_inputCustomersPhoneNumber, phoneNumber);
        }

        public void EnterCustomersRoomNumber(string roomNumber)
        {
            Assert.IsTrue(Driver.WaitForItem(_inputCustomersRoomNumber), "The Jet2holidays customer room number is not displayed");
            Driver.EnterText(_inputCustomersRoomNumber, roomNumber);
        }

        public void VerifyFreeTypeDisplayed()
        {
            Assert.True(Driver.WaitForItem(_inputFreeType), "The free type field is not displayed");
        }

        public void EnterOtherOptionFreeText(Table table)
        {
            var row = table.Rows[0];
            var text = row["Other Text"];
            Driver.EnterText(_inputFreeType, text);
        }

        public void VerifyOtherOptionFreeText(Table table)
        {
            var row = table.Rows[0];
            var expectedtext = row["Other Text"];
            var actualtext = Driver.GetInputBoxValue(_inputFreeType);
            Assert.AreEqual(expectedtext, actualtext, "The free type text is incorrect on the reported by page");
        }

        public void VerifyCharacterLimitFreeText(int charlimit)
        {
            Assert.True(Driver.CheckMaxLength(_inputFreeType, charlimit));
        }

        public void EnterOtherPhoneNumber(string phoneNumber)
        {
            Assert.IsTrue(Driver.WaitForItem(_inputOtherPhoneNumber), "The other phone number is not displayed");
            Driver.EnterText(_inputOtherPhoneNumber, phoneNumber);
        }

        public void EnterOtherEmailAddress(string emailAddress)
        {
            Assert.IsTrue(Driver.WaitForItem(_inputOtherEmailAddress), "The other email address is not displayed");
            Driver.EnterText(_inputOtherEmailAddress, emailAddress);
        }

        public void DeselectColleagueDepartmentDropdown()
        {
            Driver.DeselectDropDownOption(_drpDwnDepartment);
        }

        public void ClearColleagueEmailAddress()
        {
            Driver.WaitUntilTextPresent(_inputColleaguesEmailAddress, 1);
            Driver.ClearUsingBackspace(_inputColleaguesEmailAddress);
        }

        public void ClearColleaguePhoneNumber()
        {
            Driver.WaitUntilTextPresent(_inputColleaguesPhoneNumber, 1);
            Driver.ClearUsingBackspace(_inputColleaguesPhoneNumber);
        }

        public void DeselectCustomerDropdown()
        {
            Driver.DeselectDropDownOption(_drpDwnCustomers);
        }

        public void ClearCustomerPhoneNumber()
        {
            Driver.WaitUntilTextPresent(_inputCustomersPhoneNumber, 1);
            Driver.ClearUsingBackspace(_inputCustomersPhoneNumber);
        }

        public void ClearCustomerRoomNumber()
        {
            Driver.WaitUntilTextPresent(_inputCustomersRoomNumber, 1);
            Driver.ClearUsingBackspace(_inputCustomersRoomNumber);
        }

        public void VerifyJet2HolColleagueOptionPreselected()
        {
            Assert.True(Driver.IsElementPresent(_radJet2HolidaysColleagueActive),
                $"The Jet 2 Holidays Colleague radio button is not selected");
        }

        public void VerifyValidationMessage(string message)
        {
            Assert.AreEqual(message, Driver.GetText(_validationMessage),
                "The validation message is not being displayed.");
        }
        
        public void VerifyValidationMessageOther(string message)
        {
            Assert.AreEqual(message, Driver.GetText(_validationMessageOther),
                "The validation message is not being displayed.");
        }

        public void VerifyDepartmentInputDisplayed()
        {
            Driver.WaitForItem(_inputColleaguesDepartmentName);
        }

        public void EnterColleagueDepartmentName(string departmentName)
        {
            Driver.EnterText(_inputColleaguesDepartmentName, departmentName);
        }
    }
}