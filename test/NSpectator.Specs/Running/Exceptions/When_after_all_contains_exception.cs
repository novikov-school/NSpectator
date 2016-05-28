#region [R# naming]
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
#endregion
using NSpectator.Domain;
using NUnit.Framework;
using FluentAssertions;

namespace NSpectator.Specs.Running.Exceptions
{
    [TestFixture]
    [Category("RunningSpecs")]
    public class When_after_all_contains_exception : When_running_specs
    {
        class SpecClass : Spec
        {
            void method_level_context()
            {
                AfterAll = () => { throw new AfterAllException(); };

                It["should fail this example because of afterAll"] = () => "1".Should().Be("1");

                It["should also fail this example because of afterAll"] = () => "1".Should().Be("1");

                It["overrides exception from same level it"] = () => { throw new ItException(); };

                Context["exception thrown by both afterAll and nested before"] = () =>
                {
                    Before = () => { throw new BeforeException(); };

                    It["preserves exception from nested before"] = () => "1".Should().Be("1");
                };

                Context["exception thrown by both afterAll and nested act"] = () =>
                {
                    Act = () => { throw new ActException(); };

                    It["preserves exception from nested act"] = () => "1".Should().Be("1");
                };

                Context["exception thrown by both afterAll and nested it"] = () =>
                {
                    It["overrides exception from nested it"] = () => { throw new ItException(); };
                };

                Context["exception thrown by both afterAll and nested after"] = () =>
                {
                    It["preserves exception from nested after"] = () => "1".Should().Be("1");

                    After = () => { throw new AfterException(); };
                };
            }
        }

        [SetUp]
        public void Setup()
        {
            Run(typeof(SpecClass));
        }

        [Test]
        public void the_example_level_failure_should_indicate_a_context_failure()
        {
            TheExample("should fail this example because of afterAll")
                .Exception.GetType().Should().Be(typeof(ExampleFailureException));
            TheExample("should also fail this example because of afterAll")
                .Exception.GetType().Should().Be(typeof(ExampleFailureException));
            TheExample("overrides exception from same level it")
                .Exception.GetType().Should().Be(typeof(ExampleFailureException));
            TheExample("preserves exception from nested before")
                .Exception.GetType().Should().Be(typeof(ExampleFailureException));
            TheExample("preserves exception from nested act")
                .Exception.GetType().Should().Be(typeof(ExampleFailureException));
            TheExample("overrides exception from nested it")
                .Exception.GetType().Should().Be(typeof(ExampleFailureException));
            TheExample("preserves exception from nested after")
                .Exception.GetType().Should().Be(typeof(ExampleFailureException));
        }

        [Test]
        public void examples_with_only_after_all_failure_should_fail_because_of_after_all()
        {
            TheExample("should fail this example because of afterAll")
                .Exception.InnerException.GetType().Should().Be(typeof(AfterAllException));
            TheExample("should also fail this example because of afterAll")
                .Exception.InnerException.GetType().Should().Be(typeof(AfterAllException));
        }

        [Test]
        public void it_should_throw_exception_from_after_all_not_from_same_level_it()
        {
            TheExample("overrides exception from same level it")
                .Exception.InnerException.GetType().Should().Be(typeof(AfterAllException));
        }

        [Test]
        public void it_should_throw_exception_from_nested_before_not_from_after_all()
        {
            TheExample("preserves exception from nested before")
                .Exception.InnerException.GetType().Should().Be(typeof(BeforeException));
        }

        [Test]
        public void it_should_throw_exception_from_nested_act_not_from_after_all()
        {
            TheExample("preserves exception from nested act")
                .Exception.InnerException.GetType().Should().Be(typeof(ActException));
        }

        [Test]
        public void it_should_throw_exception_from_after_all_not_from_nested_it()
        {
            TheExample("overrides exception from nested it")
                .Exception.InnerException.GetType().Should().Be(typeof(AfterAllException));
        }

        [Test]
        public void it_should_throw_exception_from_nested_after_not_from_after_all()
        {
            TheExample("preserves exception from nested after")
                .Exception.InnerException.GetType().Should().Be(typeof(AfterException));
        }
    }
}
