using System.Collections.Generic;
using System.Linq;
using J2BIOverseasOps.Extensions;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Pages.BuildingWork.Roadworks.RoadworksIntroQs
{
    internal class RoadworksIntroQsPage : BuildingWorkCommon
    {
        //approx distance work is taking place
        private readonly By _approxDistanceWorkTakingPlace = By.XPath("//input[@id='distanceFromProperty']");

        // does work affect TRANSFER time  
        private readonly By _doesWorkAffectTransferTime = By.XPath("//p-selectbutton[@id='affectTransferTime']");
        private readonly By _doesWorkAffectTransferTimeDetails = By.XPath("//textarea[@id='newTransferTimeDetails']");
        private readonly By _isAnErrataRequired = By.XPath("//p-selectbutton[@id='transferTimeErrataRequired']");

        // does work affect vehicle access
        private readonly By _workAffectVehicleAccess = By.XPath("//p-selectbutton[@id='affectVehicleAccess']");
        private readonly By _isThereAnAlternativeTransferDropOffPoint = By.XPath("//p-selectbutton[@id='alternativeDropOff']");
        private readonly By _approxDistanceFromAlternateTransfer = By.XPath("//input[@id='alternateTransferDistance']");
        private readonly By _willThereBeAssistanceWithTheLuggage = By.XPath("//p-selectbutton[@id='assistanceWithLuggage']");
        private readonly By _willThereBeAssistanceWithTheLuggageDetails = By.XPath("//textarea[@id='luggageAssistanceAdditionalDetails']");

        // what is the extent of the roadworks
        private readonly By _whatIsTheExtentOfTheRoadworks = By.XPath("//p-selectbutton[@id='extentOfTheRoadworks']");
        
        //is the road open closed
        private readonly By _isTheRoadOpenOrClosedBy = By.XPath("//p-selectbutton[@id='isTheRoadClosed']");
        private readonly By _whenIsTheRoadClosedFrom = By.XPath("//input[@name='roadClosedFrom']");
        private readonly By _whenIsTheRoadClosedUntil = By.XPath("//input[@name='roadClosedUntil']");
        private readonly ApiCalls _apiCall;


        public RoadworksIntroQsPage(IWebDriver driver, ILog log, IRunData rundata) : base(driver, log, rundata)
        {
            _apiCall = new ApiCalls(rundata);

        }

        public void EnterDates(Table table)
        {
            foreach (var row in table.Rows)
            {
                var question = row["question"];
                var answer = row["answer"];
                EnterRoadworksDates(question, answer);
            }
        }

        public void EnterRoadworksIntroQsAnswers(Table table)
        {
            foreach (var row in table.Rows)
            {
                var question = row["question"];
                var answer = row["answer"];

                switch (question.ToLower())
                {
                    case "approx distance work taking place":
                        Driver.EnterText(_approxDistanceWorkTakingPlace, (answer));
                        break;
                    case "does the work affect transfer time":
                        Driver.ClickPSelectOption(_doesWorkAffectTransferTime, (answer));
                        break;
                    case "does the work affect transfer time details":
                        Driver.EnterText(_doesWorkAffectTransferTimeDetails, (answer));
                        break;
                    case "is an errata required":
                        Driver.ClickPSelectOption(_isAnErrataRequired, (answer));
                        break;
                    case "does the work affect vehicle access":
                        Driver.ClickPSelectOption(_workAffectVehicleAccess, answer);
                        break;
                    case "is there an alternative transfer drop off point":
                        Driver.ClickPSelectOption(_isThereAnAlternativeTransferDropOffPoint, (answer));
                        break;
                    case "approx distance from alternate transfer":
                        Driver.EnterText(_approxDistanceFromAlternateTransfer, (answer));
                        break;
                    case "will there be assistance with the luggage":
                        Driver.ClickPSelectOption(_willThereBeAssistanceWithTheLuggage, (answer));
                        break;
                    case "will there be assistance with the luggage details":
                        Driver.EnterText(_willThereBeAssistanceWithTheLuggageDetails, (answer));
                        break;
                    case "what is the extent of the roadworks":
                        Driver.ClickPSelectOption(_whatIsTheExtentOfTheRoadworks, (answer));
                        break;
                    case "is the road open or closed":
                        Driver.ClickPSelectOption(_isTheRoadOpenOrClosedBy, (answer));
                        break;
                    case "when is the road closed":
                        Driver.ClickPSelectOption(_whenIsTheRoadClosedFrom, (answer));
                        Driver.ClickPSelectOption(_whenIsTheRoadClosedUntil, (answer));
                        break;

                    default:
                        Assert.Fail($"{question} is not a valid field");
                        break;
                }
            }
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
                    case "approx distance work taking place":
                        element = _approxDistanceWorkTakingPlace;
                        break;
                    case "does the work affect transfer time":
                        element = _doesWorkAffectTransferTime;
                        break;
                    case "does the work affect transfer time details":
                        element = _doesWorkAffectTransferTimeDetails;
                        break;
                    case "is an errata required":
                        element = _isAnErrataRequired;
                        break;
                    case "does the work affect vehicle access":
                        element = _workAffectVehicleAccess;
                        break;
                    case "is there an alternative transfer drop off point":
                        element = _isThereAnAlternativeTransferDropOffPoint;
                        break;
                    case "approx distance from alternate transfer":
                        element = _approxDistanceFromAlternateTransfer;
                        break;
                    case "will there be assistance with the luggage":
                        element = _willThereBeAssistanceWithTheLuggage;
                        break;
                    case "will there be assistance with the luggage details":
                        element = _willThereBeAssistanceWithTheLuggageDetails;
                        break;
                    case "what is the extent of the roadworks":
                        element = _whatIsTheExtentOfTheRoadworks;
                        break;
                    case "is the road open or closed":
                        element = _isTheRoadOpenOrClosedBy;
                        break;
                    case "when is the road closed":
                        element = _whenIsTheRoadClosedFrom;
                        element = _whenIsTheRoadClosedUntil;
                        break;
                    default:
                        Assert.Fail($"{expectedField} is not a valid field");
                        break;
                }

                Assert.AreEqual(Driver.WaitForItem(element, 1), expectedState);
            }
        }



        private void EnterRoadworksDates(string question, string answer)
        {
            switch (question.ToLower())
            {
                case "when is the road closed":
                    Driver.ClickPSelectOption(_whenIsTheRoadClosedFrom, (answer));
                    Driver.ClickPSelectOption(_whenIsTheRoadClosedUntil, (answer));
                    break;
                default:
                    Assert.Fail($"{question} is not a valid general question");
                    return;
            }
        }

    }

}



      