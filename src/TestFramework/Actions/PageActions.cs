using FluentAssertions;
using OpenQA.Selenium;
using TestFramework.WebPages;

namespace TestFramework.Actions
{
    public class PageActions
    {
        private WebPage _webPage;

        public void NavigateToPage(string url, IWebDriver driver, string pageName)
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
            _webPage.GetWebElement(elementName).Should().NotBeNull($"it means that '{elementName}' is not found");
            _webPage.GetWebElement(elementName).Click();
        }

        public void VerifyElementIsPresent(string elementName)
        {
            _webPage.GetWebElement(elementName).Should().NotBeNull($"it means that '{elementName}' is not found");
            _webPage.GetWebElement(elementName).Displayed.Should().BeTrue($"it means that '{elementName}' element is not found");
        }

        public void VerifyElementsArePresent(string elementNames)
        {
            var names = elementNames.Split('|');
            foreach (var elementName in names)
            {
                _webPage.GetWebElement(elementName).Should().NotBeNull($"it means that '{elementName}' is not found");
                _webPage.GetWebElement(elementName).Displayed.Should().BeTrue($"it means that '{elementName}' element is not found");
            }
        }
    }
}