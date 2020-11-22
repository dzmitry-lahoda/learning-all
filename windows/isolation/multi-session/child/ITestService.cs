using System.ServiceModel;

namespace child
{

    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface ITestService
    {
        [OperationContract()]
         string GetTestData();
    }
}
