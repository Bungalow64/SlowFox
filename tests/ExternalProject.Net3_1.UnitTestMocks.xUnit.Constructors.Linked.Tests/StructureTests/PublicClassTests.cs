using ExternalProject.Net3_1.Constructors.Sample.StructureTests;
using Xunit;

namespace ExternalProject.Net3_1.UnitTestMocks.xUnit.Constructors.Linked.Tests.NamespaceTests
{
    [SlowFox.InjectMocks(typeof(PublicClass))]
    public partial class PublicClassTests
    {
        [Fact]
        public void HasDependency()
        {
            PublicClass model = Create();

            Assert.Equal(_dataReader.Object, model.DataReader);
        }
    }
}