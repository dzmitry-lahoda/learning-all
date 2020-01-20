package common

object lecture_107 {;import org.scalaide.worksheet.runtime.library.WorksheetSupport._; def main(args: Array[String])=$execute{;$skip(173); 

  def calcCoords(queens: List[Int]): Seq[(Int, Int)] =
    {
      var row = queens.length - 1
      (row to 0 by -1).zip(queens)
    };System.out.println("""calcCoords: (queens: List[Int])Seq[(Int, Int)]""");$skip(197); 
  def isSafeDiagonal(col: Int, queens: List[Int]): Boolean = {
    var row = queens.length
    var coords = calcCoords(queens)
    coords.forall { case (r, c) => row - r != Math.abs(col - c) }
  };System.out.println("""isSafeDiagonal: (col: Int, queens: List[Int])Boolean""");$skip(463); 

  def queens(n: Int): Set[List[Int]] = {

    def isSafe(col: Int, queens: List[Int]): Boolean = {
      if (queens contains col) false
      else if (isSafeDiagonal(col, queens)) true
      else false
    }

    def placeQueens(k: Int): Set[List[Int]] =
      if (k == 0) Set(List())
      else
        for {
          queens <- placeQueens(k - 1)
          col <- 0 until n
          if isSafe(col, queens)
        } yield col :: queens
    placeQueens(n)
  };System.out.println("""queens: (n: Int)Set[List[Int]]""");$skip(23); val res$0 = 

  calcCoords(List(0));System.out.println("""res0: Seq[(Int, Int)] = """ + $show(res$0));$skip(22); val res$1 = 
  calcCoords(List(1));System.out.println("""res1: Seq[(Int, Int)] = """ + $show(res$1));$skip(25); val res$2 = 
  calcCoords(List(2, 3));System.out.println("""res2: Seq[(Int, Int)] = """ + $show(res$2));$skip(28); val res$3 = 
  calcCoords(List(2, 3, 4));System.out.println("""res3: Seq[(Int, Int)] = """ + $show(res$3));$skip(25); val res$4 = 
  calcCoords(List(1, 0));System.out.println("""res4: Seq[(Int, Int)] = """ + $show(res$4));$skip(30); val res$5 = 

  isSafeDiagonal(1, List(0));System.out.println("""res5: Boolean = """ + $show(res$5));$skip(29); val res$6 = 
  isSafeDiagonal(1, List(1));System.out.println("""res6: Boolean = """ + $show(res$6));$skip(32); val res$7 = 
  isSafeDiagonal(2, List(2, 2));System.out.println("""res7: Boolean = """ + $show(res$7));$skip(32); val res$8 = 
  isSafeDiagonal(1, List(2, 2));System.out.println("""res8: Boolean = """ + $show(res$8));$skip(38); val res$9 = 
  isSafeDiagonal(3, List(0, 2, 2, 0));System.out.println("""res9: Boolean = """ + $show(res$9));$skip(32); val res$10 = 
  isSafeDiagonal(1, List(1, 0));System.out.println("""res10: Boolean = """ + $show(res$10));$skip(13); val res$11 = 

  queens(2);System.out.println("""res11: Set[List[Int]] = """ + $show(res$11));$skip(12); val res$12 = 
  queens(3);System.out.println("""res12: Set[List[Int]] = """ + $show(res$12));$skip(12); val res$13 = 
  queens(4);System.out.println("""res13: Set[List[Int]] = """ + $show(res$13));$skip(12); val res$14 = 
  queens(5);System.out.println("""res14: Set[List[Int]] = """ + $show(res$14));$skip(12); val res$15 = 
  queens(6);System.out.println("""res15: Set[List[Int]] = """ + $show(res$15))}

}
