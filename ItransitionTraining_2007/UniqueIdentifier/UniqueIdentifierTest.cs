using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using NUnit.Framework;

namespace Itransition.Training
{
    [TestFixture]
    public class UniqueIdentifierTest
    {

        private int numberOfElements = 100;

        [Test]
        public void GetNewIdentifier()
        {
            StringCollection stringCollection=new StringCollection();
            for (int i = 0; i < numberOfElements; i++)
            {
                stringCollection.Add(UniqueIdentifier.GetNewIdentifier());
            }
            while (stringCollection.Count>0)
            {
                String identifier = stringCollection[0];
                stringCollection.Remove(identifier);
                Assert.IsFalse(stringCollection.Contains(identifier));
            }

        }
    }
}
