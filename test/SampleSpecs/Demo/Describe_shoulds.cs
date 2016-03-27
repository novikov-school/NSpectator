#region [R# naming]
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
#endregion
using NSpectator;
using FluentAssertions;

namespace SampleSpecs.Demo
{
    class describe_shoulds : Spec
    {
        void given_a_non_empty_list()
        {
            it["should not be empty"] = () => new [] { 1 }.Should().NotBeEmpty();
        }
    }
}