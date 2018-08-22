#nowarn "9"
namespace System.Native
open Microsoft.FSharp.NativeInterop
open System.Runtime.InteropServices

// F# may have generic inlined code over native pointers, C# 7.3 cannot
// F# `for` loop work only with `int32`

// Design:
// - starts from exact raw number crunching and goes with probablisitc method
// - no checks for bounds or arguments validations
// - no exceptions
// - petabyte `arrays`
// - usage of SIMD and parallel and GPU inside
// - mutable to maximum
// - typed to maximum
// - all allocation are explicit by passing allocator
// - eager evaluation of all parameter
// - all comparison and hash function are passed explicitly
// - customer structures in pointers
// - zero managed memory allocations

module alg =
  open System

  //type Array256<'a when 'a : unmanaged>= {ptr:nativeptr<'a>}  

  // finds zero based index of e element in array x of length
  // - if not found
  let inline find32 (storage:nativeptr<'a>) (length:int) (comparison:'a) : int =
    let mutable result = -1
    let mutable index = 0
    while index < length do
      let value = NativePtr.get storage index
      if (value = comparison) then 
        result <- index 
        index <- length
      else
        index <- index + 1
    result
  
  let maxInt32AsInt64 = int64(Int32.MaxValue)

  // hack for NativePtr to work with int64
  let inline getShift (index:Int64) =
    let shift = index /  maxInt32AsInt64
    let remainder = int32(index %  maxInt32AsInt64)
    struct  (shift, remainder)

  let inline find (storage:nativeptr<'a>) (length:int64) (comparison:'a) : int64 =
    let struct (shift, remainder) = getShift(length)
    if shift = 0L then
      find32 storage remainder comparison |> int64
    else
        let mutable shiftingStorage = storage
        let mutable shiftIndex = 0L
        let mutable lastCounter = Int32.MaxValue
        let mutable result = -1L
        while shiftIndex <= shift do
          if shiftIndex = shift then
            lastCounter <- remainder
          let innerResult = find32 shiftingStorage lastCounter comparison
          if innerResult >= 0 then
              result <- int64(shiftIndex) + int64(innerResult)
          shiftingStorage <- NativePtr.add shiftingStorage lastCounter
            
        result
    

  let inline x a b c = a + b + c

  let inline add(value1 : ^T when ^T : (static member (+) : ^T * ^T -> ^T) and ^T : unmanaged, value2: ^T) =
    value1 + value2

  let inline sum (x:nativeptr<'a> when 'a : unmanaged and 'a : (static member (+) : 'a * 'a -> 'a) ) (l:int) =
    let mutable r = Unchecked.defaultof<'a>
    for i = 0 to  l-1 do
      let v = NativePtr.get x i
      r <- r + v    
    r

  let max (x:nativeptr<'a>) (l:int) (e:'a) =
    let mutable r = -1
    for i = 0 to  l-1 do
      let v = NativePtr.get x i
      if (v = e) then 
        r <- i      
    r