using ExternalProject.Net5.Constructors.Sample.NamespaceTests;
using Xunit;

namespace ExternalProject.Net5.UnitTestMocks.xUnit.Constructors.Linked.Tests.NamespaceTests
{
    [SlowFox.InjectMocks(typeof(ReferenceDependencyViaType))]
    public partial class ReferenceDependencyViaTypeTests
    {
        [Fact]
        public void HasDependency()
        {
            ReferenceDependencyViaType model = Create();

            Assert.Equal(_userReader.Object, model.Dependency);
        }
    }
}