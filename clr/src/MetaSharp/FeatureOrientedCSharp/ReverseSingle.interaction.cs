using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FeatureOrientedCSharp
{
    public partial class List
    {
        partial void PushInteraction(Node n)
        {
            if (first == null) last = n; else first.prev = n;
        }

        partial void ShupInteraction(Node n)
        {
            if (last == null) first = n; else last.next = n;
        }
    }
}
