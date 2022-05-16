using ExternalProject.Net3_1.UnitTestMocks.Sample.NamespaceTests;
using Xunit;

namespace ExternalProject.Net3_1.UnitTestMocks.xUnit.Sample.NamespaceTests
{
    [SlowFox.InjectMocks(typeof(ReferenceDependencyViaNamespaceAlias))]
    public partial class ReferenceDependencyViaNamespaceAliasTets
    {
        [Fact]
        public void Create_ObjectsExist()
        {
            ReferenceDependencyViaNamespaceAlias model = Create();

            Assert.NotNull(model);
            Assert.NotNull(_userReader);
        }

        [Fact]
        public void Mock_CanMock()
        {
            _userReader.Setup(p => p.GetName()).Returns("Jamie");

            var name = Create().GetName();

            Assert.Equal("Jamie", name);
            _userReader.Verify(p => p.GetName(), Moq.Times.Once);
        }
    }
}