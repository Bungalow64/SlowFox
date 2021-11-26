﻿using ExternalProject.Net3_1.SampleProject1.ConfigTests.WithUnderscores;
using Moq;
using Xunit;

namespace ExternalProject.Net3_1.SampleProject1.Tests.ConfigTests.WithUnderscores
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
