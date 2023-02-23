using ExternalProject.Net7.Constructors.Sample.BaseClasses;
using Xunit;

namespace ExternalProject.Net7.UnitTestMocks.xUnit.Constructors.Linked.Tests.BaseClasses
{
    [SlowFox.InjectMocks(typeof(DerivedNestedBaseClassWithDependency))]
    public partial class DerivedNestedBaseClassWithDependencyTests
    {
        [Fact]
        public void HasDependency()
        {
            DerivedNestedBaseClassWithDependency model = Create();

            Assert.Equal(_userWriter.Object, model.UserWriter);
            Assert.Equal(_userReader.Object, model.UserReader);
            Assert.Equal(_dataReader.Object, model.DataReader);
            Assert.Equal(_dataReader2.Object, model.DataReader2);
        }
    }
}
