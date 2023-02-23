using ExternalProject.Net6.Constructors.Sample.ConfigTests.WithUnderscores;
using Xunit;

namespace ExternalProject.Net6.UnitTestMocks.xUnit.Constructors.Linked.Tests.ConfigTests.WithUnderscores
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
