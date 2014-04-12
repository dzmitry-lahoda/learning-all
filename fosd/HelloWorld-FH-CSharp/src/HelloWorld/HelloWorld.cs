
public class  HelloWorld {
	

    protected  void print__wrappee__Hello()
    {
        System.Console.WriteLine("Hello");
    } 

    protected  void print__wrappee__Beautiful()
    {
        print__wrappee__Hello();
        System.Console.WriteLine("Beautifull");
    } 

    protected  void print()
    {
        print__wrappee__Beautiful();
        System.Console.WriteLine("World");
    } 

    public static  void Main() {
        new HelloWorld().print();
    }
}
