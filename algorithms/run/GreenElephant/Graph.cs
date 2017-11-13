using System;
using System.Collections.Generic;
using System.Text;

namespace run
{


    public interface IUnknownGraph<T> where T : struct
    {
        bool IsCycle();
        bool IsRepeated();
        bool IsSimple();
        long Length();
        long ArcCount();
        IVertex<T>[] Succ(IVertex<T> charnode);
        IVertex<T>[] Prev(IVertex<T> charnode);
    }

    public class GraphFactory
    {
        public static Graph<T> FromEdges<T>(IEdge<T>[] edges) where T : struct
        {
            var graph = new Graph<T>();
            graph._edges = edges;
            return graph;
        }

        public static IndexedBiGraph FromBiEdges(IEdge<long>[] edges)
        {
            var graph = new IndexedBiGraph();
            graph._edges = edges;
            return graph;
        }

        public static BiAdjancedBiGraph FromBiAdjancedList(AdjacencyVertex[] edges)
        {
            var graph = new BiAdjancedBiGraph();
            graph._edges = edges;
            return graph;
        }


    }

    public class BiAdjancedBiGraph
    {
        internal AdjacencyVertex[] _edges;

        internal object ConnectedComponents()
        {
            throw new NotImplementedException();
        }
    }

    public class IndexedBiGraph: IUnknownGraph<long> 
    {
        internal IEdge<long>[] _edges;


        public IndexedBiGraph()
        {
        }

        public long ArcCount()
        {
            throw new NotImplementedException();
        }

        public bool IsCycle()
        {
            throw new NotImplementedException();
        }

        public bool IsRepeated()
        {
            throw new NotImplementedException();
        }

        public bool IsSimple()
        {
            throw new NotImplementedException();
        }

        public long Length()
        {
            return _edges.Length - 1;
        }

        public IVertex<long>[] Prev(IVertex<long> charnode)
        {
            throw new NotImplementedException();
        }

        public IVertex<long>[] Succ(IVertex<long> charnode)
        {
            throw new NotImplementedException();
        }

        public IndexedBiGraph[] ConnectedComponents()
        {
            throw new NotImplementedException();


        }

        public long NodesCount()
        {
            throw new NotImplementedException();
        }

        internal unsafe BiAdjancedBiGraph ToAdjancedList()
        {
            throw new NotImplementedException();
            //run.cell first = new cell { Curr = new adjacencyVertex { Id = -1 } };

            //for (int i = 0; i < _edges.Length; i++)
            //{
            //    ref var item = ref _edges[0];
            //    ref var cc = ref find(ref first, ref item);
            //    if (first.Curr.Id == -1)
            //    {
            //        first.Curr = new adjacencyVertex { Id = item.Head.Id };
            //    }
            //    else
            //    {

            //    }
            //}

            //return null;
        }

        //private unsafe ref cell find(ref run.cell inside,  ref IEdge<long> a)
        //{
        //    ref cell r = ref inside;
        //    while (r.Next != null)
        //    {
        //        if (r.Curr.Id == a.Head.Id)
        //        {
        //            break;
        //        }

        //        r = *r.Next;
        //    }

        //    return ref r;
        //}
    }

    public class Graph<T> : IUnknownGraph<T> where T : struct
    {
        internal IEdge<T>[] _edges;



        public bool IsCycle()
        {
            if (_edges.Length == 1)
            {
                return _edges[0].Head.Equals(_edges[0].Tail);
            }

            var prune = new bool[_edges.Length];
            var toTest = 0;
            while (toTest < _edges.Length)
            {
                var item = _edges[toTest];
                var to = item.Tail;
                for (int j = 0; j < _edges.Length; j++)
                {
                    var from = _edges[j];
                    if (to.Equals(from.Head))
                    {
                        prune[j] = true;
                    }
                }

                toTest++;
            }

            bool pruned = true;
            foreach (var item in prune)
            {
                if (item == false)
                {
                    pruned = false;
                    break;
                }
            }

            return pruned;
        }

        public long Length()
        {
            return _edges.Length - 1;
        }

