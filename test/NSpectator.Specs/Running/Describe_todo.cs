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
    public class using_xit : Describe_todo
    {
        class XitClass : Spec
        {
            void method_level_context()
            {
                xit["should be pending"] = () => { };
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
    public class using_async_xit : Describe_todo
    {
        class AsyncXitClass : Spec
        {
            void method_level_context()
            {
                xitAsync["should be pending"] = async () => await Task.Run(() => { });
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
    public class using_todo : Describe_todo
    {
        class TodoClass : Spec
        {
            void method_level_context()
            {
                it["should be pending"] = todo;
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
    public class using_async_todo : Describe_todo
    {
        class AsyncTodoClass : Spec
        {
            void method_level_context()
            {
                itAsync["should be pending"] = todoAsync;
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
    public class using_todo_with_throwing_before : Describe_todo
    {
        class TodoClass : Spec
        {
            void method_level_context()
            {
                before = () => { throw new Exception(); };
                it["should be pending"] = todo;
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
