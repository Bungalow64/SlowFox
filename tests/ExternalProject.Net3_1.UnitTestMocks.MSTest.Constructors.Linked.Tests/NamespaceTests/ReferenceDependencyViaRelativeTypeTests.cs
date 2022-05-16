using ExternalProject.Net3_1.Constructors.Sample.NamespaceTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExternalProject.Net3_1.UnitTestMocks.MSTest.Constructors.Linked.Tests.NamespaceTests
{
    [TestClass]
    [SlowFox.InjectMocks(typeof(ReferenceDependencyViaRelativeType))]
    public partial class ReferenceDependencyViaRelativeTypeTests
    {
        [TestMethod]
        public void HasDependency()
        {
            ReferenceDependencyViaRelativeType model = Create();

            Assert.AreEqual(_userReader.Object, model.Dependency);
        }
    }
}