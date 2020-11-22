open System

let maxInt64ToFloat64 = float (Int64.MaxValue)
let minInt64ToFloat64 = float (Int64.MinValue)
let zeroInt64ToFloat64 = float (0L)
let divide1 = maxInt64ToFloat64 / float(Int64.MaxValue)
let divide2 = maxInt64ToFloat64 / float(Int64.MinValue)

let divide3 = minInt64ToFloat64 / float(Int64.MaxValue)
let divide4 = minInt64ToFloat64 / float(Int64.MinValue)
let divide23 = minInt64ToFloat64 / float(0L)

let divide5 = zeroInt64ToFloat64 / float(Int64.MaxValue)
let divide6 = zeroInt64ToFloat64 / float(Int64.MinValue)
let divide7 = zeroInt64ToFloat64 / float(0L)
