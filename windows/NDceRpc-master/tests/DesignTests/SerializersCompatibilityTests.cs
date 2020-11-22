using System;
using System.IO;
using System.Runtime.Serialization;
using NUnit.Framework;
using ProtoBuf;
using ProtoBuf.Meta;

namespace DesignTests
{

    [TestFixture]
    public class SerializersCompatibilityTests
    {

        [DataContract]
        public class EventMessage
        {

            public EventMessage(string sender, string data, Guid id)
            {
                Sender = sender;
                Data = data;
                Id = id;
                Timestamp = DateTime.Now;
            }

            [DataMember(Order = 4)]
            public DateTime Timestamp { get; private set; }

            [DataMember(Order = 1)]
            public Guid Id { get; private set; }

            [DataMember(Order = 2)]
            public string Sender { get; private set; }

            [DataMember(Order = 3)]
            public string Data { get; private set; }

        }


        [Test]
        public void SerializeNoPublicEmptyConstructor()
        {
            var msg = new EventMessage("sender", "data", Guid.NewGuid());
            var proto = ProtoBuf.Meta.TypeModel.Create();
            proto.Add(typeof(EventMessage), true);
            proto.CompileInPlace();
            var xml = new DataContractSerializer(typeof(EventMessage));
            xml.WriteObject(new MemoryStream(), msg);
            proto.Serialize(new MemoryStream(), msg);
        }

        [Test]
        public void DeSerializeNoPublicEmptyConstructor()
        {
            var msg = new EventMessage("sender", "data", Guid.NewGuid());
            var proto = ProtoBuf.Meta.TypeModel.Create();
            var meta = proto.Add(typeof(EventMessage), true);
            meta.UseConstructor = false;//makes Protobuf behace as DataContractSerializer for DataContractAttribute classes

            proto.CompileInPlace();

            var xml = new DataContractSerializer(typeof(EventMessage));
            var xmlStream = new MemoryStream();
            xml.WriteObject(xmlStream, msg);
            xmlStream.Position = 0;
            var protoStream = new MemoryStream();
            proto.Serialize(protoStream, msg);
            protoStream.Position = 0;

            var fromXml = xml.ReadObject(xmlStream) as EventMessage;
            Assert.AreEqual("sender", fromXml.Sender);

            var fromProto = proto.Deserialize(protoStream, null, typeof(EventMessage)) as EventMessage;
            Assert.AreEqual("sender", fromProto.Sender);

        }


        [Test]
        //[ExpectedException(typeof(InvalidOperationException))]
        [Description("Default behaviour of Protobuf serializers does not handles Exception")]
        public void SerializeException()
        {
            var stream = new MemoryStream();
            var ex = new Exception("serializable exception");
            var model = TypeModel.Create();
            model.Add(typeof (Exception), true);
            model.Serialize(stream, ex);
            stream.Position = 0;
            var deserialized = model.Deserialize(stream, null, typeof (Exception));
            //var info = new SerializationInfo(typeof(Exception), new FormatterConverter());
            //var context = new StreamingContext(StreamingContextStates.CrossProcess);
            //ex.GetObjectData(info, context);
            //Serializer.Serialize(info, context, ex);
        }
    }
}
