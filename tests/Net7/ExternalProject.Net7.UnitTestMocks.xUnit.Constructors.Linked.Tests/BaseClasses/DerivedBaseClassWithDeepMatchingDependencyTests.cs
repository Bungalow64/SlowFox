using ExternalProject.Net7.Constructors.Sample.BaseClasses;
using Xunit;

namespace ExternalProject.Net7.UnitTestMocks.xUnit.Constructors.Linked.Tests.BaseClasses
{
    [SlowFox.InjectMocks(typeof(DerivedBaseClassWithDeepMatchingDependency))]
    public partial class DerivedBaseClassWithDeepMatchingDependencyTests
    {
        [Fact]
        public void HasDependency()
        {
            DerivedBaseClassWithDeepMatchingDependency model = Create();

            Assert.Equal(_dataReader.Object, model.DataReader);
            Assert.Equal(_userWriter.Object, model.UserWriter);
            Assert.Equal(_userReader.Object, model.UserReader);
        }
    }
}
