using System.Linq;
using NUnit.Framework;

namespace NDceRpc.ServiceModel.Core.Tests
{
    [TestFixture]
    public class DispatchTableFactoryTests
    {
        [Test]
        public void GetOperations_iterfacesWithOperationsWithoudId()
        {
            var abc = DispatchTableFactory.GetOperations(typeof(IABC));
            var ids = abc.IdToOperation.Keys;

            Assert.IsTrue(ids.Count > 0);
            Assert.AreEqual(0, ids.Where(x => x < DispatchTableFactory.DEFAULT_ID_SHIFT).Count());
        }

        [Test]
        public void GetOperations_iterfacesWithOperationsWithId()
        {
            var abcd = DispatchTableFactory.GetOperations(typeof(IABCD));
            var ids = abcd.IdToOperation.Keys;

            Assert.IsTrue(ids.Count > 0);
            Assert.Contains(42, ids);
        }
    }
}