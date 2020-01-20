package common

object lecture_123 {;import org.scalaide.worksheet.runtime.library.WorksheetSupport._; def main(args: Array[String])=$execute{;$skip(86); 
  def from(n:Int) : Stream[Int] = n #:: from(n+1);System.out.println("""from: (n: Int)Stream[Int]""");$skip(27); val res$0 = 
  from(4) take (10) toList;System.out.println("""res0: List[Int] = """ + $show(res$0));$skip(98); 
  
  def sieve(s:Stream[Int]):Stream[Int] =
    s.head #:: sieve(s.tail filter (_ % s.head != 0));System.out.println("""sieve: (s: Stream[Int])Stream[Int]""");$skip(24); 
  lazy val r = (1 to 5);System.out.println("""r: => scala.collection.immutable.Range.Inclusive""");$skip(34); val res$1 = 
  Map() ++ (r map (x=>  (x, x) ));System.out.println("""res1: scala.collection.immutable.Map[Int,Int] = """ + $show(res$1));$skip(44); val res$2 = 
         
  sieve(from(2)) take (10) toList;System.out.println("""res2: List[Int] = """ + $show(res$2))}
}
