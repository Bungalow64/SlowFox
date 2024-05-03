using ExternalProject.Net6.UnitTestMocks.Sample.InjectableDependencies;
using ExternalProject.Net6.UnitTestMocks.Sample.MultipleDependenciesTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ExternalProject.Net6.UnitTestMocks.MSTest.Sample.Tests.ExcludeDependencyTests
{
    [SlowFox.InjectMocks(typeof(ThreeDependenciesWithGeneric))]
    [SlowFox.ExcludeMocks(typeof(ILogger<IUserCache>))]
    public partial class ExcludeOneGenericTests
    {
        [TestMethod]
        public void Create_ObjectsExist()
        {
            var userCacheMock = new Mock<ILogger<IUserCache>>();

            ThreeDependenciesWithGeneric model = Create(userCacheMock.Object);

            Assert.IsNotNull(model);
            Assert.IsNotNull(_userReader);
            Assert.IsNotNull(_userWriter);
        }

        [TestMethod]
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