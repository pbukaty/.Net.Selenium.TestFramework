using Allure.NUnit.Attributes;
using NUnit.Framework;

namespace NUnitTestApp.Features
{
    [AllureSuite("Mobile phone filters tests")]
    [Parallelizable]
    public class MobilePhoneFiltersTests : BaseTest
    {
        [TestCase(TestName = "Verify Mobile Page Filters")]
        public void VerifyMobilePageFilters()
        {
            PageActions.NavigateToPage("https://catalog.onliner.by/mobile");
            PageActions.VerifyPageIsLoaded("MobilePhonesPage");
            PageActions.SetElementSelectedState("xiaomiCheckBox", true);
            PageActions.VerifyElementIsSelected("xiaomiCheckBox");
            PageActions.ElementSendKeys("maxAmountTextBox", "300");
            PageActions.SelectItemFromDropdown("screenSizeMaxDropDown", "5.8\"", "text");
            PageActions.SelectItemFromDropdown("ramMinDropDown", "2gb", "value");
            PageActions.SelectItemFromDropdown("screenResolutionMinDropDown", "18", "index");
        }
    }
}