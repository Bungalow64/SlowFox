using ExternalProject.Net5.Constructors.Sample.NamespaceTests;
using NUnit.Framework;

namespace ExternalProject.Net5.UnitTestMocks.NUnit.Constructors.Linked.Tests.NamespaceTests
{
    [TestFixture]
    [SlowFox.InjectMocks(typeof(ReferenceDependencyViaNamespaceAlias))]
    public partial class ReferenceDependencyViaNamespaceAliasTests
    {
        [Test]
        public void HasDependency()
        {
            ReferenceDependencyViaNamespaceAlias model = Create();

            Assert.AreEqual(_userReader.Object, model.Dependency);
        }
    }
}