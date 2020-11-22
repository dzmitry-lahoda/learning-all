using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FeatureOrientedCSharp
{

    public partial class List
    {
        public Node last;

        public void shup(Node n)
        {
            ShupInteraction(n);
            n.prev = last; last = n;
        }

        partial void ShupInteraction(Node n);
    }

    public partial class Node
    {
        public Node prev;

    }
}
