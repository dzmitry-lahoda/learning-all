package common



object lecture_111 {
  case class Book(val title:String,val authors:Set[String]);
  def xs = List(1,2,3,4)                          //> xs: => List[Int]
  for
  {
     x<-xs
     y<- Set(x,5)
  }
  yield y                                         //> res0: List[Int] = List(1, 5, 2, 5, 3, 5, 4, 5)
  
  val books = Set(
    new Book("B11",Set("A11","A22")),
    new Book("B22",Set("A33","A22")),
    new Book("B33",Set("A11"))
  )                                               //> books  : scala.collection.immutable.Set[common.lecture_111.Book] = Set(Book(
                                                  //| B11,Set(A11, A22)), Book(B22,Set(A33, A22)), Book(B33,Set(A11)))
  
  for (b <- books; a <- b.authors if a startsWith "A1")
  yield b.title                                   //> res1: scala.collection.immutable.Set[String] = Set(B11, B33)
  
  books.flatMap( b=>
    b.authors.withFilter(a => a startsWith "A1").map(a=>b.title)
    )                                             //> res2: scala.collection.immutable.Set[String] = Set(B11, B33)
}