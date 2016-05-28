#region [R# naming]
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
#endregion
using NUnit.Framework;

namespace NSpectator.Specs.Running
{
    [TestFixture]
    [Category("RunningSpecs")]
    [Category("Async")]
    public class Describe_async_before : When_describing_async_hooks
    {
        class SpecClass : BaseSpecClass
        {
            void given_async_before_is_set()
            {
                BeforeAsync = SetStateAsync;

                It["Should have final value"] = ShouldHaveFinalState;
            }

            void given_async_before_fails()
            {
                BeforeAsync = FailAsync;

                It["Should fail"] = PassAlways;
            }

            void given_both_sync_and_async_before_are_set()
            {
                Before = SetAnotherState;

                BeforeAsync = SetStateAsync;

                It["Should not know what to expect"] = PassAlways;
            }
        }

        [SetUp]
        public void setup()
        {
            Run(typeof(SpecClass));
        }

        [Test]
        public void async_before_waits_for_task_to_complete()
        {
            ExampleRunsWithExpectedState("Should have final value");
        }

        [Test]
        public void async_before_with_exception_fails()
        {
            ExampleRunsWithException("Should fail");
        }

        [Test]
        public void context_with_both_sync_and_async_before_always_fails()
        {
            ExampleRunsWithException("Should not know what to expect");
        }
    }
}
