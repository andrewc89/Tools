
using Tools.HTML.Form.Builder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Tools.ViewModels.General;
using System.Collections.Generic;
using Tools.HTML.Form.Builder.Element;

namespace Tools.Tests
{   
    /// <summary>
    ///This is a test class for FormTests and is intended
    ///to contain all FormTests Unit Tests
    ///</summary>
    [TestClass()]
    public class FormTests
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
        ///A test for AddSelectOptions
        ///</summary>
        [TestMethod()]
        public void AddSelectOptionsTest ()
        {
            Form target = new Form
            {
                Elements = new List<IElement>
                {
                    new Select("ClassType")
                }
            };
            string ElementName = "ClassType";
            List<SelectElement> Options = new List<SelectElement>
            {
                new SelectElement { ID = 1, DisplayName = "one" },
                new SelectElement { ID = 2, DisplayName = "two" }
            };
            Form expected = target;
            var Element = (Select)expected.Elements.Find(x => x.Name.Equals("ClassType"));
            Element.Options = Options;
            Form actual;
            actual = target.AddSelectOptions(ElementName, Options);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for AllRequired
        ///</summary>
        [TestMethod()]
        public void AllRequiredTest ()
        {
            Form target = new Form
            {
                Elements = new List<IElement>
                {
                    new Input("text", "Test1"),
                    new Input("text", "Test2"),
                    new Select("Test3")
                }
            };
            Form expected = target;
            foreach (var element in expected.Elements)
            {
                element.Classes.Add("required");
            }
            Form actual;
            actual = target.AllRequired();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for AreRequired
        ///</summary>
        [TestMethod()]
        public void AreRequiredTest ()
        {
            Form target = new Form
            {
                Elements = new List<IElement>
                {
                    new Input("text", "Test1"),
                    new Input("text", "Test2"),
                    new Select("Test3")
                }
            };
            List<string> Elements = new List<string>
            {
                "Test1",
                "Test3"
            };
            Form expected = target;
            foreach (var Element in Elements)
            {
                expected.Elements.Find(x => x.Name.Equals(Element)).Classes.Add("required");
            }
            Form actual;
            actual = target.AreRequired(Elements);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IsNotRequired
        ///</summary>
        [TestMethod()]
        public void IsNotRequiredTest ()
        {
            Form target = new Form
            {
                Elements = new List<IElement>
                {
                    new Input("text", "Test1", "", "required"),
                    new Input("text", "Test2"),
                    new Select("Test3", "required")
                }
            };
            string ElementName = "Test3";
            Form expected = target;
            expected.Elements.Find(x => x.Name.Equals(ElementName)).Classes.RemoveAll(x => x.Equals("required"));
            Form actual;
            actual = target.IsNotRequired(ElementName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for IsRequired
        ///</summary>
        [TestMethod()]
        public void IsRequiredTest ()
        {
            Form target = new Form
            {
                Elements = new List<IElement>
                {
                    new Input("text", "Test1", "", "required"),
                    new Input("text", "Test2"),
                    new Select("Test3", "required")
                }
            };
            string ElementName = "Test2";
            Form expected = target;
            expected.Elements.Find(x => x.Name.Equals("Test2")).Classes.Add("required");
            Form actual;
            actual = target.IsRequired(ElementName);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for SetAction
        ///</summary>
        [TestMethod()]
        public void SetActionTest ()
        {
            Form target = new Form
            {
                Action = "TestAction"
            };
            string Action = "SecondTestAction";
            Form expected = target;
            expected.Action = Action;
            Form actual;
            actual = target.SetAction(Action);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for SetMethod
        ///</summary>
        [TestMethod()]
        public void SetMethodTest ()
        {
            Form target = new Form
            {
                Method = "TestMethod"
            };
            string Method = "SecondTestMethod";
            Form expected = target;
            expected.Method = Method;
            Form actual;
            actual = target.SetMethod(Method);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ToHtml
        ///</summary>
        //[TestMethod()]
        //public void ToHtmlTest ()
        //{
        //    Form target = new Form(); // TODO: Initialize to an appropriate value
        //    string expected = string.Empty; // TODO: Initialize to an appropriate value
        //    string actual;
        //    actual = target.ToHtml();
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}
    }
}
