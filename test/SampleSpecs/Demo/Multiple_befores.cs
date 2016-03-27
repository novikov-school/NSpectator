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
            before = () => ints = new List<int>();

            it["the ints collection should not be null"] = () => ints.Should().NotBeNull();

            context["one item in list"] = () =>
            {
                before = () => ints.Add(99);

                it["should have 1 item in list"] = () => ints.Count.should_be(1);

                it["should contain the number 99"] = () => ints.should_contain(99);

                context["another item in list"] = () =>
                {
                    before = () => ints.Add(26);

                    it["should have 2 items in list"] = () => ints.Count.should_be(2);

                    it["should contain the number 26"] = () => ints.should_contain(26);
                };
            };
        }
    }
}