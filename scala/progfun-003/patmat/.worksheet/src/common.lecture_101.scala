package common

object lecture_101 {;import org.scalaide.worksheet.runtime.library.WorksheetSupport._; def main(args: Array[String])=$execute{;$skip(52); val res$0 = 

Nil :: List(1);System.out.println("""res0: List[Any] = """ + $show(res$0));$skip(15); val res$1 = 
Nil ++ List(1);System.out.println("""res1: List[Int] = """ + $show(res$1))}
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
