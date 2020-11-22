using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Diagnostics;

delegate void Set(ref int x);

class X { }

var method = new DynamicMethod(string.Empty, typeof(void), 
    new[] { typeof(int).MakeByRefType() }, typeof(X), skipVisibility: true);

var il = method.GetILGenerator();
il.Emit(OpCodes.Ldarg_0);
il.Emit(OpCodes.Ldc_I4_7);
il.Emit(OpCodes.Stind_I4);
il.Emit(OpCodes.Ret);

var d = (Set)method.CreateDelegate(typeof(Set));
var x = 0;
d(ref x);
Console.WriteLine(x);