using NUnit.Framework;
using ExternalProject.Net5.UnitTestMocks.Sample.InjectableDependencies;
using ExternalProject.Net5.UnitTestMocks.Sample.MultipleDependenciesTests;
using Moq;

namespace ExternalProject.Net5.UnitTestMocks.NUnit.Sample.ExcludeDependencyTests
{
    [SlowFox.InjectMocks(typeof(ThreeDependencies))]
    [SlowFox.ExcludeMocks(typeof(IUserCache))]
    [TestFixture]
    public partial class ExcludeOneTests
    {
        [Test]
        public void Create_ObjectsExist()
        {
            var userCacheMock = new Mock<IUserCache>();

            ThreeDependencies model = Create(userCacheMock.Object);

            Assert.That(model, Is.Not.Null);
            Assert.That(_userReader, Is.Not.Null);
            Assert.That(_userWriter, Is.Not.Null);
        }

        [Test]
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