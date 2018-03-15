#region [R# naming]
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
#endregion
using System.Text.RegularExpressions;
using NSpectator.Domain;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace NSpectator.Specs
{
    public class ExampleBaseAssertions : ReferenceTypeAssertions<ExampleBase, ExampleBaseAssertions>
    {
        public ExampleBaseAssertions(ExampleBase example)
        {
            Subject = example;
        }

        public AndConstraint<ExampleBaseAssertions> HavePassed(string because = "")
        {
            Execute.Assertion.ForCondition(this.Subject.HasRun && this.Subject.Exception == null).BecauseOf(because).FailWith("Expected example to have passed {reason}");
            return new AndConstraint<ExampleBaseAssertions>(this);
        }

        public AndConstraint<ExampleBaseAssertions> HaveFailed(string because = "")
        {
            Execute.Assertion.ForCondition(!this.Subject.HasRun || this.Subject.Exception != null).BecauseOf(because).FailWith("Expected example to have failed {reason}");
            return new AndConstraint<ExampleBaseAssertions>(this);
        }

        protected override string Identifier { get; } = "ExampleBase";
    }

    public static class SpecExtensions
    {
        public static ExampleBaseAssertions Should(this ExampleBase example)
        {
            return new ExampleBaseAssertions(example);
        }

        public static string RegexReplace(this string input, string pattern, string replace)
        {
            return Regex.Replace(input, pattern, replace);
        }
    }
}