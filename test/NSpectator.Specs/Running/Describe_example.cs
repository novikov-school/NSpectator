#region [R# naming]
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
#endregion
using System;
using NUnit.Framework;
using FluentAssertions;

namespace NSpectator.Specs.Running
{
    [TestFixture]
    [Category("RunningSpecs")]
    public class Describe_example : When_running_specs
    {
        class SpecClass : Spec
        {
            void it_changes_status_after_run()
            {
            }

            void it_passes()
            {
            }

            void it_fails()
            {
                throw new Exception();
            }
        }

        [Test]
        public void execution_status_changes_after_run()
        {
            Run(typeof(SpecClass));
                
            var ex = TheExample("it changes status after run");
            ex.HasRun.Should().BeTrue();
        }

        [Test]
        public void passing_status_is_passed_when_it_succeeds()
        {
            Run(typeof(SpecClass));

            TheExample("it passes").Should().HavePassed();
        }

        [Test]
        public void passing_status_is_not_passed_when_it_fails()
        {
            Run(typeof(SpecClass));

            TheExample("it fails").Should().HaveFailed();
        }

        class SpecClassWithAnonymouseLambdas : Spec
        {
            void describe_specs_with_anonymous_lambdas()
            {
                Context["Some context with anonymous lambdas"] = () =>
                {
                    It["has an anonymous lambda"] = () =>
                    {
                    };
                };
            }
        }

        [Test]
        public void finds_and_runs_three_class_level_examples()
        {
            Run(typeof(SpecClass));

            TheExampleCount().Should().Be(3);
        }

        [Test]
        public void finds_and_runs_only_one_example_ignoring_anonymous_lambdas()
        {
            Run(typeof(SpecClassWithAnonymouseLambdas));

            TheExampleCount().Should().Be(1);
        }
    }
}