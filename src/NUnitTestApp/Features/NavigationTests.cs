using System.Linq;
using Allure.NUnit.Attributes;
using NUnit.Framework;

namespace NUnitTestApp.Features
{
    [AllureSuite("Navigation tests")]
    [Parallelizable]
    public class NavigationTests : Hooks
    {
        [TestCase(TestName = "Navigate to Mobile page")]
        public void NavigateToMobilePage()
        {
            Perform("NavigateToPage", "https://catalog.onliner.by/");
            Perform("RefreshPage");
            Perform("VerifyPageIsLoaded", "catalogPage");
            Perform("ElementClick", "electronics");
            Perform("VerifyElementsAreDisplayed",
                "mobilePhones&accessories/tv&video/tablets&ebooks".Split('/').ToList());
            Perform("MoveCursorToElement", "mobilePhones&accessories");
            Perform("ElementClick", "mobilePhones");
            Perform("VerifyPageIsLoaded", "MobilePhonesPage");
        }

        [TestCase(TestName = "Navigate to Mobile page FAIL")]
        public void NavigateToMobilePage_FAIL()
        {
            Perform("NavigateToPage", "https://catalog.onliner.by/");
            Perform("VerifyPageIsLoaded", "catalogPage");
            Perform("ElementClick", "electronics");
            Perform("VerifyElementsAreDisplayed",
                "mobilePhones&accessories_INVALID/tv&video/tablets&ebooks".Split('/').ToList());
            Perform("MoveCursorToElement", "mobilePhones&accessories");
            Perform("ElementClick", "mobilePhones");
            Perform("VerifyPageIsLoaded", "MobilePhonesPage");
        }
    }
}