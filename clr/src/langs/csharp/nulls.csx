using System;
using System.Linq;

string result = null;

var dictionary = result?.ToDictionary(x => x, x => x);

Console.WriteLine(dictionary == null ? "null"  : dictionary.ToString());