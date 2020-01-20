package week4


object List {
    def apply[T](x1:T,x2:T):List[T] = new Cons(x1,new Cons(x2, Nil))
    def !![T](elem:T) = new Cons[T](elem, Nil)
    def !![T](elem:T,tail:List[T]) = new Cons[T](elem,tail)
}

trait List [+T] {
  def isEmpty : Boolean
  def head:T
  def tail:List[T]
  def prepend[S >: T ] (elem:S) :List[S] = new Cons(elem,this)
}

class Cons[T](val head : T,val tail : List[T]) extends List[T]{
  def isEmpty = false 
  def index(n:Int)  = {
    def index(a:Int,list:List[T]):T =
      if (list.isEmpty) throw new IndexOutOfBoundsException() 
      else if (n == a) list.head
      else index(a+1,list.tail)        
    index(0,this)
  }    
}

object Nil extends List[Nothing]{
  def isEmpty = true
  def head = throw new NoSuchElementException()
  def tail = throw new NoSuchElementException()
  def index(n:Int)  = throw new IndexOutOfBoundsException() 
  def :::[T](elem:T) = new Cons[T](elem, Nil)  
}