﻿#r "../packages/Elasticsearch.Net/lib/net45/Elasticsearch.Net.dll"
open System
open System.Net
open System.IO.Compression
open Elasticsearch.Net
open Elasticsearch.Net.Connection

type IAudited =  abstract member operationtime : DateTime 

type Bookmark = 
  { title:string; operationtime : DateTime} 
  interface IAudited with member x.operationtime = x.operationtime

let connection = ConnectionConfiguration().ExposeRawResponse()
let client = new ElasticsearchClient(connection)
let body = {  title = "Home23"; operationtime = DateTime.UtcNow }
let indexed = client.Index("myindex3", body.GetType().Name, Guid.NewGuid().ToString("N"), body)