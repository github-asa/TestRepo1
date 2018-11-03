using System.Collections.Generic;
using System.Linq;
using J2BIOverseasOps.Extensions;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using static J2BIOverseasOps.Models.PropertiesApi;

namespace J2BIOverseasOps.Pages.Customer_Interaction
{
    internal class NoBookingsAffectedPage : BasePage
    {
        private readonly IRunData _rundData;

        //Headers
        private readonly By _destinationHeader = By.Id("destination-heading");
        private readonly By _resortHeader = By.Id("resort-heading");
        private readonly By _propertyHeader = By.Id("property-heading");
        private readonly By _pageTitle = By.Id("eros-page-heading");
        private readonly By _subHeading = By.Id("no-bookings-affected-overview");

        //Multiselects
        private readonly By _destinationMultiselect = By.CssSelector("[destinationslist]");
        private readonly By _resortMultiselect = By.CssSelector("[resortslist]");
        private readonly By _resortMultiselectDisabled = By.CssSelector("[resortslist] .ui-state-disabled");
        private readonly By _propertyMultiselect = By.CssSelector("[propertieslist]");
        private readonly By _propertyMultiselectDisabled = By.CssSelector("[propertieslist] .ui-state-disabled");

        //buttons
        private readonly By _buttonContinue = By.CssSelector("#no-bookings-affected-continue-button");
        private readonly By _buttonContinueDisabled = By.CssSelector("#no-bookings-affected-continue-button [disabled]");
        
        //Messages
        private readonly By _validationMessage = By.CssSelector("div:not([hidden]) p-message .ui-message-text");

        private readonly ApiCalls apiCalls;
        public NoBookingsAffectedPage(IWebDriver driver, ILog log, IRunData runData) : base(driver, log)
        {
            _rundData = runData;
            apiCalls=new ApiCalls(runData);
        }

        public void VerifyDestinationHeader(string destination)
        {
            var actualDestinationHeader = Driver.GetText(_destinationHeader);
            Assert.AreEqual(destination,actualDestinationHeader, $"The destination header is {actualDestinationHeader} instead of {destination}");
        }

        public void VerifyResortHeader(string resort)
        {
            var actualResortHeader = Driver.GetText(_resortHeader);
            Assert.AreEqual(actualResortHeader, resort, $"The resort header is {actualResortHeader} instead of {resort}");
        }

        public void VerifyPropertyHeader(string property)
        {
            var actualPropertyHeader = Driver.GetText(_propertyHeader);
            Assert.AreEqual(actualPropertyHeader, property, $"The property header is {actualPropertyHeader} instead of {property}");
        }

        public void VerifyPageTitle(string title)
        {
            var actualTitle = Driver.GetText(_pageTitle);
            Assert.AreEqual(actualTitle, title, $"The page title is {actualTitle} instead of {title}");
        }

        public void VerifySubHeading(string subHeading)
        {
            var actualSubHeading = Driver.GetText(_subHeading);
            Assert.AreEqual(actualSubHeading, subHeading, $"The page sub heading is {actualSubHeading} instead of {subHeading}");
        }

        public void GetAllDestinationsApiCall()
        {
            var dest = apiCalls.GetListOfDestinations();
            ScenarioContext.Current["AllDestinationsContent"] = dest;
            var allDestinations = ObtainIataCodeList(dest);
            ScenarioContext.Current["AllDestinations"] = allDestinations;
        }

        public List<string> ObtainIataCodeList(List<Destination> destinations)
        {
            var allDestinations = new List<string>();

            foreach (var destination in destinations)
            {
            allDestinations.Add($"{destination.iataCode}");
            }
            return allDestinations;
        }

        public void VerifyAvailableDestinations()
        {
            var expectedDestinations = ScenarioContext.Current.Get<List<string>>("AllDestinations").OrderBy(x => x).ToList();
            var actualDestinations = Driver.GetAllMultiselectOptions(_destinationMultiselect);

            CollectionAssert.AreEqual(expectedDestinations, actualDestinations,"The list of destinations in the destination multiselect is not correct");
        }

        public void VerifyDestinationsUnique()
        {
            var actualDestinations = Driver.GetAllMultiselectOptions(_destinationMultiselect);
            var  results = actualDestinations.CheckDuplicates();
            Assert.True(results.Item1, $"There are duplicates in the destinations list they are {results.Item2}");

        }

        public void CheckOrderOfDestinations()
        {
            //Get actual order of destinations
            var actualOrderOfDestinations = Driver.GetAllMultiselectOptions(_destinationMultiselect);
            
            //Order the list of destinations alphabetically
            var expectedOrderOfDestinations = actualOrderOfDestinations.OrderBy(x => x).ToList();

            //Compare actual and expected list of destinations
            CollectionAssert.AreEqual(actualOrderOfDestinations, expectedOrderOfDestinations, "The order of destinations in the destinations multiselect is incorrect");
        }

