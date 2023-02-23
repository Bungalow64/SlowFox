﻿using ExternalProject.Net7.Constructors.Sample.BaseClasses;
using Xunit;

namespace ExternalProject.Net7.UnitTestMocks.xUnit.Constructors.Linked.Tests.BaseClasses
{
    [SlowFox.InjectMocks(typeof(DerivedBaseClassWithDependencyAliasPartialNamespace))]
    public partial class DerivedBaseClassWithDependencyAliasPartialNamespaceTests
    {
        [Fact]
        public void HasDependency()
        {
            DerivedBaseClassWithDependencyAliasPartialNamespace model = Create();

            Assert.Equal(_userWriter.Object, model.UserWriter);
            Assert.Equal(_userReader.Object, model.UserReader);
        }
    }
}
