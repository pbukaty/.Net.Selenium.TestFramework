using System;
using System.IO;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Opera;

namespace TestFramework.Factory
{
    public class WebDriverFactory
    {
        private IWebDriver _driver;

        public IWebDriver SetWebDriver(string driver)
        {
            //TODO: add support RemoteWebDriver
            // _driver = new RemoteWebDriver();

            if (string.IsNullOrEmpty(driver))
            {
                throw new ArgumentNullException(driver, "Driver name cannot be null or empty");
            }

            var driverPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            switch (driver.ToLower())
            {
                case "chrome":
                    //TODO: read options
                    var chromeOptions = new ChromeOptions();
                    // chromeOptions.AddArguments("headless");
                    _driver = new ChromeDriver(driverPath, chromeOptions);
                    break;
                case "firefox":
                    //TODO: read options
                    _driver = new FirefoxDriver(driverPath);
                    break;
                case "edge":
                    _driver = new EdgeDriver(driverPath);
                    break;
                case "ie":
                    //TODO: there some issues with ie driver: .quit() doesn't work, issue of http request timeout
                    var options = new InternetExplorerOptions();
                    options.IgnoreZoomLevel = true;
                    _driver = new InternetExplorerDriver(driverPath, options);
                    break;
                case "opera":
                    _driver = new OperaDriver(driverPath);
                    break;
                default:
                    throw new NotSupportedException($"{driver} driver is not supported.");
            }

            //TODO: these setting should be set from config
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
            _driver.Manage().Window.Maximize();

            return _driver;
        }
    }
}