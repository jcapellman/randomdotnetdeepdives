using BenchmarkDotNet.Running;

namespace pipelines_span
{
    class Program
    {
        private static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<PEFileMagic>();
        }
    }
}