
using Tools.Strings.Parse;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tools.Tests
{   
    /// <summary>
    ///This is a test class for ParseTests and is intended
    ///to contain all ParseTests Unit Tests
    ///</summary>
    [TestClass()]
    public class ParseTests
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
        ///A test for SplitByWord
        ///</summary>
        [TestMethod()]
        public void SplitByWordTest ()
        {
            string Input = "AClassTypeTestForTheTest";
            string expected = "A Class Type Test For The Test";
            string actual;
            actual = Input.SplitByWord();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IndexOfNextUpperChar
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Tools.dll")]
        public void IndexOfNextUpperCharTest ()
        {
            string Input = "AClassTypeTestForTheTest";
            int Index = 3;
            int expected = 6;
            int actual;
            actual = Input.IndexOfNextUpperChar(Index);
            Assert.AreEqual(expected, actual);
        }
    }
}
