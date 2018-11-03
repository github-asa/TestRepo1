using System.Reflection;
using BoDi;
using log4net;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Setup
{
    [Binding]
    public class SetupLogger
    {
        private readonly IObjectContainer _container;

        public SetupLogger(IObjectContainer container)
        {
            _container = container;
        }

        [BeforeScenario]
        public void RegisterLogger()
        {
            var log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            _container.RegisterInstanceAs(log);
        }
    }
}