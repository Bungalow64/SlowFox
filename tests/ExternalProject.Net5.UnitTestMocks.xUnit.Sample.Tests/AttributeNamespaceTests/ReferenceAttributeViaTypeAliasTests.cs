using ExternalProject.Net5.UnitTestMocks.Sample.NamespaceTests;
using Xunit;
using I = SlowFox.InjectMocksAttribute;

namespace ExternalProject.Net5.UnitTestMocks.xUnit.Sample.AttributeNamespaceTests
{
    [I(typeof(ReferenceDependencyViaFullType))]
    public partial class ReferenceAttributeViaTypeAliasTests
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