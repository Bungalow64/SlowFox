using ExternalProject.Net3_1.Constructors.Sample.NamespaceTests;
using NUnit.Framework;

namespace ExternalProject.Net3_1.UnitTestMocks.NUnit.Constructors.Linked.Tests.NamespaceTests
{
    [TestFixture]
    [SlowFox.InjectMocks(typeof(ReferenceDependencyViaTypeAlias))]
    public partial class ReferenceDependencyViaTypeAliasTests
    {
        [Test]
        public void HasDependency()
        {
            ReferenceDependencyViaTypeAlias model = Create();

            Assert.AreEqual(_reader.Object, model.Dependency);
        }
    }
}