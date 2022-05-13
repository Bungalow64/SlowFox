using ExternalProject.Net5.SampleProject1.StructureTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExternalProject.Net5.UnitTestMocks.MSTest.Constructors.Linked.Tests.NamespaceTests
{
    [TestClass]
    [SlowFox.InjectMocks(typeof(PublicClass))]
    public partial class PublicClassTests
    {
        [TestMethod]
        public void HasDependency()
        {
            PublicClass model = Create();

            Assert.AreEqual(_dataReader.Object, model.DataReader);
        }
    }
}