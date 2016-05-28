#region [R# naming]

// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming

#endregion

using System;
using NSpectator;
using FluentAssertions;

namespace SampleSpecs.WebSite
{
    public class Describe_changing_failure_exception : Spec
    {
        void given_a_context_that_throws_an_exception()
        {
            It["the exception can be changed to provide out of proc information"] = () => "1".Should().Be("2");
        }

        public override Exception ExceptionToReturn(Exception originalException)
        {
            return new InvalidOperationException("A more detailed exception message.", originalException);
        }
    }

    public static class Describe_changing_failure_exception_output
    {
        public static string Output = @"
describe changing failure exception
  given a context that throws an exception
    the exception can be changed to provide out of proc information - FAILED - A more detailed exception message.

**** FAILURES ****

spec. describe changing failure exception. given a context that throws an exception. the exception can be changed to provide out of proc information.
A more detailed exception message.

1 Examples, 1 Failed, 0 Pending
";
        public static int ExitCode = 1;
    }
}