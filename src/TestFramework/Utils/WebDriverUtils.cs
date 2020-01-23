using System;
using OpenQA.Selenium;

namespace TestFramework.Utils
{
    public class WebDriverUtils
    {
        private readonly IWebDriver _driver;
        public WebDriverUtils(IWebDriver driver)
        {
            _driver = driver;
        }

        public void TakeScreenshot(string screenshotName)
        {
            var screenshot = ((ITakesScreenshot) _driver).GetScreenshot();
            screenshot.SaveAsFile($"{AppDomain.CurrentDomain.BaseDirectory}TestResults\\{screenshotName}.png");
        }

        public void ExecuteScript(IWebElement element, string script)
        {
            ((IJavaScriptExecutor)_driver).ExecuteScript(script, element);
        }
    }
}