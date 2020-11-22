using System;

checked
{    
    Console.WriteLine((double)0 / (long)0L);
    Console.WriteLine((double)Int64.MaxValue / (long)0L);
    Console.WriteLine((double)Int64.MaxValue / (long)Int64.MinValue);
    Console.WriteLine((double)Int64.MaxValue / Double.Epsilon);
    Console.WriteLine((long)0 / (long)0L);
}
