
using Xunit;
namespace ConsoleApp2
{
    public unsafe class Program
    {
        // no attemts to write disticbuted code or high level code or doing functional DSL
        // tasty bread - algo and data stucts
        // green elephat - ml/ai
        // heron - 
        // colonel - 
        //  - block storage 
        //  - crypto
        //  - dsp/audio
        //  - drawing
        // - emulating rust and code analysis
        // single machine absraction Random Access Memory (not graph or distibuted, may be some hierarchy)
        // no optimization for specific proccessor in mainline until several architetures are hit
        // no exceptions from methods, but error codes and static validation. 
        // copying of stucts is prefferable rather than copying references
        // no syncronization or lock free
        // how to control/pass deallocators?
        // `method` - not mutating method which does chech params and returs result or error code OR excects no mutation of passed values
        // `_method` - method which does not check params
        // `methodMut` - mutating method/not pure
        // `_methodMut` - mutating method which does not check params 
        // `__internal` - do not call direcly
        // `methodCopy` - pass by value (full copy) method
        // `methodName` - pass by name (lazy evaluation method)

        static unsafe T Read<T>(T* ptr) where T : unmanaged
        {
            return *ptr;
        }

        static unsafe void _leftRotation_<T>(ushort steps, ushort length, T* array) where T:unmanaged
        {
            for (ushort i = 0; i < steps; i++)
            {
                T first = array[0];
                for (ushort j = 1; j < length; j++) array[j - 1] = array[j];
                array[length - 1] = first;
            }
        }

        static int Main(string[] args)
        {
            var p = new CellListTests();
            p.RunAll();
            var p2 = new NativeArrayTests();
            p2.Test();
            var p3 = new StringHashTests();
            p3.Test();
            var p4 = new CStdLibTests();
            p4.Test();
            return 0;
        }



    }
}