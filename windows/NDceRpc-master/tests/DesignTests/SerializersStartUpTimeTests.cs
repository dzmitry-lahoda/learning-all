using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using NStopwatch;
using NDceRpc.ServiceModel.Channels;
using NDceRpc.ServiceModel.IntegrationTests;
using NUnit.Framework;
using ProtoBuf.Meta;

namespace DesignTests
{

    [TestFixture]
    public class SerializersStartUpTimeTests
    {

        private static UserInfo CreateObj()
        {
            return new UserInfo { Entitlements = new List<string> { "GOD" }, ProxyDetails = new ProxyDetails { ProxyUserCredentials = new UserCredentials() }, Token = "1231238221==" };
        }

        private static void CreateProto()
        {
            var proto = TypeModel.Create();
            proto.Add(typeof(FaultData), true);
            proto.Add(typeof(MessageRequest), true);
            proto.Add(typeof(Message), true);
            proto.Add(typeof(RpcParamData), true);
            proto.Compile();

        }

        [Test]
        public void SerializerGeneration()
        {
            var tester = new FirstCallTester(Console.Out);
            tester.Start();
            var msg = new Messages();
            tester.Stop();
            tester.Start();
            CreateProto();
            tester.Stop();
            tester.Start();
            CreateProto();
            tester.Stop();
            tester.Report();

            var reportwatch = new Reportwatch();
            reportwatch.Start("Protobuf");
            var proto = ProtoBuf.Meta.TypeModel.Create();
            proto.Add(typeof(UserInfo), true);
            proto.CompileInPlace();

            reportwatch.Stop("Protobuf");

            reportwatch.Start("Protobuf serialize");
            proto.Serialize(new MemoryStream(), CreateObj());
            reportwatch.Stop("Protobuf serialize");

            reportwatch.Start("Protobuf serialize 2");
            proto.Serialize(new MemoryStream(), CreateObj());
            reportwatch.Stop("Protobuf serialize 2");

            reportwatch.Start("DataContractSerializer ctor");
            DataContractSerializer xml = new DataContractSerializer(typeof(UserInfo));
            reportwatch.Stop("DataContractSerializer ctor");

            reportwatch.Start("DataContractSerializer serialize");
            xml.WriteObject(new MemoryStream(), CreateObj());
            reportwatch.Stop("DataContractSerializer serialize");

            reportwatch.Start("DataContractSerializer serialize 2");
            xml.WriteObject(new MemoryStream(), CreateObj());
            reportwatch.Stop("DataContractSerializer serialize 2");

            reportwatch.Report("Protobuf");
            reportwatch.Report("Protobuf serialize");
            reportwatch.Report("Protobuf serialize 2");
            reportwatch.Report("DataContractSerializer ctor");
            reportwatch.Report("DataContractSerializer serialize");
            reportwatch.Report("DataContractSerializer serialize 2");

            Assert.IsTrue(reportwatch.GetTime(new Regex("(Protobuf)")) <= reportwatch.GetTime(new Regex("(DataContractSerializer)")));
        }
    }
}
