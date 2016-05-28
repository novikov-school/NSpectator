#region [R# naming]
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
#endregion
using NUnit.Framework;
using FluentAssertions;

namespace NSpectator.Specs.Running
{
    [TestFixture]
    [Category("RunningSpecs")]
    public class Describe_example_level_tagging : When_running_specs
    {
        class SpecClass : Spec
        {
            void has_tags_in_examples()
            {
                It["is tagged with 'mytag'", "mytag"] = () => { 1.Should().Be(1); };

                It["has three tags", "mytag, expect-to-failure, foobar"] = () => { 1.Should().Be(1); };

                It["does not have a tag"] = () => { true.Should().BeTrue(); };
            }
        }

        [SetUp]
        public void Setup()
        {
            Run(typeof(SpecClass));
        }

        [Test]
        public void it_only_has_the_default_class_tag()
        {
            TheExample("does not have a tag").Tags.Should().Contain("SpecClass");
        }

        [Test]
        public void is_tagged_with_at_mytag()
        {
            TheExample("is tagged with 'mytag'").Tags.Should().Contain("mytag");
        }

        [Test]
        public void has_three_tags_and_default_class_tag()
        {
            TheExample("has three tags").Tags.Should_contain_tag("SpecClass");
            TheExample("has three tags").Tags.Should_contain_tag("mytag");
            TheExample("has three tags").Tags.Should_contain_tag("expect-to-failure");
            TheExample("has three tags").Tags.Should_contain_tag("foobar");
            TheExample("has three tags").Tags.Count.Should().Be(4);
        }
    }
}