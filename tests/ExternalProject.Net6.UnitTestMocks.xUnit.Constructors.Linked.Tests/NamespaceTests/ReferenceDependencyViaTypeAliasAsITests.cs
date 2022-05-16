using ExternalProject.Net6.Constructors.Sample.NamespaceTests;
using Xunit;

namespace ExternalProject.Net6.UnitTestMocks.xUnit.Constructors.Linked.Tests.NamespaceTests;

[SlowFox.InjectMocks(typeof(ReferenceDependencyViaTypeAliasAsI))]
public partial class ReferenceDependencyViaTypeAliasAsITests
{
    [Fact]
    public void HasDependency()
    {
        ReferenceDependencyViaTypeAliasAsI model = Create();

        Assert.Equal(_i.Object, model.Dependency);
    }
}
