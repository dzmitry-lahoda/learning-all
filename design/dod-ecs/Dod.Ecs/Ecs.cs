using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Dod.Ecs
{
    // TODO: how to check stuctures equivalence
    public static class Ecs
    {
        public unsafe static ref TOut AsReadonly<TIn, TOut, TReadonly>(ref TIn readWrite) 
            where TIn : unmanaged, TReadonly
            where TOut : unmanaged, TReadonly
            
            {
                Debug.Assert(sizeof(TOut) == sizeof(TIn));
                var result = (TOut*)Unsafe.AsPointer(ref readWrite);
                return ref Unsafe.AsRef<TOut>(result);
            }

        public unsafe static ref TOut AsReadonly<TIn, TOut>(ref TIn readWrite) 
            where TIn : unmanaged
            where TOut : unmanaged
            
            {
                Debug.Assert(sizeof(TOut) == sizeof(TIn));
                var result = (TOut*)Unsafe.AsPointer(ref readWrite);
                return ref Unsafe.AsRef<TOut>(result);
            }            

        public unsafe static ref TOut AsWrite<TIn, TOut, TReadonly, TWrite>(ref TIn read) 
            where TIn : unmanaged, TReadonly
            where TOut : unmanaged, TReadonly, TWrite
            where TWrite : TReadonly
            
            {
                Debug.Assert(sizeof(TOut) == sizeof(TIn));
                var result = (TOut*)Unsafe.AsPointer(ref read);
                return ref Unsafe.AsRef<TOut>(result);
            }

        public unsafe static ref TOut AsWrite<TIn, TOut>(ref TIn read) 
            where TIn : unmanaged
            where TOut : unmanaged
            
            {
                Debug.Assert(sizeof(TOut) == sizeof(TIn));
                var result = (TOut*)Unsafe.AsPointer(ref read);
                return ref Unsafe.AsRef<TOut>(result);
            }            

    }
}
