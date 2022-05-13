using ExternalProject.Net5.SampleProject1.NamespaceTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExternalProject.Net5.UnitTestMocks.MSTest.Constructors.Linked.Tests.NamespaceTests
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