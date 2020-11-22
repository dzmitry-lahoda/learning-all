using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Text;
using NStopwatch;
using Microsoft.Practices.Unity.InterceptionExtension;
using NUnit.Framework;

namespace DesignTests
{
    [TestFixture]
    public class ProxyTests
    {
        public interface IRuntimeType
        {
            void RunDo1();
            void RunDo1(int v1,string v2);
            string RunDo();
        }

        public interface IKnownType
        {
            void KnownDo1();
            void KnownDo1(int v1, string v2);
            string KnownDo();
        }

        public class RealProxyProxy: RealProxy,IRemotingTypeInfo
        {
            public RealProxyProxy():base(typeof(IRuntimeType)){}

            public override IMessage Invoke(IMessage msg)
            {
                return new ReturnMessage(null,null,0,null,null);
            }

            public bool CanCastTo(Type fromType, object o)
            {
                if (fromType == typeof (IKnownType) || fromType == typeof(IRuntimeType)) 
                    return true;
                return false;
            }

            public string TypeName { get; set; }
        }

        [NUnit.Framework.SetUp]
        public void SetUp()
        {
            //warm up
           var setup = new[] { typeof (IRuntimeType), typeof (IKnownType),
                    typeof (RealProxy),typeof(RealProxyProxy),
                    typeof(Interceptor),typeof(InterfaceInterceptor),
                    typeof(TypeBuilder)};
            var l = setup.Length;
        }

        [Test]
        public void StartUpTime()
        {
            var testerRealProxy = new FirstCallTester(Console.Out);
            CreateRealProxy(testerRealProxy);
            testerRealProxy.Report();

            var testerUnity = new FirstCallTester(Console.Out);
            CreateUnityInerceptor(testerUnity);
            testerUnity.Report();
            Console.WriteLine();
            Console.WriteLine(testerRealProxy.Elapsed);
            Console.WriteLine(testerUnity.Elapsed);
            Assert.IsTrue(testerRealProxy.Elapsed.Ticks < 10*testerUnity.Elapsed.Ticks);
        }

        [Test]
        public void ThroughtputTime()
        {
            var tester1 = new FirstCallTester(Console.Out);
            var tester2 = new FirstCallTester(Console.Out);
            
            var real = CreateRealProxy(tester1);
            var unity = CreateUnityInerceptor(tester2);
            
            SpeedTesting.Do(Console.Out,5,10000,(r)=> RealTime(r,real),(r) => UnityTime(r,unity));
         
        }

        private void RealTime(long loop, IKnownType obj)
        {
            for (int i = 0; i < loop; i++)
            {
                obj.KnownDo();
                obj.KnownDo1(0,null);
            }
        }

        private void UnityTime(long loop, IKnownType obj)
        {
            for (int i = 0; i < loop; i++)
            {
                obj.KnownDo();
            }
        }

        private static IKnownType CreateRealProxy(FirstCallTester tester)
        {
            tester.Start("RealProxy");
            var realProxy = new RealProxyProxy().GetTransparentProxy();
            tester.Stop();
            tester.Start("RealProxySecond");
            var realProxy2 = new RealProxyProxy().GetTransparentProxy() as IRuntimeType;
            tester.Stop();
            tester.Start("as KnownType");
            var known = realProxy as IKnownType;
            tester.Stop();
            tester.Start("RealProxyKnownDo");
            known.KnownDo();
            tester.Stop();
            tester.Start("RealProxyKnownDoSecond");
            known.KnownDo();
            tester.Stop();
            tester.Start("RealProxyKnownDo1");
            known.KnownDo1(0, "");
            tester.Stop();
            return realProxy as IKnownType;
        }



        private static IKnownType CreateUnityInerceptor(FirstCallTester tester)
        {
            var realProxy = new RealProxyProxy().GetTransparentProxy();
            tester.Start("InterfaceIntercepted");
            var interceptor = Intercept.ThroughProxy(typeof (IKnownType), realProxy, new InterfaceInterceptor(),
                                                     new[] {new NullInterceptionBehaviour()});
            tester.Stop();
            tester.Start("InterfaceInterceptedSecond");
            var interceptor2 = Intercept.ThroughProxy(typeof (IRuntimeType), realProxy as IRuntimeType, new InterfaceInterceptor(),
                                                      new[] {new NullInterceptionBehaviour()});

            tester.Stop();
            tester.Start("as KnownType");
            var known = interceptor as IKnownType;
            tester.Stop();
            tester.Start("KnownDo");
            known.KnownDo();
            tester.Stop();
            tester.Start("KnownDoSecond");
            known.KnownDo();
            tester.Stop();
            tester.Start("KnownDo1");
            known.KnownDo1(0, "");
            tester.Stop();
            return interceptor as IKnownType;
        }
    }

    public class NullInterceptionBehaviour : IInterceptionBehavior
    {
        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            return new VirtualMethodReturn(input,null);
        }

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return new List<Type>();
        }

        public bool WillExecute { get { return true; } }
    }
}
