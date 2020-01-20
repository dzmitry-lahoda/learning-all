package patmat

object week7 {;import org.scalaide.worksheet.runtime.library.WorksheetSupport._; def main(args: Array[String])=$execute{;$skip(70); 
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
  };System.out.println("""removeAt: [T](n: Int, xs: List[T])List[T]""");$skip(174); val res$0 = 
/*def flatten(xs: List[Any]): List[Any] = ???
flatten(List(List(1, 1), 2, List(3, List(5, 8))))
> res0: List[Any] = List(1, 1, 2, 3, 5, 8)*/
  reverse(List('a','b','c','d'));System.out.println("""res0: List[Char] = """ + $show(res$0));$skip(36); val res$1 = 
  removeAt(1,List('a','b','c','d'));System.out.println("""res1: List[Char] = """ + $show(res$1));$skip(36); val res$2 = 
  removeAt(3,List('a','b','c','d'));System.out.println("""res2: List[Char] = """ + $show(res$2));$skip(440); 
  def msort[T](xs : List[T])(implicit ord: math.Ordering[T]):List[T] = {
   val m = xs.length/2
   if (m == 0) xs
   else{
    def merge(xs: List[T],ys : List[T]): List[T] =   (xs, ys) match {
      case (Nil,ys) => ys
      case (xs,Nil) => xs
      case (x :: xss, y :: yss) =>
        if (ord.lt(x,y)) x :: merge(xss,ys) else y :: merge(xs,yss)
    }
     val (fst,snd) = xs.splitAt(m)
     
     merge( msort(fst), msort(snd))
   }
  };System.out.println("""msort: [T](xs: List[T])(implicit ord: scala.math.Ordering[T])List[T]""");$skip(31); val res$3 = 
  msort(List(2,3,-123,5,1,44));System.out.println("""res3: List[Int] = """ + $show(res$3));$skip(233); 
  
   
   def group[T,R](xs: List[T])(implicit map : List[T] => R): List[R] = xs match {
    case Nil      => Nil
    case x :: xs1 => {
      val (first,rest) = xs span ( y => x == y)
      map(first) :: group(rest)(map)
    }
   };System.out.println("""group: [T, R](xs: List[T])(implicit map: List[T] => R)List[R]""");$skip(83); 
   def runencode[T](xs: List[T]): List[(T,Int)] = group(xs)(x=> (x.head,x.length));System.out.println("""runencode: [T](xs: List[T])List[(T, Int)]""");$skip(63); 
   def pack[T](xs: List[T]): List[List[T]] = group(xs)(x => x);System.out.println("""pack: [T](xs: List[T])List[List[T]]""");$skip(58); 
   
   val data = List("a", "a", "a", "b", "c", "c", "a");System.out.println("""data  : List[String] = """ + $show(data ));$skip(14); val res$4 = 
   pack(data);System.out.println("""res4: List[List[String]] = """ + $show(res$4));$skip(19); val res$5 = 
   runencode(data);System.out.println("""res5: List[(String, Int)] = """ + $show(res$5))}
}
