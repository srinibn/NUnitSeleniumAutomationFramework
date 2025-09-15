using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace NUnitAutomationFramework.WebElements
{
    public class ActionsElements
    {
      # use this file to add custom action elements 
        public static void WaitForPageLoad(IWebDriver driver, int timeout = 5)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, timeout));
            wait.Until(wd => js.ExecuteScript("return document.readyState").ToString() == "complete");
        }

        public static IWebElement WaitForElementToDisplay(IWebDriver driver, By by, int timeout = 5)
        {
            DefaultWait<IWebDriver> fluentwait = new DefaultWait<IWebDriver>(driver);
            fluentwait.Timeout = TimeSpan.FromSeconds(timeout);
            fluentwait.PollingInterval = TimeSpan.FromSeconds(3);
            fluentwait.IgnoreExceptionTypes(typeof(StaleElementReferenceException));
            fluentwait.Message = "Element not found";

            IWebElement element = driver.FindElement(by);
            return element;
        }

        public static IWebElement FindElement(IWebDriver driver, By by, int timeout = 5)
        {
            IWebElement element = null;

            try
            {
                element = WaitForElementToDisplay(driver, by, timeout);
            }
            catch (StaleElementReferenceException e)
            {
                try
                {
                    Console.WriteLine("Stalement Element expection occured, re-trying to find element");
                    WaitForPageLoad(driver, timeout);
                    element = WaitForElementToDisplay(driver, by, timeout);
                }
                catch (Exception e1)
                {

                    throw new ActionExpection("Expection during Find Element operation ..");
                }
            }
            catch (NoSuchElementException ex)
            {
                throw new ActionExpection("No Such element Expection during FindElement operation ..");
            }
            catch (Exception e)
            {
                throw new ActionExpection("Expection during FindElement operation ..");
            }
            return element;
        }

        public static void ScrollToView(IWebDriver driver, IWebElement element)
        {
            /*
             * If this method is not working for you, use following code
             * ((JavascriptExecutor) driver).executeScript("arguments[0].scrollIntoView(true);", element);
             */
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoViewIfNeeded()", element);

        }

        public static void Click(IWebDriver driver, By by, int timeout = 5)
        {

            try
            {
                IWebElement element = WaitForElementToDisplay(driver, by, timeout);
                if (element != null)
                {
                    ScrollToView(driver, element);
                    element.Click();
                }
            }
            catch (StaleElementReferenceException e)
            {
                try
                {
                    Console.WriteLine("Stale element expection occured, re-trying to perform Click action");
                    IWebElement element = WaitForElementToDisplay(driver, by, timeout);
                    ScrollToView(driver, element);
                    element.Click();
                }
                catch (Exception e1)
                {
                    throw new ActionExpection("Expection during click operation ..");
                }
            }
            catch (Exception e)
            {

                throw new ActionExpection("Expection during click operation ..");

            }
        }

        public static void SelectDropDownByValue(IWebDriver driver, By by, string value, int timeout)
        {
            try
            {
                SelectElement select = new SelectElement(FindElement(driver, by, timeout));
                select.SelectByValue(value);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unable to select value from dropdown");
            }
        }

        public static void MouseOver(IWebDriver driver, By by, int timeout = 5)
        {
            Actions action = new Actions(driver);
            action.MoveToElement(FindElement(driver, by)).Perform();
        }

        public static void NavigateToUrl(IWebDriver driver, string URL)
        {
            driver.Navigate().GoToUrl(URL);
        }

        public static void SendKeys(IWebDriver driver, By by, string value)
        {
            IWebElement element = WaitForElementToDisplay(driver, by);
            if (element != null)
            {
                element.Clear();
                element.SendKeys(value);          }
        }
    }
}
