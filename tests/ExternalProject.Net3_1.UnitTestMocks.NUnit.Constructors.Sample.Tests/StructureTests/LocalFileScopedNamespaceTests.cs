using ExternalProject.Net3_1.UnitTestMocks.NUnit.Constructors.Sample.Tests.LocalClasses;
using NUnit.Framework;

namespace ExternalProject.Net3_1.UnitTestMocks.NUnit.Constructors.Sample.Tests.StructureTests
{
    [TestFixture]
    [SlowFox.InjectMocks(typeof(LocalFileScopedNamespace))]
    public partial class LocalFileScopedNamespaceTests
    {
        [Test]
        public void HasDependency()
        {
            LocalFileScopedNamespace model = Create();

            Assert.AreEqual(_dataReader.Object, model.DataReader);
        }
    }
}