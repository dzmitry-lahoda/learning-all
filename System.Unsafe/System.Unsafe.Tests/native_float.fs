module TestSingle
  #nowarn "9"
  open Microsoft.FSharp.NativeInterop
  open System.Runtime.InteropServices
  open Swensen.Unquote
  open System.Native
  open System

  [<Struct>]
  type StructPoint = 
    { X: Single;
      Y: Single }

  let allockF = Marshal.AllocHGlobal(nativeint (2 *  sizeof<StructPoint>))
  let b = NativePtr.ofNativeInt<StructPoint>(allockF)

  let p = { X = 123.0f; Y = 2.0f}
  NativePtr.set b 0 p

  let f1 = alg.find32 b 2 p
  let f2 = alg.find32 b 2 { X = 13.0f; Y = 666.0f}