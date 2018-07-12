using System;
using System.IO;
using System.IO.Pipelines;

using System.Threading.Tasks;

namespace pipelines_span
{
    public static class PEFileMagic
    {
        private static readonly byte[] MZ_HEADER = {77, 90};    // MZ

        public static async Task<bool> IsPEAsync(string fileName)
        {
            using (var fileStream = new FileStream(fileName, FileMode.Open))
            {
                var pipe = new Pipe();

                var memory = pipe.Writer.GetMemory(2);

                await fileStream.ReadAsync(memory);

                return memory.Slice(0, 2).Span.SequenceEqual(MZ_HEADER);
            }
        }
    }
}