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
    public class When_act_contains_exception : When_running_specs
    {
        private class SpecClass : Spec
        {
            void method_level_context()
            {
                Act = () => { throw new ActException(); };

                It["should fail this example because of act"] = () => "1".Should().Be("1");

                It["should also fail this example because of act"] = () => "1".Should().Be("1");

                It["overrides exception from same level it"] = () => { throw new ItException(); };

                Context["exception thrown by both act and nested before"] = () =>
                {
                    Before = () => { throw new BeforeException(); };

                    It["preserves exception from nested before"] = () => "1".Should().Be("1");
                };

                Context["exception thrown by both act and nested act"] = () =>
                {
                    Act = () => { throw new NestedActException(); };

                    It["overrides exception from nested act"] = () => "1".Should().Be("1");
                };

                Context["exception thrown by both act and nested it"] = () =>
                {
                    It["overrides exception from nested it"] = () => { throw new ItException(); };
                };

                Context["exception thrown by both act and nested after"] = () =>
                {
                    It["overrides exception from nested after"] = () => "1".Should().Be("1");

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
            TheExample("should fail this example because of act")
                .Exception.Expect<ExampleFailureException>();
            TheExample("should also fail this example because of act")
                .Exception.Expect<ExampleFailureException>();
            TheExample("overrides exception from same level it")
                .Exception.Expect<ExampleFailureException>();
            TheExample("preserves exception from nested before")
                .Exception.Expect<ExampleFailureException>();
            TheExample("overrides exception from nested act")
                .Exception.Expect<ExampleFailureException>();
            TheExample("overrides exception from nested it")
                .Exception.Expect<ExampleFailureException>();
            TheExample("overrides exception from nested after")
                .Exception.Expect<ExampleFailureException>();
        }

        [Test]
        public void examples_with_only_act_failure_should_fail_because_of_act()
        {
            TheExample("should fail this example because of act").Exception
                .InnerException.Expect<ActException>();
            TheExample("should also fail this example because of act").Exception
                .InnerException.Expect<ActException>();
        }

        [Test]
        public void it_should_throw_exception_from_act_not_from_same_level_it()
        {
            TheExample("overrides exception from same level it")
                .Exception.InnerException.Expect<ActException>();
        }

        [Test]
        public void it_should_throw_exception_from_nested_before_not_from_act()
        {
            TheExample("preserves exception from nested before")
                .Exception.InnerException.Expect<BeforeException>();
        }

        [Test]
        public void it_should_throw_exception_from_act_not_from_nested_act()
        {
            TheExample("overrides exception from nested act")
                .Exception.InnerException.Expect<ActException>();
        }

        [Test]
        public void it_should_throw_exception_from_act_not_from_nested_it()
        {
            TheExample("overrides exception from nested it")
                .Exception.InnerException.Expect<ActException>();
        }

        [Test]
        public void it_should_throw_exception_from_act_not_from_nested_after()
        {
            TheExample("overrides exception from nested after")
                .Exception.InnerException.Expect<ActException>();
        }
    }
}
