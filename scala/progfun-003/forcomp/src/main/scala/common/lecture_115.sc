package common

object lecture_115 {
 def l: List[List[Int]] = List(List(1,2),List(2,3))
                                                  //> l: => List[List[Int]]
 def lll = List(List(1),List(2,3),List(4,5,6))    //> lll: => List[List[Int]]
 def ln:List[List[Int]] = Nil                     //> ln: => List[List[Int]]
 lll.foldLeft(ln)((a,b) => b :: a   )             //> res0: List[List[Int]] = List(List(4, 5, 6), List(2, 3), List(1))
  
  val x = Map('a'->1,'b'-> 2,'c'->3)              //> x  : scala.collection.immutable.Map[Char,Int] = Map(a -> 1, b -> 2, c -> 3)
  val y = Map('b'->1,'c'->3).withDefaultValue(0)  //> y  : scala.collection.immutable.Map[Char,Int] = Map(b -> 1, c -> 3)
  val xmy = x.withDefaultValue(0).map(p => (p._1, p._2- y(p._1) ))
                                                  //> xmy  : scala.collection.immutable.Map[Char,Int] = Map(a -> 1, b -> 1, c -> 0
                                                  //| )
  xmy.filter(p => p._2 > 0)                       //> res1: scala.collection.immutable.Map[Char,Int] = Map(a -> 1, b -> 1)
  "ffabbccd" groupBy (chr => chr)                 //> res2: scala.collection.immutable.Map[Char,String] = Map(f -> ff, a -> a, b -
                                                  //| > bb, c -> cc, d -> d)
  val abba =  List(('a', 2), ('b', 2))            //> abba  : List[(Char, Int)] = List((a,2), (b,2))

val aabb = List('a','a','b','b')                  //> aabb  : List[Char] = List(a, a, b, b)
 val abb = List('a','b','b')                      //> abb  : List[Char] = List(a, b, b)
   type Occurrences = List[(Char, Int)]
 def recombine(prev :List[Char],c:Char,next :List[Char]): List[List[Char]] ={
    if (next == Nil)
      List(List(c))
    else
      recombine(next.head :: prev , c,next.tail) ::: List(c :: next) ::: List(c :: prev)
 }                                                //> recombine: (prev: List[Char], c: Char, next: List[Char])List[List[Char]]
 
 def flat_combine(occurrences: List[Char]): List[List[Char]] = {
    if (occurrences == Nil)
      List(List())
    else
      recombine(Nil,occurrences.head,occurrences.tail) ::: flat_combine(occurrences.tail)
  }                                               //> flat_combine: (occurrences: List[Char])List[List[Char]]
  flat_combine(aabb).distinct                     //> res3: List[List[Char]] = List(List(a), List(a, b), List(a, b, a), List(a, b
                                                  //| , b), List(a, a), List(a, a, b, b), List(b), List(b, b), List())

recombine(Nil,'a',abb)                            //> res4: List[List[Char]] = List(List(a), List(a, b), List(a, b, a), List(a, b
                                                  //| , b), List(a, a), List(a, a, b, b), List(a))

 def combinations(occurrences: Occurrences): List[List[(Char,Int)]] = {
    if (occurrences == Nil) List()
    else{
      val combined = flat_combine(for (   (char,count) <- occurrences; repeat <- 1 to count) yield (char)).distinct
      def occurs(w :List[Char]): List[(Char,Int)] = w.groupBy (chr => chr). map ( g => (g._1,g._2.length)).toList.sorted
      combined.map(f => occurs(f))
    }
  }                                               //> combinations: (occurrences: common.lecture_115.Occurrences)List[List[(Char,
                                                  //|  Int)]]
  combinations(abba)                              //> res5: List[List[(Char, Int)]] = List(List((a,1)), List((a,1), (b,1)), List(
                                                  //| (a,2), (b,1)), List((a,1), (b,2)), List((a,2)), List((a,2), (b,2)), List((b
                                                  //| ,1)), List((b,2)), List())
}