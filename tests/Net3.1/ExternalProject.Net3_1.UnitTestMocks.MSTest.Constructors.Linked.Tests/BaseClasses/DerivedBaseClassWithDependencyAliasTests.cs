using ExternalProject.Net3_1.Constructors.Sample.BaseClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExternalProject.Net3_1.UnitTestMocks.MSTest.Constructors.Linked.Tests.BaseClasses
{
    [TestClass]
    [SlowFox.InjectMocks(typeof(DerivedBaseClassWithDependencyAlias))]
    public partial class DerivedBaseClassWithDependencyAliasTests
    {
        [TestMethod]
        public void HasDependency()
        {
            DerivedBaseClassWithDependencyAlias model = Create();

            Assert.AreEqual(_userWriter.Object, model.UserWriter);
            Assert.AreEqual(_userReader.Object, model.UserReader);
        }
    }
}
