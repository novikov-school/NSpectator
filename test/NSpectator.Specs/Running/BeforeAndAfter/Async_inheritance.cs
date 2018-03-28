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
using FluentAssertions;

namespace NSpectator.Specs.Running.BeforeAndAfter
{
    [TestFixture]
    [Category("Async")]
    public class Async_inheritance : When_running_specs
    {
        class BaseSpec : Sequence_spec
        {
            async Task before_all()
            {
                await Task.Run(() => sequence = "A");
            }

            async Task before_each()
            {
                await Task.Run(() => sequence += "C");
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

        class DerivedClass : BaseSpec
        {
            void a_context()
            {
                BeforeAllAsync = async () => await Task.Run(() => sequence += "B");

                BeforeAsync = async () => await Task.Run(() => sequence += "D");
                Specify = () => 1.Should().Be(1, todo_reason);
                AfterAsync = async () => await Task.Run(() => sequence += "E");

                AfterAllAsync = async () => await Task.Run(() => sequence += "G");
            }
        }

        [SetUp]
        public void Setup()
        {
            Run(typeof(DerivedClass));
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