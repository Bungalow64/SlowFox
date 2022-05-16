﻿using ExternalProject.Net5.Constructors.Sample.NamespaceTests;
using Xunit;

namespace ExternalProject.Net5.UnitTestMocks.xUnit.Constructors.Linked.Tests.NamespaceTests
{
    [SlowFox.InjectMocks(typeof(ReferenceAttributeViaFullType))]
    public partial class ReferenceAttributeViaFullTypeTests
    {
        [Fact]
        public void HasDependency()
        {
            ReferenceAttributeViaFullType model = Create();

            Assert.Equal(_userReader.Object, model.Dependency);
        }
    }
}