using System.Linq;
using NSpectator;
using NUnit.Framework;

namespace NSpectator.Describer.WhenRunningSpecs
{
    [TestFixture]
    [Category("RunningSpecs")]
    public class describe_xdescribe : When_running_specs
    {
        class SpecClass : Spec
        {
            void method_level_context()
            {
                xdescribe["sub context"] = () =>
                {
                    it["needs an example or it gets filtered"] =
                        () => "Hello World".should_be("Hello World");
                };
            }
        }

        [SetUp]
        public void setup()
        {
            Run(typeof(SpecClass));
        }

        [Test]
        public void the_example_should_be_pending()
        {
            methodContext.Contexts.First().Examples.First().Pending.should_be(true);
        }
    }
}
