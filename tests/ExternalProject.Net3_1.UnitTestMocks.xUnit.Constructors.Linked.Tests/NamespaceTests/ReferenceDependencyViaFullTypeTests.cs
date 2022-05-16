using ExternalProject.Net3_1.Constructors.Sample.NamespaceTests;
using Xunit;

namespace ExternalProject.Net3_1.UnitTestMocks.xUnit.Constructors.Linked.Tests.NamespaceTests
{
    [SlowFox.InjectMocks(typeof(ReferenceDependencyViaFullType))]
    public partial class ReferenceDependencyViaFullTypeTests
    {
        [Fact]
        public void HasDependency()
        {
            ReferenceDependencyViaFullType model = Create();

            Assert.Equal(_userReader.Object, model.Dependency);
        }
    }
}