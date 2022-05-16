using ExternalProject.Net6.Constructors.Sample.BaseClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExternalProject.Net6.UnitTestMocks.MSTest.Constructors.Linked.Tests.BaseClasses
{
    [TestClass]
    [SlowFox.InjectMocks(typeof(DerivedBaseClassWithDependency))]
    public partial class DerivedBaseClassWithDependencyTests
    {
        [TestMethod]
        public void HasDependency()
        {
            DerivedBaseClassWithDependency model = Create();

            Assert.AreEqual(_userWriter.Object, model.UserWriter);
            Assert.AreEqual(_userReader.Object, model.UserReader);
        }
    }
}
