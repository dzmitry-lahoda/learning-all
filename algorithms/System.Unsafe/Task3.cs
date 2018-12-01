using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PreparingToInterview
{
    using System.Collections;

    class Stack<T>
    {
        public Node<T> top;

        public void push(T data)
        {
            Node<T> n = new Node<T>(data);
            n.Next = top;
            top = n;
        }

        public Node<T> pop()
        {
            if (top == null) return null;
            var n = new Node<T>(top.Data);
            top = top.Next;
            return n;
        }
    }

    class Queue<T>
    {
        public NodeP<T> back, front;

        public void queue(T data)
        {
            if (back == null && this.front == null)
            {
                back = new NodeP<T>(data);
                front = back;
            }
            else
            {
                back.Previous = new NodeP<T>(data);
                back = back.Previous;
            }
        }

        public NodeP<T> deq()
        {
            if (front == null) throw new Exception();
            var res = new NodeP<T>(this.front.Data);
            front = front.Previous;
            return res;
        }
    }

    internal class NodeP<T>
    {
        public T Data;
        public NodeP<T> Previous;
        
        public NodeP(T data)
        {
            this.Data = data;
        }
    }

    class Program3
    {
        
        //static class  ArrayOn3Stacks
        //{
        //    public List<int> list;

        //    public void push(int stack,int data)
        //    {
                
        //    }
        //    public int pop(int stack, int data)
        //    {
                
        //    }
        //}

        public static void Main3()
        {
            
        }
    }
}
