using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using NUnit.Framework;

namespace WCF.Tests
{


	[NUnit.Framework.TestFixture]
	public class SystemServiceModelTests
	{
		[Test]
		public void MessageVersionDefault_isRightAccordingVersion(){
			NUnit.Framework.Assert.AreEqual (System.ServiceModel.Channels.MessageVersion.Default.Envelope, EnvelopeVersion.Soap12);
			NUnit.Framework.Assert.AreEqual (System.ServiceModel.Channels.MessageVersion.Default.Addressing, AddressingVersion.WSAddressing10);
		}


        [Test]
        public void CreateMessage_givenActionWithStrinParameterAndDefaults_messageContainsActionAndBody()
        {
            var msg = Message.CreateMessage(MessageVersion.Default, "urn:DoStuff", "body");
            var content = msg.ToString();
            StringAssert.Contains("DoStuff", content);
            StringAssert.Contains("body", content);
        }

        private sealed class MyClrData
        {
            internal MyClrData(int i) { }
        }

        [Test]
        [ExpectedException(typeof(InvalidDataContractException))]
        public void CreateMessage_givenNotSerializableObjectAndDefaults_error()
        {
            var msg = Message.CreateMessage(MessageVersion.Default, "urn:DoStuff", new MyClrData(0));
            var content = msg.ToString();
        }

        [Test]
        public void CreateMessageFault_isFaultAndCanGetFaultOutOfMessage()
        {
            var fault = MessageFault.CreateFault(new FaultCode("Sender"), new FaultReason("Dummy"), "detail");
            var msg = Message.CreateMessage(MessageVersion.Default, fault, "urn:DoFault");
            Assert.IsTrue(msg.IsFault);
            var type = msg.GetType();
            var faultOfMessage = MessageFault.CreateFault(msg, 4096);
            var detailOfFault = faultOfMessage.GetDetail<string>();
            var content = msg.ToString();
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CreateMessage_CopyMessage_CopiedState()
        {
            var msg = Message.CreateMessage(MessageVersion.Default, "urn:Act");
            Assert.AreEqual(MessageState.Created, msg.State);
            var copy = msg.CreateBufferedCopy(4092);
            Assert.AreEqual(MessageState.Copied, msg.State);
            var copy2 = msg.CreateBufferedCopy(4092);
        }
	}
}
