using static System.Console;

namespace UnionSample
{
    class Program
    {
        public static void Main()
        {
            var s = BS.Of("I am string");
            WriteLine(s);
            WriteLine(Test.Foo1(s));
            WriteLine(Test.Foo2(s));
            WriteLine(BS.Of(false));

            WriteLine(Some.Of(42));        // Custom constructor for Optional<int>.Of(42);
            WriteLine(Optional<int>.None); // Custom constructor for Optional<int>.Of(Unit.unit);
        }
    }

    // One line sub-typing
    public sealed class BS : U<bool, string> { }

    // A different type!
    public sealed class Other : U<bool, string> { }

    public static class Test
    {
        // Specifying as type parameter enusres no allocations! when passing struct argument. That's why `Case` implementations structs
        public static int Foo1<T>(T x) where T : BS.IUnion =>
          x.Case == BS.Case.Case1 ? 1 : 0;

        // In worst case boxes the argument, but less noisy to declare
        public static int Foo2(BS.IUnion x) =>
          x.Case == BS.Case.Case1 ? 1 : 0;
    }

    public struct Unit
    {
        public static readonly Unit unit = new Unit();
    }

    public class Optional<T> : U<Unit, T>
    {
        public static Case1 None => Of(Unit.unit); // User-friendly custom case name

        // may be othe useful stuff
    }

    // Good custom constructor - the best for type inference
    public static class Some
    {
        public static Optional<T>.Case2 Of<T>(T x) => Optional<T>.Of(x);
    }

    // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    // !!!! CRASHES VISUAL STUDIO !!!! 
    // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

    //public sealed class List<T> : U<Unit, (T, List<T>.IUnion)> {}

    public class U<T1, T2>
    {
        // Enum closes the union for fixed number of cases - same as Tag in F# sum-type implementation.
        // Can be useful for `switch`ing prior to C# 7 where pattern matching on type is not available.
        public enum Case { Case1, Case2 };

        public interface IUnion
        {
            Case Case { get; }
        }

        public static Case1 Of(T1 x) => new Case1(x);
        public static Case2 Of(T2 x) => new Case2(x);

        public struct Case1 : IUnion
        {
            public Case Case => Case.Case1;

            public readonly T1 Value;
            public Case1(T1 x) { Value = x; }

            public override string ToString() => Value.ToString();

            // GetHashCode, Equality, etc.
        }

        public struct Case2 : IUnion
        {
            public Case Case => Case.Case1;

            public readonly T2 Value;
            public Case2(T2 x) { Value = x; }

            public override string ToString() => Value.ToString();

            // GetHashCode, Equality, etc.
        }
    }
}
