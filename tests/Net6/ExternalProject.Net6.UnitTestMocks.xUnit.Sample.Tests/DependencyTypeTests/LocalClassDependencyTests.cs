using ExternalProject.Net6.UnitTestMocks.Sample.DependencyTypeTests;
using Xunit;

namespace ExternalProject.Net6.UnitTestMocks.xUnit.Sample.Tests.DependencyTypeTests
{
    [SlowFox.InjectMocks(typeof(LocalClassDependency))]
    public partial class LocalClassDependencyTests
    {
        [Fact]
        public void Create_ObjectsExist()
        {
            LocalClassDependency model = Create();

            Assert.NotNull(model);
            Assert.NotNull(_localValue);
        }

        [Fact]
        public void Mock_CanMock()
        {
            _localValue
                .Setup(p => p.Name)
                .Returns("Jamie");

            LocalClassDependency model = Create();

            Assert.Equal("Jamie", model.GetLocalClass().Name);
        }
    }
}