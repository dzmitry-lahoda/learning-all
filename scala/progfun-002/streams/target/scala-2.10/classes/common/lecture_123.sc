package common

object lecture_123 {
  def from(n:Int) : Stream[Int] = n #:: from(n+1) //> from: (n: Int)Stream[Int]
  from(4) take (10) toList                        //> res0: List[Int] = List(4, 5, 6, 7, 8, 9, 10, 11, 12, 13)
  
  def sieve(s:Stream[Int]):Stream[Int] =
    s.head #:: sieve(s.tail filter (_ % s.head != 0))
                                                  //> sieve: (s: Stream[Int])Stream[Int]
  lazy val r = (1 to 5)                           //> r: => scala.collection.immutable.Range.Inclusive
  Map() ++ (r map (x=>  (x, x) ))                 //> res1: scala.collection.immutable.Map[Int,Int] = Map(5 -> 5, 1 -> 1, 2 -> 2, 
                                                  //| 3 -> 3, 4 -> 4)
         
  sieve(from(2)) take (10) toList                 //> res2: List[Int] = List(2, 3, 5, 7, 11, 13, 17, 19, 23, 29)
}