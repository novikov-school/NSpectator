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
using NUnit.Framework;
using FluentAssertions;

namespace NSpectator.Specs
{
    [TestFixture]
    [Category("ContextCollection")]
    public class Describe_ContextCollection
    {
        private ContextCollection contexts;

        [SetUp]
        public void Setup()
        {
            contexts = new ContextCollection();

            var context = new Context();

            context.AddExample(new ExampleBaseWrap());

            context.AddExample(new ExampleBaseWrap { Pending = true });

            context.AddExample(new ExampleBaseWrap { Exception = new Exception() });

            context.Tags.Add(Tags.Focus);

            contexts.Add(context);
        }

        [Test]
        public void Should_aggregate_examples()
        {
            contexts.Examples().Count().Should().Be(3);
        }

        [Test]
        public void Should_be_marked_with_focus()
        {
            contexts.AnyTaggedWithFocus().Should().BeTrue();
        }

        [Test]
        public void Should_aggregate_failures()
        {
            contexts.Failures().Count().Should().Be(1);
        }

        [Test]
        public void Should_aggregate_pendings()
        {
            contexts.Pendings().Count().Should().Be(1);
        }

        [Test]
        public void Should_trim_skipped_contexts()
        {
            contexts.Add(new Context());
            contexts[0].AddExample(new ExampleBaseWrap());
            contexts[0].Examples[0].HasRun = true;
            contexts.Count().Should().Be(2);
            contexts.TrimSkippedContexts();
            contexts.Count().Should().Be(1);
        }
    }
}