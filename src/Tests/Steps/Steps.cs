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
        private readonly PageActions _pageActions;

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
        [When(@"I click on '(.*)' element")]
        public void GivenIClickOn(string elementName)
        {
            _pageActions.ElementClick(elementName);
        }

        [When(@"I move cursor to '(.*)' element")]
        public void ThenIMoveCursorTo(string elementName)
        {
            _pageActions.MoveCursorToElement(elementName);
        }

        [When(@"I set '(.*)' element selected state to '(.*)'")]
        public void WhenISetElementSelectedStateTo(string elementName, bool state)
        {
            _pageActions.SetElementSelectedState(elementName, state);
        }

        [When(@"I type '(.*)' into element '(.*)'")]
        public void WhenITypeIntoElement(string text, string elementName)
        {
            _pageActions.ElementSendKeys(elementName, text);
        }

        [When(@"I select '(.*)' item from '(.*)' dropdown by '(.*)' selection type")]
        public void WhenISelectItemFromDropdown(string item, string elementName, string selectBy)
        {
            _pageActions.SelectItemFromDropdown(elementName, item, selectBy);
        }

        [Then(@"I verify that '(.*)' page is loaded")]
        public void ThenIVerifyThatPageIsLoaded(string pageName)
        {
            _pageActions.VerifyPageIsLoaded(pageName);
        }

        [Then(@"I verify '(.*)' element is exist")]
        public void ThenIVerifyElementIsExist(string elementName)
        {
            _pageActions.VerifyElementIsExist(elementName);
        }

        [Then(@"I verify '(.*)' element is not exist")]
        public void ThenIVerifyElementIsNotExist(string elementName)
        {
            _pageActions.VerifyElementIsExist(elementName, false);
        }

        [Then(@"I verify that '(.*)' element is displayed")]
        public void ThenIVerifyThatElementIsDisplayed(string elementName)
        {
            _pageActions.VerifyElementIsDisplayed(elementName);
        }

        [Then(@"I verify that elements are displayed:")]
        public void ThenIVerifyThatElementsAreDisplayed(Table table)
        {
            var elementNames = table.Rows[0].Values.ToList();
            _pageActions.VerifyElementsAreDisplayed(elementNames);
        }

        [Then(@"I verify that '(.*)' element is not displayed")]
        public void ThenIVerifyThatElementIsNotDisplayed(string elementName)
        {
            _pageActions.VerifyElementIsDisplayed(elementName, false);
        }

        [Then(@"I verify that elements are not displayed:")]
        public void ThenIVerifyThatElementsAreNotDisplayed(Table table)
        {
            var elementNames = table.Rows[0].Values.ToList();
            _pageActions.VerifyElementsAreDisplayed(elementNames, false);
        }

        [Then(@"I verify that '(.*)' element is enabled")]
        public void ThenIVerifyThatElementIsEnabled(string elementName)
        {
            _pageActions.VerifyElementIsEnabled(elementName);
        }

        [Then(@"I verify that elements are enabled:")]
        public void ThenIVerifyThatElementsAreEnabled(Table table)
        {
            var elementNames = table.Rows[0].Values.ToList();
            _pageActions.VerifyElementsAreEnabled(elementNames);
        }

        [Then(@"I verify that '(.*)' element is not enabled")]
        public void ThenIVerifyThatElementIsNotEnabled(string elementName)
        {
            _pageActions.VerifyElementIsEnabled(elementName, false);
        }

        [Then(@"I verify that elements are not enabled:")]
        public void ThenIVerifyThatElementsAreNotEnabled(Table table)
        {
            var elementNames = table.Rows[0].Values.ToList();
            _pageActions.VerifyElementsAreEnabled(elementNames, false);
        }

        [Then(@"I verify that '(.*)' element is selected")]
        public void ThenIVerifyThatElementIsSelected(string elementName)
        {
            _pageActions.VerifyElementIsSelected(elementName);
        }

        [Then(@"I verify that elements are selected:")]
        public void ThenIVerifyThatElementsAreSelected(Table table)
        {
            var elementNames = table.Rows[0].Values.ToList();
            _pageActions.VerifyElementsAreSelected(elementNames);
        }

        [Then(@"I verify that '(.*)' element is not selected")]
        public void ThenIVerifyThatElementIsNotSelected(string elementName)
        {
            _pageActions.VerifyElementIsSelected(elementName, false);
        }

        [Then(@"I verify that elements are not selected:")]
        public void ThenIVerifyThatElementsAreNotSelected(Table table)
        {
            var elementNames = table.Rows[0].Values.ToList();
            _pageActions.VerifyElementsAreSelected(elementNames, false);
        }
    }
}