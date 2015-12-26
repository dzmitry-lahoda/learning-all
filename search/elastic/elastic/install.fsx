#r "../packages/Elasticsearch.Net/lib/net45/Elasticsearch.Net.dll"
open System
open System.Net
open System.IO.Compression
open Elasticsearch.Net
open Elasticsearch.Net.Connection

type IAudited =  abstract member operationtimestamp : DateTime 

type Bookmark = 
  { title:string; operationtimestamp : DateTime} 
  interface IAudited with member x.operationtimestamp = x.operationtimestamp

let connection = ConnectionConfiguration().ExposeRawResponse()
let client = new ElasticsearchClient(connection)
let now = DateTime.UtcNow;
let body = {  title = "Home23"; operationtimestamp = now }
let indexed = client.Index("myindex3", body.GetType().Name, Guid.NewGuid().ToString("N"), body)
let result = client.Search<Bookmark>()