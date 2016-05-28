#region [R# naming]
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
#endregion
using System;
using System.Linq;
using NSpectator.Domain;
using NUnit.Framework;
using System.Threading.Tasks;
using FluentAssertions;

namespace NSpectator.Specs.Running
{
    [TestFixture]
    [Category("RunningSpecs")]
    public class When_using_xit : Describe_todo
    {
        class XitClass : Spec
        {
            void method_level_context()
            {
                xIt["should be pending"] = () => { };
            }
        }

        [Test]
        public void example_should_be_pending()
        {
            ExampleFrom(typeof(XitClass)).Pending.Should().BeTrue();
        }
    }

    [TestFixture]
    [Category("RunningSpecs")]
    [Category("Async")]
    public class Using_async_lambda_with_xit : Describe_todo
    {
        class AsyncLambdaClass : Spec
        {
            void method_level_context()
            {
                xIt["should fail because xit is set to async lambda"] = async () => await Task.Run(() => { });

                // No chance of error when (async) return value is explicitly typed. The following do not even compile:
                /*
                Func<Task> asyncTaggedDelegate = async () => await Task.Run(() => { });
                Func<Task> asyncUntaggedDelegate = () => { return Task.Run(() => { }); };

                it["Should fail because xit is set to async tagged delegate"] = asyncTaggedDelegate;

                it["Should fail because xit is set to async untagged delegate"] = asyncUntaggedDelegate;
                */
            }
        }

        [Test]
        public void Sync_pending_example_set_to_async_lambda_fails()
        {
            var example = ExampleFrom(typeof(AsyncLambdaClass));

            example.HasRun.Should().BeTrue();

            example.Exception.Should().NotBeNull();

            example.Pending.Should().BeTrue();
        }
    }

    [TestFixture]
    [Category("RunningSpecs")]
    [Category("Async")]
    public class When_using_async_xit : Describe_todo
    {
        class AsyncXitClass : Spec
        {
            void method_level_context()
            {
                xItAsync["should be pending"] = async () => await Task.Run(() => { });
            }
        }

        [Test]
        public void example_should_be_pending()
        {
            ExampleFrom(typeof(AsyncXitClass)).Pending.Should().BeTrue();
        }
    }

    [TestFixture]
    [Category("RunningSpecs")]
    public class When_using_todo : Describe_todo
    {
        class TodoClass : Spec
        {
            void method_level_context()
            {
                It["should be pending"] = Todo;
            }
        }

        [Test]
        public void example_should_be_pending()
        {
            ExampleFrom(typeof(TodoClass)).Pending.Should().BeTrue();
        }
    }

    [TestFixture]
    [Category("RunningSpecs")]
    [Category("Async")]
    public class When_using_async_todo : Describe_todo
    {
        class AsyncTodoClass : Spec
        {
            void method_level_context()
            {
                ItAsync["should be pending"] = TodoAsync;
            }
        }

        [Test]
        public void example_should_be_pending()
        {
            ExampleFrom(typeof(AsyncTodoClass)).Pending.Should().BeTrue();
        }
    }

    [TestFixture]
    [Category("RunningSpecs")]
    public class When_using_todo_with_throwing_before : Describe_todo
    {
        class TodoClass : Spec
        {
            void method_level_context()
            {
                Before = () => { throw new Exception(); };
                It["should be pending"] = Todo;
            }
        }

        [Test]
        public void example_should_not_fail_but_be_pending()
        {
            ExampleFrom(typeof(TodoClass)).Pending.Should().BeTrue();
        }
    }

    public class Describe_todo : When_running_specs
    {
        protected ExampleBase ExampleFrom(Type type)
        {
            Run(type);

            return classContext.AllExamples().First();
        }
    }
}
