using NUnit.Framework;

namespace NUnitTestApp.Features
{
    [TestFixture]
    [Parallelizable]
    public class NavigationTests : Hooks
    {
        [Test]
        public void NavigateToMobilePage()
        {
            PageActions.NavigateToPage("https://catalog.onliner.by/");
            PageActions.VerifyPageIsLoaded("catalogPage");
            PageActions.ElementClick("electronics");
            PageActions.VerifyElementsAreDisplayed(
                "mobilePhones&accessories/tv&video/tablets&ebooks".Split('/'));
            PageActions.MoveCursorToElement("mobilePhones&accessories");
            PageActions.ElementClick("mobilePhones");
            PageActions.VerifyPageIsLoaded("MobilePhonesPage");
        }

        [Test]
        public void NavigateToMobilePage_FAIL()
        {
            PageActions.NavigateToPage("https://catalog.onliner.by/");
            PageActions.VerifyPageIsLoaded("catalogPage");
            PageActions.ElementClick("electronics");
            PageActions.VerifyElementsAreDisplayed(
                "mobilePhones&accessories_INVALID/tv&video/tablets&ebooks".Split('/'));
            PageActions.MoveCursorToElement("mobilePhones&accessories");
            PageActions.ElementClick("mobilePhones");
            PageActions.VerifyPageIsLoaded("MobilePhonesPage");
        }
    }
}