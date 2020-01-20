package objsets

import org.scalatest.FunSuite

import org.junit.runner.RunWith
import org.scalatest.junit.JUnitRunner

@RunWith(classOf[JUnitRunner])
class TweetSetSuite extends FunSuite {
  trait TestSets {
    val set1 = new  Empty
    val set2 = set1.incl(new Tweet("a", "a body", 20))
    val set3 = set2.incl(new Tweet("b", "b body", 20))
    val c = new Tweet("c", "c body", 7)
    val d = new Tweet("d", "d body", 9)
    val set4c = set3.incl(c)
    val set4d = set3.incl(d)
    val set5 = set4c.incl(d)

  }

  def asSet(tweets: TweetSet): Set[Tweet] = {
    var res = Set[Tweet]()
    tweets.foreach(res += _)
    res
  } 

  def size(set: TweetSet): Int = asSet(set).size

  test("filter: on empty set") {
    new TestSets {
      assert(size(set1.filter(tw => tw.user == "a")) === 0)
    }
  }

   test("filter: a on set5 with 321") {
    new TestSets {
      assert(size(set5.filter(tw => tw.retweets == 321)) === 0)
    }
  }
  
  test("filter: a on set5") {
    new TestSets {
      assert(size(set5.filter(tw => tw.user == "a")) === 1)
    }
  }
  
  test("TweetReader.allTweets"){
      TweetReader.allTweets

}
  
  
  lazy val create:TweetSet =  {
    def create(count:Int,acc:TweetSet):TweetSet =
      if (count < 1000)  create(count + 1, acc.incl(new Tweet(count.toString(), count.toString,count)))
      else acc
    create(1,new Empty)  
  }
  
   test("Big union"){

   create union create
}
    test("Trending"){

    GoogleVsApple.trending
}
 
  
   test("Big filter"){

   create.filter (x=> true)
}

  test("filter: 20 on set5") {
    new TestSets {
      assert(size(set5.filter(tw => tw.retweets == 20)) === 2)
    }
  }

  test("union: set4c and set4d") {
    new TestSets {
      assert(size(set4c.union(set4d)) === 4)
    }
  }

  test("union: with empty set (1)") {
    new TestSets {
      assert(size(set5.union(set1)) === 4)
    }
  }

  test("union: with empty set (2)") {
    new TestSets {
      assert(size(set1.union(set5)) === 4)
    }
  }
 
  test("mostRetweeted: set5") {
     new TestSets{
      val most = set5.mostRetweeted
      assert (most.retweets === 20)
      val lessBy1 = set5.remove(most)
      val lessBy2 = lessBy1.remove(lessBy1.mostRetweeted)
      assert (lessBy2.mostRetweeted.retweets === 9)
   
     }
     }
  test("descending: set5") {
    new TestSets {
      val trends = set5.descendingByRetweet
      assert(!trends.isEmpty)
      assert(trends.head.user == "a" || trends.head.user == "b")
    }
  }
}
