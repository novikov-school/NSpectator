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
    public class Describe_exception : Spec
    {
        string nullString = null;

        void given_a_null_string()
        {
            it["should throw null-ref"] =
                expect<NullReferenceException>(() => nullString.Trim());
        }
    }

    public static class Describe_exception_output
    {
        public static string Output = @"
describe exception
  given a null string
    should throw null-ref

1 Examples, 0 Failed, 0 Pending
";
        public static int ExitCode = 0;
    }
}