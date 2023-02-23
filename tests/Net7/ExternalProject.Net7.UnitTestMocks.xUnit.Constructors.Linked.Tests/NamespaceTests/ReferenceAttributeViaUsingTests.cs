using ExternalProject.Net7.Constructors.Sample.NamespaceTests;
using Xunit;

namespace ExternalProject.Net7.UnitTestMocks.xUnit.Constructors.Linked.Tests.NamespaceTests;

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