        public void SelectASingleDestination(string destination)
        {
            Driver.SelectMultiselectOption(_destinationMultiselect,destination, true);
        }

        public void VerifySelectedDestinations(Table table)
        {
            var row = table.Rows[0];
            var expectedDestinations = row["destinations"].ConvertStringIntoList();
            var selectedDestinations = Driver.GetAllSelectedMultiselectOptions(_destinationMultiselect);

            CollectionAssert.AreEqual(expectedDestinations, selectedDestinations, "The selected destinations in the destinations multiselect is incorrect");
        }
        public void SelectMultipleDestinations(Table table)
        {
            var row = table.Rows[0];
            var destinationsToSelect = row["destinations"].ConvertStringIntoList();
            Driver.SelectMultiselectOption(_destinationMultiselect, destinationsToSelect, true);
        }

        public void VerifyResortsMultiselectDisabled()
        {
            Assert.IsTrue(Driver.WaitForItem(_resortMultiselectDisabled), "The resorts multiselect is enabled");
        }

        public void VerifyResortsMultiselectEnabled()
        {
            Assert.IsTrue(Driver.WaitForItem(_resortMultiselect), "The resorts multiselect is disabled");
        }

        public void DeselectMultipleDestinations(Table table)
        {
            var row = table.Rows[0];
            var destinationsToDeselect = row["destinations"].ConvertStringIntoList();
            Driver.DeselectMultiselectOption(_destinationMultiselect, destinationsToDeselect, true);
        }

        public void GetResortsByDestinationApiCall(int destinationId)
        {
            var reso = apiCalls.GetResortsByDestination(destinationId);
            ScenarioContext.Current["AllResortsContent"] = reso;
            var allResorts = ObtainResortNameList(reso);
            ScenarioContext.Current["AllResorts"] = allResorts;
        }

        public void GetAllResortsByDestination(string destinationName)
        {
            var allDestinations = ScenarioContext.Current.Get<List<Destination>>("AllDestinationsContent");
            var destinationId = ObtainDestinationId(allDestinations, destinationName);
            GetResortsByDestinationApiCall(destinationId);
        }
        public List<string> ObtainResortNameList(List<Resort> resorts)
        {
            var allResorts = new List<string>();

            foreach (var resort in resorts)
            {
                allResorts.Add($"{resort.Name}");
            }
            return allResorts;
        }

        public int ObtainDestinationId(List<Destination> destinations, string destinationName)
        {
            var destinationId = new int();

            foreach (var destination in destinations)
            {
                if (destination.iataCode.Equals(destinationName))
                {
                    destinationId = destination.id;
                    break;
                }
            }
            return destinationId;
        }


        public void VerifyAvailableResorts()
        {
            var expectedResorts = ScenarioContext.Current.Get<List<string>>("AllResorts");
            var actualResorts = Driver.GetAllMultiselectOptions(_resortMultiselect);
            CollectionAssert.AreEqual(expectedResorts, actualResorts,"The list of resorts in the resorts multiselect is incorrect");
        }

        public void CheckOrderOfResorts()
        {
            //Get actual order of resorts
            var actualOrderOfResorts = Driver.GetAllMultiselectOptions(_resortMultiselect);

            //Order the list of resorts alphabetically
            var expectedOrderOfResorts = actualOrderOfResorts.OrderBy(x => x).ToList();

            //Compare actual and expected list of resorts
            CollectionAssert.AreEqual(actualOrderOfResorts, expectedOrderOfResorts, "The order of resorts in the resorts multiselect is incorrect");
        }

        public void SelectMultipleResorts(Table table)
        {
            var row = table.Rows[0];
            var resortsToSelect = row["resorts"].ConvertStringIntoList();
            Driver.SelectMultiselectOption(_resortMultiselect, resortsToSelect, true);
        }

        public void VerifySelectedResorts(Table table)
        {
            var row = table.Rows[0];
            var expectedResorts = row["resorts"].ConvertStringIntoList();
            var selectedResorts = Driver.GetAllSelectedMultiselectOptions(_resortMultiselect);
            CollectionAssert.AreEqual(expectedResorts, selectedResorts, "The selected resorts in the resorts multiselect is incorrect");
        }

        public void VerifyPropertyMultiselectEnabled()
        {
            Assert.IsTrue(Driver.WaitForItem(_propertyMultiselect), "The property multiselect is disabled");
        }

        public void VerifyPropertyMultiselectDisabled()
        {
            Assert.IsTrue(Driver.WaitForItem(_propertyMultiselectDisabled), "The property multiselect is enabled");
        }

