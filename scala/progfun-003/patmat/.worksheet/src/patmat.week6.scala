package patmat


object week6 {;import org.scalaide.worksheet.runtime.library.WorksheetSupport._; def main(args: Array[String])=$execute{;$skip(203); 

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
  new Function1[Int,Int] {def apply(x:Int) = x * x};System.out.println("""res0: Int => Int = """ + $show(res$0))}
}
