#region [R# naming]
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
#endregion
using NUnit.Framework;
using System.Threading.Tasks;

namespace NSpectator.Specs.Running.BeforeAndAfter
{
    [TestFixture]
    [Category("Async")]
    public class Async_nested_contexts : When_running_specs
    {
        class SpecClass : Sequence_spec
        {
            void a_context()
            {
                beforeAllAsync = async () => await Task.Run(() => sequence += "A");
                beforeAsync = async () => await Task.Run(() => sequence += "C");

                context["a subcontext"] = () =>
                {
                    beforeAllAsync = async () => await Task.Run(() => sequence += "B");
                    beforeAsync = async () => await Task.Run(() => sequence += "D");

                    specify = () => 1.Is(1);

                    afterAsync = async () => await Task.Run(() => sequence += "E");
                    afterAllAsync = async () => await Task.Run(() => sequence += "G");
                };

                afterAsync = async () => await Task.Run(() => sequence += "F");
                afterAllAsync = async () => await Task.Run(() => sequence += "H");
            }
        }

        [SetUp]
        public void setup()
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
            Sequence_spec.sequence.should_end_with("EFGH");
        }
    }
}