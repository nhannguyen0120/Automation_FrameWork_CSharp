namespace AutomationFrameWorkWeb.PageObject
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Firefox;
    using OpenQA.Selenium.Interactions;
    using OpenQA.Selenium.Support.UI;
    using System;
    using System.Collections.Generic;
    using System.Threading;

    /// <summary>
    /// Defines the <see cref="WebAbstractPage" />.
    /// </summary>
    public class WebAbstractPage
    {
        SelectElement select;
        By by;
        Actions actions;
        IWebElement element;
        TimeSpan shortTimeOut = new TimeSpan(5);
        TimeSpan longTimeOut = new TimeSpan(60);
        IWebDriver driver;
        List<IWebElement> elements;
        WebDriverWait waitExplicit;
        IJavaScriptExecutor js;




        /// <summary>
        /// Initializes a new instance of the <see cref="WebAbstractPage"/> class.
        /// </summary>
        /// <param name="browser">The browser<see cref="string"/>.</param>
        public WebAbstractPage(string browser)
        {
            js = (IJavaScriptExecutor)driver;
            this.driver = startWebBrowser(browser);
            waitExplicit = new WebDriverWait(driver, longTimeOut);
            actions = new Actions(driver);
            
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

        #region Get color element
        public string getColorElement(string type, string locator, string color)
        {
            element = driver.FindElement(elementAttribute(type, locator));
            return element.GetCssValue(color);
        }
        #endregion

        #region Check radio button
        public void checkRadioButton(string type, string locator)
        {
            element.FindElement(elementAttribute(type, locator));
            if (!element.Selected)
            {
                element.Click();
            }
        }
        #endregion

        #region Uncheck radio button
        public void unCheckRadioButton(string type, string locator)
        {
            element.FindElement(elementAttribute(type, locator));
            if (!element.Selected)
            {
                element.Click();
            }
        }
        #endregion

        #region Element is displayed
        public bool isElementDisplayed(string type, string locator)
        {
            try
            {
                // Handle element exists in DOM, display or not
                element = driver.FindElement(elementAttribute(type, locator));
                return element.Displayed;
            }
            catch (NoSuchElementException e)
            {

                return false;
            }
        }
        #endregion

        #region Switch to iframe
        public void switchToIframe(string type, string locator)
        {
            element = driver.FindElement(elementAttribute(type, locator));
            driver.SwitchTo().Frame(element);
        }
        #endregion

        #region Double click to element
        public void doubleClickToElement(string type, string locator)
        {
            element = driver.FindElement(elementAttribute(type, locator));
            actions.DoubleClick(element);
        }
        #endregion

        #region Send keyboard to element
        public void sendKeyboardToElement(string type, string location, string key)
        {
            element = driver.FindElement(elementAttribute(type, location));
            actions.SendKeys(element, key).Perform();
        }
        #endregion

        #region Verify Image loaded
        public bool verifyImageLoaded(string type, string locator)
        {
            bool status;
            element = driver.FindElement(elementAttribute(type, locator));
            status = (bool)js.ExecuteScript("return argument[0].complete && typeof arguments[0].naturalWidth != \"undefined\" && argument [0].naturalWidth>0", element);
            if (status)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Wait to Element Visible
        public void waitToElementVisible(string type, string locator)
        {
            by = elementAttribute(type, locator);
            waitExplicit.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(by));
        }
        #endregion

        #region Wait to Element Clickable
        public void waittoElementClickable(string type, string locator)
        {
            by = elementAttribute(type, locator);
            waitExplicit.Until(ExpectedConditions.ElementToBeClickable(by));
        }
        #endregion

        #region Scroll to element
        public void scrollToElement(string type, string locator)
        {
            element = driver.FindElement(elementAttribute(type, locator));
            actions.MoveToElement(element);
            actions.Perform();
        }
        #endregion

        #region JS ScrollToElement
        public void jsScrollToElement(string type, string locator)
        {
            element = driver.FindElement(elementAttribute(type, locator));
            js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }
        #endregion

        #region JS Click On
        public void jsClickOn(string type, string locator)
        {
            element = driver.FindElement(elementAttribute(type, locator));
            js.ExecuteScript("argument[0].click()", element);
        }
        #endregion

        #region JS run on LOCATOR
        public void jsRunOnLocator(string javaScripToRunOn)
        {
            js.ExecuteScript(javaScripToRunOn);
        }
        #endregion

        #region Highligh Element
        public void highlighElement(string type, string locator)
        {
            element = driver.FindElement(elementAttribute(type, locator));
            for(int i = 0; i<10; i++)
            {
                js.ExecuteScript("arguments[0].setAttribute('style', arguments[1]);", element,
                    "color: red; border: 2px solid red;");
                js.ExecuteScript("arguments[0].setAttribute('style', arguments[1]);", element, "");
            }
        }
        #endregion

        #region Close
        public void Close()
        {
            if (driver != null)
            {
                driver.Quit();
            }
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
