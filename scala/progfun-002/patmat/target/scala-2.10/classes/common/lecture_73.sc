package common

object lecture_73 {

def insert(e: Int,xs: List[Int]): List[Int] = xs match {
     case List() => e :: Nil
     case y :: ys =>
       if (e <= y) e :: xs
       else y :: insert (e,ys)
  }                                               //> insert: (e: Int, xs: List[Int])List[Int]
    
  def isort(xs:List[Int]):List[Int]= xs match {
    case List()=>List()
    case y :: ys => insert(y,isort(ys))
  }                                               //> isort: (xs: List[Int])List[Int]
  val sorted = isort(7 :: 3 :: 9  :: 2 :: Nil)    //> sorted  : List[Int] = List(2, 3, 7, 9)
  println("Welcome to the Scala worksheet")       //> Welcome to the Scala worksheet
  new Function1[Int,Int] {def apply(x:Int) = x * x}
                                                  //> res0: Int => Int = <function1>

  val concat = (1 :: Nil) ++ (2 :: Nil)           //> concat  : List[Int] = List(1, 2)
  def last[T](xs:List[T]):T = xs match {
    case List() => throw new Error()
    case List(x) => x
    case y :: ys => last(ys)
  }                                               //> last: [T](xs: List[T])T
  def init[T](xs:List[T]):List[T] = xs match {
    case List() => throw new Error()
    case List(x) => Nil
    case y :: ys => y :: init(ys)
  }                                               //> init: [T](xs: List[T])List[T]
  
  def reverse[T](xs:List[T]):List[T] ={
    def reverse[T](xs:List[T],acc: List[T]):List[T] = xs match {
      case List() => throw new Error()
      case List(x) => x :: acc
      case y :: ys => reverse(ys,y :: acc)
    }
    reverse(xs,Nil)
  }                                               //> reverse: [T](xs: List[T])List[T]
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
  }                                               //> removeAt: [T](n: Int, xs: List[T])List[T]
  def removeAt2[T](n:Int,xs:List[T]):List[T] = (xs take n) ::: (xs drop n + 1)
                                                  //> removeAt2: [T](n: Int, xs: List[T])List[T]
   

 
  reverse(List('a','b','c','d'))                  //> res1: List[Char] = List(d, c, b, a)
  removeAt(1,List('a','b','c','d'))               //> res2: List[Char] = List(a, c, d)
  removeAt2(2,List('a','b','c','d'))              //> res3: List[Char] = List(a, b, d)
  removeAt(3,List('a','b','c','d'))               //> res4: List[Char] = List(a, b, c)

  def flatten(xs: List[Any]): List[Any] ={
    if (xs.isEmpty)
      xs
    else xs.head match {
      case h : List[Any] => flatten(h) ::: flatten(xs.tail)
      case h:Any => h :: flatten(xs.tail)
    }
  }                                               //> flatten: (xs: List[Any])List[Any]

flatten(List())                                   //> res5: List[Any] = List()
flatten(List(List(1, 1)))                         //> res6: List[Any] = List(1, 1)
flatten(List(List(1, 1), 2, List(3, List(5, 8)))) //> res7: List[Any] = List(1, 1, 2, 3, 5, 8)
}