using System;
using GreenElephant.Usafe.bit64;
using Xunit;
namespace ConsoleApp2
{
    public unsafe class Program
    {
        // no attemts to write disticbuted or networked code or high level code or doing functional DSL
        // https://en.wikipedia.org/wiki/The_Green_Elephant
        // tasty bread - algo and data stucts
        // green elephat - ml/ai
        // heron - 
        // colonel - 
        //  - block storage
        //  - dsp/audio
        //  - drawing
        // emulating rust
        // single machine absraction Random Access Memory (not graph or distibuted, may be some hierarchy)
        // no optimization for specific proccessor in mainline until several architetures are hit
        // no exceptions from methods, but error codes and static validation. 
        // copying of stucts is prefferable rather than copying references
        // no syncronization or lock free
        // how to control/pass deallocators?
        // `method` - not mutating method which does chech params and returs result or error code OR excects no mutation of passed values
        // `_method` - method which does not check params
        // `method_` - mutating method/not pure
        // `_method_` - mutating method which does not check params 
        // `__internal` - do not call direcly
        // `Method` - pass by value (full copy) method
        // `Method_` - pass by name (lazy evaluation method)
        // methodZero - zero allications
        // Rosly based resource usage checker via custom attribute [SafeUnsafe] or [UnsafeUnsafe]
        static int Main(string[] args)
        {
            var p = new CellListTests();
            p.Test();
            var p2 = new NativeArrayTests();
            p2.Test();

            return 0;
        }



    }
}