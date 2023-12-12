using Google.Protobuf;
using System.IO.Compression;
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
            var bytes = stream.ToArray();
            //var bytesZip = Compress(bytes);
            return bytes;
            //Console.WriteLine($"bytesZip:{bytesZip.Length}");
            //return bytesZip;
        }

        public protoModel.Organization? Deserialize(byte[] buffer)
        {
            Console.WriteLine($"buffer:{buffer.Length}");
            //using var stream = DecompressToStream(buffer);
            using var stream = new MemoryStream(buffer);
            return protoModel.Organization.Parser.ParseFrom(stream);
        }

        public static byte[] Compress(byte[] bytes)
        {
            if (bytes == null || bytes.Length <= 0) return bytes;

            using var compressedStream = new MemoryStream();
            using (var compressionStream = new GZipStream(compressedStream, CompressionMode.Compress))
            {
                compressionStream.Write(bytes, 0, bytes.Length);
            }
            return compressedStream.ToArray();
        }
        public static byte[] Decompress(byte[] bytes)
        {
            if (bytes == null || bytes.Length <= 0) return bytes;

            using var originalStream = new MemoryStream(bytes);
            using var decompressedStream = new MemoryStream();
            using (var decompressionStream = new GZipStream(originalStream, CompressionMode.Decompress))
            {
                decompressionStream.CopyTo(decompressedStream);
            }
            return decompressedStream.ToArray();
        }
        public static MemoryStream DecompressToStream(byte[] bytes)
        {
            if (bytes == null || bytes.Length <= 0) return default;

            using var originalStream = new MemoryStream(bytes);
            using var decompressedStream = new MemoryStream();
            using (var decompressionStream = new GZipStream(originalStream, CompressionMode.Decompress))
            {
                decompressionStream.CopyTo(decompressedStream);
            }
            return decompressedStream;
        }
    }
}
