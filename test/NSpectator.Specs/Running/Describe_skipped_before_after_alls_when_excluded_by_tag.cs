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
    public class Describe_skipped_before_alls_when_excluded_by_tag : When_running_specs
    {
        class InnocentBystander : Spec
        {
            public static string sequence = string.Empty;

            void before_all()
            {
                sequence = "should not run innocent bystander before_all";
            }

            void context_bystander()
            {
                It["should not run because of tags"] = () => "not tagged".Should().Be("not tagged");
            }
        }

        class Target : Spec
        {
            void it_specifies_something()
            {
                Specify = () => true.Expected().True();
            }
        }

        [SetUp]
        public void Setup()
        {
            tags = "Target";
            Run(typeof(Target), typeof(InnocentBystander));
        }

        [Test]
        public void should_skip_innocent_bystander_before_all()
        {
            InnocentBystander.sequence.Should().BeEmpty();
        }
    }

    [TestFixture]
    public class Describe_skipped_after_alls_when_excluded_by_tag : When_running_specs
    {
        class InnocentBystander : Spec
        {
            public static string sequence = string.Empty;

            void context_bystander()
            {
                It["should not run because of tags"] = () => "not tagged".Should().Be("not tagged");
            }

            void after_all()
            {
                sequence = "should not run innocent bystander after_all";
            }
        }

        class Target : Spec
        {
            void it_specifies_something()
            {
                Specify = () => true.Expected().True();
            }
        }

        [SetUp]
        public void Setup()
        {
            tags = "Target";
            Run(typeof(Target), typeof(InnocentBystander));
        }

        [Test]
        public void should_skip_innocent_bystander_after_all()
        {
            InnocentBystander.sequence.Expected().ToBeEmpty();
        }
    }
}
