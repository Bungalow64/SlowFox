﻿using ExternalProject.Net7.Constructors.Sample.NamespaceTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExternalProject.Net7.UnitTestMocks.MSTest.Constructors.Linked.Tests.NamespaceTests;

[TestClass]
[SlowFox.InjectMocks(typeof(ReferenceDependencyViaTypeAlias))]
public partial class ReferenceDependencyViaTypeAliasTests
{
    [TestMethod]
    public void HasDependency()
    {
        ReferenceDependencyViaTypeAlias model = Create();

        Assert.AreEqual(_reader.Object, model.Dependency);
    }
}
