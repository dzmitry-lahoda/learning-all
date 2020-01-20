package forcomp

import org.scalatest.FunSuite

import org.junit.runner.RunWith
import org.scalatest.junit.JUnitRunner

import Anagrams._

@RunWith(classOf[JUnitRunner])
class AnagramsSuite extends FunSuite {


  
  test("wordOccurrences: abcd") {
    assert(wordOccurrences("abcd") === List(('a', 1), ('b', 1), ('c', 1), ('d', 1)))
  }

  test("wordOccurrences: Robert") {
    assert(wordOccurrences("Robert") === List(('b', 1), ('e', 1), ('o', 1), ('r', 2), ('t', 1)))
  }

  test("wordOccurrences: able") {
    assert(wordOccurrences("able") === List(('a',1), ('b',1), ('e',1), ('l',1)))
  }
  
  test("wordOccurrences: ablbe") {
    assert(wordOccurrences("ablbe") === List(('a',1), ('b',2), ('e',1), ('l',1)))
  }

  
    test("wordOccurrences: abble") {
    assert(wordOccurrences("abble") === List(('a',1), ('b',2), ('e',1), ('l',1)))
  }


  test("sentenceOccurrences: abcd e") {
    assert(sentenceOccurrences(List("abcd", "e")) === List(('a', 1), ('b', 1), ('c', 1), ('d', 1), ('e', 1)))
  }



  test("dictionaryByOccurrences.get: eat") {
    assert(dictionaryByOccurrences.get(List(('a', 1), ('e', 1), ('t', 1))).map(_.toSet) === Some(Set("ate", "eat", "tea")))
  }



  test("word anagrams: married") {
    assert(wordAnagrams("married").toSet === Set("married", "admirer"))
  }

  test("word anagrams: player") {
    assert(wordAnagrams("player").toSet === Set("parley", "pearly", "player", "replay"))
  }



  test("subtract: lard - r") {
    val lard = List(('a', 1), ('d', 1), ('l', 1), ('r', 1))
    val r = List(('r', 1))
    val lad = List(('a', 1), ('d', 1), ('l', 1))
    assert(subtract(lard, r) === lad)
  }



  test("combinations: []") {
    assert(combinations(Nil) === List(Nil))
  }

  test("combinations word: lzlin") {
     val s = sentenceOccurrences(List("lzlin"))
     val w1 = wordOccurrences("lz")
     val w2 = wordOccurrences("lin")

    val combs = combinations(s);   
     
     
    assert(combs.contains(w1),combs +" does not contains " + w1)
    assert(combs.contains(w2),combs +" does not contains " + w2)
  }
  
  
      test("combinations word: lzin") {
     val s = sentenceOccurrences(List("lzin"))
     val w1 = wordOccurrences("lz")
     val w2 = wordOccurrences("lin")

    val combs = combinations(s);   
     
     
    assert(combs.contains(w1),combs +" does not contains " + w1)
    assert(combs.contains(w2),combs +" does not contains " + w2)
  }
  
    test("combinations word: llin") {
     val s = sentenceOccurrences(List("llin"))
     val w1 = wordOccurrences("l")
     val w2 = wordOccurrences("lin")

    val combs = combinations(s);        
     
    assert(combs.contains(w1),combs +" does not contains " + w1)
    assert(combs.contains(w2),combs +" does not contains " + w2)
  }
    
     test("combinations:  illnru") {  
 val s = wordOccurrences("illnru")
     val w1 = wordOccurrences("ilnu")
     val w2 = wordOccurrences("lru")
     
  val combs = combinations(s);        
     assert(combs.contains(w2),combs +" does not contains " + w2)     
    assert(combs.contains(w1),combs +" does not contains " + w1)     
 }
    
              test("combinations:  izlnru") {  
 val s = wordOccurrences("izlnru")
     val w1 = wordOccurrences("ilnu")
     val w2 = wordOccurrences("lru")
     
  val combs = combinations(s);        
     assert(combs.contains(w2),combs +" does not contains " + w2)     
    assert(combs.contains(w1),combs +" does not contains " + w1)     
 }
                 test("combinations:  ieelknru") {  
 val s = wordOccurrences("ieelknru")
     val w1 = wordOccurrences("ilnu")
     val w2 = wordOccurrences("lru")
     
  val combs = combinations(s);        
     assert(combs.contains(w2),combs +" does not contains " + w2)     
    assert(combs.contains(w1),combs +" does not contains " + w1)     
 }
              
   test("combinations:  ieelnru") {  
 val s = wordOccurrences("ieelnru")
     val w1 = wordOccurrences("ilnu")
     val w2 = wordOccurrences("lru")
     
  val combs = combinations(s);        
     assert(combs.contains(w2),combs +" does not contains " + w2)     
    assert(combs.contains(w1),combs +" does not contains " + w1)     
 }
                                      
              
              
                           test("combinations:  izzlnru") {  
 val s = wordOccurrences("izzlnru")
     val w1 = wordOccurrences("ilnu")
     val w2 = wordOccurrences("lru")
     
  val combs = combinations(s);        
     assert(combs.contains(w2),combs +" does not contains " + w2)     
    assert(combs.contains(w1),combs +" does not contains " + w1)     
 }
                           
     
         test("combinations:  izllnru") {  
 val s = wordOccurrences("izllnru")
     val w1 = wordOccurrences("ilnu")
     val w2 = wordOccurrences("lru")
     
  val combs = combinations(s);        
     assert(combs.contains(w2),combs +" does not contains " + w2)     
    assert(combs.contains(w1),combs +" does not contains " + w1)     
 }
         
