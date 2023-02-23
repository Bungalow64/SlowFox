using ExternalProject.Net6.Constructors.Sample.ConfigTests.WithNoNullCheck;
using Moq;
using Xunit;

namespace ExternalProject.Net6.Constructors.Sample.Tests.ConfigTests.WithNoNullCheck
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
