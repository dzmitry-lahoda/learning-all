package common

object lecture_97 {;import org.scalaide.worksheet.runtime.library.WorksheetSupport._; def main(args: Array[String])=$execute{;$skip(138); 
  def factorial(n:Int) : Int  = {
    if (n == 0 || n== 1)
      1
    else
      n*factorial(n-1)
  };System.out.println("""factorial: (n: Int)Int""");$skip(177); val res$0 = 
  
  
/*
prove that factorial(n) >= power(2,n) for n >=4

inductive hypoteses
if factorial(n) >= power(2,n) then factorial(n+1) >= power(2,n+1)

*/

// base case
  factorial(4);System.out.println("""res0: Int = """ + $show(res$0));$skip(312); 
/*
factorial(n+1) =>
 (n+1)* factortial(n) // from definition
 => n*power(2,n) // inductive  hypotese
 => *power(2,n+1)

*/


/*def concat[T](xs: List[T], ys: List[T]): List[T] = xs match {
  case Nil => ys
  case x :: xs1  => x ::  concat(xs1,ys)
 }*/
   // (xs foldRight ys) (_ :: _)
    
def xs = List(1,2,3);System.out.println("""xs: => List[Int]""");$skip(21); 
def ys = List(4,5,6);System.out.println("""ys: => List[Int]""");$skip(21); 
def zs = List(7,8,9);System.out.println("""zs: => List[Int]""");$skip(53); val res$1 = 

// assotiative
(xs ++ ys) ++ zs == xs ++ (ys ++ zs);System.out.println("""res1: Boolean = """ + $show(res$1));$skip(17); val res$2 = 

xs ++ Nil == xs;System.out.println("""res2: Boolean = """ + $show(res$2));$skip(17); val res$3 = 
Nil ++ xs  == xs;System.out.println("""res3: Boolean = """ + $show(res$3));$skip(22); val res$4 = 

xs  == List(1) ++ xs;System.out.println("""res4: Boolean = """ + $show(res$4))}

//concat(xs,ys)

/*


  P(xs) == xs ++ Nil == xs
  
  P(Nil) =  Nill ++ Nil = Nill
  P(x :: xs) =  x :: xs ++ Nil == x :: xs
  x :: (xs ++ Nill) = //case 2
  = x :: xs // by hyp
 
  //TODO: this should be made formal proving base case
  P(xs) = (xs ++ ys) ++ zs == xs ++ (ys ++ zs)
  P(x :: xs) ==
  (x::xs ++ ys) ++ zs =
  x :: ((xs ++ ys) ++ zs) = // case 2
  x :: (xs ++ (ys ++ zs)) = //by hyp
//TODO: really we should took to side and simplify them
  (x :: xs) ++ (ys ++ zs) // x will always be in fron regardless it concatendated to xs or or whole list

*/

}
