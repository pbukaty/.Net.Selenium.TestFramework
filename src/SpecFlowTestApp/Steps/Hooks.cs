using System;
using System.IO;
using Allure.Commons;
using Allure.NUnit.Attributes;
using BoDi;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TestFramework.Factory;
using TestFramework.Utils;

namespace SpecFlowTestApp.Steps
{
    [Binding]
    public class Hooks
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly IObjectContainer _objectContainer;

        private IWebDriver _driver;
        private WebDriverUtils _driverUtils;

        private static string _driverName;

        private static string AllureConfigDir = $"{AppDomain.CurrentDomain.BaseDirectory}";

        public Hooks(IObjectContainer objectContainer, ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext ?? throw new ArgumentNullException(nameof(scenarioContext));
            _objectContainer = objectContainer;
        }

        [OneTimeSetUp]
        public void SetupForAllure()
        {
            Environment.CurrentDirectory = Path.GetDirectoryName(GetType().Assembly.Location);
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            _driverName = Environment.GetEnvironmentVariable("driver");
            AllureLifecycle.Instance.CleanupResultDirectory();
        }

        // [BeforeFeature]
        // public static void BeforeFeature(FeatureContext featureContext)
        // {
        // }

        // [AllureStep]
        [BeforeScenario]
        public void BeforeScenario(ScenarioContext scenarioContext)
        {
            //TODO: need for debug
            _driverName = "chrome";
            //=====================

            try
            {
                _driver = new WebDriverFactory().SetWebDriver(_driverName);
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

            _scenarioContext.Set(_driver, "driver");
            _driverUtils = new WebDriverUtils(_driver);

            AllureLifecycle.Instance.SetCurrentTestActionInException(() =>
            {
                AllureLifecycle.Instance.AddAttachment("Step screenshot", AllureLifecycle.AttachFormat.ImagePng,
                    _driverUtils.TakeScreenshot());
            });
        }

        [AfterScenario]
        [TearDown]
        [AllureStep("Close all WebDriver instances")]
        public void AfterScenario(ScenarioContext scenarioContext)
        {
            _driver.Quit();
        }
    }
}