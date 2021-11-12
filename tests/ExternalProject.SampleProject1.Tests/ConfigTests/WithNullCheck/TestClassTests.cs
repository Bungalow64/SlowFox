using ExternalProject.SampleProject1.ConfigTests.WithNullCheck;
using Moq;
using System;
using Xunit;

namespace ExternalProject.SampleProject1.Tests.ConfigTests.WithNullCheck
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
        public void ThrowExceptionOnNull()
        {
            var exception = Record.Exception(() => new TestClass(null));
            Assert.IsType<ArgumentNullException>(exception);
            Assert.Equal("Value cannot be null. (Parameter 'dataReader')", exception.Message);
        }
    }
}
