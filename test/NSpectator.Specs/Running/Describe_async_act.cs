#region [R# naming]
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
#endregion

using System.Threading.Tasks;
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
                ActAsync = SetStateAsync;

                It["Should have final value"] = ShouldHaveFinalState;
            }

            void Given_async_act_fails()
            {
                ActAsync = FailAsync;

                It["Should fail because of exception"] = PassAlways;
            }

            void Given_both_sync_and_async_act_are_set()
            {
                Act = SetAnotherState;

                ActAsync = SetStateAsync;

                It["Should not know what to expect"] = PassAlways;
            }

            [Test]
            void Given_act_is_set_to_async_lambda()
            {
                Act = async () => { await Task.Delay(0); };

                It["Should fail because act is set to async lambda"] = PassAlways;

                // No chance of error when (async) return value is explicitly typed. The following do not even compile:
                /*
                Func<Task> asyncTaggedDelegate = async () => { await Task.Delay(0); };
                Func<Task> asyncUntaggedDelegate = () => { return Task.Delay(0); };

                // set to async method
                act = SetStateAsync;

                // set to async tagged delegate
                act = asyncTaggedDelegate;

                // set to async untagged delegate
                act = asyncUntaggedDelegate;
                */
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
            ExampleRunsWithException("Should fail because of exception");
        }

        [Test]
        public void Should_always_fail_context_with_both_sync_and_async_act()
        {
            ExampleRunsWithException("Should not know what to expect");
        }

        [Test]
        public void sync_act_set_to_async_lambda_fails()
        {
            ExampleRunsWithException("Should fail because act is set to async lambda");
        }
    }
}
