package greeter



object TestRepl {;import org.scalaide.worksheet.runtime.library.WorksheetSupport._; def main(args: Array[String])=$execute{;$skip(81); 

  println("Welcome to the Scala worksheet");$skip(12); 
  val i = 1;System.out.println("""i  : Int = """ + $show(i ));$skip(30); 
  def increase(i:Int) = i + 1;System.out.println("""increase: (i: Int)Int""");$skip(14); val res$0 = 
  increase(i);System.out.println("""res0: Int = """ + $show(res$0))}
}
