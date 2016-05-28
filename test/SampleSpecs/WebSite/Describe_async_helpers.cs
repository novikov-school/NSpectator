#region [R# naming]

// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming

#endregion

using System.Threading.Tasks;
using NSpectator;
using SampleSpecs.Model;
using FluentAssertions;

namespace SampleSpecs.WebSite
{
    public class Describe_async_helpers : Spec
    {
        Tea tea;

        void When_making_tea()
        {
            Context["that is 210 degrees"] = () =>
            {
                BeforeAsync = async () => await MakeTeaAsync(210);
                It["should be hot"] = () => tea.Taste().Should().Be("hot");
            };
            Context["that is 90 degrees"] = () =>
            {
                BeforeAsync = async () => await MakeTeaAsync(90);
                It["should be cold"] = () => tea.Taste().Should().Be("cold");
            };
        }

        //helper methods do not have underscores
        async Task MakeTeaAsync(int temperature)
        {
            tea = await Task.Run(() => new Tea(temperature));
        }
    }

    public static class Describe_async_helpers_output
    {
        public static string Output = @"
describe async helpers
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