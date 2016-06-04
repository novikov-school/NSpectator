#region [R# naming]
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
#endregion
using System.Collections.Generic;
using System.Linq;
using NSpectator.Domain;
using NUnit.Framework;
using FluentAssertions;

namespace NSpectator.Specs.Running
{
    [TestFixture]
    [Category("RunningSpecs")]
    public class Describe_action_indexer_add_operator : When_running_specs
    {
        private class SpecClass : Spec
        {
            void method_level_context()
            {
                Specify = () => "Hello".Expected().ToBe("Hello");
            }
        }

        [SetUp]
        public void Setup()
        {
            Run(typeof(SpecClass));
        }

        [Test]
        public void should_contain_pending_test()
        {
            TheExamples().Should().HaveCount(1);
        }

        [Test]
        public void spec_name_should_reflect_name_specified_in_ActionRegister()
        {
            TheExamples().First().Should().CastTo<Example>().Spec.Should().Be("ToBe Hello");
        }

        // no 'specify' available for AsyncExample, hence no need to test that on ExampleBase

        private IEnumerable<object> TheExamples()
        {
            return classContext.Contexts.First().AllExamples();
        }
    }
}
