using log4net;
using OpenQA.Selenium;

namespace J2BIOverseasOps.StepDefs
{
	public class BaseStepDefs
	{
		protected IWebDriver Driver;
		protected ILog Log;

		public BaseStepDefs(IWebDriver driver, ILog log)
		{
			Driver = driver;
			Log = log;
		}
	}
}

