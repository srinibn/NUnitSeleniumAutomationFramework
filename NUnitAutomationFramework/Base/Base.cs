using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System.Configuration;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using NUnitAutomationFramework.Utility;
using NUnitAutomationFramework.WebElements;

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
            string dir = System.Environment.CurrentDirectory;
            string projdir = Directory.GetParent(dir).Parent.Parent.FullName;
            string reporpath = projdir + "/Reports/Report.html";
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
            string? browser_name = ConfigurationManager.AppSettings["Browser"];
            string? url = GetEnvironementData.GetEnvData();
            SetBrowser(browser_name?.Trim());
            ActionsElements.NavigateToUrl(driver.Value, url);
            //driver.Value.Navigate().GoToUrl(url);
            driver.Value.Manage().Window.Maximize();

        }

        public IWebDriver GetDriver()
        {
            return driver.Value;

        }

        private void SetBrowser(string BrowserName)
        {
            string? RunEnivorment = ConfigurationManager.AppSettings["RunEnvironment"];
       
            if (RunEnivorment != null && RunEnivorment.Equals("Local"))
            {
                switch (BrowserName)
                {
                    case "Chrome":
                        new DriverManager().SetUpDriver(new ChromeConfig());
                        driver.Value = new ChromeDriver();
                        TestContext.Progress.WriteLine("Browser Started");
                        break;

                    default:
                        TestContext.Progress.WriteLine(" Incorrect Browser details is passed, please check browser name in app.config file");
                        break;
                }
            }
            else if (RunEnivorment != null && RunEnivorment.Equals("Remote"))
            {
                ChromeOptions options = new ChromeOptions();
                driver.Value = new RemoteWebDriver(new Uri("https://www.google.com"), options);
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
                extent_test.Value.Fail("TestCase Status : Failed", CaptureScreenShot(driver.Value, Filename));
                extent_test.Value.Fail(stackTrace);
            }
            else if (status == TestStatus.Passed)
            {
                extent_test.Value.Log(Status.Pass, MarkupHelper.CreateLabel("TestCase Status : " + status, ExtentColor.Green));
            }
            extent.Flush();
            driver.Value.Quit();
        }

        public MediaEntityModelProvider CaptureScreenShot(IWebDriver driver, string screenShotName)
        {
            ITakesScreenshot scr = (ITakesScreenshot)driver;
            var screenshot = scr.GetScreenshot().AsBase64EncodedString;

            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, screenShotName).Build();
        }

    }
}

