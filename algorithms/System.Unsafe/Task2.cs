using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PreparingToInterview
{
    using Node = Node<int>;
    class Node<T>
    {
        public Node<T> Next = null;
        public T Data = default(T);

        public Node(T data)
        {
            Data = data;
        }

        public void appendToTail(T data)
        {
            var end = new Node<T>(data);
            var findLastNode = this;
            while (findLastNode.Next != null) { findLastNode = findLastNode.Next; }
            findLastNode.Next = end;
        }

        public static Node<T> deleteNodeByData(Node<T> head, T data)
        {
            Node<T> n = head;
            if (n.Data.Equals(data))
            {
                return n.Next;
            }

            while (n.Next != null)
            {
                if (n.Next.Data.Equals(data))
                {
                    n.Next = n.Next.Next;
                    return head;
                }
                n = n.Next;
            }
            return n;
        }

    }


    class Program2
    {


        public static void Main2()
        {
//            Node head = new Node(1);
            //head.appendToTail(2);
            //head.appendToTail(1);
            //head.appendToTail(4);
            //head.appendToTail(2);

            //Node head = new Node(1);
            //head.appendToTail(2);
            //head.appendToTail(2);

            Node head = new Node(1);
            head.appendToTail(1);
            head.appendToTail(1);

            Print(head);
            DeleteDublicates(head);
            Print(head);
        }

        private static void Print(Node head)
        {
            Node n = head;
            while (n != null)
            {
                Console.WriteLine(n.Data + " ");
                n = n.Next;
            }
            Console.WriteLine();
        }
        //2 1  Write code to remove duplicates from an unsorted linked list 
        //FOLLOW UP
        //How would you solve this problem if a temporary buffer is not allowed? 
        private static void DeleteDublicates(Node head)
        {
            Node n = head;
            while (n != null)
            {
                deleteAllNodesByData(n);
                n = n.Next;
            }
        }

        private static void deleteAllNodesByData(Node notForDeletion)
        {
            Node n = notForDeletion;
            Node d = null;
            while (n.Next != null) // deleting all dublicated nodes except last one
            {
                if (n.Next.Data.Equals(notForDeletion.Data))
                {
                    d = n;
                    n.Next = n.Next.Next;
                }
                n = n.Next;
                if (n == null) break;
            }

            if (d != null && d.Next != null && d.Next.Data.Equals(notForDeletion.Data)) 
            {
                d.Next = null; // solves were last 2 equal e.g 1 2 2
            }

        }
    }
}
