using System.Collections.Generic;
using FluentAssertions;
using OpenQA.Selenium;

namespace TestFramework.Pages
{
    public class CatalogPage: BasePage
    {
//        private const string ElectronikaXpath = "//li//span[contains(@class, 'catalog-navigation-classifier') AND text()='Электроника']";
        private const string ElectronikaXpath = "//li//span[text()='Электроника']";

        private Dictionary<string, string> _elements;

        public CatalogPage(IWebDriver driver) : base(driver)
        {
            _elements = new Dictionary<string, string>();
        }

        public void PageLoadedVerification(List<IWebElement> elements)
        {
            foreach (var element in elements)
            {

                element.Displayed.Should().BeTrue();
            }
        }

        public void InitElements()
        {
            _elements = new Dictionary<string, string>();
        }

        public void Click()
        {
            var element = Driver.FindElement(By.XPath(ElectronikaXpath));
            element.Click();
        }
    }
}