using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace child
{
    public  class TestClient : ClientBase<ITestService>, ITestService
    {


        public TestClient(string endpointAddress)
            : base(new NetNamedPipeBinding(), new EndpointAddress(new Uri(endpointAddress, UriKind.Absolute)))
        { }

        public TestClient(NetNamedPipeBinding binding, string endpointAddress)
            : base(binding, new EndpointAddress(new Uri(endpointAddress, UriKind.Absolute)))
        { }


        public string GetTestData()
        {
            return Channel.GetTestData();
        }
    }
}
