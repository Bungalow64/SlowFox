using ExternalProject.Net6.Constructors.Sample.NamespaceTests;
using Xunit;

namespace ExternalProject.Net6.UnitTestMocks.xUnit.Constructors.Linked.Tests.NamespaceTests;

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
