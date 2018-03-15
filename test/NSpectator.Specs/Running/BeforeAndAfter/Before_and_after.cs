#region [R# naming]
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
#endregion
using NUnit.Framework;
using FluentAssertions;

namespace NSpectator.Specs.Running.BeforeAndAfter
{
    class Sequence_spec : Spec { public static string sequence; }

    [TestFixture]
    [Category("RunningSpecs")]
    public class Before_and_after : When_running_specs
    {
        class SpecClass : Sequence_spec
        {
            void as_long_as_the_world_has_not_come_to_an_end()
            {
                BeforeAll = () => sequence = "A";
                Before = () => sequence += "B";
                It["spec 1"] = () => sequence += "1";
                It["spec 2"] = () => sequence += "2"; //two specs cause before_each and after_each to run twice
                After = () => sequence += "C";
                AfterAll = () => sequence += "D";
            }
        }

        [Test]
        public void everything_runs_in_the_correct_order_and_with_the_correct_frequency()
        {
            Run(typeof(SpecClass));

            Sequence_spec.sequence.Should().Be("AB1CB2CD");
        }
    }

    [TestFixture]
    [Category("RunningSpecs")]
    public class Before_and_after_aliases : When_running_specs
    {
        class SpecClass : Sequence_spec
        {
            void as_long_as_the_world_has_not_come_to_an_end()
            {
                BeforeAll = () => sequence = "A";
                BeforeEach = () => sequence += "B";
                It["spec 1"] = () => sequence += "1";
                It["spec 2"] = () => sequence += "2"; //two specs cause before_each and after_each to run twice
                AfterEach = () => sequence += "C";
                AfterAll = () => sequence += "D";
            }
        }

        [Test]
        public void everything_runs_in_the_correct_order_and_with_the_correct_frequency()
        {
            Run(typeof(SpecClass));

            Sequence_spec.sequence.Should().Be("AB1CB2CD");
        }
    }
}
