namespace X
#nowarn "9"
open Microsoft.FSharp.NativeInterop
open System.Runtime.InteropServices
open NUnit.Framework
open Swensen.Unquote
open System.Native

module TestInt64 =
  let a: nativeint = 2n
  let allock = Marshal.AllocHGlobal(a)
  let b = NativePtr.ofNativeInt<int64>(allock)
  NativePtr.set b 0 123L
  NativePtr.set b 1 42L
  let z = NativePtr.get b 0
  let f1 = alg.find b 2 123L 
  let f2 = alg.find b 2 42L 
  let f3 = alg.find b 2 13L 
  let res = alg.sum b 2 

 
[<TestFixture>]
type public Test() =
  [<Test>]
  member public x.Find () =
    test <@ true @>



