using System;
using System.Diagnostics;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
// ReSharper disable CheckNamespace

namespace NSpectator
{
    [DebuggerNonUserCode]
    public class SimpleTimeSpanExpectations
    {
        public SimpleTimeSpanAssertions To { get; }

        public TimeSpan? Subject => To.Subject;

        public SimpleTimeSpanExpectations(TimeSpan? value)
        {
            To = new SimpleTimeSpanAssertions(value);
        }

        public AndConstraint<SimpleTimeSpanAssertions> ToBeCloseTo(TimeSpan expected, TimeSpan tolerance, string because)
        {
            Execute.Assertion
                .ForCondition(Subject.HasValue && Math.Abs((Subject.Value - expected).Ticks) <= tolerance.Ticks)
                .BecauseOf(because)
                .FailWith("Expected value close to {0} with tolerance {1}{reason}, but found {2}", expected, tolerance, Subject.Value);

            return new AndConstraint<SimpleTimeSpanAssertions>(To);
        }

        public AndConstraint<SimpleTimeSpanAssertions> ToBeCloseTo(TimeSpan expected, TimeSpan tolerance)
        {
            return ToBeCloseTo(expected, tolerance, string.Empty);
        }
    }
}