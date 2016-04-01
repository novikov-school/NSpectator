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
    class Describe_Extensions : Spec
    {
        void When_creating_ranges()
        {
            it["1.To(2) should be [1,2]"] = () => 1.To(2).Should().Equal(1, 2);
        }

        void Describe_Flatten()
        {
            it["[\"fifty\",\"two\"] should be fiftytwo"] = () => new[] { "fifty", "two" }.Flatten(",").Should().Be("fifty,two");
        }
    }
}