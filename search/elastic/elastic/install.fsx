#r "../packages/Elasticsearch.Net/lib/net45/Elasticsearch.Net.dll"
open System
open System.Net
open System.IO.Compression

open Elasticsearch.Net
open Elasticsearch.Net.Connection

let connection = ConnectionConfiguration().ExposeRawResponse()


type Bookmark =  { id: string; title:string; operationtime : DateTime}
let client = new ElasticsearchClient(connection)
let indexed = client.Index("myindex3","mytype", "2", { id ="1" ;  title = "Home23"; operationtime = DateTime.UtcNow })