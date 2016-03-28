using System;
using System.Linq;
using NSpectator;
using NSpectator.Domain;
using NSpectator.Specs.Running;
using NUnit.Framework;

namespace NSpectator.Specs.Running.Exceptions
{
    [TestFixture]
    [Category("RunningSpecs")]
    public class when_method_level_after_contains_exception : When_running_specs
    {
        class SpecClass : Spec
        {
            void after_each()
            {
                throw new AfterEachException();
            }

            void should_fail_this_example()
            {
                it["should fail"] = () => "hello".should_be("hello");
            }
        }

        [SetUp]
        public void setup()
        {
            Run(typeof(SpecClass));
        }

        [Test]
        public void the_example_should_fail_with_framework_exception()
        {
            classContext.AllExamples()
                        .First()
                        .Exception
                        .ShouldCastTo<ExampleFailureException>();
        }

        class AfterEachException : Exception { }
    }
}
