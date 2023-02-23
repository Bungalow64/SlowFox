using ExternalProject.Net5.Constructors.Sample.NamespaceTests;
using Xunit;

namespace ExternalProject.Net5.UnitTestMocks.xUnit.Constructors.Linked.Tests.NamespaceTests
{
    [SlowFox.InjectMocks(typeof(ReferenceDependencyViaTypeAlias))]
    public partial class ReferenceDependencyViaTypeAliasTests
    {
        [Fact]
        public void HasDependency()
        {
            ReferenceDependencyViaTypeAlias model = Create();

            Assert.Equal(_reader.Object, model.Dependency);
        }
    }
}