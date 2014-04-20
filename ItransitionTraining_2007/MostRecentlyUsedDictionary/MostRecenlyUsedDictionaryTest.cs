using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;
using NUnit.Framework;

namespace Itransition.Training
{
    //TODO: Add more tests
    [TestFixture]
    public class MostRecenlyUsedDictionaryTest
    {
        private int numberOfElements;
        private int capacity;
        private int iterationCount;
        private int O1;
        private TextWriter output;

        [SetUp]
        protected void SetUp()
        {
            numberOfElements = 1000000;
            capacity = 100;
            iterationCount = 3;
            output = Console.Out;
            O1 = 10;
        }

        [Test]
        public void Count()
        {
            MostRecentlyUsedDictionary<int, int> mostRecentlyUsedDictionary = new MostRecentlyUsedDictionary<int, int>(2);
            ((ICollection<KeyValuePair<int, int>>)mostRecentlyUsedDictionary).Add(new KeyValuePair<int, int>(1, 1));
            mostRecentlyUsedDictionary.Add(2, 2);
            Assert.AreEqual(2, mostRecentlyUsedDictionary.Count);
        }

        [Test]
        public void AddMoreThanCapacity()
        {
            MostRecentlyUsedDictionary<int, int> mostRecentlyUsedDictionary = new MostRecentlyUsedDictionary<int, int>(2);
            ((ICollection<KeyValuePair<int,int>>)mostRecentlyUsedDictionary).Add(new KeyValuePair<int,int>(1,1));
            mostRecentlyUsedDictionary.Add(2, 2);
            mostRecentlyUsedDictionary.Add(3, 3);
            Assert.AreEqual(2, mostRecentlyUsedDictionary.Count);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void AddExistingKey()
        {
            MostRecentlyUsedDictionary<string, int> mostRecentlyUsedDictionary = new MostRecentlyUsedDictionary<string, int>(2);
            mostRecentlyUsedDictionary.Add("test", 1);
            mostRecentlyUsedDictionary.Add("test", 2);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddNullKey()
        {
            MostRecentlyUsedDictionary<object, object> mostRecentlyUsedDictionary = new MostRecentlyUsedDictionary<object, object>(2);
            mostRecentlyUsedDictionary.Add(null, "test");
        }

        [Test]
        public void Contains()
        {
            MostRecentlyUsedDictionary<int, int> mostRecentlyUsedDictionary = new MostRecentlyUsedDictionary<int, int>(3);
            mostRecentlyUsedDictionary.Add(1, 1);
            mostRecentlyUsedDictionary.Add(2, 2);
            mostRecentlyUsedDictionary.Add(3, 3);
            Assert.AreEqual(true, mostRecentlyUsedDictionary.ContainsKey(1));
            Assert.AreEqual(false, mostRecentlyUsedDictionary.ContainsKey(4));
        }

        [Test]
        public void ContainsChangeMostRecentlyUsedOrder()
        {
            MostRecentlyUsedDictionary<int, int> mostRecentlyUsedDictionary = new MostRecentlyUsedDictionary<int, int>(3);
            ((ICollection<KeyValuePair<int, int>>)mostRecentlyUsedDictionary).Add(new KeyValuePair<int,int>(1,1));
            mostRecentlyUsedDictionary.Add(2, 2);
            mostRecentlyUsedDictionary.Add(3, 3);
            mostRecentlyUsedDictionary.ContainsKey(1);
            mostRecentlyUsedDictionary.Add(4, 4);
            Assert.AreEqual(false, mostRecentlyUsedDictionary.ContainsKey(2));
        }

        [Test]
        public void Remove()
        {
            MostRecentlyUsedDictionary<int, int> mostRecentlyUsedDictionary = new MostRecentlyUsedDictionary<int, int>(3);
            mostRecentlyUsedDictionary.Add(1, 1);
            mostRecentlyUsedDictionary.Add(2, 2);
            mostRecentlyUsedDictionary.Add(3, 3);
            mostRecentlyUsedDictionary.Add(4, 4);
            Assert.AreEqual(true, mostRecentlyUsedDictionary.Remove(4));
            Assert.AreEqual(false, mostRecentlyUsedDictionary.Remove(1));
        }

        [Test]
        public void RemoveChangeMostRecentlyUsedOrder()
        {
            MostRecentlyUsedDictionary<int, int> mostRecentlyUsedDictionary = new MostRecentlyUsedDictionary<int, int>(3);
            mostRecentlyUsedDictionary.Add(1, 1);
            mostRecentlyUsedDictionary.Add(2, 2);
            mostRecentlyUsedDictionary.Add(3, 3);
            mostRecentlyUsedDictionary.Add(4, 4);
            mostRecentlyUsedDictionary.Remove(4);
            ((ICollection<KeyValuePair<int, int>>)mostRecentlyUsedDictionary).Remove(new KeyValuePair<int, int>(3, 3));
            mostRecentlyUsedDictionary.Remove(1);
            Assert.AreEqual(1, mostRecentlyUsedDictionary.Count);
        }

        [Test]
        public void Clear()
        {
            MostRecentlyUsedDictionary<int, int> mostRecentlyUsedDictionary = new MostRecentlyUsedDictionary<int, int>(2);
            mostRecentlyUsedDictionary.Add(1, 1);
            ((ICollection<KeyValuePair<int, int>>)mostRecentlyUsedDictionary).Add(new KeyValuePair<int,int>(2,2));
            mostRecentlyUsedDictionary.Clear();
            Assert.AreEqual(0, mostRecentlyUsedDictionary.Count);
        }
       
        [Test]
        public void CopyTo()
        {
            MostRecentlyUsedDictionary<int, int> mostRecentlyUsedDictionary = new MostRecentlyUsedDictionary<int, int>(2);
            mostRecentlyUsedDictionary.Add(1, 1);
            ((ICollection<KeyValuePair<int, int>>)mostRecentlyUsedDictionary).Add(new KeyValuePair<int,int>(2,2));
            KeyValuePair<int, int>[] array1=new KeyValuePair<int,int>[2];
            ((ICollection<KeyValuePair<int, int>>)mostRecentlyUsedDictionary).CopyTo(array1, 0);
            KeyValuePair<int, int>[] array2 = new KeyValuePair<int, int>[2];
            ((ICollection<KeyValuePair<int, int>>)mostRecentlyUsedDictionary).CopyTo(array2, 0);
            Assert.AreEqual(array1, array2);
            Assert.AreEqual(array1.Length, mostRecentlyUsedDictionary.Count);
        }

        [Test]
        public void MostRecentlyUsedDictionaryEnumerator()
        {
            MostRecentlyUsedDictionary<int, int> mostRecentlyUsedDictionary = new MostRecentlyUsedDictionary<int, int>(2);
            mostRecentlyUsedDictionary.Add(1, 1);
            mostRecentlyUsedDictionary.Add(2, 2);
            mostRecentlyUsedDictionary.ContainsKey(1);
            StringBuilder stringBuilder = new StringBuilder();
            foreach (KeyValuePair<int, int> var in mostRecentlyUsedDictionary)
            {
                stringBuilder.Append(var.Key.ToString() + var.Value.ToString());
            }
            Assert.AreEqual("1122",stringBuilder.ToString());
        }

        [Test]
        public void AddingTime()
        {
            long[] times = new long[3];
            for (int i = 0; i < iterationCount; i++)
            {
                times[0] = GetAddingTimeToDictionary();
                times[1] = GetAddingTimeToMostRecentlyUsedDictionary(numberOfElements);
                times[2] = GetAddingTimeToMostRecentlyUsedDictionary(capacity);
            }
            for (int i = 0; i < times.Length; i++)
            {
                times[i] = times[i] / iterationCount;
            }

            output.WriteLine(
                String.Format("Adding {0:N} elements to dictionary takes {1:N} ticks",
                numberOfElements, times[0]));

            output.WriteLine(
                String.Format(
                "Adding {0:N} elements to most recently used dictionary with {1:N} capacity takes {2:N} ticks",
                numberOfElements, numberOfElements, times[1]));
            output.WriteLine(
                String.Format(
                "Adding {0:N} elements to most recently used dictionary with {1:N} capacity takes {2:N} ticks",
                numberOfElements, capacity, times[2]));
            Assert.Greater(times[0] * O1, times[1]);
            Assert.Greater(times[0] * O1, times[2]);
        }

        private long GetAddingTimeToDictionary()
        {
            long ticks = DateTime.Now.Ticks;
            Dictionary<int, int> dictionary = new Dictionary<int, int>();
            for (int i = 0; i < numberOfElements; i++)
            {
                dictionary.Add(i, i);
            }
            return DateTime.Now.Ticks - ticks;
        }

        private long GetAddingTimeToMostRecentlyUsedDictionary(int capacity)
        {
            long ticks = DateTime.Now.Ticks;
            MostRecentlyUsedDictionary<int, int> mostRecentlyUsedDictionary = new MostRecentlyUsedDictionary<int, int>(capacity);
            for (int i = 0; i < numberOfElements; i++)
            {
                mostRecentlyUsedDictionary.Add(i, i);
            }
            return DateTime.Now.Ticks - ticks;
        }

    }
}
