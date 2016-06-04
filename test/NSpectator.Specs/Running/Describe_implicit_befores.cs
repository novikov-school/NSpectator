#region [R# naming]
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
#endregion
using System.Collections.Generic;
using System.Linq;
using NSpectator.Domain;
using NUnit.Framework;
using FluentAssertions;

namespace NSpectator.Specs.Running
{
    [TestFixture]
    [Category("RunningSpecs")]
    public class Describe_implicit_befores : When_running_specs
    {
        class SpecClass : Spec
        {
            void method_level_context()
            {
                List<int> ints = new List<int>();
                ints.Add(5);

                It["should have two entries"] = () =>
                {
                    ints.Add(16);
                    ints.Should().HaveCount(1);
                };

                Specify = () => ints.Should().HaveCount(1, "because nothing more added");
            }
        }

        private IEnumerable<ExampleBase> TheMethodContextExamples()
        {
            return classContext.Contexts.First().AllExamples();
        }

        [Test]
        public void Should_not_be_possible_to_test()
        {
            
        }
    }
}
