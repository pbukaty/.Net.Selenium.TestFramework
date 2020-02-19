using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TestFramework.Extensions;
using TestFramework.Factory;
using TestFramework.Models;
using TestFramework.Readers;

namespace TestFramework.WebPages
{
    public class WebPage: BasePage
    {
        public Dictionary<string, Properties> WebElementsDictionary { get; private set; }
        public string PageName { get; }

        private readonly WebElementFactory _webElementFactory;

        public WebPage(IWebDriver driver, string pageName) : base(driver)
        {
            var page = JsonDataReader.LoadElements($"{pageName}.json");
            InitWebElementsDictionary(page.Elements);
            PageName = pageName;
            _webElementFactory = new WebElementFactory(Driver);
        }

        public IWebElement GetWebElement(string elementName, bool shouldExist = true)
        {
            try
            {
                var element = _webElementFactory.FindWebElement(WebElementsDictionary[elementName]);
                if (shouldExist)
                {
                    element.Should().BeFound(elementName, BuildErrorAdditionalInfo(elementName));
                }
                else
                {
                    element.Should().NotBeFound(elementName, BuildErrorAdditionalInfo(elementName));
                }

                return element;
            }
            catch (NotSupportedException ex)
            {
                throw new NotSupportedException($"{ex.Message}. {BuildErrorAdditionalInfo(elementName)}");
            }
        }

        public IEnumerable<IWebElement> GetWebElements(string elementName)
        {
            try
            {
                return _webElementFactory.FindWebElements(WebElementsDictionary[elementName]);
            }
            catch (NotSupportedException ex)
            {
                throw new NotSupportedException($"{ex.Message}. {BuildErrorAdditionalInfo(elementName)}");
            }
            catch (InvalidSelectorException ex)
            {
                throw new InvalidSelectorException($"{ex.Message}. {BuildErrorAdditionalInfo(elementName)}");
            }
            
        }

        public SelectElement GetDropDown(string elementName)
        {
            var element = GetWebElement(elementName);
            try
            {
                return new SelectElement(element);
            }
            catch (UnexpectedTagNameException ex)
            {
                throw new UnexpectedTagNameException($"{ex.Message}. {BuildErrorAdditionalInfo(elementName)}");
            }
        }

        private void InitWebElementsDictionary(IEnumerable<Element> elements)
        {
            WebElementsDictionary = new Dictionary<string, Properties>();
            foreach (var element in elements)
            {
                WebElementsDictionary.Add(element.Name, element.Properties);
            }
        }

        private string BuildErrorAdditionalInfo(string elementName)
        {
            return
                $"Element info: Page is '{PageName}'; element name: '{elementName}'; element locator: '{WebElementsDictionary[elementName].Locator}'";
        }
    }
}