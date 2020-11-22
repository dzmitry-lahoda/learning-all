using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharplib
{
    public interface IF<in T, out TResult> { TResult _(T v); }

    public class FF<T,TResult>
    {
        private IF<T, TResult> a;
        private IF<T, TResult> b;

        public FF(IF<T,TResult> a, IF<T,TResult> b)
        {
            this.a = a;
            this.b = b;
        }

        public void _(T v)
        {
            a._(v);
            b._(v);
        }
    }

    public static class StuctFactory
    {
        public class DoInner : IF<int, int> { public int _(int v) => (v + 1) / 2; }
        public static DoInner CreateInner()
        {
            return new DoInner();
        }

        public class  Do : IF<int, double> { public IF<int, int> doInner; public double _(int v) => (v * doInner._(v)) * 1.331231231233; }

        public static Do Create()
        {
            var d =  new Do();
            d.doInner = CreateInner();
            return d;
        }
    }

    public static class DelegateFactory
    {
        public static Func<int, int> CreateInner()
        {
            Func<int, int> doDelegateInner = (v) => (v + 1) / 2;
            return doDelegateInner;
        }

        public static Func<int, double> Create()
        {
            var inner = CreateInner();
            Func<int, double> doDelegate = (v) => (v * inner(v)) * 1.331231231233;
            return doDelegate;
        }
    }
}