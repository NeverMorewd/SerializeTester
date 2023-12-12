using BenchmarkDotNet.Attributes;
using SerializeTester.SerializeHelpers;
using System.Diagnostics;
using protoModel = Serializetest.Proto.Model;

namespace SerializeTester
{
    [MemoryDiagnoser, RankColumn]
    public class BenchmarkTest
    {
        /// <summary>
        /// 测试数据量
        /// </summary>
        private const int DataCount = 1000000;

        private static readonly Random RandomShared = new(DateTime.Now.Millisecond);

        static BenchmarkTest()
        {
            Enumerable.Range(0, 5).Select(index => $"测试标签{index}").ToList().ForEach(x => 
            {
                ProtoTestData.Tags.Add(x.ToString());
            });
            Enumerable.Range(0, DataCount).Select(index => new protoModel.Member()
            {
                Id = index,
                Name = $"测试名字{index}",
                Description = $"测试描述{RandomShared.Next(1, int.MaxValue)}",
                Address = $"测试地址{RandomShared.Next(1, int.MaxValue)}",
                Value = RandomShared.Next(1, int.MaxValue),
                UpdateTime = ((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds()
            }).ToList().ForEach(x => ProtoTestData.Members.Add(x));
        }

        [Benchmark]
        public void CustomSerialize()
        {
            RunSerialize(new CustomSerializeHelper());
        }

        [Benchmark]
        public void BinarySerialize()
        {
            RunSerialize(new BinarySerializeHelper());
        }

        [Benchmark]
        public void ProtoBufPackSerialize()
        {
            RunSerialize(new ProtoBufSerializeHelper());
        }

        [Benchmark]
        public void MessagePackSerialize()
        {
            RunSerialize(new MessagePackSerializeHelper());
        }
        [Benchmark]
        public void RealProtoBufSerialize()
        {
            RunSerialize(new NativeProtoBufSerializeHelper());
        }

        public static void TestOnce()
        {
            var serializeHelpers = new List<ISerializeHelper>
            {
                new JsonByteSerializeHelper(),
                new CustomSerializeHelper(),
                new BinarySerializeHelper(),
                new ProtoBufSerializeHelper(),
                new MessagePackSerializeHelper(),
            };
            Console.WriteLine("cold run");
            serializeHelpers.ForEach(RunSerialize);
            var realProtoHelper = new NativeProtoBufSerializeHelper();
            RunSerialize(realProtoHelper);

            Thread.Sleep(1000);
            Console.WriteLine("hot run");
            serializeHelpers.ForEach(RunSerialize);
            RunSerialize(realProtoHelper);
        }

        /// <summary>
        /// 测试数据
        /// </summary>
        private static readonly Organization TestData = new()
        {
            Id = 1,
            Tags = Enumerable.Range(0, 5).Select(index => $"测试标签{index}").ToArray(),
            Members = Enumerable.Range(0, DataCount).Select(index => new Member()
            {
                Id = index,
                Name = $"测试名字{index}",
                Description = $"测试描述{RandomShared.Next(1, int.MaxValue)}",
                Address = $"测试地址{RandomShared.Next(1, int.MaxValue)}",
                Value = RandomShared.Next(1, int.MaxValue),
                UpdateTime = ((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds()
            }).ToList()
        };
        private static protoModel.Organization ProtoTestData = new()
        {
            Id = 1,
        };
        /// <summary>
        /// proto 测试数据
        /// </summary>
        /// <param name="helper"></param>
        private static void RunSerialize(ISerializeHelper helper)
        {
            Stopwatch sw = Stopwatch.StartNew();

            var buffer = helper.Serialize(TestData);

            sw.Stop();
            Log($"{helper.GetType().Name} Serialize {sw.ElapsedMilliseconds} ms {buffer.Length/1024D} kb");

            sw.Restart();

            var data = helper.Deserialize(buffer);

            sw.Stop();

            Log($"{helper.GetType().Name} Deserialize {sw.ElapsedMilliseconds} ms {data?.Members?.Count} 项");
        }

        private static void RunSerialize(NativeProtoBufSerializeHelper helper)
        {
            Stopwatch sw = Stopwatch.StartNew();

            var buffer = helper.Serialize(ProtoTestData);

            sw.Stop();
            Log($"{helper.GetType().Name} Serialize {sw.ElapsedMilliseconds} ms {buffer.Length / 1024D} kb");

            sw.Restart();

            var data = helper.Deserialize(buffer);

            sw.Stop();

            Log($"{helper.GetType().Name} Deserialize {sw.ElapsedMilliseconds} ms {data?.Members?.Count} 项");
        }


        private static void Log(string log)
        {
            Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss fff}: {log}");
        }
    }
}
