// test for modern C#, generic + inheritance polymorphism

class Fruit { }
class Orange : Fruit { }
class Apple : Fruit { }

delegate TResult MyFunc<in T, TResult>(T value);

// will compile?
var x1 = new MyFunc<Apple, Fruit>(_ => default(Fruit));
var y1 = new MyFunc<Orange, Fruit>(_ => default(Fruit));
var z1 = new List<MyFunc<Fruit, Fruit>>();
z1.Add(x1);
z1.Add(y1);

// will compile?
var x2 = new MyFunc<Fruit, Apple>(_ => default(Apple));
var y2 = new MyFunc<Fruit, Orange>(_ => default(Orange));
var z2 = new List<MyFunc<Fruit, Fruit>>();
z2.Add(x2);
z2.Add(y2);

// what to make compile? (hint is out parameter for TResult)

// OOP variant
interface IMyFunc<in T, TResult> { TResult Apply(T value); }
struct DummyMyFunc<T, TResult> : IMyFunc<T, TResult>
{
    public TResult Apply(T value) => default(TResult); // we could fill with any action
}

// will compile?
var a1 = new DummyMyFunc<Apple, Fruit>();
var b1 = new DummyMyFunc<Orange, Fruit>();
var c1 = new List<IMyFunc<Fruit, Fruit>>();
c1.Add(a1);
c1.Add(b1);

// will compile?
var a2 = new DummyMyFunc<Fruit, Apple>();
var b2 = new DummyMyFunc<Fruit, Orange>();
var c2 = new List<IMyFunc<Fruit, Fruit>>();
c2.Add(a2);
c2.Add(b2);

// what to make compile? (hint is out parameter for TResult)

// hints
//https://stackoverflow.com/questions/3445631/still-confused-about-covariance-and-contravariance-in-out