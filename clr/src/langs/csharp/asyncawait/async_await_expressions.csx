using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


async Task<int> X()
{
  return  await Task.FromResult(1);
}


public void Y(Expression<Func<Task<int>>> e) {
    var c = e.Compile();
    Console.WriteLine(c().Result);
}
Expression<Func<Task<int>>> ee = async () => await X();
Y(ee);
