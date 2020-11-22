using System;
using NUnit.Framework;

namespace NEvents.Tests
{
    [TestFixture]
    public class DelegateTests
    {
        [Test]
        public void Event_delegateDeleteNull()
        {
            Action a = () => { };
            a -= null;

            Assert.IsNotNull(a);
        }

        [Test]
        public void Event_delegateAdd_newOneImutable()
        {
            Action a = () => { };
            var c = a;
            Action b = () => { };
            a = a + b;

            Assert.AreNotEqual(a,c);
        }

        [Test]
        public void Event_delegateDeleteNull_hasOne()
        {
            bool invoked = false;
            Action a = () =>
            {
                invoked = true;
            };
            a = a - null;

            a();

            Assert.IsTrue(invoked);
        }

        [Test]
        public void Event_delegateAddNull()
        {
            Action a = () => { };
            a += null;

            Assert.IsNotNull(a);
        }

        [Test]
        public void Event_delegateAddNull_stillTheSame()
        {
            Action a = () => { };
            var b = a;
            a += null;

            Assert.AreEqual(b,a);
        }

    }
}