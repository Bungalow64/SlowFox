using ExternalProject.Net3_1.Constructors.Sample.NamespaceTests;
using Xunit;

namespace ExternalProject.Net3_1.UnitTestMocks.xUnit.Constructors.Linked.Tests.NamespaceTests
{
    [SlowFox.InjectMocks(typeof(ReferenceDependencyViaNamespaceAlias))]
    public partial class ReferenceDependencyViaNamespaceAliasTests
    {
        [Fact]
        public void HasDependency()
        {
            ReferenceDependencyViaNamespaceAlias model = Create();

            Assert.Equal(_userReader.Object, model.Dependency);
        }
    }
}