using System.Text.Json;

namespace SerializeTester.SerializeHelpers
{
    public class JsonByteSerializeHelper : ISerializeHelper
    {
        public JsonByteSerializeHelper()
        {
            //JsonSerializerOptions.Default.TypeInfoResolver = 
        }
        public byte[] Serialize(Organization data)
        {
            return JsonSerializer.SerializeToUtf8Bytes(data);
        }

        public Organization? Deserialize(byte[] buffer)
        {
            var data = JsonSerializer.Deserialize<Organization>(buffer);
            return data;
        }
    }
}
