#region [R# naming]
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable UnusedMember.Local
// ReSharper disable FieldCanBeMadeReadOnly.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable InconsistentNaming
#endregion
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using NSpectator;
using FluentAssertions;

namespace SampleSpecs.Bug
{
    public class Describe_failing_deserialization : Spec
    {
        MemoryStream stream;
        BinaryFormatter formatter;
        object _object;

        void When_serializing_objects()
        {
            before = () =>
            {
                stream = new MemoryStream();
                formatter = new BinaryFormatter();
            };

            act = () => formatter.Serialize(stream, _object);

            context["that are not in the search path"] = () =>
            {
                before = () => _object = new Action(() => { }).Method;

                it["should deserialize them again"] = () => // fails
                {
                    stream.Position = 0;
                    formatter.Deserialize(stream).Should().NotBeNull();
                };
            };

            context["that are in the search path"] = () =>
            {
                before = () => _object = new object();

                it["should deserialize them again"] = () =>
                {
                    stream.Position = 0;
                    formatter.Deserialize(stream).Should().NotBeNull();
                };
            };
        }
    }
}
