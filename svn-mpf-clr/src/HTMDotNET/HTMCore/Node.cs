using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace dnHTM
{
    public class Node<TContent>
    {
        private List<Node<TContent>> _children = new List<Node<TContent>>();
        
        private TContent _content;

        private Node<TContent> _parent;

        public int Number { get; set; }

        public virtual Node<TContent> Parent
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

        public virtual List<Node<TContent>> Children
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

        public virtual TContent Content
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

        private void AddChild(Node<TContent> node)
        {
            Contract.Requires(node!=null);
            Contract.Ensures(node._parent==this);
            _children.Add(node);
            node.Parent = this;

        }

        public virtual void AddChildren(IEnumerable<Node<TContent>> nodes)
        {
            Contract.Requires(nodes!=null);
            foreach (var node in nodes)
            {
                AddChild(node);
            }
        }

        public virtual void AddChildren(params Node<TContent>[] htmNodes)
        {
            AddChildren(htmNodes as IEnumerable<Node<TContent>>);
        }
    }
}