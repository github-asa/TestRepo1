using System;
using System.IO;
using System.Linq;
using J2BIOverseasOps.Extensions;
using log4net;
using log4net.Appender;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace J2BIOverseasOps.Setup
{
    [Binding]
    public class GenerateTestAttachments
    {
        private readonly IWebDriver _driver;
        private readonly ILog _log;

        public GenerateTestAttachments(IWebDriver driver, ILog log)
        {
            _driver = driver;
            _log = log;
        }

        [AfterScenario(Order = 1)]
        public void GenerateScreenShots()
        {
            try
            {
                if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Failed)
                    return;

                var outputFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Output");
                Directory.CreateDirectory(outputFolderPath);

                var timeStampedName =
                    (TestContext.CurrentContext.Test.Name + "_" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss.fff"))
                    .SanitiseString();

                var sourceFilePath = Path.Combine(outputFolderPath, timeStampedName + ".html");
                File.WriteAllText(sourceFilePath, _driver.Url + Environment.NewLine + Environment.NewLine);
                File.AppendAllText(sourceFilePath, _driver.PageSource);
                TestContext.AddTestAttachment(sourceFilePath);

                var screenshotFileName = Path.Combine(outputFolderPath, timeStampedName + ".png");
                ((ITakesScreenshot) _driver).GetScreenshot()
                    .SaveAsFile(screenshotFileName, ScreenshotImageFormat.Png);
                TestContext.AddTestAttachment(screenshotFileName);

                var fileAppender = _log.Logger.Repository.GetAppenders().OfType<RollingFileAppender>()
                    .FirstOrDefault();
                if (fileAppender == null) return;

                fileAppender.Flush(1000);
                TestContext.AddTestAttachment(fileAppender.File);
            }
            catch (Exception e)
            {
                _log.Error(e.Message);
            }
        }
    }
}