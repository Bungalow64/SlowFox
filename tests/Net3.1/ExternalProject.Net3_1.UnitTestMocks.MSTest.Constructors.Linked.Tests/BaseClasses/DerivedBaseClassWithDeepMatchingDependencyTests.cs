using ExternalProject.Net3_1.Constructors.Sample.BaseClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExternalProject.Net3_1.UnitTestMocks.MSTest.Constructors.Linked.Tests.BaseClasses
{
    [TestClass]
    [SlowFox.InjectMocks(typeof(DerivedBaseClassWithDeepMatchingDependency))]
    public partial class DerivedBaseClassWithDeepMatchingDependencyTests
    {
        [TestMethod]
        public void HasDependency()
        {
            DerivedBaseClassWithDeepMatchingDependency model = Create();

            Assert.AreEqual(_dataReader.Object, model.DataReader);
            Assert.AreEqual(_userWriter.Object, model.UserWriter);
            Assert.AreEqual(_userReader.Object, model.UserReader);
        }
    }
}
