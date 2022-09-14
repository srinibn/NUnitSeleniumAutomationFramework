using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using System.Configuration;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace NUnitAutomationFramework.Base
{
    public class DriverSetup
    {
        private static string? RunEnivorment = ConfigurationManager.AppSettings["RunEnvironment"];
        private static string? browser_name = ConfigurationManager.AppSettings["Browser"];
        public static IWebDriver LocalBrowserSetup(IWebDriver driver)
        {

            if (BrowserType.Chrome.ToString().Equals(browser_name))
            {
                new DriverManager().SetUpDriver(new ChromeConfig());
                driver = new ChromeDriver();
            }
            else if (BrowserType.Firefox.ToString().Equals(browser_name))
            {
                new DriverManager().SetUpDriver(new FirefoxConfig());
                driver = new FirefoxDriver();
            }
            else if (BrowserType.IE.ToString().Equals(browser_name))
            {
                new DriverManager().SetUpDriver(new InternetExplorerConfig());
                driver = new InternetExplorerDriver();
            }
            else
            {
                Console.WriteLine("Default Browser is initated, please check browser details in app.config file");
                new DriverManager().SetUpDriver(new ChromeConfig());
                driver = new ChromeDriver();
            }

            return driver;

        }

        public static RemoteWebDriver RemoteBrowserSetup(IWebDriver driver)
        {
            if (BrowserType.Chrome.ToString().Equals(browser_name))
            {
                string? username = ConfigurationManager.AppSettings["BrowserStackUserName"];
                string? key = ConfigurationManager.AppSettings["BrowserStackKey"];

                ChromeOptions capabilities = new ChromeOptions();
                capabilities.BrowserVersion = "latest";
                Dictionary<string, object> browserstackOptions = new();
                browserstackOptions.Add("os", "Windows");
                browserstackOptions.Add("osVersion", "10");
                browserstackOptions.Add("projectName", "Selenium C# NUnit Project");
                browserstackOptions.Add("sessionName", "parallel_test");
                browserstackOptions.Add("seleniumVersion", "4.3.0");
                browserstackOptions.Add("userName", username);
                browserstackOptions.Add("accessKey", key);
                browserstackOptions.Add("browserName", browser_name);
                capabilities.AddAdditionalOption("bstack:options", browserstackOptions);

                driver = new RemoteWebDriver(
        new Uri("http://" + ConfigurationManager.AppSettings.Get("server") + "/wd/hub/"), capabilities);
            }

            return (RemoteWebDriver)driver;
        }
    }
}
