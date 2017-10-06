using System;
namespace GreenElephant.Usafe.bit64
{
    public enum CellError : byte
    {
        ok,
        cannot_push_onto_filled_list,
        cannot_pop_empty_list
    }



    public static class errorExtensions
    {
        public static void unwrap(this CellError err)
        {
            if (err != CellError.ok) System.Environment.Exit((int)err);
        }
    }

    public unsafe struct charCellresult
    {
        public charCell* __value;
        public CellError error;

        public charCell* unwrap()
        {
            this.error.unwrap();
            return __value;
        }
    }
}