        public void GetAllPropertiesByDestination(string destinationName)
        {
            var allDestinations = ScenarioContext.Current.Get<List<Destination>>("AllDestinationsContent");
            var destinationId = ObtainDestinationId(allDestinations, destinationName);
            GetPropertiesByDestinationApiCall(destinationId);
        }

        private void GetPropertiesByDestinationApiCall(int destinationId)
        {
          var prop = apiCalls.GetPropertiesByDestination(destinationId);
          ScenarioContext.Current["AllPropertiesContent"] = prop;
          var allProperties = ObtainPropertyNameList(prop);
          ScenarioContext.Current["AllProperties"] = allProperties;
        }

        private List<string> ObtainPropertyNameList(IEnumerable<Property> properties)
        {
            return properties.Select(property => $"{property.Name}").ToList();
        }

        public void VerifyAvailableProperties()
        {
            var expectedProperties = ScenarioContext.Current.Get<List<string>>("AllProperties");
            var actualProperties = Driver.GetAllMultiselectOptions(_propertyMultiselect);
            CollectionAssert.AreEqual(expectedProperties, actualProperties, "The list of properties in the properties multiselect is incorrect");
        }

        public void CheckOrderOfProperties()
        {
            //Get actual order of properties
            var actualOrderOfProperties = Driver.GetAllMultiselectOptions(_propertyMultiselect);

            //Order the list of properties alphabetically
            var expectedOrderOfProperties = actualOrderOfProperties.OrderBy(x => x).ToList();

            //Compare actual and expected list of properties
            CollectionAssert.AreEqual(expectedOrderOfProperties, actualOrderOfProperties, "The order of properties in the properties multiselect is incorrect");
        }

        public void SelectMultipleProperties(Table table)
        {
            var row = table.Rows[0];
            var propertiesToSelect = row["properties"].ConvertStringIntoList();
            Driver.SelectMultiselectOption(_propertyMultiselect, propertiesToSelect, true);
        }

        public void VerifySelectedProperties(Table table)
        {
            var row = table.Rows[0];
            var expectedProperties = row["properties"].ConvertStringIntoList();
            var selectedProperties = Driver.GetAllSelectedMultiselectOptions(_propertyMultiselect);

            CollectionAssert.AreEqual(expectedProperties, selectedProperties, "The selected properties in the properties multiselect is incorrect");
        }

        public void GetallPropertiesByResort(string resort)
        {
            var allResortsContent = ScenarioContext.Current.Get<List<Resort>>("AllResortsContent");
            var resortId = ObtainResortId(allResortsContent, resort);
            GetPropertiesByDestinationAndResortApiCall(resortId);
        }

        private int ObtainResortId(List<Resort> allResortsContent, string resort)
        {
            var resortId = new int();

            foreach (var res in allResortsContent)
            {
                if (res.Name.Equals(resort))
                {
                    resortId = res.Id;
                    break;
                }
            }
            return resortId;
        }

        private void GetPropertiesByDestinationAndResortApiCall(int resortId)
        {
            var prop = apiCalls.GetPropertiesByResort(resortId);
            ScenarioContext.Current["AllPropertiesByDestinationAndResortContent"] = prop;
            var allProperties = ObtainPropertyNameList(prop);
            ScenarioContext.Current["AllPropertiesByDestinationAndResort"] = allProperties;
        }

        public void VerifyAvailablePropertiesByDestinationAndResort()
        {
            var expectedProperties = ScenarioContext.Current.Get<List<string>>("AllPropertiesByDestinationAndResort");
            var actualProperties = Driver.GetAllMultiselectOptions(_propertyMultiselect);
            CollectionAssert.AreEqual(expectedProperties, actualProperties, "The list of properties in the properties multiselect is incorrect");
        }

        public void DeselectMultipleResorts(Table table)
        {
            var row = table.Rows[0];
            var resortsToDeselect = row["resorts"].ConvertStringIntoList();
            Driver.DeselectMultiselectOption(_resortMultiselect, resortsToDeselect, true);
        }

        public void ClickContinueButton()
        {
            Driver.ClickItem(_buttonContinue);
        }

        public void VerifyValidationMessage(string message)
        {
            Assert.AreEqual(message, Driver.GetText(_validationMessage),
                "The validation message is not being displayed.");
        }

        public void VerifyDefaultDestinations(Table table)
        {
            var row = table.Rows[0];
            var defaultDestination = row["destinations"];
            VerifyAvialableDestinations(defaultDestination);
        }


        internal void VerifyAvialableDestinations(string expectedDestinations)
        {
            var actualDestinations = Driver.GetAllMultiselectOptions(_destinationMultiselect);
            var expectedDestinastionsList = expectedDestinations.ConvertStringIntoList().OrderBy(x => x);
            CollectionAssert.AreEqual(actualDestinations, expectedDestinastionsList, "The selected destinations in the destinations multiselect are incorrect");
        }
    }
}
