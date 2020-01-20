using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using TestFramework.Extensions;
using TestFramework.WebPages;

namespace TestFramework.PageActions
{
    public class PageActions
    {
        private WebPage _webPage;
        private readonly IWebDriver _driver;

        public PageActions(IWebDriver driver)
        {
            _driver = driver;
        }

        public void NavigateToPage(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        public void VerifyPageIsLoaded(string pageName)
        {
            _webPage = new WebPage(_driver, pageName);

            foreach (var item in _webPage.WebElementsDictionary.Where(e => e.Value.IsMandatory == true))
            {
                var element = _webPage.GetWebElement(item.Key);
                element.Should().BeFound(item.Key,BuildErrorAdditionalInfo(item.Key));
                element.Should().BeDisplayed(item.Key, BuildErrorAdditionalInfo(item.Key));
            }
        }

        public void ElementClick(string elementName)
        {
            var element = _webPage.GetWebElement(elementName);

            element.Should().BeFound(elementName, BuildErrorAdditionalInfo(elementName));
            element.Click();
        }

        public void MoveCursorToElement(string elementName)
        {
            var element = _webPage.GetWebElement(elementName);
            element.Should().BeFound(elementName,BuildErrorAdditionalInfo(elementName));

            var actions = new Actions(_driver);
            actions.MoveToElement(element).Perform();
        }

        //TODO: split to two methods - ?
        public void VerifyElementIsDisplayed(string elementName, bool state = true)
        {
            var element = _webPage.GetWebElement(elementName);
            element.Should().BeFound(elementName, BuildErrorAdditionalInfo(elementName));

            if (state)
            {
                element.Should().BeDisplayed(elementName, BuildErrorAdditionalInfo(elementName));
            }
            else
            {
                element.Should().NotBeDisplayed(elementName, BuildErrorAdditionalInfo(elementName));
            }
        }

        public void VerifyElementsAreDisplayed(IList<string> elementNames, bool state = true)
        {
            foreach (var elementName in elementNames)
            {
                VerifyElementIsDisplayed(elementName, state);
            }
        }

        private string BuildErrorAdditionalInfo(string elementName)
        {
            return
                $" Page is '{_webPage.PageName}'; element locator: '{_webPage.WebElementsDictionary[elementName].Locator}'";
        }
    }
}