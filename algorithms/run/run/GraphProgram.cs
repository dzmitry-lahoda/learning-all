using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using static run.GraphExtensions;
namespace run
{
    // parralle lock free stuct based expression based span based, 
    // semi immutable (i.e. mutable only if it is safe, not immutable otherwice)
    // propety gives functional, not just infrmation - functional desing
    // with policy based calcualte, opt out heuristics, precalculateions, interfaces, generic
    // works on embeeded devices(no code gen, but heuristics) with tiny graphs and on cloud with 2GB+ graphs(code gen)
    // e.g. we can check by herustic that is tree and return trified interface
    // allocate seldom in big chunks, but chunks can be streams, so can be small
    // 
    public class GraphProgram
    {
        public void Test()
        {
            var a = new intarc[] { _(0, 0), _(0, 1), _(0, 2), _(1, 3), _(2, 0), _(2, 1), _(2, 4),
            _(3, 2), _(3, 4), _(4, 1) };
            var selfCycle = new IArc<int>[] { _(0, 0) };
            var twoCycle = new IArc<int>[] { _(0, 2), _(2, 0) };
            var noCycle = new IArc<int>[] { _(0, 2), _(3, 0) };
            var noCycle2 = new IArc<int>[] { _(0, 2), _(2, 1) };
            var decoupledCycles = new IArc<int>[] { _(0, 2), _(2, 0), _(3, 1), _(1, 3), };
            var binaryChildCycle = new IArc<int>[] { _(0, 2), _(2, 3), _(3, 2), _(2, 4), _(4, 2) };
            var threeCycle = new IArc<int>[] { _(1, 3), _(3, 2), _(2, 1) };
            var unorderedThreeCycle = new IArc<int>[] { _(3, 2), _(1, 3), _(2, 1) };
            var repeatedWithCycle = new IArc<int>[] { _(1, 3), _(3, 1), _(1, 3), _(3, 2), _(2, 1) };
            var repeatedZeroCycle = new IArc<int>[] { _(0, 0), _(0, 0), _(0, 0) };
            var notSimpleNoRepated = new IArc<int>[] { _(0, 2), _(2, 1), _(1, 3), _(3, 2), _(2, 0) };
            var repeatedNoCycle = new IArc<int>[] { _(0, 2), _(2, 3), _(0, 2), _(2, 4) };

            //Assert.True(directCyclesOnly(decoupledCycles));
            //Assert.True(directCyclesOnly(repeatedZeroCycle));
            //Assert.True(directCyclesOnly(binaryChildCycle));            
            //Assert.False(directCyclesOnly(notSimpleNoRepated));
            //Assert.False(directCyclesOnly(threeCycle));
            //Assert.False(directCyclesOnly(unorderedThreeCycle));

            Assert.True(isCycle(selfCycle));
            Assert.True(isCycle(twoCycle));
            Assert.True(isCycle(threeCycle));
            Assert.True(isCycle(unorderedThreeCycle));
            Assert.True(isCycle(repeatedWithCycle));
            Assert.False(isCycle(noCycle));
            Assert.False(isCycle(noCycle2));
            Assert.False(isCycle(repeatedNoCycle));

            //Assert.Equal(new arc[] { _(0, 0), _(0, 0) }, simplify(repeatedZeroCycle));
            //Assert.Equal(new arc[] { _(0, 2), _(2, 0) }, simplify(notSimpleNoRepated)); 
            //Assert.Equal((0,1,3,4)}, simplify((0,1,3,2,1,3,4))); 
            //Assert.False(isCycle(decoupledCycles));

            Assert.True(isSimple(threeCycle));
            Assert.True(isSimple(unorderedThreeCycle));
            Assert.False(isSimple(repeatedWithCycle));
            Assert.False(isSimple(notSimpleNoRepated));

            Assert.True(isRepeated(repeatedNoCycle));
            Assert.True(isRepeated(repeatedWithCycle));
            Assert.False(isRepeated(threeCycle));
            Assert.False(isRepeated(unorderedThreeCycle));
            Assert.False(isRepeated(notSimpleNoRepated));


            Assert.Equal(0, length(selfCycle));
            //Assert.Equal(0, length(repeatedZeroCycle));
            Assert.Equal(1, length(twoCycle));
            Assert.Equal(2, length(threeCycle));
            Assert.Equal(3, length(new IArc<int>[] { _(1, 3), _(3, 2), _(2, 4), _(4, 1) }));

            var singlePath = new IArc<char>[] { _('a', 'b') };
            var doublePath = new IArc<char>[] { _('a', 'b'), _('a','c'),_('c','b') };
            var chars = new IArc<char>[] { _('a', 'b'), _('b', 'c'), _('c', 'd'), _('d', 'e'), _('e', 'f'), _('f', 'a') };
            var chars2 = new IArc<char>[] { _('a', 'b'), _('b', 'c'), _('c', 'd'), _('d', 'e'), _('e', 'f'), _('f', 'a'),
                _('a', 'd'),_('e', 'b'),_('f', 'a'),
            };

            Assert.Equal(6, arcCount(chars));
            Assert.Equal(9, arcCount(chars2));

            Assert.Equal(1, acyclicPaths(singlePath, _('a'), _('b')).Length);
            Assert.Equal(2, acyclicPaths(doublePath, _('a'), _('b')).Length);
            var paths = acyclicPaths(chars2, _('a'), _('b'));
            Assert.Equal(2, paths.Length);
        }

