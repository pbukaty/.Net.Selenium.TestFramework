using System;
using System.Collections.Generic;
using System.Linq;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.MarkupUtils;
using AventStack.ExtentReports.Reporter;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TestFramework.Factory;
using TestFramework.Utils;
using Reports = AventStack.ExtentReports.ExtentReports;

namespace Tests.Steps
{
    [Binding]
    public class Hooks
    {
        private readonly ScenarioContext _scenarioContext;
        private IWebDriver _driver;

        [ThreadStatic]
        private static ExtentTest _feature;
        [ThreadStatic]
        private static ExtentTest _scenario;
        [ThreadStatic]
        private static ExtentTest _step;
        private static Reports _extent;
        private static string _reportPath;
        private static string _reportName;

        private WebDriverUtils _driverUtils;

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext ?? throw new ArgumentNullException(nameof(scenarioContext));
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            _reportName = $"Report_{DateTime.Now:dd-MM-yyyy_HHmmss}.html";

            _reportPath = $"{AppDomain.CurrentDomain.BaseDirectory}TestResults\\";
            FileUtils.CreateDirectory(_reportPath);

            var htmlReporter = new ExtentHtmlReporter(_reportPath);
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;

            _extent = new Reports();
            _extent.AttachReporter(htmlReporter);
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            _feature = _extent.CreateTest<Feature>(featureContext.FeatureInfo.Title);
            if (featureContext.FeatureInfo.Tags.Contains("ignoreFeature"))
            {
                _feature.Model.Status = Status.Skip;
                Assert.Ignore("Ignore feature");
            }
        }

        [BeforeScenario]
        public void BeforeScenario(ScenarioContext scenarioContext)
        {
            _scenario = _feature.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);

            if (scenarioContext.ScenarioInfo.Tags.Contains("ignoreScenario"))
            {
                _scenario.Model.Status = Status.Skip;
                Assert.Ignore("Ignore scenario");
            }

            _driver = new WebDriverFactory().SetWebDriver("chrome");
            _scenarioContext.Set(_driver, "driver");

            _driverUtils = new WebDriverUtils(_driver);
        }

        [AfterStep]
        public void InsertReportingSteps(ScenarioContext scenarioContext)
        {
            if (scenarioContext.TestError == null)
            {
                CreatePassNode(scenarioContext);
            }
            else
            {
                CreateFailNode(scenarioContext);
            }

            SetStepInfo(scenarioContext);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            _driver?.Quit();

            //TODO: doesn't work
            _scenario.Info($"Duration: {_scenario.Model.RunDuration}");
        }

        [AfterFeature]
        public static void AfterFeature()
        {
            //TODO: doesn't work
            _feature.Info($"Duration: {_feature.Model.RunDuration}");
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            //Flush report once test completes
            _extent.Flush();

            FileUtils.RenameFile(_reportPath, "index.html", _reportName);
            FileUtils.RenameFile(_reportPath, "dashboard.html", $"Dashboard_{_reportName}");
        }

        private void CreatePassNode(ScenarioContext scenarioContext)
        {
            var stepInfo = scenarioContext.StepContext.StepInfo;
            var stepType = stepInfo.StepInstance.Keyword;
            var stepDescription = $"{stepType}{stepInfo.Text}";

            switch (stepType.Trim())
            {
                case "Given":
                    _step = _scenario.CreateNode<Given>(stepDescription);
                    break;
                case "When":
                    _step = _scenario.CreateNode<When>(stepDescription);
                    break;
                case "Then":
                    _step = _scenario.CreateNode<Then>(stepDescription);
                    break;
                case "And":
                    _step = _scenario.CreateNode<And>(stepDescription);
                    break;
            }
        }

        private void CreateFailNode(ScenarioContext scenarioContext)
        {
            var stepInfo = scenarioContext.StepContext.StepInfo;
            var stepType = stepInfo.StepInstance.Keyword;
            var stepDescription = $"{stepType}{stepInfo.Text}";
            var errorType = scenarioContext.TestError.GetType().ToString();
            var errorMessage = scenarioContext.TestError.Message
                .Replace("<", "[")
                .Replace(">", "]");

            switch (stepType.Trim())
            {
                case "Given":
                    _step = _scenario.CreateNode<Given>(stepDescription).Fail($"{errorType}: {errorMessage}");
                    break;
                case "When":
                    _step = _scenario.CreateNode<When>(stepDescription).Fail($"{errorType}: {errorMessage}");
                    break;
                case "Then":
                    _step = _scenario.CreateNode<Then>(stepDescription).Fail($"{errorType}: {errorMessage}");
                    break;
                case "And":
                    _step = _scenario.CreateNode<And>(stepDescription).Fail($"{errorType}: {errorMessage}");
                    break;
            }

            AddScreenshot();
        }

        private void SetStepInfo(ScenarioContext scenarioContext)
        {
            var stepInfo = scenarioContext.StepContext.StepInfo;

            _step.Info($"Duration: {_step.Model.RunDuration.ToString()}.");

            if (stepInfo.Table != null)
            {
                _step.Info("Parameters:");
                _step.Info(MarkupHelper.CreateTable(ConvertTableToArray(stepInfo.Table)));
            }
        }

        private void AddScreenshot()
        {
            var screenshotName = $"{_scenario.Model.Name}_{DateTime.Now:dd-MM-yyyy_HHmmss}";
            _driverUtils.TakeScreenshot(screenshotName);

            var mediaModel =
                MediaEntityBuilder.CreateScreenCaptureFromPath($"{AppDomain.CurrentDomain.BaseDirectory}TestResults\\{screenshotName}.png").Build();
            _step.Fail("Details: ", mediaModel);
        }

        private string[,] ConvertTableToArray(Table table)
        {
            var data = new List<string[]>();

            foreach (var row in table.Rows)
            {
                data.Add(row.Keys as string[]);
                data.Add(row.Values as string[]);
            }

            var array = new string [data.Count, data[0].Length];
            for (var i = 0; i < data.Count; i++)
            {
                for (var j = 0; j < data[0].Length; j++)
                {
                    array[i, j] = data[i][j];
                }
            }

            return array;
        }
    }
}