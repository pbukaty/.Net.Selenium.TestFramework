using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using TestFramework.Models;

namespace TestFramework.Factory
{
    public class WebElementFactory
    {
        private readonly IWebDriver _driver;

        public WebElementFactory(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement FindWebElement(Properties properties)
        {
            var locator = properties.Locator;
            var locatorType = properties.LocatorType ?? "XPath";

            try
            {
                switch (locatorType)
                {
                    case "Id":
                        return _driver.FindElement(By.Id(locator));
                    case "Name":
                        return _driver.FindElement(By.Name(locator));
                    case "XPath":
                        return _driver.FindElement(By.XPath(locator));
                    case "CssSelector":
                        return _driver.FindElement(By.CssSelector(locator));
                    case "ClassName":
                        return _driver.FindElement(By.ClassName(locator));
                    case "LinkText":
                        return _driver.FindElement(By.LinkText(locator));
                    case "PartialLinkText":
                        return _driver.FindElement(By.PartialLinkText(locator));
                    case "TagName":
                        return _driver.FindElement(By.TagName(locator));
                    default:
                        throw new NotSupportedException($"'{locatorType}' is not supported locator type");
                }
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<IWebElement> FindWebElements(Properties properties)
        {
            var locator = properties.Locator;
            var locatorType = string.IsNullOrEmpty(properties.LocatorType) ? "XPath" : properties.LocatorType;
            switch (locatorType)
            {
                case "Id":
                    return _driver.FindElements(By.Id(locator));
                case "Name":
                    return _driver.FindElements(By.Name(locator));
                case "XPath":
                    return _driver.FindElements(By.XPath(locator));
                case "CssSelector":
                    return _driver.FindElements(By.CssSelector(locator));
                case "ClassName":
                    return _driver.FindElements(By.ClassName(locator));
                case "LinkText":
                    return _driver.FindElements(By.LinkText(locator));
                case "PartialLinkText":
                    return _driver.FindElements(By.PartialLinkText(locator));
                case "TagName":
                    return _driver.FindElements(By.TagName(locator));
                default:
                    throw new NotSupportedException($"'{locatorType}' is not supported locator type.");
            }
        }
    }
}