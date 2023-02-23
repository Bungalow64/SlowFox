using ExternalProject.Net5.Constructors.Sample.NamespaceTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExternalProject.Net5.UnitTestMocks.MSTest.Constructors.Linked.Tests.NamespaceTests
{
    [TestClass]
    [SlowFox.InjectMocks(typeof(ReferenceAttributeViaFullType))]
    public partial class ReferenceAttributeViaFullTypeTests
    {
        [TestMethod]
        public void HasDependency()
        {
            ReferenceAttributeViaFullType model = Create();

            Assert.AreEqual(_userReader.Object, model.Dependency);
        }
    }
}