  test("combinations:  iellnu") {  
 val s = wordOccurrences("iellnu")
     val w1 = wordOccurrences("ilnu")
     
     
  val combs = combinations(s);        
          
    assert(combs.contains(w1),combs +" does not contains " + w1)     
 }
         
     test("combinations:  iellnru") {  
 val s = wordOccurrences("iellnru")
     val w1 = wordOccurrences("ilnu")
     val w2 = wordOccurrences("lru")
     
  val combs = combinations(s);        
     assert(combs.contains(w2),combs +" does not contains " + w2)     
    assert(combs.contains(w1),combs +" does not contains " + w1)     
 }
    
 
         test("combinations word: a") {
     val s = sentenceOccurrences(List("a"))
     val w1 = wordOccurrences("a")
     
    val combs = combinations(s);   
     
   val abcombs= List(
      List(),
      List(('a', 1))      
    )
    assert(combs.toSet === abcombs.toSet)
  }
     
    test("combinations word: ab") {
     val s = sentenceOccurrences(List("ab"))
     val w1 = wordOccurrences("ab")
     
    val combs = combinations(s);   
     
   val abcombs= List(
      List(),
      List(('a', 1)),
      List(('b', 1)),
      List(('a', 1),('b', 1))
    )
    assert(combs.toSet === abcombs.toSet)
  }
  
   
               
  
  test("combinations word: abba") {
    val abba = wordOccurrences("abba")
    val ab = wordOccurrences("ab")
    val abb = wordOccurrences("abb")
    val combs = combinations(abba);
    
    assert(combinations(abba).contains(ab))
    assert(combinations(abba).contains(abb))
  }

    test("combinations: aaa") {
    val aaa = List(('a', 3))
    val aaacomb = List(
      List(),
      List(('a', 1)),
      List(('a', 2)),
      List(('a', 3))

    )
    assert(combinations(aaa).toSet === aaacomb.toSet)
  }
    
        test("combinations: aaaa") {
    val aaaa = List(('a', 4))
    val aaaacomb = List(
      List(),
      List(('a', 1)),
      List(('a', 2)),
      List(('a', 3)),
      List(('a', 4))

    )
    assert(combinations(aaaa).toSet === aaaacomb.toSet)
  }
    
  test("combinations: abba") {
    val abba = List(('a', 2), ('b', 2))
    val abbacomb = List(
      List(),
      List(('a', 1)),
      List(('a', 2)),
      List(('b', 1)),
      List(('a', 1), ('b', 1)),
      List(('a', 2), ('b', 1)),
      List(('b', 2)),
      List(('a', 1), ('b', 2)),
      List(('a', 2), ('b', 2))
    )
    assert(combinations(abba).toSet === abbacomb.toSet)
  }



  test("sentence anagrams: Abba able") {
    val a = sentenceAnagrams(List("Abba","able"))
    assert(a.contains(List("be", "Abba", "Al")))
    assert(a.contains(List("Abba", "be", "Al")))
    assert(a.contains(List("Abba", "Abel")))
    assert(a.contains(List("Abba", "able")))
    assert(a.contains(List("Abba", "bale")))
    assert(a.contains(List("Abba", "Bela")))
    assert(a.contains(List("Al", "be", "Abba")))  
    assert(a.contains(List("Abba", "Elba")))
     // List(be, Al, Abba), List(Abel, Abba), List(able, Abba ),  List(bale, Abba), List(Bela, Abba), List(Elba, Abba), List(Al, be, Abba), List(Abba, Al, be), List(Al, Abba, be)
  }
  
 test("sentence anagrams occurences: Linux rulez") {  
 val s = sentenceOccurrences(List("Linux","rulez"))
 val multi =   sentenceAnagrams(s,Nil)  
   assert(multi.length > 0)
 }


  test("sentence anagrams: []") {
    val sentence = List()
    assert(sentenceAnagrams(sentence) === List(Nil))
  }

  test("sentence anagrams: Linux rulez") {
    val sentence = List("Linux", "rulez")
    val anas = List(
      List("Rex", "Lin", "Zulu"),
      List("nil", "Zulu", "Rex"),
      List("Rex", "nil", "Zulu"),
      List("Zulu", "Rex", "Lin"),
      List("null", "Uzi", "Rex"),
      List("Rex", "Zulu", "Lin"),
      List("Uzi", "null", "Rex"),
      List("Rex", "null", "Uzi"),
      List("null", "Rex", "Uzi"),
      List("Lin", "Rex", "Zulu"),
      List("nil", "Rex", "Zulu"),
      List("Rex", "Uzi", "null"),
      List("Rex", "Zulu", "nil"),
      List("Zulu", "Rex", "nil"),
      List("Zulu", "Lin", "Rex"),
      List("Lin", "Zulu", "Rex"),
      List("Uzi", "Rex", "null"),
      List("Zulu", "nil", "Rex"),
      List("rulez", "Linux"),
      List("Linux", "rulez")
    )
    assert(sentenceAnagrams(sentence).toSet === anas.toSet)
  }  

  
}
