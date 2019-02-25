using System;
using System.IO;
using System.Linq;

using BenchmarkDotNet.Attributes;

namespace pipelines_span
{
    [CoreJob]
    public class PEFileMagic
    {
        private static readonly byte[] MZ_HEADER = {77, 90};    // MZ
        
        [Params("test1.exe", "test2.exe", "test3.exe")]
        public string fileName;
        
        [Benchmark]
        public bool IsPESpan()
        {
            using (var fileStream = new FileStream(Path.Combine(@"..\..\..\..\..\..\..\Samples", fileName), FileMode.Open))
            {
                Span<byte> memory = new byte[2];
                    
                fileStream.Read(memory);

                return memory.Slice(0, 2).SequenceEqual(MZ_HEADER);
            }
        }

        [Benchmark]
        public bool IsPEMemorySpan()
        {
            using (var fileStream = new FileStream(Path.Combine(@"..\..\..\..\..\..\..\Samples", fileName), FileMode.Open))
            {
                Memory<byte> memory = new byte[2];
                
                fileStream.Read(memory.Span);

                return memory.Slice(0, 2).Span.SequenceEqual(MZ_HEADER);
            }
        }

        [Benchmark]
        public bool IsPEBinaryReader()
        {
            using (var fileStream = new FileStream(Path.Combine(@"..\..\..\..\..\..\..\Samples", fileName), FileMode.Open))
            {
                var reader = new BinaryReader(fileStream);

                var bytes = reader.ReadBytes(2);

                return bytes.SequenceEqual(MZ_HEADER);
            }
        }
    }
}