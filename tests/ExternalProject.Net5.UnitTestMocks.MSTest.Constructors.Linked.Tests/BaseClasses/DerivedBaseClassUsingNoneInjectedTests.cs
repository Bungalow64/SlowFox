using ExternalProject.Net5.Constructors.Sample.BaseClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExternalProject.Net5.UnitTestMocks.MSTest.Constructors.Linked.Tests.BaseClasses
{
    [TestClass]
    [SlowFox.InjectMocks(typeof(DerivedBaseClassUsingNoneInjected))]
    public partial class DerivedBaseClassUsingNoneInjectedTests
    {
        [TestMethod]
        public void HasDependency()
        {
            DerivedBaseClassUsingNoneInjected model = Create();

            Assert.AreEqual(_dataReader.Object, model.DataReader);
            Assert.AreEqual(_dataReader2.Object, model.DataReader2);
            Assert.AreEqual(_userReader.Object, model.UserReader);
        }
    }
}