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
    [TestFixture]
    public class Nested_contexts : When_running_specs
    {
        class SpecClass : Sequence_spec
        {
            void a_context()
            {
                BeforeAll = () => sequence = "A";
                Before = () => sequence += "C";

                Context["a subcontext"] = () =>
                {
                    BeforeAll = () => sequence += "B";
                    Before = () => sequence += "D";

                    specify = () => 1.Expected().ToBe(1);

                    After = () => sequence += "E";
                    AfterAll = () => sequence += "G";
                };

                After = () => sequence += "F";
                AfterAll = () => sequence += "H";
            }
        }

        [SetUp]
        public void Setup()
        {
            Run(typeof(SpecClass));
        }

        [Test]
        public void before_alls_at_every_level_run_before_before_eaches_from_the_outside_in()
        {
            Sequence_spec.sequence.Should().StartWith("ABCD");
        }

        [Test]
        public void after_alls_at_every_level_run_after_after_eaches_from_the_inside_out()
        {
            Sequence_spec.sequence.Should().EndWith("EFGH");
        }
    }
}