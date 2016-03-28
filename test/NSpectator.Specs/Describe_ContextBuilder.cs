#region [R# naming]
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NSpectator.Domain;
using NUnit.Framework;
using FluentAssertions;

namespace NSpectator.Specs
{
    [TestFixture]
    public class Describe_ContextBuilder
    {
        protected Mock<ISpecFinder> finderMock;

        protected ContextBuilder builder;

        protected List<Type> typesForFinder;

        protected List<Context> contexts;

        [SetUp]
        public void setup_base()
        {
            typesForFinder = new List<Type>();

            finderMock = new Mock<ISpecFinder>();
            finderMock.Setup(f => f.SpecClasses()).Returns(typesForFinder);
            
            DefaultConventions conventions = new DefaultConventions();

            conventions.Initialize();

            builder = new ContextBuilder(finderMock.Object, conventions);
        }

        public void GivenTypes(params Type[] types)
        {
            typesForFinder.AddRange(types);
        }

        public IList<Context> TheContexts()
        {
            return builder.Contexts();
        }

        [Test]
        public void Should_be_parent_class_and_setup_works()
        {

        }
    }

    [TestFixture]
    public class When_building_contexts : Describe_ContextBuilder
    {
        public class Child : Parent { }

        public class Sibling : Parent { }

        public class Parent : Spec { }

        [SetUp]
        public void setup()
        {
            GivenTypes(typeof(Child), typeof(Sibling), typeof(Parent));

            TheContexts();
        }

        [Test]
        public void should_get_specs_from_specFinder()
        {
            finderMock.Verify(f => f.SpecClasses());
            // finderMock.AssertWasCalled(f => f.SpecClasses());
        }

        [Test]
        public void the_primary_context_should_be_parent()
        {
            TheContexts().First().Name.Should().Be(typeof(Parent).Name, StringComparison.InvariantCultureIgnoreCase);
        }

        [Test]
        public void the_parent_should_have_the_child_context()
        {
            TheContexts().First().Contexts.First().Name.Should().Be(typeof(Child).Name, StringComparison.InvariantCultureIgnoreCase);
        }

        [Test]
        public void it_should_only_have_the_parent_once()
        {
            TheContexts().Count().should_be(1);
        }

        [Test]
        public void it_should_have_the_sibling()
        {
            TheContexts().First().Contexts.Should().Contain(c => c.Name.Equals(typeof(Sibling).Name, StringComparison.InvariantCultureIgnoreCase));
        }
    }

    [TestFixture]
    public class When_finding_method_level_examples : Describe_ContextBuilder
    {
        class Class_with_method_level_example : Spec
        {
            void it_should_be_considered_an_example() { }

            void specify_should_be_considered_as_an_example() { }

            // -----

            async Task it_should_be_considered_an_example_with_async() { await Task.Delay(0); }

            async Task<long> it_should_be_considered_an_example_with_async_result() { await Task.Delay(0); return 0L; }

            async void it_should_be_considered_an_example_with_async_void() { await Task.Delay(0); }

            async Task specify_should_be_considered_as_an_example_with_async() { await Task.Delay(0); }
        }

        [SetUp]
        public void Setup()
        {
            GivenTypes(typeof(Class_with_method_level_example));
        }

        [Test]
        public void should_find_method_level_example_if_the_method_name_starts_with_the_word_IT()
        {
            ShouldContainExample("it should be considered an example");
        }

        [Test]
        public void should_find_async_method_level_example_if_the_method_name_starts_with_the_word_IT()
        {
            ShouldContainExample("it should be considered an example with async");
        }

        [Test]
        public void should_find_async_method_level_example_if_the_method_name_starts_with_the_word_IT_and_it_returns_result()
        {
            ShouldContainExample("it should be considered an example with async result");
        }

        [Test]
        public void should_find_async_method_level_example_if_the_method_name_starts_with_the_word_IT_and_it_returns_void()
        {
            ShouldContainExample("it should be considered an example with async void");
        }

