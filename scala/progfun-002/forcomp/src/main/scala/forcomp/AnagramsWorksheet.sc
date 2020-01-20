package forcomp
import Anagrams._

object AnagramsWorksheet {


  for {
   a<- "abc"
   b <-"deg"
  }
  yield List(a,b)                                 //> res0: scala.collection.immutable.IndexedSeq[List[Char]] = Vector(List(a, d),
                                                  //|  List(a, e), List(a, g), List(b, d), List(b, e), List(b, g), List(c, d), Lis
                                                  //| t(c, e), List(c, g))

 val s = sentenceOccurrences(List("iellnru"))     //> s  : forcomp.Anagrams.Occurrences = List((e,1), (i,1), (l,2), (n,1), (r,1), 
                                                  //| (u,1))
               
           val combs = combinations(s)            //> combs  : List[forcomp.Anagrams.Occurrences] = List(List(), List((e,1), (i,1)
                                                  //| , (l,2), (n,1), (r,1)), List((u,1)), List((e,1), (i,1), (l,2), (n,1), (r,1),
                                                  //|  (u,1)), List((i,1), (l,2), (n,1), (r,1), (u,1)), List((l,2), (n,1), (r,1), 
                                                  //| (u,1)), List((l,1), (n,1), (r,1), (u,1)), List((n,1), (r,1), (u,1)), List((r
                                                  //| ,1), (u,1)), List((e,1), (u,1)), List((e,1), (i,1), (u,1)), List((e,1), (i,1
                                                  //| ), (l,1), (u,1)), List((e,1), (i,1), (l,2), (u,1)), List((e,1), (i,1), (l,2)
                                                  //| , (n,1), (u,1)), List((e,1), (i,1), (l,2), (n,1), (r,1), (u,1)), List(), Lis
                                                  //| t((e,1), (i,1), (l,2), (n,1), (r,1)), List((i,1), (l,2), (n,1), (r,1)), List
                                                  //| ((l,2), (n,1), (r,1)), List((l,1), (n,1), (r,1)), List((n,1), (r,1)), List((
                                                  //| r,1)), List((e,1)), List((e,1), (i,1)), List((e,1), (i,1), (l,1)), List((e,1
                                                  //| ), (i,1), (l,2)), List((e,1), (i,1), (l,2), (n,1)), List((e,1), (r,1), (u,1)
                                                  //| ), List((e,1), (i,1), (r,1), (u,1)), List((e,1), (i,1), (l,1), (r,1), (u,1))
                                                  //| , List((e,1), (i,1), (l,
                                                  //| Output exceeds cutoff limit.
                 combs.contains(wordOccurrences("iellnru"))
                                                  //> res1: Boolean = true
               combs.contains(wordOccurrences("rule"))
                                                  //> res2: Boolean = true
              combs.contains(wordOccurrences("ru"))
                                                  //> res3: Boolean = true
                            combs.contains(wordOccurrences("le"))
                                                  //> res4: Boolean = true
                combs.contains(wordOccurrences("lin"))
                                                  //> res5: Boolean = true
               combs.contains(wordOccurrences("linu"))
                                                  //> res6: Boolean = false
                 combs.contains(wordOccurrences("linur"))
                                                  //> res7: Boolean = true
                   combs.contains(wordOccurrences("li"))
                                                  //> res8: Boolean = true
                     combs.contains(wordOccurrences("nu"))
                                                  //> res9: Boolean = true
                             
               
               
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