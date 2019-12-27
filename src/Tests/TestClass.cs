using TestFramework.Actions;
using TestFramework.Factory;
using Xunit;

namespace Tests
{
    public class UnitTest1
    {
        private readonly PageActions _pageActions;

        public UnitTest1()
        {
            _pageActions = new PageActions();
        }

        [Fact]
        public void TestMethod()
        {
            var pageName = "CatalogPage";

            using (var driver = new WebDriverFactory().SetWebDriver("chrome"))
            {
                driver.Manage().Window.Maximize();

                //TODO: should be initialization new page "VerifyPageIsLoaded"
                _pageActions.NavigateToPage("https://catalog.onliner.by/", driver, pageName);
                _pageActions.ElementClick("electronics");
                _pageActions.VerifyElementsArePresent("mobilePhones&accessories|tv&video|tablets&ebooks");

                //TODO: click on any item and check that another page is loaded
                _pageActions.ElementClick("mobilePhones&accessories");
                _pageActions.ElementClick("mobilePhones");
                _pageActions.VerifyPageIsLoaded(driver, "MobilePhonesPage");
            }
        }
    }
}