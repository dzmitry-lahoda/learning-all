namespace core

open System
open System.Net
open System.IO.Compression
open Nest

type IAudited =  abstract member operationtimestamp : DateTimeOffset 

[<ElasticsearchType()>]
type Knowledge() = 

  member val operationtimestamp = DateTimeOffset.UtcNow with get,set
  [<String(Index = FieldIndexOption.NotAnalyzed)>]
  member val id:String = null with get, set
  [<String(Index = FieldIndexOption.NotAnalyzed)>]
  member val ownerId:String = null with get, set
  [<String(Index = FieldIndexOption.NotAnalyzed)>]
  member val title:String = null with get, set
  interface IAudited with member x.operationtimestamp = x.operationtimestamp



