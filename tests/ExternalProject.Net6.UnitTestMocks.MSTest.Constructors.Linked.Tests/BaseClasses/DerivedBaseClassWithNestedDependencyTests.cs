using ExternalProject.Net6.Constructors.Sample.BaseClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExternalProject.Net6.UnitTestMocks.MSTest.Constructors.Linked.Tests.BaseClasses
{
    [TestClass]
    [SlowFox.InjectMocks(typeof(DerivedBaseClassWithNestedDependency))]
    public partial class DerivedBaseClassWithNestedDependencyTests
    {
        [TestMethod]
        public void HasDependency()
        {
            DerivedBaseClassWithNestedDependency model = Create();

            Assert.AreEqual(_userWriter.Object, model.UserWriter);
            Assert.AreEqual(_userReader.Object, model.UserReader);
            Assert.AreEqual(_dataReader.Object, model.DataReader);
        }
    }
}
