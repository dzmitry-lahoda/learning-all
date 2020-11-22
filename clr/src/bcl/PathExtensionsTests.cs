using System;
using System.IO;
using NUnit.Framework;

namespace extensions
{
	[TestFixture]
	public class PathExtensionsTests
	{

		[Test]
		public void GetPathWithoutHead_null()
		{
			Assert.IsNull(PathExtensions.GetPathWithoutHead(null));
		}

		[Test]
		public void GetPathWithoutHead_empty()
		{
    		Assert.Throws<ArgumentException>(()=> PathExtensions.GetPathWithoutHead(string.Empty));
		}

		[Test]
		public void GetPathWithoutHead_DirectoriesAndRightSlashes()
		{
			var path = "Directory1/Directory2/Directory3";
			var result = PathExtensions.GetPathWithoutHead(path);
			Assert.AreEqual ("Directory2/Directory3", result);
		}

		[Test]
		public void GetPathWithoutHead_DirectoriesAndLeftSlashes()
		{
			var path = @"Directory1\Directory2\Directory3";
			var result = PathExtensions.GetPathWithoutHead(path);
			Assert.AreEqual (@"Directory2\Directory3", result);
		}

		[Test]
		public void GetPathWithoutHead_RightSlashDirectoriesAndRightSlashes()
		{
			var path = "/Directory1/Directory2/Directory3";
			var result = PathExtensions.GetPathWithoutHead(path);
			Assert.AreEqual ("Directory2/Directory3", result);
		}

		[Test]
		public void GetPathWithoutHead_Single()
		{
			var path = "Directory1";
			var result = PathExtensions.GetPathWithoutHead(path);
			Assert.AreEqual (string.Empty, result);
		}

		[Test]
		public void GetPathWithoutHead_LeftSlashSingle()
		{
			var path = @"\Directory1";
			var result = PathExtensions.GetPathWithoutHead(path);
			Assert.AreEqual (string.Empty, result);
		}

		[Test]
		public void GetPathWithoutHead_LeftSlashSingleAndRetainSlash()
		{
			var path = @"\Directory1";
			var result = PathExtensions.GetPathWithoutHead(path,true);
			Assert.AreEqual (string.Empty, result);
		}

		[Test]
		public void GetPathWithoutHead_RightSlashSingle()
		{
			var path = "/Directory1";
			var result = PathExtensions.GetPathWithoutHead(path);
			Assert.AreEqual (string.Empty, result);
		}

		[Test]
		public void GetPathWithoutHead_LeftSlashDirectoriesAndLeftSlashes()
		{
			var path = @"\Directory1\Directory2\Directory3";
			var result = PathExtensions.GetPathWithoutHead(path);
			Assert.AreEqual (@"Directory2\Directory3", result);
		}

		[Test]
		public void GetPathWithoutHead_RightSlashDirectoriesAndRightSlashesAndRetainSeparator()
		{
			var path = "/Directory1/Directory2/Directory3";
			var result = PathExtensions.GetPathWithoutHead(path, true);
			Assert.AreEqual ("/Directory2/Directory3", result);
		}

		[Test]
		public void GetPathWithoutHead_LeftSlashDirectoriesAndLeftSlashesAndRetainSeparator()
		{
			var path = @"\Directory1\Directory2\Directory3";
			var result = PathExtensions.GetPathWithoutHead(path,true);
			Assert.AreEqual (@"\Directory2\Directory3", result);
		}

		[Test]
		public void GetPathWithoutHead_RootAndLeftSlashes()
		{
			var path = @"c:\dir\dir\file.ext";
			var result = PathExtensions.GetPathWithoutHead(path);
			Assert.AreEqual (@"dir\dir\file.ext", result);
		}

		[Test]
		public void GetPathWithoutHead_ServerAndLeftSlashes()
		{
			var path = @"\\server\dir\dir\file.ext";
			var result = PathExtensions.GetPathWithoutHead (path);
			Assert.AreEqual (@"dir\dir\file.ext", result);
		}

		[Test]
		public void GetPathWithoutHead_Case1()
		{
			var path = @"\\a1\b2$\c3\d5";
			var result = PathExtensions.GetPathWithoutHead (path);
			Assert.AreEqual (@"b2$\c3\d5", result);
		}

		[Test]
		public void GetPathWithoutHead_SingleDigit()
		{
			var path = "2";
			var result = PathExtensions.GetPathWithoutHead (path);
			Assert.AreEqual (string.Empty, result);
		}

		[Test]
		public void GetPathWithoutHead_SingleDigitRetainSlash()
		{
			var path = "2";
			var result = PathExtensions.GetPathWithoutHead (path, true);
			Assert.AreEqual (string.Empty, result);
		}

		[Test]
		public void GetPathWithoutHead_SingleDigitAndSlashAndRetainSlash()
		{
			var path = @"2\";
			var result = PathExtensions.GetPathWithoutHead (path, true);
			Assert.AreEqual (@"\", result);
		}

		[Test]
		public void GetPathWithoutHead_Case3()
		{
			var path = @"3\4.txt";
			var result = PathExtensions.GetPathWithoutHead (path);
			Assert.AreEqual ("4.txt", result);
		}


		[Test]
		public void GetPathWithoutHead_Case3RetainSlash()
		{
			var path = @"3\4.txt";
			var result = PathExtensions.GetPathWithoutHead (path, true);
			Assert.AreEqual (@"\4.txt", result);
		}

		[Test]
		public void GetPathWithoutHead_Case4RetainSlash()
		{
			var path = @"\3\4.txt";
			var result = PathExtensions.GetPathWithoutHead (path, true);
			Assert.AreEqual (@"\4.txt", result);
		}

	}
}

