open System

let rings (current:Int32) (steps:Int32) (length:Int32) =
  let shift = current - steps
  if shift < 0 then shift + length else shift
  
let five = rings 10 5 20
printfn "%b" (five = 5)

let i17 = rings 10 13 20
printfn "%b" (i17 = 17)

let ringsu (current:UInt32) (steps:UInt32) (length:UInt32) =
  if current < steps then
    length - steps + current
  else 
    current - steps
    
let fiveu = ringsu 10u 5u 20u
printfn "%b" (fiveu = 5u)

let i17u = ringsu 10u 13u 20u
printfn "%b" (i17u = 17u)


let a = 10u
let b = 12u

let c = a - b
printfn "%b" (c = 4294967294u)
