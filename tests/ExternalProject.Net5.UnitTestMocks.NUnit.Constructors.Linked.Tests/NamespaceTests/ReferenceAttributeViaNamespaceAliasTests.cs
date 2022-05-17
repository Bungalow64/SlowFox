using ExternalProject.Net5.Constructors.Sample.NamespaceTests;
using NUnit.Framework;

namespace ExternalProject.Net5.UnitTestMocks.NUnit.Constructors.Linked.Tests.NamespaceTests
{
    [TestFixture]
    [SlowFox.InjectMocks(typeof(ReferenceAttributeViaNamespaceAlias))]
    public partial class ReferenceAttributeViaNamespaceAliasTests
    {
        [Test]
        public void HasDependency()
        {
            ReferenceAttributeViaNamespaceAlias model = Create();

            Assert.AreEqual(_userReader.Object, model.Dependency);
        }
    }
}