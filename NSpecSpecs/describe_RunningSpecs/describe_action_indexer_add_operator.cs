using System.Collections.Generic;
using System.Linq;
using NSpectator;
using NSpectator.Domain;
using NUnit.Framework;

namespace NSpecSpecs.WhenRunningSpecs
{
    [TestFixture]
    [Category("RunningSpecs")]
    public class describe_action_indexer_add_operator : When_running_specs
    {
        private class SpecClass : nspec
        {
            void method_level_context()
            {
                specify = () => "Hello".should_be("Hello");
            }
        }

        [SetUp]
        public void setup()
        {
            Run(typeof(SpecClass));
        }

        [Test]
        public void should_contain_pending_test()
        {
            TheExamples().Count().should_be(1);
        }

        [Test]
        public void spec_name_should_reflect_name_specified_in_ActionRegister()
        {
            TheExamples().First().ShouldCastTo<Example>().Spec.should_be("Hello should be Hello");
        }

        // no 'specify' available for AsyncExample, hence no need to test that on ExampleBase

        private IEnumerable<object> TheExamples()
        {
            return classContext.Contexts.First().AllExamples();
        }
    }
}
