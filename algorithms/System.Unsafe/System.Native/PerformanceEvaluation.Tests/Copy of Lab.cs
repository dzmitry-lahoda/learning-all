using System.Diagnostics.Contracts;
using MatrixExtensions.Generic;
using MatrixExtensions.Operations;

namespace MatrixExtensionsTestProject1
{
    [Pure]
    public static class Lab2
    {

        public static int[,] MultiplyStrassen(this int[,] x, int[,] y)
        {
            Contract.Requires(x.RowCount() == y.ColumnCount());
            int xc = x.ColumnCount();
            int xr = x.RowCount();
            int yc = y.ColumnCount();
            int yr = y.RowCount();
            //TODO: make it strassen istead recursive
            Block xb = new Block(0, xr, 0, xc);
            Block yb = new Block(0, yr, 0, yc);
            return Multiply(x, xb, y, yb);
        }

        private struct Block
        {
            public long r0, r, c0, c;

            public Block(long r0, long r, long c0, long c)
            {
                this.r0 = r0;
                this.r = r;
                this.c0 = c0;
                this.c = c;
            }
        }

        private static int[,] Clone(int[,] w,Block wb)
        {
            var cloned = new int[ wb.r-wb.r0,wb.c-wb.c0];
            long ic = 0;
            for (long i = wb.r0; i < wb.r; i++)
            {
                long jc = 0;
                for (long j = wb.c0; j < wb.c; j++)
                {
                    cloned[ic, jc] = w[i, j];
                    jc++;
                }
                ic++;
            }
            return cloned;
        }

        private static int[,] Multiply(int[,] x, Block xb, int[,] y, Block yb)
        {
            if (xb.r0 == xb.r - 1)
            {
                var cs = new[,] { { x[xb.r0, xb.c0] * y[yb.r0, yb.c0] } };
                return cs;
            }

            var xrd = (xb.r + xb.r0) / 2;
            var xcd = (xb.c + xb.c0) / 2;
            var yrd = (yb.r + yb.r0) / 2;
            var ycd = (yb.c + yb.c0) / 2;

            var xb11 = new Block(xb.r0, xrd, xb.c0, xcd); var xb12 = new Block(xb.r0, xrd, xcd, xb.c);
            var xb21 = new Block(xrd, xb.r, xb.c0, xcd); var xb22 = new Block(xrd, xb.r, xcd, xb.c);

            var x11 = Clone(x, xb11);var x12 = Clone(x, xb12);
            var x21 = Clone(x, xb21);var x22 = Clone(x, xb22);

            var yb11 = new Block(yb.r0, yrd, yb.c0, ycd); var yb12 = new Block(yrd, yb.r, yb.c0, ycd);
            var yb21 = new Block(yrd, yb.r, yb.c0, ycd); var yb22 = new Block(yrd, yb.r, ycd, yb.c);
            
            var y11 = Clone(y, yb11);var y12 = Clone(y, yb12);
            var y21 = Clone(y, yb21);var y22 = Clone(y, yb22);

            var p1 = Multiply(x11, xb11, y12.Substract(y22), yb12);
            var p2 = Multiply(x11.Add(x12), xb11, y22, yb22);
            var p3 = Multiply(x21.Add(x22), xb21, y11, yb11);
            var p4 = Multiply(x22, xb22, y21.Substract(y11), yb21);
            var p5 = Multiply(x11.Add(x22), xb11, y11.Add(y22), yb11);
            var p6 = Multiply(x12.Substract(x22), xb12, y21.Substract(y22), yb21);
            var p7 = Multiply(x11.Substract(x12), xb11, y11.Substract(y12), yb11);

            var c11 = p5.Add(p4).Substract(p2).Add(p6);
            var c21 = p3.Add(p4);
            var c12 = p1.Add(p2);
            var c22 = p1.Add(p5).Substract(p3).Substract(p7);


            var c = new int[c11.RowCount() + c22.RowCount(), c21.ColumnCount() + c12.ColumnCount()];

            for (long i = 0; i < c11.RowCount(); i++)
            {

                for (long j = 0; j < c11.ColumnCount(); j++)
                {
                    c[i, j] = c11[i, j];
                }
                int cd = c12.ColumnCount();
                for (long j = 0; j < cd; j++)
                {
                    c[i, j + cd] = c12[i, j];
                }
            }

            int rd = c11.RowCount();
            for (long i = 0; i < c21.RowCount(); i++)
            {
                for (long j = 0; j < c21.ColumnCount(); j++)
                {
                    c[i + rd, j] = c21[i, j];
                }
                int cd = c22.ColumnCount();
                for (long j = 0; j < c22.ColumnCount(); j++)
                {
                    c[i + rd, j + cd] = c22[i, j];
                }
            }

            return c;
        }
    }
}