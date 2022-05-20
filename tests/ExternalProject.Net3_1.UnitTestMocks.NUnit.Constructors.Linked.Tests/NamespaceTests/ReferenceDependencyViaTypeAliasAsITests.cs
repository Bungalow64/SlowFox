using ExternalProject.Net3_1.Constructors.Sample.NamespaceTests;
using NUnit.Framework;

namespace ExternalProject.Net3_1.UnitTestMocks.NUnit.Constructors.Linked.Tests.NamespaceTests
{
    [TestFixture]
    [SlowFox.InjectMocks(typeof(ReferenceDependencyViaTypeAliasAsI))]
    public partial class ReferenceDependencyViaTypeAliasAsITests
    {
        [Test]
        public void HasDependency()
        {
            ReferenceDependencyViaTypeAliasAsI model = Create();

            Assert.That(model.Dependency, Is.EqualTo(_i.Object));
        }
    }
}