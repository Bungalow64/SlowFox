using ExternalProject.Net7.UnitTestMocks.Sample.DependencyTypeTests;
using Xunit;

namespace ExternalProject.Net7.UnitTestMocks.xUnit.Sample.Tests.DependencyTypeTests
{
    [SlowFox.InjectMocks(typeof(IntDependency))]
    public partial class IntDependencyTests
    {
        [Fact]
        public void Create_ObjectsExist()
        {
            const int index = 1001;
            IntDependency model = Create(index);

            Assert.NotNull(model);
            Assert.NotNull(_userReader);
        }

        [Fact]
        public void Mock_CanMock()
        {
            _userReader.Setup(p => p.GetName()).Returns("Jamie");

            const int index = 1001;
            var model = Create(index);

            Assert.Equal("Jamie", model.GetName());
            _userReader.Verify(p => p.GetName(), Moq.Times.Once);

            Assert.Equal(index, model.GetIndex());
        }
    }
}
