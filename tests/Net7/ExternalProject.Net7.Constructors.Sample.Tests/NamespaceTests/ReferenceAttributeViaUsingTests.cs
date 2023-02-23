﻿using ExternalProject.Net7.Constructors.Sample.InjectableDependencies;
using ExternalProject.Net7.Constructors.Sample.NamespaceTests;
using Moq;
using Xunit;

namespace ExternalProject.Net7.Constructors.Sample.Tests.NamespaceTests
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
