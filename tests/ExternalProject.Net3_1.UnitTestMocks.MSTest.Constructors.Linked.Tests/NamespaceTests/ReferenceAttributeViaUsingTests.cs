using ExternalProject.Net3_1.Constructors.Sample.NamespaceTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExternalProject.Net3_1.UnitTestMocks.MSTest.Constructors.Linked.Tests.NamespaceTests
{
    [TestClass]
    [SlowFox.InjectMocks(typeof(ReferenceAttributeViaUsing))]
    public partial class ReferenceAttributeViaUsingTests
    {
        [TestMethod]
        public void HasDependency()
        {
            ReferenceAttributeViaUsing model = Create();

            Assert.AreEqual(_userReader.Object, model.Dependency);
        }
    }
}