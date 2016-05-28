#region [R# naming]
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSpectator;

namespace SampleSpecsFocus
{
    public class Describe_focus : Spec
    {
        [Tag("focus")]
        void It_is_run() { }

        void It_is_not_run() { }
    }

    public static class Describe_focus_output
    {
        public static string Output = @"
describe focus
  It is run

1 Examples, 0 Failed, 0 Pending

NSpectator found context/examples tagged with ""focus"" and only ran those.
";
    }
}