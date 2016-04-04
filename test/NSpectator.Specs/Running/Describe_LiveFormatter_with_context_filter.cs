#region [R# naming]
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
#endregion
using System.Collections.Generic;
using System.Reflection;
using NSpectator.Domain;
using NSpectator.Domain.Formatters;
using NUnit.Framework;
using FluentAssertions;

namespace NSpectator.Specs.Running
{
    [TestFixture]
    public class Describe_LiveFormatter_with_context_filter : When_running_specs
    {
        class Liveconsole_sample_spec : Spec
        {
            void a_context_with_a_pending_example()
            {
                it["pending example"] = todo;
            }

            void a_context_with_a_grandchild_example()
            {
                context["a context with an example"] = () =>
                {
                    it["1 is 1"] = () => 1.Expected().ToBe(1);
                };
            }

            void a_context_without_an_example() { }
        }

        [SetUp]
        public void Setup()
        {
            formatter = new FormatterStub();

            var invocation = new RunnerInvocation(Assembly.GetExecutingAssembly().Location, typeof(Liveconsole_sample_spec).Name, formatter, false);

            contexts = invocation.Run();
        }

        [Test]
        public void it_writes_the_example()
        {
            formatter.WrittenExamples.Should().Contain(contexts.FindExample("1 is 1"));
        }

        [Test]
        public void it_writes_contexts_with_examples()
        {
            formatter.WrittenContexts.Should().Contain(contexts.Find("a context with an example"));
        }

        [Test]
        public void it_writes_context_with_grandchild_examples()
        {
            formatter.WrittenContexts.Should().Contain(contexts.Find("a context with a grandchild example"));
        }

        [Test]
        public void it_skips_contexts_without_examples()
        {
            formatter.WrittenContexts.Should().NotContain(c => c.Name == "a context without an example");
        }

        [Test]
        public void it_skips_contexts_that_were_not_included()
        {
            formatter.WrittenContexts.Should().NotContain(c => c.Name == "SampleSpec");
        }

        [Test]
        public void it_skips_examples_whose_contexts_were_not_included()
        {
            formatter.WrittenExamples.Should().NotContain(e => e.Spec == "an excluded example by ancestry");
        }

        [Test]
        public void it_writes_the_pending_example()
        {
            formatter.WrittenExamples.Should().Contain(contexts.FindExample("pending example"));
        }
    }

    public class FormatterStub : ConsoleFormatter, IFormatter, ILiveFormatter
    {
        public List<Context> WrittenContexts;
        public List<ExampleBase> WrittenExamples;

        public FormatterStub()
        {
            WrittenContexts = new List<Context>();
            WrittenExamples = new List<ExampleBase>();
        }

        public new void Write(ContextCollection contexts)
        {
        }

        public new void Write(Context context)
        {
            base.Write(context);
            WrittenContexts.Add(context);
        }

        public new void Write(ExampleBase example, int level)
        {
            base.Write(example, level);
            WrittenExamples.Add(example);
        }
    }
}