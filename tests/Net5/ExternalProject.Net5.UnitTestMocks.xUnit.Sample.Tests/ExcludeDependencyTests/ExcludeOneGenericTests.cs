using ExternalProject.Net5.UnitTestMocks.Sample.InjectableDependencies;
using ExternalProject.Net5.UnitTestMocks.Sample.MultipleDependenciesTests;
using Xunit;
using Moq;

namespace ExternalProject.Net5.UnitTestMocks.xUnit.Sample.Tests.ExcludeDependencyTests
{
    [SlowFox.InjectMocks(typeof(ThreeDependenciesWithGeneric))]
    [SlowFox.ExcludeMocks(typeof(ILogger<IUserCache>))]
    public partial class ExcludeOneGenericTests
    {
        [Fact]
        public void Create_ObjectsExist()
        {
            var userCacheMock = new Mock<ILogger<IUserCache>>();

            ThreeDependenciesWithGeneric model = Create(userCacheMock.Object);

            Assert.NotNull(model);
            Assert.NotNull(_userReader);
            Assert.NotNull(_userWriter);
        }

        [Fact]
        public void Mock_CanMock()
        {
            var userCacheMock = new Mock<ILogger<IUserCache>>();
            userCacheMock
                .Setup(p => p.Log());

            ThreeDependenciesWithGeneric model = Create(userCacheMock.Object);

            model.Log();

            userCacheMock
                .Verify(p => p.Log(), Times.Once);
        }
    }
}