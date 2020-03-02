using System;
using Allure.Commons;
using Allure.NUnit.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using TestFramework.Factory;
using TestFramework.PageActions;
using TestFramework.Utils;

namespace NUnitTestApp.Features
{
    [TestFixture]
    public class BaseTest : AllureReport
    {
        protected PageActions PageActions { get; private set; }
        private IWebDriver Driver { get; set; }

        [SetUp]
        [AllureStep("Setup WebDriver")]
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

            PageActions = new PageActions(Driver);

            AllureLifecycle.Instance.SetCurrentTestActionInException(() =>
            {
                AllureLifecycle.Instance.AddAttachment("Step screenshot", AllureLifecycle.AttachFormat.ImagePng,
                    new WebDriverUtils(Driver).TakeScreenshot());
            });
        }

        [TearDown]
        [AllureStep("Close all WebDriver instances")]
        public void QuitDriver()
        {
            Driver.Quit();
        }
    }
}