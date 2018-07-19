// Learn more about F# at http://fsharp.org. See the 'F# Tutorial' project
// for more guidance on F# programming.

#load "Library1.fs"
#nowarn "9"
open Library1
open Microsoft.FSharp.NativeInterop
open System.Runtime.InteropServices
// Define your library scripting code here

let a: nativeint = 1n
let allock = Marshal.AllocHGlobal(a)
let b = NativePtr.ofNativeInt<int64>(allock)
