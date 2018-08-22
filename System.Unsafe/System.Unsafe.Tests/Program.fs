// Learn more about F# at http://fsharp.org
#nowarn "9"
open Expecto
open System
open Microsoft.FSharp.NativeInterop
open Swensen.Unquote
open System.Native
open System.Reflection
open Expecto

let alloc (x:nativeint) = System.Runtime.InteropServices.Marshal.AllocHGlobal(x)
let set = NativePtr.set
let get = NativePtr.get
let add = NativePtr.add
let read = NativePtr.read
let write = NativePtr.write
let ofNativeInt = NativePtr.ofNativeInt


[<Tests>]
let tests = 
  testList "alg" [
      test "get shift" {
        let struct (shift0, index0) = alg.getShift 0L
        Assertions.test <@ shift0 = 0L @>
        Assertions.test <@ index0 = 0 @>

        let struct (shift1, index1) = alg.getShift 1L
        Assertions.test <@ shift1 = 0L @>
        Assertions.test <@ index1 = 1 @>

        let struct (shift2, index2) = alg.getShift alg.maxInt32AsInt64
        Assertions.test <@ shift2 = 1L @>
        Assertions.test <@ index2 = 0 @>

        let struct (shift3, index3) = 2L * alg.maxInt32AsInt64 + 1L |> alg.getShift 
        Assertions.test <@ shift3 = 2L @>
        Assertions.test <@ index3 = 1 @>

        let struct (shift4, index4) = alg.getShift Int64.MaxValue
        Assertions.test <@ int64(shift4) * int64(Int32.MaxValue) + int64(index4) = Int64.MaxValue @>
      }

      test "find int32" {
          let for2Long: nativeint = 2n
          let memory = alloc(for2Long)
          let typed = ofNativeInt<int32>(memory)
          set typed 0 123
          set typed 1 42  
          let f1 = alg.find32 typed 2 123
          Assertions.test <@ f1 = 0 @> 
          let f2 = alg.find32 typed 2 42
          Assertions.test <@ f2 = 1 @> 
          let f3 = alg.find32 typed 2 13 
          Assertions.test <@ f3 = -1 @> 
      }
  ]

[<EntryPoint>]
let main argv =
    runTests defaultConfig tests
