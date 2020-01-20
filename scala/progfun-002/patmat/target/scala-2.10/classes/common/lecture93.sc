package common

object lecture93 {


   def group[T,R](xs: List[T])(implicit map : List[T] => R): List[R] = xs match {
    case Nil      => Nil
    case x :: xs1 => {
      val (first,rest) = xs span ( y => x == y)
      map(first) :: group(rest)(map)
    }
   }                                              //> group: [T, R](xs: List[T])(implicit map: List[T] => R)List[R]
   def encode[T](xs: List[T]): List[(T,Int)] = group(xs)(x=> (x.head,x.length))
                                                  //> encode: [T](xs: List[T])List[(T, Int)]
   def pack[T](xs: List[T]): List[List[T]] = group(xs)(x => x)
                                                  //> pack: [T](xs: List[T])List[List[T]]
   
   val data = List("a", "a", "a", "b", "c", "c", "a")
                                                  //> data  : List[String] = List(a, a, a, b, c, c, a)
   data takeWhile (x => x == "a")                 //> res0: List[String] = List(a, a, a)
   data span (x => x == "a")                      //> res1: (List[String], List[String]) = (List(a, a, a),List(b, c, c, a))
   data partition (x=> x == "a")                  //> res2: (List[String], List[String]) = (List(a, a, a, a),List(b, c, c))
   pack(data)                                     //> res3: List[List[String]] = List(List(a, a, a), List(b), List(c, c), List(a))
                                                  //| 
   encode(data)                                   //> res4: List[(String, Int)] = List((a,3), (b,1), (c,2), (a,1))
}