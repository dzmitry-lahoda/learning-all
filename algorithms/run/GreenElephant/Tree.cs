/*
A binary tree is either an empty tree or a node (called the root) consisting of a single integer value and two further binary trees. 

            5                                       
       XXXX  XXXXXXXX
      8              9
   XXX XX        XXX  XXXX
  X     XX       X       X
12       2       7       4
              XXX     XXX
              X      X
              1      3


Its root contains the value 5, and the roots of its left and right subtrees have the values 8 and 9, respectively. 
The right subtrees of the nodes containing values 4 and 7, as well as the left and right subtrees of the nodes containing values 1, 2, 3 and 12, are empty trees.
A binary tree can be given using a pointer data structure with left atribute as tree, right as tree, value as integer. 
An empty tree is represented by an empty pointer (denoted by NULL, null, None, nil etc.). 
A non-empty tree is represented by a pointer to an object representing its root. 
The height of a binary tree is defined as the length of the longest possible path in the tree. 
In particular, a tree consisting of only one node has height 0 and, conventionally, an empty tree has height −1. 

A path in a binary tree is a non-empty sequence of nodes that one can traverse by following the pointers. 
The length of a path is the number of pointers it traverses. 
A path of length K is a sequence of nodes P[0], P[1], ..., P[K], such that node P[I + 1] is the root of the left or right subtree of P[I], for 0 ≤ I < K. 

*/
using System;
using System.Collections.Generic;
using System.Text;

namespace run
{    
    public static class TreeExtensions
    {
        public static BinaryTree _(int value, BinaryTree left, BinaryTree right)
        {
            return new BinaryTree { x = value, l = left, r = right };
        }
    }
    public struct minmax
    {
        int minValue, maxValue;

        public minmax(int minValue, int maxValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
        }

        public minmax max(int possibleMax) => this.maxValue < possibleMax ? new minmax(this.minValue, possibleMax) : this;
        public minmax min(int possibleMin) => this.minValue > possibleMin ? new minmax(possibleMin, this.maxValue) : this;
        public minmax minMax(int minMax) => this.min(minMax).max(minMax);
        public minmax minMax(int? minMax) => minMax.HasValue ? this.min(minMax.Value).max(minMax.Value) : this;
        public minmax minMax(int possibleMin, int possibleMax) => this.min(possibleMin).max(possibleMax);
        public minmax minMax(minmax minMax) => this.min(minMax.minValue).max(minMax.maxValue);
        public int abs() => Math.Abs(this.maxValue - this.minValue);
        public minmax greaterOf(minmax minMax) => this.abs() > minMax.abs() ? this : minMax;

        public override string ToString() => $"({this.minValue}, {this.maxValue})";
    }

}
