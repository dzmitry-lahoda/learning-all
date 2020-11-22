using System;
using vscde;

namespace vscode
{
    class Program
    {


        static void Main(string[] args)
        {
            var person1 = new Person1().PersonLike();
            var person2 = new Person2().PersonLike();
            // The type '(string Name, int Year)' cannot be used as type parameter 'T' in the generic type or method 'Structural.Like<T>(Person3)'. 
            //There is no boxing conversion from '(string Name, int Year)' to 'System.IEquatable<System.ValueTuple>'. 
            //var person3 = new Person3().Like<(string Name,int Year)>();
            //Console.WriteLine(person1 == person2); // fails to compile
            var t = person1.GetType();//System.ValueTuple`2[System.String,System.Int32]
            ValueTuple<int,string> a;
            ValueTuple z;
            var person3 = new Person3().Like2<string,int>();
            var person32 = new Person3().Like3<(string Name,int Year), string, int>();
            // The type '(string Name, int Year)' cannot be used as type parameter 'T' in the generic type or method 'Structural.Like3<T, T1, T2>(Person3)'.
            // There is no boxing conversion from '(string Name, int Year)' to 'System.IEquatable<(string, long)>
            //var person33 = new Person3().Like3<(string Name,int Year), string, long>();
            Console.WriteLine();
            Console.WriteLine(person1 is System.IEquatable<System.ValueTuple>);
            Console.WriteLine(person1 is System.IEquatable<System.ValueTuple<string,int>>);
            Console.WriteLine(person1.Equals(person2)); // fails to compile

            Console.WriteLine("Hello World!");
        }
    }
}
