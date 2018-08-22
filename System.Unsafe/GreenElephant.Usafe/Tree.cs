﻿
using System;
using System.Collections.Generic;
using System.Text;

namespace System.Collections.Unsafe
{

    public unsafe struct intBinaryTree
    {
        public int x;
        public intBinaryTree* l;
        public intBinaryTree* r;
    };

    public static unsafe class TreeExtensions
    {
        public static intBinaryTree _(int value, intBinaryTree* left, intBinaryTree* right)
        {
            return new intBinaryTree { x = value, l = left, r = right };
        }
    }

    public unsafe struct HeightTree
    {
        public int Height;
        public HeightTree* Parent;
    }

    public static unsafe class TreeUsafe
    {
        public static HeightTree* Merge(HeightTree* treem1, HeightTree* treem2)
        {
            HeightTree* h, l;
            if (treem1->Height >= treem2->Height)
            {
                h = treem1;
                l = treem2;
            }
            else
            {
                h = treem2;
                l = treem1;
            }

            l->Parent = h;
            if (l->Height == h->Height)
            {
                h->Height++;
            }

            return h;
        }

        public static HeightTree* FindRoot(int x, HeightTree*[] tree)
        {
            var t = tree[x];
            while (t->Parent != null)
            {
                t = t->Parent;
            }

            return t;
        }
    }


 

}