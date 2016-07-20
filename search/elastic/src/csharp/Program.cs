using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using Nest;
using Elasticsearch;
using Elasticsearch.Net;
using Newtonsoft.Json;

namespace csharp
{
    class Program
    {
        public class TheType
        {
            public bool Prop { get; set; }
        }

        static void Main(string[] args)
        {
            var asd = typeof(Newtonsoft.Json.ConstructorHandling);
            var settings = new ConnectionSettings();
            var inferrer = new Inferrer(settings);
            Expression<Func<TheType, object>> e1 = x => x.Prop;
            var firstCall = inferrer.PropertyName(new PropertyName { Expression = e1 });
            Console.WriteLine(firstCall);
            Expression<Func<TheType, object>> e2 = x => x.Prop;
            var secondCall = inferrer.PropertyName(new PropertyName { Expression = e2 });
            Console.WriteLine(secondCall);

        }
    }
}
