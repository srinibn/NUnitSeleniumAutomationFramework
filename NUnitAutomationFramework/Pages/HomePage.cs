using OpenQA.Selenium;
using NUnitAutomationFramework.WebElements;
using NUnitAutomationFramework.Base;
using AventStack.ExtentReports;

namespace NUnitAutomationFramework.Pages
{
    public class HomePage
    {
        private readonly IWebDriver driver;
        private readonly ExtentTest test;
        public HomePage(IWebDriver driver, ExtentTest test)
        {
            this.driver = driver;
            this.test = test;
        }

        readonly string opentab = "//*[@id='opentab']";
        readonly string mouseover = "//*[@id='mousehover']";
        readonly string top = "//*[contains(text(), 'Top')]";

        public void OpenTab()
        {

            ActionsElements.Click(driver, By.XPath(opentab));
            test.Log(Status.Info, "Successfully clicked on open tab button");
            
        }
        public void MouseOver()
        {
            IWebElement element = ActionsElements.FindElement(driver, By.XPath(mouseover));
            ActionsElements.ScrollToView(driver, element);
            ActionsElements.MouseOver(driver, By.XPath(mouseover));
            test.Log(Status.Info, "Successfully mouseover on Mouseover button");
            ActionsElements.Click(driver, By.XPath(top));
            test.Log(Status.Info, "Successfully clicked on top button");

        }
    }
}

