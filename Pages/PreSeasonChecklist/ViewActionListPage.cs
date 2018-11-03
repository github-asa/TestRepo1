using J2BIOverseasOps.Extensions;
using log4net;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Pages.PreSeasonChecklist
{
    public class ViewActionListPage : BasePage
    {
        private readonly By _actionTableRows = By.CssSelector("table tr td");

        private int _index;

        public ViewActionListPage(IWebDriver driver, ILog log) : base(driver, log)
        {
        }

        private By ActionType => By.CssSelector($"#actions-list-action-type-{_index}");
        private By ActionName => By.CssSelector($"#actions-list-action-name-{_index}");
        private By ActionNames => By.CssSelector($"[id*=actions-list-action-name-]");
        private By EditActionButton => By.CssSelector($"#edit-actions-button-{_index}");

        public void VerifyActionsList()
        {
            Assert.IsTrue(Driver.WaitForItem(_actionTableRows), "The actions table is not displayed");
            var propertyId = ScenarioContext.Current.Get<string>("PropertyId");
            var expectedActionName = $"PropertyId-{propertyId}";
            _index = GetActionIndex(expectedActionName);
            var actionName = Driver.GetText(ActionName);
            var actionType = Driver.GetText(ActionType);
            var viewActionLink = Driver.GetText(EditActionButton);

            Assert.AreEqual("Overseas Pre Season Checklist Fail",
                actionType, $"The action type is not as expected for row {_index + 1}");
            Assert.AreEqual(expectedActionName, actionName,
                $"The action name is not as expected for row {_index + 1}");
            Assert.AreEqual("Edit", viewActionLink,
                $"The edit action link is not as expected for row {_index + 1}");
        }

        private int GetActionIndex(string actionName)
        {
            Driver.WaitForItem(ActionNames);
            var actions = Driver.FindElements(ActionNames);

            var i = 0;
            foreach (var action in actions)
            {
                if (actionName.Equals(Driver.GetText(action)))
                {
                    _index = actions.IndexOf(action);
                    break;
                }

                i++;
            }

            return i;
        }

        public void ClickEditActionButton()
        {
            var propertyId = ScenarioContext.Current.Get<string>("PropertyId");
            var actionName = $"PropertyId-{propertyId}";
            _index = GetActionIndex(actionName);
            Driver.ClickItem(EditActionButton);
        }

        public void VerifyActionListDoesNotContainAction()
        {



            Assert.IsTrue(Driver.WaitForItem(_actionTableRows), "The actions table is not displayed");
            var propertyId = ScenarioContext.Current.Get<string>("PropertyId");
            var expectedActionName = $"PropertyId-{propertyId}";

            Driver.WaitForItem(ActionNames);
            var actions = Driver.FindElements(ActionNames);

            var created = false;
            foreach (var action in actions)
            {
                if (expectedActionName.Equals(Driver.GetText(action)))
                {
                    created = true;
                    break;
                }
            }
            Assert.IsFalse(created, "An Action has been created when not expected.");
        }
    }
}