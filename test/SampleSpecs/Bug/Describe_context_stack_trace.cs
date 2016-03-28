#region [R# naming]

// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming

#endregion

using System;
using FluentAssertions;
using NSpectator;

namespace SampleSpecs.Bug
{
    public class Describe_context_stack_trace : Spec
    {
        bool isTrue = false;

        void Exception_thrown_in_act()
        {
            act = () =>
            {
                MethodThrowsExceptionAndShouldBeInStackTrace();

                isTrue = true;
            };

            it["is true"] = () => isTrue.Should().BeTrue();
        }

        void MethodThrowsExceptionAndShouldBeInStackTrace()
        {
            throw new InvalidOperationException("Exception in act.");
        }
    }

    public static class Describe_context_stack_trace_output
    {
        public static string Output = @"
describe context stack trace
  exception thrown in act
    is true - FAILED - Expected: True, But was: False

**** FAILURES ****

spec. describe context stack trace. exception thrown in act. is true.
Context Failure: Exception in act., Example Failure: Expected: True, But was: False

1 Examples, 1 Failed, 0 Pending
";
        public static int ExitCode = 1;
    }
}