using BenchmarkDotNet.Running;

namespace SerializeTester
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("here we go!");
            //test with benchmark
            BenchmarkRunner.Run<BenchmarkTest>();
            //test once only
            //BenchmarkTest.TestOnce();
        }
    }
}
