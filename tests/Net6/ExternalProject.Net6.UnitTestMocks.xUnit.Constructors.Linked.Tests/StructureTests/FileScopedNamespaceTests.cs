using ExternalProject.Net6.Constructors.Sample.StructureTests;
using Xunit;

namespace ExternalProject.Net6.UnitTestMocks.xUnit.Constructors.Linked.Tests.NamespaceTests;

[SlowFox.InjectMocks(typeof(FileScopedNamespace))]
public partial class FileScopedNamespaceTests
{
    [Fact]
    public void HasDependency()
    {
        FileScopedNamespace model = Create();

        Assert.Equal(_dataReader.Object, model.DataReader);
    }
}
