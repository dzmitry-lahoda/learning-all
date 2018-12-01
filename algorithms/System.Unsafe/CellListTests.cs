using System.Collections.Unsafe;
using System.Collections.Unsafe.bit64;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Collections.Unsafe.bit64;
using static System.Collections.Unsafe.bit64.stdlib;
using static System.Collections.Unsafe.bit64.longCellExtensions;

namespace ConsoleApp2
{

    public unsafe class CellListTests
    {
        private Allocator allocator;

        public unsafe struct Allocator : IcharNew
        {
            public char* Apply(ulong count) => (char*)malloc((int)count);
        }

        public unsafe delegate void A(int* a);

        public void RunAll()
        {
            A a;
            //var aa = &a;
            this.LenghtTest();
            this.Push();
            this.Pop();
            this.Insert();
            this.Test();
            this.AllocationDeallocation();
            this.OneTwoThreePushPushLenthScenario();
            this.AddAtHeadScenario();
            this.BuildWithLocalRef();
            this.CountTest();
            this.GetNthTest();
        }

        

        [Fact]
        public void Insert()
        {
            longCell* head = NULL;
            insert(&head, 0, 13l);
            Assert.Equal(1ul, length(head));
            Assert.Equal(13l, head->element);
            Assert.True(NULL == head->next);

            insert(&head, 1, 42l);
            Assert.Equal(2ul, length(head));
            Assert.Equal(13l, head->element);
            Assert.True(NULL != head->next);
            Assert.Equal(42l, head->next->element);

            insert(&head, 1, 5l);
            Assert.Equal(3ul, length(head));
            Assert.Equal(13l, head->element);
            Assert.True(NULL != head->next);
            Assert.Equal(5l, head->next->element);
            free(&head);
        }

        [Fact]
        public void AddAtHeadScenario()
        {
            longCell* head = NULL;
            int i;
            for (i = 1; i < 6; i++)
                push(&head, i);
            // head == {5, 4, 3, 2, 1};
        }

        [Fact]
        public void OneTwoThreePushPushLenthScenario()
        {
            var head = buildOneTwoThree(); // Start with {1, 2, 3}
            push(&head, 13); // Push 13 on the front, yielding {13, 1, 2, 3}
                             // (The '&' is because head is passed
                             // as a reference pointer.)
            push(&(head->next), 42);
            // Push 42 into the second position
            // yielding {13, 42, 1, 2, 3}
            // Demonstrates a use of '&' on
            // the .next field of a node.
            // (See technique #2 below.)
            var len = length(head);
            Assert.Equal(5UL, len);
        }

        [Fact]
        public void BuildWithSpecialCase()
        {
            longCell* head = NULL;
            longCell* tail;
            int i;
            // Deal with the head node here, and set the tail pointer
            push(&head, 1);
            tail = head;
            // Do all the other nodes using 'tail'
            for (i = 2; i < 6; i++)
            {
                push(&(tail->next), i); // add node at tail->next
                tail = tail->next; // advance tail to point to last node
            }
            // head == {1, 2, 3, 4, 5};
        }

        [Fact]
        public void BuildWithLocalRef()
        {
            longCell* head = NULL;
            longCell** lastPtrRef = &head; // Start out pointing to the head pointer
            int i;
            for (i = 1; i < 6; i++)
            {
                push(lastPtrRef, i); // Add node at the last pointer in the list
                lastPtrRef = &((*lastPtrRef)->next); // Advance to point to the
                                                     // new last pointer
            }
            // head == {1, 2, 3, 4, 5};
        }


        [Fact]
        public void CountTest()
        {
            var myList = buildOneTwoThree();
            ulong result = count(myList, 2);
            Assert.Equal(1Ul, result);
        }

        [Fact]
        public void GetNthTest()
        {
            var myList = buildOneTwoThree();
            var lastNode = index(myList, 2);
            Assert.Equal(3L, lastNode);
        }

        /// <summary>
        /// Tests memory usage.
        /// </summary>
        private void AllocationDeallocation()
        {
            var newCell = charCellExtensions.newCell('a');
            Assert.True(newCell->next == charCellExtensions.NULL);
            charCellExtensions.free(&newCell);
            Assert.True(newCell == charCellExtensions.NULL);
        }

        longCell* buildOneTwoThree() => newCell(1, newCell(2, newCell(3)));

        public longCell* toCells(long length, long* memory)
        {
            longCell* head = null;
            for (long i = length - 1; i >= 0; i--)
            {
                var cell = (longCell*)malloc((ulong)sizeof(longCell));
                cell->element = memory[i];
                if (head != null)
                    cell->next = head;
                else
                    cell->next = null;
                head = cell;
            }
            return head;
        }

