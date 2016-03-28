using NSpectator;
using NUnit.Framework;
using System.Threading.Tasks;
using NSpectator.Specs.Running;

namespace NSpectator.Specs.Running.BeforeAndAfter
{
    [TestFixture]
    public class when_there_are_no_specs : When_running_specs
    {
        [SetUp]
        public void setup()
        {
            Sequence_spec.sequence = "";
        }

        class before_all_example_spec : Sequence_spec
        {
            void before_all()
            {
                sequence = "executed";
            }
        }

        [Test]
        public void before_all_is_not_executed()
        {
            Run(typeof (before_all_example_spec));

            Sequence_spec.sequence.Is("");
        }

        class before_each_example_spec : Sequence_spec
        {
            void before_each()
            {
                sequence = "executed";
            }
        }

        [Test]
        public void before_each_is_not_executed()
        {
            Run(typeof (before_each_example_spec));

            Sequence_spec.sequence.Is("");
        }

        class after_each_example_spec : Sequence_spec
        {
            void after_each()
            {
                sequence = "executed";
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
            void after_all()
            {
                sequence = "executed";
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