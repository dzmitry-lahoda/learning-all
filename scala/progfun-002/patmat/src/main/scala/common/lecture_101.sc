package common

object lecture_101 {

Nil :: List(1)                                    //> res0: List[Any] = List(List(), 1)
Nil ++ List(1)                                    //> res1: List[Int] = List(1)
  /*
  Prove
  (xs ++ ys) map f  =  (xs map f) ++ (ys map f)
  
  Given:
  Nil map f = Nil
  
  (x :: xs) map f = f(x) :: (xs map f)
  
  Start:
   Base:
  (Nil ++ ys) map f =
  ys map f
  
  
  (Nil map f) ++ (ys map f) =
  Nil ++ (ys map f)
  
  Start 2:
 ys map f = Nill ++ (ys map f)
 zs = Nill ++ zs
  Nill ++ zs = zs
  
  Base 2:
  Nil ++ Nil =  Nil
   Nill ++ (z :: zs) = z :: zs //case 1
   
   1:
  Nil ++ (ys map f) =  ys map f
   
  
  */
}