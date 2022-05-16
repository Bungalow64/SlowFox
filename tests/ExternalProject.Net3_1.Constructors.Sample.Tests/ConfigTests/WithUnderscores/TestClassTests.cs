﻿using ExternalProject.Net3_1.Constructors.Sample.ConfigTests.WithUnderscores;
using Moq;
using Xunit;

namespace ExternalProject.Net3_1.Constructors.Sample.Tests.ConfigTests.WithUnderscores
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
