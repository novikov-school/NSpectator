#region [R# naming]
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
#endregion
using NUnit.Framework;
using System.Threading.Tasks;
using FluentAssertions;

namespace NSpectator.Specs.Running.BeforeAndAfter
{
    [TestFixture]
    [Category("RunningSpecs")]
    [Category("Async")]
    public class Async_class_levels_and_context_methods : When_running_specs
    {
        class SpecClass : Sequence_spec
        {
            async Task before_all()
            {
                await Task.Run(() => sequence = "A");
            }

            async Task before_each()
            {
                await Task.Run(() => sequence += "C");
            }

            void a_context()
            {
                beforeAllAsync = async () => await Task.Run(() => sequence += "B");

                beforeAsync = async () => await Task.Run(() => sequence += "D");
                specify = () => 1.Is(1);
                afterAsync = async () => await Task.Run(() => sequence += "E");

                afterAllAsync = async () => await Task.Run(() => sequence += "G");
            }

            async Task after_each()
            {
                await Task.Run(() => sequence += "F");
            }

            async Task after_all()
            {
                await Task.Run(() => sequence += "H");
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
            Sequence_spec.sequence.should_start_with("ABCD");
        }

        [Test]
        public void after_alls_at_every_level_run_after_after_eaches_from_the_inside_out()
        {
            Sequence_spec.sequence.Should().EndWith("EFGH");
        }
    }
}
