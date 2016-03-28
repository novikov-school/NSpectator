#region [R# naming]

// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming

#endregion

using NSpectator;

namespace SampleSpecs.Bug
{
    class Given_a_subcontext_that_fails : Spec
    {
        void When_totaling_failures()
        {
            //could not find a way to exercise this requirement using nspec
            //that didn't require using the broken behavior
            //which led to an impossibility of getting the spec to fail with the broken code
            //and pass with the correct code.... NUnit???
            it["should count this failure"] = () => 1.should_be(2);
        }
    }
}