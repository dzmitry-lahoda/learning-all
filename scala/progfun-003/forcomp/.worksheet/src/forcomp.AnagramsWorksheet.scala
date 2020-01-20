package forcomp
import Anagrams._

object AnagramsWorksheet {;import org.scalaide.worksheet.runtime.library.WorksheetSupport._; def main(args: Array[String])=$execute{;$skip(119); val res$0 = 


  for {
   a<- "abc"
   b <-"deg"
  }
  yield List(a,b);System.out.println("""res0: scala.collection.immutable.IndexedSeq[List[Char]] = """ + $show(res$0));$skip(47); 

 val s = sentenceOccurrences(List("iellnru"));System.out.println("""s  : forcomp.Anagrams.Occurrences = """ + $show(s ));$skip(55); 
               
           val combs = combinations(s);System.out.println("""combs  : List[forcomp.Anagrams.Occurrences] = """ + $show(combs ));$skip(60); val res$1 = 
                 combs.contains(wordOccurrences("iellnru"));System.out.println("""res1: Boolean = """ + $show(res$1));$skip(55); val res$2 = 
               combs.contains(wordOccurrences("rule"));System.out.println("""res2: Boolean = """ + $show(res$2));$skip(52); val res$3 = 
              combs.contains(wordOccurrences("ru"));System.out.println("""res3: Boolean = """ + $show(res$3));$skip(66); val res$4 = 
                            combs.contains(wordOccurrences("le"));System.out.println("""res4: Boolean = """ + $show(res$4));$skip(55); val res$5 = 
                combs.contains(wordOccurrences("lin"));System.out.println("""res5: Boolean = """ + $show(res$5));$skip(55); val res$6 = 
               combs.contains(wordOccurrences("linu"));System.out.println("""res6: Boolean = """ + $show(res$6));$skip(58); val res$7 = 
                 combs.contains(wordOccurrences("linur"));System.out.println("""res7: Boolean = """ + $show(res$7));$skip(57); val res$8 = 
                   combs.contains(wordOccurrences("li"));System.out.println("""res8: Boolean = """ + $show(res$8));$skip(59); val res$9 = 
                     combs.contains(wordOccurrences("nu"));System.out.println("""res9: Boolean = """ + $show(res$9))}
                             
               
               
  // val multi =   sentenceAnagrams(s,Nil)
 
 //multi
 //ofMulti(List(nullocc))
 //multi.map(ofMulti)
             // val s1 = List("I", "love")
              //val s2 = List("Linux","Love")
             //val s3 = List("I", "love","you")
               // sentenceAnagrams(s1)
              // sentenceAnagrams(s2)
//sentenceAnagrams(s3)

}
