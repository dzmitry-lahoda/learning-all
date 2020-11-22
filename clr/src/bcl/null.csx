
using System;
using static System.Console;
interface A
{
    bool? B { get; set; }
}

class AA : A
{
    public bool? B { get; set; }
}

public bool hit(object z) => z is A zz && zz != null && zz.B != false;
public bool hit2(object z) => z is A zz && zz.B != false;

var tt = new AA();
if (tt == null) WriteLine("tt == null");

WriteLine(hit(null));
WriteLine(hit2(null));

static A ret()
{
    A t = new AA();
    t = null;
    return t;
}

WriteLine(hit(ret()));
WriteLine(hit2(ret()));

var n10 = null + 10;
WriteLine(string.Format("null + 10 == null : {0}", n10 == null));
