#r "../../packages/FSharp.Data/lib/net40/FSharp.Data.dll"
open System
open System.Collections.Generic
open FSharp.Data

let file = @"abc.csv"
let csv = CsvFile.Load(file).Cache()
let dict = new Dictionary<string,HashSet<string>>();

for row in csv.Rows do
  let a = row.GetColumn("Key1")
  let succ, result = dict.TryGetValue(a)
  if (succ) then
     result.Add(row.GetColumn("Key2")) |> ignore
     dict.Item(a) <- result
  else 
     let n = new HashSet<string>()
     n.Add(row.GetColumn("Storage Key")) |> ignore
     dict.Item(a) <- n

for i in dict do
  if i.Value.Count > 1 then 
    Console.WriteLine(i.Key)