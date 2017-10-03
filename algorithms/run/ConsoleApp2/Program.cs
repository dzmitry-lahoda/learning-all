using GreenElephant.Usafe.bit64;
using System;
using Xunit;
namespace ConsoleApp2
{
    public unsafe class Program
    {
        public unsafe struct CharCellAllocator : IcharCellAllocator
        {
            public charCell* Apply(ulong count) => (charCell*)System.Runtime.InteropServices.Marshal.AllocHGlobal((int)((ulong)sizeof(charCell) * count));
        }

        public unsafe struct CharCellCellAllocator : IcharCellCellAllocator
        {
            public charCellCell* Apply(ulong count) => (charCellCell*)System.Runtime.InteropServices.Marshal.AllocHGlobal((int)((ulong)sizeof(charCellCell) * count));
        }

        public unsafe struct Allocator : IAllocator
        {
            public void** Apply(ulong count) => (void**)System.Runtime.InteropServices.Marshal.AllocHGlobal((int)count);
        }

        // single machine absraction
         static int Main(string[] args)
        {
            Test();
            return 0;
        }

        private static unsafe void Test()
        {
            var intCells = new intCellExtensions();
            var charCellAllocator = new CharCellAllocator();
            var charCellCellAllocator = new CharCellCellAllocator();
            var allocator = new Allocator();
            var charCells = new charCellExtensions();
            var aCell = new charCell { element = 'a' };


            Assert.Equal(0UL, intCells.length(intCells.empty));

            intCell* mem1 = (intCell*)System.Runtime.InteropServices.Marshal.AllocHGlobal(sizeof(intCell) * 1);
            *mem1 = new intCell { element = 123, next = null };
            Assert.Equal(1UL, intCells.length(mem1));

            intCell* mem = (intCell*)System.Runtime.InteropServices.Marshal.AllocHGlobal(sizeof(intCell) * 5);
            intCells.toCells(new int[] { 2, 7, 1, 8, 2 }, mem);
            Assert.Equal(5UL, intCells.length(mem));
 
            charCell* abc = charCellAllocator.Apply(3);

            charCells.toCells("abc".ToCharArray(), abc);

            Assert.Equal('a', charCells.head(&aCell));
            Assert.Equal('a', (*charCells.last(&aCell)).element);
            Assert.Equal('c', (*charCells.last(abc)).element);   
            var abcArrayed = charCells.toArray(abc, allocator);
            Assert.Equal(3UL, abcArrayed.lenght);
            Assert.Equal('a', abcArrayed.index[0]);
            Assert.Equal('b', abcArrayed.index[1]);
            Assert.Equal('c', abcArrayed.index[2]);

            
            charCellCell* prefixesOfEmpty = charCells.prefixes(charCells.empty, charCellCellAllocator, allocator);
            Assert.True(null == prefixesOfEmpty);
            //charCellCell* prefixesOfA = charCells.prefixes(&aCell, charCellCellAllocator, allocator);
            //Assert.True(null != prefixes2);
            //Assert.Equal('a', (*prefixes2).element.element);
            //Assert.True((*prefixes2).next == null);
        }
    }
}