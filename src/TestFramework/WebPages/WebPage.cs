using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using OpenQA.Selenium;
using TestFramework.Models;
using TestFramework.Providers;

namespace TestFramework.WebPages
{
    public class WebPage: BasePage
    {
        private Dictionary<string, string> _webElementsDictionary;
        private readonly Page _page;

        public WebPage(IWebDriver driver, string pageName) : base(driver)
        {
            _page = JsonDataReader.LoadElements("Pages.json").PageCollection.ToList().Single(p => p.Name == pageName);

            InitWebElementsDictionary(_page.Elements);
        }

        public void VerifyPageLoaded()
        {
            foreach (var element in _page.Elements.Where(e => e.IsMandatory == true))
            {
                var webElement = GetWebElement(element.Name);
                webElement.Should().NotBeNull($"it means that '{element.Name}' element is not found");
                webElement.Displayed.Should().BeTrue($"it means that '{element.Name}' element is not visible");
            }
        }

        public IWebElement GetWebElement(string elementName)
        {
            return FindWebElement(_webElementsDictionary[elementName]);
        }

        private void InitWebElementsDictionary(IEnumerable<Element> elements)
        {
            _webElementsDictionary = new Dictionary<string, string>();
            foreach (var element in elements)
            {
                _webElementsDictionary.Add(element.Name, element.Locator);
            }
        }

        private IWebElement FindWebElement(string locator)
        {
            try
            {
                return Driver.FindElement(By.XPath(locator));
            }
            catch
            {
                //TODO: should be logged
                return null;
            }
        }
    }
}