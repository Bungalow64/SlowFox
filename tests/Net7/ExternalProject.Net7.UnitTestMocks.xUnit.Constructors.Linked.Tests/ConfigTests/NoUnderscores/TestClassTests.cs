using ExternalProject.Net7.Constructors.Sample.ConfigTests.NoUnderscores;
using Xunit;

namespace ExternalProject.Net7.UnitTestMocks.xUnit.Constructors.Linked.Tests.ConfigTests.NoUnderscores
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
