using ExternalProject.Net6.Constructors.Sample.NamespaceTests;
using Xunit;

namespace ExternalProject.Net6.UnitTestMocks.xUnit.Constructors.Linked.Tests.NamespaceTests;

[SlowFox.InjectMocks(typeof(ReferenceDependencyViaRelativeType))]
public partial class ReferenceDependencyViaRelativeTypeTests
{
    [Fact]
    public void HasDependency()
    {
        ReferenceDependencyViaRelativeType model = Create();

        Assert.Equal(_userReader.Object, model.Dependency);
    }
}
