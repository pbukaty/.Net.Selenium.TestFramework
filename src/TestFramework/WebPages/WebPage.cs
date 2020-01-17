using System.Collections.Generic;
using OpenQA.Selenium;
using TestFramework.Factory;
using TestFramework.Models;
using TestFramework.Readers;

namespace TestFramework.WebPages
{
    public class WebPage: BasePage
    {
        public string PageName { get; }
        public Dictionary<string, Properties> WebElementsDictionary { get; private set; }

        public WebPage(IWebDriver driver, string pageName) : base(driver)
        {
            var page = JsonDataReader.LoadElements($"{pageName}.json");
            InitWebElementsDictionary(page.Elements);
            PageName = pageName;
        }

        public IWebElement GetWebElement(string elementName)
        {
            return new WebElementFactory().FindWebElement(Driver, WebElementsDictionary[elementName]);
        }

        private void InitWebElementsDictionary(IEnumerable<Element> elements)
        {
            WebElementsDictionary = new Dictionary<string, Properties>();
            foreach (var element in elements)
            {
                WebElementsDictionary.Add(element.Name, element.Properties);
            }
        }
    }
}