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
    [Category("Async")]
    public class Async_nested_contexts : When_running_specs
    {
        class SpecClass : Sequence_spec
        {
            void a_context()
            {
                BeforeAllAsync = async () => await Task.Run(() => sequence += "A");
                BeforeAsync = async () => await Task.Run(() => sequence += "C");

                Context["a subcontext"] = () =>
                {
                    BeforeAllAsync = async () => await Task.Run(() => sequence += "B");
                    BeforeAsync = async () => await Task.Run(() => sequence += "D");

                    specify = () => 1.Expected().ToBe(1);

                    AfterAsync = async () => await Task.Run(() => sequence += "E");
                    AfterAllAsync = async () => await Task.Run(() => sequence += "G");
                };

                AfterAsync = async () => await Task.Run(() => sequence += "F");
                AfterAllAsync = async () => await Task.Run(() => sequence += "H");
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
            Sequence_spec.sequence.Should().StartWith("ABCD");
        }

        [Test]
        public void after_alls_at_every_level_run_after_after_eaches_from_the_inside_out()
        {
            Sequence_spec.sequence.Should().EndWith("EFGH");
        }
    }
}