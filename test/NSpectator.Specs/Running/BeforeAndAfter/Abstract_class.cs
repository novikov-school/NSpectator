#region [R# naming]
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
#endregion
using NUnit.Framework;

namespace NSpectator.Specs.Running.BeforeAndAfter
{
    [TestFixture]
    public class Abstract_class : When_running_specs
    {
        abstract class Abstract : Sequence_spec
        {
            void before_all()
            {
                sequence = "A";
            }

            void before_each()
            {
                sequence += "C";
            }

            void a_context()
            {
                beforeAll = () => sequence += "B";

                before = () => sequence += "D";
                specify = () => 1.Expected().ToBe(1);
                after = () => sequence += "E";

                afterAll = () => sequence += "G";
            }

            void after_each()
            {
                sequence += "F";
            }

            void after_all()
            {
                sequence += "H";
            }
        }

        class Concrete : Abstract {}

        [Test]
        public void all_features_are_supported_from_abstract_classes_when_run_under_the_context_of_a_derived_concrete()
        {
            Run(typeof(Concrete));
            Sequence_spec.sequence.Expected().ToBe("ABCDEFGH");
        }
    }
}