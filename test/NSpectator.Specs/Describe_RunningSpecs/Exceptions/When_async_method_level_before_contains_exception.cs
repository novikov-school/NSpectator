using System;
using System.Linq;
using NSpectator;
using NSpectator.Domain;
using NUnit.Framework;
using System.Threading.Tasks;
using NSpectator.Describer.WhenRunningSpecs;

namespace NSpectator.Describer.describe_RunningSpecs.Exceptions
{
    [TestFixture]
    [Category("RunningSpecs")]
    [Category("Async")]
    public class when_async_method_level_before_contains_exception : When_running_specs
    {
        class SpecClass : Spec
        {
            async Task before_each()
            {
                await Task.Delay(0);

                throw new BeforeException();
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
        public void the_example_should_fail_with_ContextFailureException()
        {
            classContext.AllExamples()
                        .First()
                        .Exception
                        .ShouldCastTo<ExampleFailureException>();
        }
    }
}
