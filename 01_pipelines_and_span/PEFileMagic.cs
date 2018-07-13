using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using BenchmarkDotNet.Attributes;

namespace pipelines_span
{
    public static class PEFileMagic
    {
        private static readonly byte[] MZ_HEADER = {77, 90};    // MZ

        [Benchmark]
        public static async Task<bool> IsPEAsync(string fileName)
        {
            using (var fileStream = new FileStream(fileName, FileMode.Open))
            {
                var arr = new byte[2];

                Memory<byte> memory = arr;
                    
                await fileStream.ReadAsync(memory);

                return memory.Slice(0, 2).Span.SequenceEqual(MZ_HEADER);
            }
        }

        [Benchmark]
        public static bool IsPE(string fileName)
        {
            using (var fileStream = new FileStream(fileName, FileMode.Open))
            {
                var reader = new BinaryReader(fileStream);

                var bytes = reader.ReadBytes(2);

                return bytes.SequenceEqual(MZ_HEADER);
            }
        }
    }
}