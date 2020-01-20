using TestFramework.Factory;
using TestFramework.PageActions;
using Xunit;

namespace Tests
{
    //TODO: remove this class
    public class XUnitTest
    {

        [Fact]
        public void TestMethod()
        {
            using (var driver = new WebDriverFactory().SetWebDriver("chrome"))
            {
                //TODO: should be moved to pre-requisites
                var pageActions = new PageActions(driver);
                driver.Manage().Window.Maximize();

                pageActions.NavigateToPage("https://catalog.onliner.by/");
                pageActions.ElementClick("electronics");
                pageActions.VerifyElementsAreDisplayed("mobilePhones&accessories/tv&video/tablets&ebooks".Split('/'));

                pageActions.MoveCursorToElement("mobilePhones&accessories");
                pageActions.ElementClick("mobilePhones");
                pageActions.VerifyPageIsLoaded("MobilePhonesPage");
            }
        }
    }
}