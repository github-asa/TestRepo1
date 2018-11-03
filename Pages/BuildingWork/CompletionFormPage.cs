using J2BIOverseasOps.Extensions;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;


namespace J2BIOverseasOps.Pages.BuildingWork
{
    internal class CompletionFormPage : BuildingWorkCommon
    {
        public CompletionFormPage(IWebDriver driver, ILog log, IRunData runData) : base(driver, log, runData)
        {
        }

        private readonly By _calendarCompletionOfWork = By.XPath("//p-calendar[@id='completionOfWorksDate']");
        private readonly By _completionDateValidation = By.Id("completionOfWorksDate-validation");

        private readonly By _infoProvidedByJobTitle = By.Id("providedByRole");
        private readonly By _infoProvidedByJobTitleValidation = By.Id("providedByRole-validation");

        private readonly By _infoProvidedByName = By.Id("providedByName");
        private readonly By _infoProvidedByNameValidation = By.Id("providedByName-validation");

        private readonly By _additionalInfo = By.Id("additionalInformation");

        private readonly By _backBtn = By.XPath("//button//span[contains(text(),'Back')]");
        private readonly By _submitBtn = By.XPath("//span[contains(text(),'Submit')]");


        public void VerifyCompletionFormValidationErrorMessage(Table table)
        {

            foreach (var row in table.Rows)
            {
                var field = row["field"];
                var expectedError = row["error"];
                switch (field.ToLower())
                {
                    case "info provided by name":
                        VerifyBwFieldValidationErrorMessage(_infoProvidedByNameValidation, expectedError);
                        break;
                    case "info provided by job title":
                        VerifyBwFieldValidationErrorMessage(_infoProvidedByJobTitleValidation, expectedError);
                        break;
                    case "date of completion":
                        VerifyBwFieldValidationErrorMessage(_completionDateValidation, expectedError);
                        break;
                    default:
                        Assert.Fail($"{field} is not a valid field");
                        return;
                }
            }
        }



        public void EnterCompletionFormAnswerToGeneralQ(Table table)
        {
            foreach (var row in table.Rows)
            {
                var question = row["question"];
                var answer = row["answer"];
                switch (question.ToLower())
                {
                    case "date of completion of works":
                        Driver.ClickItem(_calendarCompletionOfWork);
                        SelectBwDateFromCalendar(_calendarCompletionOfWork, answer);
                        break;
                    case "info provided by name":
                        Driver.EnterText(_infoProvidedByName, answer);
                        break;
                    case "info provided by job title":
                        Driver.EnterText(_infoProvidedByJobTitle, answer);
                        break;
                    case "additional info to be aware of":
                        Driver.EnterText(_additionalInfo, answer);
                        break;
                    default:
                        Assert.Fail($"{question} is not a valid completion form question");
                        break;
                }

            }
        }


        public void ClickSubmitButton()
        {
            Driver.ClickItem(_submitBtn);
        }

        public void VerifyCompletionFormFieldValues(Table table)
        {
            foreach (var row in table.Rows)
            {
                var field = row["field"];
                var value = row["value"];
                switch (field.ToLower())
                {
                    case "category":
                        Assert.True(Driver.WaitForItem(By.XPath($"//div[contains(text(),'{value}')]")),
                            "Unable to find category of the BW on completion form"); //TODO TEMP
                        break;
                }
            }
        }
    }
}
    






