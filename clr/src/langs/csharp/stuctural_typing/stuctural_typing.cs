using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace vscde
{
        public class Person1
        {
                       public string Name {get;}
                       public int Year{get;}

                       public Guid Id{get;}
        }

        public class Person2
        {
                       public string Name {get; set;}
                       public int Year {get; set;}

                       public Person2 Parent{get; set;}
        }

        
        public struct Person3
        {
                
                       public string Name {get; set;}
                       public int Year {get; set;}                       
        }

        public static class Structural{
            public static (string Name, int Year) PersonLike(this Person1 p) => (p.Name, p.Year);
            public static (string Name, int Year) PersonLike(this Person2 p) => (p.Name, p.Year);

            public static (string Name, int Year) PersonLike(this object p) =>
                // or expression bodied switch?
                p is Person1 p1 ? p1.PersonLike() 
                : p is Person2 p2 ? p2.PersonLike() 
                : throw new NotSupportedException("p should be PersonLike `(string Name, int Year)`");

            public static ValueTuple<T1,T2> Like2<T1,T2>(this Person3 p) 
               // fails to compile - should iface or non sealed
               //where T: (string Name, int Year)
               //where T: ValueTuple
            {
                // FEC? Runtime + `static class` cache of compiled 
                // - compiled FEC should be absolutely FAST
                return default(ValueTuple<T1,T2>);
            }

            public static T Like3<T, T1,T2>(this Person3 p) 
               // fails to compile - should iface or non sealed
               //where T: (string Name, int Year)
               //where T: ValueTuple
              // where T: ValueTuple<T1,T2>
              where T:
               IEquatable<ValueTuple<T1,T2>>, 
               IStructuralEquatable, 	
               IStructuralComparable,
               IComparable, 
               IComparable<ValueTuple<T1,T2>>, 
               ITuple
            {
                // FEC? Runtime + `static class` cache of compiled 
                // - compiled FEC should be absolutely FAST
                return default(T);
            }


            public static T Like<T>(this Person3 p) 
               // fails to compile - should iface or non sealed
               //where T: (string Name, int Year)
               //where T: ValueTuple
               where T: 
               IEquatable<ValueTuple>, 
               IStructuralEquatable, 	
               IStructuralComparable,
               IComparable, 
               IComparable<ValueTuple>, 
               ITuple
            {
                // FEC? Runtime + `static class` cache of compiled 
                // - compiled FEC should be absolutely FAST
                return default(T);
            }
        }


}