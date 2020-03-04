using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Allure.Commons;
using Allure.NUnit.Attributes;
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

        [AllureStep("Navigate to page '&url&'")]
        public void NavigateToPage(string url)
        {
            _driver.Navigate().GoToUrl(url);
        }

        [AllureStep("Refresh page")]
        public void RefreshPage()
        {
            _driver.Navigate().Refresh();
        }

        [AllureStep("Verify page '&pageName&' is loaded")]
        public void VerifyPageIsLoaded(string pageName)
        {
            _webPage = new WebPage(_driver, pageName);

            foreach (var item in _webPage.WebElementsDictionary.Where(e => e.Value.IsMandatory == true))
            {
                VerifyElementIsDisplayed(item.Key);
            }
        }

        [AllureStep("Click on element '&elementName&'")]
        public void ElementClick(string elementName)
        {
            var element = _webPage.GetWebElement(elementName);
            ElementClick(element, elementName);
        }

        [AllureStep("Move cursor on element '&elementName&'")]
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
                throw new ElementClickInterceptedException($"{ex.Message}. {_webPage.BuildErrorAdditionalInfo(elementName)}");
            }
        }

        // [AllureStep("Set element '&elementName&' selected state into ")]
        public void SetElementSelectedState(string elementName, bool state)
        {
            //TODO: workaround because [AllureStep] annotation and 
            AllureLifecycle.Instance.RunStep($"Set element '{elementName}' selected state into '{state}'", () =>
            {
                var element = _webPage.GetWebElement(elementName);
                if (element.Selected != state)
                {
                    ElementClick(element, elementName);
                }
            });
        }

        [AllureStep("Type text '&text&' into element '&elementName&'")]
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
                throw new WebDriverException($"{ex.Message}. {_webPage.BuildErrorAdditionalInfo(elementName)}");
            }
        }

        [AllureStep("Select value '&value&' from dropdown '&elementName&' by '&selectBy&'")]
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
                throw new Exception($"{ex.Message}. {_webPage.BuildErrorAdditionalInfo(elementName)}");
            }
        }

        [AllureStep("Verify element '&elementName&' is exist")]
        public void VerifyElementIsExist(string elementName)
        {
            _webPage.GetWebElement(elementName);
        }

        [AllureStep("Verify element '&elementName&' is not exist")]
        public void VerifyElementIsNotExist(string elementName)
        {
            _webPage.GetWebElement(elementName, false);
        }

        [AllureStep("Verify element '&elementName&' is displayed")]
        public void VerifyElementIsDisplayed(string elementName)
        {
            var element = _webPage.GetWebElement(elementName);
            element.Should().BeDisplayed(_webPage.BuildErrorAdditionalInfo(elementName));
        }

        [AllureStep("Verify element '&elementName&' is not displayed")]
        public void VerifyElementIsNotDisplayed(string elementName)
        {
            var element = _webPage.GetWebElement(elementName);
            element.Should().NotBeDisplayed(_webPage.BuildErrorAdditionalInfo(elementName));
        }

        [AllureStep("Verify elements are displayed")]
        public void VerifyElementsAreDisplayed(IList<string> elementNames)
        {
            foreach (var elementName in elementNames)
            {
                VerifyElementIsDisplayed(elementName);
            }
        }

        [AllureStep("Verify elements are not displayed")]
        public void VerifyElementsAreNotDisplayed(IList<string> elementNames)
        {
            foreach (var elementName in elementNames)
            {
                VerifyElementIsNotDisplayed(elementName);
            }
        }

        [AllureStep("Verify element '&elementName&' is selected")]
        public void VerifyElementIsSelected(string elementName)
        {
            var element = _webPage.GetWebElement(elementName);
            element.Should().BeSelected(_webPage.BuildErrorAdditionalInfo(elementName));
        }

        [AllureStep("Verify element '&elementName&' is not selected")]
        public void VerifyElementIsNotSelected(string elementName)
        {
            var element = _webPage.GetWebElement(elementName);
            element.Should().NotBeSelected(_webPage.BuildErrorAdditionalInfo(elementName));
        }

        [AllureStep("Verify elements are selected")]
        public void VerifyElementsAreSelected(IList<string> elementNames)
        {
            foreach (var elementName in elementNames)
            {
                VerifyElementIsSelected(elementName);
            }
        }

        [AllureStep("Verify elements '&elementNames&' are not selected")]
        public void VerifyElementsAreNotSelected(IList<string> elementNames)
        {
            foreach (var elementName in elementNames)
            {
                VerifyElementIsNotSelected(elementName);
            }
        }

        [AllureStep("Verify element '&elementName&' is enabled")]
        public void VerifyElementIsEnabled(string elementName)
        {
            var element = _webPage.GetWebElement(elementName);
            element.Should().BeEnabled(_webPage.BuildErrorAdditionalInfo(elementName));
        }

        [AllureStep("Verify element '&elementName&' is not enabled")]
        public void VerifyElementIsNotEnabled(string elementName)
        {
            var element = _webPage.GetWebElement(elementName);
            element.Should().NotBeEnabled(_webPage.BuildErrorAdditionalInfo(elementName));
        }

        [AllureStep("Verify elements are enabled")]
        public void VerifyElementsAreEnabled(IList<string> elementNames)
        {
            foreach (var elementName in elementNames)
            {
                VerifyElementIsEnabled(elementName);
            }
        }

        [AllureStep("Verify elements are not enabled")]
        public void VerifyElementsAreNotEnabled(IList<string> elementNames)
        {
            foreach (var elementName in elementNames)
            {
                VerifyElementIsNotEnabled(elementName);
            }
        }

        [AllureStep("Verify elements '&elementName&' contains text '&text&'")]
        public void VerifyElementsContainsText(string elementName, string text)
        {
            //TODO: should be replaced by waiting
            Thread.Sleep(5000);
            var elements = _webPage.GetWebElements(elementName);
            //TODO: need to verify that elements count > 0

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
                    throw new Exception($"{ex.Message} & {ex2.Message} {_webPage.BuildErrorAdditionalInfo(elementName)}");
                }
            }
        }
    }
}