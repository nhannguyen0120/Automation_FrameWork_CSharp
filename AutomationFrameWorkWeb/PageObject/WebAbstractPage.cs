using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AutomationFrameWorkWeb.PageObject
{
  public class WebAbstractPage
    {
        SelectElement select ;
        By by;
        Action action;
        IWebElement element;
        TimeSpan shortTimeOut = new TimeSpan(5);
        TimeSpan longTimeOut = new TimeSpan(60);
        IWebDriver driver;
        List<IWebElement> elements;
        WebDriverWait waitExplicit;
        public WebAbstractPage(string browser)
        {
            this.driver = startWebBrowser(browser);
            waitExplicit = new WebDriverWait(driver,longTimeOut);
        
        }

        #region Open URL
        public void openUrl(string ulrValue) { driver.Navigate().GoToUrl(ulrValue); }
        #endregion

        #region Maximum Browser
        public void maximumBrowser() { driver.Manage().Window.Maximize(); }
        #endregion

        #region Back to page
        public void backToPage() { driver.Navigate().Back(); }
        #endregion

        #region Accept Alert
        public void acceptAlert() { driver.SwitchTo().Alert().Accept(); }
        #endregion

        #region Cancle Alert
        public void cancleAlert() { driver.SwitchTo().Alert().Dismiss(); }
        #endregion

        #region Wait to Alert Presence
        public void waitToAlertPresence() { waitExplicit.Until (ExpectedConditions.AlertIsPresent()); }
        #endregion

        #region Sendkey To Alert
        public void sendkeyToAlert(string value) { driver.SwitchTo().Alert().SendKeys(value); }
        #endregion

        #region Get Text Alert
          //   public void getTextAlert() { driver.SwitchTo().Alert().Text; }
        #endregion

        #region Click to element
        public void clickToElement(string type, string locator)
        {
            element = driver.FindElement(elementAttribute(type,locator));
            element.Click();
        }
        #endregion

        #region Sendkey to element
        public void sendKeyToElement(string type, string locator, string value)
        {
            element = driver.FindElement(elementAttribute(type, locator));
            waitToElementPresent(type, locator);
            element.Clear();
            element.SendKeys(value);
        }
        #endregion

        #region Clear text in element
        public void clearTextInElement(string type, string locator)
        {
            element = driver.FindElement(elementAttribute(type, locator));
            waitToElementPresent(type, locator);
            element.Clear();

        }
        #endregion

        #region Select in dropdown
        public void selectInDropDown(string type, string locator, string valueItem)
        {
            element = driver.FindElement(elementAttribute(type, locator));
            select = new SelectElement(element);
            select.SelectByValue(valueItem);
        }
        #endregion

        #region getItemInDropDown
        public string getItemInDropDown(string type, string locator)
        {
            element = driver.FindElement(elementAttribute(type, locator));
            select = new SelectElement(element);
            return select.SelectedOption.Text;
        }
        #endregion

        #region Sleep in seconds
        public void sleepInSecond(int numberInSecond)
        {
            try
            {
                Thread.Sleep(numberInSecond * 1000);
            }
            catch (ThreadInterruptedException e)
            {
                Console.WriteLine(System.Environment.StackTrace);
            }
        }
        #endregion


        #region Wait to Element present
        public void waitToElementPresent(string type, string locator)
        {
            by = elementAttribute(type, locator);
            waitExplicit.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(by));
        }
        #endregion

        By elementAttribute(string type,string locator)
        {
            switch (type.ToUpper())
            {
                case "CSS":
                    by = By.CssSelector(locator);
                    return by;
                    break;

                case "XPATH":
                    by = By.XPath(locator);
                    return by;
                    break;

                default:
                    by = By.XPath(locator);
                    return by;
                    break;
            }
           
        }

        public static IWebDriver startWebBrowser(string browser)
        {
            switch (browser)
            {
                case "CHROME":
                    return new ChromeDriver();
                    break;

                case "FIREFOX":
                    return new FirefoxDriver();

                default:
                    return new ChromeDriver();
                    break;
            }
        }
        
    }
}
