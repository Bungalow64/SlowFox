using ExternalProject.Net5.Constructors.Sample.NamespaceTests;
using NUnit.Framework;

namespace ExternalProject.Net5.UnitTestMocks.NUnit.Constructors.Linked.Tests.NamespaceTests
{
    [TestFixture]
    [SlowFox.InjectMocks(typeof(ReferenceDependencyViaTypeAlias))]
    public partial class ReferenceDependencyViaTypeAliasTests
    {
        [Test]
        public void HasDependency()
        {
            ReferenceDependencyViaTypeAlias model = Create();

            Assert.That(model.Dependency, Is.EqualTo(_reader.Object));
        }
    }
}