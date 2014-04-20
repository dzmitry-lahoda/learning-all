using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PreparingToInterview
{
    using N = BGNode<int>;
    class BGNode<T>
    {
        public BGNode<T> ChildLeft, ChildRight, Parent;

        public T Value;

        public BGNode(T value)
        {
            this.Value = value;
        }
    }

    //Implement an algorithm to determine if a string has all unique characters  What if you
    //can not use additional data structures?
    class Program4
    {
        public static void Main4()
        {
            //N f = new N("F");
            //N b = new N("B");
            //f.ChildLeft = b;
            //N a = new N("A");
            //b.ChildLeft = a;
            //N d = new N("D");
            //b.ChildRight = d;
            //N c = new N("C");
            //N e = new N("E");
            //d.ChildLeft = c;
            //d.ChildRight = e;
            //N g = new N("G");
            //f.ChildRight = g;
            //N i = new N("I");
            //f.ChildRight = i;
            //N h = new N("H");
            //i.ChildLeft = h;

            N f = new N(8);
            N b = new N(3);
            f.ChildLeft = b;
            N a = new N(1);
            b.ChildLeft = a;
            N d = new N(6);
            b.ChildRight = d;
            N c = new N(4);
            N e = new N(7);
            //d.ChildLeft = c;
            d.ChildRight = e;
            N g = new N(10);
            f.ChildRight = g;
            N i = new N(14);
            f.ChildRight = i;
            N h = new N(13);
            i.ChildLeft = h;

            insertIntoSearchBinaryTree(f, c);
            inorder(f);

        }

        private static void insertIntoSearchBinaryTree(BGNode<int> root, BGNode<int> toInsert)
        {
            if (root.Value > toInsert.Value) 
            {
                if (root.ChildLeft == null)
                {
                    root.ChildLeft = toInsert;
                }
                else { insertIntoSearchBinaryTree(root.ChildLeft, toInsert); }
            }
            if (root.Value < toInsert.Value) 
            {
                if (root.ChildRight == null)
                {
                    root.ChildRight = toInsert;
                }
                else { insertIntoSearchBinaryTree(root.ChildRight, toInsert); }
            }
        }

        private static void pre(N root)
        {
            Console.WriteLine(root.Value);
            if (root.ChildLeft != null) pre(root.ChildLeft);
            if (root.ChildRight != null) pre(root.ChildRight);
        }

        private static void inorder(N root)
        {
            if (root.ChildLeft != null) inorder(root.ChildLeft);
            Console.WriteLine(root.Value);
            if (root.ChildRight != null) inorder(root.ChildRight);
        }

        private static void post(N root)
        {
            if (root.ChildLeft != null) post(root.ChildLeft);
            if (root.ChildRight != null) post(root.ChildRight);
            Console.WriteLine(root.Value);
        }
    }
}
