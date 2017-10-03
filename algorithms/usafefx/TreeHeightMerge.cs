using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
namespace run
{
    public class TreeHeightMerge
    {
        public unsafe void Test()
        {
            var tree1 = new HeightTree { Height = 0 };
            var tree2 = new HeightTree { Height = 0 };
            var array = new[] { &tree1, &tree2 };
            var tree3 = new HeightTree { Height = 0, Parent = &tree1 };
            var array3 = new[] { &tree1, &tree2, &tree3 };
            //var array2 = 
            var tt = &tree1;
            var r = TreeUsafe.FindRoot(0, array);
            Assert.True(tt == r);
            Assert.True(&tree1 == TreeUsafe.FindRoot(2, array3));

            var treem1 = new HeightTree { Height = 0 };
            var treem2 = new HeightTree { Height = 0 };

            var zeroThree = new HeightTree { Height = 2 };
            var oneTree = new HeightTree { Height = 1, Parent = &zeroThree };
            var twoTree = new HeightTree { Height = 0, Parent = &oneTree };
            var add = new HeightTree { Height = 0};
            
            var tree = TreeUsafe.Merge(&treem1, &treem2);
            Assert.Equal(1, tree->Height);
            Assert.True(&treem1 == tree);
            Assert.True(treem2.Parent == tree);

            var tttt = TreeUsafe.Merge(&add, &zeroThree);
            Assert.Equal(2, zeroThree.Height);
            Assert.True(&zeroThree == tttt);
            Assert.True(add.Parent == tttt);
        }


   

    }
}
