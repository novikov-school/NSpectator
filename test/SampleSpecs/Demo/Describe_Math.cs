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
    class Describe_Math : Spec
    {
        void Verify_strictly_increasing_numbers()
        {
            new[]
                {
                1, 2, 3,
                4, 5, 6,
                7, 8, 9
                }.EachConsecutive2(
                    (smaller, larger) =>
                        it["{0} should be greater than {1}".With(larger, smaller)] =
                            () => larger.Should().BeGreaterThan(smaller));
        }
    }
}