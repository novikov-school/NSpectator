using NSpectator;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace NSpectator.Describer.WhenRunningSpecs
{
    [TestFixture]
    [Category("RunningSpecs")]
    [Category("Async")]
    public class describe_async_method_level_examples : describe_method_level_examples_common_cases
    {
        class AsyncSpecClass : Spec
        {
            public static bool first_async_example_executed, last_async_example_executed;

            async Task it_should_be_an_async_example()
            {
                await Task.Run(() =>
                {
                    first_async_example_executed = true;
                    "hello".should_be("hello");
                });
            }

            async Task it_should_be_failing_async()
            {
                await Task.Run(() =>
                {
                    last_async_example_executed = true;
                    "hello".should_not_be("hello");
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
    public class describe_async_wrong_method_level_examples : when_running_method_level_examples
    {
        class WrongAsyncSpecClass : Spec
        {
            async Task<long> it_should_be_failing_with_task_result()
            {
                await Task.Run(() => "hello".should_be("hello"));

                return -1L;
            }

            async void it_should_throw_with_async_void()
            {
                await Task.Run(() => "hello".should_be("hello"));
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
            classContext.Examples[0].HasRun.should_be_true();
        }

        [Test]
        public void async_example_with_result_should_fail()
        {
            classContext.Examples[0].Exception.Should().NotBeNull();
        }

        [Test]
        public void async_example_with_void_should_execute()
        {
            classContext.Examples[1].HasRun.should_be_true();
        }

        [Test]
        public void async_example_with_void_should_fail()
        {
            classContext.Examples[1].Exception.Should().NotBeNull();
        }
    }
}
