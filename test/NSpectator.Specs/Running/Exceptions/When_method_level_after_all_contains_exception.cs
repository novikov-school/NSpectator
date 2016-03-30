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
using FluentAssertions;

namespace NSpectator.Specs.Running.Exceptions
{
    [TestFixture]
    [Category("RunningSpecs")]
    public class When_method_level_after_all_contains_exception : When_running_specs
    {
        class SpecClass : Spec
        {
            void after_all()
            {
                throw new AfterAllException();
            }

            void should_fail_this_example()
            {
                it["should fail"] = () => "hello".Should().Be("hello");
            }

            void should_also_fail_this_example()
            {
                it["should also fail"] = () => "hello".Should().Be("hello");
            }
        }

        [SetUp]
        public void setup()
        {
            Run(typeof(SpecClass));
        }

        [Test]
        public void the_first_example_should_fail_with_framework_exception()
        {
            classContext.AllExamples()
                        .First()
                        .Exception
                        .ShouldCastTo<ExampleFailureException>();
        }

        [Test]
        public void the_second_example_should_fail_with_framework_exception()
        {
            classContext.AllExamples()
                        .Last()
                        .Exception
                        .ShouldCastTo<ExampleFailureException>();
        }

        class AfterAllException : Exception { }
    }
}
