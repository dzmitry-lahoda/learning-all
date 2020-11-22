
using System.Linq;

using NUnit.Framework;
using managed_entities;

namespace managedtests
{

    [TestFixture]
    public class SerializationTests
    {
       



        [Test]
        public void empty_request()
        {
           custom_request request = new custom_request();
           var size =  request.getSize();
           Assert.AreEqual(sizeof(int)*5+sizeof(byte),size);
            byte[] b;
            request.toArray(out b,size);
           custom_request dr = new custom_request();
            dr.fromArray(b);
            Assert.AreEqual(request.m_name,dr.m_name);
            Assert.AreEqual(request.m_ids.Count, dr.m_ids.Count);
            Assert.AreEqual(request.m_types.Count, dr.m_types.Count);
        }

        [Test]
        public void one_one_one_request()
        {
            custom_request request = new custom_request();
            request.m_name = "1";
            request.m_ids.Add("1");
            request.m_types.Add("1");
            var size = request.getSize();
            Assert.AreEqual(sizeof(int) * 5 + sizeof(byte)*3+sizeof(byte)*3, size);
            byte[] b;
            request.toArray(out b,size);
            custom_request dr = new custom_request();
            dr.fromArray(b);
            Assert.AreEqual("1", dr.m_name);
            Assert.AreEqual("1", dr.m_ids.First());
            Assert.AreEqual("1", dr.m_types.First());
            Assert.AreEqual(request.m_ids.Count, dr.m_ids.Count);
            Assert.AreEqual(request.m_types.Count, dr.m_types.Count);
        }


        [Test]
        public void two_two_two_request()
        {
            custom_request request = new custom_request();
            request.m_name = "22";
            request.m_ids.Add("22");
            request.m_ids.Add("22");
            request.m_types.Add("22");
            request.m_types.Add("22");
            var size = request.getSize();
            byte[] b;
            request.toArray(out b, size);
            custom_request dr = new custom_request();
            dr.fromArray(b);
            Assert.AreEqual("22", dr.m_name);
            Assert.AreEqual("22", dr.m_ids.Skip(1).First());
            Assert.AreEqual("22", dr.m_types.Skip(1).First());
            Assert.AreEqual(request.m_ids.Count, dr.m_ids.Count);
            Assert.AreEqual(request.m_types.Count, dr.m_types.Count);
        }

        [Test]
        public void complex_request()
        {
            var name = "cool data";
            var id = "very very cool id";
            var type = "very very cool type";
            custom_request request = new custom_request();
            request.m_name = name;
            request.m_ids.Add(id);
            request.m_ids.Add(id);
            request.m_ids.Add(id);
            request.m_types.Add(type);
            request.m_types.Add(type);
            var size = request.getSize();
            byte[] b;
            request.toArray(out b, size);
            custom_request dr = new custom_request();
            dr.fromArray(b);
            Assert.AreEqual(name, dr.m_name);
            Assert.AreEqual(id, dr.m_ids.Skip(2).First());
            Assert.AreEqual(type, dr.m_types.Skip(1).First());
            Assert.AreEqual(request.m_ids.Count, dr.m_ids.Count);
            Assert.AreEqual(request.m_types.Count, dr.m_types.Count);
        }
    }
}
