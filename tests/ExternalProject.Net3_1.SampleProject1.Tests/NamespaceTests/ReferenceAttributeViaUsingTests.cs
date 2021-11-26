﻿using ExternalProject.Net3_1.SampleProject1.InjectableDependencies;
using ExternalProject.Net3_1.SampleProject1.NamespaceTests;
using Moq;
using Xunit;

namespace ExternalProject.Net3_1.SampleProject1.Tests.NamespaceTests
{
    public class ReferenceAttributeViaUsingTests
    {
        [Fact]
        public void HasConstructor()
        {
            var exception = Record.Exception(() => new ReferenceAttributeViaUsing(new Mock<IUserReader>().Object));
            Assert.Null(exception);
        }
    }
}
