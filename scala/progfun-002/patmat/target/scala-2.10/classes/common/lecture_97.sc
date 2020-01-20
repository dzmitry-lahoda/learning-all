package common

object lecture_97 {
  def factorial(n:Int) : Int  = {
    if (n == 0 || n== 1)
      1
    else
      n*factorial(n-1)
  }                                               //> factorial: (n: Int)Int
  
  
/*
prove that factorial(n) >= power(2,n) for n >=4

inductive hypoteses
if factorial(n) >= power(2,n) then factorial(n+1) >= power(2,n+1)

*/

// base case
  factorial(4)                                    //> res0: Int = 24
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
    
def xs = List(1,2,3)                              //> xs: => List[Int]
def ys = List(4,5,6)                              //> ys: => List[Int]
def zs = List(7,8,9)                              //> zs: => List[Int]

// assotiative
(xs ++ ys) ++ zs == xs ++ (ys ++ zs)              //> res1: Boolean = true

xs ++ Nil == xs                                   //> res2: Boolean = true
Nil ++ xs  == xs                                  //> res3: Boolean = true

xs  == List(1) ++ xs                              //> res4: Boolean = false

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