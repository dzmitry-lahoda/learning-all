using System;
using System.Linq;

public class Monad<T>
{
    private T instance;
    public Monad(T instance) => this.instance = instance;
    public Monad<TTo> Bind<TTo>(Func<T, Monad<TTo>> monad) => monad(this.instance);
    public Monad<TTo> Bind<TTo>(Func<T, TTo> monad) => new Monad<TTo>(monad(this.instance));
}

public class MayBe<T>
{
    private T instance;
    public static MayBe<T> None = new MayBe<T>();
    public MayBe(T instance) => this.instance = instance;
    private MayBe() { }
    public MayBe<TTo> Bind<TTo>(Func<T, MayBe<TTo>> monad) => instance == null ? MayBe<TTo>.None : monad(this.instance);
    public MayBe<TTo> Bind<TTo>(Func<T, TTo> monad) => instance == null ? MayBe<TTo>.None : new MayBe<TTo>(monad(this.instance));
}

public static class MayBeExtensions
{
    public static MayBe<T> MayReturn<T>(this T instance) => instance == null ? MayBe<T>.None : new MayBe<T>(instance);
}

public static class MonadExtensions
{
    public static Monad<T> Return<T>(this T instance) => new Monad<T>(instance);
    public static Monad<T> Return<T>(this Monad<T> instance) => instance;
}

public static class Test
{
    public static MayBe<string> TestA() => new MayBe<string>("may");
    public static MayBe<int> TestB(string x) => new MayBe<int>(x.Length);
    public static string TestC(int x) => x.ToString();
    public static int Test1(int v) => v;
    public static int Test2(int v) => v;
    public static void Do<T, TTo>()
    {
        var result = 1.Return().Bind(Test1).Bind(Test2);
        var result2 = TestA().Bind(TestB).Bind(TestC);
        T v = default(T);
        Func<T, Monad<TTo>> f = null;
        var identity = v.Return().Return().Bind(f) == f(v);
        //var assosiativity = 1.MayReturn().Bind(TestC).Bind(TestB) == ;
    }
}





