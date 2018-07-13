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

            DateTime start = DateTime.Now;

            for (var x = 0; x < 1000; x++) {
                var result = await PEFileMagic.IsPEAsync(args[0]);
            }
            
            start = DateTime.Now;

            for (var x = 0; x < 1000; x++)
            {
                var result = PEFileMagic.IsPE(args[0]);
            }

            Console.WriteLine(DateTime.Now.Subtract(start).TotalMilliseconds);

            start = DateTime.Now;

            for (var x = 0; x < 1000; x++)
            {
                var result = await PEFileMagic.IsPEAsync(args[0]);
            }

            Console.WriteLine(DateTime.Now.Subtract(start).TotalMilliseconds);

            Console.ReadKey();
        }
    }
}