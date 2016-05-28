#region [R# naming]
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
#endregion
using System.Linq;
using NSpectator.Domain;
using NUnit.Framework;
using System.Threading.Tasks;
using FluentAssertions;

namespace NSpectator.Specs.Running.Exceptions
{
    [TestFixture]
    [Category("RunningSpecs")]
    [Category("Async")]
    public class When_async_method_level_before_contains_exception : When_running_specs
    {
        class SpecClass : Spec
        {
            async Task before_each()
            {
                await Task.Delay(0);

                throw new BeforeException();
            }

            void should_fail_this_example()
            {
                It["should fail"] = () => "hello".Should().Be("hello");
            }
        }

        [SetUp]
        public void Setup()
        {
            Run(typeof(SpecClass));
        }

        [Test]
        public void the_example_should_fail_with_ContextFailureException()
        {
            classContext.AllExamples()
                        .First()
                        .Exception
                        .Should().CastTo<ExampleFailureException>();
        }
    }
}
