using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using i8 = System.SByte;
using i16 = System.Int16;
using i32 = System.Int32;
using i64 = System.Int64;
using u8 = System.Byte;
using u16 = System.UInt16;
using u32 = System.UInt32;
using u64 = System.UInt64;
using f32 = System.Single;
using f64 = System.Double;

namespace indexed_memory
{

    internal static class Optimization
    {
        // backport of values from 3.0 to target performance into future, compile fold old target still get improvements on new runtime
        public const short AggressiveInliningAndOptimization = 0x0100 | 0x0200;
    }

    [CoreJob()]

    public class ArrayIndexer
    {

        [Params(100_000_000)]
        public int N;


        [IterationSetup]
        public void GlobalSetup()
        {
         
        }

        [Benchmark(Baseline = true)]
        public void RawArrayIndexer()
        {
            for (int i = 0; i < N; i++)
            {
                rawArray[i] = (byte)(i % 42);
            }
        }

        [Benchmark]
        public void ArrayWrapIndexer()
        {
            for (int i = 0; i < N; i++)
            {
                arrayWrap[i] = (byte)(i % 42);
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
        }
    }
}
