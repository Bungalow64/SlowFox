using ExternalProject.Net5.SampleProject1.ConfigTests.NoUnderscores;
using Moq;
using Xunit;

namespace ExternalProject.Net5.SampleProject1.Tests.ConfigTests.NoUnderscores
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
