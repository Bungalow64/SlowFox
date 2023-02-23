using ExternalProject.Net7.Constructors.Sample.NamespaceTests;
using Xunit;

namespace ExternalProject.Net7.UnitTestMocks.xUnit.Constructors.Linked.Tests.NamespaceTests;

[SlowFox.InjectMocks(typeof(ReferenceAttributeViaTypeAlias))]
public partial class ReferenceAttributeViaTypeAliasTests
{
    [Fact]
    public void HasDependency()
    {
        ReferenceAttributeViaTypeAlias model = Create();

        Assert.Equal(_userReader.Object, model.Dependency);
    }
}
