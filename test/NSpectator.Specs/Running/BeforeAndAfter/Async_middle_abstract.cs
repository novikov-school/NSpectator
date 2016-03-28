using NSpectator;
using NUnit.Framework;
using System.Threading.Tasks;
using NSpectator.Specs.Running;

namespace NSpectator.Specs.Running.BeforeAndAfter
{
    [TestFixture]
    [Category("RunningSpecs")]
    [Category("Async")]
    public class async_middle_abstract : When_running_specs
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

                1.Is(1);
            }
        }

        [SetUp]
        public void setup()
        {
            Concrete.sequence = "";
        }

        [Test]
        public void befores_are_run_from_middle_abstract_classes()
        {
            Run(typeof(Concrete));

            Concrete.sequence.should_start_with("ABC");
        }

        [Test]
        public void afters_are_run_from_middle_abstract_classes()
        {
            Run(typeof(Concrete));

            Concrete.sequence.should_end_with("DEF");
        }
    }
}
