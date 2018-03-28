#region [R# naming]
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
#endregion
using NUnit.Framework;
using FluentAssertions;

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
                BeforeAll = () => sequence += "B";

                Before = () => sequence += "D";
                Specify = () => 1.Should().Be(1, todo_reason);
                After = () => sequence += "E";

                AfterAll = () => sequence += "G";
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
            Sequence_spec.sequence.Should().Be("ABCDEFGH");
        }
    }
}