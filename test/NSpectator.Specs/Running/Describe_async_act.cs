#region [R# naming]
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
#endregion

using NUnit.Framework;
using FluentAssertions;

namespace NSpectator.Specs.Running
{
    [TestFixture]
    [Category("RunningSpecs")]
    [Category("Async")]
    public class Describe_async_act : When_describing_async_hooks
    {
        class SpecClass : BaseSpecClass
        {
            void Given_async_act_is_set()
            {
                actAsync = SetStateAsync;

                it["Should have final value"] = ShouldHaveFinalState;
            }

            void Given_async_act_fails()
            {
                actAsync = FailAsync;

                it["Should fail"] = PassAlways;
            }

            void Given_both_sync_and_async_act_are_set()
            {
                act = SetAnotherState;

                actAsync = SetStateAsync;

                it["Should not know what to expect"] = PassAlways;
            }
        }

        [SetUp]
        public void Setup()
        {
            Run(typeof(SpecClass));
        }

        [Test]
        public void Async_act_waits_for_task_to_complete()
        {
            ExampleRunsWithExpectedState("Should have final value");
        }

        [Test]
        public void Async_act_with_exception_fails()
        {
            ExampleRunsWithException("Should fail");
        }

        [Test]
        public void Should_always_fail_context_with_both_sync_and_async_act()
        {
            ExampleRunsWithException("Should not know what to expect");
        }
    }
}
