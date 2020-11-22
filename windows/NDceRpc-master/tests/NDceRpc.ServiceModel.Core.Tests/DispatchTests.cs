using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.Text;
using NUnit.Framework;

namespace NDceRpc.ServiceModel.Test
{
    [TestFixture]
    public class TestDispatch
    {
        private const int OperationDispId = 111;

        [ServiceContract]
        public interface IServiceWithOneMinimalOperation
        {
            [OperationContract]
            void Do();
        }

        [ServiceContract]
        public interface IServiceWithOneMinimalInteopOperation
        {
            [OperationContract]
            [DispId(OperationDispId)]
            void Do();

          
        }

        [Test]
        public void ImplicitId()
        {
            var operation = TypeExtensions.GetAllServiceImplmentations<IServiceWithOneMinimalOperation>();
            var ops = DispatchFactory.CreateOperations(operation);
            Assert.AreEqual(1,ops.Count);
        }

        [Test]
        public void ExplicitId()
        {
            var operation = TypeExtensions.GetAllServiceImplmentations<IServiceWithOneMinimalInteopOperation>();
            var ops = DispatchFactory.CreateOperations(operation);
            Assert.AreEqual(1, ops.Count);
            Assert.AreEqual(OperationDispId,ops.Values.First().Identifier);
        }
    }
}
