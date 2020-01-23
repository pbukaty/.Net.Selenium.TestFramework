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
            switch (driver)
            {
                case "chrome":
                    _driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
                    break;
                case "firefox":
                    _driver = new FirefoxDriver();
                    break;
                case "edge":
                    _driver = new EdgeDriver();
                    break;
                case "ie":
                    _driver = new InternetExplorerDriver();
                    break;
                case "opera":
                    _driver = new OperaDriver();
                    break;
                default:
                    throw new NotSupportedException($"{driver} driver is not supported.");
            }

            return _driver;
        }
    }
}