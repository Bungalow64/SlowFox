using ExternalProject.Net6.Constructors.Sample.NamespaceTests;
using Xunit;

namespace ExternalProject.Net6.UnitTestMocks.xUnit.Constructors.Linked.Tests.NamespaceTests;

[SlowFox.InjectMocks(typeof(ReferenceAttributeViaUsing))]
public partial class ReferenceAttributeViaUsingTests
{
    [Fact]
    public void HasDependency()
    {
        ReferenceAttributeViaUsing model = Create();

        Assert.Equal(_userReader.Object, model.Dependency);
    }
}
