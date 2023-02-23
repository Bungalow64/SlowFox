using ExternalProject.Net6.UnitTestMocks.Sample.NamespaceTests;
using Xunit;

namespace ExternalProject.Net6.UnitTestMocks.xUnit.Sample.NamespaceTests
{
    [SlowFox.InjectMocks(typeof(ReferenceDependencyViaPartialNamespaceAlias))]
    public partial class ReferenceDependencyViaPartialNamespaceAliasTests
    {
        [Fact]
        public void Create_ObjectsExist()
        {
            ReferenceDependencyViaPartialNamespaceAlias model = Create();

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