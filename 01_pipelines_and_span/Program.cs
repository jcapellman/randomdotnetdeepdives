using System;
using System.IO;

using System.Threading.Tasks;

namespace pipelines_span
{
    class Program
    {
        private static async Task Main(string[] args)
        {
            if (!File.Exists(args[0]))
            {
                Console.WriteLine($"{args[0]} does not exist");

                return;
            }

            var result = await PEFileMagic.IsPEAsync(args[0]);

            Console.WriteLine($"{args[0]} is PE: {result}");
        }
    }
}