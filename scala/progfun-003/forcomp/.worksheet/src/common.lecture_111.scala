package common



object lecture_111 {
  case class Book(val title:String,val authors:Set[String]);import org.scalaide.worksheet.runtime.library.WorksheetSupport._; def main(args: Array[String])=$execute{;$skip(124); ;
  def xs = List(1,2,3,4);System.out.println("""xs: => List[Int]""");$skip(53); val res$0 = 
  for
  {
     x<-xs
     y<- Set(x,5)
  }
  yield y;System.out.println("""res0: List[Int] = """ + $show(res$0));$skip(133); 
  
  val books = Set(
    new Book("B11",Set("A11","A22")),
    new Book("B22",Set("A33","A22")),
    new Book("B33",Set("A11"))
  );System.out.println("""books  : scala.collection.immutable.Set[common.lecture_111.Book] = """ + $show(books ));$skip(75); val res$1 = 
  
  for (b <- books; a <- b.authors if a startsWith "A1")
  yield b.title;System.out.println("""res1: scala.collection.immutable.Set[String] = """ + $show(res$1));$skip(95); val res$2 = 
  
  books.flatMap( b=>
    b.authors.withFilter(a => a startsWith "A1").map(a=>b.title)
    );System.out.println("""res2: scala.collection.immutable.Set[String] = """ + $show(res$2))}
}
