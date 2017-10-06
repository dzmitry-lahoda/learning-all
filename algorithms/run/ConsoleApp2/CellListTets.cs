using GreenElephant.Usafe;
using GreenElephant.Usafe.bit64;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ConsoleApp2
{
    public unsafe class CellListTests
    {
        private intCellExtensions intCells;
        private charCellExtensions charCells;
        private IcharCellAllocator charCellAllocator;
        private IcharCellCellAllocator charCellCellAllocator;
        private Allocator allocator;

        public unsafe struct CharCellAllocator : IcharCellAllocator
        {
            public charCell* Apply(ulong count) => (charCell*)System.Runtime.InteropServices.Marshal.AllocHGlobal((int)((ulong)sizeof(charCell) * count));
        }

        public unsafe struct CharCellCellAllocator : IcharCellCellAllocator
        {
            public charCellCell* Apply(ulong count) => (charCellCell*)System.Runtime.InteropServices.Marshal.AllocHGlobal((int)((ulong)sizeof(charCellCell) * count));
        }

        public unsafe struct Allocator : IcharNew
        {
            public char* Apply(ulong count) => (char*)System.Runtime.InteropServices.Marshal.AllocHGlobal((int)count);
        }



        public unsafe void Test()
        {
            this.SetUp();

            var aCell = new charCell { element = 'a' };
            var bCell = new charCell { element = 'b' };
            var abCells = new charCell { element = 'a', next = &bCell };
            var otherACell = new charCell { element = 'a' };
            var otherABCells = new charCell { element = 'a', next = &bCell };
            intCell* mem1 = (intCell*)System.Runtime.InteropServices.Marshal.AllocHGlobal(sizeof(intCell) * 1);
            *mem1 = new intCell { element = 123, next = null };
            intCell* mem = (intCell*)System.Runtime.InteropServices.Marshal.AllocHGlobal(sizeof(intCell) * 5);
            intCells.toCells(new int[] { 2, 7, 1, 8, 2 }, mem);
            charCell* abc = charCellAllocator.Apply(3);

            this.Push();
            this.Pop();
            this.Concat();

            Assert.True(charCells.equals(&aCell, &aCell));
            Assert.True(charCells.equals(charCells.empty, charCells.empty));
            Assert.True(charCells.equals(&aCell, &otherACell));
            Assert.True(charCells.equals(&abCells, &otherABCells));
            Assert.False(charCells.equals(&aCell, &bCell));
            Assert.False(charCells.equals(&aCell, &abCells));

            Assert.Equal(0UL, intCells.length(intCells.empty));
            Assert.Equal(1UL, intCells.length(mem1));
            Assert.Equal(5UL, intCells.length(mem));

            charCells.toCells("abc".ToCharArray(), abc);
            Assert.Equal('a', charCells.head(&aCell));
            Assert.Equal('a', (*charCells.last(&aCell)).element);
            Assert.Equal('c', (*charCells.last(abc)).element);

            var abcArrayed = charCells.toArray(abc, allocator);
            Assert.Equal(3UL, abcArrayed.lenght);
            Assert.Equal('a', abcArrayed.index[0]);
            Assert.Equal('b', abcArrayed.index[1]);
            Assert.Equal('c', abcArrayed.index[2]);

            //charCellCell* prefixesOfEmpty = charCells.prefixes(charCells.empty, charCellCellAllocator, allocator);
            //Assert.True((*prefixesOfEmpty).element == charCells.empty);
            //charCellCell* prefixesOfA = charCells.prefixes(&aCell, charCellCellAllocator, allocator);
            //Assert.True(null != prefixesOfA);
            //Assert.True((*prefixesOfA).element == charCells.empty);
            //var nextPrefixesOfA = *((*prefixesOfA).next);
            //Assert.Equal('a', (*(nextPrefixesOfA.element)).element);
            //Assert.True((*prefixes2).next == null);
        }

        private void Concat()
        {
            charCell bCell = new charCell { element = 'b' };
            var aCell = new charCell { element = 'a', next = &bCell };

            Assert.True(this.charCells._concat_(charCells.empty, charCells.empty) == charCells.empty);

            //var abCells = this.charCells._concat(&aCell, &bCell);
            var aMutadebCells = this.charCells._concat_(&aCell, &bCell);

        }

        private void Pop()
        {
            charCell bCell = new charCell { element = 'b' };
            var aCell = new charCell { element = 'a', next = &bCell };

            var result = this.charCells.pop_(&aCell).unwrap();
            Assert.True(result == &bCell);
        }

        private void SetUp()
        {
            this.intCells = new intCellExtensions();
            this.charCells = new charCellExtensions();
            this.charCellAllocator = new CharCellAllocator();
            this.charCellCellAllocator = new CharCellCellAllocator();
            this.allocator = new Allocator();

        }

        private void Push()
        {
            var aCell = new charCell { element = 'a' };
            var bCell = new charCell { element = 'b' };
            var result = this.charCells.push(aCell, &bCell, this.charCellAllocator).unwrap();

            Assert.Equal('a', (*result).element);
            Assert.True(&bCell == (*result).next);
            Assert.True(aCell.next == this.charCells.empty);

            this.charCells.push_(&aCell, &bCell).unwrap();
            Assert.True(aCell.next == &bCell);
        }
    }
}
