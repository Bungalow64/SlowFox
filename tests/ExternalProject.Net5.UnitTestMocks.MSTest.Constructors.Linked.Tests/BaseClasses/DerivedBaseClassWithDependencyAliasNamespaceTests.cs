using ExternalProject.Net5.Constructors.Sample.BaseClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExternalProject.Net5.UnitTestMocks.MSTest.Constructors.Linked.Tests.BaseClasses
{
    [TestClass]
    [SlowFox.InjectMocks(typeof(DerivedBaseClassWithDependencyAliasNamespace))]
    public partial class DerivedBaseClassWithDependencyAliasNamespaceTests
    {
        [TestMethod]
        public void HasDependency()
        {
            DerivedBaseClassWithDependencyAliasNamespace model = Create();

            Assert.AreEqual(_userWriter.Object, model.UserWriter);
            Assert.AreEqual(_userReader.Object, model.UserReader);
        }
    }
}
