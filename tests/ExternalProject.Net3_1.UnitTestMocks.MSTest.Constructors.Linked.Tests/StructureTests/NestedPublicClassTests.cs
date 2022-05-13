using ExternalProject.Net3_1.SampleProject1.StructureTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExternalProject.Net3_1.UnitTestMocks.MSTest.Constructors.Linked.Tests.NamespaceTests
{
    [TestClass]
    [SlowFox.InjectMocks(typeof(NestedPublicClass.InnerClass))]
    public partial class NestedPublicClassTests
    {
        [TestMethod]
        public void HasDependency()
        {
            NestedPublicClass.InnerClass model = Create();

            Assert.AreEqual(_dataReader.Object, model.DataReader);
        }
    }
}