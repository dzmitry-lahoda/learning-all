using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace ML.Service.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            //var uri = new Uri("http://localhost/NaiveBayesService.svc");
            ServiceHost host = new ServiceHost(typeof(NaiveBayesService));//,uri);
            host.Open();
            
            


            Console.ReadKey();
        }
    }
}
