using BoDi;
using log4net.Config;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Setup
{
    [Binding]
    public class LoadConfig
    {
        private readonly IObjectContainer _container;

        public LoadConfig(IObjectContainer container)
        {
            _container = container;
        }

        [BeforeScenario(Order = 0)]
        public void Load()
        {
            XmlConfigurator.Configure();
            var data = new RunData();
            _container.RegisterInstanceAs<IRunData>(data);
        }
    }
}