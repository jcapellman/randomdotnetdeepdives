namespace pipelines_span
{
    class Program
    {
        private static void Main(string[] args)
        {
            //var summary = BenchmarkRunner.Run<PEFileMagic>();
            #if RELEASE
            var summary = BenchmarkRunner.Run<LargeArray>();
            #else
            var la = new LargeArray {size = 1};

            la.TraditionalTest();
            #endif
        }
    }
}