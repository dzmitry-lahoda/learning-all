using System.ServiceModel;

namespace child
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single,InstanceContextMode = InstanceContextMode.Single,IncludeExceptionDetailInFaults = true)]
    public class TestService : ITestService
    {
        private string _prev;

        public string GetTestData()
        {
            var o = OperationContext.Current;
            var c =  o.SessionId +" " + o.ServiceSecurityContext.WindowsIdentity.Name + " " + o.ServiceSecurityContext.IsAnonymous;
            var r = _prev + "-->" + c;
            _prev = c;
            return r;
        }
    }
}