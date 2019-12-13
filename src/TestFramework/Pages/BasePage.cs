using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TestFramework.Factory;

namespace TestFramework.Pages
{
    public class BasePage
    {
        protected IWebDriver Driver;

        public BasePage(IWebDriver driver)
        {
            Driver = driver;
        }

        public void PageLoadedVerification()
        {

        }
    }
}