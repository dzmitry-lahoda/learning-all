/*
 
For example, the sequence of nodes with values 5, 10, 12, 21 is a path of length 3 in the tree from the above figure. 
The sequence of nodes with values 12, 20 is a path of length 1. 
The sequence of nodes with values 21, 12, 20 is not a valid path.
A path always goes to the left if following this path we only traverse the pointers to left subtrees.
For example, sequence of nodes with values 10, 12, 21 is a path that always goes to the left. 
Analogously we define a path that always goes to the right; sequence of nodes with values 12, 20 is an example of such a path.
For example, the tree shown in the above figure is of height 3.

Write Function given a non-empty binary tree T consisting of N nodes, 
returns the maximum length of a path that always goes to the left or always goes to the right. 
For example, given tree T shown in the figure above, the function should return 2.
The longest such path starts in the node containing value 10 and goes left. 
Note that the values contained in the nodes are not relevant in this task.
For the purpose of entering your own test cases, you can denote a tree recursively in the following way. 
An empty binary tree is denoted by None. 
A non-empty tree is denoted as (X, L, R), where X is the value contained in the root and 
L and R denote the left and right subtrees, respectively. 
The tree from the above figure can be denoted as:
 (5, 
   (3, None, None), 
   (10, 
     (12, 
       (21, None, None), 
       (20, None, None)), 
     None))
Assume that:
   N is an integer within the range [1..10,000];
   the height of tree T is an integer within the range [0..800].
worst-case complexity 
  time  is O(N);
  space  is O(N).


small_double_zigzag 
small_random 
small_comb 
medium_left 
medium_right 
medium_random 
medium_balanced 
medium_zigzag 
medium_comb 
large_left 
large_right 
large_zigzag 
large_random 
large_balanced 
large_comb 
very_large_comb 
very_large_random 
very_large_balanced 
example 
example test 
simple 
simple test 
boundary 
boundary cases 
balanced 
boundary cases 
extreme_values 
extreme values, largest answer 
balanced_10 
balanced_100 
balanced_1000 
balanced_10000 
balanced_100000 
skewed_10 
skewed_100 
skewed_1000 
path_10 
path_100 
path_500 

*/

using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using static run.TreeExtensions;

namespace run
{
    public class TreeStraightPath
    {

        // 100000
        //public class Node
        //{
        //    public int Value;
        //    public Node Next;
        //}


        //public Node Reverse(Node current)
        //{

        //    Node previous = null;
        //    while (current != null)
        //    {
        //        var next = current.Next;
        //        current.Next = previous;
        //        previous = current;
        //        current = next;
        //    }

        //    return previous;
        //}
        public void Test()
        {
            Assert.Equal(-1, DirectionLenght(null));
            Assert.Equal(0, DirectionLenght(_(0,null, null)));
            Assert.Equal(1, DirectionLenght(_(0, _(0, null, null), null)));
            Assert.Equal(1, DirectionLenght(_(0, null, _(0, null, null))));
            Assert.Equal(2, DirectionLenght(_(0, null, _(0, null, _(0, null, null)))));
            Assert.Equal(1, DirectionLenght(_(0, null, _(0, _(0, null, null), null))));
            Assert.Equal(1, DirectionLenght(_(0, _(0, null, null), _(0, null, null))));
            Assert.Equal(2, DirectionLenght(_(0,
                                        _(0, _(0, null, null), null),
                                        _(0, _(0, null, null), null))));
            var et =
                        _(5, 
                             _(8, 
                               _(12, null, null), 
                               _(2, null, null)),
                             _(9, 
                               _(7, _(1, null, null), null), 
                               _(4, _(3, null, null), null)));
            Assert.Equal(2, DirectionLenght(et));

            var et2 = _(5,
   _(3, null, null),
   _(10,
     _(12,
       _(21, null, null),
       _(20, null, null)),
     null));

            Assert.Equal(2, DirectionLenght(et2));

            var t = _(0, null, null);
            var tt = t;
            for (int i = 0; i < 1000; i++)
            {
                tt.r = _(0, null, null);
                tt = tt.r;
            }

            Assert.Equal(1000, DirectionLenght(t));
        }

        public int DirectionLenght(Tree T) => T == null ? -1  : Math.Max(curse(0, T.l, false), curse(0, T.r, true));
        

        private int curse(int length, Tree t, bool right)
        {
            if (t == null)
                return length;

            length = length + 1;

            var newVal = 0;
            var newVal2 = 0;
            if (t.l != null)
            {
                var newL = right ? 0 : length;
                newVal = curse(newL, t.l, right);
            }

            if (t.r != null)
            {
                var newL = !right ? 0 : length;
                newVal2 = curse(newL, t.r, right);
            }

            return Math.Max(length, Math.Max(newVal, newVal2));
        }
        
    }
}