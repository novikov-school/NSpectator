#region [R# naming]
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
#endregion
using NSpectator.Domain;
using NUnit.Framework;
using FluentAssertions;

namespace NSpectator.Specs
{
    [TestFixture]
    [Category("Example")]
    public class When_parsing_expressions
    {
        [Test]
        public void Should_clear_quotes()
        {
            new Example(() => "hello".Is("hello")).Spec.Should().Be("hello Is hello");
        }

        // no 'specify' available for AsyncExample, hence no way to test that on AsyncExample
    }

    [TestFixture]
    [Category("Example")]
    public class Describe_ExampleBase
    {
        [Test]
        public void Should_concatenate_its_contexts_name_into_a_full_name()
        {
            var context = new Context("context name");

            var example = new ExampleBaseWrap("example name");

            context.AddExample(example);

            example.FullName().Should().Be("context name. example name.");
        }

        [Test]
        public void Should_be_marked_as_pending_if_parent_context_is_pending()
        {
            var context = new Context("pending context", null, isPending: true);

            var example = new ExampleBaseWrap("example name");

            context.AddExample(example);

            example.Pending.Should().BeTrue();
        }

        [Test]
        public void Should_be_marked_as_pending_if_any_parent_context_is_pending()
        {
            var parentContext = new Context("parent pending context", null, isPending: true);
            var context = new Context("not pending");
            var example = new ExampleBaseWrap("example name");

            parentContext.AddContext(context);

            context.AddExample(example);

            example.Pending.Should().BeTrue();
        }
    }
}