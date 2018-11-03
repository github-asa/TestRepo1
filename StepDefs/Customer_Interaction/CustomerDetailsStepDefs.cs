using J2BIOverseasOps.Pages.Customer_Interaction;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.Customer_Interaction
{
    [Binding]
    public sealed class CustomerDetailsStepDefs : BaseStepDefs
    {
        private readonly BookingDetailsPage _bookingDetails;

        public CustomerDetailsStepDefs(IWebDriver driver, ILog log) : base(driver, log)
        {
            _bookingDetails = new BookingDetailsPage(driver, log);
        }

        [Then(@"the customer overview section is displayed with the name, DOB, age, telephone number and 'More' icon:")]
        public void ThenTheCustomerOverviewSectionIsDisplayedWithTheNameDobAgeTelephoneNumberAndIcon(Table table)
        {
            _bookingDetails.VerifyCustomerOverviewDetails(table);
        }

        [Then(@"the lead customer name is in bold")]
        public void ThenTheLeadCustomerNameIsInBold()
        {
            _bookingDetails.VerifyLeadCustomerNameIsBold();
        }

        [Then(
            @"the customer with a birthday during there stay will have a cake icon and the day of the week displayed beside their name:")]
        public void
            ThenTheCustomerWithABirthdayDuringThereStayWillHaveACakeIconAdnTheDayOfTheWeekDisplayedBesideTheirName(
                Table table)
        {
            _bookingDetails.VerifyBirthdayDetailsAreDisplayed(table);
        }

        [When(@"I click the 'More' icon for each customer then the '(.*)' are displayed:")]
        public void WhenIClickTheIconForEachCustomerThenTheContactDetailsAreDisplayed(string header, Table table)
        {
            _bookingDetails.VerifyContactDetailsAreDisplayed(header, table);
        }
    }
}