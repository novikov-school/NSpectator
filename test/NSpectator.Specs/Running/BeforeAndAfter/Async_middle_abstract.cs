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
using Slant.Expectations;

namespace NSpectator.Specs.Running.BeforeAndAfter
{
    [TestFixture]
    [Category("RunningSpecs")]
    [Category("Async")]
    public class Async_middle_abstract : When_running_specs
    {
        class Base : Sequence_spec
        {
            async Task before_all()
            {
                await Task.Run(() => sequence += "A");
            }
            async Task after_all()
            {
                await Task.Run(() => sequence += "F");
            }
        }

        abstract class Abstract : Base
        {
            async Task before_all()
            {
                await Task.Run(() => sequence += "B");
            }
            async Task before_each()
            {
                await Task.Run(() => sequence += "C");
            }
            async Task after_each()
            {
                await Task.Run(() => sequence += "D");
            }
            async Task after_all()
            {
                await Task.Run(() => sequence += "E");
            }
        }

        class Concrete : Abstract
        {
            async Task it_one_is_one()
            {
                await Task.Delay(0);

                1.Expected().ToBe(1);
            }
        }

        [SetUp]
        public void setup()
        {
            Sequence_spec.sequence = "";
        }

        [Test]
        public void befores_are_run_from_middle_abstract_classes()
        {
            Run(typeof(Concrete));

            Sequence_spec.sequence.Should().StartWith("ABC");
        }

        [Test]
        public void afters_are_run_from_middle_abstract_classes()
        {
            Run(typeof(Concrete));

            Sequence_spec.sequence.Should().EndWith("DEF");
        }
    }
}
