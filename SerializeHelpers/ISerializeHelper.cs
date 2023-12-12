namespace SerializeTester.SerializeHelpers
{
    public interface ISerializeHelper
    {
        byte[] Serialize(Organization data);

        Organization? Deserialize(byte[] buffer);
    }
}
