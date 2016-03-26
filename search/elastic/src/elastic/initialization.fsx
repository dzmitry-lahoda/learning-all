#r "../packages/Elasticsearch.Net/lib/net45/Elasticsearch.Net.dll"
open System
open System.Net
open System.IO.Compression
open Elasticsearch.Net
open Elasticsearch.Net.Connection

let client = new ElasticsearchClient(null,null,null)
printfn "%s" (client.ToString())
client.Index("test","test","test")
