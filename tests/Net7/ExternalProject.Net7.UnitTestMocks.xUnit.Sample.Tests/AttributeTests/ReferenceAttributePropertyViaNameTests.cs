using ExternalProject.Net7.UnitTestMocks.Sample.NamespaceTests;
using Xunit;

namespace ExternalProject.Net7.UnitTestMocks.xUnit.Sample.Tests.AttributeTests
{
    [SlowFox.InjectMocks(type: typeof(ReferenceDependencyViaFullType))]
    public partial class ReferenceAttributePropertyViaNameTests
    {
        [Fact]
        public void Create_ObjectsExist()
        {
            ReferenceDependencyViaFullType model = Create();

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