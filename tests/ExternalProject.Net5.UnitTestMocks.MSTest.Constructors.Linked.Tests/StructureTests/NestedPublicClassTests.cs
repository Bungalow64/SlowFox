using ExternalProject.Net5.Constructors.Sample.StructureTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExternalProject.Net5.UnitTestMocks.MSTest.Constructors.Linked.Tests.NamespaceTests
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