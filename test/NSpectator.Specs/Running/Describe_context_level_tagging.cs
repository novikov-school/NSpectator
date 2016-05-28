using NUnit.Framework;
using FluentAssertions;

namespace NSpectator.Specs.Running
{
    [TestFixture]
    public class Describe_context_level_tagging : When_running_specs
    {
        class SpecClass : Spec
        {
            void has_tags_in_contexts()
            {
                Context["is tagged with 'mytag'", "mytag"] = () =>
                {
                    It["is tagged with 'mytag'"] = () => { 1.Should().Be(1); };
                };

                Context["has three tags", "mytag, expect-to-failure, foobar"] = () =>
                {
                    It["has three tags"] = () => { 1.Should().Be(1); };
                };

                Context["does not have a tag"] = () =>
                {
                    It["does not have a tag"] = () => { true.Should().BeTrue(); };
                };

                Context["has a nested context", "nested-tag"] = () =>
                {
                    Context["is the nested context"] = () =>
                    {
                        It["is the nested example"] = () => { true.Should().BeTrue(); };
                    };
                };
            }
        }

        [SetUp]
        public void Setup()
        {
            Run(typeof(SpecClass));
        }

        [Test]
        public void it_only_contains_default_tag()
        {
            TheContext("does not have a tag").Tags.Should().Contain("SpecClass");
        }

        [Test]
        public void is_tagged_with_mytag()
        {
            TheContext("is tagged with 'mytag'").Tags.Should().Contain("mytag");
        }

        [Test]
        public void has_three_tags_and_the_default()
        {
            TheContext("has three tags").Tags.Should().Contain("SpecClass");
            TheContext("has three tags").Tags.Should().Contain("mytag");
            TheContext("has three tags").Tags.Should().Contain("expect-to-failure");
            TheContext("has three tags").Tags.Should().Contain("foobar");
            TheContext("has three tags").Tags.Should().HaveCount(4);
        }

        [Test]
        public void nested_contexts_should_inherit_the_tag()
        {
            TheContext("has a nested context").Tags.Should().Contain("nested-tag");
            TheContext("is the nested context").Tags.Should().Contain("nested-tag");
        }

        [Test]
        public void nested_examples_should_inherit_the_tag()
        {
            TheContext("has a nested context").Tags.Should().Contain("nested-tag");
            TheExample("is the nested example").Tags.Should().Contain("nested-tag");
        }
    }
}