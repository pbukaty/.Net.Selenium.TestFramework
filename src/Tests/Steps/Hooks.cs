using System;
using System.Collections.Generic;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.MarkupUtils;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TestFramework.Factory;
using TestFramework.Utils;
//TODO: should be changed
using ExtentReports1 = AventStack.ExtentReports.ExtentReports;

namespace Tests.Steps
{
    [Binding]
    public class Hooks
    {
        private readonly ScenarioContext _scenarioContext;
        private IWebDriver _driver;

        private static ExtentTest _feature;
        private static ExtentTest _scenario;
        private static ExtentTest _step;
        private static ExtentReports1 _extent;
        private static string _reportPath;
        private static string _reportName;

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

            _extent = new ExtentReports1();
            _extent.AttachReporter(htmlReporter);
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            _feature = _extent.CreateTest<Feature>(featureContext.FeatureInfo.Title);
        }

        [BeforeScenario]
        public void BeforeScenario(ScenarioContext scenarioContext)
        {
            _scenario = _feature.CreateNode<Scenario>(scenarioContext.ScenarioInfo.Title);

            _driver = new WebDriverFactory().SetWebDriver("chrome");
            _scenarioContext.Set(_driver, "driver");
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
            _driver.Quit();

            _scenario.Info($"Duration: {_scenario.Model.RunDuration}");
        }

        [AfterFeature]
        public static void AfterFeature()
        {
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

            switch (stepType.Trim())
            {
                case "Given":
                    _step = _scenario.CreateNode<Given>(stepDescription).Fail(scenarioContext.TestError.Message);
                    break;
                case "When":
                    _step = _scenario.CreateNode<When>(stepDescription).Fail(scenarioContext.TestError.Message);
                    break;
                case "Then":
                    _step = _scenario.CreateNode<Then>(stepDescription).Fail(scenarioContext.TestError.Message);
                    break;
                case "And":
                    _step = _scenario.CreateNode<And>(stepDescription).Fail(scenarioContext.TestError.Message);
                    break;
            }
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