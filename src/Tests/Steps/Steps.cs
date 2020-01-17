using System;
using System.Linq;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TestFramework.PageActions;

namespace Tests.Steps
{
    [Binding]
    public class Steps
    {
        private readonly ScenarioContext _scenarioContext;
        private PageActions _pageActions;

        public Steps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext ?? throw new ArgumentNullException(nameof(scenarioContext));

            var driver = _scenarioContext.Get<IWebDriver>("driver");
            driver.Manage().Window.Maximize();

            _pageActions = new PageActions(driver);
        }

        [Given(@"I navigate to page '(.*)'")]
        public void GivenINavigateToUrlOnPage(string url)
        {
            _pageActions.NavigateToPage(url);
        }

        [Given(@"I click on '(.*)' element")]
        [Then(@"I click on '(.*)' element")]
        public void GivenIClickOn(string elementName)
        {
            _pageActions.ElementClick(elementName);
        }

        [Then(@"I verify that elements are displayed:")]
        public void ThenIVerifyThatElementsAreDisplayed(Table table)
        {
            var elementNames = table.Rows[0].Values.ToList();
            _pageActions.VerifyElementsAreDisplayed(elementNames);
        }

        [Then(@"I moved cursor to '(.*)' element")]
        public void ThenIMovedCursorTo(string elementName)
        {
            _pageActions.MoveToElement(elementName);
        }

        [Then(@"I verify that '(.*)' page is loaded")]
        public void ThenIVerifyThatPageIsLoaded(string pageName)
        {
            _pageActions.VerifyPageIsLoaded(pageName);
        }
    }
}