using ExternalProject.Net3_1.Constructors.Sample.BaseClasses;
using Xunit;

namespace ExternalProject.Net3_1.UnitTestMocks.xUnit.Constructors.Linked.Tests.BaseClasses
{
    [SlowFox.InjectMocks(typeof(DerivedBaseClassWithInjectedDependency))]
    public partial class DerivedBaseClassWithInjectedDependencyTests
    {
        [Fact]
        public void HasDependency()
        {
            DerivedBaseClassWithInjectedDependency model = Create();

            Assert.Equal(_userWriter.Object, model.UserWriter);
            Assert.Equal(_userReader.Object, model.UserReader);
        }
    }
}
