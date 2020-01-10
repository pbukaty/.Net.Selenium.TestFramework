using TestFramework.Factory;
using TestFramework.PageActions;
using Xunit;

namespace Tests
{
    public class XUnitTest
    {

        [Fact]
        public void TestMethod()
        {
            var pageName = "CatalogPage";

            using (var driver = new WebDriverFactory().SetWebDriver("chrome"))
            {
                //TODO: should be moved to pre-requisites
                var pageActions = new PageActions(driver);
                driver.Manage().Window.Maximize();

                pageActions.NavigateToPage(driver, "https://catalog.onliner.by/", pageName);
                pageActions.ElementClick("electronics");
                pageActions.VerifyElementsAreDisplayed("mobilePhones&accessories/tv&video/tablets&ebooks".Split('/'));

                pageActions.MoveToElement("mobilePhones&accessories");
                pageActions.ElementClick("mobilePhones");
                pageActions.VerifyPageIsLoaded(driver, "MobilePhonesPage");
            }
        }
    }
}