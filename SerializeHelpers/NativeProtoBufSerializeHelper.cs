using Google.Protobuf;
using protoModel = Serializetest.Proto.Model;

namespace SerializeTester.SerializeHelpers
{
    /// <summary>
    /// native protobuf
    /// </summary>
    internal class NativeProtoBufSerializeHelper
    {
        public byte[] Serialize(protoModel.Organization data)
        {
            using var stream = new MemoryStream();
            data.WriteTo(stream);
            return stream.ToArray();
        }

        public protoModel.Organization? Deserialize(byte[] buffer)
        {
            using var stream = new MemoryStream(buffer);
            return protoModel.Organization.Parser.ParseFrom(stream);
        }
    }
}
