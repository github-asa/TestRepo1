using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Setup
{
    [Binding]
    public class TearDownWebDriver
    {
        private readonly IWebDriver _driver;

        public TearDownWebDriver(IWebDriver driver)
        {
            _driver = driver;
        }

        [AfterScenario]
        public void DisposeDriver()
        {
            _driver?.Dispose();
        }
    }
}