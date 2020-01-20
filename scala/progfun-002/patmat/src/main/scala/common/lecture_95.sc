package common

object lecture_95 {
def concat[T](xs: List[T], ys: List[T]): List[T] =
    (xs foldRight ys) (_ :: _)                    //> concat: [T](xs: List[T], ys: List[T])List[T]
    List(1,2,3) reduceLeft ((x,y)=> x + y)        //> res0: Int = 6
   List(1,2,3,2) reduceLeft (_ + _)               //> res1: Int = 8
List(1,2,3).foldRight(-1)(_+_)                    //> res2: Int = 5
   
  
concat ( List(1,2,3) ,List(4,5,6))                //> res3: List[Int] = List(1, 2, 3, 4, 5, 6)
 def mapFun[T,U] (xs:List[T], f: T => U):List[U] =
      (xs foldRight List[U]())((x,y)=>  f(x) :: y )
                                                  //> mapFun: [T, U](xs: List[T], f: T => U)List[U]
 
 mapFun[Int,Int](List(1,2,3),x=> x * 2)           //> res4: List[Int] = List(2, 4, 6)
 
 def lengthFun[T] (xs:List[T]):Int =
      (xs foldRight 0)((x,y)=>  1 + y )           //> lengthFun: [T](xs: List[T])Int
 
 lengthFun(List(1,2,3))                           //> res5: Int = 3
  // def mapFun[T,U] (xs:List[T], f: T => U):List[U] =
//      (x foldRight  xs.head)((x,y)=>  f(x) )
}