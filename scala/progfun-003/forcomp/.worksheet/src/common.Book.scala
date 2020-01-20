package common

case class Book(val title:String,val authors:List[String]);

object lecture_111 {
  def xs = List(1,2,3,4)
  for
  {
     x<-xs
     y<- Set(x,5)
  }
  yield y
  
  val books = Set(
    new Book("B11",List("A1","B2"))
  )
  
  for (b <- books; a <- b.authors if a startsWith "B1")
  yield b.title
}
