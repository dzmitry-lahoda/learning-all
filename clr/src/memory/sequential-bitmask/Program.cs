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
    [CoreJob]
    [MemoryDiagnoser]

    public class BitMaskAllocateAndSet
    {

        /// <summary>
        /// Reads whether the specified bit in a mask is set.
        /// Similar in behavior to the x86 instruction BT.
        /// </summary>
        /// <param name="value">The mask.</param>
        /// <param name="bitOffset">The ordinal position of the bit to read.
        /// Any value outside the range [0..63] is treated as congruent mod 63.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool ExtractBit(ulong value, int bitOffset)
            => (value & (1ul << bitOffset)) != 0;


        /// <summary>
        /// Writes the specified bit in a mask and returns the new value.
        /// Similar in behavior to the x86 instructions BTS and BTR.
        /// Executes without branching.
        /// </summary>
        /// <param name="value">The mask.</param>
        /// <param name="bitOffset">The ordinal position of the bit to write.
        /// Any value outside the range [0..63] is treated as congruent mod 64.</param>
        /// <param name="on">True to set the bit to 1, or false to set it to 0.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong WriteBit(ulong value, int bitOffset, bool on)
        {
            ulong onn = on ? 1ul << bitOffset : 0;
            return (value & ~(1ul << bitOffset)) | onn;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool WriteBit(ref ulong value, int bitOffset, bool on)
        {
            ulong mask = 1ul << bitOffset;
            ulong onn = on ? 1ul << bitOffset : 0;

            bool btw = (value & mask) != 0;
            value = (value & ~mask) | onn;

            return btw;
        }



        [Params(2_000_000)]
        public int N;
        private bool[] bits;

        [ThreadStatic]
        private bool[] perThreadBits;

        private u64 u64Bits;
        private byte[] storage;
        [ThreadStatic]
        private u64 u64BitsPerThread;

        [IterationSetup]
        public void GlobalSetup()
        {
            if (N <= 0) throw new Exception();
            bits = new bool[64];
            u64Bits = 0;
            storage = new byte[N];
        }

        [Benchmark(Baseline = true)]
        public void thread_usnafe_shared_bool_array()
        {
            for (int i = 0; i < N; i++)
            {
                for (var j = 0; j < 64; j++)
                {
                    bits[j] = true;
                }
            }
        }

        [Benchmark]
        public void bool_create_each_timearray()
        {
            for (int i = 0; i < N; i++)
            {
                var localBits = new bool[64];
                for (var j = 0; j < 64; j++)
                {
                    localBits[j] = true;
                }
            }
        }


        [Benchmark]
        public void bool_create_one_local_for_N_timearray()
        {
            var localBits = new bool[64];
            for (int i = 0; i < N; i++)
            {
                for (var j = 0; j < 64; j++)
                {
                    localBits[j] = true;
                }
            }
        }

        [Benchmark]
        public void bool_array_per_thread_with_check()
        {
            for (int i = 0; i < N; i++)
            {
                if (perThreadBits == null) perThreadBits = new bool[64];
                for (var j = 0; j < 64; j++)
                {
                    perThreadBits[j] = true;
                }
            }
        }

        [Benchmark]
        public void bool_stack_array()
        {
            for (int i = 0; i < N; i++)
            {
                StackAllocated();
            }
        }


        [Benchmark]
        public void write_from_bits_number()
        {
            u64 value = 0b1111111111111111111111111111111111111111111111111111111111111111;
            for (int i = 0; i < N; i++)
            {

                for (var j = 0; j < 64 / 8; j++)
                {
                    // Console.WriteLine((0b00001111 * 0x0202020202UL & 0x010884422010UL) % 1023);
                    // Console.WriteLine(0b11110000);
                    // Console.WriteLine((0b01001101 * 0x0202020202UL & 0x010884422010UL) % 1023);
                    // Console.WriteLine(0b10110010);   

                    //https://stackoverflow.com/questions/746171/most-efficient-algorithm-for-bit-reversal-from-msb-lsb-to-lsb-msb-in-c
                    WriteByte((u8)(((u8)value * 0x0202020202UL & 0x010884422010UL) % 1023));
                    value = value >> 8;
                }
            }
        }

        [Benchmark]
        public void write_from_bits_array()
        {
            for (int i = 0; i < N; i++)
            {
                u8 current = 0;
                var bitOffset = 0;
                for (var j = 0; j < 64; j++)
                {
                    var bit = bits[0];
                    if (bit)
                    {
                        current |= (u8)(1 << (7 - bitOffset));
                    }

                    if (++bitOffset == 8)
                    {
                        WriteByte(current);
                        current = 0;
                        bitOffset = 0;
                    }
                }
            }
        }



        private void WriteByte(byte current)
        {
            storage[0] = current;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private unsafe void StackAllocated()
        {
            bool* stackBits = stackalloc bool[66];

            for (var j = 0; j < 64; j++)
            {
                stackBits[j] = true;
            }
        }


        [Benchmark]
        public void u64BitsWriteBitRef()
        {
            for (int i = 0; i < N; i++)
            {
                for (var j = 0; j < 64; j++)
                {
                    WriteBit(ref u64Bits, j, true);
                }
            }
        }

        [Benchmark]
        public void u64OneByOne()
        {

            for (int i = 0; i < N; i++)
            {
                u64 value = 0;
                for (var j = 0; j < 63; j++)
                {
                    value |= 1;
                    value <<= 1;
                }

                value |= 1;
            }
        }

        [Benchmark]
        public void u64BitsWriteBitSet()
        {
            for (int i = 0; i < N; i++)
            {
                for (var j = 0; j < 64; j++)
                {
                    u64Bits = WriteBit(u64Bits, j, true);
                }
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
