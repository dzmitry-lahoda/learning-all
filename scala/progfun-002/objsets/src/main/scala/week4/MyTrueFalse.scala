package week4

object MyTrue  extends MyBoolean {
    def ifThenElse[T] (t: => T, e: => T):T = t
}

object MyFalse extends MyBoolean{
    def ifThenElse[T] (t: => T, e: => T):T = e
}