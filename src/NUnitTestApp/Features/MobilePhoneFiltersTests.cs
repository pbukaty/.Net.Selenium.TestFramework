using Allure.NUnit.Attributes;
using NUnit.Framework;

namespace NUnitTestApp.Features
{
    [AllureSuite("Mobile phone filters tests")]
    [Parallelizable]
    public class MobilePhoneFiltersTests : Hooks
    {
        [TestCase(TestName = "Verify Mobile Page Filters")]
        public void VerifyMobilePageFilters()
        {
            Perform("NavigateToPage", "https://catalog.onliner.by/mobile");
            Perform("VerifyPageIsLoaded", "MobilePhonesPage");
            Perform("SetElementSelectedState", "xiaomiCheckBox", true);
            Perform("VerifyElementIsSelected", "xiaomiCheckBox");
            Perform("ElementSendKeys", "maxAmountTextBox", "300");
            Perform("SelectItemFromDropdown", "screenSizeMaxDropDown", "5.8\"", "text");
            Perform("SelectItemFromDropdown", "ramMinDropDown", "2gb", "value");
            Perform("SelectItemFromDropdown", "screenResolutionMinDropDown", "18", "index");
        }
    }
}