using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;


namespace AutomationFrameWorkWeb.Test
{
  
    
  public class BaseTest : IDisposable
    {
        IWebDriver driver;
       
        public BaseTest()
        {
            driver.Manage().Window.Maximize();

        }
        public void Dispose()
        {
            driver.Quit();
        }
   
    }

    class Tesfigture : IClassFixture<BaseTest>
    {
        public Tesfigture(BaseTest data)
        {

        }
    }

   
}
