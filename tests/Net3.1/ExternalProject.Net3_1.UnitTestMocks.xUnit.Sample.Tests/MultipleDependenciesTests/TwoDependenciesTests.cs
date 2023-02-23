using ExternalProject.Net3_1.UnitTestMocks.Sample.MultipleDependenciesTests;
using Xunit;
using Moq;

namespace ExternalProject.Net3_1.UnitTestMocks.xUnit.Sample.MultipleDependenciesTests
{
    [SlowFox.InjectMocks(typeof(TwoDependencies))]
    public partial class TwoDependenciesTests
    {
        [Fact]
        public void Create_ObjectsExist()
        {
            TwoDependencies model = Create();

            Assert.NotNull(model);
            Assert.NotNull(_userReader);
            Assert.NotNull(_userWriter);
        }

        [Fact]
        public void Mock_CanMock()
        {
            string requestedName = null;

            _userReader.Setup(p => p.GetName()).Returns("Jamie");

            _userWriter
                .Setup(p => p.UpdateName(It.IsAny<string>()))
                .Callback<string>(p => requestedName = p);

            var model = Create();
            model.UpdateName("Jamie2");

            Assert.Equal("Jamie", model.GetName());
            Assert.Equal("Jamie2", requestedName);

            _userReader.Verify(p => p.GetName(), Times.Once);
            _userWriter.Verify(p => p.UpdateName(It.IsAny<string>()), Times.Once);
        }
    }
}