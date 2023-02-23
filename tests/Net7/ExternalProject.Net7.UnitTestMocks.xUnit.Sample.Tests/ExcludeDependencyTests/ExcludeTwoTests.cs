using ExternalProject.Net7.UnitTestMocks.Sample.InjectableDependencies;
using ExternalProject.Net7.UnitTestMocks.Sample.MultipleDependenciesTests;
using Xunit;
using Moq;

namespace ExternalProject.Net7.UnitTestMocks.xUnit.Sample.ExcludeDependencyTests
{
    [SlowFox.InjectMocks(typeof(ThreeDependencies))]
    [SlowFox.ExcludeMocks(typeof(IUserCache), typeof(IUserReader))]
    public partial class ExcludeTwoTests
    {
        [Fact]
        public void Create_ObjectsExist()
        {
            var userCacheMock = new Mock<IUserCache>();
            var userReaderMock = new Mock<IUserReader>();

            ThreeDependencies model = Create(userReaderMock.Object, userCacheMock.Object);

            Assert.NotNull(model);
            Assert.NotNull(_userWriter);
        }

        [Fact]
        public void Mock_CanMock()
        {
            var userCacheMock = new Mock<IUserCache>();
            var userReaderMock = new Mock<IUserReader>();

            userCacheMock
                .Setup(p => p.ClearCache());

            userReaderMock
                .Setup(p => p.GetName())
                .Returns("Jamie");

            ThreeDependencies model = Create(userReaderMock.Object, userCacheMock.Object);

            Assert.Equal("Jamie", model.GetName());
            model.ClearCache();

            userCacheMock
                .Verify(p => p.ClearCache(), Times.Once);

            userReaderMock
                .Verify(p => p.GetName(), Times.Once);
        }
    }
}