using System;

public class closures
{

	private static void Do(string[] args)
	{
		int a = 0;
		var array = new int[4];

		Action<int> each = GetLambda();
		Array.ForEach(array, each);

		Action<int> each2 = GetLambda2();
		Array.ForEach(array, each2);

		Console.WriteLine("Outside =" + a);
		Array.ForEach(array, f => a++);
		Console.WriteLine("Outside =" + a);
		Console.ReadLine();
	}

	private static Action<int> GetLambda()
	{
		int a = 0;
		return f => { a++; Console.WriteLine("In lambda =" + a); Do(ref a); };
	}

	private static Action<int> GetLambda2()
	{
		int a = 0;
		return f => { a++; Console.WriteLine("In lambda 2 = " + a); };
	}

	private static void Do(ref int k)
	{
		Console.WriteLine("Inside = " + k);
	}
}