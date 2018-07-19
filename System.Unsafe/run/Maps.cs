using System;
using System.Collections.Generic;
using System.Text;

namespace run
{

    public struct City : IVertex<long>
    {
        public long Id { get; internal set; }
        public string Name { get; internal set; }
    }

    public struct Road : IEdge<long>
    {
        public long Id { get; internal set; }
        public IVertex<long> Head { get; internal set; }
        public IVertex<long> Tail { get; internal set; }
        public double Distance { get; internal set; }
    }

    public static class Maps
    {
        public static City city(long id,string name)
        {
            return new City { Id = id, Name = name };
        }

        public static Road road(long id, City one, City two, double distance)
        {
            return new Road { Id = id, Head = one, Tail = two, Distance = distance };
        }
    }

}
