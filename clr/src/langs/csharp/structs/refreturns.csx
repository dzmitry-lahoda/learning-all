using System;

struct SimpleStuct
{
    public long a;
    public float b;
}

struct ComplexStuct
{
    public SimpleStuct ss;

    public double b;
}

class R
{
    protected ComplexStuct s;

    internal ref ComplexStuct S => ref s;
}

static R r = new R();

public static void Do() {   
    ref var ss = ref r.S.ss;    
    ss.a = 123;
    if (r.S.ss.a != 123)
        throw new Exception("Need double ref");

    r.S.ss.a = 42;
    if (r.S.ss.a != 42)
        throw new Exception("Need double ref");  
}


Do();