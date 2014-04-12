using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dnHTM
{

    public class Level
    {

        private List<HtmNode> _Nodes = new List<HtmNode>();

        public Level(int number)
        {
            Number = number;
        }

        public int Number { get; set; }

        public List<HtmNode> Nodes
        {
            get
            {
                return _Nodes;
            }
            set
            {
                _Nodes = value;
            }
         }

        public void AddNodes(IEnumerable<HtmNode> nodes)
        {
            _Nodes.AddRange(nodes);
            foreach (var node in nodes)
            {
                node.Level = this;
            }
        }

        public void AddNodes(params HtmNode[] htmNodes)
        {
            _Nodes.AddRange(htmNodes);
            foreach (var node in htmNodes)
            {
                node.Level = this;
            }
        }


    }
}
