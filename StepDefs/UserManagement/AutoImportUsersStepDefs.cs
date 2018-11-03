using J2BIOverseasOps.Pages.UserManagement;
using log4net;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.StepDefs.UserManagement
{
    [Binding]
    public sealed class AutoImportUsersStepDefs : BaseStepDefs
    {
        private readonly AutoImportUsers _autoImportUsers;

        public AutoImportUsersStepDefs(IWebDriver driver, ILog log, IRunData rundata) : base(driver, log)
        {
            _autoImportUsers=new AutoImportUsers(driver,log,rundata);
        }

        [Given(@"I get the list of all employees from the datafeed and save in a Json file")]
        public void GivenIGetTheListOfAllEmployeesFromTheDatafeedAndSaveInAJsonFile()
        {
            _autoImportUsers.SaveExistingEmployees();
        }

        [Given(@"I add a new user to the data feed API with a rank as ""(.*)""")]
        public void GivenIAddANewUserToTheDataFeedAPIWithAJobTitleAs(string rank)
        {
            _autoImportUsers.PostNewEmployees(rank);
        }

        [Given(@"I update the Titles to roles mapping list")]
        public void GivenIUpdateTheUserToRoleMappingList()
        {
            _autoImportUsers.UpdateTitlesToRolesMapping();
        }


        [When(@"I reset the list of employees to the original data")]
        public void WhenIResetTheListOfEmployeesToTheOriginalData()
        {
            _autoImportUsers.ResetToOldEmployeesData();
        }

        [When(@"I get the expected role for the newly pushed user and save it into scenario context as ""(.*)""")]
        public void WhenIGetTheExpectedRoleForTheNewlyPushedUserAndSaveItIntoScenarioContextAs(string scenarioContextKey)
        {
            _autoImportUsers.GetExpectedRole(scenarioContextKey);
        }



    }
}
