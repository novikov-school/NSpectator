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
using NSpectator.Domain;
using NUnit.Framework;
using FluentAssertions;
using NSpectator.Domain.Formatters;

namespace NSpectator.Specs.Running
{
    [TestFixture]
    public class When_running_specs
    {
        [SetUp]
        public void InitializeRunnerInvocation()
        {
            formatter = new FormatterStub();
        }

        protected When_running_specs Run(params Type[] types)
        {
            //if (types.Count() == 1) tags = types.First().Name;

            this.types = types;

            var tagsFilter = new Tags().Parse(tags);

            builder = new ContextBuilder(new SpecFinder(types), tagsFilter, new DefaultConventions());

            runner = new ContextRunner(tagsFilter, formatter, failFast);

            contextCollection = builder.Contexts();

            contextCollection.Build();

            classContext = contextCollection
                .AllContexts()
                .Where(c => c is ClassContext)
                .Cast<ClassContext>()
                .FirstOrDefault(c => types.Contains(c.type));

            methodContext = contextCollection.AllContexts().FirstOrDefault(c => c is MethodContext);

            runner.Run(contextCollection);

            return this;
        }

        protected Context TheContext(string name)
        {
            var theContext = contextCollection
                .SelectMany(rootContext => rootContext.AllContexts())
                .SelectMany(contexts => contexts.AllContexts().Where(context => context.Name.ToLower() == name.ToLower())).First();

            theContext.Name.Should().BeEquals(name, StringComparison.InvariantCultureIgnoreCase, "because we ignore case");

            return theContext;
        }

        protected IEnumerable<ExampleBase> AllExamples()
        {
            return contextCollection
                .SelectMany(rootContext => rootContext.AllExamples());
        }

        protected ExampleBase TheExample(string name)
        {
            var theExample = contextCollection
                .SelectMany(rootContext => rootContext.AllContexts())
                .SelectMany(contexts => contexts.AllExamples().Where(example => example.Spec.ToLower() == name.ToLower())).FirstOrDefault();

            if (theExample == null) Assert.Fail("Did not find example named: " + name);

            theExample.Spec.Should().BeEquals(name, StringComparison.InvariantCultureIgnoreCase);

            return theExample;
        }

        protected int TheExampleCount()
        {
            var theExamples = contextCollection
                .SelectMany(rootContext => rootContext.AllContexts())
                .SelectMany(contexts => contexts.AllExamples())
                    .Distinct();

            return theExamples.Count();
        }

        protected ContextBuilder builder;
        protected ContextCollection contextCollection;
        protected ClassContext classContext;
        protected bool failFast;
        protected Context methodContext;
        protected ContextCollection contexts;
        protected FormatterStub formatter;
        ContextRunner runner;
        protected Type[] types;
        protected string tags;
    }
}