using System;
using System.Diagnostics;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

// ReSharper disable CheckNamespace

namespace NSpectator
{
    [DebuggerNonUserCode]
    public class DateTimeExpectations
    {
        public DateTimeAssertions To { get; }

        public DateTime? Subject => To.Subject;

        public DateTimeExpectations(DateTime actual)
        {
            To = new DateTimeAssertions(actual);
        }

        public AndConstraint<DateTimeAssertions> ToBeCloseTo(DateTime expected, DateTime tolerance)
        {
            return ToBeCloseTo(expected, tolerance, string.Empty);
        }

        public AndConstraint<DateTimeAssertions> ToBeCloseTo(DateTime expected, DateTime tolerance, string because)
        {
            Execute.Assertion
                .ForCondition(Subject.HasValue && ((Subject.Value - expected).Ticks <= tolerance.Ticks))
                .BecauseOf(because)
                .FailWith("Expected {context:date and time} to be close to {0} with tolerance {1}{reason}, but found {2}.",
                    expected, tolerance, Subject.HasValue ? Subject.Value : default(DateTime?));

            return new AndConstraint<DateTimeAssertions>(To);
        }
    }
}