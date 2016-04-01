using NSpectator;
using SampleSpecs.Model;
using FluentAssertions;

public class Describe_helpers : Spec
{
    Tea tea;

    void when_making_tea()
    {
        context["that is 210 degrees"] = () =>
        {
            before = () => MakeTea(210);
            it["should be hot"] = () => tea.Taste().Should().Be("hot");
        };
        context["that is 90 degrees"] = () =>
        {
            before = () => MakeTea(90);
            it["should be cold"] = () => tea.Taste().Should().Be("cold");
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