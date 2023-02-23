using ExternalProject.Net7.Constructors.Sample.BaseClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExternalProject.Net7.UnitTestMocks.MSTest.Constructors.Linked.Tests.BaseClasses
{
    [TestClass]
    [SlowFox.InjectMocks(typeof(DerivedBaseClassWithInjectedDependency))]
    public partial class DerivedBaseClassWithInjectedDependencyTests
    {
        [TestMethod]
        public void HasDependency()
        {
            DerivedBaseClassWithInjectedDependency model = Create();

            Assert.AreEqual(_userWriter.Object, model.UserWriter);
            Assert.AreEqual(_userReader.Object, model.UserReader);
        }
    }
}
