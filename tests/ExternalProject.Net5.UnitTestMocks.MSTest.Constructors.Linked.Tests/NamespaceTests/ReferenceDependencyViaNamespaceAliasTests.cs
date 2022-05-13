using ExternalProject.Net5.SampleProject1.NamespaceTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExternalProject.Net5.UnitTestMocks.MSTest.Constructors.Linked.Tests.NamespaceTests
{
    [TestClass]
    [SlowFox.InjectMocks(typeof(ReferenceDependencyViaNamespaceAlias))]
    public partial class ReferenceDependencyViaNamespaceAliasTests
    {
        [TestMethod]
        public void HasDependency()
        {
            ReferenceDependencyViaNamespaceAlias model = Create();

            Assert.AreEqual(_userReader.Object, model.Dependency);
        }
    }
}