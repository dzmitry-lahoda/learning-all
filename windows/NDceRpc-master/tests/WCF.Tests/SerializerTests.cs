using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using NUnit.Framework;

namespace WCF.Tests
{
    [TestFixture]
    public class SerializerTests
    {
        [DataContract]
        public class Data
        {
            [DataMember(Order = 1)]
            public List<string> List { get; set; }
        }

        [Test]
        public void NullListIsNull()
        {
            var ser = new DataContractSerializer(typeof(Data));
            var stream = new MemoryStream();
            ser.WriteObject(stream, new Data());
            stream.Position = 0;
            var data = ser.ReadObject(stream) as Data;
            Assert.IsTrue(data.List == null);
        }
        [Test]
        public void EmptyListIsEmpty()
        {
            var ser = new DataContractSerializer(typeof(Data));
            var stream = new MemoryStream();
            ser.WriteObject(stream, new Data{List = new List<string>()});
            stream.Position = 0;
            var data = ser.ReadObject(stream) as Data;
            Assert.IsTrue(data.List.Count == 0);
        }
    }
}
