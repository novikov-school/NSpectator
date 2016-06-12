#region [R# naming]
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
#endregion
using System.Reflection;
using NSpectator.Domain;
using NSpectator.Domain.Formatters;
using NUnit.Framework;
using FluentAssertions;

namespace NSpectator.Specs.ClassContextBug
{
    public class NestContextsTests
    {
        public void Debug()
        {
            // the specification class you want to test
            // this can be a regular expression
            var testClassYouWantToDebug = "NSpectator.Specs.ClassContextBug.Child";

            // initialize NSpectator specfinder
            var finder = new SpecFinder(
                new Reflector(Assembly.GetExecutingAssembly().Location),
                testClassYouWantToDebug);

            // initialize NSpectator builder
            var builder = new ContextBuilder(finder, new DefaultConventions());

            // this line runs the tests you specified in the filter
            var noTagsFilter = new Tags();
            TestFormatter formatter = new TestFormatter();
            new ContextRunner(noTagsFilter, formatter, false).Run(builder.Contexts().Build());

            Context grandParent = formatter.Contexts[0];

            grandParent.Name.Should().Be("Grand Parent");
            grandParent.Contexts.Should().HaveCount(2);
            grandParent.Contexts[0].Name.Should().Be("Grand Parent Context");
            grandParent.Contexts[1].Name.Should().Be("Parent");
            grandParent.Contexts[0].Examples[0].Spec.Should().Be("TestValue should be \"Grand Parent!!!\"");
            grandParent.Contexts[0].Examples[0].Exception.Should().BeNull();
            grandParent.Contexts[0].Examples[0].Pending.Should().BeFalse();

            Context parent = formatter.Contexts[0].Contexts[1];
            parent.Name.Should().Be("Parent");
            parent.Contexts.Should().HaveCount(2);
            parent.Contexts[0].Name.Should().Be("Parent Context");
            parent.Contexts[1].Name.Should().Be("Child");
            parent.Contexts[0].Examples[0].Spec.Should().Be("TestValue should be \"Grand Parent.Parent!!!@@@\"");
            parent.Contexts[0].Examples[0].Exception.Should().BeNull();
            parent.Contexts[0].Examples[0].Pending.Should().BeFalse();

            Context child = formatter.Contexts[0].Contexts[1].Contexts[1];
            child.Name.Should().Be("Child");
            child.Contexts.Should().HaveCount(1);
            child.Contexts[0].Name.Should().Be("Child Context");
            child.Contexts[0].Examples[0].Spec.Should().Be("TestValue should be \"Grand Parent.Parent.Child!!!@@@###\"");
            child.Contexts[0].Examples[0].Exception.Should().BeNull();
            child.Contexts[0].Examples[0].Pending.Should().BeFalse();
        }
    }

    public class TestFormatter : IFormatter
    {
        public ContextCollection Contexts { get; set; }

        public void Write(ContextCollection contexts)
        {
            this.Contexts = contexts;
        }
    }
}