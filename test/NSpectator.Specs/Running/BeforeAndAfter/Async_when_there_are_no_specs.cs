using NSpectator;
using NUnit.Framework;
using System.Threading.Tasks;
using NSpectator.Specs.Running;

namespace NSpectator.Specs.Running.BeforeAndAfter
{
    [TestFixture]
    [Category("Async")]
    public class async_when_there_are_no_specs : When_running_specs
    {
        [SetUp]
        public void setup()
        {
            Sequence_spec.sequence = "";
        }

        class async_before_all_example_spec : Sequence_spec
        {
            async Task before_all()
            {
                await Task.Run(() => sequence = "executed");
            }
        }

        [Test]
        public void async_before_all_is_not_executed()
        {
            Run(typeof(async_before_all_example_spec));

            Sequence_spec.sequence.Is("");
        }

        class async_before_each_example_spec : Sequence_spec
        {
            async Task before_each()
            {
                await Task.Run(() => sequence = "executed");
            }
        }

        [Test]
        public void async_before_each_is_not_executed()
        {
            Run(typeof(async_before_each_example_spec));

            Sequence_spec.sequence.Is("");
        }

        class after_each_example_spec : Sequence_spec
        {
            async Task after_each()
            {
                await Task.Run(() => sequence += "executed");
            }
        }

        [Test]
        public void after_each_is_not_executed()
        {
            Run(typeof (after_each_example_spec));

            Sequence_spec.sequence.Is("");
        }

        class after_all_example_spec : Sequence_spec
        {
            async Task after_all()
            {
                await Task.Run(() => sequence += "executed");
            }
        }

        [Test]
        public void after_all_is_not_executed()
        {
            Run(typeof (after_all_example_spec));

            Sequence_spec.sequence.Is("");
        }
    }
}