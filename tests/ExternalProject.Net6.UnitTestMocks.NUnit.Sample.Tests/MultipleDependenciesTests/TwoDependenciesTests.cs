using ExternalProject.Net6.UnitTestMocks.Sample.MultipleDependenciesTests;
using Moq;

namespace ExternalProject.Net6.UnitTestMocks.NUnit.Sample.MultipleDependenciesTests
{
    [SlowFox.InjectMocks(typeof(TwoDependencies))]
    [TestFixture]
    public partial class TwoDependenciesTests
    {
        [Test]
        public void Create_ObjectsExist()
        {
            TwoDependencies model = Create();

            Assert.IsNotNull(model);
            Assert.IsNotNull(_userReader);
            Assert.IsNotNull(_userWriter);
        }

        [Test]
        public void Mock_CanMock()
        {
            string? requestedName = null;

            _userReader.Setup(p => p.GetName()).Returns("Jamie");

            _userWriter
                .Setup(p => p.UpdateName(It.IsAny<string>()))
                .Callback<string>(p => requestedName = p);

            var model = Create();
            model.UpdateName("Jamie2");

            Assert.AreEqual("Jamie", model.GetName());
            Assert.AreEqual("Jamie2", requestedName);

            _userReader.Verify(p => p.GetName(), Times.Once);
            _userWriter.Verify(p => p.UpdateName(It.IsAny<string>()), Times.Once);
        }
    }
}