using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
public sealed class Unit { }

[StructLayout(LayoutKind.Explicit)]
struct MyUnion<T1,T2>
{
    [FieldOffset(0)] T1 I;
    [FieldOffset(0)] T2 F;
}

public class DiscriminatedUnion<TType, T1, T2>
{
    public interface IItem<TType, T1, T2> { }

    public interface IItem : IItem<TType, T1, T2> { }

    public struct Item1 : IItem
    {
        public T1 Value { get; set; }
    }

    public struct Item2 : IItem
    {
        public T2 Value { get; set; }
    }

    public static IItem New1(T1 item1) => new Item1 { Value = item1 };
    public static IItem New2 (T2 item2) => new Item2 { Value = item2 };
}



public sealed class Option : DiscriminatedUnion<Option, int, Unit> { }
public sealed class IntString : DiscriminatedUnion<IntString, int, string> { }

public sealed class IntString2 : DiscriminatedUnion<IntString2, int, string> { }

var o1 = Option.New1(1);
var o2 = Option.New2(new Unit());

var is1 = IntString.New1(1);
var is2 = IntString.New2("1");

var is21 = IntString2.New1(1);

//(42,5): error CS1503: Argument 1: cannot convert from 'IUnion<Union<int, Unit>, int, Unit>' to 'IUnion<Option, int, string>
//void Foo(IUnion<Option, int, string> a){}

void Foo(IntString.IItem  a)
{
    switch (a)
    {
        case IntString.Item1 x: Console.WriteLine(x.Value);break;
        case IntString.Item2 x: Console.WriteLine(x.Value); break;
    }
}

void Foo2(Option.IItem a)
{
    switch (a)
    {
        case Option.Item1 x: Console.WriteLine(x.Value); break;
        case Option.Item2 x: Console.WriteLine(x.Value); break;
    }
}
Foo(is1);
Foo2(o1);
Foo2(o2);
// error CS1503: Argument 1: cannot convert from 'Union<IntString2, int, string>.IUnion' to 'Union<IntString, int, string>.IUnion'
//Foo(is21);