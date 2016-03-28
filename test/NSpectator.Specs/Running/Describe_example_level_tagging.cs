#region [R# naming]
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
#endregion
using NUnit.Framework;

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
                it["is tagged with 'mytag'", "mytag"] = () => { 1.should_be(1); };

                it["has three tags", "mytag, expect-to-failure, foobar"] = () => { 1.should_be(1); };

                it["does not have a tag"] = () => { true.should_be_true(); };
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
            TheExample("does not have a tag").Tags.should_contain("SpecClass");
        }

        [Test]
        public void is_tagged_with_at_mytag()
        {
            TheExample("is tagged with 'mytag'").Tags.Should_contain_tag("mytag");
        }

        [Test]
        public void has_three_tags_and_default_class_tag()
        {
            TheExample("has three tags").Tags.Should_contain_tag("SpecClass");
            TheExample("has three tags").Tags.Should_contain_tag("mytag");
            TheExample("has three tags").Tags.Should_contain_tag("expect-to-failure");
            TheExample("has three tags").Tags.Should_contain_tag("foobar");
            TheExample("has three tags").Tags.Count.should_be(4);
        }
    }
}