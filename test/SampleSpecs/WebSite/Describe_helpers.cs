#region [R# naming]
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
#endregion
using FluentAssertions;
using NSpectator;
using SampleSpecs.Model;

namespace SampleSpecs.WebSite
{
    public class Describe_helpers : Spec
    {
        Tea tea;

        void when_making_tea()
        {
            Context["that is 210 degrees"] = () =>
            {
                Before = () => MakeTea(210);
                It["should be hot"] = () => tea.Taste().Should().Be("hot");
            };
            Context["that is 90 degrees"] = () =>
            {
                Before = () => MakeTea(90);
                It["should be cold"] = () => tea.Taste().Should().Be("cold");
            };
        }

        //helper methods do not have underscores
        void MakeTea(int temperature)
        {
            tea = new Tea(temperature);
        }
    }

    public static class Describe_helpers_output
    {
        public static string Output = @"
describe helpers
  when making tea
    that is 210 degrees
      should be hot
    that is 90 degrees
      should be cold

2 Examples, 0 Failed, 0 Pending
";
        public static int ExitCode = 0;
    }
}