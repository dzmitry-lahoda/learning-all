
open System


// int 32 and int 64 values
let int32MaxValueLong = int64(Int32.MaxValue)
let int32MaxValue = Int32.MaxValue
let int32MinValue = Int32.MinValue
let int64MaxValue = Int64.MaxValue
let max64DivideMax32 = int64MaxValue / int32MaxValueLong
let max64DivideMax32Back = max64DivideMax32 * int32MaxValueLong
let remain = max64DivideMax32 / int32MaxValueLong
let remainBack  = remain * int32MaxValueLong
let int32xint32 = int64(Int32.MaxValue) * int64(Int32.MaxValue) * 2L
let int64MaxValueMinusint32xint32 = int64MaxValue - int32xint32
let question = (Int64.MaxValue / int64(Int32.MaxValue)) * int64(Int32.MaxValue)  - Int64.MaxValue
let question2 = (UInt64.MaxValue / uint64(UInt32.MaxValue)) * uint64(UInt32.MaxValue)  - UInt64.MaxValue
