#region [R# naming]

// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming

#endregion

using System;
using NSpectator;

namespace SampleSpecs.WebSite
{
    public class Describe_changing_stacktrace_message : Spec
    {
        void given_a_context_that_throws_an_exception()
        {
            it["the stack trace can be altered to provide more information"] = () => { throw new InvalidOperationException("An exception was thrown"); };
        }

        public override string StackTraceToPrint(string flattenedStackTrace)
        {
            return flattenedStackTrace + "More Information to help diagnose issue\n";
        }
    }

    public static class Describe_changing_stacktrace_message_output
    {
        public static string Output = @"
describe changing stacktrace message
  given a context that throws an exception
    the stack trace can be altered to provide more information - FAILED - An exception was thrown

**** FAILURES ****

spec. describe changing stacktrace message. given a context that throws an exception. the stack trace can be altered to provide more information.
An exception was thrown
More Information to help diagnose issue

1 Examples, 1 Failed, 0 Pending
";
        public static int ExitCode = 1;
    }
}