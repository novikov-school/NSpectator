#region [R# naming]
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
#endregion
using System;
using System.Linq;
using NSpectator;
using NSpectator.Domain;
using NUnit.Framework;
using NSpectator.Specs;
using FluentAssertions;

namespace NSpectator.Specs
{
    [TestFixture]
    public class Describe_Context
    {
        [Test]
        public void it_should_make_a_sentence_from_underscored_context_names()
        {
            new Context("i_love_underscores").Name.Should().Be("i love underscores");
        }
    }

    [TestFixture]
    public class When_counting_failures
    {
        [Test]
        public void given_nested_contexts_and_the_child_has_a_failure()
        {
            var child = new Context("child");

            child.AddExample(new ExampleBaseWrap { Exception = new Exception() });

            var parent = new Context("parent");

            parent.AddContext(child);

            parent.Failures().Count().Should().Be(1);
        }
    }

    public class Child_act : Parent_act
    {
        void act_each()
        {
            actResult += "child";
        }
    }

    public class Parent_act : Spec
    {
        public string actResult;
        void act_each()
        {
            actResult = "parent";
        }
    }

    [TestFixture]
    public class When_creating_act_contexts_for_derived_class
    {
        [SetUp]
        public void setup()
        {
            parentContext = new ClassContext(typeof(Parent_act));

            childContext = new ClassContext(typeof(Child_act));

            parentContext.AddContext(childContext);

            instance = new Child_act();

            parentContext.Build();
        }

        [Test]
        public void should_run_the_acts_in_the_right_order()
        {
            childContext.RunActs(instance);

            instance.actResult.should_be("parentchild");
        }

        ClassContext childContext, parentContext;

        Child_act instance;
    }

    public class Child_before : Parent_before
    {
        void before_each()
        {
            beforeResult += "child";
        }
    }

    public class Parent_before : Spec
    {
        public string beforeResult;
        void before_each()
        {
            beforeResult = "parent";
        }
    }

    [TestFixture]
    public class when_creating_contexts_for_derived_classes
    {
        [SetUp]
        public void setup()
        {
            conventions = new DefaultConventions();

            conventions.Initialize();

            parentContext = new ClassContext(typeof(Parent_before), conventions);

            childContext = new ClassContext(typeof(Child_before), conventions);

            parentContext.AddContext(childContext);
        }

        [Test]
        public void the_root_context_should_be_the_parent()
        {
            parentContext.Name.Should().Be(typeof(Parent_before).Name.Replace("_", " "));
        }

        [Test]
        public void it_should_have_the_child_as_a_context()
        {
            parentContext.Contexts.First().Name.Should().Be(typeof(Child_before).Name.Replace("_", " "));
        }

        private ClassContext childContext;

        private DefaultConventions conventions;

        private ClassContext parentContext;
    }

    [TestFixture]
    public class when_creating_before_contexts_for_derived_class
    {
        [SetUp]
        public void setup()
        {
            parentContext = new ClassContext(typeof(Parent_before));

            childContext = new ClassContext(typeof(Child_before));

            parentContext.AddContext(childContext);

            instance = new Child_before();

            parentContext.Build();
        }

        [Test]
        public void should_run_the_befores_in_the_proper_order()
        {
            childContext.RunBefores(instance);

            instance.beforeResult.should_be("parentchild");
        }

        ClassContext childContext, parentContext;

        Child_before instance;
    }

    public class trimming_contexts
    {
        protected Context rootContext;

        [SetUp]
        public void SetupBase()
        {
            rootContext = new Context("root context");
        }

        public Context GivenContextWithNoExamples()
        {
            return new Context("context with no example");
        }

        public Context GivenContextWithExecutedExample()
        {
            var context = new Context("context with example");
            context.AddExample(new ExampleBaseWrap("example"));
            context.Examples[0].HasRun = true;

            return context;
        }
    }

    [TestFixture]
    public class trimming_unexecuted_contexts_one_level_deep : trimming_contexts
    {
        Context contextWithExample;

        Context contextWithoutExample;
        
        [SetUp]
        public void given_nested_contexts_with_and_without_executed_examples()
        {
            contextWithoutExample = GivenContextWithNoExamples();

            rootContext.AddContext(contextWithoutExample);

            contextWithExample = GivenContextWithExecutedExample();

            rootContext.AddContext(contextWithExample);

            rootContext.Contexts.Count().should_be(2);

            rootContext.TrimSkippedDescendants();
        }

        [Test]
        public void it_contains_context_with_example()
        {
            rootContext.AllContexts().should_contain(contextWithExample);
        }

        [Test]
        public void it_doesnt_contain_empty_context()
        {
            rootContext.AllContexts().should_not_contain(contextWithoutExample);
        }
    }

    [TestFixture]
    public class trimming_unexecuted_contexts_two_levels_deep : trimming_contexts
    {
        Context childContext;

        Context parentContext;

        public void GivenContextWithAChildContextThatHasExample()
        {
            parentContext = GivenContextWithNoExamples();

            childContext = GivenContextWithExecutedExample();

            parentContext.AddContext(childContext);

            rootContext.AddContext(parentContext);

            rootContext.AllContexts().should_contain(parentContext);
        }

        public void GivenContextWithAChildContextThatHasNoExample()
        {
            parentContext = GivenContextWithNoExamples();

            childContext = GivenContextWithNoExamples();

            parentContext.AddContext(childContext);

            rootContext.AddContext(parentContext);

            rootContext.AllContexts().should_contain(parentContext);
        }

        [Test]
        public void it_keeps_all_contexts_if_examples_exists_at_level_2()
        {
            GivenContextWithAChildContextThatHasExample();

            rootContext.TrimSkippedDescendants();

            rootContext.AllContexts().should_contain(parentContext);

            rootContext.AllContexts().should_contain(childContext);
        }

        [Test]
        public void it_removes_all_contexts_if_no_child_context_has_examples()
        {
            GivenContextWithAChildContextThatHasNoExample();

            rootContext.TrimSkippedDescendants();

            rootContext.AllContexts().should_not_contain(parentContext);

            rootContext.AllContexts().should_not_contain(childContext);
        }
    }
}