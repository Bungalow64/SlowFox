using ExternalProject.Net6.Constructors.Sample.ConfigTests.NoUnderscores;
using Moq;
using Xunit;

namespace ExternalProject.Net6.Constructors.Sample.Tests.ConfigTests.NoUnderscores
{
    public class TestClassTests
    {
        [Fact]
        public void CanBeBuilt()
        {
            var model = new TestClass(new Mock<IDataReader>().Object);
            Assert.NotNull(model);
        }
    }
}
