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
using NSpectator.Specs.Running;
using NUnit.Framework;
using FluentAssertions;

namespace NSpectator.Specs.Running
{
    [TestFixture]
    public class Describe_abstract_class_examples : When_running_specs
    {
        abstract class AbstractClass : Spec
        {
            void Specify_an_example_in_abstract_class()
            {
                true.Should().BeTrue();
            }
        }

        abstract class AnotherAbstractClassInChain : AbstractClass
        {
            void Specify_an_example_in_another_abstract_class()
            {
                true.Should().BeTrue();
            }
        }

        class ConcreteClass : AnotherAbstractClassInChain
        {
            void Specify_an_example()
            {
                true.Should().BeTrue();
            }
        }

        class DerivedConcreteClass : ConcreteClass
        {
            void Specify_an_example_in_derived_concrete_class()
            {
                true.Should().BeTrue();
            }
        }

        [SetUp]
        public void Setup()
        {
            Run(typeof(DerivedConcreteClass), typeof(ConcreteClass), typeof(AbstractClass), typeof(AnotherAbstractClassInChain));
        }

        [Test]
        public void Abstracts_should_not_be_added_as_class_contexts()
        {
            var allClassContexts =
                contextCollection[0].AllContexts().Where(c => c is ClassContext).ToList();

            allClassContexts.should_contain(c => c.Name.Equals("ConcreteClass", StringComparison.InvariantCultureIgnoreCase));

            allClassContexts.should_not_contain(c => c.Name.Equals("AbstractClass", StringComparison.InvariantCultureIgnoreCase));

            allClassContexts.should_not_contain(c => c.Name.Equals("AnotherAbstractClassInChain", StringComparison.InvariantCultureIgnoreCase));
        }

        //TODO: specify that concrete classes must have an example of their own or they won't host 
        //abstract superclass's examples or do away with abstract classes altogether .
        //I'm not sure this complexity is warranted.

        [Test]
        public void Examples_of_abtract_classes_are_included_in_the_first_derived_concrete_class()
        {
            TheContext("ConcreteClass").Examples.Count().should_be(3);

            TheExample("specify an example in abstract class").Should_have_passed();

            TheExample("specify an example in another abstract class").Should_have_passed();
        }

        [Test]
        public void Subsequent_derived_concrete_class_do_not_contain_the_examples_from_the_abtract_class()
        {
            TheContext("DerivedConcreteClass").Examples.Count().Should().Be(1);
        }
    }
}
