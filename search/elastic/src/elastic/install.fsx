#r "../../packages/Elasticsearch.Net/lib/net46/Elasticsearch.Net.dll"
#r "../../packages/NEST/lib/net46/NEST.dll"
open System
open System.Net
open System.IO.Compression
open Elasticsearch.Net
open Elasticsearch
open Nest

type IAudited =  abstract member operationtimestamp : DateTime 

type Knowledge = 
  { title:string; operationtimestamp : DateTime} 
  interface IAudited with member x.operationtimestamp = x.operationtimestamp

let connection = ConnectionConfiguration()
let client = new ElasticClient(connection)
let now = DateTime.UtcNow;
let body = {  title = "Home23"; operationtimestamp = now }
let indexed = client.Index("myindex3", body.GetType().Name, Guid.NewGuid().ToString("N"), body)
let result = client.Search<Knowledge>()