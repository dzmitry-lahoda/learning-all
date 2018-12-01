using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Threading;

namespace Itransition.Training
{
    //TODO: List based only on LinkedList<>
    /// <summary>
    /// Represents a most recently used generic collection of keys and values.
    /// When an element is referenced or added, it becomes the first item in collection.
    /// If count of elements greater than capacity, last element is removed.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    public class MostRecentlyUsedDictionary<TKey, TValue> : IDictionary<TKey,TValue>,ICollection
    {
        private Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();

        private LinkedList<TKey> linkedList = new LinkedList<TKey>();

        private int capacity;

        /// <summary>
        /// Gets maximal capacity.
        /// </summary>
        public int Capacity
        {
            get
            {
                return capacity;
            }
        }

        /// <summary>
        /// Creates new <see cref="MostRecentlyUsedDictionary&lt;TKey,TValue>"/>.
        /// </summary>
        /// <param name="capacity">Maximal capacity of <see cref="MostRecentlyUsedDictionary&lt;TKey,TValue>"/>.</param>
        public MostRecentlyUsedDictionary(int capacity)
        {
            if (capacity <= 0)
            {
                throw new ArgumentException("Should be positive.", "capacity");
            }
            this.capacity = capacity;
        }

        /// <summary>
        /// Returns true if capacity equals count
        /// </summary>
        public bool IsFull
        {
            get
            {
                return linkedList.Count == capacity;
            }
        }

        #region Synchronization

        private object syncRoot;

        /// <summary>
        /// Gets a value indicating whether access to the 
        ///  <see cref="MostRecentlyUsedDictionary&lt;TKey,TValue>"/> is synchronized (thread safe).
        /// </summary>
        public virtual bool IsSynchronized
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Gets an object that can be used to synchronize access to the 
        /// <see cref="MostRecentlyUsedDictionary&lt;TKey,TValue>"/>.
        /// </summary>
        public virtual object SyncRoot
        {
            get
            {
                if (this.syncRoot == null)
                {
                    Interlocked.CompareExchange(ref this.syncRoot, new Object(), null);
                }
                return this.syncRoot;
            }
        }

        //TODO: Internal class SyncMostRecentlyUsedDictionary and Synchronized like in Hashtable

        #endregion Synchronization

        #region IDictionary<TKey,TValue> Members

        /// <summary>
        /// Adds an element with the provided key and value to the <see cref="MostRecentlyUsedDictionary&lt;TKey,TValue>"/>.
        /// </summary>
        /// <param name="key">The object to use as the key of the element to add.</param>
        /// <param name="value">The object to use as the value of the element to add.</param>
        /// <exception cref="System.ArgumentNullException">
        ///     key is null.</exception>
        /// <exception cref="System.ArgumentException">:
        ///     An element with the same key already exists in the <see cref="MostRecentlyUsedDictionary&lt;TKey,TValue>"/>.</exception>
        /// <exception cref="System.NotSupportedException">
        ///     The <see cref="MostRecentlyUsedDictionary&lt;TKey,TValue>"/> is read-only.</exception>
        public virtual void Add(TKey key, TValue value)
        {
            if (IsFull)
            {
                dictionary.Remove(linkedList.Last.Value);
                linkedList.RemoveLast();
            }
            dictionary.Add(key, value);
            linkedList.AddFirst(key);
        }

        /// <summary>
        /// Determines whether the <see cref="MostRecentlyUsedDictionary&lt;TKey,TValue>"/>
        /// contains an element with the specified key.
        /// </summary>
        /// <param name="key">The key to locate in the <see cref="MostRecentlyUsedDictionary&lt;TKey,TValue>"/>.</param>
        /// <returns>true if the <see cref="MostRecentlyUsedDictionary&lt;TKey,TValue>"/> contains
        ///     an element with the key; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">
        ///     key is null.</exception>
        public bool ContainsKey(TKey key)
        {
            LinkedListNode<TKey> linkedListNode = linkedList.Find(key);
            if (linkedListNode != null)
            {
                RemoveAddFirst(linkedListNode);
                return true;
            }
            return false;
        }

        private void RemoveAddFirst(LinkedListNode<TKey> linkedListNode)
        {
            linkedList.Remove(linkedListNode);
            linkedList.AddFirst(linkedListNode);
        }

        //TODO:Return sorted keys
        /// <summary>
        /// Gets an <see cref="System.Collections.Generic.ICollection&lt;TKey>"/> containing the keys of
        /// the <see cref="MostRecentlyUsedDictionary&lt;TKey,TValue>"/>.
        /// </summary>
        public ICollection<TKey> Keys
        {
            get 
            {
                return dictionary.Keys; 
            }
        }

        //TODO:Return sorted values
        /// <summary>
        /// Gets an <see cref="System.Collections.Generic.ICollection&lt;TValue>"/> containing the values in
        ///     the <see cref="MostRecentlyUsedDictionary&lt;TKey,TValue>"/>.
        /// </summary>
        public ICollection<TValue> Values
        {
            get
            {
                return dictionary.Values;
            }
        }

        /// <summary>
        /// Removes the element with the specified key from the <see cref="MostRecentlyUsedDictionary&lt;TKey,TValue>"/>.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns>true if the element is successfully removed; otherwise, false. This method
        ///     also returns false if key was not found in the original <see cref="MostRecentlyUsedDictionary&lt;TKey,TValue>"/>.</returns>
        /// <exception cref="System.ArgumentNullException">
        ///     key is null.</exception>
        /// <exception cref="System.NotSupportedException">
        ///     The <see cref="MostRecentlyUsedDictionary&lt;TKey,TValue>"/> is read-only.</exception>
        public bool Remove(TKey key)
        {
            if (dictionary.Remove(key))
            {
                linkedList.Remove(key);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key whose value to get.</param>
        /// <param name="value">When this method returns, the value associated with the specified key, if
        ///     the key is found; otherwise, the default value for the type of the value
        ///     parameter. This parameter is passed uninitialized.</param>
        /// <returns>true if the object <see cref="MostRecentlyUsedDictionary&lt;TKey,TValue>"/>
        ///     contains an element with the specified key; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">
        ///     key is null.</exception>
        public bool TryGetValue(TKey key, out TValue value)
        {
            if (dictionary.TryGetValue(key, out value))
            {
                linkedList.RemoveLast();
                linkedList.AddFirst(key);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets or sets the element with the specified key.
        /// </summary>
        /// <param name="key">The key of the element to get or set.</param>
        /// <returns>The element with the specified key.</returns>
        /// <exception cref="">System.ArgumentNullException:
        ///     key is null.</exception>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException">
        ///     The property is retrieved and key is not found.</exception>
        /// <exception cref="System.NotSupportedException">
        ///     The property is set and the <see cref="MostRecentlyUsedDictionary&lt;TKey,TValue>"/>
        ///     is read-only.</exception>
        public TValue this[TKey key]
        {
            get
            {
                LinkedListNode<TKey> linkedListNode = linkedList.Find(key);
                if (linkedListNode != null)
                {
                    RemoveAddFirst(linkedListNode);
                }
                return dictionary[key];
            }
            set
            {
                LinkedListNode<TKey> linkedListNode = linkedList.Find(key);
                if (linkedListNode != null)
                {
                    RemoveAddFirst(linkedListNode);
                    dictionary[key] = value;
                }
                else
                {
                    this.Add(key, value);
                }
             }
        }

        #endregion

        #region ICollection<KeyValuePair<TKey,TValue>> Members

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key,item.Value);
        }

        /// <summary>
        /// Removes all keys and values from the <see cref="MostRecentlyUsedDictionary&lt;TKey,TValue>"/>.
        /// </summary>
        public void Clear()
        {
            dictionary.Clear();
            linkedList.Clear();
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            LinkedListNode<TKey> linkedListNode = linkedList.Find(item.Key);
            if ((linkedListNode != null) && (EqualityComparer<TValue>.Default.Equals(dictionary[item.Key], item.Value)))
            {
                RemoveAddFirst(linkedListNode);
                return true;
            }
            return false;
        }

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
        {
            //TODO:Sorted copying
            ((ICollection<KeyValuePair<TKey, TValue>>)dictionary).CopyTo(array, index);
        }

        /// <summary>
        /// Gets the number of key/value pairs contained in the <see cref="MostRecentlyUsedDictionary&lt;TKey,TValue>"/>.
        /// </summary>
        public int Count
        {
            get 
            {
                return linkedList.Count;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="System.Collections.Generic.ICollection&lt;KeyValuePair&lt;TKey,TValue>> is read-only.
        /// </summary>
        bool ICollection<KeyValuePair<TKey,TValue>>.IsReadOnly
        {
            get 
            {
                return ((ICollection<KeyValuePair<TKey, TValue>>)dictionary).IsReadOnly;
            }
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            if (((ICollection<KeyValuePair<TKey, TValue>>)dictionary).Remove(item))
            {
                linkedList.Remove(item.Key);
                return true;
            }
            return false;
        }

        #endregion

        #region IEnumerable, IEnumerator

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="MostRecentlyUsedDictionary&lt;TKey,TValue>"/>.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return new MostRecentlyUsedDictionaryEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new MostRecentlyUsedDictionaryEnumerator(this);
        }

        private class MostRecentlyUsedDictionaryEnumerator : IEnumerator<KeyValuePair<TKey, TValue>>
        {
            public Dictionary<TKey, TValue> dictionary;
            public LinkedList<TKey> linkedList;
            private LinkedListNode<TKey> linkedListNode = null;
            private bool isbeforefirst = true;

            public MostRecentlyUsedDictionaryEnumerator(MostRecentlyUsedDictionary<TKey, TValue> mostRecentlyUsedDictionary)
            {
                this.dictionary = mostRecentlyUsedDictionary.dictionary;
                this.linkedList = mostRecentlyUsedDictionary.linkedList;
            }

            /// <summary>
            /// Gets the element in the collection at the current position of the enumerator.
            /// </summary>
            public KeyValuePair<TKey, TValue> Current
            {
                get
                {
                    try
                    {
                        return new KeyValuePair<TKey, TValue>(linkedListNode.Value, dictionary[linkedListNode.Value]);
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }

            public void Dispose()
            {
            }

            object IEnumerator.Current
            {
                get
                {
                    return this.Current;
                }
            }

            /// <summary>
            /// Advances the enumerator to the next element of the collection.
            /// </summary>
            /// <returns>true if the enumerator was successfully advanced to the next element; false
            ///     if the enumerator has passed the end of the collection.</returns>
            public bool MoveNext()
            {
                if (isbeforefirst == true)
                {
                    isbeforefirst = false;
                    linkedListNode = linkedList.First;
                    return (linkedListNode != null);
                }
                linkedListNode = linkedListNode.Next;
                return (linkedListNode!=null);
            }

            void IEnumerator.Reset()
            {
                isbeforefirst = true;
                linkedListNode = null;
            }

        }
        #endregion


        #region ICollection Members

        public void CopyTo(Array array, int index)
        {
            //TODO:Sorted copying
            ((ICollection)dictionary).CopyTo(array, index);
        }

        #endregion
    }
}
