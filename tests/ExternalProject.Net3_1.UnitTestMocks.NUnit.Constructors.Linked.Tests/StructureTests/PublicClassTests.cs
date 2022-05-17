using ExternalProject.Net3_1.Constructors.Sample.StructureTests;
using NUnit.Framework;

namespace ExternalProject.Net3_1.UnitTestMocks.NUnit.Constructors.Linked.Tests.NamespaceTests
{
    [TestFixture]
    [SlowFox.InjectMocks(typeof(PublicClass))]
    public partial class PublicClassTests
    {
        [Test]
        public void HasDependency()
        {
            PublicClass model = Create();

            Assert.AreEqual(_dataReader.Object, model.DataReader);
        }
    }
}