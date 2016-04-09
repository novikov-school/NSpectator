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
    class Describe_tags : Spec
    {
        public void tags_at_context_level()
        {
            // NOTE: you have to run the spec runner with a tag filter to see how
            // tags can be used to filter which contexts and examples are executed:
            //     SpecRunner <path-to-specs-dll> --tag mytag-one,~mytag-two

            context["when tags are specified at the context level", "mytag-one"] = () =>
            {
                it["tags all examples within that context"] = () => { 1.Should().Be(1); };

                context["when tags are nested", "mytag-two"] = () => { it["tags all the nested examples and nested contexts cumlatively"] = () => { 1.Should().Be(1); }; };
            };
        }
    }
}