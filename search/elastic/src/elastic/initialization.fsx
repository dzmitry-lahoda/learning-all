#r "../../packages/Elasticsearch.Net/lib/net45/Elasticsearch.Net.dll"
open System
open System.Net
open System.IO.Compression
open Elasticsearch.Net


let client = new ElasticLowLevelClient()
printfn "%s" (client.ToString())
client.Index("test","test", new PostData<Object>("data"))
