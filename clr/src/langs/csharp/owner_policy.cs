using System;
using System.Threading;
using System.Threading.Tasks;

public class DBContext1{
    internal System.SByte counter;
    public string[] data 
    {
        get {
            if (counter < 0) throw new Exception("FAILS FAST");
            // could use https://docs.microsoft.com/en-us/dotnet/api/system.threading.asynclocal-1?view=netcore-3.1
            // to pass ownership from one thread to another if needed
            return new []{"a", "b"};}
        }
}

public ref struct DBAccess1<T>{
    private readonly DBContext1 ef;
    public DBAccess1(Func<DBContext1, T> tx, DBContext1 ef) {
        this.ef = ef;
        Execute = () => {
            ef.counter++;
            var r=  tx(ef);
            ef.counter = -1;
            return r;
        };
    }
    public readonly Func<T> Execute;
}



public class Services1 {
    public DBAccess1<T> DB<T>(Func<DBContext1, T> transaction) => new DBAccess1<T>(transaction, new DBContext1()); 
}

// public class Services2 {
//     // ref structs cannot be type args
//     public DBAccess2<T> DB<T>(Func<DBContext2, T> transaction) => new DBAccess2<T>(transaction); 
// }


// public ref struct DBContext2{
    
//     public DBContext2(DBContext1 heap){
//         ef = heap;
//     }
//     private readonly DBContext1 ef;
//     public string[] data => ef.data;

    
// }

// public ref struct DBAccess2<T>{
//     public DBAccess2(Func<DBContext2, T> tx) => Execute = tx;
//     public readonly Func<DBContext2, T> Execute;
// }

class Program {

    // cannot escape context
    ///Field cannot be of type 'DBAccess1<string>' unless it is an instance member of a ref struct.
    // private DBAccess1<string> x;

    static void Main(string[] args) {
        var s = new Services1();
        var result = s.DB<string>((ctx)=> {
            Console.WriteLine("TX");
            return ctx.data[0];
            }).Execute(); 
        
        var escape = s.DB<string>((ctx)=> {
            Console.WriteLine("TX");
            return ctx.data[0];
            });
        Task.Run(()=> {
            // fails to compile
            //Console.WriteLine(escape);
        });

        DBContext1 hack = null;
        s.DB<string>((ctx)=>
             {
                 hack = ctx;
            return "haha";
            }).Execute(); 

            //on. System.Exception: FAILS FAST
        Console.WriteLine(hack.data);
        // ALL COUNTS++ -1 and fail fast can be automated via https://docs.microsoft.com/en-us/dotnet/api/system.reflection.dispatchproxy?view=netcore-3.1 with neglectible perf (DB io or Expression parse is more costly)
        // but can hack ctx out so if to be perverted
        // next step is m      
        Console.WriteLine("Hello, world!");
    }
}