        private IArc<T>[][] acyclicPaths<T>(IArc<T>[] graph, INode<T> from, INode<T> charnode2) where T : struct
        {
            for (int i = 0; i < graph.Length; i++)
            {
                ref var h = ref graph[i];
                if (h.From.Equals(from))
                {
                    var refrom = h.To;
                    for (int j = 0; j < graph.Length; j++)
                    {
                        ref var iter = ref graph[j];
                        if (iter.From.Equals(refrom))
                        {
                            refrom = iter.To;
                        }
                    }

                    break;
                }
            }

            return new IArc<T>[0][];
        }

        public long arcCount(IArc<char>[] chars)
        {
            return chars.Length;
        }

        private bool directCyclesOnly<T>(IArc<T>[] graph) where T : struct
        {
            throw new NotImplementedException();
        }

        private IArc<T>[] simplify<T>(IArc<T>[] graph) where T : struct
        {
            var count = -1;
            for (int i = 0; i < graph.Length; i++)
            {
                for (int j = 0; j < graph.Length; j++)
                {
                    if (j != i)
                    {
                        if (graph[i].From.Equals(graph[j].From) || graph[i].To.Equals(graph[j].To))
                        {
                            count++;
                        }
                    }
                }
            }

            var simplified = new IArc<T>[graph.Length - count];
            var index = 0;
            simplified[index] = graph[index];
            for (int i = 1; i < graph.Length; i++)
            {
                for (int j = 0; j < graph.Length; j++)
                {
                    if (j != i)
                    {
                        if (graph[i].From.Equals(graph[j].From) || graph[i].To.Equals(graph[j].To))
                        {
                            count++;
                        }
                    }
                }
            }
            return simplified;
        }

        private bool isSimple<T>(IArc<T>[] graph) where T : struct
        {
            if (isCycle(graph))
            {
                var count = 0;
                for (int i = 0; i < graph.Length; i++)
                {
                    for (int j = 0; j < graph.Length; j++)
                    {
                        if (j != i)
                        {
                            if (graph[i].From.Equals(graph[j].From) || graph[i].To.Equals(graph[j].To))
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

        private bool isRepeated<T>(IArc<T>[] graph) where T : struct
        {
            var count = 0;
            for (int i = 0; i < graph.Length; i++)
            {
                for (int j = 0; j < graph.Length; j++)
                {
                    if (j != i)
                    {
                        if (graph[i].Equals(graph[j]))
                        {
                            count++;
                            if (count > 1) return true;
                        }
                    }
                }
            }


            return false;
        }

        private bool isCycle<T>(IArc<T>[] selfCycle) where T : struct
        {
            if (selfCycle.Length == 1)
            {
                return selfCycle[0].From.Equals(selfCycle[0].To);
            }

            var prune = new bool[selfCycle.Length];
            var toTest = 0;
            while (toTest < selfCycle.Length)
            {
                var item = selfCycle[toTest];
                var to = item.To;
                for (int j = 0; j < selfCycle.Length; j++)
                {
                    var from = selfCycle[j];
                    if (to.Equals(from.From))
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

        private int length<T>(IArc<T>[] path) where T : struct
        {
            return path.Length - 1;
        }
    }
}