        public IVertex<T>[] Prev(IVertex<T> charnode)
        {
            var succs = 0;
            foreach (var item in _edges)
            {
                if (item.Tail.Equals(charnode))
                {
                    succs++;
                }
            }

            var itemz = new IVertex<T>[succs];
            var index = 0;
            while (succs > 0)
            {
                var item = _edges[index];
                if (item.Tail.Equals(charnode))
                {
                    itemz[succs - 1] = item.Head;
                }

                index++;
                succs--;
            }

            return itemz;
        }

        public IVertex<T>[] Succ(IVertex<T> charnode)
        {
            var succs = 0;
            foreach (var item in _edges)
            {
                if (item.Head.Equals(charnode))
                {
                    succs++;
                }
            }

            var itemz = new IVertex<T>[succs];
            var index = 0;
            while (succs > 0)
            {
                var item = _edges[index];
                if (item.Head.Equals(charnode))
                {
                    itemz[succs - 1] = item.Tail;
                }

                index++;
                succs--;
            }

            return itemz;
        }

        public IEdge<T>[][] AcyclicPaths(IVertex<T> from, IVertex<T> charnode2)
        {
            for (int i = 0; i < _edges.Length; i++)
            {
                ref var h = ref _edges[i];
                if (h.Head.Equals(from))
                {
                    var refrom = h.Tail;
                    for (int j = 0; j < _edges.Length; j++)
                    {
                        ref var iter = ref _edges[j];
                        if (iter.Head.Equals(refrom))
                        {
                            refrom = iter.Tail;
                        }
                    }

                    break;
                }
            }

            return new IEdge<T>[0][];
        }

        public long ArcCount()
        {
            return _edges.Length;
        }

        private bool DirectCyclesOnly()
        {
            throw new NotImplementedException();
        }

        public IEdge<T>[] Simplify()
        {
            var count = -1;
            for (int i = 0; i < _edges.Length; i++)
            {
                for (int j = 0; j < _edges.Length; j++)
                {
                    if (j != i)
                    {
                        if (_edges[i].Head.Equals(_edges[j].Head) || _edges[i].Tail.Equals(_edges[j].Tail))
                        {
                            count++;
                        }
                    }
                }
            }

            var simplified = new IEdge<T>[_edges.Length - count];
            var index = 0;
            simplified[index] = _edges[index];
            for (int i = 1; i < _edges.Length; i++)
            {
                for (int j = 0; j < _edges.Length; j++)
                {
                    if (j != i)
                    {
                        if (_edges[i].Head.Equals(_edges[j].Head) || _edges[i].Tail.Equals(_edges[j].Tail))
                        {
                            count++;
                        }
                    }
                }
            }
            return simplified;
        }



        public bool IsSimple()
        {
            if (IsCycle())
            {
                var count = 0;
                for (int i = 0; i < _edges.Length; i++)
                {
                    for (int j = 0; j < _edges.Length; j++)
                    {
                        if (j != i)
                        {
                            if (_edges[i].Head.Equals(_edges[j].Head) || _edges[i].Tail.Equals(_edges[j].Tail))
                            {
                                count++;
                                if (count > 2) return false;
                            }
                        }
                    }
                }
            }

            return true;
        }

        public bool IsRepeated()
        {
            var count = 0;
            for (int i = 0; i < _edges.Length; i++)
            {
                for (int j = 0; j < _edges.Length; j++)
                {
                    if (j != i)
                    {
                        if (_edges[i].Equals(_edges[j]))
                        {
                            count++;
                            if (count > 1) return true;
                        }
                    }
                }
            }


            return false;
        }
    }




    public static class GraphExtensions
    {
        public static intarc _(intnode from, intnode to)
        {
            return new intarc { Head = from, Tail = to };
        }

        public static intarc _(int from, int to)
        {
            return new intarc { Head = _(from), Tail = _(to) };
        }

        public static intnode _(int value)
        {
            return new intnode { Id = value };
        }

        public static chararc _(charnode from, charnode to)
        {
            return new chararc { Head = from, Tail = to };
        }

        public static chararc _(char from, char to)
        {
            return new chararc { Head = _(from), Tail = _(to) };
        }

        public static charnode _(char value)
        {
            return new charnode { Id = value };
        }
    }
}
