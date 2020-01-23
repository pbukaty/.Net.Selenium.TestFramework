using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using TestFramework.Models;

namespace TestFramework.Factory
{
    public class WebElementFactory
    {
        public IWebElement FindWebElement(IWebDriver driver, Properties properties)
        {
            var locator = properties.Locator;
            var locatorType = properties.LocatorType ?? "XPath";

            switch (locatorType)
            {
                case "Id":
                    return driver.FindElement(By.Id(locator));
                case "Name":
                    return driver.FindElement(By.Name(locator));
                case "XPath":
                    return driver.FindElement(By.XPath(locator));
                case "CssSelector":
                    return driver.FindElement(By.CssSelector(locator));
                case "ClassName":
                    return driver.FindElement(By.ClassName(locator));
                case "LinkText":
                    return driver.FindElement(By.LinkText(locator));
                case "PartialLinkText":
                    return driver.FindElement(By.PartialLinkText(locator));
                case "TagName":
                    return driver.FindElement(By.TagName(locator));
                default:
                    throw new NotSupportedException($"'{locatorType}' is not supported locator type");
            }
        }

        public IEnumerable<IWebElement> FindWebElements(IWebDriver driver, Properties properties)
        {
            var locator = properties.Locator;
            var locatorType = string.IsNullOrEmpty(properties.LocatorType) ? "XPath" : properties.LocatorType;
            switch (locatorType)
            {
                case "Id":
                    return driver.FindElements(By.Id(locator));
                case "Name":
                    return driver.FindElements(By.Name(locator));
                case "XPath":
                    return driver.FindElements(By.XPath(locator));
                case "CssSelector":
                    return driver.FindElements(By.CssSelector(locator));
                case "ClassName":
                    return driver.FindElements(By.ClassName(locator));
                case "LinkText":
                    return driver.FindElements(By.LinkText(locator));
                case "PartialLinkText":
                    return driver.FindElements(By.PartialLinkText(locator));
                case "TagName":
                    return driver.FindElements(By.TagName(locator));
                default:
                    throw new NotSupportedException($"'{locatorType}' is not supported locator type.");
            }
        }
    }
}