using ExternalProject.Net3_1.Constructors.Sample.NamespaceTests;
using NUnit.Framework;

namespace ExternalProject.Net3_1.UnitTestMocks.NUnit.Constructors.Linked.Tests.NamespaceTests
{
    [TestFixture]
    [SlowFox.InjectMocks(typeof(ReferenceDependencyViaNamespaceAlias))]
    public partial class ReferenceDependencyViaNamespaceAliasTests
    {
        [Test]
        public void HasDependency()
        {
            ReferenceDependencyViaNamespaceAlias model = Create();

            Assert.That(model.Dependency, Is.EqualTo(_userReader.Object));
        }
    }
}