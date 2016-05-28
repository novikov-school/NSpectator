#region [R# naming]
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
#endregion

using FluentAssertions;
using NSpectator;

namespace SampleSpecs.WebSite
{
    public class Describe_pending : Spec
    {
        void when_creating_pending_specifications()
        {
            It["pending spec"] = Todo;
            //or just add an 'x' at the beginning of a specification that isn't quite ready
            xIt["\"\" should be \"something else\""] = () => "".Should().Be("something else");
        }
    }

    public static class Describe_pending_output
    {
        public static string Output = @"
describe pending
  when creating pending specifications
    pending spec - PENDING
    """" should be ""something else"" - PENDING

2 Examples, 0 Failed, 2 Pending
";
        public static int ExitCode = 0;
    }
}