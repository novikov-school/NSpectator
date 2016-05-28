using System;
using System.Linq;
using NSpectator;
using NSpectator.Domain;
using NSpectator.Specs.Running;
using NUnit.Framework;
using FluentAssertions;

namespace NSpectator.Specs.Running.Exceptions
{
    [TestFixture]
    [Category("RunningSpecs")]
    public class When_method_level_before_all_contains_exception : When_running_specs
    {
        class SpecClass : Spec
        {
            void before_all()
            {
                throw new BeforeAllException();
            }

            void should_fail_this_example()
            {
                It["should fail"] = () => "hello".Should().Be("hello");
            }

            void should_also_fail_this_example()
            {
                It["should also fail"] = () => "hello".Should().Be("hello");
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
                        .Should().CastTo<ExampleFailureException>();
        }

        [Test]
        public void the_second_example_should_fail_with_framework_exception()
        {
            classContext.AllExamples()
                        .Last()
                        .Exception
                        .Should().CastTo<ExampleFailureException>();
        }

        class BeforeAllException : Exception { }
    }
}