        [Fact]
        public void LenghtTest()
        {
            var oneTwoThree = buildOneTwoThree();
            Assert.Equal(3UL, length(oneTwoThree));

            var oneCell = (longCell*)malloc(sizeof(longCell) * 1);
            oneCell->next = null;
            fixed (long* x = new long[] { 1, 2, 3, 4, 5 })
            {
                var fiveCells = toCells(5, x);
                Assert.Equal(5UL, length(fiveCells));
            }

            Assert.Equal(0UL, length(NULL));
            Assert.Equal(1UL, length(oneCell));

        }

        [Fact]
        public unsafe void Test()
        {
            var aCell = new charCell { element = 'a' };
            var bCell = new charCell { element = 'b' };
            var abCells = new charCell { element = 'a', next = &bCell };
            var otherACell = new charCell { element = 'a' };
            var otherABCells = new charCell { element = 'a', next = &bCell };
            intCell* mem1 = (intCell*)System.Runtime.InteropServices.Marshal.AllocHGlobal(sizeof(intCell) * 1);
            *mem1 = new intCell { element = 123, next = null };
            intCell* mem = (intCell*)System.Runtime.InteropServices.Marshal.AllocHGlobal(sizeof(intCell) * 5);
            intCellExtensions.toCells(new int[] { 2, 7, 1, 8, 2 }, mem);
            charCell* abc = charCellExtensions.charCellAlloc(3);



            this.Concat();

            Assert.True(charCellExtensions.equals(&aCell, &aCell));
            Assert.True(charCellExtensions.equals(charCellExtensions.NULL, charCellExtensions.NULL));
            Assert.True(charCellExtensions.equals(&aCell, &otherACell));
            Assert.True(charCellExtensions.equals(&abCells, &otherABCells));
            Assert.False(charCellExtensions.equals(&aCell, &bCell));
            Assert.False(charCellExtensions.equals(&aCell, &abCells));



            charCellExtensions.toCells("abc".ToCharArray(), abc);
            Assert.Equal('a', charCellExtensions.head(&aCell));
            Assert.Equal('a', (*charCellExtensions.last(&aCell)).element);
            Assert.Equal('c', (*charCellExtensions.last(abc)).element);

            var abcArrayed = charCellExtensions.toArray(abc);
            Assert.Equal(3UL, abcArrayed.lenght);
            Assert.Equal('a', abcArrayed.index[0]);
            Assert.Equal('b', abcArrayed.index[1]);
            Assert.Equal('c', abcArrayed.index[2]);

            //charCellCell* prefixesOfEmpty = charCells.prefixes(charCells.NULL, charCellCellAllocator, allocator);
            //Assert.True((*prefixesOfEmpty).element == charCells.NULL);
            //charCellCell* prefixesOfA = charCells.prefixes(&aCell, charCellCellAllocator, allocator);
            //Assert.True(null != prefixesOfA);
            //Assert.True((*prefixesOfA).element == charCells.NULL);
            //var nextPrefixesOfA = *((*prefixesOfA).next);
            //Assert.Equal('a', (*(nextPrefixesOfA.element)).element);
            //Assert.True((*prefixes2).next == null);
        }

        private void Concat()
        {
            charCell bCell = new charCell { element = 'b' };
            var aCell = new charCell { element = 'a', next = &bCell };

            Assert.True(charCellExtensions._concat_(charCellExtensions.NULL, charCellExtensions.NULL) == charCellExtensions.NULL);

            //var abCells = this.charCells._concat(&aCell, &bCell);
            var aMutadebCells = charCellExtensions._concat_(&aCell, &bCell);

        }

        [Fact]
        public void Pop()
        {
            charCell bCell = new charCell { element = 'b' };
            var aCell = new charCell { element = 'a', next = &bCell };

            var result = charCellExtensions.pop_(&aCell).unwrap();
            Assert.True(result == &bCell);

            var head = buildOneTwoThree();
            {
                long a = pop(&head);
                Assert.Equal(1, a);
                long b = pop(&head);
                Assert.Equal(2, b);
                long c = pop(&head);
                Assert.Equal(3, c);
                ulong len = length(head);
                Assert.Equal(0ul, len);
            }

            free(&head);
        }


        [Fact]
        public void Push()
        {
            var aCell = new charCell { element = 'a' };
            var bCell = new charCell { element = 'b' };
            var result = charCellExtensions.push(aCell, &bCell).unwrap();

            Assert.Equal('a', (*result).element);
            Assert.True(&bCell == (*result).next);
            Assert.True(aCell.next == charCellExtensions.NULL);

            charCellExtensions.push_(&aCell, &bCell).unwrap();
            Assert.True(aCell.next == &bCell);

            var hCell = charCellExtensions.charCellAlloc(1);
            var hCellRef = hCell;
            hCell->element = 'h';
            hCell->next = charCellExtensions.NULL;
            charCellExtensions.push(&hCell, 'n');
            Assert.True(hCell->element == 'n');
            Assert.True(hCell->next == hCellRef);


            var ott = buildOneTwoThree();
            var pushed = push(new longCell { element = 0 }, ott).unwrap();

            Assert.Equal(0L, pushed->element);
            Assert.Equal(4UL, length(pushed));
        }
    }
}
