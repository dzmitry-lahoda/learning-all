package common

object lecture_115 {;import org.scalaide.worksheet.runtime.library.WorksheetSupport._; def main(args: Array[String])=$execute{;$skip(88); 
 def l: List[List[Int]] = List(List(1,2),List(2,3));System.out.println("""l: => List[List[Int]]""");$skip(47); 
 def lll = List(List(1),List(2,3),List(4,5,6));System.out.println("""lll: => List[List[Int]]""");$skip(30); 
 def ln:List[List[Int]] = Nil;System.out.println("""ln: => List[List[Int]]""");$skip(38); val res$0 = 
 lll.foldLeft(ln)((a,b) => b :: a   );System.out.println("""res0: List[List[Int]] = """ + $show(res$0));$skip(40); 
  
  val x = Map('a'->1,'b'-> 2,'c'->3);System.out.println("""x  : scala.collection.immutable.Map[Char,Int] = """ + $show(x ));$skip(49); 
  val y = Map('b'->1,'c'->3).withDefaultValue(0);System.out.println("""y  : scala.collection.immutable.Map[Char,Int] = """ + $show(y ));$skip(67); 
  val xmy = x.withDefaultValue(0).map(p => (p._1, p._2- y(p._1) ));System.out.println("""xmy  : scala.collection.immutable.Map[Char,Int] = """ + $show(xmy ));$skip(28); val res$1 = 
  xmy.filter(p => p._2 > 0);System.out.println("""res1: scala.collection.immutable.Map[Char,Int] = """ + $show(res$1));$skip(34); val res$2 = 
  "ffabbccd" groupBy (chr => chr);System.out.println("""res2: scala.collection.immutable.Map[Char,String] = """ + $show(res$2));$skip(39); 
  val abba =  List(('a', 2), ('b', 2));System.out.println("""abba  : List[(Char, Int)] = """ + $show(abba ));$skip(34); 

val aabb = List('a','a','b','b');System.out.println("""aabb  : List[Char] = """ + $show(aabb ));$skip(29); 
 val abb = List('a','b','b')
   type Occurrences = List[(Char, Int)];System.out.println("""abb  : List[Char] = """ + $show(abb ));$skip(260); 
 def recombine(prev :List[Char],c:Char,next :List[Char]): List[List[Char]] ={
    if (next == Nil)
      List(List(c))
    else
      recombine(next.head :: prev , c,next.tail) ::: List(c :: next) ::: List(c :: prev)
 };System.out.println("""recombine: (prev: List[Char], c: Char, next: List[Char])List[List[Char]]""");$skip(217); 
 
 def flat_combine(occurrences: List[Char]): List[List[Char]] = {
    if (occurrences == Nil)
      List(List())
    else
      recombine(Nil,occurrences.head,occurrences.tail) ::: flat_combine(occurrences.tail)
  };System.out.println("""flat_combine: (occurrences: List[Char])List[List[Char]]""");$skip(30); val res$3 = 
  flat_combine(aabb).distinct;System.out.println("""res3: List[List[Char]] = """ + $show(res$3));$skip(24); val res$4 = 

recombine(Nil,'a',abb);System.out.println("""res4: List[List[Char]] = """ + $show(res$4));$skip(400); 

 def combinations(occurrences: Occurrences): List[List[(Char,Int)]] = {
    if (occurrences == Nil) List()
    else{
      val combined = flat_combine(for (   (char,count) <- occurrences; repeat <- 1 to count) yield (char)).distinct
      def occurs(w :List[Char]): List[(Char,Int)] = w.groupBy (chr => chr). map ( g => (g._1,g._2.length)).toList.sorted
      combined.map(f => occurs(f))
    }
  };System.out.println("""combinations: (occurrences: common.lecture_115.Occurrences)List[List[(Char, Int)]]""");$skip(21); val res$5 = 
  combinations(abba);System.out.println("""res5: List[List[(Char, Int)]] = """ + $show(res$5))}
}
