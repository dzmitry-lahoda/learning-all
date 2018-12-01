using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using static run.GraphExtensions;
using static run.Maps;
namespace run
{
    // very abstract by usage of memory
    // data originaloty - no conversion during graph creation. only on demand
    // parralle lock free stuct based expression based span based, 
    // semi immutable (i.e. mutable only if it is safe, not immutable otherwice). 
    // do buld calculation on infinite large graphs and thne outpits reulst on demand
    // propety gives functional, not just infrmation - functional desing
    // infinite large multicar
    // if we run on fpgas
    // with policy based calcualte, opt out heuristics, precalculateions, interfaces, generic
    // works on embeeded devices(no code gen, but heuristics) with tiny graphs and on cloud with 2GB+ graphs(code gen)
    // e.g. we can check by herustic that is tree and return trified interface
    // allocate seldom in big chunks, but chunks can be streams, so can be small
    // tree is graph
    unsafe struct x
    {
        int* z;
    }

    public class GraphProgram
    {
        public unsafe void Test()
        {
            var a = GraphFactory.FromEdges(new IEdge<int>[] { _(0, 0), _(0, 1), _(0, 2), _(1, 3), _(2, 0), _(2, 1), _(2, 4),
            _(3, 2), _(3, 4), _(4, 1) });
            var selfCycle = GraphFactory.FromEdges(new IEdge<int>[] { _(0, 0) });
            var twoCycle = GraphFactory.FromEdges(new IEdge<int>[] { _(0, 2), _(2, 0) });
            var noCycle = GraphFactory.FromEdges(new IEdge<int>[] { _(0, 2), _(3, 0) });
            var noCycle2 = GraphFactory.FromEdges(new IEdge<int>[] { _(0, 2), _(2, 1) });
            var decoupledCycles = GraphFactory.FromEdges(new IEdge<int>[] { _(0, 2), _(2, 0), _(3, 1), _(1, 3), });
            var binaryChildCycle = GraphFactory.FromEdges(new IEdge<int>[] { _(0, 2), _(2, 3), _(3, 2), _(2, 4), _(4, 2) });
            var threeCycle = GraphFactory.FromEdges(new IEdge<int>[] { _(1, 3), _(3, 2), _(2, 1) });
            var unorderedThreeCycle = GraphFactory.FromEdges(new IEdge<int>[] { _(3, 2), _(1, 3), _(2, 1) });
            var repeatedWithCycle = GraphFactory.FromEdges(new IEdge<int>[] { _(1, 3), _(3, 1), _(1, 3), _(3, 2), _(2, 1) });
            var repeatedZeroCycle = GraphFactory.FromEdges(new IEdge<int>[] { _(0, 0), _(0, 0), _(0, 0) });
            var notSimpleNoRepated = GraphFactory.FromEdges(new IEdge<int>[] { _(0, 2), _(2, 1), _(1, 3), _(3, 2), _(2, 0) });
            var repeatedNoCycle = GraphFactory.FromEdges(new IEdge<int>[] { _(0, 2), _(2, 3), _(0, 2), _(2, 4) });

            int* az = NewMethod();

            var q = 0;
            //Assert.True(directCyclesOnly(decoupledCycles));
            //Assert.True(directCyclesOnly(repeatedZeroCycle));
            //Assert.True(directCyclesOnly(binaryChildCycle));            
            //Assert.False(directCyclesOnly(notSimpleNoRepated));
            //Assert.False(directCyclesOnly(threeCycle));
            //Assert.False(directCyclesOnly(unorderedThreeCycle));

            Assert.True(selfCycle.IsCycle());
            Assert.True(twoCycle.IsCycle());
            Assert.True(threeCycle.IsCycle());
            Assert.True(unorderedThreeCycle.IsCycle());
            Assert.True(repeatedWithCycle.IsCycle());
            Assert.False(noCycle.IsCycle());
            Assert.False(noCycle2.IsCycle());
            Assert.False(repeatedNoCycle.IsCycle());

            //Assert.Equal(new arc[] { _(0, 0), _(0, 0) }, simplify(repeatedZeroCycle));
            //Assert.Equal(new arc[] { _(0, 2), _(2, 0) }, simplify(notSimpleNoRepated)); 
            //Assert.Equal((0,1,3,4)}, simplify((0,1,3,2,1,3,4))); 
            //Assert.False(isCycle(decoupledCycles));

            Assert.True(threeCycle.IsSimple());
            Assert.True(unorderedThreeCycle.IsSimple());
            Assert.False(repeatedWithCycle.IsSimple());
            Assert.False(notSimpleNoRepated.IsSimple());

            Assert.True(repeatedNoCycle.IsRepeated());
            Assert.True(repeatedWithCycle.IsRepeated());
            Assert.False(threeCycle.IsRepeated());
            Assert.False(unorderedThreeCycle.IsRepeated());
            Assert.False(notSimpleNoRepated.IsRepeated());


            Assert.Equal(0, selfCycle.Length());
            //Assert.Equal(0, length(repeatedZeroCycle));
            Assert.Equal(1, twoCycle.Length());
            Assert.Equal(2, threeCycle.Length());
            var simple4 = GraphFactory.FromEdges(new IEdge<int>[] { _(1, 3), _(3, 2), _(2, 4), _(4, 1) });
            Assert.Equal(3, simple4.Length());

            var singlePath = GraphFactory.FromEdges(new IEdge<char>[] { _('a', 'b') });
            var doublePath = GraphFactory.FromEdges(new IEdge<char>[] { _('a', 'b'), _('a', 'c'), _('c', 'b') });
            var chars = GraphFactory.FromEdges(new IEdge<char>[] { _('a', 'b'), _('b', 'c'), _('c', 'd'), _('d', 'e'), _('e', 'f'), _('f', 'a') });
            var chars2 = GraphFactory.FromEdges(new IEdge<char>[] { _('a', 'b'), _('b', 'c'), _('c', 'd'), _('d', 'e'), _('e', 'f'), _('f', 'a'),
                _('a', 'd'),_('e', 'b'),_('b', 'f'),
            });

            Assert.Equal(6, chars.ArcCount());
            Assert.Equal(9, chars2.ArcCount());

            Assert.Equal(0, singlePath.Succ(_('b')).Length);
            Assert.Equal(1, chars.Succ(_('b')).Length);
            Assert.Equal(2, chars2.Succ(_('b')).Length);

            Assert.Equal(1, singlePath.Prev(_('b')).Length);
            Assert.Equal(1, chars.Prev(_('b')).Length);
            Assert.Equal(2, chars2.Prev(_('b')).Length);

            Assert.True(chars.IsCycle());
            Assert.True(chars2.IsCycle());
            Assert.False(doublePath.IsCycle());

            Assert.True(chars.IsSimple());
            Assert.False(chars2.IsSimple());


            var Kaneohe = city(1, "Kaneohe");
            var Honolulu = city(2, "Honolulu");
            var Wahiawa = city(3, "Wahiawa");
            var PearlCity = city(4, "PearlCity");
            var Maili = city(5, "Maili");
            var Kahului = city(6, "Kahului");
            var Keokea = city(7, "Keokea");
            var Lahaina = city(8, "Lahaina");
            var Laie = city(9, "Laie");
            var Kona = city(10, "Kona");
            var Kamuela = city(11, " Kamuela");
            var Hana = city(12, "Hana");
            var Hilo = city(13, "Hilo");

            var roads = new IEdge<long>[]
            {
                road(1, Kaneohe, Honolulu, 11),
road(2, Wahiawa, PearlCity, 1),
road(3, PearlCity, Honolulu, 13),
road(4, Wahiawa, Maili, 15),
road(5, Kahului, Keokea, 16),
road(6, Maili, PearlCity, 20),
road(7, Lahaina, Kahului, 22),
road(8, Laie, Kaneohe, 24),
road(9, Laie, Wahiawa, 28),
road(10, Kona, Kamuela, 31),
road(11, Kamuela, Hilo, 45),
road(12, Kahului, Hana, 60),
road(13, Kona, Hilo, 114),
};

            var hawaiRoads = GraphFactory.FromBiEdges(roads).ToAdjancedList();

            var islands = hawaiRoads.ConnectedComponents();

            //How many simple cycles are there? List them. Do not repeat paths that differ only in the starting poin
            // 5

            //Assert.Equal(1, acyclicPaths(singlePath, _('a'), _('b')).Length);
            //Assert.Equal(2, acyclicPaths(doublePath, _('a'), _('b')).Length);
            //var paths = acyclicPaths(chars2, _('a'), _('b'));
            //Assert.Equal(2, paths.Length);
        }

        private static unsafe int* NewMethod()
        {
            var z = 123;
            int* x = &z;
            return x;
        }
    }
}