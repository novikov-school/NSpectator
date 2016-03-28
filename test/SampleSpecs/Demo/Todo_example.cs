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
    class Todo_example : Spec
    {
        void soon()
        {
            it["everyone will have a drink"] = todo;
            xspecify = () => true.Should().BeFalse("because it should fails");
        }
    }
}