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
    public class Async_abstract_class : When_running_specs
    {
        abstract class Abstract : Sequence_spec
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
                BeforeAllAsync = async () => await Task.Run(() => sequence += "B");

                BeforeAsync = async () => await Task.Run(() => sequence += "D");
                Specify = () => 1.Expected().ToBe(1);
                AfterAsync = async () => await Task.Run(() => sequence += "E");

                AfterAllAsync = async () => await Task.Run(() => sequence += "G");
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

        class Concrete : Abstract {}

        [Test]
        public void all_async_features_are_supported_from_abstract_classes_when_run_under_the_context_of_a_derived_concrete()
        {
            Run(typeof(Concrete));
            Sequence_spec.sequence.Expected().ToBe("ABCDEFGH");
        }
    }
}
