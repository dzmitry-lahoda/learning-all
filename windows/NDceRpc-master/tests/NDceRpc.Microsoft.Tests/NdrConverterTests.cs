using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NDceRpc.Ndr;
using NUnit.Framework;

namespace NDceRpc.Test
{
    [TestFixture]
    public class NdrConverterTests
    {
     

        [Test]
        public void NdrFcLong_local_theSameAsBitConverter()
        {
            var value = 100100100;
            var ndr = NdrConverter.NdrFcLong(value);
            var clr = BitConverter.GetBytes(value);

            Assert.AreEqual(ndr[0],clr[0]);
            Assert.AreEqual(ndr[1], clr[1]);
            Assert.AreEqual(ndr[2], clr[2]);
            Assert.AreEqual(ndr[3], clr[3]);
        }
    }
}
