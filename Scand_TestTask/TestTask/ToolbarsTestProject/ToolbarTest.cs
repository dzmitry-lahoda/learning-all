using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.Xml;

namespace Scand.Toolbars
{
    
    
    /// <summary>
    ///This is a test class for ToolbarTest and is intended
    ///to contain all ToolbarTest Unit Tests
    ///</summary>
    [TestClass]
    public class ToolbarTest
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
        //public static void ToolbarTestInitialize(TestContext testContext)
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
        [TestInitialize]
        public void TestInitialize()
        {
            var settings = ConfigurationManager.AppSettings;
            var value = settings["BaseUri"];
            var uri=new Uri(value,UriKind.Absolute);
            TestContext.Properties.Add("BaseUri", uri);
        }
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion
        
        [ExpectedException(typeof(UriFormatException))]
        [TestMethod]
        public void LoadXmlWithWrongUri()
        {
            var toolbar = new Toolbar();
            toolbar.LoadXml("Bad formed URI.");
        }

        [TestMethod]
        public void LoadXmlCorrectAllXml()
        {
            var toolbar = new Toolbar();
            var baseUri = (Uri)TestContext.Properties["BaseUri"];
            var uri = new Uri(baseUri, "res/CorrectAll.xml");
            toolbar.LoadXml(uri.AbsoluteUri);
        }

        [TestMethod]
        public void LoadXmlCorrectNoImage()
        {
            var toolbar = new Toolbar();
            var baseUri = (Uri)TestContext.Properties["BaseUri"];
            var uri = new Uri(baseUri, "res/correctNoImage.xml");
            toolbar.LoadXml(uri.AbsoluteUri);
        }

        [TestMethod]
        public void LoadXmlCorrectNoText()
        {
            var toolbar = new Toolbar();
            var baseUri = (Uri)TestContext.Properties["BaseUri"];
            var uri = new Uri(baseUri, "res/correctNoText.xml");
            toolbar.LoadXml(uri.AbsoluteUri);
        }

        [TestMethod]
        public void LoadXmlCorrectManyImages()
        {
            var toolbar = new Toolbar();
            var baseUri = (Uri)TestContext.Properties["BaseUri"];
            var uri = new Uri(baseUri, "res/correctManyImages.xml");
            toolbar.LoadXml(uri.AbsoluteUri);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongXmlDataException))]
        public void LoadXmlNotCorrectEmptyId()
        {
            TestExceptionalCase("notcorrectEmptyId.xml");
        }

        [TestMethod]
        [ExpectedException(typeof(WrongXmlDataException))]
        public void LoadXmlNotCorrectIdenticalId()
        {
            TestExceptionalCase("notcorrectIdenticalId.xml");
        }

        [TestMethod]
        [ExpectedException(typeof(WrongXmlDataException))]
        public void LoadXmlNotCorrectNoId()
        {
            TestExceptionalCase("notcorrectNoId.xml");
        }

        [TestMethod]
        [ExpectedException(typeof(WrongXmlDataException))]
        public void LoadXmlNotCorrectNoImageNoText()
        {
            TestExceptionalCase("notcorrectNoImageNoText.xml");
        }

        [TestMethod]
        [ExpectedException(typeof(XmlException))]
        public void LoadXmlNotCorrectXml1()
        {
            TestExceptionalCase("notcorrectXml1.xml.txt");
        }

        [TestMethod]
        [ExpectedException(typeof(XmlException))]
        public void LoadXmlNotCorrectXml2()
        {
            TestExceptionalCase("notcorrectXml2.xml.txt");
        }

        [TestMethod]
        [ExpectedException(typeof(WrongXmlDataException))]
        public void LoadXmlNotCorrectWrongImgUri()
        {
            TestExceptionalCase("notcorrectWrongImgUri.xml");
        }

        //        [TestMethod]
        //        public void NotCorrectUnableToGetImage()
        //        {
        //            TestExceptionalCase("notcorrectUnableToGetImage.xml");
        //        }
        //
        //        [TestMethod]
        //        
        //        public void NotCorrectImageIsNotImage()
        //        {
        //            TestExceptionalCase("notcorrectImageIsNotImage.xml");
        //        }

        /// <summary>
        ///A test for SetButtonImage
        ///</summary>
        [TestMethod]
        public void SetButtonImageTest()
        {
            var toolbar = new Toolbar();
            var baseUri = (Uri)TestContext.Properties["BaseUri"];
            var uri = new Uri(baseUri, "res/CorrectAll.xml");
            toolbar.LoadXml(uri.AbsoluteUri);
            var buttonId = "btn1";
            uri = new Uri(baseUri, "img/img.gif");
            toolbar.SetButtonImage(buttonId, uri.AbsoluteUri);
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void SetImageBeforeLoadXmlTest()
        {
            var toolbar = new Toolbar();
            var baseUri = (Uri)TestContext.Properties["BaseUri"];
            var uri = new Uri(baseUri, "img/img.gif");
            toolbar.SetButtonImage("btn1", uri.AbsoluteUri);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SetButtonImageUsingWrongIdTest()
        {
            var toolbar = new Toolbar();
            var baseUri = (Uri)TestContext.Properties["BaseUri"];
            var uri = new Uri(baseUri, "res/correctAll.xml");
            toolbar.LoadXml(uri.AbsoluteUri);
            var imageUri = new Uri(baseUri, "img/img.gif");
            toolbar.SetButtonImage("btn", imageUri.AbsoluteUri);
        }

        private void TestExceptionalCase(string xmlFileName)
        {
            var toolbar = new Toolbar();
            var baseUri = (Uri)TestContext.Properties["BaseUri"];
            var uri = new Uri(baseUri, "res/" + xmlFileName);
            toolbar.LoadXml(uri.AbsoluteUri);
        }      
    }
}
