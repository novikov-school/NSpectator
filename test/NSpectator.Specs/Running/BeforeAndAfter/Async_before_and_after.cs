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
    [Category("RunningSpecs")]
    [Category("Async")]
    public class Async_before_and_after : When_running_specs
    {
        class SpecClass : Sequence_spec
        {
            void as_long_as_the_async_world_has_not_come_to_an_end()
            {
                BeforeAllAsync = async () => await Task.Run(() => sequence = "A");
                BeforeAsync = async () => await Task.Run(() => sequence += "B");
                ItAsync["spec 1"] = async () => await Task.Run(() => sequence += "1");
                ItAsync["spec 2"] = async () => await Task.Run(() => sequence += "2"); //two specs cause before_each and after_each to run twice
                AfterAsync = async () => await Task.Run(() => sequence += "C");
                AfterAllAsync = async () => await Task.Run(() => sequence += "D");
            }
        }

        [Test]
        public void everything_async_runs_in_the_correct_order_and_with_the_correct_frequency()
        {
            Run(typeof(SpecClass));

            Sequence_spec.sequence.Expected().ToBe("AB1CB2CD");
        }
    }

    [TestFixture]
    [Category("RunningSpecs")]
    [Category("Async")]
    public class Async_before_and_after_aliases : When_running_specs
    {
        class SpecClass : Sequence_spec
        {
            void as_long_as_the_async_world_has_not_come_to_an_end()
            {
                BeforeAllAsync = async () => await Task.Run(() => sequence = "A");
                BeforeEachAsync = async () => await Task.Run(() => sequence += "B");
                ItAsync["spec 1"] = async () => await Task.Run(() => sequence += "1");
                ItAsync["spec 2"] = async () => await Task.Run(() => sequence += "2"); //two specs cause before_each and after_each to run twice
                AfterEachAsync = async () => await Task.Run(() => sequence += "C");
                AfterAllAsync = async () => await Task.Run(() => sequence += "D");
            }
        }

        [Test]
        public void everything_async_runs_in_the_correct_order_and_with_the_correct_frequency()
        {
            Run(typeof(SpecClass));

            Sequence_spec.sequence.Expected().ToBe("AB1CB2CD");
        }
    }
}
