using ExternalProject.Net3_1.Constructors.Sample.BaseClasses;
using Xunit;

namespace ExternalProject.Net3_1.UnitTestMocks.xUnit.Constructors.Linked.Tests.BaseClasses
{
    [SlowFox.InjectMocks(typeof(DerivedBaseClassUsingNoneInjected))]
    public partial class DerivedBaseClassUsingNoneInjectedTests
    {
        [Fact]
        public void HasDependency()
        {
            DerivedBaseClassUsingNoneInjected model = Create();

            Assert.Equal(_dataReader.Object, model.DataReader);
            Assert.Equal(_dataReader2.Object, model.DataReader2);
            Assert.Equal(_userReader.Object, model.UserReader);
        }
    }
}