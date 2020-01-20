package common

object lecture_95 {;import org.scalaide.worksheet.runtime.library.WorksheetSupport._; def main(args: Array[String])=$execute{;$skip(117); 
def concat[T](xs: List[T], ys: List[T]): List[T] =
    (xs foldRight ys) (_ :: _);System.out.println("""concat: [T](xs: List[T], ys: List[T])List[T]""");$skip(43); val res$0 = 
    List(1,2,3) reduceLeft ((x,y)=> x + y);System.out.println("""res0: Int = """ + $show(res$0));$skip(36); val res$1 = 
   List(1,2,3,2) reduceLeft (_ + _);System.out.println("""res1: Int = """ + $show(res$1));$skip(31); val res$2 = 
List(1,2,3).foldRight(-1)(_+_);System.out.println("""res2: Int = """ + $show(res$2));$skip(42); val res$3 = 
   
  
concat ( List(1,2,3) ,List(4,5,6));System.out.println("""res3: List[Int] = """ + $show(res$3));$skip(103); 
 def mapFun[T,U] (xs:List[T], f: T => U):List[U] =
      (xs foldRight List[U]())((x,y)=>  f(x) :: y );System.out.println("""mapFun: [T, U](xs: List[T], f: T => U)List[U]""");$skip(42); val res$4 = 
 
 mapFun[Int,Int](List(1,2,3),x=> x * 2);System.out.println("""res4: List[Int] = """ + $show(res$4));$skip(79); 
 
 def lengthFun[T] (xs:List[T]):Int =
      (xs foldRight 0)((x,y)=>  1 + y );System.out.println("""lengthFun: [T](xs: List[T])Int""");$skip(26); val res$5 = 
 
 lengthFun(List(1,2,3));System.out.println("""res5: Int = """ + $show(res$5))}
  // def mapFun[T,U] (xs:List[T], f: T => U):List[U] =
//      (x foldRight  xs.head)((x,y)=>  f(x) )
}
