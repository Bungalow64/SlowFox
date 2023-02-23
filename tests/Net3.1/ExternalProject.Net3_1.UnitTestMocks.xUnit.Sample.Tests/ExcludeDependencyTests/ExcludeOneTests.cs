using ExternalProject.Net3_1.UnitTestMocks.Sample.InjectableDependencies;
using ExternalProject.Net3_1.UnitTestMocks.Sample.MultipleDependenciesTests;
using Xunit;
using Moq;

namespace ExternalProject.Net3_1.UnitTestMocks.xUnit.Sample.ExcludeDependencyTests
{
    [SlowFox.InjectMocks(typeof(ThreeDependencies))]
    [SlowFox.ExcludeMocks(typeof(IUserCache))]
    public partial class ExcludeOneTests
    {
        [Fact]
        public void Create_ObjectsExist()
        {
            var userCacheMock = new Mock<IUserCache>();

            ThreeDependencies model = Create(userCacheMock.Object);

            Assert.NotNull(model);
            Assert.NotNull(_userReader);
            Assert.NotNull(_userWriter);
        }

        [Fact]
        public void Mock_CanMock()
        {
            var userCacheMock = new Mock<IUserCache>();
            userCacheMock
                .Setup(p => p.ClearCache());

            ThreeDependencies model = Create(userCacheMock.Object);

            model.ClearCache();

            userCacheMock
                .Verify(p => p.ClearCache(), Times.Once);
        }
    }
}