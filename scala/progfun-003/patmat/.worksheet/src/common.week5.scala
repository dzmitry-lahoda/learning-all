package common

object week5{;import org.scalaide.worksheet.runtime.library.WorksheetSupport._; def main(args: Array[String])=$execute{;$skip(687); 


  
  def msort[T](xs : List[T])
  // can be < or > and deduced from collection element type by default which is attached to element type
  // implicit searches companion object, in this case Ordering.T mostly
  (implicit ord: math.Ordering[T])
  
  :List[T] = {
   val m = xs.length/2
   if (m == 0) xs
   else{
    def merge(xs: List[T],ys : List[T]): List[T] =   (xs, ys) match {
      case (Nil,ys) => ys
      case (xs,Nil) => xs
      case (x :: xss, y :: yss) =>
        if (ord.lt(x,y))
          x :: merge(xss,ys)
        else
          y :: merge(xs,yss)
    }
     val (fst,snd) = xs splitAt m
     
     merge( msort(fst), msort(snd))
   }
  };System.out.println("""msort: [T](xs: List[T])(implicit ord: scala.math.Ordering[T])List[T]""");$skip(31); val res$0 = 
  msort(List(2,3,-123,5,1,44));System.out.println("""res0: List[Int] = """ + $show(res$0));$skip(33); val res$1 = 
  msort(List(2.2,3,-123,5,1,44));System.out.println("""res1: List[Double] = """ + $show(res$1));$skip(52); 
  
  // tuples are case classes
  val pair = (1,"");System.out.println("""pair  : (Int, String) = """ + $show(pair ));$skip(10); val res$2 = 
  pair._1;System.out.println("""res2: Int = """ + $show(res$2))}
}
