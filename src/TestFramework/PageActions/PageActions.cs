using System.Collections.Generic;
using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using TestFramework.WebPages;

namespace TestFramework.PageActions
{
    public class PageActions
    {
        private WebPage _webPage;
        private readonly IWebDriver _webDriver;

        public PageActions(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public void NavigateToPage(IWebDriver driver, string url, string pageName)
        {
            driver.Navigate().GoToUrl(url);
            VerifyPageIsLoaded(driver, pageName);
        }

        public void VerifyPageIsLoaded(IWebDriver driver, string pageName)
        {
            _webPage = new WebPage(driver, pageName);

            _webPage.VerifyPageLoaded();
        }

        public void ElementClick(string elementName)
        {
            var element = _webPage.GetWebElement(elementName);
            element.Should().NotBeNull($"it means that '{elementName}' is not found");
            element.Click();
        }

        public void MoveToElement(string elementName)
        {
            var element = _webPage.GetWebElement(elementName);
            element.Should().NotBeNull($"it means that '{elementName}' is not found");
        
            var actions = new Actions(_webDriver);
            actions.MoveToElement(element).Perform();
        }

        public void VerifyElementIsDisplayed(string elementName, bool state = true)
        {
            var errorMessage = state ? "is not visible" : "is visible";

            var element = _webPage.GetWebElement(elementName);
            element.Should().NotBeNull($"it means that '{elementName}' is not found");
            element.Displayed.Should().Be(state, $"it means that '{elementName}' element {errorMessage}");
        }

        public void VerifyElementsAreDisplayed(IList<string> elementNames, bool state = true)
        {
            var errorMessage = state ? "is not visible" : "is visible";

            foreach (var elementName in elementNames)
            {
                var element = _webPage.GetWebElement(elementName);
                element.Should().NotBeNull($"it means that '{elementName}' is not found");
                element.Displayed.Should().Be(state, $"it means that '{elementName}' element {errorMessage}");
            }
        }
    }
}