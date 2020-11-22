    public class Class1<T>: Class1<T>.I1 where T:Class1<T>
    {
        public class Class2<T> : Class1<T> where T : Class1<T> {}

        public class Class3<T> : Class2<T> where T : Class1<T>
        {
            public Class3(Class1<T> c1, Class2<T> c2) { }
        }

        public Class1<T> P1 { get { return default(Class1<T>); } }

        interface I1
        {
             Class1<T> P1 { get; }
        }
    }