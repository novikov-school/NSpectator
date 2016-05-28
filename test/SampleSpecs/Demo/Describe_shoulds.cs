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
    class Describe_shoulds : Spec
    {
        void given_a_non_empty_list()
        {
            It["should not be empty"] = () => new[] { 1 }.Should().NotBeEmpty();
        }
    }
}