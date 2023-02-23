using ExternalProject.Net7.Constructors.Sample.NamespaceTests;
using Xunit;

namespace ExternalProject.Net7.UnitTestMocks.xUnit.Constructors.Linked.Tests.NamespaceTests;

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
