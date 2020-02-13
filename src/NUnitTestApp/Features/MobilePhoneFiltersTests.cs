using NUnit.Framework;

namespace NUnitTestApp.Features
{
    [TestFixture]
    [Parallelizable]
    public class MobilePhoneFiltersTests : Hooks
    {
        [Test]
        public void VerifyMobilePageFilters()
        {
            PageActions.NavigateToPage("https://catalog.onliner.by/mobile");
            PageActions.VerifyPageIsLoaded("MobilePhonesPage");
            PageActions.SetElementSelectedState("xiaomiCheckBox", true);
            PageActions.VerifyElementIsSelected("xiaomiCheckBox");
        }
    }
}