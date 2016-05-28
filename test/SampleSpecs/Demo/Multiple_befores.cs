#region [R# naming]

// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming

#endregion

using System.Collections.Generic;
using FluentAssertions;
using NSpectator;

namespace SampleSpecs.Demo
{
    class Multiple_befores : Spec
    {
        List<int> ints;

        void list_manipulation()
        {
            Before = () => ints = new List<int>();

            It["the ints collection should not be null"] = () => ints.Should().NotBeNull();

            Context["one item in list"] = () =>
            {
                Before = () => ints.Add(99);

                It["should have 1 item in list"] = () => ints.Should().HaveCount(1);

                It["should contain the number 99"] = () => ints.Should().Contain(99);

                Context["another item in list"] = () =>
                {
                    Before = () => ints.Add(26);

                    It["should have 2 items in list"] = () => ints.Should().HaveCount(2);

                    It["should contain the number 26"] = () => ints.Should().Contain(26);
                };
            };
        }
    }
}