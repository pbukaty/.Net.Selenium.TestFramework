using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using OpenQA.Selenium;

namespace TestFramework.Extensions
{
    public class WebElementAssertions : ReferenceTypeAssertions<IWebElement, WebElementAssertions>
    {
        public WebElementAssertions(IWebElement instance) => Subject = instance;

        protected override string Identifier => "WebElementAssertions";

        [CustomAssertion]
        public AndConstraint<WebElementAssertions> BeFound(string elementName, string additionalInfo, string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(Subject != null)
                .FailWith($"Element '{elementName}' is not found. {additionalInfo}");

            return new AndConstraint<WebElementAssertions>(this);
        }

        [CustomAssertion]
        public AndConstraint<WebElementAssertions> NotBeFound(string elementName, string additionalInfo, string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(Subject == null)
                .FailWith($"Element '{elementName}' is found but should not. {additionalInfo}");

            return new AndConstraint<WebElementAssertions>(this);
        }

        [CustomAssertion]
        public AndConstraint<WebElementAssertions> BeDisplayed(string elementName, string additionalInfo, string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(Subject.Displayed)
                .FailWith($"Element '{elementName}' is not displayed. {additionalInfo}");

            return new AndConstraint<WebElementAssertions>(this);
        }

        [CustomAssertion]
        public AndConstraint<WebElementAssertions> NotBeDisplayed(string elementName, string additionalInfo, string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(Subject.Displayed == false)
                .FailWith($"Element '{elementName}' is displayed but should not. {additionalInfo}");

            return new AndConstraint<WebElementAssertions>(this);
        }

        [CustomAssertion]
        public AndConstraint<WebElementAssertions> BeEnabled(string elementName, string additionalInfo, string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(Subject.Enabled)
                .FailWith($"Element '{elementName}' is not enabled. {additionalInfo}");

            return new AndConstraint<WebElementAssertions>(this);
        }

        [CustomAssertion]
        public AndConstraint<WebElementAssertions> NotBeEnabled(string elementName, string additionalInfo, string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(Subject.Enabled == false)
                .FailWith($"Element '{elementName}' is enabled but should not. {additionalInfo}");

            return new AndConstraint<WebElementAssertions>(this);
        }

        [CustomAssertion]
        public AndConstraint<WebElementAssertions> BeSelected(string elementName, string additionalInfo, string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(Subject.Selected)
                .FailWith($"Element '{elementName}' is not selected. {additionalInfo}");

            return new AndConstraint<WebElementAssertions>(this);
        }

        [CustomAssertion]
        public AndConstraint<WebElementAssertions> NotBeSelected(string elementName, string additionalInfo, string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(Subject.Selected == false)
                .FailWith($"Element '{elementName}' is selected but should not. {additionalInfo}");

            return new AndConstraint<WebElementAssertions>(this);
        }
    }
}