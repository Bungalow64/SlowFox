using ExternalProject.Net7.Constructors.Sample.BaseClasses;
using Xunit;

namespace ExternalProject.Net7.UnitTestMocks.xUnit.Constructors.Linked.Tests.BaseClasses
{
    [SlowFox.InjectMocks(typeof(DerivedBaseClassWithNestedDependency))]
    public partial class DerivedBaseClassWithNestedDependencyTests
    {
        [Fact]
        public void HasDependency()
        {
            DerivedBaseClassWithNestedDependency model = Create();

            Assert.Equal(_userWriter.Object, model.UserWriter);
            Assert.Equal(_userReader.Object, model.UserReader);
            Assert.Equal(_dataReader.Object, model.DataReader);
        }
    }
}
