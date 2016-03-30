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
    public class Describe_DefaultConventions
    {
        protected Conventions defaultConvention;

        [SetUp]
        public void setup_base()
        {
            defaultConvention = new DefaultConventions();

            defaultConvention.Initialize();
        }
    }

    [TestFixture]
    [Category("DefaultConvention")]
    public class When_determining_before_methods : Describe_DefaultConventions
    {
        [Test]
        public void Should_match_before_each()
        {
            ShouldBeBefore("before_each");
        }

        [Test]
        public void Should_ignore_case()
        {
            ShouldBeBefore("Before_Each");
        }

        void ShouldBeBefore(string methodName)
        {
            defaultConvention.IsMethodLevelBefore(methodName).Should().BeTrue();

            defaultConvention.IsMethodLevelContext(methodName).Should().BeFalse();
        }
    }

    [TestFixture]
    [Category("DefaultConvention")]
    public class When_determining_act_methods : Describe_DefaultConventions
    {
        [Test]
        public void Should_match_act_each()
        {
            ShouldBeAct("act_each");
        }

        [Test]
        public void Should_ignore_case()
        {
            ShouldBeAct("Act_Each");
        }

        void ShouldBeAct(string methodName)
        {
            defaultConvention.IsMethodLevelAct(methodName).Should().BeTrue();

            defaultConvention.IsMethodLevelContext(methodName).Should().BeFalse();
        }
    }

    [TestFixture]
    [Category("DefaultConvention")]
    public class When_determining_example_methods : Describe_DefaultConventions
    {
        [Test]
        public void Should_match_it()
        {
            ShouldBeExample("it_Should_be_true");
        }

        [Test]
        public void Should_match_specify()
        {
            ShouldBeExample("specify_Should_be_true");
        }

        [Test]
        public void Should_ignore_case_when_matching_it()
        {
            ShouldBeExample("It_ShouldBe_True");
        }

        [Test]
        public void Should_ignore_case_when_matching_specify()
        {
            ShouldBeExample("Specify_ShouldBe_True");
        }

        [Test]
        public void Should_not_match_IterationShould()
        {
            defaultConvention.IsMethodLevelExample("IterationShould").Should().BeFalse();
        }

        void ShouldBeExample(string methodName)
        {
            defaultConvention.IsMethodLevelExample(methodName).Should().BeTrue();

            defaultConvention.IsMethodLevelContext(methodName).Should().BeFalse();
        }
    }

    [TestFixture]
    [Category("DefaultConvention")]
    public class When_determining_context_methods : Describe_DefaultConventions
    {
        [Test]
        public void Should_be_match_describe_a_specification()
        {
            defaultConvention.IsMethodLevelContext("describe_a_specification").Should().BeTrue();
        }

        [Test]
        public void Should_ignore_case()
        {
            defaultConvention.IsMethodLevelContext("Describe_A_Specification").Should().BeTrue();
        }

        [Test]
        public void Should_not_match_methods_dont_contain_underscores()
        {
            defaultConvention.IsMethodLevelContext("GivenUser").Should().BeFalse();
        }
    }

}
