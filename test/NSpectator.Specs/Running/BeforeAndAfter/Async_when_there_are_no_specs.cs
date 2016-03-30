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
    public class Async_when_there_are_no_specs : When_running_specs
    {
        [SetUp]
        public void Setup()
        {
            Sequence_spec.sequence = "";
        }

        class Async_before_all_example_spec : Sequence_spec
        {
            async Task before_all()
            {
                await Task.Run(() => sequence = "executed");
            }
        }

        [Test]
        public void Async_before_all_is_not_executed()
        {
            Run(typeof(Async_before_all_example_spec));

            Sequence_spec.sequence.Is("");
        }

        class Async_before_each_example_spec : Sequence_spec
        {
            async Task before_each()
            {
                await Task.Run(() => sequence = "executed");
            }
        }

        [Test]
        public void async_before_each_is_not_executed()
        {
            Run(typeof(Async_before_each_example_spec));

            Sequence_spec.sequence.Is("");
        }

        class After_each_example_spec : Sequence_spec
        {
            async Task after_each()
            {
                await Task.Run(() => sequence += "executed");
            }
        }

        [Test]
        public void after_each_is_not_executed()
        {
            Run(typeof (After_each_example_spec));

            Sequence_spec.sequence.Is("");
        }

        class After_all_example_spec : Sequence_spec
        {
            async Task after_all()
            {
                await Task.Run(() => sequence += "executed");
            }
        }

        [Test]
        public void after_all_is_not_executed()
        {
            Run(typeof (After_all_example_spec));

            Sequence_spec.sequence.Is("");
        }
    }
}