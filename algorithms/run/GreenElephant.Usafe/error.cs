using System;
namespace System.Collections.Unsafe
{
    public static class errorExtensions
    {
        public static void unwrap(this CellError err)
        {
            if (err != CellError.ok) System.Environment.Exit((int)err);
        }
    }
}