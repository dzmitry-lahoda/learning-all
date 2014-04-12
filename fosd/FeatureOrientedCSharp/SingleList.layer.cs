using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FeatureOrientedCSharp
{
    partial class List
    {
        public Node first;
        public void push(Node n)
        {
            PushInteraction(n);
            n.next = first; first = n;
        }

        partial void PushInteraction(Node n);

    }

    public partial class Node
    {
        public Node next;
    }

}