        [Test]
        public void should_find_method_level_example_if_the_method_starts_with_SPECIFY()
        {
            ShouldContainExample("specify should be considered as an example");
        }

        [Test]
        public void should_find_async_method_level_example_if_the_method_starts_with_SPECIFY()
        {
            ShouldContainExample("specify should be considered as an example with async");
        }

        [Test]
        public void should_exclude_methods_that_start_with_ITs_from_child_context()
        {
            TheContexts().First().Contexts.Count.should_be(0);
        }

        private void ShouldContainExample(string exampleName)
        {
            TheContexts().First().Examples.Any(s => s.Spec == exampleName);
        }
    }

    [TestFixture]
    public class when_building_method_contexts
    {
        private Context classContext;

        private class SpecClass : Spec
        {
            public void public_method() { }

            void private_method() { }

            void before_each() { }

            void act_each() { }
        }

        [SetUp]
        public void setup()
        {
            var finderMock = new Mock<ISpecFinder>();

            DefaultConventions defaultConvention = new DefaultConventions();

            defaultConvention.Initialize();

            var builder = new ContextBuilder(finderMock.Object, defaultConvention);

            classContext = new Context("class");

            builder.BuildMethodContexts(classContext, typeof(SpecClass));
        }

        [Test]
        public void it_should_add_the_public_method_as_a_sub_context()
        {
            classContext.Contexts.should_contain(c => c.Name == "public method");
        }

        [Test]
        public void it_should_not_create_a_sub_context_for_the_private_method()
        {
            classContext.Contexts.should_contain(c => c.Name == "private method");
        }

        [Test]
        public void it_should_disregard_method_called_before_each()
        {
            classContext.Contexts.should_not_contain(c => c.Name == "before each");
        }

        [Test]
        public void it_should_disregard_method_called_act_each()
        {
            classContext.Contexts.should_not_contain(c => c.Name == "act each");
        }
    }

    [TestFixture]
    public class when_building_class_and_method_contexts_with_tag_attributes : Describe_ContextBuilder
    {
        [Tag("@class-tag")]
        class SpecClass : Spec
        {
            [Tag("@method-tag")]
            void public_method() { }
        }

        [SetUp]
        public void setup()
        {
            GivenTypes(typeof(SpecClass));
        }

        [Test]
        public void it_should_tag_class_context()
        {
            var classContext = TheContexts()[0];
            classContext.Tags.Should_contain_tag("@class-tag");
        }

        [Test]
        public void it_should_tag_method_context()
        {
            var methodContext = TheContexts()[0].Contexts[0];
            methodContext.Tags.Should_contain_tag("@method-tag");
        }
    }

    [TestFixture]
    [Category("ContextBuilder")]
    public class Describe_second_order_inheritance : Describe_ContextBuilder
    {
        class Base_spec : Spec { }

        class Child_spec : Base_spec { }

        class Grand_child_spec : Child_spec { }

        [SetUp]
        public void Setup()
        {
            GivenTypes(
                typeof(Base_spec),
                typeof(Child_spec),
                typeof(Grand_child_spec));
        }

        [Test]
        public void the_root_context_should_be_base_spec()
        {
            TheContexts().First().Name.should_be(typeof(Base_spec));
        }

        [Test]
        public void the_next_context_should_be_derived_spec()
        {
            TheContexts().First().Contexts.First().Name.should_be(typeof(Child_spec));
        }

        [Test]
        public void the_next_next_context_should_be_derived_spec()
        {
            TheContexts().First().Contexts.First().Contexts.First().Name.should_be(typeof(Grand_child_spec));
        }
    }

    public static class InheritanceExtentions
    {
        public static void should_be(this string actualName, Type expectedType)
        {
            expectedType.Name.Replace("_", " ").Should().Be(actualName, StringComparison.InvariantCultureIgnoreCase);
        }

    }
}