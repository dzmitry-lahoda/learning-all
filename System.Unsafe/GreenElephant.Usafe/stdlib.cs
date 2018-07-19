 
  
  
  

using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace System.Collections.Unsafe
{

namespace bit16
{
   public static  unsafe class  stdlib
   {
     [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
     public static void* malloc(ushort size)
	 {
	   return (void*)System.Runtime.InteropServices.Marshal.AllocHGlobal((int)size);
	 }

	    public static void free(void* mem)
        {
            System.Runtime.InteropServices.Marshal.FreeHGlobal((IntPtr)mem);
        }

	 [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
     public static void* malloc(int size)
	 {
	   return (void*)System.Runtime.InteropServices.Marshal.AllocHGlobal((int)size);
	 }

	 [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
     public static char* _(string str)
	 {
	   fixed(char* cString = str.ToCharArray())
	   {
	     return cString;
	   }
	 }
	 


	 public static   ushort strlen(char *s)
 {
     char *p = s;
     while (*p != '\0') p++;
     return (ushort)(p - s);
 }
           public static  void* memset (void* s, int c, ushort n)
 {
      char* us = (char*)s;
             char uc = (char)c;
     while (n-- != 0)
         *us++ = uc;
     return s;
 }

    
    public delegate int compareint(int* a, int* b);

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static void swap(int* a, int* b)
        {
            int* c = a;
            *a = *b;
            *b = *c;
        }


    public delegate int comparelong(long* a, long* b);

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static void swap(long* a, long* b)
        {
            long* c = a;
            *a = *b;
            *b = *c;
        }


    public delegate int comparefloat(float* a, float* b);

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static void swap(float* a, float* b)
        {
            float* c = a;
            *a = *b;
            *b = *c;
        }


    public delegate int comparedouble(double* a, double* b);

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static void swap(double* a, double* b)
        {
            double* c = a;
            *a = *b;
            *b = *c;
        }


    public delegate int comparedecimal(decimal* a, decimal* b);

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static void swap(decimal* a, decimal* b)
        {
            decimal* c = a;
            *a = *b;
            *b = *c;
        }


    public delegate int compareBigInteger(BigInteger* a, BigInteger* b);

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static void swap(BigInteger* a, BigInteger* b)
        {
            BigInteger* c = a;
            *a = *b;
            *b = *c;
        }


    public delegate int compareComplex(Complex* a, Complex* b);

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static void swap(Complex* a, Complex* b)
        {
            Complex* c = a;
            *a = *b;
            *b = *c;
        }


    public delegate int comparechar(char* a, char* b);

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static void swap(char* a, char* b)
        {
            char* c = a;
            *a = *b;
            *b = *c;
        }


   }



}
namespace bit32
{
   public static  unsafe class  stdlib
   {
     [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
     public static void* malloc(uint size)
	 {
	   return (void*)System.Runtime.InteropServices.Marshal.AllocHGlobal((int)size);
	 }

	    public static void free(void* mem)
        {
            System.Runtime.InteropServices.Marshal.FreeHGlobal((IntPtr)mem);
        }

	 [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
     public static void* malloc(int size)
	 {
	   return (void*)System.Runtime.InteropServices.Marshal.AllocHGlobal((int)size);
	 }

	 [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
     public static char* _(string str)
	 {
	   fixed(char* cString = str.ToCharArray())
	   {
	     return cString;
	   }
	 }
	 


	 public static   uint strlen(char *s)
 {
     char *p = s;
     while (*p != '\0') p++;
     return (uint)(p - s);
 }
           public static  void* memset (void* s, int c, uint n)
 {
      char* us = (char*)s;
             char uc = (char)c;
     while (n-- != 0)
         *us++ = uc;
     return s;
 }

    
    public delegate int compareint(int* a, int* b);

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static void swap(int* a, int* b)
        {
            int* c = a;
            *a = *b;
            *b = *c;
        }


    public delegate int comparelong(long* a, long* b);

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static void swap(long* a, long* b)
        {
            long* c = a;
            *a = *b;
            *b = *c;
        }


    public delegate int comparefloat(float* a, float* b);

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static void swap(float* a, float* b)
        {
            float* c = a;
            *a = *b;
            *b = *c;
        }


    public delegate int comparedouble(double* a, double* b);

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static void swap(double* a, double* b)
        {
            double* c = a;
            *a = *b;
            *b = *c;
        }


    public delegate int comparedecimal(decimal* a, decimal* b);

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static void swap(decimal* a, decimal* b)
        {
            decimal* c = a;
            *a = *b;
            *b = *c;
        }


    public delegate int compareBigInteger(BigInteger* a, BigInteger* b);

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static void swap(BigInteger* a, BigInteger* b)
        {
            BigInteger* c = a;
            *a = *b;
            *b = *c;
        }


    public delegate int compareComplex(Complex* a, Complex* b);

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static void swap(Complex* a, Complex* b)
        {
            Complex* c = a;
            *a = *b;
            *b = *c;
        }


    public delegate int comparechar(char* a, char* b);

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static void swap(char* a, char* b)
        {
            char* c = a;
            *a = *b;
            *b = *c;
        }


   }



}
namespace bit64
{
   public static  unsafe class  stdlib
   {
     [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
     public static void* malloc(ulong size)
	 {
	   return (void*)System.Runtime.InteropServices.Marshal.AllocHGlobal((int)size);
	 }

	    public static void free(void* mem)
        {
            System.Runtime.InteropServices.Marshal.FreeHGlobal((IntPtr)mem);
        }

	 [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
     public static void* malloc(int size)
	 {
	   return (void*)System.Runtime.InteropServices.Marshal.AllocHGlobal((int)size);
	 }

	 [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
     public static char* _(string str)
	 {
	   fixed(char* cString = str.ToCharArray())
	   {
	     return cString;
	   }
	 }
	 


	 public static   ulong strlen(char *s)
 {
     char *p = s;
     while (*p != '\0') p++;
     return (ulong)(p - s);
 }
           public static  void* memset (void* s, int c, ulong n)
 {
      char* us = (char*)s;
             char uc = (char)c;
     while (n-- != 0)
         *us++ = uc;
     return s;
 }

    
    public delegate int compareint(int* a, int* b);

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static void swap(int* a, int* b)
        {
            int* c = a;
            *a = *b;
            *b = *c;
        }


    public delegate int comparelong(long* a, long* b);

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static void swap(long* a, long* b)
        {
            long* c = a;
            *a = *b;
            *b = *c;
        }


    public delegate int comparefloat(float* a, float* b);

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static void swap(float* a, float* b)
        {
            float* c = a;
            *a = *b;
            *b = *c;
        }


    public delegate int comparedouble(double* a, double* b);

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static void swap(double* a, double* b)
        {
            double* c = a;
            *a = *b;
            *b = *c;
        }


    public delegate int comparedecimal(decimal* a, decimal* b);

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static void swap(decimal* a, decimal* b)
        {
            decimal* c = a;
            *a = *b;
            *b = *c;
        }


    public delegate int compareBigInteger(BigInteger* a, BigInteger* b);

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static void swap(BigInteger* a, BigInteger* b)
        {
            BigInteger* c = a;
            *a = *b;
            *b = *c;
        }


    public delegate int compareComplex(Complex* a, Complex* b);

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static void swap(Complex* a, Complex* b)
        {
            Complex* c = a;
            *a = *b;
            *b = *c;
        }


    public delegate int comparechar(char* a, char* b);

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static void swap(char* a, char* b)
        {
            char* c = a;
            *a = *b;
            *b = *c;
        }


   }



}
}

