using ExternalProject.Net5.Constructors.Sample.NamespaceTests;
using NUnit.Framework;

namespace ExternalProject.Net5.UnitTestMocks.NUnit.Constructors.Linked.Tests.NamespaceTests
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