using System;
using System.Linq;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TestFramework.Factory;
using TestFramework.PageActions;

namespace Tests.Steps
{
    [Binding]
    public class Steps
    {
        private readonly ScenarioContext _scenarioContext;
        private IWebDriver _driver;
        private PageActions _pageActions;

        public Steps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext ?? throw new ArgumentNullException(nameof(scenarioContext));

            _driver = new WebDriverFactory().SetWebDriver("chrome");
            _driver.Manage().Window.Maximize();

            _pageActions = new PageActions(_driver);
        }

        [Given(@"I navigate to page:")]
        public void GivenINavigateToUrlOnPage(Table table)
        {
            _pageActions.NavigateToPage(_driver, table.Rows[0]["url"], table.Rows[0]["pageName"]);
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
        public void ThenIVerifyThatIsLoaded(string pageName)
        {
            _pageActions.VerifyPageIsLoaded(_driver, pageName);
        }
    }
}