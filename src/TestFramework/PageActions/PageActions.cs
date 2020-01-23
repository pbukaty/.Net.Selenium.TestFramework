using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using TestFramework.Constants;
using TestFramework.Extensions;
using TestFramework.Utils;
using TestFramework.WebPages;

namespace TestFramework.PageActions
{
    public class PageActions
    {
        private WebPage _webPage;
        private readonly IWebDriver _driver;
        private readonly WebDriverUtils _driverUtils;

        public PageActions(IWebDriver driver)
        {
            _driver = driver;
            _driverUtils = new WebDriverUtils(_driver);
        }

        public void NavigateToPage(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        public void RefreshPage()
        {
            _driver.Navigate().Refresh();
        }

        public void VerifyPageIsLoaded(string pageName)
        {
            _webPage = new WebPage(_driver, pageName);

            foreach (var item in _webPage.WebElementsDictionary.Where(e => e.Value.IsMandatory == true))
            {
                // var element = _webPage.GetWebElement(item.Key);
                // element.Should().BeDisplayed(item.Key, BuildErrorAdditionalInfo(item.Key));
                VerifyElementIsDisplayed(item.Key);
            }
        }

        public void ElementClick(string elementName)
        {
            var element = _webPage.GetWebElement(elementName);
            ElementClick(element, elementName);
        }

        public void MoveCursorToElement(string elementName)
        {
            var element = _webPage.GetWebElement(elementName);
            try
            {
                var actions = new Actions(_driver);
                actions.MoveToElement(element).Perform();
            }
            catch (ElementClickInterceptedException ex)
            {
                throw new ElementClickInterceptedException($"{ex.Message}. {BuildErrorAdditionalInfo(elementName)}");
            }
        }

        public void SetElementSelectedState(string elementName, bool state)
        {
            var element = _webPage.GetWebElement(elementName);
            if (element.Selected != state)
            {
                ElementClick(element, elementName);
            }
        }

        public void ElementSendKeys(string elementName, string text)
        {
            var element = _webPage.GetWebElement(elementName);
            try
            {
                element.Clear();
                element.SendKeys(text);
            }
            catch (WebDriverException ex)
            {
                throw new WebDriverException($"{ex.Message}. {BuildErrorAdditionalInfo(elementName)}");
            }
        }

        public void SelectItemFromDropdown(string elementName, string value, string selectBy)
        {
            var dropDown = _webPage.GetDropDown(elementName);
            try
            {
                switch (selectBy)
                {
                    case "value":
                        dropDown.SelectByValue(value);
                        break;
                    case "text":
                        dropDown.SelectByText(value);
                        break;
                    case "index":
                        dropDown.SelectByIndex(int.Parse(value));
                        break;
                    default:
                        throw new NotSupportedException($"'{selectBy}' is not supported selected type");
                }
            }
            catch (NoSuchElementException ex)
            {
                throw new NoSuchElementException($"{ex.Message}. {BuildErrorAdditionalInfo(elementName)}");
            }
        }

        public void VerifyElementIsExist(string elementName, bool shouldExist = true)
        {
            _webPage.GetWebElement(elementName, shouldExist);
        }

        public void VerifyElementIsDisplayed(string elementName, bool isDisplayed = true)
        {
            var element = _webPage.GetWebElement(elementName);

            if (isDisplayed)
            {
                element.Should().BeDisplayed(elementName, BuildErrorAdditionalInfo(elementName));
            }
            else
            {
                element.Should().NotBeDisplayed(elementName, BuildErrorAdditionalInfo(elementName));
            }
        }

        public void VerifyElementsAreDisplayed(IList<string> elementNames, bool isDisplayed = true)
        {
            foreach (var elementName in elementNames)
            {
                VerifyElementIsDisplayed(elementName, isDisplayed);
            }
        }

        public void VerifyElementIsSelected(string elementName, bool isSelected = true)
        {
            var element = _webPage.GetWebElement(elementName);

            if (isSelected)
            {
                element.Should().BeSelected(elementName, BuildErrorAdditionalInfo(elementName));
            }
            else
            {
                element.Should().NotBeSelected(elementName, BuildErrorAdditionalInfo(elementName));
            }
        }

        public void VerifyElementsAreSelected(IList<string> elementNames, bool isSelected = true)
        {
            foreach (var elementName in elementNames)
            {
                VerifyElementIsSelected(elementName, isSelected);
            }
        }

        public void VerifyElementIsEnabled(string elementName, bool isEnabled = true)
        {
            var element = _webPage.GetWebElement(elementName);

            if (isEnabled)
            {
                element.Should().BeEnabled(elementName, BuildErrorAdditionalInfo(elementName));
            }
            else
            {
                element.Should().NotBeEnabled(elementName, BuildErrorAdditionalInfo(elementName));
            }
        }

        public void VerifyElementsAreEnabled(IList<string> elementNames, bool isEnabled = true)
        {
            foreach (var elementName in elementNames)
            {
                VerifyElementIsEnabled(elementName, isEnabled);
            }
        }

        public void VerifyElementsContainsText(string elementName, string text)
        {
            var elements = _webPage.GetWebElement(elementName);
        }

        private void ElementClick(IWebElement element, string elementName)
        {
            try
            {
                element.Click();
            }
            catch (ElementClickInterceptedException ex)
            {
                try
                {
                    _driverUtils.ExecuteScript(element, JavaScript.Click);
                }
                catch (WebDriverException ex2)
                {
                    throw new Exception($"{ex.Message} & {ex2.Message} {BuildErrorAdditionalInfo(elementName)}");
                }
            }
        }

        private string BuildErrorAdditionalInfo(string elementName)
        {
            return
                $"Element info: Page is '{_webPage.PageName}'; element name: '{elementName}'; element locator: '{_webPage.WebElementsDictionary[elementName].Locator}'";
        }
    }
}