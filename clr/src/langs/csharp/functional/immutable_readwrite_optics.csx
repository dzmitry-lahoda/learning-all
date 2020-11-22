using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Level000 { }
class Level001 { }
class Level002 { }
class Level00
{
    private Level000 a;

    public Level00(Level000 a, Level001 l001, Level002 l002)
    {
        this.a = a;
        L001 = l001;
        L002 = l002;
    }

    public Level000 L000 { get; }
    public Level001 L001 { get; }
    public Level002 L002 { get; }
}
class Level01 { }


class Level0
{
    private Level01 a;

    public Level0(Level00 l00, Level01 a)
    {
        L00 = l00;
        this.a = a;
    }

    public Level00 L00 { get; }
    public Level01 L01 { get; }
}

interface ILens<TWhole, TPart>
{
    TPart Get(TWhole s);
    TWhole Set(TPart a, TWhole s);
}

class LL0000 : ILens<Level0, Level00>
{
    public Level00 Get(Level0 s) => s.L00;

    public Level0 Set(Level00 a, Level0 s) => new Level0(a, null);
}

class LL000 : ILens<Level00, Level000>
{
    public Level000 Get(Level00 s) => s.L000;

    public Level00 Set(Level000 a, Level00 s) => new Level00(a, default(Level001), s.L002);
}

class LL001 : ILens<Level00, Level001>
{
    public Level001 Get(Level00 s) => s.L001;

    public Level00 Set(Level001 a, Level00 s) => new Level00(s.L000, a, s.L002);
}

class LL0001 : ILens<Level0, Level01>
{
    public Level01 Get(Level0 s) => s.L01;

    public Level0 Set(Level01 a, Level0 s) => new Level0(s.L00, a);

}
class Lenz
{
    public static ILens<A, C> ComposeLens<A, B, C>(ILens<A, B> ab, ILens<B, C> bc)
    {
        Func<A, C> sget = a => bc.Get(ab.Get(a));
        Func<C, A, A> sset = (c, a) => ab.Set(bc.Set(c, ab.Get(a)), a);
        return null;
    }
}


var l0 = new Level0(null, null);
var l00 = new Level00(null, null, null);

var val= new LL0000().Set(new LL0000().Set(l00, l0 ));


//var lenz = Lenz.ComposeLens<Level00>