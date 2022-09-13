using OpenQA.Selenium;
using NUnitAutomationFramework.WebElements;
using NUnitAutomationFramework.Base;
using AventStack.ExtentReports;

namespace NUnitAutomationFramework.Pages
{
    public class HomePage : BaseSetup
    {
        readonly string opentab = "//*[@id='opentab']";
        readonly string mouseover = "//*[@id='mousehover']";
        readonly string top = "//*[contains(text(), 'Tops')]";

        public void OpenTab(IWebDriver driver, ExtentTest test)
        {

            ActionsElements.Click(driver, By.XPath(opentab));
            test.Log(Status.Info, "Successfully clicked on open tab button");
            
        }
        public void MouseOver(IWebDriver driver, ExtentTest test)
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

