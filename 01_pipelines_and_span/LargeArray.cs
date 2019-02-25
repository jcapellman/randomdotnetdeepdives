using System;

using BenchmarkDotNet.Attributes;

namespace pipelines_span
{
    [CoreJob]
    public class LargeArray
    {
        [Params(1, 10, 100, 1000)] public int size;

        private int[] GenerateArray(int sizeOfArray)
        {
            var arr = new int[sizeOfArray];

            for (var x = 0; x < sizeOfArray; x++)
            {
                arr[x] = x;
            }

            return arr;
        }

        private int randomIndex => new Random(1).Next(0, size - 1);

        [Benchmark]
        public void TraditionalTest()
        {
            var arr = GenerateArray(size);

            var result = arr[randomIndex];
        }

        [Benchmark]
        public void SliceTest()
        {
            var arr = GenerateArray(size);

            var result = arr.AsSpan(randomIndex, 1);
        }
    }
}