using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Allure.Commons;
using NUnit.Framework;
using OpenQA.Selenium;
using TestFramework.Factory;
using TestFramework.PageActions;
using TestFramework.Utils;

namespace NUnitTestApp.Features
{
    [TestFixture]
    public class Hooks : AllureReport
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

            _driverUtils = new WebDriverUtils(Driver);
            PageActions = new PageActions(Driver);

            AllureLifecycle.Instance.SetCurrentTestActionInException(() =>
            {
                AllureLifecycle.Instance.AddAttachment("Step screenshot", AllureLifecycle.AttachFormat.ImagePng,
                    _driverUtils.TakeScreenshot());
            });
        }

        [TearDown]
        public void AfterTest()
        {
            Driver.Quit();
        }

        protected void Perform(string methodName, params object[] parameters)
        {
            var method = PageActions.GetType().GetMethod(methodName);
            AllureLifecycle.Instance.RunStep($"{BuildStepDescription(methodName, parameters)}", () => method?.Invoke(PageActions, parameters));
        }

        private string BuildStepDescription(string methodName, params object[] parameters)
        {
            var stepDescription = new StringBuilder();
            stepDescription.Append(methodName);
            if (parameters.Length != 0)
            {
                stepDescription.Append(". Parameters: ").Append(BuildParametersString(parameters));
            }

            return stepDescription.ToString();
        }

        private string BuildParametersString(params object[] parameters)
        {
            var parametersString = new StringBuilder();
            foreach (var parameter in parameters)
            {
                if (parameter is IEnumerable && parameter.GetType() != typeof(string))
                {
                    foreach (var item in (IEnumerable<object>) parameter)
                    {
                        parametersString.Append(item).Append(";");
                    }
                }
                else
                {
                    parametersString.Append(parameter).Append(";");
                }
            }

            return parametersString.ToString();
        }
    }
}