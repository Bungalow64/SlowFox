﻿using ExternalProject.Net5.SampleProject1.ConfigTests.UsingDefaults;
using Moq;
using Xunit;

namespace ExternalProject.Net5.SampleProject1.Tests.ConfigTests.UsingDefaults
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
