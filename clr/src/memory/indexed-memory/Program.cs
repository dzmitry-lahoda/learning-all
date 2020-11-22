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

        public interface ISpan<T>
        {
            T this[i32 index]
            {
                [MethodImpl(Optimization.AggressiveInliningAndOptimization)]
                get;
                [MethodImpl(Optimization.AggressiveInliningAndOptimization)]
                set;
            }

            i32 Length
            {
                [MethodImpl(Optimization.AggressiveInliningAndOptimization)]
                get;
            }
        }
        
        public struct ArraySpan : ISpan<u8>
        {
            public ArraySpan(u8[] items)
            {                
                this.items = items;
            }

            private u8[] items;

            public u8 this[i32 index]
            {
                [MethodImpl(Optimization.AggressiveInliningAndOptimization)]
                get => items[index];
                [MethodImpl(Optimization.AggressiveInliningAndOptimization)]
                set => items[index] = value;
            }

            public i32 Length
            {
                [MethodImpl(Optimization.AggressiveInliningAndOptimization)]
                get => items.Length;
            }
        }

        public struct MemorySpan : ISpan<u8>
        {
            public MemorySpan(Memory<u8> items)
            {
                this.items = items;
            }

            private Memory<u8> items;

            public u8 this[i32 index]
            {
                [MethodImpl(Optimization.AggressiveInliningAndOptimization)]
                get => items.Span[index];
                [MethodImpl(Optimization.AggressiveInliningAndOptimization)]
                set => items.Span[index] = value;
            }

            public i32 Length
            {
                [MethodImpl(Optimization.AggressiveInliningAndOptimization)]
                get => items.Length;
            }
        }

        [Params(100_000_000)]
        public int N;
        private byte[] rawArray;
        private byte[] rawArrayForSpan;
        private Memory<byte> memory;
        private ArraySpan arrayWrap;
        private MemorySpan memoryWrap;

        [IterationSetup]
        public void GlobalSetup()
        {
            if (N <= 0) throw new Exception();
            rawArray = new byte[N];
            rawArrayForSpan = new byte[N];
            memory = new Memory<byte>(new byte[N]);
            arrayWrap = new ArraySpan(new byte[N]);
            memoryWrap = new MemorySpan(new Memory<byte>(new byte[N]));
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

        [Benchmark]
        public void MemoryWrapIndexer()
        {
            for (int i = 0; i < N; i++)
            {
                memoryWrap[i] = (byte)(i % 42);
            }
        }        

        [Benchmark]
        public void ArrayViaSpanIndexer()
        {
            var span = new Span<byte>(rawArrayForSpan);
            for (int i = 0; i < N; i++)
            {
                span[i] = (byte)(i % 42);
            }
        }

        [Benchmark]
        public void ArrayWithParamsSpanIndexer()
        {
            var span = new Span<byte>(rawArrayForSpan);
            for (int i = 0; i < N; i++)
            {
                SpanAndIndex(span, i);
            }
        }

        private static void SpanAndIndex(Span<byte> span, int i)
        {
            span[i] = (byte)(i % 42);
        }

        [Benchmark]
        public void ArrayViaParamSpanIndexer()
        {
            var span = new Span<byte>(rawArrayForSpan);
            ViaSpan(span);
        }

        private void ViaSpan(Span<byte> span)
        {
            for (int i = 0; i < N; i++)
            {
                span[i] = (byte)(i % 42);
            }
        }

        [Benchmark]
        public void MemoryIndexer()
        {
            for (int i = 0; i < N; i++)
            {
                memory.Span[i] = (byte)(i % 42);
            }
        }


        [Benchmark]
        public void MemorySpanIndexer()
        {
            var span = memory.Span;
            for (int i = 0; i < N; i++)
            {
                span[i] = (byte)(i % 42);
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
