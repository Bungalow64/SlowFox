using ExternalProject.Net3_1.Constructors.Sample.NamespaceTests;
using Xunit;

namespace ExternalProject.Net3_1.UnitTestMocks.xUnit.Constructors.Linked.Tests.NamespaceTests
{
    [SlowFox.InjectMocks(typeof(ReferenceAttributeViaNamespaceAlias))]
    public partial class ReferenceAttributeViaNamespaceAliasTests
    {
        [Fact]
        public void HasDependency()
        {
            ReferenceAttributeViaNamespaceAlias model = Create();

            Assert.Equal(_userReader.Object, model.Dependency);
        }
    }
}