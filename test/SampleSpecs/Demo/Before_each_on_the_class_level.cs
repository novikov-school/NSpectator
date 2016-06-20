#region [R# naming]

// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming

#endregion

using System.Collections.Generic;
using NSpectator;
using Slant.Expectations;

namespace SampleSpecs.Demo
{
    class Before_each_on_the_class_level : Spec
    {
        List<int> ints = null;

        void before_each()
        {
            ints = new List<int>();
        }

        void it_should_run_before_on_class_level()
        {
            Before = () => ints.Add(12);

            Specify = () => ints.Count.Expected().ToBe(1);
        }
    }
}