using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestFramework.Utils
{
    public class WebDriverUtils
    {
        private readonly IWebDriver _driver;
        public WebDriverUtils(IWebDriver driver)
        {
            _driver = driver;
        }

        public void TakeScreenshot(string reportPath, string screenshotName)
        {
            var screenshot = ((ITakesScreenshot) _driver).GetScreenshot();
            screenshot.SaveAsFile($"{reportPath}\\{screenshotName}.png");
        }

        public void ExecuteScript(IWebElement element, string script)
        {
            ((IJavaScriptExecutor)_driver).ExecuteScript(script, element);
        }

        public void ExecuteScript(SelectElement element, string script)
        {
            ((IJavaScriptExecutor)_driver).ExecuteScript(script, element);
        }
    }
}