﻿using ExternalProject.Net7.Constructors.Sample.StructureTests;
using Xunit;

namespace ExternalProject.Net7.UnitTestMocks.xUnit.Constructors.Linked.Tests.NamespaceTests;

[SlowFox.InjectMocks(typeof(NestedPublicClass.InnerClass))]
public partial class NestedPublicClassTests
{
    [Fact]
    public void HasDependency()
    {
        NestedPublicClass.InnerClass model = Create();

        Assert.Equal(_dataReader.Object, model.DataReader);
    }
}
