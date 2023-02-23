using ExternalProject.Net3_1.Constructors.Sample.NamespaceTests;
using Xunit;

namespace ExternalProject.Net3_1.UnitTestMocks.xUnit.Constructors.Linked.Tests.NamespaceTests
{
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
}