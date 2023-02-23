using ExternalProject.Net7.UnitTestMocks.Sample.DependencyTypeTests;
using Xunit;

namespace ExternalProject.Net7.UnitTestMocks.xUnit.Sample.Tests.DependencyTypeTests
{
    [SlowFox.InjectMocks(typeof(MixedSystemTypeDependencies))]
    public partial class MixedSystemTypeDependenciesTests
    {
        [Fact]
        public void Create_ObjectsExist()
        {
            MixedSystemTypeDependencies model = Create(1001, "Jamie", 10, 20, 30, 40.4);

            Assert.NotNull(model);
        }

        [Fact]
        public void Mock_CanMock()
        {
            MixedSystemTypeDependencies model = Create(1001, "Jamie", 10, 20, 30, 40.4);

            Assert.Equal(1001, model.GetIndex());
            Assert.Equal("Jamie", model.GetName());
            Assert.Equal(10, model.GetLongNumber());
            Assert.Equal(20, model.GetInt32Number());
            Assert.Equal(30, model.GetInt64Number());
            Assert.Equal(40.4, model.GetDoubleNumber());
        }
    }
}
