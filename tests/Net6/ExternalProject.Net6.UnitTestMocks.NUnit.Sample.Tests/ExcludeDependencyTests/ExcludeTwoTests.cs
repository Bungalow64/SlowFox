using ExternalProject.Net6.UnitTestMocks.Sample.InjectableDependencies;
using ExternalProject.Net6.UnitTestMocks.Sample.MultipleDependenciesTests;
using Moq;

namespace ExternalProject.Net6.UnitTestMocks.NUnit.Sample.ExcludeDependencyTests
{
    [SlowFox.InjectMocks(typeof(ThreeDependencies))]
    [SlowFox.ExcludeMocks(typeof(IUserCache), typeof(IUserReader))]
    [TestFixture]
    public partial class ExcludeTwoTests
    {
        [Test]
        public void Create_ObjectsExist()
        {
            var userCacheMock = new Mock<IUserCache>();
            var userReaderMock = new Mock<IUserReader>();

            ThreeDependencies model = Create(userReaderMock.Object, userCacheMock.Object);

            Assert.That(model, Is.Not.Null);
            Assert.That(_userWriter, Is.Not.Null);
        }

        [Test]
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

            Assert.That(model.GetName(), Is.EqualTo("Jamie"));
            model.ClearCache();

            userCacheMock
                .Verify(p => p.ClearCache(), Times.Once);

            userReaderMock
                .Verify(p => p.GetName(), Times.Once);
        }
    }
}