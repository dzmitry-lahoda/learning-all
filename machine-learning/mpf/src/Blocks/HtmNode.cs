using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using Blocks;
using MPF.Blocks;

namespace dnHTM
{
    public class HtmNode
    {
        private List<HtmNode> _children = new List<HtmNode>();

        private FirstOrderMarkovChain _content;

        private HtmNode _parent;

        private Algorithm _algortihm;

        private string _name;
        
        public HtmNode(int number)
        {
            Number = number;
        }

        public double MaxDistance { get; set; }

        public int MaxNumberOfGroups { get; set; }

        public int MaxGroupSize { get; set; }
        
   public List<Input> Inputs { get; set; }
        public Output Out { get; set; }
        public Level Level { get; set; }

 
        public void AddInput(Input input)
        {
            Inputs.Add(input);
        }


        public int Number { get; set; }

        public virtual HtmNode Parent
        {
            get
            {
                return _parent;
            }
                
            set
            {
                _parent = value;
            }
        }

        public virtual List<HtmNode> Children
        {
            get
            {
                return _children;
            }
            set
            {
                _children = value;
            }
        }

        public virtual FirstOrderMarkovChain Content
        {
            get
            {
                return _content;
            }
            set
            {
                _content = value;
            }
        }

        private void AddChild(HtmNode node)
        {
            Contract.Requires(node!=null);
            Contract.Ensures(node._parent==this);
            _children.Add(node);
            node.Parent = this;

        }

        public virtual void AddChildren(IEnumerable<HtmNode> nodes)
        {
            Contract.Requires(nodes!=null);
            foreach (var node in nodes)
            {
                AddChild(node);
            }
        }

        public virtual void AddChildren(params HtmNode[] htmNodes)
        {
            AddChildren(htmNodes as IEnumerable<HtmNode>);
        }
    }
}