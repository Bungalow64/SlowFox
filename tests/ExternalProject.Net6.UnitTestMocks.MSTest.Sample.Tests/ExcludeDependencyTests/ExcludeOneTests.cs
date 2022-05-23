using ExternalProject.Net6.UnitTestMocks.Sample.InjectableDependencies;
using ExternalProject.Net6.UnitTestMocks.Sample.MultipleDependenciesTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ExternalProject.Net6.UnitTestMocks.MSTest.Sample.ExcludeDependencyTests
{
    [SlowFox.InjectMocks(typeof(ThreeDependencies))]
    [SlowFox.ExcludeMocks(typeof(IUserCache))]
    [TestClass]
    public partial class ExcludeOneTests
    {
        [TestMethod]
        public void Create_ObjectsExist()
        {
            var userCacheMock = new Mock<IUserCache>();

            ThreeDependencies model = Create(userCacheMock.Object);

            Assert.IsNotNull(model);
            Assert.IsNotNull(_userReader);
            Assert.IsNotNull(_userWriter);
        }

        [TestMethod]
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