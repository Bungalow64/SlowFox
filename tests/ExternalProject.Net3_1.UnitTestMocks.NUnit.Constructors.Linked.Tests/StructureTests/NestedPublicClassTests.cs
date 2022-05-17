using ExternalProject.Net3_1.Constructors.Sample.StructureTests;
using NUnit.Framework;

namespace ExternalProject.Net3_1.UnitTestMocks.NUnit.Constructors.Linked.Tests.NamespaceTests
{
    [TestFixture]
    [SlowFox.InjectMocks(typeof(NestedPublicClass.InnerClass))]
    public partial class NestedPublicClassTests
    {
        [Test]
        public void HasDependency()
        {
            NestedPublicClass.InnerClass model = Create();

            Assert.AreEqual(_dataReader.Object, model.DataReader);
        }
    }
}