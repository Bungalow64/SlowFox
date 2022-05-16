using ExternalProject.Net6.UnitTestMocks.Sample.MultipleDependenciesTests;
using Xunit;

namespace ExternalProject.Net6.UnitTestMocks.xUnit.Sample.MultipleDependenciesTests
{
    [SlowFox.InjectMocks(typeof(DuplicateDependency))]
    public partial class DuplicateDependencyTests
    {
        [Fact]
        public void Create_ObjectsExist()
        {
            DuplicateDependency model = Create();

            Assert.NotNull(model);
            Assert.NotNull(_userReader1);
            Assert.NotNull(_userReader2);
            Assert.NotNull(_userWriter);
        }

        [Fact]
        public void Mock_CanMock()
        {
            _userReader1.Setup(p => p.GetName()).Returns("Jamie1");
            _userReader2.Setup(p => p.GetName()).Returns("Jamie2");

            var model = Create();

            Assert.Equal("Jamie1", model.GetName1());
            Assert.Equal("Jamie2", model.GetName2());

            _userReader1.Verify(p => p.GetName(), Moq.Times.Once);
            _userReader2.Verify(p => p.GetName(), Moq.Times.Once);
        }
    }
}