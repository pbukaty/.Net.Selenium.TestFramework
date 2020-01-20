using OpenQA.Selenium;

namespace TestFramework.Extensions
{
    public static class WebElementExtensions
    {
        public static WebElementAssertions Should(this IWebElement instance)
        {
            return new WebElementAssertions(instance);
        }
    }
}