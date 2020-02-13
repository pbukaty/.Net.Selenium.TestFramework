using System;
using NUnit.Framework;
using OpenQA.Selenium;
using TestFramework.Factory;
using TestFramework.PageActions;
using TestFramework.Utils;

namespace NUnitTestApp.Features
{
    [TestFixture]
    public class Hooks
    {
        public IWebDriver Driver { get; private set; }
        public PageActions PageActions { get; private set; }

        private WebDriverUtils _driverUtils;

        [SetUp]
        public void BeforeTest()
        {
            var driverName = Environment.GetEnvironmentVariable("driver");
            driverName = "chrome";

            try
            {
                Driver = new WebDriverFactory().SetWebDriver(driverName);
            }
            catch (ArgumentNullException ex)
            {
                Assert.Fail(ex.Message);
            }
            catch (NotSupportedException ex)
            {
                Assert.Fail(ex.Message);
            }
            catch (DriverServiceNotFoundException ex)
            {
                Assert.Fail(ex.Message);
            }

            Driver.Manage().Window.Maximize();

            _driverUtils = new WebDriverUtils(Driver);
            PageActions = new PageActions(Driver);
        }

        [TearDown]
        public void AfterTest()
        {
            Driver.Quit();
        }
    }
}