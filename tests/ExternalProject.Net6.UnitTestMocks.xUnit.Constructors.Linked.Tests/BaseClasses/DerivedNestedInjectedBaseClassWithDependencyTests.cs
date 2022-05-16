using ExternalProject.Net6.Constructors.Sample.BaseClasses;
using Xunit;

namespace ExternalProject.Net6.UnitTestMocks.xUnit.Constructors.Linked.Tests.BaseClasses
{
    [SlowFox.InjectMocks(typeof(DerivedNestedInjectedBaseClassWithDependency))]
    public partial class DerivedNestedInjectedBaseClassWithDependencyTests
    {
        [Fact]
        public void HasDependency()
        {
            DerivedNestedInjectedBaseClassWithDependency model = Create();

            Assert.Equal(_userWriter.Object, model.UserWriter);
            Assert.Equal(_userReader.Object, model.UserReader);
            Assert.Equal(_dataReader2.Object, model.DataReader2);
        }
    }
}
