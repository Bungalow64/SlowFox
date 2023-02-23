using ExternalProject.Net7.Constructors.Sample.ConfigTests.UsingDefaults;
using Moq;
using Xunit;

namespace ExternalProject.Net7.Constructors.Sample.Tests.ConfigTests.UsingDefaults
{
    public class TestClassTests
    {
        [Fact]
        public void CanBeBuilt()
        {
            var model = new TestClass(new Mock<IDataReader>().Object);
            Assert.NotNull(model);
        }

        [Fact]
        public void NoExceptionOnNull()
        {
            var exception = Record.Exception(() => new TestClass(null));
            Assert.Null(exception);
        }
    }
}
