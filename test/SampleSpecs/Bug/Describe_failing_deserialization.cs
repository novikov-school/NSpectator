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
using FluentAssertions;
using NSpectator;

namespace SampleSpecs.Bug
{
    public class Describe_failing_deserialization : Spec
    {
        object _object;
        BinaryFormatter formatter;
        MemoryStream stream;

        void When_serializing_objects()
        {
            Before = () =>
            {
                stream = new MemoryStream();
                formatter = new BinaryFormatter();
            };

            Act = () => formatter.Serialize(stream, _object);

            Context["that are not in the search path"] = () =>
            {
                Before = () => _object = new Action(() => { }).Method;

                It["should deserialize them again"] = () => // fails
                {
                    stream.Position = 0;
                    formatter.Deserialize(stream).Should().NotBeNull();
                };
            };

            Context["that are in the search path"] = () =>
            {
                Before = () => _object = new object();

                It["should deserialize them again"] = () =>
                {
                    stream.Position = 0;
                    formatter.Deserialize(stream).Should().NotBeNull();
                };
            };
        }
    }
}