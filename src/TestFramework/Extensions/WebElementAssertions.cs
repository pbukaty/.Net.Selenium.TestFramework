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
        public AndConstraint<WebElementAssertions> BeFound(string additionalInfo, string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(Subject != null)
                .FailWith($"Element is not found. {additionalInfo}");

            return new AndConstraint<WebElementAssertions>(this);
        }

        [CustomAssertion]
        public AndConstraint<WebElementAssertions> NotBeFound(string additionalInfo, string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(Subject == null)
                .FailWith($"Element is found but should not. {additionalInfo}");

            return new AndConstraint<WebElementAssertions>(this);
        }

        [CustomAssertion]
        public AndConstraint<WebElementAssertions> BeDisplayed(string additionalInfo, string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(Subject.Displayed)
                .FailWith($"Element is not displayed. {additionalInfo}");

            return new AndConstraint<WebElementAssertions>(this);
        }

        [CustomAssertion]
        public AndConstraint<WebElementAssertions> NotBeDisplayed(string additionalInfo, string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(Subject.Displayed == false)
                .FailWith($"Element is displayed but should not. {additionalInfo}");

            return new AndConstraint<WebElementAssertions>(this);
        }

        [CustomAssertion]
        public AndConstraint<WebElementAssertions> BeEnabled(string additionalInfo, string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(Subject.Enabled)
                .FailWith($"Element is not enabled. {additionalInfo}");

            return new AndConstraint<WebElementAssertions>(this);
        }

        [CustomAssertion]
        public AndConstraint<WebElementAssertions> NotBeEnabled(string additionalInfo, string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(Subject.Enabled == false)
                .FailWith($"Element is enabled but should not. {additionalInfo}");

            return new AndConstraint<WebElementAssertions>(this);
        }

        [CustomAssertion]
        public AndConstraint<WebElementAssertions> BeSelected(string additionalInfo, string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(Subject.Selected)
                .FailWith($"Element is not selected. {additionalInfo}");

            return new AndConstraint<WebElementAssertions>(this);
        }

        [CustomAssertion]
        public AndConstraint<WebElementAssertions> NotBeSelected(string additionalInfo, string because = "", params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .ForCondition(Subject.Selected == false)
                .FailWith($"Element is selected but should not. {additionalInfo}");

            return new AndConstraint<WebElementAssertions>(this);
        }
    }
}