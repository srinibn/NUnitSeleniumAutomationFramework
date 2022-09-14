using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnitAutomationFramework.Utility;
using NUnitAutomationFramework.WebElements;
using OpenQA.Selenium;
using System.Configuration;

namespace NUnitAutomationFramework.Base
{
    public class BaseSetup
    {

        public ExtentReports extent;
        public ExtentTest test;
        public ThreadLocal<IWebDriver> driver = new ThreadLocal<IWebDriver>();
        public ThreadLocal<ExtentTest> extent_test = new ThreadLocal<ExtentTest>();

        [OneTimeSetUp]
        public void Setup()
        {
            string? testclassfilename = TestContext.CurrentContext.Test.ClassName;
            string dir = System.Environment.CurrentDirectory;
            string? projdir = Directory.GetParent(dir)?.Parent?.Parent?.FullName;
            string reporpath = projdir + "\\Reports\\Report.html";
            var htmlreport = new ExtentHtmlReporter(reporpath);
            extent = new ExtentReports();
            extent.AttachReporter(htmlreport);
            extent.AddSystemInfo("Enivorment", "QA");
        }

        [SetUp]
        public void Start_Browser()
        {
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
            extent_test.Value = test;
            SetBrowser();

            string? url = GetEnvironementData.GetEnvData();
            ActionsElements.NavigateToUrl(driver.Value, url);
            driver.Value.Manage().Window.Maximize();
        }

        public IWebDriver GetDriver()
        {
            return driver.Value;

        }

        private void SetBrowser()
        {
            string? RunEnivorment = ConfigurationManager.AppSettings["RunEnvironment"];

            if (RunEnivorment != null && RunEnivorment.Equals("Local"))
            {

                driver.Value = DriverSetup.LocalBrowserSetup(driver.Value);
            }
            else if (RunEnivorment != null && RunEnivorment.Equals("Remote"))
            {
                driver.Value = DriverSetup.RemoteBrowserSetup(driver.Value);
            }
            else
            {
                TestContext.Progress.WriteLine("Please check browser name and run enivorment value in app.config file");

            }
        }

        [TearDown]
        public void SetTestResults()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;

            if (status == TestStatus.Failed)
            {
                var stackTrace = TestContext.CurrentContext.Result.StackTrace;
                DateTime date = DateTime.Now;
                string Filename = "Screenshot_" + date.ToString("h_mm_ss") + ".png";
                extent_test?.Value?.Fail("TestCase Status : Failed", CaptureScreenShot(driver.Value, Filename));
                extent_test?.Value?.Fail(stackTrace);
            }
            else if (status == TestStatus.Passed)
            {
                extent_test.Value.Log(Status.Pass, MarkupHelper.CreateLabel("TestCase Status : " + status, ExtentColor.Green));
            }
            extent.Flush();
            driver.Value.Quit();
        }

        public static MediaEntityModelProvider CaptureScreenShot(IWebDriver driver, string screenShotName)
        {
            ITakesScreenshot scr = (ITakesScreenshot)driver;
            var screenshot = scr.GetScreenshot().AsBase64EncodedString;

            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, screenShotName).Build();
        }

    }
}

