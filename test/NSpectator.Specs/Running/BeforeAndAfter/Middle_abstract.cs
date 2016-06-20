#region [R# naming]
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
#endregion
using NUnit.Framework;
using FluentAssertions;
using Slant.Expectations;

namespace NSpectator.Specs.Running.BeforeAndAfter
{
    [TestFixture]
    [Category("RunningSpecs")]
    public class Middle_abstract : When_running_specs
    {
        class Base : Sequence_spec
        {
            void before_all()
            {
                sequence += "A";
            }
            void after_all()
            {
                sequence += "F";
            }
        }

        abstract class Abstract : Base
        {
            void before_all()
            {
                sequence += "B";
            }
            void before_each()
            {
                sequence += "C";
            }
            void after_each()
            {
                sequence += "D";
            }
            void after_all()
            {
                sequence += "E";
            }
        }

        class Concrete : Abstract
        {
            void it_one_is_one()
            {
                1.Expected().ToBe(1);
            }
        }

        [SetUp]
        public void Setup()
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
