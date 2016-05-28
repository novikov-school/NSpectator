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
    public class Describe_async_after : When_describing_async_hooks
    {
        class SpecClass : BaseSpecClass
        {
            void given_async_after_is_set()
            {
                It["Should have initial value"] = ShouldHaveInitialState;

                AfterAsync = SetStateAsync;
            }

            void given_async_after_fails()
            {
                It["Should fail"] = PassAlways;

                AfterAsync = FailAsync;
            }

            void given_both_sync_and_async_after_are_set()
            {
                It["Should not know what to do"] = PassAlways;

                After = SetAnotherState;

                AfterAsync = SetStateAsync;
            }
        }

        [SetUp]
        public void setup()
        {
            Run(typeof(SpecClass));
        }

        [Test]
        public void async_after_waits_for_task_to_complete()
        {
            ExampleRunsWithExpectedState("Should have initial value");
        }

        [Test]
        public void async_after_with_exception_fails()
        {
            ExampleRunsWithException("Should fail");
        }

        [Test]
        public void context_with_both_sync_and_async_after_always_fails()
        {
            ExampleRunsWithException("Should not know what to do");
        }
    }
}
