///*

//            5                                       
//       XXXX  XXXXXXXX
//      8              9
//   XXX XX        XXX  XXXX
//  X     XX       X       X
//12       2       7       4
//              XXX     XXX
//              X      X
//              1      3


//For example, the sequence of nodes with values 5, 9, 7, 1 is a path of length 3 in the tree from the above figure. 
//The sequence of nodes with values 8, 2 is a path of length 1.
//The sequence of nodes with values 12, 8, 2 is not a valid path.

//For example, the tree shown in the above figure is of height 3.
//The amplitude of path P in tree T is the maximum difference between values of nodes on path P. 
//The amplitude of tree T is the maximum amplitude of paths in T.
//If tree T is empty, then it does not have any paths and its amplitude is assumed to be 0. 
//For example, the amplitude of a path consisting of nodes with values 5, 9, 4, 3 is 6, 
//the amplitude of a path consisting of nodes with values 9, 7, 1 is 8. 
//The amplitude of the tree is 8, as no other path in it has a greater amplitude.

//Given a binary tree T consisting of N nodes, returns its amplitude.

//For the purpose of entering your own test cases, you can denote a tree recursively in the following way. 
//An empty binary tree is denoted by None. 
//A non-empty tree is denoted as (X, L, R), where X is the value contained in the root and L and R denote the left and right subtrees, respectively.
//  (5, (8, (12, None, None), (2, None, None)),
//      (9, (7, (1, None, None), None), (4, (3, None, None), None)))
//The function should return 8, as explained above.

//Assume that:
//•	N is an integer within the range [0..100,000];
//•	each element of tree T is an integer within the range [−1,000,000,000..1,000,000,000];
//•	the height of tree T is an integer within the range [−1..500].
//Complexity worst-case
//time is O(N);
//space  is O(N).
// */

//using System;
//using System.Collections.Generic;
//using System.Text;
//using Xunit;
//using static run.TreeExtensions;

//namespace run
//{
//    public class TreeAmplitude
//    {


//        public void Test()
//        {
//            Assert.Equal(0, Amplitude(null));
//            Assert.Equal(1, Amplitude(_(0, _(1, null, null), null)));
//            Assert.Equal(1, Amplitude(_(0, null, _(1, null, null))));
//            Assert.Equal(2, Amplitude(_(0, null, _(1, null, _(2, null, null)))));
//            Assert.Equal(2, Amplitude(_(0, null, _(1, _(2, null, null), null))));
//            Assert.Equal(2, Amplitude(_(1, null, _(0, _(2, null, null), null))));
//            Assert.Equal(2, Amplitude(_(0, _(1, null, null), _(2, null, null))));
//            Assert.Equal(8, Amplitude(_(-7, _(1, null, null), _(-3, null, null))));
//            Assert.Equal(8, Amplitude(_(-7, _(-3, null, null), _(1, null, null))));
//            Assert.Equal(3, Amplitude(_(1,
//                                        _(1, _(4, null, null), null),
//                                        _(0, _(2, null, null), null))));
//            var et =
//                        _(5, _(8, _(12, null, null), _(2, null, null)),
//                             _(9, _(7, _(1, null, null), null), _(4, _(3, null, null), null)));
//            Assert.Equal(8, Amplitude(et));

//            var t = _(0, null, null);
//            var tt = t;
//            for (int i = 0; i < 1000; i++)
//            {
//                tt.r = _(0, null, null);
//                tt = tt.r;
//            }

//            Assert.Equal(0, Amplitude(t));
//        }
  
//        // recursion, grawing stack, getting depth and then doing stack allow upfront, replicate tree with align of parent on demand and prune as soon as searched
//        public int Amplitude(BinaryTree T)
//        {
//            if (null == T)
//                return 0;

//            //var frame = new Stack<Tuple<Tree,MinMax>>(1 + 500);
//            //var prune = new HashSet<Tree>();
//            //var root = T;
//            //frame.Push(Tuple.Create(T, minMax));
//            //while (((frame.Count > 0) || (root == T)) && T != null)
//            //{
//            //    if (T.l != null && !prune.Contains(T.l))
//            //    {
//            //        frame.Push(Tuple.Create(T.l, minMax.MinMax2(T.l.x)));
//            //        T = T.l;
//            //    }
//            //    else if (T.r != null && !prune.Contains(T.r))
//            //    {
//            //        frame.Push(Tuple.Create(T.r, minMax.MinMax2(T.r.x)));
//            //        T = T.r;
//            //    }
//            //    else if (frame.Count > 1)
//            //    {
//            //        // end case of recursion
//            //        var frameParent = frame.Pop();
//            //        prune.Add(frameParent.Item1);
//            //        var op = frame.Pop();
//            //        T = op.Item1;
//            //        minMax = op.Item2.MinMax2(frameParent.Item2);

//            //    }
//            //    else
//            //    {
//            //        T = null;
//            //    }

//            //}

//            return curse(new minmax(T.x, T.x), T).abs();
//        }

//        private minmax curse(minmax minMax, BinaryTree t) =>
//            t == null ?
//                minMax
//                : curse(minMax.minMax(t.l?.x), t.l).greaterOf(curse(minMax.minMax(t.r?.x), t.r));        
//    }
//}
