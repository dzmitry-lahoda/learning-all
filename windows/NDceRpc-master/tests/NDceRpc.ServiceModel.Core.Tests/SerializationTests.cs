using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using NDceRpc.Serialization;
using NDceRpc.ServiceModel.Channels;
using NUnit.Framework;

namespace NDceRpc.ServiceModel.Core.Tests
{
    [TestFixture]
    public class SerializationTests
    {
        [DataContract]
        public class ComplexObject
        {
            [DataMember(Order = 1)]
            public DataV1 Prop1 { get; set; }
            [DataMember(Order = 2)]
            public DataV2 Prop2 { get; set; }
        }

        [DataContract]
        public class DataV1
        {
            [DataMember(Order = 1)]
            public int Prop1 { get; set; }
        }

        [DataContract]
        public class DataV2
        {
            [DataMember(Order = 1)]
            public int Prop1 { get; set; }
        }

        [DataContract]
        public class OtherDataV1
        {
            [DataMember(Order = 1)]
            public int OtherProp { get; set; }
        }

        [DataContract]
        public class OtherDataV2
        {
            [DataMember(Order = 1)]
            public int OtherProp { get; set; }
        }

        [Test]
        public void DataContract_serialize_streamNotEmpty()
        {
            var serializer = new ProtobufObjectSerializer();
            var stream = new MemoryStream();
            var obj = new DataV1 { Prop1 = 1 };
            serializer.WriteObject(stream, obj);

            Assert.IsTrue(stream.Length > 0);
        }

        [Test]
        public void DataContract_serializeDeserialize_theSame()
        {
            var serializer = new ProtobufObjectSerializer();
            var stream = new MemoryStream();
            var obj = new DataV1 { Prop1 = 1 };
            serializer.WriteObject(stream, obj);
            stream.Position = 0;
            var de = (DataV1)serializer.ReadObject(stream, typeof(DataV1));
            Assert.AreEqual(obj.Prop1, de.Prop1);
        }

        [Test]
        public void DataContract_serializeDeserializeOtherTypeSameSignature_theSame()
        {
            var serializer = new ProtobufObjectSerializer();
            var stream = new MemoryStream();
            var obj = new DataV1 { Prop1 = 1 };
            serializer.WriteObject(stream, obj);
            stream.Position = 0;
            var de = (DataV2)serializer.ReadObject(stream, typeof(DataV2));
            Assert.AreEqual(obj.Prop1, de.Prop1);
        }

        [Test]
        public void ConcurentSerialization()
        {
            var tasks = new List<Task>();
            var types = new[] { typeof(DataV1), typeof(DataV2), typeof(OtherDataV1), typeof(OtherDataV2) };
            for (int i = 0; i < 5; i++)
            {
                var task = new Task(() =>
                {
                    var serializer = new ProtobufObjectSerializer();
                    var childTasks = new List<Task>();
                    foreach (var type in types)
                    {
                        var childDask = new Task(() =>
                        {
                            var memoryStream = new MemoryStream();
                            serializer.WriteObject(memoryStream, Activator.CreateInstance(type));
                            serializer.ReadObject(memoryStream, type);
                        });
                        childTasks.Add(childDask);
                        childDask.Start();
                    }
                    Task.WaitAll(childTasks.ToArray());
                });
                tasks.Add(task);
                task.Start();
            }
            Task.WaitAll(tasks.ToArray());
        }

        [Test]
        public void SerializeMessage()
        {
            var req = new MessageRequest { Session = Guid.NewGuid().ToString() };
            var serializer = new ProtobufObjectSerializer();
            var stream = new MemoryStream();
            serializer.WriteObject(stream, req);
            Assert.IsTrue(stream.ToArray().Length > 0);
        }


        [Test]
        public void DeSerializeMessage()
        {
            var session = Guid.NewGuid().ToString();
            var req = new MessageRequest { Session = session };
            var serializer = new ProtobufObjectSerializer();
            var stream = new MemoryStream();
            serializer.WriteObject(stream, req);
            stream.Position = 0;
            var deserialized = (MessageRequest)serializer.ReadObject(stream, typeof(MessageRequest));
            Assert.AreEqual(session, deserialized.Session);
        }

        [Test]
        public void SerializeGuid()
        {
            var serializer = new ProtobufObjectSerializer();
            var stream = new MemoryStream();
            serializer.WriteObject(stream, Guid.NewGuid());
            Assert.IsTrue(stream.ToArray().Length > 0);
        }

        [Test]
        public void SerializeList()
        {
            var serializer = new ProtobufObjectSerializer();
            var stream = new MemoryStream();
            serializer.WriteObject(stream, new List<string> { ":)" });
            Assert.IsTrue(stream.ToArray().Length > 0);
        }

        [Test]
        public void SerializeComplextObject()
        {
            var serializer = new ProtobufObjectSerializer();
            var stream = new MemoryStream();
            var obj = new ComplexObject { Prop1 = new DataV1 { Prop1 = 1 }, Prop2 = new DataV2 { Prop1 = 2 } };
            serializer.WriteObject(stream, obj);
            Assert.IsTrue(stream.ToArray().Length > 0);
            stream.Position = 0;
            var deserialized = (ComplexObject)serializer.ReadObject(stream, typeof(ComplexObject));

            Assert.IsNotNull(deserialized);
            Assert.IsNotNull(deserialized.Prop1);
            Assert.IsNotNull(deserialized.Prop2);
        }




        [Test]
        public void SerializeNull()
        {
            var serializer = new ProtobufObjectSerializer();
            var stream = new MemoryStream();
            serializer.WriteObject(stream, null);

        }

        [Test]
        public void DeSerializeNull()
        {
            var serializer = new ProtobufObjectSerializer();
            var stream = new MemoryStream();
            serializer.WriteObject(stream, null);
            stream.Position = 0;
            var deserialized = serializer.ReadObject(stream, typeof(object));
            Assert.IsNull(deserialized);
        }



        [DataContract]
        public class DataMembersWithoutOrder
        {

            [DataMember()]
            public string Prop
            {
                get;
                set;
            }


  
        }

        [Test]
        [Description("Binary serialization is order dependant")]
        public void Serialize_DataMembersWithoutOrder()
        {
            var serializer = new ProtobufObjectSerializer();
            var stream = new MemoryStream();
            var obj = new DataMembersWithoutOrder { Prop = "value", };
            serializer.WriteObject(stream, obj);
            Assert.AreEqual(0,stream.Length);
        }

        [DataContract]
        public class Data
        {
            [DataMember(Order = 1)]
            public List<string> List { get; set; }
        }

        [Test]
        public void NullListIsNull()
        {
            var ser = new ProtobufObjectSerializer();
            var stream = new MemoryStream();
            ser.WriteObject(stream, new Data());
            stream.Position = 0;
            var data = ser.ReadObject(stream, typeof(Data)) as Data;
            Assert.IsTrue(data.List == null);
        }
        [Test]
        [Description("Protobuf on the wire does not distinguish empty collection and null")]
        public void EmptyListIsNull()
        {
            var ser = new ProtobufObjectSerializer();
            var stream = new MemoryStream();
            ser.WriteObject(stream, new Data { List = new List<string>() });
            stream.Position = 0;

            var data = ser.ReadObject(stream, typeof(Data)) as Data;

            Assert.IsNull(data.List);
        }
    }
}
