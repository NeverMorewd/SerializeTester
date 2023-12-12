﻿using MessagePack;

namespace SerializeTester.SerializeHelpers
{
    public class MessagePackSerializeHelper : ISerializeHelper
    {
        // 这种方式需要在类和字段上添加特性，稍显麻烦，但添加压缩选项后，组包体积、组包和解包速度更快
        //readonly MessagePackSerializerOptions _options = MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4BlockArray);

        // 这种方式不需要给传输对象添加特性，也可添加压缩选项
        readonly MessagePackSerializerOptions _options =
            MessagePack.Resolvers.ContractlessStandardResolver.Options.WithCompression(MessagePackCompression
                .Lz4BlockArray);

        public byte[] Serialize(Organization data)
        {
            return MessagePackSerializer.Serialize(data, _options);
        }

        public Organization? Deserialize(byte[] buffer)
        {
            return MessagePackSerializer.Deserialize<Organization>(buffer, _options);
        }
    }
}
