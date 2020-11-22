using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace ConsoleApplication1
{

    abstract class ExpressionVisitor
    {
        MultiMethods.MultiMethod.Action<Expression> _visit;

        public ExpressionVisitor()
        {
            _visit = MultiMethods.Dispatcher.Action<Expression>(this.Visit);
        }

        public void Visit(Expression e)
        {
            _visit.Invoke(e);
        }
    }

    abstract class Expression{ }

    class IntExp : Expression
    {
        public int value=1;
    }

    class DoubleExp : Expression
    {
        public double value = 1.0203;
    }

    class AddExp : Expression
    {
        public Expression e1 = new IntExp();
        public Expression e2 = new IntExp();
    }

    class MinusExp : Expression
    {
        public Expression e1 = new IntExp();
        public Expression e2 = new IntExp();


    }

    class Program
    {

        

        static void Main(string[] args)
        {
            var PrettyPrint = new PrettyPrint();
            PrettyPrint.Visit(new MinusExp());
        }
    }

    class PrettyPrint : ExpressionVisitor
    {



        protected void Visit(IntExp e)
        {
            Console.WriteLine(e.value + " INTEXP ONLY");
        }

        protected void Visit(AddExp e)
        {
            Visit(e.e1);
            Console.WriteLine(" + ");
            Visit(e.e2);
        }

        protected void Visit(MinusExp e)
        {
            Visit(e.e1);
            Console.WriteLine(" - ");
            Visit(e.e2);
        }

    }
}
