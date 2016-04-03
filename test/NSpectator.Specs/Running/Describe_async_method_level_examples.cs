#region [R# naming]
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
#endregion
using NUnit.Framework;
using System.Threading.Tasks;
using FluentAssertions;

namespace NSpectator.Specs.Running
{
    [TestFixture]
    [Category("RunningSpecs")]
    [Category("Async")]
    public class Describe_async_method_level_examples : Describe_method_level_examples_common_cases
    {
        class AsyncSpecClass : Spec
        {
            public static bool first_async_example_executed, last_async_example_executed;

            async Task it_should_be_an_async_example()
            {
                await Task.Run(() =>
                {
                    first_async_example_executed = true;
                    "hello".Should().Be("hello");
                });
            }

            async Task it_should_be_failing_async()
            {
                await Task.Run(() =>
                {
                    last_async_example_executed = true;
                    "hello".Should().NotBe("hello");
                });
            }
        }

        [SetUp]
        public void setup()
        {
            RunWithReflector(typeof(AsyncSpecClass));
        }

        protected override bool FirstExampleExecuted => AsyncSpecClass.first_async_example_executed;

        protected override bool LastExampleExecuted => AsyncSpecClass.last_async_example_executed;
    }

    [TestFixture]
    [Category("RunningSpecs")]
    [Category("Async")]
    public class Describe_async_wrong_method_level_examples : When_running_method_level_examples
    {
        class WrongAsyncSpecClass : Spec
        {
            async Task<long> it_should_be_failing_with_task_result()
            {
                await Task.Run(() => "hello".Should().Be("hello"));

                return -1L;
            }

            async void it_should_throw_with_async_void()
            {
                await Task.Run(() => "hello".Should().Be("hello"));
            }
        }

        [SetUp]
        public void setup()
        {
            RunWithReflector(typeof(WrongAsyncSpecClass));
        }

        [Test]
        public void async_example_with_result_should_execute()
        {
            classContext.Examples[0].HasRun.Should().BeTrue();
        }

        [Test]
        public void async_example_with_result_should_fail()
        {
            classContext.Examples[0].Exception.Should().NotBeNull();
        }

        [Test]
        public void async_example_with_void_should_execute()
        {
            classContext.Examples[1].HasRun.Should().BeTrue();
        }

        [Test]
        public void async_example_with_void_should_fail()
        {
            classContext.Examples[1].Exception.Should().NotBeNull();
        }
    }
}
