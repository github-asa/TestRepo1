using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions.Common;
using J2BIOverseasOps.Pages.Customer_Interaction;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.Customer_Interaction
{
    [Binding]
    public sealed class NoBookingsAffectedStepDefs: BaseStepDefs
    {
        private readonly NoBookingsAffectedPage _noBookingsAffectedPage;

        public NoBookingsAffectedStepDefs(IWebDriver driver, ILog log, IRunData runData) : base(driver, log)
        {
            _noBookingsAffectedPage = new NoBookingsAffectedPage(driver, log, runData);
        }

        [Then(@"I verify the destination multiselect heading is ""(.*)"" on the no bookings affected page")]
        public void ThenIVerifyTheDestinationMultiselectHeadingIsOnTheNoBookingsAffectedPage(string destination)
        {
            _noBookingsAffectedPage.VerifyDestinationHeader(destination);
        }

        [Then(@"I verify the resort multiselect heading is ""(.*)"" on the no bookings affected page")]
        public void ThenIVerifyTheResortMultiselectHeadingIsOnTheNoBookingsAffectedPage(string resort)
        {
            _noBookingsAffectedPage.VerifyResortHeader(resort);
        }

        [Then(@"I verify the property multiselect heading is ""(.*)"" on the no bookings affected page")]
        public void ThenIVerifyThePropertyMultiselectHeadingIsOnTheNoBookingsAffectedPage(string property)
        {
            _noBookingsAffectedPage.VerifyPropertyHeader(property);
        }

        [Then(@"I verify the title of the page is ""(.*)"" on the no bookings affected page")]
        public void ThenIVerifyTheTitleOfThePageIsOnTheNoBookingsAffectedPage(string title)
        {
            _noBookingsAffectedPage.VerifyPageTitle(title);
        }

        [Then(@"I verify the sub heading is ""(.*)"" on the no bookings affected page")]
        public void ThenIVerifyTheSubHeadingIsOnTheNoBookingsAffectedPage(string subHeading)
        {
            _noBookingsAffectedPage.VerifySubHeading(subHeading);
        }

        [When(@"I retrieve all the destinations from the destination API")]
        public void WhenIRetrieveAllTheDestinationsFromTheDestinationAPI()
        {
            _noBookingsAffectedPage.GetAllDestinationsApiCall();
        }

        [Then(@"I verify that the destinations multiselect contains a list of available destinations via their IATA airport code on the bookings affected page")]
        public void ThenIVerifyThatTheDestinationsMultiselectContainsAListOfAvailableDestinationsViaTheirIATAAirportCodeOnTheBookingsAffectedPage()
        {
            _noBookingsAffectedPage.VerifyAvailableDestinations();
        }

        [Then(@"I verify that I see the IATA code once for each destination in the destinations multiselect on the no bookings affected page")]
        public void ThenIVerifyThatISeeTheIATACodeOnceForEachDestinationInTheDestinationsMultiselectOnTheNoBookingsAffectedPage()
        {
            _noBookingsAffectedPage.VerifyDestinationsUnique();
        }

        [Then(@"I verify that the destinations in the destinations multiselect are in alphabetical order by IATA code")]
        public void ThenIVerifyThatTheDestinationsInTheDestinationsMultiselectAreInAlphabeticalOrderByIATACode()
        {
            _noBookingsAffectedPage.CheckOrderOfDestinations();
        }

        [When(@"I select ""(.*)"" in the destination multiselect on the no bookings affected page")]
        public void WhenISelectTheInTheDestinationsMultiselectOnTheNoBookingsAffectedPage(string destination)
        {
            _noBookingsAffectedPage.SelectASingleDestination(destination);
        }

        [Then(@"I verify that the following destinations have been selected on the no bookings affected page:")]
        public void ThenIVerifyThatTheFollowingDestinationsHaveBeenSelectedOnTheNoBookingsAffectedPage(Table table)
        {
            _noBookingsAffectedPage.VerifySelectedDestinations(table);
        }

        [When(@"I select the following destinations from the destinations multiselect on the no bookings affected page:")]
        public void WhenISelectTheFollowingDestinationsFromTheDestinationsMultiselectOnTheNoBookingsAffectedPage(Table table)
        {
            _noBookingsAffectedPage.SelectMultipleDestinations(table);
        }

        [Given(@"I verify the resorts multiselect is disabled on the no bookings affected page")]
        [When(@"I verify the resorts multiselect is disabled on the no bookings affected page")]
        [Then(@"I verify the resorts multiselect is disabled on the no bookings affected page")]
        public void ThenIVerifyTheResortsMultiselectIsDisabledOnTheNoBookingsAffectedPage()
        {
            _noBookingsAffectedPage.VerifyResortsMultiselectDisabled();
        }

        [Then(@"I verify the resort multiselect is enabled on the no bookings affected page")]
        public void ThenIVerifyTheResortsMultiselectIsEnabledOnTheNoBookingsAffectedPage()
        {
            _noBookingsAffectedPage.VerifyResortsMultiselectEnabled();
        }

        [When(@"I deselect the following destinations from the destinations multiselect on the no bookings affected page:")]
        public void WhenIDeselectTheFollowingDestinationsFromTheDestinationsMultiselectOnTheNoBookingsAffectedPage(Table table)
        {
            _noBookingsAffectedPage.DeselectMultipleDestinations(table);
        }

        [When(@"I retrieve all the resorts from the resorts API for the destination ""(.*)""")]
        public void WhenIRetrieveAllTheResortsFromTheResortsAPIForTheDestination(string destinationName)
        {
            _noBookingsAffectedPage.GetAllResortsByDestination(destinationName);
        }

        [Then(@"I verify that the resorts multiselect contains a list of available resorts according to the selected destination on the bookings affected page")]
        public void ThenIVerifyThatTheResortsMultiselectContainsAListOfAvailableResortsAccordingToTheSelectedDestinationOnTheBookingsAffectedPage()
        {
            _noBookingsAffectedPage.VerifyAvailableResorts();
        }

        [Then(@"I verify that the resorts in the resorts multiselect are in alphabetical alphabetical order on the reported by page")]
        public void ThenIVerifyThatTheResortsInTheResortsMultiselectAreInAlphabeticalAlphabeticalOrderOnTheReportedByPage()
        {
            _noBookingsAffectedPage.CheckOrderOfResorts();
        }

        [When(@"I select the following resorts from the resorts multiselect on the no bookings affected page:")]
        public void WhenISelectTheFollowingResortsFromTheResortsMultiselectOnTheNoBookingsAffectedPage(Table table)
        {
            _noBookingsAffectedPage.SelectMultipleResorts(table);
        }

        [Then(@"I verify that the following resorts have been selected on the no bookings affected page:")]
        public void ThenIVerifyThatTheFollowingResortsHaveBeenSelectedOnTheNoBookingsAffectedPage(Table table)
        {
            _noBookingsAffectedPage.VerifySelectedResorts(table);
        }

        [Then(@"I verify the property multiselect is enabled on the no bookings affected page")]
        public void ThenIVerifyThePropertyMultiselectIsEnabledOnTheNoBookingsAffectedPage()
        {
            _noBookingsAffectedPage.VerifyPropertyMultiselectEnabled();
        }

        [Then(@"I verify the property multiselect is disabled on the no bookings affected page")]
        public void ThenIVerifyThePropertyMultiselectIsDisabledOnTheNoBookingsAffectedPage()
        {
            _noBookingsAffectedPage.VerifyPropertyMultiselectDisabled();
        }

        [When(@"I retrieve all the properties from the properties API for the destination ""(.*)""")]
        public void WhenIRetrieveAllThePropertiesFromThePropertiesAPIForTheDestination(string destinationName)
        {
            _noBookingsAffectedPage.GetAllPropertiesByDestination(destinationName);
        }

        [Then(@"I verify that the property multiselect contains a list of available properties according to the selected destination on the no bookings affected page")]
        public void ThenIVerifyThatThePropertyMultiselectContainsAListOfAvailablePropertiesAccordingToTheSelectedDestinationOnTheBookingsAffectedPage()
        {
            _noBookingsAffectedPage.VerifyAvailableProperties();
        }

        [Then(@"I verify that the propeties in the property multiselect are in alphabetical alphabetical order on the reported by page")]
        public void ThenIVerifyThatThePropetiesInThePropertyMultiselectAreInAlphabeticalAlphabeticalOrderOnTheReportedByPage()
        {
            _noBookingsAffectedPage.CheckOrderOfProperties();
        }

        [When(@"I select the following properties from the property multiselect on the no bookings affected page:")]
        public void WhenISelectTheFollowingPropertiesFromThePropertyMultiselectOnTheNoBookingsAffectedPage(Table table)
        {
            _noBookingsAffectedPage.SelectMultipleProperties(table);
        }

        [Then(@"I verify that the following properties have been selected on the no bookings affected page:")]
        public void ThenIVerifyThatTheFollowingPropertiesHaveBeenSelectedOnTheNoBookingsAffectedPage(Table table)
        {
            _noBookingsAffectedPage.VerifySelectedProperties(table);
        }

        [When(@"I retrieve all the properties from the properties API for the resort '(.*)'")]
        public void WhenIRetrieveAllThePropertiesFromThePropertiesAPIForTheDestinationAndResort(string resort)
        {
            _noBookingsAffectedPage.GetallPropertiesByResort(resort);
        }

        [Then(@"I verify that the property multiselect contains a list of available properties according to the selected destination and resort on the no bookings affected page")]
        public void ThenIVerifyThatThePropertyMultiselectContainsAListOfAvailablePropertiesAccordingToTheSelectedDestinationAndResortOnTheNoBookingsAffectedPage()
        {
            _noBookingsAffectedPage.VerifyAvailablePropertiesByDestinationAndResort();
        }

        [When(@"I deselect the following resorts from the resorts multiselect on the no bookings affected page:")]
        public void WhenIDeselectTheFollowingResortsFromTheResortsMultiselectOnTheNoBookingsAffectedPage(Table table)
        {
            _noBookingsAffectedPage.DeselectMultipleResorts(table);
        }

        [When(@"I click the continue button on the no bookings affected page")]
        public void WhenIClickTheContinueButtonOnTheNoBookingsAffectedPage()
        {
            _noBookingsAffectedPage.ClickContinueButton();
        }


        [Then(@"a validation message '(.*)' is displayed on the no bookings affected page")]
        public void ThenAValidationMessageIsDisplayedOnTheNoBookingsAffectedPage(string message)
        {
            _noBookingsAffectedPage.VerifyValidationMessage(message);
        }

        [When(@"I verify the default destinations displayed on the no bookings affected page are:")]
        public void WhenIVerifyTheDefaultDestinationsDisplayedOnTheNoBookingsAffectedPageAre(Table table)
        {
            _noBookingsAffectedPage.VerifyDefaultDestinations(table);
        }

    }
}

