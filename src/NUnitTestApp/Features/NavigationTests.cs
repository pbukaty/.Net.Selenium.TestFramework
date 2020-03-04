using System.Linq;
using Allure.NUnit.Attributes;
using NUnit.Framework;

namespace NUnitTestApp.Features
{
    [AllureSuite("Navigation tests")]
    [Parallelizable]
    public class NavigationTests : BaseTest
    {
        [TestCase(TestName = "Navigate to Mobile page")]
        public void NavigateToMobilePage()
        {
            PageActions.NavigateToPage("https://catalog.onliner.by/");
            PageActions.RefreshPage();
            PageActions.VerifyPageIsLoaded("catalogPage");
            PageActions.ElementClick("electronics");
            PageActions.VerifyElementsAreDisplayed("mobilePhones&accessories/tv&video/tablets&ebooks".Split('/').ToList());
            PageActions.MoveCursorToElement("mobilePhones&accessories");
            PageActions.ElementClick("mobilePhones");
            PageActions.VerifyPageIsLoaded("MobilePhonesPage");
        }

        [TestCase(TestName = "Navigate to Mobile page FAIL")]
        public void NavigateToMobilePage_FAIL()
        {
            PageActions.NavigateToPage("https://catalog.onliner.by/");
            PageActions.VerifyPageIsLoaded("catalogPage");
            PageActions.ElementClick("electronics");
            PageActions.VerifyElementsAreDisplayed("tv&video/mobilePhones&accessories_INVALID/tablets&ebooks".Split('/').ToList());
            PageActions.MoveCursorToElement("mobilePhones&accessories");
            PageActions.ElementClick("mobilePhones");
            PageActions.VerifyPageIsLoaded("MobilePhonesPage");
        }
    }
}