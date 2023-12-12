using ProtoBuf;

namespace SerializeTester.SerializeHelpers
{
    /// <summary>
    /// Protobuf-net
    /// </summary>
    public class ProtoNetBufSerializeHelper:ISerializeHelper
    {
        public byte[] Serialize(Organization data)
        {
            using var stream = new MemoryStream();
            Serializer.Serialize(stream, data);
            return stream.ToArray();
        }

        public Organization? Deserialize(byte[] buffer)
        {
            using var stream = new MemoryStream(buffer);
            return Serializer.Deserialize<Organization>(stream);
        }

    }
}
