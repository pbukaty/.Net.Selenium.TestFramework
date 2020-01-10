using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using OpenQA.Selenium;
using TestFramework.Factory;
using TestFramework.Models;
using TestFramework.Providers;

namespace TestFramework.WebPages
{
    public class WebPage: BasePage
    {
        private Dictionary<string, Properties> _webElementsDictionary;

        public WebPage(IWebDriver driver, string pageName) : base(driver)
        {
            var page = JsonDataReader.LoadElements($"{pageName}.json");

            InitWebElementsDictionary(page.Elements);
        }

        public void VerifyPageLoaded()
        {
            foreach (var element in _webElementsDictionary.Where(e => e.Value.IsMandatory == true))
            {
                var webElement = GetWebElement(element.Key);
                webElement.Should().NotBeNull($"it means that '{element.Key}' element is not found");
                webElement.Displayed.Should().BeTrue($"it means that '{element.Key}' element is not visible");
            }
        }

        public IWebElement GetWebElement(string elementName)
        {
            return new WebElementFactory().FineWebElement(Driver, _webElementsDictionary[elementName]);
        }

        private void InitWebElementsDictionary(IEnumerable<Element> elements)
        {
            _webElementsDictionary = new Dictionary<string, Properties>();
            foreach (var element in elements)
            {
                _webElementsDictionary.Add(element.Name, element.Properties);
            }
        }
    }
}