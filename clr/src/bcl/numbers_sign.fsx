#nowarn "9"
open System
open Microsoft.FSharp.NativeInterop

let one: System.SByte = 0b0000_0001y
let minusOne: System.SByte = 0b1111_1111y

let mutable oneByte = 1y
let oneOne = &&oneByte |> NativePtr.toNativeInt |> NativePtr.ofNativeInt<byte> |> NativePtr.read
let stringed1 = Convert.ToString(oneOne, 2)


let inline reinterpretCast<'T,'F when 'T : unmanaged and 'F:unmanaged>(x : 'F) =
  &&x |> NativePtr.toNativeInt |> NativePtr.ofNativeInt<'T> |> NativePtr.read

let inline readAs (x:'a) : 'b =
  &&x |> NativePtr.toNativeInt |> NativePtr.ofNativeInt |> NativePtr.read<'b>



let mutable moneByte = -1y
let moneOne = reinterpretCast<byte,sbyte> moneByte

let mstringed1 = Convert.ToString(moneOne, 2)

let minus128: System.SByte = 0b1000_0000y
let getSign = (minusOne &&& 0b1000_0000y >>> 7) * 7y

let zero: System.SByte = 0b0000_0000y

let getSign1 = if (zero &&& 0b1000_0000y >>> 7) = 0y then 7y  else 7y

let sm4 =  -4y
let sm4b = Convert.ToString(reinterpretCast<byte,sbyte> sm4, 2)

let sm33 =  -33y
let sm33b = Convert.ToString(reinterpretCast<byte,sbyte> sm33, 2)

let sm8 =   -8y
let sm8u = readAs sm8 : byte
let sm8b = Convert.ToString(sm8u, 2)

let s4 =  4y
let s4b = Convert.ToString(reinterpretCast<byte,sbyte> s4, 2)
let s8 =   8y
let s8b = Convert.ToString(reinterpretCast<byte,sbyte> s8, 2)

let bbb =  0b1000_0001uy;

let c = reinterpretCast<sbyte,byte>(bbb)


let bbb1 =  0b0000_0011uy;

let c1 = reinterpretCast<sbyte,byte>(bbb1)

let sm3 =  -3y
let sm3b = Convert.ToString(reinterpretCast<byte,sbyte> sm3, 2)

let sm2 =  -2y
let sm2b = Convert.ToString(reinterpretCast<byte,sbyte> sm2, 2)