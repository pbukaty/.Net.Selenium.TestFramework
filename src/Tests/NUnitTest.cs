using NUnit.Framework;
using TestFramework.Factory;
using TestFramework.PageActions;

namespace Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class NUnitTest
    {
        [Test]
        public void NavigateToMobilePage()
        {
            using (var driver = new WebDriverFactory().SetWebDriver("chrome"))
            {
                //TODO: should be moved to pre-requisites
                var pageActions = new PageActions(driver);
                driver.Manage().Window.Maximize();

                pageActions.NavigateToPage("https://catalog.onliner.by/");
                pageActions.VerifyPageIsLoaded("catalogPage");
                pageActions.ElementClick("electronics");
                pageActions.VerifyElementsAreDisplayed("mobilePhones&accessories/tv&video/tablets&ebooks".Split('/'));

                pageActions.MoveCursorToElement("mobilePhones&accessories");
                pageActions.ElementClick("mobilePhones");
                pageActions.VerifyPageIsLoaded("MobilePhonesPage");
            }
        }

        [Test]
        public void VerifyMobilePageFilters()
        {
            using (var driver = new WebDriverFactory().SetWebDriver("chrome"))
            {
                //TODO: should be moved to pre-requisites
                var pageActions = new PageActions(driver);
                driver.Manage().Window.Maximize();

                pageActions.NavigateToPage("https://catalog.onliner.by/mobile");
                pageActions.VerifyPageIsLoaded("MobilePhonesPage");
                pageActions.SetElementSelectedState("xiaomiCheckBox", true);
                pageActions.VerifyElementIsSelected("xiaomiCheckBox");
            }
        }
    }
}