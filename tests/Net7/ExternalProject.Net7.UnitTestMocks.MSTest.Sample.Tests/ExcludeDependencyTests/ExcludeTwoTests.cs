using ExternalProject.Net7.UnitTestMocks.Sample.InjectableDependencies;
using ExternalProject.Net7.UnitTestMocks.Sample.MultipleDependenciesTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ExternalProject.Net7.UnitTestMocks.MSTest.Sample.ExcludeDependencyTests
{
    [SlowFox.InjectMocks(typeof(ThreeDependencies))]
    [SlowFox.ExcludeMocks(typeof(IUserCache), typeof(IUserReader))]
    [TestClass]
    public partial class ExcludeTwoTests
    {
        [TestMethod]
        public void Create_ObjectsExist()
        {
            var userCacheMock = new Mock<IUserCache>();
            var userReaderMock = new Mock<IUserReader>();

            ThreeDependencies model = Create(userReaderMock.Object, userCacheMock.Object);

            Assert.IsNotNull(model);
            Assert.IsNotNull(_userWriter);
        }

        [TestMethod]
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

            Assert.AreEqual("Jamie", model.GetName());
            model.ClearCache();

            userCacheMock
                .Verify(p => p.ClearCache(), Times.Once);

            userReaderMock
                .Verify(p => p.GetName(), Times.Once);
        }
    }
}