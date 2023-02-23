using ExternalProject.Net3_1.UnitTestMocks.Sample.NamespaceTests;
using Xunit;

namespace ExternalProject.Net3_1.UnitTestMocks.xUnit.Sample.ConfigTests.AsStrict
{
    [SlowFox.InjectMocks(typeof(ReferenceDependencyViaUsing))]
    public partial class AsStrictTests
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
            var exception = Record.Exception(() => Create().GetName());
            Assert.IsType<Moq.MockException>(exception);
        }
    }
}