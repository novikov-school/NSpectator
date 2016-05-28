#region [R# naming]

// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming

#endregion

using FluentAssertions;
using NSpectator;

namespace SampleSpecs.Demo
{
    public class Describe_Extensions : Spec
    {
        void When_creating_ranges()
        {
            It["1.To(2) should be [1,2]"] = () => 1.To(2).Should().Equal(1, 2);
        }

        void Describe_Flatten()
        {
            It["[\"fifty\",\"two\"] should be fiftytwo"] = () => new[] { "fifty", "two" }.Flatten(",").Should().Be("fifty,two");
        }

        void Describe_Repeat()
        {
            const int n = 12;

            It[$"should execute {n} times"] = () =>
            {
                int x = 0;
                1.To(n).Do(_ => x++);

                x.Should().Be(n);
            };

            It[$"should execute {n} times with single call"] = () =>
            {
                int x = 0;
                1.To(n, _ => x++);

                x.Should().Be(n);
            };

            
        }
    }
}