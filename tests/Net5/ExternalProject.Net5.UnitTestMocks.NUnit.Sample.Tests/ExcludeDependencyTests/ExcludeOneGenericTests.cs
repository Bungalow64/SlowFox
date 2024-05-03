using ExternalProject.Net5.UnitTestMocks.Sample.InjectableDependencies;
using ExternalProject.Net5.UnitTestMocks.Sample.MultipleDependenciesTests;
using Moq;
using NUnit.Framework;

namespace ExternalProject.Net5.UnitTestMocks.NUnit.Sample.Tests.ExcludeDependencyTests
{
    [SlowFox.InjectMocks(typeof(ThreeDependenciesWithGeneric))]
    [SlowFox.ExcludeMocks(typeof(ILogger<IUserCache>))]
    public partial class ExcludeOneGenericTests
    {
        [Test]
        public void Create_ObjectsExist()
        {
            var userCacheMock = new Mock<ILogger<IUserCache>>();

            ThreeDependenciesWithGeneric model = Create(userCacheMock.Object);

            Assert.NotNull(model);
            Assert.NotNull(_userReader);
            Assert.NotNull(_userWriter);
        }

        [Test]
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