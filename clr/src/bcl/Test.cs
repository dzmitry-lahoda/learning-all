using NUnit.Framework;
using System;

namespace types
{
	[TestFixture ()]
	public class TypeTest
	{
		[Test ()]
		public void NumericalTypeIdentifiers ()
		{
			var t = typeof(Assert);
			var typeHandle = t.TypeHandle.Value.ToInt32();//TODO: fix this for 64 bit process

			var typeId = t.GUID;
			var typeIdBytes = typeId.ToByteArray();
			var metadataToken = t.MetadataToken;
			var first4Bytes = BitConverter.ToInt32(typeIdBytes, 0);
			var second4Bytes = BitConverter.ToInt32(typeIdBytes, 4);
			var hashOfTypeId = typeId.GetHashCode();
			var hashCodeOfType = t.GetHashCode();
             
			Assert.AreEqual(16,typeIdBytes.Length);

			Assert.AreNotEqual(first4Bytes,hashOfTypeId);
			Assert.AreNotEqual(second4Bytes,hashOfTypeId);

			Assert.AreNotEqual(first4Bytes,typeHandle);
			Assert.AreNotEqual(second4Bytes,typeHandle);

			Assert.AreNotEqual(typeHandle,hashOfTypeId);
			Assert.AreNotEqual(typeHandle,metadataToken);
			Assert.AreNotEqual(typeHandle,hashCodeOfType);



			Assert.AreNotEqual(hashCodeOfType,metadataToken);
			Assert.AreNotEqual(hashOfTypeId,metadataToken);

			Assert.AreNotEqual(hashOfTypeId,hashCodeOfType);

			Assert.AreNotEqual(first4Bytes,hashCodeOfType);
			Assert.AreNotEqual(second4Bytes,hashCodeOfType);

		
		}
	}
}

