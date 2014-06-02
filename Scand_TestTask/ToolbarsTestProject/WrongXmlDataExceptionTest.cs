using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.Serialization;

namespace Scand.Toolbars
{

    /// <summary>
    ///This is a test class for WrongXmlDataExceptionTest and is intended
    ///to contain all WrongXmlDataExceptionTest Unit Tests
    ///</summary>
    [TestClass]
    public class WrongXmlDataExceptionTest
    {

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        /// <summary>
        ///A test for GetObjectData
        ///</summary>
        [TestMethod]
        public void GetObjectData()
        {
            var exception = new WrongXmlDataException("Dublicate button id.", "http://thesite/nottest.xml", 3, "id", "btn1");
            var info = new SerializationInfo(typeof(WrongXmlDataException),new FormatterConverter());
            var context = new StreamingContext();
            exception.GetObjectData(info, context);
            Assert.AreEqual(info.GetInt32("ButtonNodeNumber"), exception.ButtonNodeNumber);
            Assert.AreEqual(info.GetString("ParamName"), exception.ParamName);
            Assert.AreEqual(info.GetString("XmlUri"), exception.XmlUri);
            Assert.AreEqual(info.GetString("ParamValue"), exception.ParamValue);
        }

        /// <summary>
        ///A test for WrongXmlDataException Constructor
        ///</summary>
        [TestMethod]
        public void WrongXmlDataExceptionConstructorTest()
        {
            var message = "Message";
            var xmlUri = "http://localhost/mysite/res/notcorrect.xml";
            var buttonNodeNumber = 1;
            var paramName = "id";
            var paramValue = "";
            WrongXmlDataException exception = new WrongXmlDataException(message, xmlUri, buttonNodeNumber, paramName, paramValue);
            Assert.AreEqual(buttonNodeNumber, exception.ButtonNodeNumber);
            Assert.AreEqual(paramName, exception.ParamName);
            Assert.AreEqual(xmlUri, exception.XmlUri);
            Assert.AreEqual(paramValue, exception.ParamValue);
        }
    }
}
