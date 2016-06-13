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
    public class Describe_class_level : Spec
    {
        string sequence;
        // before, act, and it can also be declared at the class level like so
        void before_each()
        {
            sequence = "arrange, ";
        }

        void act_each()
        {
            sequence += "act";
        }

        // prefixing a method with "it_" or "specify_"
        // should tells NSpectator to treat the method as an example
        void specify_given_befores_and_acts_run_in_the_correct_sequence()
        {
            sequence.Should().Be("arrange, act");
        }
    }

    public static class Describe_class_level_output
    {
        public static string Output = @"
describe class level
  specify given befores and acts run in the correct sequence

1 Examples, 0 Failed, 0 Pending
";
        public static int ExitCode = 0;
    }
}