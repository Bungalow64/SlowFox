using ExternalProject.Net7.UnitTestMocks.Sample.NamespaceTests;
using Xunit;

namespace ExternalProject.Net7.UnitTestMocks.xUnit.Sample.ConfigTests.AsLoose
{
    [SlowFox.InjectMocks(typeof(ReferenceDependencyViaUsing))]
    public partial class AsLooseTests
    {
        [Fact]
        public void Create_ObjectsExist()
        {
            ReferenceDependencyViaUsing model = Create();

            Assert.NotNull(model);
            Assert.NotNull(_userReader);
        }

        [Fact]
        public void Mock_CanMock()
        {
            var name = Create().GetName();

            Assert.Null(name);
            _userReader.Verify(p => p.GetName(), Moq.Times.Once);
        }
    }
}