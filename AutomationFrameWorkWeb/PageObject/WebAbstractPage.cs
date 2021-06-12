namespace AutomationFrameWorkWeb.PageObject
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Firefox;
    using OpenQA.Selenium.Support.UI;
    using System;
    using System.Collections.Generic;
    using System.Threading;

    /// <summary>
    /// Defines the <see cref="WebAbstractPage" />.
    /// </summary>
    public class WebAbstractPage
    {
        /// <summary>
        /// Defines the select.
        /// </summary>
        SelectElement select;

        /// <summary>
        /// Defines the by.
        /// </summary>
        By by;

        /// <summary>
        /// Defines the action.
        /// </summary>
        Action action;

        /// <summary>
        /// Defines the element.
        /// </summary>
        IWebElement element;

        /// <summary>
        /// Defines the shortTimeOut.
        /// </summary>
        TimeSpan shortTimeOut = new TimeSpan(5);

        /// <summary>
        /// Defines the longTimeOut.
        /// </summary>
        TimeSpan longTimeOut = new TimeSpan(60);

        /// <summary>
        /// Defines the driver.
        /// </summary>
        IWebDriver driver;

        /// <summary>
        /// Defines the elements.
        /// </summary>
        List<IWebElement> elements;

        /// <summary>
        /// Defines the waitExplicit.
        /// </summary>
        WebDriverWait waitExplicit;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebAbstractPage"/> class.
        /// </summary>
        /// <param name="browser">The browser<see cref="string"/>.</param>
        public WebAbstractPage(string browser)
        {
            this.driver = startWebBrowser(browser);
            waitExplicit = new WebDriverWait(driver, longTimeOut);
        }

        /// <summary>
        /// The openUrl.
        /// </summary>
        /// <param name="ulrValue">The ulrValue<see cref="string"/>.</param>
        public void openUrl(string ulrValue)
        {
            driver.Navigate().GoToUrl(ulrValue);
        }

        /// <summary>
        /// The maximumBrowser.
        /// </summary>
        public void maximumBrowser()
        {
            driver.Manage().Window.Maximize();
        }

        /// <summary>
        /// The backToPage.
        /// </summary>
        public void backToPage()
        {
            driver.Navigate().Back();
        }

        /// <summary>
        /// The acceptAlert.
        /// </summary>
        public void acceptAlert()
        {
            driver.SwitchTo().Alert().Accept();
        }

        /// <summary>
        /// The cancleAlert.
        /// </summary>
        public void cancleAlert()
        {
            driver.SwitchTo().Alert().Dismiss();
        }

        /// <summary>
        /// The waitToAlertPresence.
        /// </summary>
        public void waitToAlertPresence()
        {
            waitExplicit.Until(ExpectedConditions.AlertIsPresent());
        }

        /// <summary>
        /// The sendkeyToAlert.
        /// </summary>
        /// <param name="value">The value<see cref="string"/>.</param>
        public void sendkeyToAlert(string value)
        {
            driver.SwitchTo().Alert().SendKeys(value);
        }

        //   public void getTextAlert() { driver.SwitchTo().Alert().Text; }
        /// <summary>
        /// The clickToElement.
        /// </summary>
        /// <param name="type">The type<see cref="string"/>.</param>
        /// <param name="locator">The locator<see cref="string"/>.</param>
        public void clickToElement(string type, string locator)
        {
            element = driver.FindElement(elementAttribute(type, locator));
            element.Click();
        }

        /// <summary>
        /// The sendKeyToElement.
        /// </summary>
        /// <param name="type">The type<see cref="string"/>.</param>
        /// <param name="locator">The locator<see cref="string"/>.</param>
        /// <param name="value">The value<see cref="string"/>.</param>
        public void sendKeyToElement(string type, string locator, string value)
        {
            element = driver.FindElement(elementAttribute(type, locator));
            waitToElementPresent(type, locator);
            element.Clear();
            element.SendKeys(value);
        }

        /// <summary>
        /// The clearTextInElement.
        /// </summary>
        /// <param name="type">The type<see cref="string"/>.</param>
        /// <param name="locator">The locator<see cref="string"/>.</param>
        public void clearTextInElement(string type, string locator)
        {
            element = driver.FindElement(elementAttribute(type, locator));
            waitToElementPresent(type, locator);
            element.Clear();
        }

        /// <summary>
        /// The selectInDropDown.
        /// </summary>
        /// <param name="type">The type<see cref="string"/>.</param>
        /// <param name="locator">The locator<see cref="string"/>.</param>
        /// <param name="valueItem">The valueItem<see cref="string"/>.</param>
        public void selectInDropDown(string type, string locator, string valueItem)
        {
            element = driver.FindElement(elementAttribute(type, locator));
            select = new SelectElement(element);
            select.SelectByValue(valueItem);
        }

        /// <summary>
        /// The getItemInDropDown.
        /// </summary>
        /// <param name="type">The type<see cref="string"/>.</param>
        /// <param name="locator">The locator<see cref="string"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string getItemInDropDown(string type, string locator)
        {
            element = driver.FindElement(elementAttribute(type, locator));
            select = new SelectElement(element);
            return select.SelectedOption.Text;
        }

        /// <summary>
        /// The sleepInSecond.
        /// </summary>
        /// <param name="numberInSecond">The numberInSecond<see cref="int"/>.</param>
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

        /// <summary>
        /// The waitToElementPresent.
        /// </summary>
        /// <param name="type">The type<see cref="string"/>.</param>
        /// <param name="locator">The locator<see cref="string"/>.</param>
        public void waitToElementPresent(string type, string locator)
        {
            by = elementAttribute(type, locator);
            waitExplicit.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(by));
        }

        /// <summary>
        /// The getAttributeValue.
        /// </summary>
        /// <param name="type">The type<see cref="string"/>.</param>
        /// <param name="locator">The locator<see cref="string"/>.</param>
        /// <param name="attributename">The attributename<see cref="string"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string getAttributeValue(string type, string locator, string attributename)
        {
            element = driver.FindElement(elementAttribute(type, locator));
            return element.GetAttribute(attributename);
        }

        /// <summary>
        /// The elementAttribute.
        /// </summary>
        /// <param name="type">The type<see cref="string"/>.</param>
        /// <param name="locator">The locator<see cref="string"/>.</param>
        /// <returns>The <see cref="By"/>.</returns>
        By elementAttribute(string type, string locator)
        {
            switch (type.ToUpper())
            {
                case "CSS":
                    by = By.CssSelector(locator);
                    return by;


                case "XPATH":
                    by = By.XPath(locator);
                    return by;


                default:
                    by = By.XPath(locator);
                    return by;

            }
        }

        #region Get text Element
        public string getTextElemet(string type, string locator)
        {
            element = driver.FindElement(elementAttribute(type, locator));
            return element.Text;
        }
        #endregion

        /// <summary>
        /// The startWebBrowser.
        /// </summary>
        /// <param name="browser">The browser<see cref="string"/>.</param>
        /// <returns>The <see cref="IWebDriver"/>.</returns>
        public static IWebDriver startWebBrowser(string browser)
        {
            switch (browser)
            {
                case "CHROME":
                    return new ChromeDriver();

                case "FIREFOX":
                    return new FirefoxDriver();

                default:
                    return new ChromeDriver();

            }
        }
    }
}
