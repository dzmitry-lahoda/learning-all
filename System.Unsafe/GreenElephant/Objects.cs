using System;
using System.Collections.Generic;
using System.Text;

namespace run
{




    public interface IVertex<out TId, in TKey, out TValue>
    {
        TId Id { get; }
        TValue this[TKey index] { get; }
    }

    public class Cell<T>
    {
        public T Curr { get; internal set; }
        public Cell<T> Next { get; internal set; }
    }

    public class AdjacencyVertex
    {
        public long Id { get; internal set; }
        public AdjacencyVertex Next { get; internal set; }
    }

    public interface IEdge<out TId, in TKey, out TValue>
    {
        TId Id { get; }
        IVertex< TId,  TKey,  TValue> Head { get; }
        IVertex<TId, TKey, TValue> Tail { get; }
        TValue this[TKey index] { get; }
    }

    public class Vertex<TId, TKey,TValue> : IVertex<TId, TKey,TValue>
    {
        public Vertex(TId id)
        {
            this.Id = id;
        }

        public TId Id { get; set; }

        public TValue this[TKey index] => throw new NotImplementedException();
    };

    public class VertexedEdge<TId, TKey, TValue> : IEdge<TId, TKey, TValue>
    {
        public VertexedEdge(TId id, IVertex<TId, TKey, TValue> head, IVertex<TId, TKey, TValue> tail)
        {
            this.Id = id;
            this.Head = head;
            this.Tail = tail;
        }

        public TValue this[TKey index] => throw new NotImplementedException();

        public IVertex<TId, TKey, TValue> Head { get; private set; }
        public IVertex<TId, TKey, TValue> Tail { get; private set; }

        public TId Id { get; set; }
    };
}
