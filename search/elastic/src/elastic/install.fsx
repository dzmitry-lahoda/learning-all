#r "../../packages/Elasticsearch.Net/lib/net46/Elasticsearch.Net.dll"
#r "../../packages/NEST/lib/net46/NEST.dll"
#r "../../build/Debug/core.dll"

open System
open System.Net
open System.IO.Compression
open Elasticsearch.Net
open Elasticsearch
open Nest
open model

let connection = new ConnectionSettings()
let client = new ElasticClient(connection)
client.Ping()
let now = DateTime.UtcNow;
let body = {  title = "Home23"; operationtimestamp = now }

let index = new IndexRequest<String>(IndexName.op_Implicit("indexname"), TypeName.op_Implicit("typename"))

let indexed = client.Index("body", fun i -> i.Index(IndexName.op_Implicit("indexname")) :> IIndexRequest)
let result = client.Search<Knowledge>()