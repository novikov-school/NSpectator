#region [R# naming]
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
#endregion

using System;
using System.Linq;
using NUnit.Framework;
using FluentAssertions;

namespace NSpectator.Specs.Running
{
    [TestFixture]
    public class Describe_fail_fast : When_running_specs
    {
        class SpecClass : Spec
        {
            void given_a_spec_with_multiple_failures()
            {
                it["this one isn't a failure"] = () => "not failure".should_be("not failure");

                it["this one is a failure"] = () => "hi".should_be("hello");

                it["this one also fails"] = () => "another".should_be("failure");

                context["nested examples"] = () =>
                {
                    it["is skipped"] = () => "skipped".should_be("skipped");

                    it["is also skipped"] = () => "skipped".should_be("skipped");
                };
            }

            void another_context()
            {
                it["does not run because of failure on line 20"] = () => true.should_be_true();

                it["also does not run because of failure on line 20"] = () => true.should_be_true();
            }
        }

        [SetUp]
        public void Setup()
        {
            failFast = true;
            Run(typeof(SpecClass));
        }

        [Test]
        public void should_skip()
        {
            TheExample("is skipped").HasRun.Should().BeFalse();
        }

        [Test]
        public void only_two_examples_are_executed_one_will_be_a_failure()
        {
            AllExamples().Count(s => s.HasRun).Should().Be(2);

            TheExample("this one isn't a failure").HasRun.Should().BeTrue();

            TheExample("this one is a failure").HasRun.Should().BeTrue();
        }

        [Test]
        public void only_executed_examples_are_printed()
        {
            formatter.WrittenContexts.First().Name.Should().Be("SpecClass", StringComparison.InvariantCultureIgnoreCase);

            formatter.WrittenExamples.Count.Should().Be(2);

            formatter.WrittenExamples.First().FullName().Should().Contain("this one isn't a failure");

            formatter.WrittenExamples.Last().FullName().Should().Contain("this one is a failure");
        }
    }
}
