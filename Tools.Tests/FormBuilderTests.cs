
using Tools.HTML.Form.Builder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Tools.HTML.Form.Builder.Element;

namespace Tools.Tests
{  
    /// <summary>
    ///This is a test class for FormBuilderTests and is intended
    ///to contain all FormBuilderTests Unit Tests
    ///</summary>
    [TestClass()]
    public class FormBuilderTests
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
        ///A test for Construct
        ///</summary>
        //public void ConstructTestHelper<T> ()
        //{
        //    FormBuilder target = new FormBuilder(); // TODO: Initialize to an appropriate value
        //    T obj = default(T); // TODO: Initialize to an appropriate value
        //    target.Construct<T>(obj);
        //    Assert.Inconclusive("A method that does not return a value cannot be verified.");
        //}

        //[TestMethod()]
        //public void ConstructTest ()
        //{
        //    ConstructTestHelper<GenericParameterHelper>();
        //}

        /// <summary>
        ///A test for Construct
        ///</summary>
        //[TestMethod()]
        //public void ConstructTest1 ()
        //{
        //    FormBuilder target = new FormBuilder(); // TODO: Initialize to an appropriate value
        //    target.Construct();
        //    Assert.Inconclusive("A method that does not return a value cannot be verified.");
        //}

        /// <summary>
        ///A test for AddInput
        ///</summary>
        [TestMethod()]
        public void AddInputTest ()
        {
            FormBuilder target = new FormBuilder
            {
                Form = new Form
                {
                    Elements = new List<IElement>()
                }
            };
            Type PropertyType = typeof(string);
            string Name = "DisplayName";
            string Value = "Test";
            FormBuilder Expected = target;
            Expected.Form.Elements.Add(new Input("text", Name, Value));
            target.AddInput(PropertyType, Name, Value);
            Assert.AreEqual(target, Expected);
        }

        /// <summary>
        ///A test for AddSelect
        ///</summary>
        //[TestMethod()]
        //public void AddSelectWithIDTest ()
        //{
        //    FormBuilder target = new FormBuilder(); // TODO: Initialize to an appropriate value
        //    string Name = string.Empty; // TODO: Initialize to an appropriate value
        //    long ID = 0; // TODO: Initialize to an appropriate value
        //    target.AddSelect(Name, ID);
        //    Assert.Inconclusive("A method that does not return a value cannot be verified.");
        //}

        /// <summary>
        ///A test for AddSelect
        ///</summary>
        [TestMethod()]
        public void AddSelectTest ()
        {
            FormBuilder target = new FormBuilder
            {
                Form = new Form
                {
                    Elements = new List<IElement>()
                }
            };
            string Name = "Test";
            FormBuilder Expected = target;
            Expected.Form.Elements.Add(new Select(Name));
            target.AddSelect(Name);
            Assert.AreEqual(target, Expected);
        }
    }
}
