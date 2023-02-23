using ExternalProject.Net3_1.Constructors.Sample.ConfigTests.WithUnderscores;
using Xunit;

namespace ExternalProject.Net3_1.UnitTestMocks.xUnit.Constructors.Linked.Tests.ConfigTests.WithUnderscores
{
    [SlowFox.InjectMocks(typeof(TestClass))]
    public partial class TestClassTests
    {
        [Fact]
        public void HasDependency()
        {
            TestClass model = Create();

            Assert.Equal(_dataReader.Object, model.DataReader);
        }
    }
}
