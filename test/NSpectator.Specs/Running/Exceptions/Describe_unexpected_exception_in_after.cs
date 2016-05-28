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
    public class Describe_unexpected_exception_in_after : When_running_specs
    {
        private class SpecClass : Spec
        {
            void method_level_context()
            {
                Context["When same exception thrown in after"] = () =>
                {
                    Before = () => { throw new KnownException(); };

                    It["fails because of same exception thrown again in after"] = Expect<KnownException>();

                    After = () => { throw new KnownException(); };
                };

                Context["When different exception thrown in after"] = () =>
                {
                    Before = () => { throw new KnownException(); };

                    It["fails because of different exception thrown in after"] = Expect<KnownException>();

                    After = () => { throw new SomeOtherException(); };
                };
            }
        }

        [SetUp]
        public void Setup()
        {
            Run(typeof(SpecClass));
        }

        [Test]
        public void Should_fail_because_of_same_exception_in_after()
        {
            var example = TheExample("fails because of same exception thrown again in after");

            example.Exception.Should().NotBeNull();
            example.Exception.Should().BeOfType<ExampleFailureException>();
        }

        [Test]
        public void Should_fail_because_of_different_exception_in_after()
        {
            var example = TheExample("fails because of different exception thrown in after");

            example.Exception.Should().NotBeNull();
            example.Exception.Should().BeOfType<ExampleFailureException>();
        }
    }
}
