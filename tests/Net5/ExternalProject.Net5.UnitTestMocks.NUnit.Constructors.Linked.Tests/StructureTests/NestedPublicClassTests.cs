using ExternalProject.Net5.Constructors.Sample.StructureTests;
using NUnit.Framework;

namespace ExternalProject.Net5.UnitTestMocks.NUnit.Constructors.Linked.Tests.NamespaceTests
{
    [TestFixture]
    [SlowFox.InjectMocks(typeof(NestedPublicClass.InnerClass))]
    public partial class NestedPublicClassTests
    {
        [Test]
        public void HasDependency()
        {
            NestedPublicClass.InnerClass model = Create();

            Assert.That(model.DataReader, Is.EqualTo(_dataReader.Object));
        }
    }
}