
open System.Linq

type istorage =
  abstract member save<'a> : 'a -> int
  abstract member get<'a> : unit ->  IQueryable<'a>

