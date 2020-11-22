using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharplib
{
    class parent_plus_child_retrun
    {
        public class SomeItem
        {
            public Guid ID { get; set; }
            public string Name { get; set; }
            public bool HasChildren { get; set; }
            public IEnumerable<SomeItem> Children { get; set; }
        }

        //private static IEnumerable<T> YieldTree(T item, )
        //{
        //    if (item != null)
        //    {
        //        yield return item;

        //        foreach (var c in item.Children)
        //        {                    
        //            var cc = YieldTree(c);
        //            foreach (var ccc in cc)
        //            {
        //                yield return ccc;
        //            }
        //        }
        //    }
        //}
    }
}
