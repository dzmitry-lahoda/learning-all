#r "D:\src\clr\src\langs\csharplib\bin\Debug\csharplib.dll"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using csharplib;
using static csharplib.Unit;



public class Sut
{
    public static Sut D { get => new Sut(); }

    public Sut DD { get => new Sut(); }
    public event ConsoleCancelEventHandler Event;
}



public static class Test
{

    public static void sub() => Sut.D.DD.Event += (s,e)=> { };

    // error CS0070: The event 'Sut.Event' can only appear on the left hand side of += or -= (except when used from within the type 'Sut')
    //public static Unit subC() => Sut.D?.DD.Event += (s, e) => { };

    public static void subV() => Sut.D?.DD?.Do(x => x.Event += (s, e) => { });
    public static Unit? fSub() => Sut.D?.DD?.FDo(x => x.Event += (s, e) => { });
}


