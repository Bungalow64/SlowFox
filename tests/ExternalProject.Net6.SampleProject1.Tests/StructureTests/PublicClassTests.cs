﻿using ExternalProject.Net6.SampleProject1.StructureTests;
using Moq;
using Xunit;

namespace ExternalProject.Net6.SampleProject1.Tests.StructureTests
{
    public class PublicClassTests
    {
        [Fact]
        public void NestedClassHasConstructor()
        {
            var exception = Record.Exception(() => new PublicClass(new Mock<IDataReader>().Object));
            Assert.Null(exception);
        }
    }
}
