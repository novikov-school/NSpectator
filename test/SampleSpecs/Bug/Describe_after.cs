﻿#region [R# naming]

// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming

#endregion

using NSpectator;
using FluentAssertions;

namespace SampleSpecs.Bug
{
    public class Describe_after : Spec
    {
        string sequence = "";

        void before_each()
        {
            sequence += "1";
        }

        void it_is_one()
        {
            sequence.Should().Be("1");
        }

        void it_is_still_just_one()
        {
            sequence.Should().Be("1");
        }

        void after_each()
        {
            sequence = "";
        }
    }
}