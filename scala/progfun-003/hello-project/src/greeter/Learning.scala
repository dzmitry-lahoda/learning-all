package greeter



object Learning {
  def recpower (x:Double,p:Int) : Double = if (p == 1) x else x*recpower(x,p-1)
  def and(x: Boolean,y: Boolean) = if (x) y else false
  

  }
