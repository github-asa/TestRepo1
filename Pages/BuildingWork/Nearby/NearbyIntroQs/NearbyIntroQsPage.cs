using System.Threading;
using J2BIOverseasOps.Extensions;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;


namespace J2BIOverseasOps.Pages.BuildingWork.Nearby.NearbyIntroQs
{
    internal class NearbyIntroQsPage : BuildingWorkCommon
    {
        private readonly ApiCalls _apiCall;

        //details of the work taking place
        private readonly By _detailsOfWorkTakingPlace = By.XPath("//textarea[@id='detailsAboutTheWork']");

        //distance of the work from our property
        private readonly By _approxDistanceOfWorkTakingPlace = By.XPath("//textarea[@id='workDistance']");

        //where is the building work taking place
        private readonly By _whereIsTheBuildingWorkTakingPlace = By.XPath("//p-dropdown[@id='propertyType']");

        //details of other property type
        private readonly By _detailsOfOtherPropertyType = By.XPath("//textarea[@id='otherPropertyType']");

        //name of the beach
        private readonly By _pleaseGiveTheNameOfTheBeach = By.XPath("//input[@id='beachName']");

        //name of the property
        private readonly By _whatIsTheNameOfTheProperty = By.XPath("//input[@id='propertyName']");

        //renovation build type
        private readonly By _renovationBuildType = By.XPath(" //span[text()='Renovation']");

        //renovation build type
        private readonly By _newBuildBuildType = By.XPath(" //span[text()='New Build']");

        //mandatory validation error messages
        private readonly By _workDetailsValidation = By.Id("detailsAboutTheWork-validation");
        private readonly By _workDistanceValidation = By.Id("workDistance-validation");
        private readonly By _propertyTypeValidation = By.Id("propertyType-validation");
        private readonly By _detailsOfOtherPropertyTypeValidation = By.Id("otherPropertyType-validation");
        private readonly By _beachNameValidation = By.Id("beachName-validation");
        private readonly By _propertyNameValidation = By.Id("propertyName-validation");

        public NearbyIntroQsPage(IWebDriver driver, ILog log, IRunData rundata) : base(driver, log, rundata)
        {
            _apiCall = new ApiCalls(rundata);

        }

        public void VerifyFieldsDisplayedOrNot(string displayedOrNot, Table table)
        {
            var expectedState = displayedOrNot == "displayed";
            foreach (var row in table.Rows)
            {
                var expectedField = row["field"];
                By element = null;
                switch (expectedField.ToLower())
                {
                    case "details of the work taking place":
                        element = _detailsOfWorkTakingPlace;
                        break;
                    case "approx. distance work is taking place from our property":
                        element = _approxDistanceOfWorkTakingPlace;
                        break;
                    case "where is the building work taking place":
                        element = _whereIsTheBuildingWorkTakingPlace;
                        break;
                    case "please give details":
                        element = _detailsOfOtherPropertyType;
                        break;
                    default:
                        Assert.Fail($"{expectedField} is not a valid field");
                        break;
                }

                Assert.AreEqual(Driver.WaitForItem(element, 1), expectedState);
            }
        }

        public void EnterNearbyIntroQsAnswers(Table table)
        {
            foreach (var row in table.Rows)
            {
                var question = row["question"];
                var answer = row["answer"];

                switch (question.ToLower())
                {
                    case "details of the work taking place":
                        Driver.EnterText(_detailsOfWorkTakingPlace, (answer));
                        break;
                    case "approx. distance work is taking place from our property":
                        Driver.EnterText(_approxDistanceOfWorkTakingPlace, (answer));
                        break;
                    case "where is the building work taking place":
                        Driver.SelectDropDownOption(_whereIsTheBuildingWorkTakingPlace, (answer));
                        break;
                    case "please give details":
                        Driver.EnterText(_detailsOfOtherPropertyType, (answer));
                        break;
                    case "please give the name of the beach":
                        Driver.EnterText(_pleaseGiveTheNameOfTheBeach, (answer));
                        break;
                    case "what is the name of the property":
                        Driver.EnterText(_whatIsTheNameOfTheProperty, (answer));
                        break;
                    case "please specify the build type":
                        ChooseBuildType(answer);
                        break;
                    default:
                        Assert.Fail($"{question} is not a valid field");
                        break;
                }
            }
        }

        private void ChooseBuildType(string buildType)
        {
            //if (buildType == "Renovation")
            //    Driver.ClickItem(_renovationBuildType);
            //else Driver.ClickItem(_newBuildBuildType);
            switch (buildType)
            {
                case "Renovation":
                    Driver.ClickItem(_renovationBuildType);
                    break;
                case "New Build":
                    Driver.ClickItem(_newBuildBuildType);
                    break;
                default:
                    Assert.Fail($"{buildType} is not a valid field");
                    break;
            }
        }

        public void IntroQValidationErrorMessageIsDisplayed(Table table)
        {
            foreach (var row in table.Rows)
            {
                var field = row["field"];
                var expectedError = row["error"];
                switch (field.ToLower())
                {
                    case "details of the work taking place":
                        VerifyBwFieldValidationErrorMessage(_workDetailsValidation, expectedError);
                        break;
                    case "approx. distance work is taking place from our property":
                        VerifyBwFieldValidationErrorMessage(_workDistanceValidation, expectedError);
                        break;
                    case "where is the building work taking place":
                        VerifyBwFieldValidationErrorMessage(_propertyTypeValidation, expectedError);
                        break;
                    case "please give details":
                        VerifyBwFieldValidationErrorMessage(_detailsOfOtherPropertyTypeValidation, expectedError);
                        break;
                    case "please give the name of the beach":
                        VerifyBwFieldValidationErrorMessage(_beachNameValidation, expectedError);
                        break;
                    case "what is the name of the property":
                        VerifyBwFieldValidationErrorMessage(_propertyNameValidation, expectedError);
                        break;
                    default:
                        Assert.Fail($"{field} is not a valid field");
                        return;

                }


            }

        }

        public void IntroQValidationErrorMessageIsNotDisplayed(Table table)
        {
            foreach (var row in table.Rows)
            {
                var field = row["field"];

                switch (field.ToLower())
                {
                    case "details of the work taking place":
                        Assert.True(!Driver.WaitForItem(_workDetailsValidation, 1),
                            $"Was able to find mandatory error message for {field} when expecting it not to be there");
                        break;
                    case "approx. distance work is taking place from our property":
                        Assert.True(!Driver.WaitForItem(_workDistanceValidation, 1),
                            $"Was able to find mandatory error message for {field} when expecting it not to be there");
                        break;
                    case "where is the building work taking place":
                        Assert.True(!Driver.WaitForItem(_propertyTypeValidation, 1),
                            $"Was able to find mandatory error message for {field} when expecting it not to be there");
                        break;
                    case "please give details":
                        Assert.True(!Driver.WaitForItem(_detailsOfOtherPropertyTypeValidation, 1),
                            $"Was able to find mandatory error message for {field} when expecting it not to be there");
                        break;
                    case "please give the name of the beach":
                        Assert.True(!Driver.WaitForItem(_beachNameValidation, 1),
                            $"Was able to find mandatory error message for {field} when expecting it not to be there");
                        break;
                    case "what is the name of the property":
                        Assert.True(!Driver.WaitForItem(_propertyNameValidation, 1),
                            $"Was able to find mandatory error message for {field} when expecting it not to be there");
                        break;
                    default:
                        Assert.Fail($"{field} is not a valid field");
                        return;
                }
            }
        }


    }
}
