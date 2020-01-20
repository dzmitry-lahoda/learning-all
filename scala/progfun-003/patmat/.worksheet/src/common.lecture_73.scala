package common

object lecture_73 {;import org.scalaide.worksheet.runtime.library.WorksheetSupport._; def main(args: Array[String])=$execute{;$skip(205); 

def insert(e: Int,xs: List[Int]): List[Int] = xs match {
     case List() => e :: Nil
     case y :: ys =>
       if (e <= y) e :: xs
       else y :: insert (e,ys)
  };System.out.println("""insert: (e: Int, xs: List[Int])List[Int]""");$skip(121); 
    
  def isort(xs:List[Int]):List[Int]= xs match {
    case List()=>List()
    case y :: ys => insert(y,isort(ys))
  };System.out.println("""isort: (xs: List[Int])List[Int]""");$skip(47); 
  val sorted = isort(7 :: 3 :: 9  :: 2 :: Nil);System.out.println("""sorted  : List[Int] = """ + $show(sorted ));$skip(44); 
  println("Welcome to the Scala worksheet");$skip(52); val res$0 = 
  new Function1[Int,Int] {def apply(x:Int) = x * x};System.out.println("""res0: Int => Int = """ + $show(res$0));$skip(41); 

  val concat = (1 :: Nil) ++ (2 :: Nil);System.out.println("""concat  : List[Int] = """ + $show(concat ));$skip(133); 
  def last[T](xs:List[T]):T = xs match {
    case List() => throw new Error()
    case List(x) => x
    case y :: ys => last(ys)
  };System.out.println("""last: [T](xs: List[T])T""");$skip(146); 
  def init[T](xs:List[T]):List[T] = xs match {
    case List() => throw new Error()
    case List(x) => Nil
    case y :: ys => y :: init(ys)
  };System.out.println("""init: [T](xs: List[T])List[T]""");$skip(251); 
  
  def reverse[T](xs:List[T]):List[T] ={
    def reverse[T](xs:List[T],acc: List[T]):List[T] = xs match {
      case List() => throw new Error()
      case List(x) => x :: acc
      case y :: ys => reverse(ys,y :: acc)
    }
    reverse(xs,Nil)
  };System.out.println("""reverse: [T](xs: List[T])List[T]""");$skip(393); 
  /* xs match {
   
    case List() => throw new Error()
    case List(x) => List(x)
    case y :: ys =>  reverse(ys) ++ List(y)
  }    */
  
  def removeAt[T](n : Int, xs : List[T]):List[T] = xs match {
    case List() => throw new Error()
    case List(x) => n match{
      case 0 => Nil
    }
    case y :: ys => n match {
      case 0 => ys
      case n => y :: removeAt(n-1,ys)
    }
  };System.out.println("""removeAt: [T](n: Int, xs: List[T])List[T]""");$skip(79); 
  def removeAt2[T](n:Int,xs:List[T]):List[T] = (xs take n) ::: (xs drop n + 1);System.out.println("""removeAt2: [T](n: Int, xs: List[T])List[T]""");$skip(40); val res$1 = 
   

 
  reverse(List('a','b','c','d'));System.out.println("""res1: List[Char] = """ + $show(res$1));$skip(36); val res$2 = 
  removeAt(1,List('a','b','c','d'));System.out.println("""res2: List[Char] = """ + $show(res$2));$skip(37); val res$3 = 
  removeAt2(2,List('a','b','c','d'));System.out.println("""res3: List[Char] = """ + $show(res$3));$skip(36); val res$4 = 
  removeAt(3,List('a','b','c','d'));System.out.println("""res4: List[Char] = """ + $show(res$4));$skip(210); 

  def flatten(xs: List[Any]): List[Any] ={
    if (xs.isEmpty)
      xs
    else xs.head match {
      case h : List[Any] => flatten(h) ::: flatten(xs.tail)
      case h:Any => h :: flatten(xs.tail)
    }
  };System.out.println("""flatten: (xs: List[Any])List[Any]""");$skip(17); val res$5 = 

flatten(List());System.out.println("""res5: List[Any] = """ + $show(res$5));$skip(26); val res$6 = 
flatten(List(List(1, 1)));System.out.println("""res6: List[Any] = """ + $show(res$6));$skip(50); val res$7 = 
flatten(List(List(1, 1), 2, List(3, List(5, 8))));System.out.println("""res7: List[Any] = """ + $show(res$7))}
}
