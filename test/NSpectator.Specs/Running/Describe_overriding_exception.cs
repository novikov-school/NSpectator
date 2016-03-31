#region [R# naming]
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
#endregion
using System;
using NUnit.Framework;
using System.Threading.Tasks;
using FluentAssertions;

namespace NSpectator.Specs.Running
{
    [TestFixture]
    public class Describe_overriding_exception : When_running_specs
    {
        class SpecClass : Spec
        {
            void before_each()
            {
                throw new InvalidOperationException("Exception to replace.");
            }

            void specify_method_level_failure()
            {
                "1".Should().Be("1");
            }

            async Task specify_async_method_level_failure()
            {
                await Task.Delay(0);

                "1".Should().Be("1");
            }

            public override Exception ExceptionToReturn(Exception originalException)
            {
                return new ArgumentException("Redefined exception.", originalException);
            }
        }

        [SetUp]
        public void setup()
        {
            Run(typeof(SpecClass));
        }

        [Test]
        public void the_examples_exception_is_replaced_with_exception_provided_in_override()
        {
            TheExample("specify method level failure").Exception.InnerException.GetType().Should().Be(typeof(ArgumentException));
        }

        [Test]
        public void the_examples_exception_is_replaced_with_exception_provided_in_override_if_async_method()
        {
            TheExample("specify async method level failure").Exception.InnerException.GetType().Should().Be(typeof(ArgumentException));
        }
    }
}
