using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
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
                _driverUtils.ExecuteScript(dropDown, JavaScript.ScrollIntoElement);

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
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}. {BuildErrorAdditionalInfo(elementName)}");
            }
        }

        public void VerifyElementIsExist(string elementName)
        {
            _webPage.GetWebElement(elementName);
        }

        public void VerifyElementIsNotExist(string elementName)
        {
            _webPage.GetWebElement(elementName, false);
        }

        public void VerifyElementIsDisplayed(string elementName)
        {
            var element = _webPage.GetWebElement(elementName);
            element.Should().BeDisplayed(elementName, BuildErrorAdditionalInfo(elementName));
        }

        public void VerifyElementIsNotDisplayed(string elementName)
        {
            var element = _webPage.GetWebElement(elementName);
            element.Should().NotBeDisplayed(elementName, BuildErrorAdditionalInfo(elementName));
        }

        public void VerifyElementsAreDisplayed(IList<string> elementNames)
        {
            foreach (var elementName in elementNames)
            {
                VerifyElementIsDisplayed(elementName);
            }
        }

        public void VerifyElementsAreNotDisplayed(IList<string> elementNames)
        {
            foreach (var elementName in elementNames)
            {
                VerifyElementIsNotDisplayed(elementName);
            }
        }

        public void VerifyElementIsSelected(string elementName)
        {
            var element = _webPage.GetWebElement(elementName);
            element.Should().BeSelected(elementName, BuildErrorAdditionalInfo(elementName));
        }

        public void VerifyElementIsNotSelected(string elementName)
        {
            var element = _webPage.GetWebElement(elementName);
            element.Should().NotBeSelected(elementName, BuildErrorAdditionalInfo(elementName));
        }

        public void VerifyElementsAreSelected(IList<string> elementNames)
        {
            foreach (var elementName in elementNames)
            {
                VerifyElementIsSelected(elementName);
            }
        }

        public void VerifyElementsAreNotSelected(IList<string> elementNames)
        {
            foreach (var elementName in elementNames)
            {
                VerifyElementIsNotSelected(elementName);
            }
        }

        public void VerifyElementIsEnabled(string elementName)
        {
            var element = _webPage.GetWebElement(elementName);
            element.Should().BeEnabled(elementName, BuildErrorAdditionalInfo(elementName));
        }

        public void VerifyElementIsNotEnabled(string elementName)
        {
            var element = _webPage.GetWebElement(elementName);
            element.Should().NotBeEnabled(elementName, BuildErrorAdditionalInfo(elementName));
        }

        public void VerifyElementsAreEnabled(IList<string> elementNames)
        {
            foreach (var elementName in elementNames)
            {
                VerifyElementIsEnabled(elementName);
            }
        }

        public void VerifyElementsAreNotEnabled(IList<string> elementNames)
        {
            foreach (var elementName in elementNames)
            {
                VerifyElementIsNotEnabled(elementName);
            }
        }

        public void VerifyElementsContainsText(string elementName, string text)
        {
            //TODO: should be replaced by waiting
            Thread.Sleep(5000);
            var elements = _webPage.GetWebElements(elementName);

            var strs = new List<string>();
            foreach (var element in elements)
            {
                strs.Add(element.Text);
            }
            File.WriteAllLines($"{AppDomain.CurrentDomain.BaseDirectory}log.txt", strs);
        }

        private void ElementClick(IWebElement element, string elementName)
        {
            try
            {
                element.Click();
            }
            catch (ElementNotInteractableException ex)
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