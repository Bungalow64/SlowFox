using ExternalProject.Net6.UnitTestMocks.Sample.NamespaceTests;
using Xunit;

namespace ExternalProject.Net6.UnitTestMocks.xUnit.Sample.ConfigTests.NoUnderscores
{
    [SlowFox.InjectMocks(typeof(ReferenceDependencyViaUsing))]
    public partial class NoUnderscoresTests
    {
        [Fact]
        public void Create_ObjectsExist()
        {
            ReferenceDependencyViaUsing model = Create();

            Assert.NotNull(model);
            Assert.NotNull(userReader);
        }

        [Fact]
        public void Mock_CanMock()
        {
            userReader.Setup(p => p.GetName()).Returns("Jamie");

            var name = Create().GetName();

            Assert.Equal("Jamie", name);
            userReader.Verify(p => p.GetName(), Moq.Times.Once);
        }
    }
}