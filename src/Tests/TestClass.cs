using TestFramework.Factory;
using TestFramework.Pages;
using Xunit;

namespace Tests
{
    public class UnitTest1
    {
        [Fact]
        public void TestMethod()
        {
            var driver = new WebDriverFactory().SetWebDriver("chrome");
            driver.Navigate().GoToUrl("https://catalog.onliner.by/");

            var catalogPage = new CatalogPage(driver);
            catalogPage.Click();
        }
    }
}