#nowarn "9"
namespace System.Native
open Microsoft.FSharp.NativeInterop
open System.Runtime.InteropServices

module alg =
  type Array256<'a when 'a : unmanaged>= {ptr:nativeptr<'a>}  

  // finds zero based index of e element in array x of lenght l
  // - if not found
  let find (x:nativeptr<'a>) (l:int) (e:'a) =
    let mutable r = -1
    for i = 0 to  l-1 do
      let v = NativePtr.get x i
      if (v = e) then 
        r <- i      
    r

  let inline add(value1 : ^T when ^T : (static member (+) : ^T * ^T -> ^T) and ^T : unmanaged, value2: ^T) =
    value1 + value2

  let inline x a b c = a + b + c
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