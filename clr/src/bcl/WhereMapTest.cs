#region License and Terms
// MoreLINQ - Extensions to LINQ to Objects
// Copyright (c) 2008 Jonathan Skeet. All rights reserved.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Linq;
using System.Collections;

namespace MoreLinq.Test
{
    [TestFixture]
    public class WhereMapTest
    {
        private class CountingEnumerable : IEnumerable<int>
        {
            private int currentIndex = 0;
            private int[] collection;
            public int get_current = 0;

            public CountingEnumerable(int[] collection)
            {
                this.collection = collection;
            }

            private class CountingEnumerator : IEnumerator<int>
            {
                private CountingEnumerable enumerable;

                public CountingEnumerator(CountingEnumerable enumerable)
                {
                    this.enumerable = enumerable;
                }

                public int Current
                {
                    get
                    {
                        this.enumerable.get_current++;
                        return this.enumerable.collection[this.enumerable.currentIndex];
                    }
                }

                object IEnumerator.Current
                {
                    get
                    {
                        throw new NotImplementedException();
                    }
                }

                public void Dispose()
                {
                   
                }

                public bool MoveNext()
                {
                    this.enumerable.currentIndex++;

                    if (this.enumerable.currentIndex > this.enumerable.collection.Length - 1)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }

                public void Reset()
                {
                    this.enumerable.currentIndex = 0;
                }
            }

            public IEnumerator<int> GetEnumerator()
            {
                return new CountingEnumerator(this);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                throw new NotImplementedException();
            }
        }

        [Test]
        public void WhereMap()
        {
            var collection = new CountingEnumerable(new[] { 1, 2, 3, 4, 5 });
            var linqWhere = collection.Where(x => x % 2 == 0).ToArray();
            var linqMap = linqWhere.Select(x => x.ToString());
            var linqEager = linqMap.ToArray();
            Assert.AreEqual(2, linqEager.Length);
            Assert.AreEqual("2", linqEager[0]);
            Assert.AreEqual("4", linqEager[1]);
            Console.WriteLine(collection.get_current);
        }
    }
}