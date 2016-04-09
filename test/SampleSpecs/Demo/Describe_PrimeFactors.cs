#region [R# naming]
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
#endregion
using NSpectator;
using SampleSpecs.Model;
using FluentAssertions;

namespace SampleSpecs.Demo
{
    class Descrribe_PrimeFactors : Spec
    {
        void Given_first_ten_integer_numbers()
        {
            new Each<int, int[]>
                {
                { 0, new int[] { } },
                { 1, new int[] { } },
                { 2, new[] { 2 } },
                { 3, new[] { 3 } },
                { 4, new[] { 2, 2 } },
                { 5, new[] { 5 } },
                { 6, new[] { 2, 3 } },
                { 7, new[] { 7 } },
                { 8, new[] { 2, 2, 2 } },
                { 9, new[] { 3, 3 } },
                }.Do((given, expected) =>
                    it[$"{given} should be {expected}"] = () =>
                        given.Primes().Should().Equal(expected)
                );
        }
    }
}