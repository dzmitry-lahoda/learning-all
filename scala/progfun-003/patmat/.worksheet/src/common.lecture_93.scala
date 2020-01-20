package common

object lecture_93 {;import org.scalaide.worksheet.runtime.library.WorksheetSupport._; def main(args: Array[String])=$execute{;$skip(264); 
  
   def group[T,R](xs: List[T])(implicit map : List[T] => R): List[R] = xs match {
    case Nil      => Nil
    case x :: xs1 => {
      val (first,rest) = xs span ( y => x == y)
      map(first) :: group(rest)(map)
    }
   };System.out.println("""group: [T, R](xs: List[T])(implicit map: List[T] => R)List[R]""");$skip(80); 
   def encode[T](xs: List[T]): List[(T,Int)] = group(xs)(x=> (x.head,x.length));System.out.println("""encode: [T](xs: List[T])List[(T, Int)]""");$skip(63); 
   def pack[T](xs: List[T]): List[List[T]] = group(xs)(x => x);System.out.println("""pack: [T](xs: List[T])List[List[T]]""");$skip(58); 
   
   val data = List("a", "a", "a", "b", "c", "c", "a");System.out.println("""data  : List[String] = """ + $show(data ));$skip(34); val res$0 = 
   data takeWhile (x => x == "a");System.out.println("""res0: List[String] = """ + $show(res$0));$skip(29); val res$1 = 
   data span (x => x == "a");System.out.println("""res1: (List[String], List[String]) = """ + $show(res$1));$skip(33); val res$2 = 
   data partition (x=> x == "a");System.out.println("""res2: (List[String], List[String]) = """ + $show(res$2));$skip(14); val res$3 = 
   pack(data);System.out.println("""res3: List[List[String]] = """ + $show(res$3));$skip(42); val res$4 = 
   List(1,2,3) reduceLeft ((x,y)=> x + y);System.out.println("""res4: Int = """ + $show(res$4));$skip(34); val res$5 = 
   List(1,2,3) reduceLeft (_ + _);System.out.println("""res5: Int = """ + $show(res$5));$skip(16); val res$6 = 
   encode(data);System.out.println("""res6: List[(String, Int)] = """ + $show(res$6))}
}
