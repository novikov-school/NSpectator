#region [R# naming]
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
#endregion
using NUnit.Framework;
using FluentAssertions;

namespace NSpectator.Specs.Running.BeforeAndAfter
{
    [TestFixture]
    public class When_there_are_no_specs : When_running_specs
    {
        [SetUp]
        public void Setup()
        {
            Sequence_spec.sequence = string.Empty;
        }

        class Before_all_example_spec : Sequence_spec
        {
            void before_all()
            {
                sequence = "executed";
            }
        }

        [Test]
        public void before_all_is_not_executed()
        {
            Run(typeof (Before_all_example_spec));

            Sequence_spec.sequence.Should().BeEmpty();
        }

        class Before_each_example_spec : Sequence_spec
        {
            void before_each()
            {
                sequence = "executed";
            }
        }

        [Test]
        public void before_each_is_not_executed()
        {
            Run(typeof (Before_each_example_spec));

            Sequence_spec.sequence.Should().BeEmpty();
        }

        class After_each_example_spec : Sequence_spec
        {
            void after_each()
            {
                sequence = "executed";
            }
        }

        [Test]
        public void after_each_is_not_executed()
        {
            Run(typeof (After_each_example_spec));

            Sequence_spec.sequence.Should().BeEmpty();
        }

        class After_all_example_spec : Sequence_spec
        {
            void after_all()
            {
                sequence = "executed";
            }
        }

        [Test]
        public void after_all_is_not_executed()
        {
            Run(typeof (After_all_example_spec));

            Sequence_spec.sequence.Should().BeEmpty();
        }
    }
}