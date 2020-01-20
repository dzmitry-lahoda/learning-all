package common

object week5{


  
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
  }                                               //> msort: [T](xs: List[T])(implicit ord: scala.math.Ordering[T])List[T]
  msort(List(2,3,-123,5,1,44))                    //> res0: List[Int] = List(-123, 1, 2, 3, 5, 44)
  msort(List(2.2,3,-123,5,1,44))                  //> res1: List[Double] = List(-123.0, 1.0, 2.2, 3.0, 5.0, 44.0)
  
  // tuples are case classes
  val pair = (1,"")                               //> pair  : (Int, String) = (1,"")
  pair._1                                         //> res2: Int = 1
}