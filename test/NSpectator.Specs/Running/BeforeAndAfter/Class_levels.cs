#region [R# naming]
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
#endregion
using NUnit.Framework;
using Slant.Expectations;

namespace NSpectator.Specs.Running.BeforeAndAfter
{
    [TestFixture]
    [Category("RunningSpecs")]
    public class Class_levels : When_running_specs
    {
        class SpecClass : Sequence_spec
        {
            void before_all()
            {
                sequence = "A";
            }

            void before_each()
            {
                sequence += "B";
            }

            void it_one_is_one()
            {
                sequence += "1";
            }

            void it_two_is_two()
            {
                sequence += "2"; //two specs cause before_each and after_each to run twice
            }

            void after_each()
            {
                sequence += "C";
            }

            void after_all()
            {
                sequence += "D";
            }
        }

        [Test]
        public void everything_runs_in_the_correct_order()
        {
            Run(typeof(SpecClass));

            Sequence_spec.sequence.Expected().ToBe("AB1CB2CD");
        }
    }
}