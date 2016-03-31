using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace NSpectator
{
    public class BooleanExpectations
    {
        public BooleanAssertions To { get; }

        public BooleanExpectations(bool? subject)
        {
            To = new BooleanAssertions(subject);
        }

        public AndConstraint<BooleanAssertions> True()
        {
            return To.BeTrue();
        }

        public AndConstraint<BooleanAssertions> False()
        {
            return To.BeFalse();
        }
    }
}