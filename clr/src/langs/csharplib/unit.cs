namespace csharplib
{
    public struct Unit
    {
        public static Unit unit { get => new Unit(); }
    }

    public static class Extensions
    {
        public static void Do<T>(this T sub, System.Action<T> d)
        {
            d(sub);
        }

        public static Unit FDo<T>(this T sub, System.Action<T> d)
        {
            d(sub); return Unit.unit;
        }
    }
}
