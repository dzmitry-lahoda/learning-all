#r "../../packages/Elasticsearch.Net/lib/net46/Elasticsearch.Net.dll"
#r "../../packages/NEST/lib/net46/NEST.dll"
#r "../../packages/Newtonsoft.Json/lib/net45/Newtonsoft.Json.dll"

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

public static class Interactive
{
    public class TheType
    {
        public bool Prop { get; set; }
    }

    public static void Do()
    {
        var asd = typeof(Newtonsoft.Json.ConstructorHandling);
        var settings = new ConnectionSettings();
        var inferrer = new Inferrer(settings);
        Expression<Func<TheType, object>> e = x => x.Prop;
        var firstCall = inferrer.PropertyName(new PropertyName { Expression = e });
        Console.WriteLine(firstCall);
        var secondCall = inferrer.PropertyName(new PropertyName { Expression = e });
        Console.WriteLine(firstCall);
    }
}

Interactive.Do();